using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Guid>
    {
        IRepository<TaskHistory> _historyRepository;
        IRepository<TodoTask> _todoRepository;
        IUserAccessor _accessor;
        public ChangeTaskStatusCommandHandler(IRepository<TaskHistory> historyRepository, IRepository<TodoTask> todoRepository, IUserAccessor accessor)
        {
            _historyRepository = historyRepository;
            _todoRepository = todoRepository;
            _accessor = accessor;
        }


        public async Task<Guid> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            TodoTask? todo = await _todoRepository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(todo);
            if (todo.Status == request.Status)
                throw new InvalidDataException("Task already in this status");
            if (todo.UserId != _accessor.UserId)
                throw new InvalidDataException("You are not allowed to edit this task");
            TaskHistory history = new TaskHistory()
            {
                TaskId = todo.Id,
                UserId = todo.UserId,
                OldStatus = todo.Status,
                NewStatus = request.Status
            };
            todo.Status = request.Status;
            todo.DueDate=todo.DueDate.ToUniversalTime();
            await _todoRepository.UpdateAsync(todo);
            await _historyRepository.InsertAsync(history);
            return todo.Id;
        }
    }
}