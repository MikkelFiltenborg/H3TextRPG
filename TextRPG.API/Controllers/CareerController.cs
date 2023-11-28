using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        IBaseCRUDRepo<Career> CareerRepo { get; set; }
        public CareerController(IBaseCRUDRepo<Career> careerRepo)
        {
            CareerRepo = careerRepo;
        }

        // GetAll api/<CareerController>
        [HttpGet]
        public async Task<ActionResult> GetAllCareer()
        {
            try
            {
                var career = await CareerRepo.GetAll();

                if (career == null)
                    return Problem("Unexpected. Career wasn't found.");

                return Ok(career);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<CareerController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCareerById(int id)
        {
            try
            {
                var career = await CareerRepo.GetById(id);

                if (career == null)
                    return NotFound();

                return Ok(career);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<CareerController>
        [HttpPost]
        public async Task<ActionResult> PostCareer(Career career)
        {
            try
            {
                var createCareer = await CareerRepo.Create(career);

                if (createCareer == null)
                    return StatusCode(500, "Failed. Career wasn't created.");

                return CreatedAtAction("PostCareer", new { id = createCareer.Id }, createCareer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create Career {ex.Message}");
            }
        }

        // Update api/<CareerController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCareer(Career career, int id)
        {
            try
            {
                if (career == null)
                    return NotFound();
                await CareerRepo.Update(career);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(career);
        }

        // Delete api/<CareerController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCareer(int id)
        {
            try
            {
                await CareerRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
