using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        IBaseCRUDRepo<Weapon> WeaponRepo { get; set; }
        public WeaponController(IBaseCRUDRepo<Weapon> weaponRepo)
        {
            WeaponRepo = weaponRepo;
        }

        // GetAll api/<WeaponController>
        [HttpGet]
        public async Task<ActionResult> GetAllWeapon()
        {
            try
            {
                var weapon = await WeaponRepo.GetAll();

                if (weapon == null)
                    return Problem("Unexpected. Weapon wasn't found.");

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<WeaponController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWeaponById(int id)
        {
            try
            {
                var weapon = await WeaponRepo.GetById(id);

                if (GetWeaponById == null)
                    return NotFound();

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<WeaponController>
        [HttpPost]
        public async Task<ActionResult> PostWeapon(Weapon wepaon)
        {
            try
            {
                var createWepaon = await WeaponRepo.Create(wepaon);

                if (createWepaon == null)
                    return StatusCode(500, "Failed. Weapon wasn't created.");

                return CreatedAtAction("PostWeapon", new { id = createWepaon.Id }, createWepaon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Weapon. {ex.Message}");
            }
        }

        // Update api/<WeaponController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutWeapon(Weapon weapon, int id)
        {
            try
            {
                if (weapon == null)
                    return NotFound();

                await WeaponRepo.Update(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(weapon);
        }

        // Delete api/<WeaponController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeapon(int id)
        {
            try
            {
                await WeaponRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
