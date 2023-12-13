using Microsoft.EntityFrameworkCore;
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
        public async void CareerRepo_GetAllRaces_OnSuccess()
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
            int id = 1;

            // Act
            var result = await careerRepo.GetById(id);

            // Assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async void CareerRepo_DeleteOneCareer_OnSuccess()
        {
            // Arrange
            Arrange();
            int id = 1;

            // Act
            var resultBefore = await careerRepo.GetAll();
            var amountBefore = resultBefore.Count();
            await careerRepo.Delete(id);
            var resultAfter = await careerRepo.GetAll();
            var amountAfter = resultAfter.Count();

            // Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void CareerRepo_UpdateOneCareer_OnSuccess()
        {
            // Arrange
            Arrange();
            int careerId = 2;
            string careerType = "Race2";

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
    }
}
