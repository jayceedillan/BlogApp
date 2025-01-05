using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;

namespace BlogApp.Application.Users.Queries.GetUsers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationUser?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve user by ID from the repository
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            return user;
        }
    }
}
