using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace User.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("{Name} is required.");
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{Email} is required.")
                .EmailAddress().WithMessage("{Email} must be a valid email address.");
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty().WithMessage("{Password} is required.");
        }
    }
}