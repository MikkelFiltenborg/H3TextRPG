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
        public async Task<ActionResult> GetAllWeapons()
        {
            try
            {
                var weapon = await WeaponRepo.GetAll();

                if (weapon == null)
                    return Problem("Unexpected. Weapon wasn't returned");

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET: api/<WeaponController>
        [HttpGet]
        public List<Weapon> Get()
        {
            return WeaponRepo.GetAll();
        }*/

        // GetById api/<WeaponController>
        public async Task<ActionResult> GetWepaonById(int id)
        {
            try
            {
                var weapon = await WeaponRepo.GetById(id);

                if (GetWepaonById == null)
                    return NotFound();

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET api/<WeaponController>/5
        [HttpGet("{id}")]
        public Weapon Get(int id)
        {
            return WeaponRepo.GetById(id);
        }*/

        // Create api/<WeaponController>
        public async Task<ActionResult> PostWeapon(Weapon wepaon)
        {
            try
            {
                //TODO: cannot await void (weapon).
                var createWepaon = await WeaponRepo.Create(wepaon);

                if (createWepaon == null)
                    return StatusCode(500, "Failed. Weapon wasn't created.");

                return CreatedAtAction("PostWeapon", new {Id = createWepaon.Id}, createWepaon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Weapon {ex.Message}");
            }
        }
        /*
        // POST api/<WeaponController>
        [HttpPost]
        public void Post([FromBody] Weapon weapon)
        {
            WeaponRepo.Create(weapon);
        }*/

        // Update api/<WeaponController>
        public async Task<ActionResult> PutWeapon(Weapon weapon, int id)
        {
            try
            {
                var oldWeapon = await WeaponRepo.GetById(id);

                if (weapon == null)
                    return NotFound();

                oldWeapon.WeaponDamageModifier = weapon.WeaponDamageModifier;
                oldWeapon.SkillRoll = weapon.SkillRoll;
                oldWeapon.Range = weapon.Range;
                oldWeapon.AvailableToHero = weapon.AvailableToHero;
                oldWeapon.StarterWeapon = weapon.StarterWeapon;
                oldWeapon.Value = weapon.Value;
                oldWeapon.Note = weapon.Note;
                oldWeapon.WeaponType = weapon.WeaponType;

                WeaponRepo.Update(oldWeapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(weapon);
        }
        /*
        // PUT api/<WeaponController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Weapon newWeapon)
        {
            var oldWeapon = WeaponRepo.GetById(newWeapon.Id);

            if (newWeapon == oldWeapon) Console.Write("Weapon already exists");

            else
            {
                oldWeapon.WeaponDamageModifier = newWeapon.WeaponDamageModifier;
                oldWeapon.MinimumSkillRoll = newWeapon.MinimumSkillRoll;
                //oldWeapon.Range = newWeapon.Range;
                oldWeapon.AvailableToHero = newWeapon.AvailableToHero;
                oldWeapon.StarterWeapon = newWeapon.StarterWeapon;
                oldWeapon.Value = newWeapon.Value;
                oldWeapon.Note = newWeapon.Note;
                oldWeapon.WeaponType = newWeapon.WeaponType;

                WeaponRepo.Update(oldWeapon);
            }
        }*/

        // Delete api/<WeaponController>
        public async Task<ActionResult> DeleteWeapon(int id)
        {
            try
            {
                var weapon = await WeaponRepo.GetById(id);

                if (GetWepaonById == null)
                    return NotFound();

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // DELETE api/<WeaponController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            WeaponRepo.Delete(id);
        }*/
    }
}
