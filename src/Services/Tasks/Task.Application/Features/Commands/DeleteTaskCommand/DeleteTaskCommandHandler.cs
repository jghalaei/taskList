using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using MediatR;
using Task.Core.Entities;

namespace Task.Application.Features.Commands.DeleteTaskCommand
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IRepository<TodoTask> _repository;

        public DeleteTaskCommandHandler(IRepository<TodoTask> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}