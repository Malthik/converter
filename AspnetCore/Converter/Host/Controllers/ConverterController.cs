
using Converter.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        private readonly IConverterService _converterService;

        public ConverterController
        (
            IConverterService converterService
        )
        {
            _converterService = converterService;
        }

        [HttpGet("json-to-csv")]
        public IActionResult ConverterJsonToCsv([FromQuery] string jsonString)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(jsonString))
                    throw new Exception("invalid-json");

                var csv = _converterService.ConverterJsonToCsv(jsonString);

                return Ok(new { csv });
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("invalid-json"))
                    return BadRequest("O json informado não é valido. Por favor, valide as informações inseridas e tente novamente.");

                return BadRequest("Ocorreu um erro ao converter o Json em CSV. Por favor, valide as informações inseridas e tente novamente.");
            }
        }
    }
}