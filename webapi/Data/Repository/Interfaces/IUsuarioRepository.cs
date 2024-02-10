using webapi.Models;

namespace webapi.Data.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuarios GetUsuarioByUserName(string userName);
        void AddNewUsuario(string userName);
        bool HasUserBuUserName(string userName);
    }
}
