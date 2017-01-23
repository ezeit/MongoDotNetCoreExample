using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6_WEBAPI_MongoDB.Models
{
    public class DataAccess
    {
        MongoClient _client;       
        IMongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");            
            _db = _client.GetDatabase("EmployeeDB");            
        }

        public IEnumerable<Product> GetProducts()
        {          
            var products = _db.GetCollection<Product>("Products").Find(_ => true).ToEnumerable<Product>();
            return products;
        }


        public Product GetProduct(ObjectId id)
        {            
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            return _db.GetCollection<Product>("Products").Find(filter).First();
        }

        public Product GetProductByProperty(string propertyName, object propertyValue)
        {
            var filter = Builders<Product>.Filter.Eq(propertyName,propertyValue);
            return _db.GetCollection<Product>("Products").Find(filter).First();
        }
        public Product Create(Product p)
        {
            _db.GetCollection<Product>("Products").InsertOne(p);
            return p;
        }

        public UpdateResult Update(ObjectId id, params object[] updates)
        {
            List<KeyValuePair<string, object>> updateInfo = new List<KeyValuePair<string, object>>();
            foreach (var item in updates)
            {
                var info = item as Nullable<KeyValuePair<string, object>>;
                if(info.HasValue)
                    updateInfo.Add(info.Value);
            }
            var filter = Builders<Product>.Filter.Eq(x=>x.Id, id);
            var updateOperation = Builders<Product>.Update;
            UpdateDefinition<Product> updateDefinition = null;
            foreach (var item in updateInfo)
            {                
                if(updateDefinition == null)
                {
                    updateDefinition = updateOperation.Set(item.Key, item.Value);
                }else
                {
                    updateDefinition = updateDefinition.Set(item.Key, item.Value);
                }
            }

            return _db.GetCollection<Product>("Products").UpdateOne(filter, updateDefinition);
        }

        public DeleteResult Remove(ObjectId id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var operation = _db.GetCollection<Product>("Products").DeleteOne(filter);
            return operation;
        }
    }
}
