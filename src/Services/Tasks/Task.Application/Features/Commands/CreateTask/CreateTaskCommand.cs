using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Task.Application.Features.Commands.CreateTask
{
    public record CreateTaskCommand(Guid UserId, string Title, DateTime DueDate = default, string Comment = "") : IRequest<Guid>;
}