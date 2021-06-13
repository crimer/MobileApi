using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using MobileApi.Core.Repository;
using MobileApi.Core.Services.User.Models;

namespace MobileApi.Core.Services.User
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public Task<Result<List<UserProfile>>> GetUserProfile()
        {
            return _userRepository.GetUserProfile();
        }
    }
}