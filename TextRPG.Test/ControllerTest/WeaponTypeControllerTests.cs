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
    public class WeaponTypeControllerTests
    {
        private readonly WeaponTypeController controller;
        private readonly Mock<IBaseCRUDRepo<WeaponType>> repo = new();

        public WeaponTypeControllerTests()
        {
            controller = new WeaponTypeController(repo.Object);
        }

        [Fact]
        public async Task WeaponTypeRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetWeaponTypeData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<WeaponType>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostWeaponType(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponTypeRepo_GetAll_Return200()
        {
            //Arrange
            List<WeaponType> list = new List<WeaponType>()
            {
                MockDataRepos.GetWeaponTypeData(1),
                MockDataRepos.GetWeaponTypeData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllWeaponType();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponTypeRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetWeaponTypeById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponTypeRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteWeaponType(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponTypeRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<WeaponType>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutWeaponType(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
