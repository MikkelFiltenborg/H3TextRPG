using Microsoft.AspNetCore.Mvc;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextRPG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        IBaseCRUDRepo<Weapon> WeaponRepo { get; set; }
        public WeaponController(IBaseCRUDRepo<Weapon> weaponRepo)
        {
            WeaponRepo = weaponRepo;
        }

        // GET: api/<WeaponController>
        [HttpGet]
        public List<Weapon> Get()
        {
            return WeaponRepo.GetAll();
        }

        // GET api/<WeaponController>/5
        [HttpGet("{id}")]
        public Weapon Get(int id)
        {
            return WeaponRepo.GetById(id);
        }

        // POST api/<WeaponController>
        [HttpPost]
        public void Post([FromBody] Weapon weapon)
        {
            WeaponRepo.Create(weapon);
        }

        // PUT api/<WeaponController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Weapon newWeapon)
        {
            var oldWeapon = WeaponRepo.GetById(newWeapon.Id);

            if (newWeapon == oldWeapon) Console.Write("Weapon already exists");

            else
            {
                oldWeapon.WeaponDamageModifier = newWeapon.WeaponDamageModifier;
                oldWeapon.MinimumSkillRoll = newWeapon.MinimumSkillRoll;
                oldWeapon.Range = newWeapon.Range;
                oldWeapon.AvailableToHero = newWeapon.AvailableToHero;
                oldWeapon.StarterWeapon = newWeapon.StarterWeapon;
                oldWeapon.Value = newWeapon.Value;
                oldWeapon.Note = newWeapon.Note;
                oldWeapon.WeaponType = newWeapon.WeaponType;

                WeaponRepo.Update(oldWeapon);
            }
        }

        // DELETE api/<WeaponController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            WeaponRepo.Delete(id);
        }
    }
}
