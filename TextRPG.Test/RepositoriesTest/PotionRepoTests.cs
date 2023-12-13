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
    public class PotionRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly PotionRepo PotionRepo;
        public PotionRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("PotionRepo").Options;

            context = new Dbcontext(options);
            PotionRepo = new PotionRepo(context);
        }

        //tests begins here

        [Fact]
        public async void PotionRepo_CreateNewPotion_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int newPotionId = 3;
            var item = MockDataRepos.GetPotionData(newPotionId);

            //Act
            var returnValue = await PotionRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newPotionId, returnValue.Id);
        }

        [Fact]
        public async void PotionRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int newPotionId = 2;
            //string newPotionType = "Potion-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetPotionData(newPotionId);


            Task result() => PotionRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void PotionRepo_GetAllPotions_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();



            //Act
            var result = await PotionRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Potion>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void PotionRepo_GetOnePotionById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await PotionRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void PotionRepo_GetInvalidPotionById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int PotionId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => PotionRepo.GetById(PotionId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void PotionRepo_DeleteOnePotion_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await PotionRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await PotionRepo.Delete(id);
            var resultAfter = await PotionRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void PotionRepo_DeleteInvalidPotion_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int PotionId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => PotionRepo.Delete(PotionId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void PotionRepo_UpdateOnePotion_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int PotionId = 2;

            var item = MockDataRepos.GetPotionData(PotionId);

            //Act
            var result = await PotionRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void PotionRepo_UpdateInvalidPotion_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionData(1));
            context.Add(MockDataRepos.GetPotionData(2));
            context.SaveChanges();

            int PotionId = 3;

            var item = MockDataRepos.GetPotionData(PotionId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => PotionRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
