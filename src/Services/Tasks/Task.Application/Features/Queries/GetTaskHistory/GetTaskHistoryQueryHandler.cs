using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetTaskHistory;

public class GetTaskHistoryQueryHandler : IRequestHandler<GetTaskHistoryQuery, IEnumerable<TaskHistory>>
{
    private readonly IRepository<TodoTask> _taskRepository;
    private readonly IRepository<TaskHistory> _repository;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;
    public GetTaskHistoryQueryHandler(IRepository<TaskHistory> repository, IMapper mapper, IRepository<TodoTask> taskRepository, IUserAccessor userAccessor)
    {
        _repository = repository;
        _mapper = mapper;
        _taskRepository = taskRepository;
        _userAccessor = userAccessor;
    }

    public async Task<IEnumerable<TaskHistory>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
    {
        var todo = await _taskRepository.GetByIdAsync(request.TaskId);
        ArgumentNullException.ThrowIfNull(todo);
        if (todo.UserId != _userAccessor.UserId)
            throw new InvalidDataException("You are not allowed to get this task");
        var result = await _repository.GetAllAsync(t => t.TaskId == request.TaskId);
        return _mapper.Map<IEnumerable<TaskHistory>>(result);
    }
}