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
    public class EntityBaseSystemRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly EntityBaseSystemRepo EntityBaseSystemRepo;
        public EntityBaseSystemRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("EntityBaseSystemRepo").Options;

            context = new Dbcontext(options);
            EntityBaseSystemRepo = new EntityBaseSystemRepo(context);
        }

        //tests begins here

        [Fact]
        public async void EntityBaseSystemRepo_CreateNewEntityBaseSystem_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int newEntityBaseSystemId = 3;
            var item = MockDataRepos.GetEntityBaseSystemData(newEntityBaseSystemId);

            //Act
            var returnValue = await EntityBaseSystemRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newEntityBaseSystemId, returnValue.Id);
        }

        [Fact]
        public async void EntityBaseSystemRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int newEntityBaseSystemId = 2;
            //string newEntityBaseSystemType = "EntityBaseSystem-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetEntityBaseSystemData(newEntityBaseSystemId);


            Task result() => EntityBaseSystemRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void EntityBaseSystemRepo_GetAllEntityBaseSystems_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();



            //Act
            var result = await EntityBaseSystemRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<EntityBaseSystem>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void EntityBaseSystemRepo_GetOneEntityBaseSystemById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await EntityBaseSystemRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void EntityBaseSystemRepo_GetInvalidEntityBaseSystemById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int EntityBaseSystemId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => EntityBaseSystemRepo.GetById(EntityBaseSystemId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void EntityBaseSystemRepo_DeleteOneEntityBaseSystem_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await EntityBaseSystemRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await EntityBaseSystemRepo.Delete(id);
            var resultAfter = await EntityBaseSystemRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void EntityBaseSystemRepo_DeleteInvalidEntityBaseSystem_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int EntityBaseSystemId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => EntityBaseSystemRepo.Delete(EntityBaseSystemId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void EntityBaseSystemRepo_UpdateOneEntityBaseSystem_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int EntityBaseSystemId = 2;

            var item = MockDataRepos.GetEntityBaseSystemData(EntityBaseSystemId);

            //Act
            var result = await EntityBaseSystemRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void EntityBaseSystemRepo_UpdateInvalidEntityBaseSystem_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetEntityBaseSystemData(1));
            context.Add(MockDataRepos.GetEntityBaseSystemData(2));
            context.SaveChanges();

            int EntityBaseSystemId = 3;

            var item = MockDataRepos.GetEntityBaseSystemData(EntityBaseSystemId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => EntityBaseSystemRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
