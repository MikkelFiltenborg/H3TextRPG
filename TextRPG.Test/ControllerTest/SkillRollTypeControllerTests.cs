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
    public class SkillRollTypeControllerTests
    {
        private readonly SkillRollTypeController controller;
        private readonly Mock<IBaseCRUDRepo<SkillRollType>> repo = new();

        public SkillRollTypeControllerTests()
        {
            controller = new SkillRollTypeController(repo.Object);
        }

        [Fact]
        public async Task SkillRollTypeRepo_CreateNew_Return201()
        {
            //Arrange
            var input = MockDataRepos.GetSkillRollTypeData(1);
            int statusCode = 201;

            repo.Setup(s => s.Create(It.IsAny<SkillRollType>())).ReturnsAsync(input);

            //Act
            var result = await controller.PostSkillRollType(input);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SkillRollTypeRepo_GetAll_Return200()
        {
            //Arrange
            List<SkillRollType> list = new List<SkillRollType>()
            {
                MockDataRepos.GetSkillRollTypeData(1),
                MockDataRepos.GetSkillRollTypeData(2)
            };

            int statusCode = 200;

            repo.Setup(s => s.GetAll()).ReturnsAsync(list);

            //Act
            var result = await controller.GetAllSkillRollType();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SkillRollTypeRepo_GetById_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetSkillRollTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.GetById(id)).ReturnsAsync(item);

            //Act
            var result = await controller.GetSkillRollTypeById(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SkillRollTypeRepo_Delete_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetSkillRollTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Delete(id)).ReturnsAsync(item);

            //Act
            var result = await controller.DeleteSkillRollType(id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SkillRollTypeRepo_Update_Return200()
        {
            //Arrange
            var item = MockDataRepos.GetSkillRollTypeData(1);

            int id = 1;
            int statusCode = 200;

            repo.Setup(s => s.Update(It.IsAny<SkillRollType>())).ReturnsAsync(item);

            //Act
            var result = await controller.PutSkillRollType(item, id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(statusCode, statusCodeResult.StatusCode);
        }

    }
}
