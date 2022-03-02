using FinesWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinesWebApi.Models
{
    public class ResponseFine
    {

        public ResponseFine(Fine fine)
        {
            Id = fine.Id;
            CarNum = fine.car_num;
            Region = fine.region;
            LicenceNum = fine.licence_num;
            CreateDate = (DateTime) fine.create_date;
            Photo = fine.photo;
        }

        public int Id { get; set; }
        public string CarNum { get; set; }
        public string Region { get; set; }
        public string LicenceNum { get; set; }
        public DateTime CreateDate { get; set; }
        public string Photo { get; set; }

    }
}