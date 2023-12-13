using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class SkillRollTypeRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly SkillRollTypeRepo SkillRollTypeRepo;
        public SkillRollTypeRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("SkillRollTypeRepo").Options;

            context = new Dbcontext(options);
            SkillRollTypeRepo = new SkillRollTypeRepo(context);
        }

        //tests begins here

        [Fact]
        public async void SkillRollTypeRepo_CreateNewSkillRollType_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int newSkillRollTypeId = 3;
            var item = MockDataRepos.GetSkillRollTypeData(newSkillRollTypeId);

            //Act
            var returnValue = await SkillRollTypeRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newSkillRollTypeId, returnValue.Id);
        }

        [Fact]
        public async void SkillRollTypeRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int newSkillRollTypeId = 2;
            //string newSkillRollTypeType = "SkillRollType-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetSkillRollTypeData(newSkillRollTypeId);


            Task result() => SkillRollTypeRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void SkillRollTypeRepo_GetAllSkillRollTypes_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();



            //Act
            var result = await SkillRollTypeRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<SkillRollType>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void SkillRollTypeRepo_GetOneSkillRollTypeById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await SkillRollTypeRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void SkillRollTypeRepo_GetInvalidSkillRollTypeById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int SkillRollTypeId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => SkillRollTypeRepo.GetById(SkillRollTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void SkillRollTypeRepo_DeleteOneSkillRollType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await SkillRollTypeRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await SkillRollTypeRepo.Delete(id);
            var resultAfter = await SkillRollTypeRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void SkillRollTypeRepo_DeleteInvalidSkillRollType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int SkillRollTypeId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => SkillRollTypeRepo.Delete(SkillRollTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void SkillRollTypeRepo_UpdateOneSkillRollType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int SkillRollTypeId = 2;

            var item = MockDataRepos.GetSkillRollTypeData(SkillRollTypeId);

            //Act
            var result = await SkillRollTypeRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void SkillRollTypeRepo_UpdateInvalidSkillRollType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetSkillRollTypeData(1));
            context.Add(MockDataRepos.GetSkillRollTypeData(2));
            context.SaveChanges();

            int SkillRollTypeId = 3;

            var item = MockDataRepos.GetSkillRollTypeData(SkillRollTypeId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => SkillRollTypeRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
