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
    public class RaceRepoTests
    {
        //Set up Mock DataBase
        public Dbcontext context {  get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly RaceRepo RaceRepo;
        public RaceRepoTests() 
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("RaceRepo").Options;

            context = new Dbcontext(options);
            RaceRepo = new RaceRepo(context);
        }

        //tests begins here

        [Fact]
        public async void RaceRepo_CreateNewRace_OnSucces()
        {

            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int newRaceId = 3;
            string newRaceType = "Race-3";

            //Act
            Race race = new Race()
            {
                Id = newRaceId,
                RaceType = newRaceType,
            };
            var returnValue = await RaceRepo.Create(race);
            context.SaveChanges();

            //Assert
            Assert.Equal(newRaceId, returnValue.Id);
            Assert.Equal(newRaceType, returnValue.RaceType);
        }

        [Fact]
        public async void RaceRepo_CreateHasSameIdAsAnother_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int newRaceId = 2;
            string newRaceType = "Race-3";
            string errormessage = "System.InvalidOperationException";

            //Act
            Race race = new Race()
            {
                Id = newRaceId,
                RaceType = newRaceType,
            };


            Task result() => RaceRepo.Create(race);

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.GetType().ToString());
        }

        [Fact]
        public async void RaceRepo_GetAllRaces_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();



            //Act
            var result = await RaceRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.IsType<List<Race>>(result);
            Assert.Equal(2, amount);

        }

        [Fact]
        public async void RaceRepo_GetOneRaceById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();
            int id = 1;

            //Act
            var result = await raceRepo.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async void RaceRepo_GetInvalidRaceById_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int RaceId = 3; 
            string errormessage1 = "Sequence contains no elements";
            string errormessage2 = "System.InvalidOperationException";

            //Act

            Task result() => RaceRepo.GetById(RaceId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert

            //Hvis du vil sammenligne message
            Assert.Equal(errormessage1, exception.Message);

            //Hvis du vil sammenligne type af Error
            Assert.Equal(errormessage2, exception.GetType().ToString());
        }

        [Fact]
        public async void RaceRepo_DeleteOneRace_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int id = 1;

            //Act
            var resultbefore = await RaceRepo.GetAll();
            var amountBefore = resultbefore.Count();
            await RaceRepo.Delete(id);
            var resultAfter = await RaceRepo.GetAll();
            var amountAfter = resultAfter.Count();

            //Assert
            Assert.NotEqual(amountBefore, amountAfter);
        }

        [Fact]
        public async void RaceRepo_DeleteInvalidRace_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int RaceId = 3;
            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => RaceRepo.Delete(RaceId);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }

        [Fact]
        public async void RaceRepo_UpdateOneRace_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int RaceId = 2;
            string RaceType = "Race2";

            Race race = new Race()
            {
                Id = RaceId,
                RaceType = RaceType,
            };

            //Act
            var result = await RaceRepo.Update(race);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Contains(RaceType, result.RaceType);

        }

        [Fact]
        public async void RaceRepo_UpdateInvalidRace_OnFailure()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataRepos.GetRaceData(1));
            context.Add(MockDataRepos.GetRaceData(2));
            context.SaveChanges();

            int RaceId = 3;
            string RaceType = "Race2";

            Race race = new Race()
            {
                Id = RaceId,
                RaceType = RaceType,
            };

            string errormessage = "Sequence contains no elements";

            //Act

            Task result() => RaceRepo.Update(race);
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            //Assert
            Assert.Equal(errormessage, exception.Message);
        }
    }
}
