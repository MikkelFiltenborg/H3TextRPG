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
    public class PotionTypeRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly PotionTypeRepo PotionTypeRepo;
        public PotionTypeRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("PotionTypeRepo").Options;

            context = new Dbcontext(options);
            PotionTypeRepo = new PotionTypeRepo(context);
        }

        //tests begins here

        [Fact]
        public async void PotionTypeRepo_CreateNewPotionType_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int newPotionTypeId = 3;
            var item = MockDataRepos.GetPotionTypeData(newPotionTypeId);

            //Act
            var returnValue = await PotionTypeRepo.Create(item);
            context.SaveChanges();

            //Assert
            Assert.Equal(newPotionTypeId, returnValue.Id);
        }

        [Fact]
        public async void PotionTypeRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int newPotionTypeId = 2;
            //string newPotionTypeType = "PotionType-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            var item = MockDataRepos.GetPotionTypeData(newPotionTypeId);


            Task result() => PotionTypeRepo.Create(item);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void PotionTypeRepo_GetAllPotionTypes_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();



            //Act
            var result = await PotionTypeRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<PotionType>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void PotionTypeRepo_GetOnePotionTypeById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result1 = await PotionTypeRepo.GetById(id);

            //Assert
            Assert.Equal(id, result1.Id);
        }

        [Fact]
        public async void PotionTypeRepo_GetInvalidPotionTypeById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int PotionTypeId = 3;
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => PotionTypeRepo.GetById(PotionTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void PotionTypeRepo_DeleteOnePotionType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await PotionTypeRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await PotionTypeRepo.Delete(id);
            var resultAfter = await PotionTypeRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void PotionTypeRepo_DeleteInvalidPotionType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int PotionTypeId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => PotionTypeRepo.Delete(PotionTypeId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void PotionTypeRepo_UpdateOnePotionType_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int PotionTypeId = 2;

            var item = MockDataRepos.GetPotionTypeData(PotionTypeId);

            //Act
            var result = await PotionTypeRepo.Update(item);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async void PotionTypeRepo_UpdateInvalidPotionType_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetPotionTypeData(1));
            context.Add(MockDataRepos.GetPotionTypeData(2));
            context.SaveChanges();

            int PotionTypeId = 3;

            var item = MockDataRepos.GetPotionTypeData(PotionTypeId);

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => PotionTypeRepo.Update(item);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
