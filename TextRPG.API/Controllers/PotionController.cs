using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotionController : ControllerBase
    {
        IBaseCRUDRepo<Potion> PotionRepo { get; set; }
        public PotionController(IBaseCRUDRepo<Potion> potionRepo)
        {
            PotionRepo = potionRepo;
        }

        // GetAll api/<PotionController>
        [HttpGet]
        public async Task<ActionResult> GetAllPotion()
        {
            try
            {
                var potion = await PotionRepo.GetAll();

                if (potion == null)
                    return Problem("Unexpected. Potion wasn't returned");

                return Ok(potion);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        //// GET: api/<PotionController>
        //[HttpGet]
        //public List<Potion> Get()
        //{S
        //    return PotionRepo.GetAll();
        //}*/

        // GetById: api/<PotionController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPotionById(int id)
        {
            try
            {
                var potion = await PotionRepo.GetById(id);

                if(potion == null)
                    return NotFound();

                return Ok(potion);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET api/<PotionController>/5
        //[HttpGet("{id}")]
        //public Potion Get(int id)
        //{
        //    return PotionRepo.GetById(id);
        //}*/

        // Create: api/<PotionController>
        [HttpPost]
        public async Task<ActionResult<Potion>> PostPotion(Potion potion)
        {
            try
            {
                var createPotion = await PotionRepo.Create(potion);

                if (createPotion == null)
                    return StatusCode(500, "Failed. Potion wasn't created.");

                //TODO: Id problem (potion).
                return CreatedAtAction("PostPotion", new { id = createPotion.Id }, createPotion);
                //return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Potion {ex.Message}");
            }
        }
        /*
        // POST api/<PotionController>
        [HttpPost]
        public void Post([FromBody] Potion potion)
        {
            PotionRepo.Create(potion);
        }*/

        // Update: api/<PotionController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPotion(Potion potion, int id)
        {
            try
            {
                var oldPotion = await PotionRepo.GetById(id);

                if (potion == null)
                    return NotFound();

                oldPotion.Amount = potion.Amount;
                oldPotion.PotionType = potion.PotionType;

                PotionRepo.Update(oldPotion);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(potion);
        }
        /*
        //// PUT api/<PotionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Potion newPotion)
        //{
        //    var oldPotion = PotionRepo.GetById(newPotion.Id);

        //    if (newPotion == oldPotion) Console.Write("Potion already exists");

        //    else
        //    {
        //        oldPotion.Amount = newPotion.Amount;
        //        oldPotion.PotionType = newPotion.PotionType;

        //        PotionRepo.Update(oldPotion);
        //    }
        //}*/

        // Delete api/<PotionController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePotion(int id)
        {
            try
            {
                var potion = await PotionRepo.GetById(id);

                if (potion == null)
                    return NotFound();

                PotionRepo.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // DELETE api/<PotionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PotionRepo.Delete(id);
        }*/
    }
}
