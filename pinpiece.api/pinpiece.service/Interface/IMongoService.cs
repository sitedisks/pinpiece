namespace pinpiece.service.Interface
{
    using pinpiece.domain.Models;
    using pinpiece.domain.Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMongoService
    {
        Task<IList<Restaurant>> MLabConnection();
        Task<IList<dtoPin>> RetreiveAllPins();
        Task<int> InsertReloadData(Reload data);
        Task<dtoPin> InsertPinPostData(Pin pin);
        Task<IList<dtoPin>> RetreiveNearByPins(Coord coord);
        Task<IList<dtoPin>> RetreiveNearByWithDistancePins(Coord coord);
    }
}
