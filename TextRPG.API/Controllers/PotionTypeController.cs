using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotionTypeController : ControllerBase
    {
        IBaseCRUDRepo<PotionType> PotionTypeRepo { get; set; }
        public PotionTypeController(IBaseCRUDRepo<PotionType> potionTypeRepo)
        {
            PotionTypeRepo = potionTypeRepo;
        }

        // GetAll api/<PotionTypeController>
        [HttpGet]
        public async Task<ActionResult> GetAllPotionType()
        {
            try
            {
                var potionType = await PotionTypeRepo.GetAll();

                if (potionType == null)
                    return Problem("Unexpected. PotionType wasn't found.");

                return Ok(potionType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        //// GET: api/<PotionTypeController>
        //[HttpGet]
        //public List<PotionType> Get()
        //{S
        //    return PotionTypeRepo.GetAll();
        //}*/

        // GetById: api/<PotionTypeController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPotionTypeById(int id)
        {
            try
            {
                var potionType = await PotionTypeRepo.GetById(id);

                if(potionType == null)
                    return NotFound();

                return Ok(potionType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET api/<PotionTypeController>/5
        //[HttpGet("{id}")]
        //public PotionType Get(int id)
        //{
        //    return PotionTypeRepo.GetById(id);
        //}*/

        // Create: api/<PotionTypeController>
        [HttpPost]
        public async Task<ActionResult<PotionType>> PostPotionType(PotionType potionType)
        {
            try
            {
                var createPotionType = await PotionTypeRepo.Create(potionType);

                if (createPotionType == null)
                    return StatusCode(500, "Failed. PotionType wasn't created.");

                return CreatedAtAction("PostPotion", new { id = createPotionType.Id }, createPotionType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the PotionType. {ex.Message}");
            }
        }
        /*
        // POST api/<PotionTypeController>
        [HttpPost]
        public void Post([FromBody] PotionType potionType)
        {
            PotionTypeRepo.Create(potionType);
        }*/

        // Update: api/<PotionTypeController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPotionType(PotionType potionType, int id)
        {
            try
            {
                var oldPotionType = await PotionTypeRepo.GetById(id);

                if (potionType == null)
                    return NotFound();

                await PotionTypeRepo.Update(oldPotionType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(potionType);
        }
        /*
        //// PUT api/<PotionTypeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] PotionType newPotionType)
        //{
        //    var oldPotionType = PotionTypeRepo.GetById(newPotionType.Id);

        //    if (newPotionType == oldPotionType) Console.Write("PotionType already exists");

        //    else
        //    {
        //        //oldPotion.Amount = newPotion.Amount;
        //        //oldPotion.PotionType = newPotion.PotionType;

        //        //PotionRepo.Update(oldPotion);
        //    }
        //}*/

        // Delete api/<PotionTypeController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePotionType(int id)
        {
            try
            {
                //var potionType = await PotionTypeRepo.GetById(id);

                //if (potionType == null)
                //    return NotFound();

                await PotionTypeRepo.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
                return Ok(potionType);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // DELETE api/<PotionTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PotionTypeRepo.Delete(id);
        }*/
    }
}
