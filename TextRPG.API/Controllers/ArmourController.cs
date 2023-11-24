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
                    return NotFound();

                return Ok(armour);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET: api/<ArmourController>
        [HttpGet]
        public List<Armour> Get()
        {
            return ArmourRepo.GetAll();
        }*/

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
        /*
        // GET api/<ArmourController>/5
        [HttpGet("{id}")]
        public Armour Get(int id)
        {
            return ArmourRepo.GetById(id);
        }*/

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
                //return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Weapon {ex.Message}");
            }
        }
        /*
        // POST api/<ArmourController>
        [HttpPost]
        public void Post([FromBody] Armour armour)
        {
            ArmourRepo.Create(armour);
        }*/

        // Update api/<ArmourController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutArmour(Armour armour, int id)
        {
            try
            {
                var oldArmour = await ArmourRepo.GetById(id);

                if (armour == null)
                    return NotFound();

                oldArmour.ArmourTypeName = armour.ArmourTypeName;
                oldArmour.ArmourModifier = armour.ArmourModifier;
                oldArmour.AvailableToHero = armour.AvailableToHero;
                oldArmour.Value = armour.Value;
                oldArmour.Note = armour.Note;
                ArmourRepo.Update(oldArmour);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(armour);
        }
        /*
        // PUT api/<ArmourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Armour newArmour)
        {
            //TODO: Needs to Check if Data is Okay to Enter database
            var oldArmour = ArmourRepo.GetById(newArmour.Id);

            if (!string.IsNullOrWhiteSpace(newArmour.ArmourTypeName)) oldArmour.ArmourTypeName = newArmour.ArmourTypeName;
            oldArmour.ArmourModifier = newArmour.ArmourModifier;
            oldArmour.AvailableToHero = newArmour.AvailableToHero;
            oldArmour.Value = newArmour.Value;

            if (!string.IsNullOrWhiteSpace(newArmour.Note)) oldArmour.Note = newArmour.Note;
            ArmourRepo.Update(oldArmour);
        }*/

        // Delete api/<ArmourController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArmour(int id)
        {
            try
            {
                //var armour = await ArmourRepo.GetById(id);

                //if (armour == null)
                //    return NotFound();

                await ArmourRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // DELETE api/<ArmourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ArmourRepo.Delete(id);
        }*/
    }
}
