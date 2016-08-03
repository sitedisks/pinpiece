using MongoDB.Driver;
using pinpiece.api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pinpiece.api.Properties;
using MongoDB.Bson;
using NLog;

namespace pinpiece.api.Services
{
    public class DBAccessService
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _mongoDb;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<IList<Restaurant>> MLabConnection()
        {
            IList<Restaurant> restaurantBook = new List<Restaurant>();
            try
            {
                _client = new MongoClient(app.Default.mlabconnection);
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
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return restaurantBook;
        }

        public async Task<int> InsertReloadData(Reload data)
        {
            try
            {
                _client = new MongoClient(app.Default.mlabconnection);
                _mongoDb = _client.GetDatabase("heroku_dfqn70ws");
                var collection = _mongoDb.GetCollection<BsonDocument>("reload");

                var record = new BsonDocument {
                    {"userid", data.UserId},
                    {"token", data.Token},
                    {"coord", new BsonArray { data.Coord.lng, data.Coord.lat } }  
                };

                await collection.InsertOneAsync(record);
                return 1;
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return 0;
        }

        public async Task<Pin> InsertPinPostData(Pin pin) {
            try
            {
                _client = new MongoClient(app.Default.mlabconnection);
                _mongoDb = _client.GetDatabase("heroku_dfqn70ws");
                var collection = _mongoDb.GetCollection<BsonDocument>("pin");

                var record = new BsonDocument {
                    {"pinid", pin.UserId},
                    {"userid", pin.Token},
                    {"token", pin.Token},
                    {"gender", pin.Gender},
                    {"coord", new BsonArray { pin.Coord.lng, pin.Coord.lat } },
                    {"timestamp", pin.CreatedDate},
                    {"private", pin.IsPrivate}
                };

                await collection.InsertOneAsync(record);
                return pin;
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }
            return null;
        }

        public async Task<IList<Pin>> RetreiveNearByPins(Coord coord) {
            return null;
        }

    }
}