namespace pinpiece.api.Controllers
{
    using NLog;
    using pinpiece.domain.Models;
    using pinpiece.domain.Dto;
    using pinpiece.service.Interface;
    using pinpiece.service.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
 

    [RoutePrefix("pins")]
    public class MongoController : ApiController
    {
        private readonly IMongoService _svc;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MongoController(IMongoService svc) {
            _svc = svc;
        }

        [HttpPost, Route("reload")]
        public async Task<IHttpActionResult> Reload([FromBody] Reload reload)
        {
            IList<dtoPin> nearPins = new List<dtoPin>();
            Coord coord = new Coord
            {
                lat = reload.Coord.lat,
                lng = reload.Coord.lng
            };

            try
            {
                await _svc.InsertReloadData(reload);
                nearPins = await _svc.RetreiveNearByWithDistancePins(coord);
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

                dtoPin = await _svc.InsertPinPostData(pin);
              
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }


            return Ok(dtoPin);
        }

        [HttpGet, Route("all")]
        public async Task<IHttpActionResult> RetreiveAll() {
            IList<dtoPin> allPins = new List<dtoPin>();

            try {
                allPins = await _svc.RetreiveAllPins();
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return Ok(allPins);

        }

    }
}
