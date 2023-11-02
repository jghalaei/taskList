using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace User.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
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
        }
    }
}