
using Business.Handlers.ErrorReports.Commands;
using FluentValidation;

namespace Business.Handlers.ErrorReports.ValidationRules
{

    public class CreateErrorReportValidator : AbstractValidator<CreateErrorReportCommand>
    {
        public CreateErrorReportValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Severity).NotEmpty();
            RuleFor(x => x.Component).NotEmpty();
            RuleFor(x => x.ResolutionMessage).NotEmpty();
            RuleFor(x => x.ErrorStatus).NotEmpty();

        }
    }
    public class UpdateErrorReportValidator : AbstractValidator<UpdateErrorReportCommand>
    {
        public UpdateErrorReportValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Severity).NotEmpty();
            RuleFor(x => x.Component).NotEmpty();
            RuleFor(x => x.ResolutionMessage).NotEmpty();
            RuleFor(x => x.ErrorStatus).NotEmpty();

        }
    }
}