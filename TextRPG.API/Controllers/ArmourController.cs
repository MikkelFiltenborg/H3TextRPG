using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmourController : ControllerBase
    {
        IBaseCRUDRepo<Armour> ArmourRepo { get; set; }
        public ArmourController(IBaseCRUDRepo<Armour> armourRepo)
        {
            ArmourRepo = armourRepo;
        }

        // GetAll api/<ArmourController>
        [HttpGet]
        public async Task<ActionResult> GetAllArmour()
        {
            try
            {
                var armour = await ArmourRepo.GetAll();

                if (armour == null)
                    return Problem("Unexpected. Armour wasn't found.");

                return Ok(armour);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<ArmourController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetArmourById(int id)
        {
            try
            {
                var armour = await ArmourRepo.GetById(id);

                if (armour == null)
                    return NotFound();

                return Ok(armour);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<ArmourController>
        [HttpPost]
        public async Task<ActionResult> PostArmour(Armour armour)
        {
            try
            {
                var createArmour = await ArmourRepo.Create(armour);

                if (createArmour == null)
                    return StatusCode(500, "Failed. Weapon wasn't created.");

                return CreatedAtAction("PostArmour", new { Id = createArmour.Id }, createArmour);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Weapon {ex.Message}");
            }
        }

        // Update api/<ArmourController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutArmour(Armour armour, int id)
        {
            try
            {
                if (armour == null)
                    return NotFound();

                await ArmourRepo.Update(armour);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(armour);
        }

        // Delete api/<ArmourController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArmour(int id)
        {
            try
            {
                await ArmourRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
