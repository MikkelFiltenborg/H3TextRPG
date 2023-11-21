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

        // GET: api/<PotionController>
        [HttpGet]
        public List<Potion> Get()
        {
            return PotionRepo.GetAll();
        }

        // GET api/<PotionController>/5
        [HttpGet("{id}")]
        public Potion Get(int id)
        {
            return PotionRepo.GetById(id);
        }

        // POST api/<PotionController>
        [HttpPost]
        public void Post([FromBody] Potion potion)
        {
            PotionRepo.Create(potion);
        }

        // PUT api/<PotionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Potion newPotion)
        {
            var oldPotion = PotionRepo.GetById(newPotion.Id);

            if (newPotion == oldPotion) Console.Write("Potion already exists");

            else
            {
                oldPotion.Amount = newPotion.Amount;
                oldPotion.PotionType = newPotion.PotionType;

                PotionRepo.Update(oldPotion);
            }
        }

        // DELETE api/<PotionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PotionRepo.Delete(id);
        }
    }
}
