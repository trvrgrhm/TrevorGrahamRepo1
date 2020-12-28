using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RpsGame_NoDb;
using Xunit;

namespace RpsGameMain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            //creating the in-memory Db
            var options = new DbContextOptionsBuilder<RpsDbContext>().UseInMemoryDatabase(databaseName:"TestDb").Options;


            //act
            //add to the in-memory db
            using (var context = new RpsDbContext(options)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);

                context.SaveChanges();                
            }

            //assert
            //verify the result was as expected
            using (var context = new RpsDbContext(options)){
                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);
                
            }
        }
        [Theory]
        [InlineData(6,7)]
        [InlineData(5,8)]
        [InlineData(4,9)]
        public void VariousInputsEqualThirteen(int x, int y){
            //act
            int z = x + y;
            //assert
            Assert.Equal(13,z);
        }
        [Theory]
        [InlineData(5,7)]
        [InlineData(4,8)]
        [InlineData(3,9)]
        public void VariousInputsDoNotEqualThirteen(int x, int y){
            //act
            int z = x + y;
            //assert
            Assert.NotEqual(13,z);
        }
        [Fact]
        public void CreatePlayerAndAddsToDatabase()
        {
            //arrange
            //creating the in-memory Db
            var options = new DbContextOptionsBuilder<RpsDbContext>().UseInMemoryDatabase(databaseName:"TestDb").Options;


            //act
            //add to the in-memory db
            Player p1= new Player();
            string fName = "Sparky";
            string lName = "Jones";


            using (var context = new RpsDbContext(options)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);
                p1 = repo.CreatePlayer(fName,lName);

                context.SaveChanges();                
            }

            //assert
            //verify the result was as expected
            using (var context = new RpsDbContext(options)){
                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);
                // Player result = context.players.Where(x => x.Fname == fName && x.Lname == lName).FirstOrDefault();
                Player result = repo.CreatePlayer("Sparky","Jones");
                Assert.True(p1.playerId.Equals(result.playerId));
            }
        }

    }
}
