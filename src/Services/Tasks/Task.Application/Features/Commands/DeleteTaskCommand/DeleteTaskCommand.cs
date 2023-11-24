using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Task.Application.Features.Commands.DeleteTaskCommand
{
    public record DeleteTaskCommand(Guid Id) : IRequest<bool>;
}