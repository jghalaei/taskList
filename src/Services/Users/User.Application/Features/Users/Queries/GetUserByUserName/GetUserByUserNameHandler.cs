using MediatR;
using GenericContracts.Contracts;
using User.Core.Entities;

namespace User.Application.Features.Users.Queries.GetUserByUserName
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserNameQuery, UserVm?>
    {

        private readonly IRepository<AppUser> _userRepository;


        public GetUserByUserNameHandler(IRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserVm?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(u => u.UserName == request.UserName);
            if (user == null)
                return null;
            return UserVm.MapToUserVm(user);

        }
    }
}