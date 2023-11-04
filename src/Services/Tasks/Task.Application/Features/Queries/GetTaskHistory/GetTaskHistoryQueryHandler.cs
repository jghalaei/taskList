using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetTaskHistory;

public class GetTaskHistoryQueryHandler : IRequestHandler<GetTaskHistoryQuery, IEnumerable<TaskHistory>>
{
    private readonly IRepository<TaskHistory> _repository;

    public GetTaskHistoryQueryHandler(IRepository<TaskHistory> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskHistory>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(t => t.TaskId == request.TaskId);
    }
}