using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public class ArmourRepoTest
    {
        // Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly ArmourRepo armourRepo;
        public ArmourRepoTest()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("TestDay").Options;

            context = new Dbcontext(options);
            armourRepo = new ArmourRepo(context);
        }

        private void Arrange()
        {
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetArmourData(1));
            context.Add(MockDataRepos.GetArmourData(2));
            context.SaveChanges();
        }

        // Tests begins here
        [Fact]
        public async void ArmourRepo_CreateNewArmour_OnSuccess()
        {
            // Arrange
            Arrange();

            // Act
            var armourMock = MockDataRepos.GetArmourData(3);

            var returnValue = await armourRepo.Create(armourMock);
            context.SaveChanges();

            // Assert
            Assert.Equal(armourMock.Id, returnValue.Id);
            Assert.Equal(armourMock.ArmourTypeName, returnValue.ArmourTypeName);
            //Assert.Equal(newArmourModifier, returnValue.ArmourModifier);
            //Assert.Equal(newAvailableToHero, returnValue.AvailableToHero);
            //Assert.Equal(newValue, returnValue.Value);
            //Assert.Equal(newNote, returnValue.Note);
        }

        [Fact]
        public async void ArmourRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            Arrange();

            string errormessage = "System.InvalidOperationException";

            //Act
            var armourMock = MockDataRepos.GetArmourData(1);

            Task result() => armourRepo.Create(armourMock);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void ArmourRepo_GetAllArmour_OnSuccess()
        {
            // Arrange
            Arrange();

            // Act
            var result = await armourRepo.GetAll();
            int amount = result.Count();

            // Assert
            Assert.IsType<List<Armour>>(result);
            Assert.Equal(2, amount);
        }

        [Fact]
        public async void ArmourRepo_GetOneArmourById_OnSuccess()
        {
            // Arrange
            Arrange();
            int armourId = 1;

            // Act
            var result = await armourRepo.GetById(armourId);

            // Assert
            Assert.Equal(armourId, result.Id);
        }

        [Fact]
        public async void ArmourRepo_GetInvalidArmourById_OnFailure()
        {
            // Arrange
            Arrange();

            int armourId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            // Act
            Task result() => armourRepo.GetById(armourId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert

            // Compare message
            Assert.Equal(errormessage1, exception.Message);
            // Compare type of Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void ArmourRepo_DeleteOneArmour_OnSuccess()
        {
            // Arrange
            Arrange();
            int armourId = 1;

            // Act
            var resultBefore = await armourRepo.GetAll();
            var amountBefore = resultBefore.Count();
            await armourRepo.Delete(armourId);
            var resultAfter = await armourRepo.GetAll();
            var amountAfter = resultAfter.Count();

            // Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void ArmourRepo_DeleteInvalidArmour_OnFailure()
        {
            // Arrange
            Arrange();

            int armourId = 3;
            string errormessage = "Sequence contains no elements";

            // Act
            Task result() => armourRepo.Delete(armourId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void ArmourRepo_UpdateOneArmour_OnSuccess()
        {
            // Arrange
            Arrange();

            // Act
            var armourMock = MockDataRepos.GetArmourData(2);
            var result = await armourRepo.Update(armourMock);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            //Assert.Equal(newArmourTypeName, result.ArmourTypeName);
            //Assert.Equal(newArmourModifier, result.ArmourModifier);
            //Assert.Equal(newAvailableToHero, result.AvailableToHero);
            //Assert.Equal(newValue, result.Value);
            //Assert.Equal(newNote, result.Note);
        }

        [Fact]
        public async void ArmourRepo_UpdateInvalidArmour_OnFailure()
        {
            // Arrange
            Arrange();

            string errormessage = "Sequence contains no elements";

            // Act
            var armourMock = MockDataRepos.GetArmourData(3);
            Task result() => armourRepo.Update(armourMock);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}