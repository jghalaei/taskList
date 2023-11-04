using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Features.Commands.ChangeTaskStatus;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.EditTaskCommand
{
    public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, Guid>
    {
        private readonly IRepository<TodoTask> _repository;
        private readonly IMapper _mapper;
        private readonly Mediator _mediator;
        public EditTaskCommandHandler(IRepository<TodoTask> repository, IMapper mapper, Mediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            TodoTask? oldTodo = await _repository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(oldTodo);
            TodoTask updatingTodo = _mapper.Map<TodoTask>(request);

            if (oldTodo.Status != updatingTodo.Status)
            {
                ChangeTaskStatusCommand cmd = new ChangeTaskStatusCommand(oldTodo.Id, updatingTodo.Status);
                await _mediator.Send(cmd);
            }
            var result = await _repository.UpdateAsync(updatingTodo);
            return result;
        }
    }
}