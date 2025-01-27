
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ErrorReports.ValidationRules;


namespace Business.Handlers.ErrorReports.Commands
{


    public class UpdateErrorReportCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
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

        public class UpdateErrorReportCommandHandler : IRequestHandler<UpdateErrorReportCommand, IResult>
        {
            private readonly IErrorReportRepository _errorReportRepository;
            private readonly IMediator _mediator;

            public UpdateErrorReportCommandHandler(IErrorReportRepository errorReportRepository, IMediator mediator)
            {
                _errorReportRepository = errorReportRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateErrorReportValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateErrorReportCommand request, CancellationToken cancellationToken)
            {
                var isThereErrorReportRecord = await _errorReportRepository.GetAsync(u => u.CreatedUserId == request.CreatedUserId);


                isThereErrorReportRecord.CreatedDate = request.CreatedDate;
                isThereErrorReportRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereErrorReportRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereErrorReportRecord.Status = request.Status;
                isThereErrorReportRecord.IsDeleted = request.IsDeleted;
                isThereErrorReportRecord.Id = request.Id;
                isThereErrorReportRecord.Title = request.Title;
                isThereErrorReportRecord.Description = request.Description;
                isThereErrorReportRecord.Severity = request.Severity;
                isThereErrorReportRecord.Component = request.Component;
                isThereErrorReportRecord.ResolutionMessage = request.ResolutionMessage;
                isThereErrorReportRecord.ErrorStatus = request.ErrorStatus;


                _errorReportRepository.Update(isThereErrorReportRecord);
                await _errorReportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

