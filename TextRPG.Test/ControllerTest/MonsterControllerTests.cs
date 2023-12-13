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
    public class MonsterControllerTests
    {
        private readonly MonsterController controller;
        private readonly Mock<IBaseCRUDRepo<Monster>> repo = new();

        public MonsterControllerTests()
        {
            controller = new MonsterController(repo.Object);
        }

        [Fact]
        public async Task MonsterRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetMonsterData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<Monster>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostMonster(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MonsterRepo_GetAll_Return200()
        {
            //Arrange
            List<Monster> list = new List<Monster>()
            {
                MockDataRepos.GetMonsterData(1),
                MockDataRepos.GetMonsterData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllMonster();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MonsterRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetMonsterData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetMonsterById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MonsterRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetMonsterData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteMonster(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MonsterRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetMonsterData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<Monster>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutMonster(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
