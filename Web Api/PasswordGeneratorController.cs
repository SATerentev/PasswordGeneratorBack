using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PassGeneratorService.Application.DTO;
using PassGeneratorService.Application.Interfaces;
using PassGeneratorService.Application.Settings;

namespace PassGeneratorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordGeneratorController : ControllerBase
    {
        private readonly PasswordSettings _settings;
        private readonly IGeneratorService _generatorService;
        private readonly IPasswordRepository _repository;

        public PasswordGeneratorController(IOptions<PasswordSettings> options, IGeneratorService generatorService, IPasswordRepository repository)
        {
            _settings = options.Value;
            _generatorService = generatorService;
            _repository = repository;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GeneratePassword([FromQuery] GenerationRequest requestData)
        {
            if (requestData == null)
            {
                return BadRequest(new ErrorResponse { Type = "Error", Title = "Null", Detail = "Empty Request" });
            }

            if (requestData.PassLength < _settings.MinLength || requestData.PassLength > _settings.MaxLength)
            {
                return BadRequest(new ErrorResponse
                {
                    Type = "Validation",
                    Title = "Password Length",
                    Detail = $"Length must be between {_settings.MinLength} and {_settings.MaxLength}"
                });
            }

            var password = _generatorService.GeneratePass
                (
                requestData.PassLength, 
                requestData.IncludeUppercase, 
                requestData.IncludeNumbers, 
                requestData.IncludeSymbols
                );

            await _repository.SavePassword(password);

            return Ok(new GenerationResponse { Password = password.Value });
        }
    }
}
