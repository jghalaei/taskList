using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.EditTaskCommand
{
    public record EditTaskCommand(Guid Id, string Title, DateTime DueDate, ETodoTaskStatus Status, string Comment) : IRequest<Guid>
    {
    }
}