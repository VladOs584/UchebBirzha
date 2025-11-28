using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}