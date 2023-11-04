using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandValidator : AbstractValidator<ChangeTaskStatusCommand>
    {
        public ChangeTaskStatusCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("{UserId} is required.");
            RuleFor(u => u.Status).IsInEnum().WithMessage("{Status} is required.");
        }
    }
}