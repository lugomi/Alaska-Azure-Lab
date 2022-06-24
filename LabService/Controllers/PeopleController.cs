using LabService.Exceptions;
using LabService.Models;
using LabService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LabService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(ILogger<PeopleController> logger, IPeopleRepository peopleRepository)
        {
            _logger = logger;
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPeople()
        {
            _logger.LogTrace("Get All People called");

            return Ok(await _peopleRepository.GetAllPeople());
        }
        
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetPerson(string name)
        {
            _logger.LogTrace("Get person with name '{name}' called", name);

            try
            {
                return Ok(await _peopleRepository.GetPerson(name));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Error);
            }
        }



        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            _logger.LogTrace("Add Person called with data: \n{data}", JsonConvert.SerializeObject(person));

            try
            {
                return Ok(await _peopleRepository.AddPerson(person));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}