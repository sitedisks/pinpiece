using MongoDB.Driver;
using pinpiece.api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pinpiece.api.Properties;
using MongoDB.Bson;
using NLog;
using MongoDB.Driver.GeoJsonObjectModel;
using pinpiece.api.Models.Dto;

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
                    {"coord", new BsonArray { data.Coord.lng, data.Coord.lat } },
                    {"timestamp", DateTime.UtcNow }
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

        public async Task<dtoPin> InsertPinPostData(Pin pin)
        {
            dtoPin dtoPin = new dtoPin();
            try
            {
                _client = new MongoClient(app.Default.mlabconnection);
                _mongoDb = _client.GetDatabase("heroku_dfqn70ws");
                var collection = _mongoDb.GetCollection<BsonDocument>("pin");

                var record = new BsonDocument {
                    {"pinid", pin.PinId},
                    {"userid", pin.UserId},
                    {"token", pin.Token},
                    {"gender", pin.Gender},
                    {"coord", new BsonArray { pin.Coord.lng, pin.Coord.lat } },
                    {"timestamp", DateTime.UtcNow},
                    {"private", pin.IsPrivate}
                };

                await collection.InsertOneAsync(record);
             
                // dtoPin (mySql) read dtoPin from mySql by PinId
                dtoPin.Id = pin.PinId;
                dtoPin.UserId = pin.UserId;
                dtoPin.Token = pin.Token;
                dtoPin.Latitude = pin.Coord.lat;
                dtoPin.Longitude = pin.Coord.lng;
                dtoPin.ImageUri = "http://www.pinpiece.com/image/blahblahblah";
                dtoPin.Text = "This is testing data, should load from MySql";
                dtoPin.IsPrivate = pin.IsPrivate;
                dtoPin.CreatedDateTime = pin.CreatedDate;
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }
            return dtoPin;
        }

        public async Task<IList<dtoPin>> RetreiveNearByPins(Coord coord)
        {

            IList<dtoPin> nearPins = new List<dtoPin>();

            try
            {
                _client = new MongoClient(app.Default.mlabconnection);
                _mongoDb = _client.GetDatabase("heroku_dfqn70ws");
                var collection = _mongoDb.GetCollection<BsonDocument>("pin");

                var gp = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(coord.lng, coord.lat));
                var filter = Builders<BsonDocument>.Filter.Near("coord", gp, app.Default.howClose);
                var result = await collection.Find(filter).Limit(app.Default.numberOfPins).ToListAsync();

                foreach (var pin in result)
                {
                    dtoPin dtoPin = new dtoPin {
                        Id = (Int64)pin["pinid"],
                        UserId = (Int64)pin["userid"],
                        Token = pin["token"].ToString(),
                        Longitude =  pin["coord"].AsBsonArray[0].ToDouble(),
                        Latitude = pin["coord"].AsBsonArray[1].ToDouble(),
                        ImageUri = "http://www.pinpiece.com/image/blahblahblah",
                        Text = "This is testing data, should load from MySql",
                        IsPrivate = (bool)pin["private"],
                        CreatedDateTime = (DateTime)pin["timestamp"]
                    };

                    nearPins.Add(dtoPin);
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return nearPins;
        }

    }
}