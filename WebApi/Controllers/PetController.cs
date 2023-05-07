using Azure.Core;
using Microsoft.AspNetCore.Mvc; 

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pet>>> GetPets()
        {
            var response = await _petService.GetPets();
            if (!response.Success)
                return Problem(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var response = await _petService.GetPet(id);
            if (!response.Success)
                return Problem(response.Message);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddPet([FromBody] AddPetDto request)
        {
            var response = await _petService.AddPet(request);
            if (!response.Success)
                return Problem(response.Message);
            return Ok(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdatePet([FromBody] UpdatePetDto request)
        {
            var response = await _petService.UpdatePet(request);
            if (!response.Success)
                return Problem(response.Message);
            return Ok(response.Message);
        }   

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePet(int id)
        {
            var response = await _petService.DeletePet(id);
            if (!response.Success)
                return Problem(response.Message);
            return Ok(response.Message);
        }
    }
}
