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
using Test_work.Entity;
using Test_work.Models;

namespace Test_work.Controllers
{
    public class tovarsController : ApiController
    {
        private test_workEntities db = new test_workEntities();

        // GET: api/tovars
        [ResponseType(typeof(List<ResponseTovar>))]
        public IHttpActionResult Gettovar()
        {
            return Ok(db.tovar.ToList().ConvertAll(p => new ResponseTovar(p)));
        }

        // GET: api/tovars/5
        [ResponseType(typeof(tovar))]
        public IHttpActionResult Gettovar(int id)
        {
            //tovar tovar = db.tovar.Find(id);
            //if (tovar == null)
            //{
            //    return NotFound();
            //}
            var id_tovar = db.tovar.ToList().Where(p => p.id_tovar == id).ToList();

            return Ok(id_tovar);
        }

        // PUT: api/tovars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttovar(int id, tovar tovar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tovar.id_tovar)
            {
                return BadRequest();
            }

            db.Entry(tovar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tovarExists(id))
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

        // POST: api/tovars
        [ResponseType(typeof(tovar))]
        public IHttpActionResult Posttovar(tovar tovar)
        {

            if (string.IsNullOrWhiteSpace(tovar.Name_tovar) || tovar.Name_tovar.Length > 100)
                ModelState.AddModelError("Name_tovar", "Превышено допустимое количество символов");
            if (string.IsNullOrWhiteSpace(tovar.Price_tovar) || tovar.Price_tovar.Length > 100)
                ModelState.AddModelError("Name_tovar", "Превышено допустимое количество символов");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tovar.Add(tovar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tovar.id_tovar }, tovar);
        }

        // DELETE: api/tovars/5
        [ResponseType(typeof(tovar))]
        public IHttpActionResult Deletetovar(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid tovar id");

            using (test_workEntities db = new test_workEntities())
            {
                var tovar = db.tovar.Where(s => s.id_tovar == id).FirstOrDefault();

                db.Entry(tovar).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tovarExists(int id)
        {
            return db.tovar.Count(e => e.id_tovar == id) > 0;
        }
    }
}