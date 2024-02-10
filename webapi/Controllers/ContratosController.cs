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
        private readonly IUsuarioRepository _usuarioRepository;
        private IWebHostEnvironment _hostEnvironment;
        public ContratosController(IContratosRepository contratosRepository, IWebHostEnvironment hostEnvironment, IUsuarioRepository usuarioRepository)
        {
            _contratosRepository = contratosRepository;
            _hostEnvironment = hostEnvironment;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("GetAllContratos")]
        public IActionResult GetAllContratos()
        {
            return Ok(_contratosRepository.GetAll());
        }

        [HttpGet("GetFileContratos")]
        public IActionResult GetFileContratos()
        {
            return Ok(_contratosRepository.GetFileContratos());
        }
        
        [HttpPost("GetFileContratos")]
        public IActionResult GetFileContratos([FromForm] string path)
        {
            return Ok(_contratosRepository.GetFileContratosByPath(path));
        }

        [HttpPost("PostFile/{userName}")]
        public async Task<IActionResult> PostFile(IFormFile file, string userName)
        {
            userName = userName.ToUpper();
            string path = Path.Combine(_hostEnvironment.WebRootPath, file.FileName);
            
            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            }
            if (!_contratosRepository.HasFileContratoInPath(path))
            {
                List<Contratos> contratos = FileRead.LerArquivoCsv(path, _usuarioRepository.GetUsuarioIdByUserName(userName));

                await _contratosRepository.SalvarDadosContratos(contratos, _usuarioRepository.GetUsuarioIdByUserName(userName), path);
                return Ok(contratos);
            }

            return Ok();
        }
    }
}
