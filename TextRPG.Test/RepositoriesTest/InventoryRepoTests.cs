using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;
using TextRPG.Repository.Server;
using TextRPG.Test.MockData;

namespace TextRPG.Test.RepositoriesTest
{
    public class InventoryRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly InventoryRepo InventoryRepo;
        public InventoryRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("InventoryRepo").Options;

            context = new Dbcontext(options);
            InventoryRepo = new InventoryRepo(context);
        }

        //tests begins here

        [Fact]
        public async void InventoryRepo_CreateNewInventory_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int newInventoryId = 3;
            var item = MockDataRepos.GetInventoryData(newInventoryId);

            //Act
            var returnValue = await InventoryRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newInventoryId, returnValue.Id);
        }

        [Fact]
        public async void InventoryRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int newInventoryId = 2;
            //string newInventoryType = "Inventory-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetInventoryData(newInventoryId);


            Task result() => InventoryRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void InventoryRepo_GetAllInventorys_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();



            //Act
            var result = await InventoryRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Inventory>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void InventoryRepo_GetOneInventoryById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await InventoryRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void InventoryRepo_GetInvalidInventoryById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int InventoryId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => InventoryRepo.GetById(InventoryId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void InventoryRepo_DeleteOneInventory_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await InventoryRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await InventoryRepo.Delete(id);
            var resultAfter = await InventoryRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void InventoryRepo_DeleteInvalidInventory_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int InventoryId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => InventoryRepo.Delete(InventoryId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void InventoryRepo_UpdateOneInventory_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int InventoryId = 2;

            var item = MockDataRepos.GetInventoryData(InventoryId);

            //Act
            var result = await InventoryRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void InventoryRepo_UpdateInvalidInventory_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetInventoryData(1));
            context.Add(MockDataRepos.GetInventoryData(2));
            context.SaveChanges();

            int InventoryId = 3;

            var item = MockDataRepos.GetInventoryData(InventoryId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => InventoryRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
