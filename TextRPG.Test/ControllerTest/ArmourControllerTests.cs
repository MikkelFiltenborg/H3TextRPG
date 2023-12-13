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
    public class ArmourControllerTests
    {
        private readonly ArmourController controller;
        private readonly Mock<IBaseCRUDRepo<Armour>> repo = new();

        public ArmourControllerTests()
        {
            controller = new ArmourController(repo.Object);
        }

        [Fact]
        public async Task ArmourRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetArmourData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Armour>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostArmour(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ArmourRepo_GetAll_Return200()
        {
            //Arrange
            List<Armour> list = new List<Armour>()
            {
                MockDataRepos.GetArmourData(1),
                MockDataRepos.GetArmourData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllArmour();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ArmourRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetArmourData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetArmourById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ArmourRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetArmourData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteArmour(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ArmourRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetArmourData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Armour>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutArmour(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
