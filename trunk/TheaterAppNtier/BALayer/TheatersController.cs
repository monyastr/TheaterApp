using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityLayer;
using DALayer;
using System.Linq;
using System.Collections;

namespace BALayer
{
    public class TheatersController : Controller
    {
       
        TheatersProvider provider = new TheatersProvider();
        // GET: Theaters
        public async Task<ActionResult> Index()
        {
            return View( );
        }
        [HttpPost]
        public ActionResult GetList()
        {
            try
            {
                var theaterList = provider.GetList();
                int totalrows = theaterList.Count;
                return Json(new { data = theaterList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }


        }

        // GET: Theaters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Theater theater = provider.Get(id) ;
            if (theater == null)
            {
                return HttpNotFound();
            }
            return View(theater);
        }

        // GET: Theaters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Theaters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public async Task<ActionResult> Create([Bind(Include = "TheaterID,Name,Address")] Theater theater)
        {
            if (ModelState.IsValid)
            {
                provider.Create(theater);
                return RedirectToAction("Index");
            }

            return View(theater);
        }

        // GET: Theaters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theater theater = provider.Get(id);
            if (theater == null)
            {
                return HttpNotFound();
            }
            return View(theater);
        }

        // POST: Theaters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public async Task<ActionResult> Edit([Bind(Include = "TheaterID,Name,Address")] Theater theater)
        {
            if (ModelState.IsValid)
            {
                provider.Edit(theater);
                return RedirectToAction("Index");
            }
            return View(theater);
        }

        // GET: Theaters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theater theater = provider.Get(id);
            if (theater == null)
            {
                return HttpNotFound();
            }
            return View(theater);
        }

        // POST: Theaters/Delete/5
        [HttpPost, ActionName("Delete")]        
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            provider.Delete(id);
            return RedirectToAction("Index");
        }
       
        // GET: AddMovie/AddMovie/5
        public async Task<ActionResult> AddMovie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theater theater = provider.Get(id);
            if (theater == null)
            {
                return HttpNotFound();
            }
            var movieList = provider.GetMovieList(theater);

            AddMovie addMovie = new AddMovie
            {
                Theater = theater
            };
            ViewData["ListItem"] = movieList;            
            return View(addMovie);
        }

        // POST: AddMovie/AddMovie/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AddMovie( AddMovie movie)
        {
            string movieName = movie.Rate.MovieName;
            Movie movieId = provider.GetMovieId(movie);
            if (movieId == null)
            {
                return HttpNotFound();
            }
            
            Rate rate = new Rate
            {
                TheaterID = movie.Theater.TheaterID,
                TheaterName = movie.Theater.Name,
                MovieName = movieName,
                MovieRate = movie.Rate.MovieRate,
                MovieID = movieId.MovieID
            };
            if (ModelState.IsValid)
            {
                provider.AddMovie(rate);
                return RedirectToAction("Index");
            }

            return View();
        }
        
    }
}
