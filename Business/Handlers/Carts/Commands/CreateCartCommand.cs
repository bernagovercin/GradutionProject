
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Carts.ValidationRules;

namespace Business.Handlers.Carts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCartCommand : IRequest<IResult>
    {

        public int UserId { get; set; }
        public System.Collections.Generic.List<Cartİtem> CartItems { get; set; }


        public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, IResult>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IMediator _mediator;
            public CreateCartCommandHandler(ICartRepository cartRepository, IMediator mediator)
            {
                _cartRepository = cartRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCartValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
            {
                var isThereCartRecord = _cartRepository.Query().Any(u => u.UserId == request.UserId);

                if (isThereCartRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCart = new Cart
                {
                    UserId = request.UserId,
                    CartItems = request.CartItems,

                };

                _cartRepository.Add(addedCart);
                await _cartRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}