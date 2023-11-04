using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public record ChangeTaskStatusCommand(Guid Id, ETodoTaskStatus Status) : IRequest<Guid>;
}