using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetAllUserTasks
{
    public record GetAllUserTasksQuery(Guid UserId, List<ETodoTaskStatus>? AcceptableStates, string SearchText = "", DateTime? DueDateMin = null, DateTime? DueDateMax = null) : IRequest<IEnumerable<TodoTask>>;
}