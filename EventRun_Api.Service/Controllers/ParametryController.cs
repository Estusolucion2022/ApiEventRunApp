using EventRun_Api.Core;
using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using EventRun_Api.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventRun_Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametryController : ControllerBase
    {

        private readonly DocumentTypeCore _documentTypeCore;
        private readonly CitiesCore _citiesCore;
        private readonly CountriesCore _countriesCore;
        private readonly GendersCore _gendersCore;
        private readonly PaymentMethodsCore _paymentMethodsCore;
        private readonly RacesCore _racesCore;
        private readonly CategoriesCore _categoriesCore;

        public ParametryController(IConfiguration configuration)
        {
            _documentTypeCore = new(configuration);
            _citiesCore = new(configuration);
            _countriesCore = new(configuration);
            _gendersCore = new(configuration);
            _paymentMethodsCore = new(configuration);
            _racesCore = new(configuration);
            _categoriesCore = new(configuration);
        }

        [HttpGet]
        [Route("GetParametry")]
        public IActionResult GetParametry(string tipoParametria)
        {
            try
            {
                List<SelectOption> lstOpciones = new();

                switch (tipoParametria)
                {
                    case "DocumentTypes":
                        if (_documentTypeCore.GetDocumentsType() is List<DocumentType> lstDocumentTpes)
                        {
                            foreach (DocumentType item in lstDocumentTpes)
                            {
                                lstOpciones.Add(new SelectOption { Text = item.Description, Value = item.Code });
                            }
                        }
                        break;
                    case "Cities":
                        if (_citiesCore.GetCities() is List<City> lstCities)
                        {
                            foreach (City item in lstCities)
                            {
                                lstOpciones.Add(new SelectOption { Text = item.Name, Value = item.Code });
                            }
                        }
                        break;
                    case "Countries":
                        if (_countriesCore.GetCountries() is List<Country> lstCountries)
                        {
                            foreach (Country item in lstCountries)
                            {
                                lstOpciones.Add(new SelectOption { Text = item.Name, Value = item.Code });
                            }
                        }
                        break;
                    case "Genders":
                        if (_gendersCore.GetGenders() is List<Gender> lstGenders)
                        {
                            foreach (Gender item in lstGenders)
                            {
                                lstOpciones.Add(new SelectOption { Text = item.Description, Value = item.Id.ToString() });
                            }
                        }
                        break;
                    case "PaymentMethods":
                        if (_paymentMethodsCore.GetPaymentMethods() is List<PaymentMethod> lstPaymentMethods)
                        {
                            foreach (PaymentMethod item in lstPaymentMethods)
                            {
                                lstOpciones.Add(new SelectOption { Text = item.Description, Value = item.Id.ToString() });
                            }
                        }
                        break;
                }

                return StatusCode(StatusCodes.Status200OK, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = lstOpciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Fallo al consultar parametrias",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetRaces")]
        public IActionResult GetRaces()
        {
            try
            {
                List<SelectOption> lstOpciones = new();

                if (_racesCore.GetRaces(true) is List<Race> lstRaces)
                {
                    foreach (Race item in lstRaces)
                    {
                        lstOpciones.Add(new SelectOption { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = lstOpciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Fallo al consultar parametrias",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                List<SelectOption> lstOpciones = new();

                if (_categoriesCore.GetCategories(true) is List<Category> lstCategories)
                {
                    foreach (Category item in lstCategories)
                    {
                        lstOpciones.Add(new SelectOption { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.SinErrores,
                    Data = lstOpciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Code = (int)EnumCodeResponse.CodeResponse.ErrorGeneral,
                    Message = "Fallo al consultar parametrias",
                    Error = ex.Message
                });
            }
        }
    }
}
