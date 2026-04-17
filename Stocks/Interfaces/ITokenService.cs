using StocksOperation.Models;

namespace StocksOperation.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
