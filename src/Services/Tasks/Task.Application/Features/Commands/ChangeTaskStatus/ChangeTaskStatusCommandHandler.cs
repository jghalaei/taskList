using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Guid>
    {
        IRepository<TaskHistory> _historyRepository;
        IRepository<TodoTask> _todoRepository;
        public ChangeTaskStatusCommandHandler(IRepository<TaskHistory> historyRepository, IRepository<TodoTask> todoRepository)
        {
            _historyRepository = historyRepository;
            _todoRepository = todoRepository;
        }


        public async Task<Guid> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            TodoTask? todo = await _todoRepository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(todo);
            if (todo.Status == request.Status)
                throw new InvalidDataException("Task already in this status");
            TaskHistory history = new TaskHistory()
            {
                TaskId = todo.Id,
                UserId = todo.UserId,
                OldStatus = todo.Status,
                NewStatus = request.Status
            };
            todo.Status = request.Status;
            await _todoRepository.UpdateAsync(todo);
            await _historyRepository.InsertAsync(history);
            return todo.Id;
        }
    }
}