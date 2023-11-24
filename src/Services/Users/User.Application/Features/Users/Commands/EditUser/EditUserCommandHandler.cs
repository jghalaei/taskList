using GenericContracts.Contracts;
using MediatR;
using User.Application.Features.Users.Queries;
using User.Core.Entities;

namespace User.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserVm>
    {
        IRepository<AppUser> _repository;
        public EditUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<UserVm> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _repository.GetOneAsync(u => u.UserName == request.UserName);
            if (user == null)
                throw new InvalidDataException("User not found");
            user.Name = request.Name;
            user.UserName = request.UserName;
            user.Email = request.Email;
            var result= await _repository.UpdateAsync(user);
            return UserVm.MapToUserVm(result);
        }
    }
}