
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


namespace Business.Handlers.ErrorReports.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteErrorReportCommand : IRequest<IResult>
    {
        public string Title { get; set; }
        public string Severity { get; set; }

        public class DeleteErrorReportCommandHandler : IRequestHandler<DeleteErrorReportCommand, IResult>
        {
            private readonly IErrorReportRepository _errorReportRepository;
            private readonly IMediator _mediator;

            public DeleteErrorReportCommandHandler(IErrorReportRepository errorReportRepository, IMediator mediator)
            {
                _errorReportRepository = errorReportRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteErrorReportCommand request, CancellationToken cancellationToken)
            {
                var errorReportToDelete = _errorReportRepository.Get(p => p.Title == request.Title && p.Severity == request.Severity); ;

                _errorReportRepository.Delete(errorReportToDelete);
                await _errorReportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

