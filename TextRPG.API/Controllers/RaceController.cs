using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        IBaseCRUDRepo<Race> RaceRepo { get; set; }
        public RaceController(IBaseCRUDRepo<Race> raceRepo)
        {
            RaceRepo = raceRepo;
        }

        // GetAll: api/<RaceController>
        [HttpGet]
        public async Task<ActionResult> GetAllRace()
        {
            try
            {
                var race = await RaceRepo.GetAll();

                if (race == null)
                    return Problem("Unexpected. Race wasn't found.");

                return Ok(race);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<RaceController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRaceById(int id)
        {
            try
            {
                var race = await RaceRepo.GetById(id);

                if (race == null)
                    return NotFound();

                return Ok(race);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<RaceController>
        [HttpPost]
        public async Task<ActionResult> PostRace(Race race)
        {
            try
            {
                var createRace = await RaceRepo.Create(race);

                if (createRace == null)
                    return StatusCode(500, "Failed. Race wasn't created.");

                return CreatedAtAction("PostRace", new { Id = createRace.Id }, createRace);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create Race {ex.Message}");
            }
        }

        // Update api/<RaceController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutRace(Race race, int id)
        {
            try
            {
                if (race == null)
                    return NotFound();

                await RaceRepo.Update(race);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(race);
        }

        // Delete api/<RaceController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRace(int id)
        {
            try
            {
                await RaceRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
