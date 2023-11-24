using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Task.Application.Features.Commands.EditTaskCommand
{
    public class EditTaskCommandValidator : AbstractValidator<EditTaskCommand>
    {
        public EditTaskCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("{UserId} is required.");
            RuleFor(u => u.Title).NotEmpty().WithMessage("{Title} is required.");
        }
    }
}