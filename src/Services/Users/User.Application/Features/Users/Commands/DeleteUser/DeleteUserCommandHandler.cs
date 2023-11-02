using GenericContracts.Contracts;
using MediatR;
using User.Core.Entities;

namespace User.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        IRepository<AppUser> _repository;

        public DeleteUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}