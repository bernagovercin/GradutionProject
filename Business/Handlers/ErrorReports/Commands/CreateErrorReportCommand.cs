
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
using Business.Handlers.ErrorReports.ValidationRules;

namespace Business.Handlers.ErrorReports.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateErrorReportCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Component { get; set; }
        public string ResolutionMessage { get; set; }
        public string ErrorStatus { get; set; }


        public class CreateErrorReportCommandHandler : IRequestHandler<CreateErrorReportCommand, IResult>
        {
            private readonly IErrorReportRepository _errorReportRepository;
            private readonly IMediator _mediator;
            public CreateErrorReportCommandHandler(IErrorReportRepository errorReportRepository, IMediator mediator)
            {
                _errorReportRepository = errorReportRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateErrorReportValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateErrorReportCommand request, CancellationToken cancellationToken)
            {
                var isThereErrorReportRecord = _errorReportRepository.Query().Any(u => u.Title == request.Title && u.Severity == request.Severity);

                if (isThereErrorReportRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedErrorReport = new ErrorReport
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Severity = request.Severity,
                    Component = request.Component,
                    ResolutionMessage = request.ResolutionMessage,
                    ErrorStatus = request.ErrorStatus,

                };

                _errorReportRepository.Add(addedErrorReport);
                await _errorReportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}