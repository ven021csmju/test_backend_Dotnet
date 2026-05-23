using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface IAuthService
{
    Task<TokenResponse?> LoginAsync(LoginRequest request);
    Task<UserResponse?> GetMeAsync(string username);
}
