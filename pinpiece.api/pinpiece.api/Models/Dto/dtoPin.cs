using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinpiece.api.Models.Dto
{
    public class dtoPin
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUri { get; set; }
        public string Text { get; set; }
        public bool IsPrivate { get; set; }
        public double? Distance { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}