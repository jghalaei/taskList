using GenericContracts.Contracts;
using MediatR;
using User.Core.Entities;

namespace User.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Guid>
    {
        IRepository<AppUser> _repository;
        public EditUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _repository.GetOneAsync(u => u.UserName == request.UserName);
            if (user == null)
                throw new InvalidDataException("User not found");
            user.Name = request.Name;
            user.UserName = request.UserName;
            user.Email = request.Email;
            return await _repository.UpdateAsync(user);
        }
    }
}