using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Data.Repository.Interfaces;
using webapi.Models;
using webapi.Services;
using webapi.Services.Interfaces;

namespace webapi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;

        public AutenticacaoController(IJwtService jwtService,
            IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        [HttpPost("{userName}")]
        public IActionResult AutenticarUsuario(string userName)
        {
            var hasUser = _usuarioRepository.HasUserBuUserName(userName.ToUpper());

            if (!hasUser)
            {
                _usuarioRepository.AddNewUsuario(userName.ToUpper());
            }

            string jwt = _jwtService.CreateJwtToken(_usuarioRepository.GetUsuarioByUserName(userName.ToUpper()));

            return Ok(jwt);
        }
    }
}
