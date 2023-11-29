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

                if (hero == null)
                    return Problem("Unexpected. Hero wasn't found.");

                return Ok(hero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

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

        // Update api/<HeroController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutHero(Hero hero, int id)
        {
            try
            {
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

        // Delete api/<HeroController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(int id)
        {
            try
            {
                await HeroRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
