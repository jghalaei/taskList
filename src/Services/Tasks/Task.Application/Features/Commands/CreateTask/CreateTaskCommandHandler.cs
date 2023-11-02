using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Task.Application.Entities;
using TaskList.Domain.Contracts.Persistence;

namespace Task.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        readonly IRepository<ToDoTask> _repository;
        readonly IMapper _mapper;
        public CreateTaskCommandHandler(IRepository<ToDoTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<ToDoTask>(request);
            var result = await _repository.InsertAsync(todo);
            return result;
        }
    }
}