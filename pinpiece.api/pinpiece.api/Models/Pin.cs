using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinpiece.api.Models
{
    public class Pin
    {
        public Int64 PinId { get; set; }
        public Int64 UserId { get; set; }
        public string Token { get; set; }
        public string Gender { get; set; }
        public Coord Coord { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}