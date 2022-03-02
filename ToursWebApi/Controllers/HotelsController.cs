using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ToursWebApi.Entities;
using ToursWebApi.Models;

namespace ToursWebApi.Controllers
{
    public class HotelsController : ApiController
    {
        private ToursBasesEntities db = new ToursBasesEntities();

        // GET: api/Hotels
        [ResponseType(typeof(List<ResponseHotel>))]
        public IHttpActionResult GetHotels()
        {
            return Ok(db.Hotels.ToList().ConvertAll(p => new ResponseHotel(p)));
        }

        // GET: api/Hotels/5
        [ResponseType(typeof(Hotels))]
        public IHttpActionResult GetHotels(int id)
        {
            Hotels hotels = db.Hotels.Find(id);
            if (hotels == null)
            {
                return NotFound();
            }

            return Ok(hotels);
        }

        // PUT: api/Hotels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotels(int id, Hotels hotels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotels.ID)
            {
                return BadRequest();
            }

            db.Entry(hotels).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Hotels
        [ResponseType(typeof(Hotels))]
        public IHttpActionResult PostHotels(Hotels hotels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotels);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotels.ID }, hotels);
        }

        // DELETE: api/Hotels/5
        [ResponseType(typeof(Hotels))]
        public IHttpActionResult DeleteHotels(int id)
        {
            Hotels hotels = db.Hotels.Find(id);
            if (hotels == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotels);
            db.SaveChanges();

            return Ok(hotels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelsExists(int id)
        {
            return db.Hotels.Count(e => e.ID == id) > 0;
        }
    }
}