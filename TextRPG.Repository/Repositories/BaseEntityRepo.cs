using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Repositories
{
    public abstract class BaseEntityRepo
    {
        // EntityBaseSystem
        internal void UpdateEBS(EntityBaseSystem old, EntityBaseSystem updated)
        {
            old.Strength = updated.Strength;
            old.Agility = updated.Agility;
            old.Vigor = updated.Vigor;
            old.Spirit = updated.Spirit;
            old.Health = updated.Health;
            old.Energy = updated.Energy;
            old.HealthModifier = updated.HealthModifier;
            old.EnergyModifier = updated.EnergyModifier;
            old.DamagerModifier = updated.DamagerModifier;
            old.ArmourModifier = updated.ArmourModifier;
        }

        // Inventory
        internal void UpdateInventory(Inventory old, Inventory updated)
        {
            old.Id = updated.Id;
            old.Gold = updated.Gold;
            old.ArmourId = updated.ArmourId;
        }

        // Potion
        internal void UpdatePotion(Inventory old, Inventory updated)
        {
            if (updated.Potions != null && old.Potions != null)
            {
                List<Potion> temp = new List<Potion>();
                foreach (var potion in updated.Potions)
                {
                    if (old.Potions.Exists(x => x.PotionTypeId == potion.PotionTypeId))
                    {
                        //Update potion
                        var p = old.Potions.Find(x => x.PotionTypeId == potion.PotionTypeId);
                        if (p != null)
                        {
                            p.InventoryId = potion.InventoryId;
                            p.PotionTypeId = potion.PotionTypeId;
                            p.Amount = potion.Amount;
                        }
                    }
                    else
                    {
                        //Add New potion
                        temp.Add(new Potion
                        {
                            InventoryId = old.Id,
                            PotionTypeId = potion.PotionTypeId,
                            Amount = potion.Amount
                        });
                    }
                }
                old.Potions.AddRange(temp);
            }
        }


    }
}
