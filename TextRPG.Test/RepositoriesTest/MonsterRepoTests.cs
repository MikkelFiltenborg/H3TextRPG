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
    public class MonsterRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly MonsterRepo MonsterRepo;
        public MonsterRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("TestDay").Options;

            context = new Dbcontext(options);
            MonsterRepo = new MonsterRepo(context);
        }

        //tests begins here

        [Fact]
        public async void MonsterRepo_CreateNewMonster_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int newMonsterId = 3;
            string newMonsterType = "MonsterName-3";
            var item = MockDataRepos.GetMonsterData(newMonsterId);

            //Act
            var returnValue = await MonsterRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newMonsterId, returnValue.Id);
            Assert.Equal(newMonsterType, returnValue.MonsterName);
        }

        [Fact]
        public async void MonsterRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int newMonsterId = 2;
            //string newMonsterType = "Monster-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetMonsterData(newMonsterId);


            Task result() => MonsterRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void MonsterRepo_GetAllMonsters_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();



            //Act
            var result = await MonsterRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Monster>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void MonsterRepo_GetOneMonsterById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await MonsterRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void MonsterRepo_GetInvalidMonsterById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int MonsterId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => MonsterRepo.GetById(MonsterId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void MonsterRepo_DeleteOneMonster_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await MonsterRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await MonsterRepo.Delete(id);
            var resultAfter = await MonsterRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void MonsterRepo_DeleteInvalidMonster_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int MonsterId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => MonsterRepo.Delete(MonsterId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void MonsterRepo_UpdateOneMonster_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int MonsterId = 2;
            string MonsterName = "MonsterName-2";

            var item = MockDataRepos.GetMonsterData(MonsterId);

            //Act
            var result = await MonsterRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Contains(MonsterName, result.MonsterName);

        }

        [Fact]
        public async void MonsterRepo_UpdateInvalidMonster_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetMonsterData(1));
            context.Add(MockDataRepos.GetMonsterData(2));
            context.SaveChanges();

            int MonsterId = 3;

            var item = MockDataRepos.GetMonsterData(MonsterId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => MonsterRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
