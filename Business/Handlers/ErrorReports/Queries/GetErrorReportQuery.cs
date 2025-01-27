
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ErrorReports.Queries
{
    public class GetErrorReportQuery : IRequest<IDataResult<ErrorReport>>
    {
        public int CreatedUserId { get; set; }

        public class GetErrorReportQueryHandler : IRequestHandler<GetErrorReportQuery, IDataResult<ErrorReport>>
        {
            private readonly IErrorReportRepository _errorReportRepository;
            private readonly IMediator _mediator;

            public GetErrorReportQueryHandler(IErrorReportRepository errorReportRepository, IMediator mediator)
            {
                _errorReportRepository = errorReportRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ErrorReport>> Handle(GetErrorReportQuery request, CancellationToken cancellationToken)
            {
                var errorReport = await _errorReportRepository.GetAsync(p => p.CreatedUserId == request.CreatedUserId);
                return new SuccessDataResult<ErrorReport>(errorReport);
            }
        }
    }
}
