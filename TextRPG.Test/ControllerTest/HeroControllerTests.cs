using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Threading.Tasks;
using TextRPG.API.Controllers;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Test.MockData;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TextRPG.Test.ControllerTest
{
    public class HeroControllerTests
    {
        private readonly HeroController controller;
        private readonly Mock<IBaseCRUDRepo<Hero>> repo = new();

        public HeroControllerTests()
        {
            controller = new HeroController(repo.Object);
        }

        [Fact]
        public async Task HeroRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetHeroData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Hero>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostHero(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task HeroRepo_GetAll_Return200()
        {
            //Arrange
            List<Hero> list = new List<Hero>()
            {
                MockDataRepos.GetHeroData(1),
                MockDataRepos.GetHeroData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllHero();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task HeroRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetHeroData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetHeroById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task HeroRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetHeroData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteHero(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task HeroRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetHeroData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Hero>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutHero(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
