using webapi.Models;

namespace webapi.Data.Repository.Interfaces
{
    public interface IContratosRepository
    {
        IEnumerable<Contratos> GetAll();
        Task SalvarDadosContratos(List<Contratos> contratos);
    }
}
