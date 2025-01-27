
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;


namespace Business.Handlers.WareHouses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWareHouseCommand : IRequest<IResult>
    {
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        


        public class DeleteWareHouseCommandHandler : IRequestHandler<DeleteWareHouseCommand, IResult>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public DeleteWareHouseCommandHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteWareHouseCommand request, CancellationToken cancellationToken)
            {
                var wareHouseToDelete = _wareHouseRepository.Get(p => p.Category == request.Category
                                                                  && p.ProductName == request.ProductName
                                                                  && p.ColorName == request.ColorName
                                                                 );
                _wareHouseRepository.Delete(wareHouseToDelete);
                await _wareHouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

