using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using MobileApi.Core.Services.User.Models;

namespace MobileApi.Core.Repository
{
    public interface IUserRepository
    {
        Task<Result<List<UserProfile>>> GetUserProfile();
    }
}