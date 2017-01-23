using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC6_WEBAPI_MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TestLibrary
{
    [TestClass]
    public class Test
    {
        static DataAccess data;
        static List<ObjectId> ids;
        static bool cleanup = true;

        [AssemblyInitialize]
        public static void init(TestContext context)
        {
            data = new DataAccess();
            ids = new List<ObjectId>();
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            if (cleanup)
                foreach (var id in ids)
                {
                    var productDeleted = data.Remove(id);
                    Assert.AreEqual(true, productDeleted.IsAcknowledged);
                }
        }

        [TestMethod]
        [Priority(0)]
        public void CreateProductWithStaticData()
        {
            Product pTest = new Product()
            {
                Category = "Test",
                Price = 250.3,
                ProductId = 4,
                ProductName = "Test CreateProductWithStaticData"
            };

            data.Create(pTest);

            Assert.IsNotNull(pTest.Id);
            ids.Add(pTest.Id);
        }

        [TestMethod]
        [Priority(1)]
        public void CreateProductWithDynamicData()
        {
            Product pTest = new Product()
            {
                Category = "Test",
                Price = 750.38,
                ProductId = 5,
                ProductName = "Test CreateProductWithDynamicData"
            };
            List<BsonElement> metadata = new List<BsonElement>();
            metadata.Add(new BsonElement("Metadata 1", "Valor Metadata 1"));
            metadata.Add(new BsonElement("Metadata 2", "Valor Metadata 2"));
            metadata.Add(new BsonElement("Metadata 3", "Valor Metadata 3"));

            pTest.Metadata.AddRange(metadata);
            data.Create(pTest);

            Assert.IsNotNull(pTest.Id);
            Assert.AreEqual(3, pTest.Metadata.ElementCount);
            ids.Add(pTest.Id);
        }

        [TestMethod]
        [Priority(2)]
        public void UpdateProductStaticData()
        {
            Product p = data.GetProductByProperty("ProductId", 4);            
        
            var result = data.Update(p.Id, 
                                        new KeyValuePair<string, object>("ProductName", "Test CreateProductWithStaticData Modificado"),
                                        new KeyValuePair<string, object>("Price", 102.0)
            );

            p = data.GetProductByProperty("ProductId", 4);

            Assert.AreEqual(true, result.IsAcknowledged);
            Assert.AreEqual(1, result.ModifiedCount);
            Assert.AreEqual("Test CreateProductWithStaticData Modificado", p.ProductName);
            Assert.AreEqual(102, p.Price);
        }
    }
}
