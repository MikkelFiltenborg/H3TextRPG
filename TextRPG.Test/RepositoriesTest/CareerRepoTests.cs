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
    public class CareerRepoTests
    {
        // Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly CareerRepo careerRepo;
        public CareerRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("TestDay").Options;

            context = new Dbcontext(options);
            careerRepo = new CareerRepo(context);
        }

        private void Arrange()
        {
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetCareerData(1));
            context.Add(MockDataRepos.GetCareerData(2));
            context.SaveChanges();
        }

        // Tests begins here
        [Fact]
        public async void CareerRepo_CreateNewCareer_OnSuccess()
        {
            // Arrange
            Arrange();

            int newCareerId = 3;
            string newCareerType = "Career-3";

            // Act
            Career career = new Career()
            {
                Id = newCareerId,
                CareerType = newCareerType,
            };

            var returnValue = await careerRepo.Create(career);
            context.SaveChanges();

            // Assert
            Assert.Equal(newCareerId, returnValue.Id);
            Assert.Equal(newCareerType, returnValue.CareerType);
        }

        [Fact]
        public async void CareerRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            Arrange();

            int newCareerId = 2;
            string newCareerType = "Career-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            Career career = new Career()
            {
                Id = newCareerId,
                CareerType = newCareerType,
            };

            Task result() => careerRepo.Create(career);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void CareerRepo_GetAllCareer_OnSuccess()
        {
            // Arrange
            Arrange();

            // Act
            var result = await careerRepo.GetAll();
            int amount = result.Count();

            // Assert
            Assert.IsType<List<Career>>(result);
            Assert.Equal(2, amount);
        }

        [Fact]
        public async void CareerRepo_GetOneCareerById_OnSuccess()
        {
            // Arrange
            Arrange();
            int careerId = 1;

            // Act
            var result = await careerRepo.GetById(careerId);

            // Assert
            Assert.Equal(careerId, result.Id);
        }

        [Fact]
        public async void CareerRepo_GetInvalidCareerById_OnFailure()
        {
            // Arrange
            Arrange();

            int careerId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            // Act
            Task result() => careerRepo.GetById(careerId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert

            // Compare message
            Assert.Equal(errormessage1, exception.Message);
            // Compare type of Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void CareerRepo_DeleteOneCareer_OnSuccess()
        {
            // Arrange
            Arrange();
            int careerId = 1;

            // Act
            var resultBefore = await careerRepo.GetAll();
            var amountBefore = resultBefore.Count();
            await careerRepo.Delete(careerId);
            var resultAfter = await careerRepo.GetAll();
            var amountAfter = resultAfter.Count();

            // Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void CareerRepo_DeleteInvalidCareer_OnFailure()
        {
            // Arrange
            Arrange();

            int careerId = 3;
            string errormessage = "Sequence contains no elements";

            // Act
            Task result() => careerRepo.Delete(careerId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void CareerRepo_UpdateOneCareer_OnSuccess()
        {
            // Arrange
            Arrange();
            int careerId = 2;
            string careerType = "Career2";

            Career career = new Career()
            {
                Id = careerId,
                CareerType = careerType,
            };

            // Act
            var result = await careerRepo.Update(career);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Contains(careerType, result.CareerType);
        }

        [Fact]
        public async void CareerRepo_UpdateInvalidCareer_OnFailure()
        {
            // Arrange
            Arrange();

            int careerId = 3;
            string careerType = "Career2";

            Career career = new Career()
            {
                Id = careerId,
                CareerType = careerType,
            };

            string errormessage = "Sequence contains no elements";

            // Act
            Task result() => careerRepo.Update(career);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}