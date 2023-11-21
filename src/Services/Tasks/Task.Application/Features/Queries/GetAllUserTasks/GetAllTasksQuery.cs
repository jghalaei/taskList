using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetAllUserTasks
{
    public record GetAllTasksQuery(List<ETodoTaskStatus>? AcceptableStates, string SearchText = "", DateTime? DueDateMin = null, DateTime? DueDateMax = null) : IRequest<IEnumerable<TaskViewModel>>;
}