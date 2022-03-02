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

namespace ToursWebApi.Controllers
{
    public class HotelsCommentsController : ApiController
    {
        private ToursBasesEntities db = new ToursBasesEntities();

        // GET: api/HotelsComments
        public IQueryable<HotelsComment> GetHotelsComment()
        {
            return db.HotelsComment;
        }

        [Route("api/getHotelComments")]
        public IHttpActionResult GetHotelComments(int HotelId)
        {
            var hotelComments = db.HotelsComment.ToList().Where(p => p.HotelsId == HotelId).ToList();
            return Ok(hotelComments);

        }

        // GET: api/HotelsComments/5
        [ResponseType(typeof(HotelsComment))]
        public IHttpActionResult GetHotelsComment(int id)
        {
            HotelsComment hotelsComment = db.HotelsComment.Find(id);
            if (hotelsComment == null)
            {
                return NotFound();
            }

            return Ok(hotelsComment);
        }

        // PUT: api/HotelsComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotelsComment(int id, HotelsComment hotelsComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelsComment.Id)
            {
                return BadRequest();
            }

            db.Entry(hotelsComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelsCommentExists(id))
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

        // POST: api/HotelsComments
        [ResponseType(typeof(HotelsComment))]
        public IHttpActionResult PostHotelsComment(HotelsComment hotelsComment)
        {
            hotelsComment.CreationDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(hotelsComment.Author) || hotelsComment.Author.Length > 50)
                ModelState.AddModelError("Author", "Author is required string up to 100 symbols.");
            if (string.IsNullOrWhiteSpace(hotelsComment.Text))
                ModelState.AddModelError("Text", "Text is required string.");
            if (!(db.Hotels.ToList().FirstOrDefault(p => p.ID == hotelsComment.HotelsId) is Hotels))
                ModelState.AddModelError("HotelsID", "HotelId is hotel's id from database.");
                
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HotelsComment.Add(hotelsComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotelsComment.Id }, hotelsComment);
        }

        // DELETE: api/HotelsComments/5
        [ResponseType(typeof(HotelsComment))]
        public IHttpActionResult DeleteHotelsComment(int id)
        {
            HotelsComment hotelsComment = db.HotelsComment.Find(id);
            if (hotelsComment == null)
            {
                return NotFound();
            }

            db.HotelsComment.Remove(hotelsComment);
            db.SaveChanges();

            return Ok(hotelsComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelsCommentExists(int id)
        {
            return db.HotelsComment.Count(e => e.Id == id) > 0;
        }
    }
}