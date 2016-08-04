using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pinpiece.api.Services;
using System.Threading.Tasks;
using pinpiece.api.Models.Dto;
using pinpiece.api.Models;
using NLog;

namespace pinpiece.api.Controllers
{
    [RoutePrefix("mongo")]
    public class MongoController : ApiController
    {
        DBAccessService srv = new DBAccessService();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpPost, Route("reload")]
        public async Task<IHttpActionResult> Retreive([FromBody] Reload reload)
        {
            IList<dtoPin> nearPins = new List<dtoPin>();
            Coord coord = new Coord
            {
                lat = reload.Coord.lat,
                lng = reload.Coord.lng
            };

            try
            {
                await srv.InsertReloadData(reload);
                nearPins = await srv.RetreiveNearByPins(coord);
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return Ok(nearPins);
        }

        [HttpPost, Route("newpin")]
        public async Task<IHttpActionResult> PostPin([FromBody] Pin pin) {
            dtoPin dtoPin = new dtoPin();
            try {

                dtoPin = await srv.InsertPinPostData(pin);
              
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }


            return Ok(dtoPin);
        }

    }
}
