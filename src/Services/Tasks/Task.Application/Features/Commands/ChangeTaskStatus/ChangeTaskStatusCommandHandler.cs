using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using GenericContracts.EventBusMessages;
using MassTransit;
using MediatR;
using Task.Application.Accessors;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Guid>
    {
        private readonly IRepository<TaskHistory> _historyRepository;
        private readonly IRepository<TodoTask> _todoRepository;
        private readonly IUserAccessor _accessor;
        private readonly IPublishEndpoint _publishEndpoint;
        public ChangeTaskStatusCommandHandler(IRepository<TaskHistory> historyRepository, IRepository<TodoTask> todoRepository, IUserAccessor accessor, IPublishEndpoint publishEndpoint)
        {
            _historyRepository = historyRepository;
            _todoRepository = todoRepository;
            _accessor = accessor;
            _publishEndpoint = publishEndpoint;
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
            todo.DueDate = todo.DueDate.ToUniversalTime();
            await _todoRepository.UpdateAsync(todo);
            await _historyRepository.InsertAsync(history);
            await _publishEndpoint.Publish(new TaskStatusUpdatedEvent(todo.UserId, todo.DueDate, history.OldStatus, history.NewStatus), cancellationToken);
            return todo.Id;
        }
    }
}