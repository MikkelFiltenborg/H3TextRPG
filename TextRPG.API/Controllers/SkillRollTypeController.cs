using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillRollTypeController : ControllerBase
    {
        IBaseCRUDRepo<SkillRollType> SkillRollTypeRepo { get; set; }
        public SkillRollTypeController(IBaseCRUDRepo<SkillRollType> skillRollTypeRepo)
        {
            SkillRollTypeRepo = skillRollTypeRepo;
        }

        // GetAll api/<SkillRollTypeController>
        [HttpGet]
        public async Task<ActionResult> GetAllSkillRollType()
        {
            try
            {
                var skillRollType = await SkillRollTypeRepo.GetAll();

                if (skillRollType == null)
                    return Problem("Unexpected. SkillRollType wasn't found.");

                return Ok(skillRollType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GetById api/<SkillRollTypeController>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSkillRollTypeById(int id)
        {
            try
            {
                var skillRollType = await SkillRollTypeRepo.GetById(id);

                if (GetSkillRollTypeById == null)
                    return NotFound();

                return Ok(skillRollType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create api/<SkillRollTypeController>
        [HttpPost]
        public async Task<ActionResult> PostWeaponType(SkillRollType SkillRollType)
        {
            try
            {
                var createSkillRollType = await SkillRollTypeRepo.Create(SkillRollType);

                if (createSkillRollType == null)
                    return StatusCode(500, "Failed. SkillRollType wasn't created.");

                return CreatedAtAction("PostWeapon", new { id = createSkillRollType.Id }, createSkillRollType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while trying to create the SkillRollType. {ex.Message}");
            }
        }

        // Update api/<SkillRollTypeController>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutWeaponType(SkillRollType skillRollType, int id)
        {
            try
            {
                if (skillRollType == null)
                    return NotFound();

                await SkillRollTypeRepo.Update(skillRollType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(skillRollType);
        }

        // Delete api/<SkillRollTypeController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkillRollType(int id)
        {
            try
            {
                await SkillRollTypeRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
