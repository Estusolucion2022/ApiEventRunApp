using Azure;
using EventRun_Api.Core;
using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using EventRun_Api.Models.Models;
using EventRun_Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using Response = EventRun_Api.Models.Response;

namespace EventRun_Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunnersController : ControllerBase
    {
        private readonly RunnerCore _runnerCore;
        private readonly LogUpdateRunnerCore _logUpdateRunnerCore;
        private readonly Email _email;
        private IConfiguration _configuration { get; }

        public RunnersController(IConfiguration configuration)
        {
            _runnerCore = new(configuration);
            _logUpdateRunnerCore = new(configuration);
            _email = new Email(configuration);
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string documentNumber, string documentType)
        {
            try
            {
                RunnerResponse? runnerResponse = _runnerCore.GetRunner(documentNumber, documentType);
                int accessCode = 0;

                if (runnerResponse != null)
                {
                    accessCode = new Random().Next(10000, 99999);
                    _email.SendEmail(
                        _email.GetBodyEmailAccess(runnerResponse.FirstName, accessCode.ToString()),
                        runnerResponse.Email,
                        _configuration["AppSettings:EmailSubjectAccessCode"]!
                        );
                }

                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = runnerResponse,
                    Message = accessCode == 0 ? "" : accessCode.ToString()
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al consultar competidor",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("SearchAll")]
        public IActionResult SearchAll()
        {
            try
            {
                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = _runnerCore.GetRunners()
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al consultar competidor",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] Runner runner)
        {
            try
            {
                runner.CreationDate = DateTime.Now;
                int idRunner = _runnerCore.SaveRunner(runner);

                RunnerResponse runnerResponse = _runnerCore.GetRunnerById(idRunner); 

                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Message = "Continúa diligenciando los datos con el Comprobante de Pago.",
                    Data = runnerResponse
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al guardar competidor",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] RunnerUpdate updateRunner)
        {
            try
            {
                int idRunner = _runnerCore.UpdateRunner(updateRunner.Runner);
                RunnerResponse runnerResponse = _runnerCore.GetRunnerById(idRunner);

                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Message = "Competidor actualizado con exito",
                    Data = runnerResponse
                };

                LogUpdateRunner logUpdateRunner = new LogUpdateRunner
                {
                    Iduser = updateRunner.IdUser,
                    IdRunner = updateRunner.Runner.Id,
                    Description = updateRunner.Description,
                    CreationDate = DateTime.Now
                };
                _logUpdateRunnerCore.CreateLog(logUpdateRunner);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al actualizar competidor",
                    Error = ex.Message
                });
            }
        }
    }
}
