using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinpiece.api.Models
{
    public class Reload
    {
        public Int64 UserId { get; set; }
        public string Token { get; set; }
        public Coord Coord { get; set; }
    }
}