using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterApp.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace TheaterApp.Controllers
{
    public class RatesController : Controller
    {
        private Context db = new Context();

        // GET: Rates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rates/Create
        [HttpPost]        
        public async Task<ActionResult> Create([Bind(Include = "RateID,TheaterID,MovieID,MovieName,TheaterName,MovieRate")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Rates.Add(rate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rate);
        }


        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]        
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rate rate = await db.Rates.FindAsync(id);
            db.Rates.Remove(rate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CheckExists (int? TheaterId, string MovieName)
        {
            Rate rate = db.Rates.SingleOrDefault(r => (r.TheaterID == TheaterId) && (r.MovieName == MovieName) );            
            return Json(rate.MovieRate, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
