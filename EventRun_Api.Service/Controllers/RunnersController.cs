using Azure;
using EventRun_Api.Core;
using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
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

        public RunnersController(IConfiguration configuration)
        {
            _runnerCore = new(configuration);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string? documentNumber = null, string? documentType = null)
        {
            try
            {
                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = (documentNumber != null && documentType != null) ?
                            _runnerCore.GetRunner(documentNumber ?? "", documentType ?? "") :
                            _runnerCore.GetRunners()
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
                    Message = "Continúa diligenciada forma de pago y Comprobante de Pago.",
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
    }
}
