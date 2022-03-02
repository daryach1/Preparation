using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToursWebApi.Entities;

namespace ToursWebApi.Models
{
    public class ResponseHotel
    {
        public ResponseHotel(Hotels hotel)
        {
            Id = hotel.ID;
            Name = hotel.Name;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
    }
}