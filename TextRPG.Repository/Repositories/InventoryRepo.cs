using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;

namespace TextRPG.Repository.Repositories
{
    public class InventoryRepo : IBaseCRUDRepo<Inventory>
    {
        Dbcontext context;

        public InventoryRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Inventory>> GetAll()
        {
            return await context.Inventory
                .Include(x => x.Weapons)
                .Include(x => x.Armour)
                .Include(x => x.Potions)
                .ToListAsync();
        }

        // GetById
        public async Task<Inventory> GetById(int id)
        {
            return await context.Inventory
                .Include(x => x.Weapons)
                .Include(x => x.Armour)
                .Include(x => x.Potions)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Inventory> Create(Inventory newInventory)
        {
            context.Inventory.Add(newInventory);
            await context.SaveChangesAsync();
            return newInventory;
        }

        // Update
        public async Task<Inventory?> Update(Inventory updateInventory)
        {
            Inventory inventory = await GetById(updateInventory.Id);
            if (inventory != null && updateInventory != null)
            {
                inventory.Gold = updateInventory.Gold;
                context.Inventory.Update(inventory);
                await context.SaveChangesAsync();
            }
            return inventory;
        }

        // Delete
        public async Task<Inventory> Delete(int id)
        {
            Inventory inventory = await GetById(id);
            if (inventory != null)
            {
                context.Inventory.Remove(inventory);
                await context.SaveChangesAsync();
            }
            return inventory!;
        }
    }
}
