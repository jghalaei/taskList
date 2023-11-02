using GenericContracts.Contracts;
using MediatR;
using User.Application.PasswordHash;
using User.Application.Services;
using User.Core.Entities;

namespace User.Application.Features.Users.Queries.CheckPassword
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly JwtService _jwtService;
        public LoginQueryHandler(IRepository<AppUser> repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser? user = await _repository.GetOneAsync(u => u.UserName == request.Username);
            if (user == null)
                return string.Empty;
            bool isLoggedIn = PasswordHasher.VerifyPassword(request.GivenPassword, user.Salt, user.Password);
            if (!isLoggedIn)
                return string.Empty;
            return _jwtService.GenerateToken(user.Id, user.UserName);
        }
    }
}