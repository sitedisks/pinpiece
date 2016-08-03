
namespace pinpiece.api.Models
{
    public class Restaurant
    {
        public string borough { get; set; }
        public string cuisine { get; set; }
        public string name { get; set; }
        public Address address { get; set; }
    }

    public class Address
    {
        public string building { get; set; }
        public string street { get; set; }
        public string zipcode { get; set; }
        public Coord coord { get; set; }
    }


}