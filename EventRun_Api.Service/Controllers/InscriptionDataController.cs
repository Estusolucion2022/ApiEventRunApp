using EventRun_Api.Core;
using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using EventRun_Api.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EventRun_Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionDataController : ControllerBase
    {
        private readonly InscriptionDataCore _inscriptionDataCore;
        private readonly RunnerCore _runnerCore;
        private readonly Email _email;
        private IConfiguration _configuration { get; }

        public InscriptionDataController(IConfiguration config) { 
            _email = new Email(config);
            _inscriptionDataCore = new(config);
            _runnerCore = new(config);
            _configuration = config;
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] InscriptionData inscriptionData)
        {
            Response response = new();
            if (_inscriptionDataCore.VerifyPaymentCode(inscriptionData.DetailsPayment))
            {
                try
                {
                    if (_inscriptionDataCore.GetInscriptionDataSpecific(inscriptionData.IdRunner,
                            inscriptionData.IdRace) == null
                        )
                    {
                        inscriptionData.RegistrationDate = DateTime.Now;
                        _inscriptionDataCore.SaveInscriptionData(inscriptionData);
                        response.Code = (int)EnumCodeResponse.CodeResponse.SinErrores;
                        response.Message = "¡Bienvenido!\nA su correo registrado le llegara la confirmación de su inscripción\r\n";
                    }
                    else
                    {
                        response.Code = (int)EnumCodeResponse.CodeResponse.YaInscrito;
                        response.Message = "Usuario ya inscrito, por favor validar";
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                    {
                        Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                        Message = "Error al guardar inscripcion",
                        Error = ex.Message
                    });
                }

                if (response.Code == (int)EnumCodeResponse.CodeResponse.SinErrores)
                {
                    try
                    {
                        InscriptionDataResponse? inscriptionDataResponse = _inscriptionDataCore.GetInscriptionDataSpecific
                            (inscriptionData.IdRunner, inscriptionData.IdRace);
                        RunnerResponse runner = _runnerCore.GetRunnerById(inscriptionData.IdRunner);
                        if (inscriptionDataResponse != null && runner != null)
                        {
                            _email.SendEmail(
                                _email.GetBodyEmailCreate(inscriptionDataResponse, runner),
                                runner.Email,
                                _configuration["AppSettings:EmailSubjectCreation"]!
                                );
                        }
                        else
                        {
                            response.Code = (int)EnumCodeResponse.CodeResponse.ErrorEnviarCorreo;
                            response.Message = "Error al enviar correo";
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                        {
                            Code = (int)EnumCodeResponse.CodeResponse.ErrorEnviarCorreo,
                            Message = "Error al enviar correo",
                            Error = ex.Message
                        });
                    }
                }
            }
            else {
                response.Code = (int)EnumCodeResponse.CodeResponse.CodigoYaRegistrado;
                response.Message = "Codigo de pago ya registrado, por favor valide con el administrador";
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
        [Route("GetInsciptionData")]
        public IActionResult GetInsciptionData(int idRunner)
        {
            try
            {
                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = _inscriptionDataCore.GetInscriptionData(idRunner)
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al consultar inscriptiones",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetReportInsciptionData")]
        public IActionResult GetReportInsciptionData()
        {
            try
            {
                Response response = new()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = _inscriptionDataCore.GetReportInscriptionsData()
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Error al consultar inscriptiones",
                    Error = ex.Message
                });
            }
        }
    }
}
