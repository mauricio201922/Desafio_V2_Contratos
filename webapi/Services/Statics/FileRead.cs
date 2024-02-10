using Microsoft.Win32;
using webapi.Models;

namespace webapi.Services.Statics
{
    public static class FileRead
    {
        public static List<Contratos> LerArquivoCsv(string caminhoArquivo)
        {
            var contratos = new List<Contratos>();
            int indexAtual = 0;
            
            using (var reader = new StreamReader(caminhoArquivo))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line.Split(';');
                    if (indexAtual >= 1)
                    {
                        contratos.Add(new Contratos
                        {
                            Nome = fields[0],
                            CPF = fields[1],
                            Contrato = Convert.ToInt32(fields[2]),
                            Produto = fields[3],
                            Vencimento = fields[4],
                            Valor = Convert.ToDouble(fields[5]),
                            UsuarioId = 1
                        });
                    }
                    indexAtual++;
                }
            }
            return contratos;
        }
    }
}
