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
using FinesWebApi.Entities;
using FinesWebApi.Models;


namespace FinesWebApi.Controllers
{
    public class FinesController : ApiController
    {
        private FinesEntities db = new FinesEntities();

        // GET: api/Fines
        [ResponseType(typeof(List<ResponseFine>))]
        public IHttpActionResult GetFine()
        {
            return Ok(db.Fine.ToList().ConvertAll(p => new ResponseFine(p)));
        }

        [Route("api/getFine")]
        public IHttpActionResult GetFineDate(DateTime modified)
        {
            var fineDate = db.Fine.ToList().Where(p => p.create_date == modified).ToList();
            return Ok(fineDate);
        }

        // GET: api/Fines/5
        [ResponseType(typeof(Fine))]
        public IHttpActionResult GetFine(int id)
        {
            Fine fine = db.Fine.Find(id);
            if (fine == null)
            {
                return NotFound();
            }

            return Ok(fine);
        }

        // PUT: api/Fines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFine(int id, Fine fine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fine.Id)
            {
                return BadRequest();
            }

            db.Entry(fine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(id))
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

        // POST: api/Fines
        [ResponseType(typeof(Fine))]
        public IHttpActionResult PostFine(Fine fine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fine.Add(fine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fine.Id }, fine);
        }

        // DELETE: api/Fines/5
        [ResponseType(typeof(Fine))]
        public IHttpActionResult DeleteFine(int id)
        {
            Fine fine = db.Fine.Find(id);
            if (fine == null)
            {
                return NotFound();
            }

            db.Fine.Remove(fine);
            db.SaveChanges();

            return Ok(fine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FineExists(int id)
        {
            return db.Fine.Count(e => e.Id == id) > 0;
        }
    }
}