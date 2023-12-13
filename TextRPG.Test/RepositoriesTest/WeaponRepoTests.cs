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
    public class WeaponRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly WeaponRepo WeaponRepo;
        public WeaponRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("WeaponRepo").Options;

            context = new Dbcontext(options);
            WeaponRepo = new WeaponRepo(context);
        }

        //tests begins here

        [Fact]
        public async void WeaponRepo_CreateNewWeapon_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int newWeaponId = 3;
            var item = MockDataRepos.GetWeaponData(newWeaponId);

            //Act
            var returnValue = await WeaponRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newWeaponId, returnValue.Id);
        }

        [Fact]
        public async void WeaponRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int newWeaponId = 2;
            //string newWeaponType = "Weapon-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetWeaponData(newWeaponId);


            Task result() => WeaponRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void WeaponRepo_GetAllWeapons_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();



            //Act
            var result = await WeaponRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Weapon>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void WeaponRepo_GetOneWeaponById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await WeaponRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void WeaponRepo_GetInvalidWeaponById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int WeaponId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => WeaponRepo.GetById(WeaponId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void WeaponRepo_DeleteOneWeapon_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await WeaponRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await WeaponRepo.Delete(id);
            var resultAfter = await WeaponRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void WeaponRepo_DeleteInvalidWeapon_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int WeaponId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => WeaponRepo.Delete(WeaponId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void WeaponRepo_UpdateOneWeapon_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int WeaponId = 2;

            var item = MockDataRepos.GetWeaponData(WeaponId);

            //Act
            var result = await WeaponRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void WeaponRepo_UpdateInvalidWeapon_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetWeaponData(1));
            context.Add(MockDataRepos.GetWeaponData(2));
            context.SaveChanges();

            int WeaponId = 3;

            var item = MockDataRepos.GetWeaponData(WeaponId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => WeaponRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
