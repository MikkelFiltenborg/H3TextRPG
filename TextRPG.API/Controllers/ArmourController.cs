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
        // GET: api/<ArmourController>
        [HttpGet]
        public List<Armour> Get()
        {
            return ArmourRepo.GetAll();
        }

        // GET api/<ArmourController>/5
        [HttpGet("{id}")]
        public Armour Get(int id)
        {
            return ArmourRepo.GetById(id);
        }

        // POST api/<ArmourController>
        [HttpPost]
        public void Post([FromBody] Armour armour)
        {
            ArmourRepo.Create(armour);
        }

        // PUT api/<ArmourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Armour newArmour)
        {
            //TODO: Needs to Check if Data is Okay to Enter database
            var oldArmour = ArmourRepo.GetById(armour.Id);

            if (!string.IsNullOrWhiteSpace(armour.ArmourType)) oldArmour.ArmourType = armour.ArmourType;
            oldArmour.ArmourModifier = armour.ArmourModifier;
            oldArmour.AvailableToHero = armour.AvailableToHero;
            oldArmour.Value = armour.Value;

            if (!string.IsNullOrWhiteSpace(armour.Note)) oldArmour.Note = armour.Note;
            ArmourRepo.Update(oldArmour);
        }

        // DELETE api/<ArmourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ArmourRepo.Delete(id);
        }
    }
}
