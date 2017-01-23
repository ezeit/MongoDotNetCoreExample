using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6_WEBAPI_MongoDB.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        [BsonElement("ProductId")]
        public int ProductId { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("Price")]
        public double Price { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }

        public BsonDocument Metadata { get; set; }

        public Product()
        {
            this.Metadata = new BsonDocument();
        }
    }
}
