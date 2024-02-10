using webapi.Models;

namespace webapi.Data.Repository.Interfaces
{
    public interface IContratosRepository
    {
        IEnumerable<Contratos> GetAll();
        Task SalvarDadosContratos(List<Contratos> contratos, int usuarioId, string path);
        List<Files> GetFileContratos();
        List<Files> GetFileContratosByPath(string path);
        bool HasFileContratoInPath(string path);
    }
}
