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
        //IBaseCRUDRepo<EntityBaseSystem> EBSRepo {  get; set; }
        //IBaseCRUDRepo<Inventory> InventoryRepo { get; set; }
        public HeroController(
            IBaseCRUDRepo<Hero> heroRepo
            //IBaseCRUDRepo<EntityBaseSystem> ebsRepo,
            //IBaseCRUDRepo<Inventory> inventoryRepo
            )
        {
            HeroRepo = heroRepo;
            //EBSRepo = ebsRepo;
            //InventoryRepo = inventoryRepo;
        }

        // GET: api/<HeroController>
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            //HeroRepo.GetAll();
            return HeroRepo.GetAll();
        }

        // GET api/<HeroController>/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            Hero hero = HeroRepo.GetById(id);
            return hero;
        }

        // POST api/<HeroController>
        [HttpPost]
        public void Post([FromBody] Hero hero)
        {
            HeroRepo.Create(hero);
        }

        // PUT api/<HeroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hero hero)
        {
            HeroRepo.Update(hero);
        }

        // DELETE api/<HeroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            HeroRepo.Delete(id);
        }
    }
}
