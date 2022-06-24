using Microsoft.AspNetCore.Mvc;

namespace LabService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IConfiguration _configuration;

        public ConfigController(ILogger<PeopleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetConfig()
        {
            _logger.LogTrace("Get config called");

            return Ok(_configuration.AsEnumerable()
                .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }
    }
}