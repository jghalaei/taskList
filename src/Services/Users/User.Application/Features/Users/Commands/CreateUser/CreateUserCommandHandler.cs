using GenericContracts.Contracts;
using MediatR;
using User.Application.Features.Users.Queries;
using User.Application.PasswordHash;
using User.Core.Entities;

namespace User.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserVm>
    {
        private IRepository<AppUser> _repository;

        public CreateUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<UserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string hashedPass = PasswordHasher.HashPassword(request.Password, out string salt);
            AppUser user = new AppUser()
            {
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = hashedPass,
                Salt = salt
            };
            var result = await _repository.InsertAsync(user);
            return UserVm.MapToUserVm(result);
        }
    }
}