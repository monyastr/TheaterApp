using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using DALayer;

namespace BALayer
{
    public class HomeController : Controller
    {
        
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList()
        {
            HomeProvider provider = new HomeProvider();
            var movieList = provider.GetList();
            int totalrows = movieList.Count;            
            return Json(new { data = movieList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrows }, JsonRequestBehavior.AllowGet);
            

        }
    }
}