using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Application.ViewModels;
using Task.Core.Entities;
using MassTransit;
using GenericContracts.EventBusMessages;
using Elasticsearch.Net;
namespace Task.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskViewModel>
    {
        readonly IRepository<TodoTask> _repository;
        readonly IMapper _mapper;
        readonly IUserAccessor _userAccessor;
        readonly IPublishEndpoint _publishEndpoint;
        public CreateTaskCommandHandler(IRepository<TodoTask> repository, IMapper mapper, IUserAccessor userAccessor, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TaskViewModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<TodoTask>(request);
            todo.UserId = _userAccessor.UserId;
            if (await _repository.GetOneAsync(t => t.Title == todo.Title && t.UserId == todo.UserId) != null)
            {
                throw new InvalidDataException("Task already exists");
            }
            todo.DueDate = todo.DueDate.ToUniversalTime();
            var addedTask = await _repository.InsertAsync(todo);

            await _publishEndpoint.Publish(new TaskCreatedEvent(addedTask.UserId, addedTask.DueDate), cancellationToken);

            return _mapper.Map<TaskViewModel>(addedTask);

        }

    }
}
