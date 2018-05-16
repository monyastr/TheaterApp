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
    public class HomeController : Controller
    {
        private Context db = new Context();
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList()
        {
            try
            {
                var movieList = db.Movies.Include(m => m.Rates).
                    Select(l => new
                    {
                        l.MovieName,
                        l.MovieID,                        
                        l.Rates
                    });

                int totalrows = movieList.ToList().Count;

                return Json(new { data = movieList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}