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
    public class CareerControllerTests
    {
        private readonly CareerController controller;
        private readonly Mock<IBaseCRUDRepo<Career>> repo = new();

        public CareerControllerTests()
        {
            controller = new CareerController(repo.Object);
        }

        [Fact]
        public async Task CareerRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetCareerData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Career>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostCareer(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CareerRepo_GetAll_Return200()
        {
            //Arrange
            List<Career> list = new List<Career>()
            {
                MockDataRepos.GetCareerData(1),
                MockDataRepos.GetCareerData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllCareer();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CareerRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetCareerData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetCareerById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CareerRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetCareerData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteCareer(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CareerRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetCareerData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Career>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutCareer(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
