using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Microsoft.VisualBasic;
using Task.Application.Accessors;
using Task.Application.Features.Commands.ChangeTaskStatus;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.EditTaskCommand
{
    public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, TaskViewModel>
    {
        private readonly IRepository<TodoTask> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public EditTaskCommandHandler(IRepository<TodoTask> repository, IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        public async Task<TaskViewModel> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            TodoTask? oldTodo = await _repository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(oldTodo);
            if (oldTodo.UserId != _userAccessor.UserId)
                throw new InvalidDataException("You are not allowed to edit this task");

            TodoTask updatingTodo = _mapper.Map<TodoTask>(request);
            if (oldTodo.Status != updatingTodo.Status)
            {
                ChangeTaskStatusCommand cmd = new ChangeTaskStatusCommand(oldTodo.Id, updatingTodo.Status);
                await _mediator.Send(cmd, cancellationToken);
            }
            updatingTodo.UserId = oldTodo.UserId;
            updatingTodo.DueDate = updatingTodo.DueDate.ToUniversalTime();
            var result = await _repository.UpdateAsync(updatingTodo);
            return _mapper.Map<TaskViewModel>(result);
        }
    }
}