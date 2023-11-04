using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetAllUserTasks
{
    public record GetOpenTasksQuery(Guid UserId, string SearchText = "", DateTime? DueDateMin = null, DateTime? DueDateMax = null) : IRequest<IEnumerable<TodoTask>>;
}