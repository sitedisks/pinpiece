namespace pinpiece.domain.Models
{
    using System;

    public class Reload
    {
        public Int64 UserId { get; set; }
        public string Token { get; set; }
        public Coord Coord { get; set; }
    }
}