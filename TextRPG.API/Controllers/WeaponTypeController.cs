using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponTypeController : ControllerBase
    {
        IBaseCRUDRepo<WeaponType> WeaponTypeRepo { get; set; }
        public WeaponTypeController(IBaseCRUDRepo<WeaponType> weaponTypeRepo)
        {
            WeaponTypeRepo = weaponTypeRepo;
        }

        // GetAll api/<WeaponTypeController>
        [HttpGet]
        public async Task<ActionResult> GetAllWeaponType()
        {
            try
            {
                var weaponType = await WeaponTypeRepo.GetAll();

                if (weaponType == null)
                    return Problem("Unexpected. WeaponType wasn't found.");

                return Ok(weaponType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<WeaponTypeController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWeaponTypeById(int id)
        {
            try
            {
                var weaponType = await WeaponTypeRepo.GetById(id);

                if (GetWeaponTypeById == null)
                    return NotFound();

                return Ok(weaponType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<WeaponTypeController>
        [HttpPost]
        public async Task<ActionResult> PostWeaponType(WeaponType weaponType)
        {
            try
            {
                var createWepaonType = await WeaponTypeRepo.Create(weaponType);

                if (createWepaonType == null)
                    return StatusCode(500, "Failed. WeaponType wasn't created.");

                return CreatedAtAction("PostWeapon", new { id = createWepaonType.Id }, createWepaonType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the WeaponType. {ex.Message}");
            }
        }

        // Update api/<WeaponTypeController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutWeaponType(WeaponType weaponType, int id)
        {
            try
            {
                if (weaponType == null)
                    return NotFound();

                await WeaponTypeRepo.Update(weaponType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(weaponType);
        }

        // Delete api/<WeaponTypeController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeaponType(int id)
        {
            try
            {
                await WeaponTypeRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
