using MongoDB.Bson;
using MVC6_WEBAPI_MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new DataAccess();
            var p = data.GetProducts();
            //List<BsonElement> metadata = new List<BsonElement>();
            //metadata.Add(new BsonElement("Descripción", "Mesa clásica con descripción"));
            //metadata.Add(new BsonElement("Ubicación", 3));
            //Product pTest = new Product() {
            //    Category = "Casa",
            //    Price = 150,
            //    ProductId =3,
            //    ProductName = "Mesa clásica 02",
            //    Metadata = new BsonDocument(metadata)
            //};
            //
            //data.Create(pTest);

            Product pTest = data.GetProductByProperty("ProductId", 3);

            foreach (var meta in pTest.Metadata.Elements)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("{0}:{1}", meta.Name, meta.Value.ToString());
            }
        }
    }
}
