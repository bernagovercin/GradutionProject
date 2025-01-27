
using Business.Handlers.Customers.Commands;
using FluentValidation;

namespace Business.Handlers.Customers.ValidationRules
{

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();

        }
    }
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();

        }
    }
}