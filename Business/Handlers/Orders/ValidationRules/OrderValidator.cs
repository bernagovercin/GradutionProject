﻿
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OrderItems).NotEmpty();

        }
    }
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OrderItems).NotEmpty();

        }
    }
}