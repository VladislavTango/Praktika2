
namespace PraktikaDomain.Interfaces
{
    public interface IJwtTokentService
    {
        public string GenerateToken(int userId, string userEmail);
        public (int userId, string userEmail) TokenClaimCatcher(string token);
    }
}
