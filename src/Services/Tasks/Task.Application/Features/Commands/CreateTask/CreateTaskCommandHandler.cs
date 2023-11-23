using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskViewModel>
    {
        readonly IRepository<TodoTask> _repository;
        readonly IMapper _mapper;
        readonly IUserAccessor _userAccessor;
        public CreateTaskCommandHandler(IRepository<TodoTask> repository, IMapper mapper, IUserAccessor userAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<TaskViewModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<TodoTask>(request);
            todo.UserId = _userAccessor.UserId;
            if (await _repository.GetOneAsync(t => t.Title == todo.Title && t.UserId == todo.UserId) != null)
                throw new InvalidDataException("Task already exists");
                todo.DueDate = todo.DueDate.ToUniversalTime();
            var addedTask = await _repository.InsertAsync(todo);

            return _mapper.Map<TaskViewModel>(addedTask);
        }
    }
}
