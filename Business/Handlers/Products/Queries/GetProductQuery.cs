
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Products.Queries
{
    public class GetProductQuery : IRequest<IDataResult<Product>>
    {
        public int CreatedUserId { get; set; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IDataResult<Product>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(p => p.CreatedUserId == request.CreatedUserId);
                return new SuccessDataResult<Product>(product);
            }
        }
    }
}
