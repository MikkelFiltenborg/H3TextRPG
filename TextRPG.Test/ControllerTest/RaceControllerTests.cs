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
    public class RaceControllerTests
    {
        private readonly RaceController controller;
        private readonly Mock<IBaseCRUDRepo<Race>> repo = new();

        public RaceControllerTests()
        {
            controller = new RaceController(repo.Object);
        }

        [Fact]
        public async Task RaceRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetRaceData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Race>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostRace(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RaceRepo_GetAll_Return200()
        {
            //Arrange
            List<Race> list = new List<Race>()
            {
                MockDataRepos.GetRaceData(1),
                MockDataRepos.GetRaceData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllRace();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RaceRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetRaceData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetRaceById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RaceRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetRaceData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteRace(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RaceRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetRaceData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Race>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutRace(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
