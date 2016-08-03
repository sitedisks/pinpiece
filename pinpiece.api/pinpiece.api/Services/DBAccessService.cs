using MongoDB.Driver;
using pinpiece.api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pinpiece.api.Properties;
using MongoDB.Bson;

namespace pinpiece.api.Services
{
    public class DBAccessService
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _mongoDb;

        public async Task<IList<Restaurant>> MLabConnection()
        {
            IList<Restaurant> restaurantBook = new List<Restaurant>();
            try
            {
                string conn = app.Default.mlabconnection;
                _client = new MongoClient(conn);
                _mongoDb = _client.GetDatabase("heroku_dfqn70ws");
                var collection = _mongoDb.GetCollection<BsonDocument>("restaurants");

                var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");

                var result = await collection.Find(filter).Limit(app.Default.numberOfPins).ToListAsync();

                foreach (var item in result)
                {

                    BsonValue address = item["address"];

                    Address newAdd = new Address
                    {
                        building = address["building"].ToString(),
                        street = address["street"].ToString(),
                        zipcode = address["zipcode"].ToString(),
                        coord = new Coord
                        {
                            lng = address["coord"].AsBsonArray[0].ToDouble(),
                            lat = address["coord"].AsBsonArray[1].ToDouble(),
                        }
                    };

                    Restaurant newRest = new Restaurant
                    {
                        borough = item["borough"].ToString(),
                        cuisine = item["cuisine"].ToString(),
                        name = item["name"].ToString(),
                        address = newAdd
                    };

                    restaurantBook.Add(newRest);

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            return restaurantBook;
        }
    }
}