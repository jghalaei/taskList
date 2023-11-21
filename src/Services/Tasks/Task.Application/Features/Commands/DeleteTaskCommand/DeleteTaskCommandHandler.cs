using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.DeleteTaskCommand
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IRepository<TodoTask> _repository;
        private readonly IUserAccessor _accessor;
        public DeleteTaskCommandHandler(IRepository<TodoTask> repository, IUserAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(todo);
            if (todo.UserId != _accessor.UserId)
                throw new InvalidDataException("You are not allowed to delete this task");
            return await _repository.DeleteAsync(request.Id);
        }
    }
}