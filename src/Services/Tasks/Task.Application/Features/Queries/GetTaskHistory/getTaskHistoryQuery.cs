using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetTaskHistory
{
    public class GetTaskHistoryQuery : IRequest<IEnumerable<TaskHistory>>
    {
        public Guid TaskId { get; set; }
    }
}