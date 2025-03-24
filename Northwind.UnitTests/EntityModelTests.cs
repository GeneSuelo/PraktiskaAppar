using Northwind.EntityModels; //För NorthwindDatabaseContext

namespace Northwind.UnitTests
{
    public class EntityModelTests
    {
        [Fact]
        public void DatabaseConnectTest()
        {
            using NorthwindDatabaseContext db = new();

            Assert.True(db.Database.CanConnect()); // testa database connection
        }

        [Fact]
        public void CategoryCountTest() 
        {
            using NorthwindDatabaseContext db = new();
            int expected = 8;
            int actual = db.Categories.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProductId1IsChaiTest()
        {
            using NorthwindDatabaseContext db = new();
            string expected = "Chai";
            Product? productmedId1 = db.Products.Find(1);
            string actual = productmedId1?.ProductName ?? string.Empty; //kontrollerar null

            Assert.Equal(expected, actual);
        }
    }
}
