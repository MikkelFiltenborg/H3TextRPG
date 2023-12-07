using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        IBaseCRUDRepo<Monster> MonsterRepo { get; set; }
        public MonsterController(IBaseCRUDRepo<Monster> monsterRepo)
        {
            MonsterRepo = monsterRepo;
        }

        // GetAll api/<MonsterController>
        [HttpGet]
        public async Task<ActionResult> GetAllMonster()
        {
            try
            {
                var monster = await MonsterRepo.GetAll();

                if (monster == null)
                    return Problem("Unexpected. Monster wasn't found.");

                return Ok(monster);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<MonsterController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMonsterById(int id)
        {
            try
            {
                var monster = await MonsterRepo.GetById(id);

                if (monster == null)
                    return NotFound();

                return Ok(monster);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<MonsterController>
        [HttpPost]
        public async Task<ActionResult<Monster>> PostMonster(Monster monster)
        {
            try
            {
                var createMonster = await MonsterRepo.Create(monster);

                if (createMonster == null)
                    return StatusCode(500, "Failed. Monster wasn't created");

                return CreatedAtAction("PostMonster", new { id = createMonster.Id }, createMonster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the Monster. {ex.Message}");
            }
        }

        // Update api/<MonsterController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMonster(Monster monster, int id)
        {
            try
            {
                if (monster == null)
                    return NotFound();

                await MonsterRepo.Update(monster);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(monster);
        }

        // Delete api/<MonsterController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMonster(int id)
        {
            try
            {
                await MonsterRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
