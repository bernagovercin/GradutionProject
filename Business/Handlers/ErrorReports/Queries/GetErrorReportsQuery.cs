
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.ErrorReports.Queries
{

    public class GetErrorReportsQuery : IRequest<IDataResult<IEnumerable<ErrorReport>>>
    {
        public class GetErrorReportsQueryHandler : IRequestHandler<GetErrorReportsQuery, IDataResult<IEnumerable<ErrorReport>>>
        {
            private readonly IErrorReportRepository _errorReportRepository;
            private readonly IMediator _mediator;

            public GetErrorReportsQueryHandler(IErrorReportRepository errorReportRepository, IMediator mediator)
            {
                _errorReportRepository = errorReportRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ErrorReport>>> Handle(GetErrorReportsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ErrorReport>>(await _errorReportRepository.GetListAsync());
            }
        }
    }
}