using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        IBaseCRUDRepo<Hero> HeroRepo { get; set; }
        public HeroController(IBaseCRUDRepo<Hero> heroRepo)
        {
            HeroRepo = heroRepo;
        }

        // GetAll api/<HeroController>
        [HttpGet]
        public async Task<ActionResult> GetAllHero()
        {
            try
            {
                var hero = await HeroRepo.GetAll();

                if(hero == null)
                    return Problem("Unexpected. Hero wasn't found");

                return Ok(hero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET: api/<HeroController>
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            //HeroRepo.GetAll();
            return HeroRepo.GetAll();
        }*/

        // GetById api/<HeroController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetHeroById(int id)
        {
            try
            {
                var hero = await HeroRepo.GetById(id);

                if (hero == null)
                    return NotFound();

                return Ok(hero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // GET api/<HeroController>/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            Hero hero = HeroRepo.GetById(id);
            return hero;
        }*/

        // Create api/<HeroController>
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            try
            {
                var createHero = await HeroRepo.Create(hero);

                if (createHero == null)
                    return StatusCode(500, "Failed. Hero wasn't created");

                return CreatedAtAction("PostHero", new { id = createHero.Id }, createHero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Hero. {ex.Message}");
            }
        }
        /*
        // POST api/<HeroController>
        [HttpPost]
        public void Post([FromBody] Hero hero)
        {
            HeroRepo.Create(hero);
        }*/

        // Update api/<HeroController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutHero(Hero hero, int id)
        {
            try
            {
                //Not in use \/
                //var oldHero = await HeroRepo.GetById(id);

                if (hero == null)
                    return NotFound();

                await HeroRepo.Update(hero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(hero);
        }
        /*
        // PUT api/<HeroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hero hero)
        {
            HeroRepo.Update(hero);
        }*/

        // Delete api/<HeroController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(int id)
        {
            try
            {
                //var hero = await HeroRepo.GetById(id);

                //if (hero == null)
                //    return NotFound();

                await HeroRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*
        // DELETE api/<HeroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            HeroRepo.Delete(id);
        }*/
    }
}
