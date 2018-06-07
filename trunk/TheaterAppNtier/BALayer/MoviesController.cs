using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class MoviesController : Controller
    {
        MoviesProvider provider = new MoviesProvider();
        // GET: Movies
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult GetList()
        {
            var movieList = provider.GetList();
            int totalrows = movieList.Count;
            return Json(new { data = movieList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrows }, JsonRequestBehavior.AllowGet);
        }
        

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            HttpPostedFileBase image = Request.Files["Image"];
            Movie movieImg = new Movie();
            if (image != null)
            {
                var filename = image.FileName;                
                ContentRepository service = new ContentRepository();
                movieImg = service.UploadImageInDataBase(image, movie);
                
            }
            

            if (ModelState.IsValid)
            {
                provider.Create(movieImg);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = provider.Get(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MovieID,MovieName,Logo,Description")] Movie movie)
        {
            if (ModelState.IsValid)
            {

                provider.Edit(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = provider.Get(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            provider.Delete(id);           
            return RedirectToAction("Index");
        }
        public ActionResult RetrieveImage(int id)

        {
            byte[] cover = provider.RetrieveImage(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}
