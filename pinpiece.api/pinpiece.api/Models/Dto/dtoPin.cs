﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinpiece.api.Models.Dto
{
    public class dtoPin
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string Token { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUri { get; set; }
        public string Text { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}