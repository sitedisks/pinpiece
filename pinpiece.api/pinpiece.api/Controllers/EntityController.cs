namespace pinpiece.api.Controllers
{
    using pinpiece.data.DbContext;
    using pinpiece.data.Interface;
    using pinpiece.domain.Entity;
    using pinpiece.service.Interface;
    using pinpiece.service.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("pin")]
    public class EntityController : ApiController
    {
        private readonly IEntityService _svc;

        public EntityController(IEntityService svc)
        {
            _svc = svc;
        }

        [HttpGet, Route("all")]
        public async Task<IHttpActionResult> RetreiveAll()
        {
            IList<tbPin> list = new List<tbPin>();

            try
            {
                list = await _svc.GetAllPins();
            }
            catch (Exception ex)
            {

            }

            return Ok(list);
        }
    }
}
