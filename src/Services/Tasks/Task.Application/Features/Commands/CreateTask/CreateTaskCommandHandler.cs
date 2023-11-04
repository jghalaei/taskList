using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        readonly IRepository<TodoTask> _repository;
        readonly IMapper _mapper;
        public CreateTaskCommandHandler(IRepository<TodoTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<TodoTask>(request);
            var result = await _repository.InsertAsync(todo);
            return result;
        }
    }
}