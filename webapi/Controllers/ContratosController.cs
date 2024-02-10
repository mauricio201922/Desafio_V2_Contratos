using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Formats.Asn1;
using System.Globalization;
using webapi.Data.Repository.Interfaces;
using webapi.Models;
using webapi.Services.Statics;

namespace webapi.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ContratosController : ControllerBase
    {
        private readonly IContratosRepository _contratosRepository;
        public ContratosController(IContratosRepository contratosRepository)
        {
            _contratosRepository = contratosRepository;
        }

        [HttpGet("GetAllContratos")]
        public IActionResult GetAllContratos()
        {
            return Ok(_contratosRepository.GetAll());
        }

        [HttpPost("PostFile")]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            List<Contratos> contratos = FileRead.LerArquivoCsv(filePath);

            await _contratosRepository.SalvarDadosContratos(contratos);
            return Ok(contratos);
        }
    }
}
