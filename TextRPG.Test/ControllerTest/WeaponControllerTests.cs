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
    public class WeaponControllerTests
    {
        private readonly WeaponController controller;
        private readonly Mock<IBaseCRUDRepo<Weapon>> repo = new();

        public WeaponControllerTests()
        {
            controller = new WeaponController(repo.Object);
        }

        [Fact]
        public async Task WeaponRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetWeaponData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Weapon>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostWeapon(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponRepo_GetAll_Return200()
        {
            //Arrange
            List<Weapon> list = new List<Weapon>()
            {
                MockDataRepos.GetWeaponData(1),
                MockDataRepos.GetWeaponData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllWeapon();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetWeaponById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteWeapon(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task WeaponRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetWeaponData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Weapon>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutWeapon(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
