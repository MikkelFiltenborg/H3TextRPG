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
    public class PotionTypeControllerTests
    {
        private readonly PotionTypeController controller;
        private readonly Mock<IBaseCRUDRepo<PotionType>> repo = new();

        public PotionTypeControllerTests()
        {
            controller = new PotionTypeController(repo.Object);
        }

        [Fact]
        public async Task PotionTypeRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetPotionTypeData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<PotionType>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostPotionType(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PotionTypeRepo_GetAll_Return200()
        {
            //Arrange
            List<PotionType> list = new List<PotionType>()
            {
                MockDataRepos.GetPotionTypeData(1),
                MockDataRepos.GetPotionTypeData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllPotionType();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PotionTypeRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetPotionTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetPotionTypeById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PotionTypeRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetPotionTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeletePotionType(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PotionTypeRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetPotionTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<PotionType>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutPotionType(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
