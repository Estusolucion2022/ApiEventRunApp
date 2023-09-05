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
        private readonly Email _email;
        private IConfiguration _configuration { get; }

        public UsersController(IConfiguration configuration) 
        {
            _configuration = configuration;
            _userCore = new(configuration); 
            _email = new Email(configuration); 
        }

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

        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(string user)
        {
            Response response = new();
            int accessCode = 0;
            try
            {
                string email = _userCore.GetEmailByUser(user)!;
                if (email != null) 
                {
                accessCode = new Random().Next(10000, 99999);
                _email.SendEmail(
                    _email.GetBodyEmailAccess(user, accessCode.ToString()),
                    email,
                    _configuration["AppSettings:EmailSubjectAccessCode"]!
                    );
                }
                response.Code = (int)EnumCodeResponse.CodeResponse.SinErrores;
                response.Data = accessCode;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al mandar correo",
                    Error = ex.Message
                });
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public IActionResult RecoverPassword(RecoverPassword model)
        {
            Response response = new();
            try
            {
                model.Password = Security.GetSHA256(model.Password);
                bool res = _userCore.UpdatePasswordUser(model);
                if (res) response.Code = (int)EnumCodeResponse.CodeResponse.SinErrores;
                else response.Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al mandar correo",
                    Error = ex.Message
                });
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
