using EventRun_Api.Core;
using EventRun_Api.Models.Enums;
using EventRun_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventRun_Api.Models.Models;
using System.Security.Permissions;
using EventRun_Api.Utils;

namespace EventRun_Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserCore _userCore;

        public UsersController(IConfiguration configuration) { _userCore = new(configuration); }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search([FromBody] Credentials credentials)
        {
            try
            {
                credentials.Password = Security.GetSHA256(credentials.Password ?? string.Empty);
                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = _userCore.GetUser(credentials.UserPlataform ?? string.Empty, credentials.Password)
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al consultar usuario",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                user.CreationDate = DateTime.Now;
                user.Password = Security.GetSHA256(user.Password);
                int idUser = _userCore.SaveUser(user);

                user = _userCore.GetUserById(idUser);

                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Message = "Usuario guardado con exito",
                    Data = user
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al guardar usuario",
                    Error = ex.Message
                });
            }
        }
    }
}
