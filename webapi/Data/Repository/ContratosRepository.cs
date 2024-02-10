using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Data.Repository.Interfaces;
using webapi.Models;

namespace webapi.Data.Repository
{
    public class ContratosRepository : IContratosRepository
    {
        private readonly ApplicationContext _context;
        public ContratosRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Contratos> GetAll()
        {
            return _context.Contratos.AsEnumerable();
        }

        public async Task SalvarDadosContratos(List<Contratos> contratos, int usuarioId, string path)
        {
            await _context.Files.AddAsync(new Files
            {
                Path = path,
                UsuarioId = usuarioId,
            });
            await _context.Contratos.AddRangeAsync(contratos);
            await _context.SaveChangesAsync();
        }

        public List<Files> GetFileContratos()
        {
            return _context.Files.Include(u => u.Usuario).ToList();
        }

        public List<Files> GetFileContratosByPath(string path)
        {
            return _context.Files.Include(u => u.Usuario).Where(w => w.Path == path).Select(s => new Files
            {
                Path = s.Path,
                Usuario = s.Usuario
            }).ToList();
        }

        public bool HasFileContratoInPath(string path)
        {
            return _context.Files.Any(a => a.Path == path);
        }
    }
}
