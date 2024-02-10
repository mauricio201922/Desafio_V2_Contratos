using webapi.Context;
using webapi.Data.Repository.Interfaces;
using webapi.Models;

namespace webapi.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;
        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Usuarios GetUsuarioByUserName(string userName)
        {
            return _context.Usuarios.First(x => x.Name == userName);
        }
        public void AddNewUsuario(string userName)
        {
            _context.Add(new Usuarios
            {
                Name = userName,
            });
            _context.SaveChanges();
        }
        public bool HasUserBuUserName(string userName)
        {
            return _context.Usuarios.Any(a => a.Name == userName);
        }
    }
}
