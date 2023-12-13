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
    public class WeaponTypeRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly WeaponTypeRepo WeaponTypeRepo;
        public WeaponTypeRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("WeaponTypeRepo").Options;

            context = new Dbcontext(options);
            WeaponTypeRepo = new WeaponTypeRepo(context);
        }

        //tests begins here

        [Fact]
        public async void WeaponTypeRepo_CreateNewWeaponType_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int newWeaponTypeId = 3;
            var item = MockDataRepos.GetWeaponTypeData(newWeaponTypeId);

            //Act
            var returnValue = await WeaponTypeRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newWeaponTypeId, returnValue.Id);
        }

        [Fact]
        public async void WeaponTypeRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int newWeaponTypeId = 2;
            //string newWeaponTypeType = "WeaponType-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetWeaponTypeData(newWeaponTypeId);


            Task result() => WeaponTypeRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void WeaponTypeRepo_GetAllWeaponTypes_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();



            //Act
            var result = await WeaponTypeRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<WeaponType>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void WeaponTypeRepo_GetOneWeaponTypeById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await WeaponTypeRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void WeaponTypeRepo_GetInvalidWeaponTypeById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int WeaponTypeId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => WeaponTypeRepo.GetById(WeaponTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void WeaponTypeRepo_DeleteOneWeaponType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await WeaponTypeRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await WeaponTypeRepo.Delete(id);
            var resultAfter = await WeaponTypeRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void WeaponTypeRepo_DeleteInvalidWeaponType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int WeaponTypeId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => WeaponTypeRepo.Delete(WeaponTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void WeaponTypeRepo_UpdateOneWeaponType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int WeaponTypeId = 2;

            var item = MockDataRepos.GetWeaponTypeData(WeaponTypeId);

            //Act
            var result = await WeaponTypeRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void WeaponTypeRepo_UpdateInvalidWeaponType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponTypeData(1));
            context.Add(MockDataRepos.GetWeaponTypeData(2));
            context.SaveChanges();

            int WeaponTypeId = 3;

            var item = MockDataRepos.GetWeaponTypeData(WeaponTypeId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => WeaponTypeRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
