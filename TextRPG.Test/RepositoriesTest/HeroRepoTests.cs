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
    public class HeroRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly HeroRepo HeroRepo;
        public HeroRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("TestDay").Options;

            context = new Dbcontext(options);
            HeroRepo = new HeroRepo(context);
        }

        //tests begins here

        [Fact]
        public async void HeroRepo_CreateNewHero_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int newHeroId = 3;
            string newHeroType = "HeroName-3";
            var item = MockDataRepos.GetHeroData(newHeroId);

            //Act
            var returnValue = await HeroRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newHeroId, returnValue.Id);
            Assert.Equal(newHeroType, returnValue.HeroName);
        }

        [Fact]
        public async void HeroRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int newHeroId = 2;
            //string newHeroType = "Hero-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetHeroData(newHeroId);


            Task result() => HeroRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void HeroRepo_GetAllHeros_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();



            //Act
            var result = await HeroRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Hero>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void HeroRepo_GetOneHeroById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await HeroRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void HeroRepo_GetInvalidHeroById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int HeroId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => HeroRepo.GetById(HeroId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void HeroRepo_DeleteOneHero_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await HeroRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await HeroRepo.Delete(id);
            var resultAfter = await HeroRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void HeroRepo_DeleteInvalidHero_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int HeroId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => HeroRepo.Delete(HeroId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void HeroRepo_UpdateOneHero_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int HeroId = 2;
            string HeroName = "HeroName-2";

            var item = MockDataRepos.GetHeroData(HeroId);

            //Act
            var result = await HeroRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Contains(HeroName, result.HeroName);

        }

        [Fact]
        public async void HeroRepo_UpdateInvalidHero_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetHeroData(1));
            context.Add(MockDataRepos.GetHeroData(2));
            context.SaveChanges();

            int HeroId = 3;

            var item = MockDataRepos.GetHeroData(HeroId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => HeroRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
