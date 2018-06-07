using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer;
using System.Data.Entity;
using System.Threading.Tasks;
using DALayer;

namespace BALayer
{
    public class RatesController : Controller
    {
        RatesProvider provider = new RatesProvider();
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
                provider.Create(rate);
                return RedirectToAction("Index");
            }

            return View(rate);
        }


        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]        
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            provider.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CheckExists (int? TheaterId, string MovieName)
        {
            Rate rate = provider.CheckExists(TheaterId, MovieName);            
            return Json(rate.MovieRate, JsonRequestBehavior.AllowGet);
        }

       
    }
}
