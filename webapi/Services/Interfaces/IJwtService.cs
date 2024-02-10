using webapi.Models;

namespace webapi.Services.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(Usuarios? user);
    }
}
