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

        public async Task SalvarDadosContratos(List<Contratos> contratos)
        {
            await _context.Contratos.AddRangeAsync(contratos);
            await _context.SaveChangesAsync();
        }
    }
}
