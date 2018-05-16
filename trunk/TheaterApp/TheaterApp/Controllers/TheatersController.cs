using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheaterApp.Models;

namespace TheaterApp.Controllers
{
    public class TheatersController : Controller
    {
        private Context db = new Context();

        // GET: Theaters
        public async Task<ActionResult> Index()
        {
            return View(await db.Theaters.ToListAsync());
        }
        [HttpPost]
        public ActionResult GetList()
        {
            try
            {
                var theaterList = db.Theaters.Include(m => m.Rates).
                    Select(l => new
                    {
                        l.Name,
                        l.Address,                        
                        l.Rates,
                        l.TheaterID
                    });

                int totalrows = theaterList.ToList().Count;

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
            Theater theater = await db.Theaters.FindAsync(id);
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
                db.Theaters.Add(theater);
                await db.SaveChangesAsync();
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
            Theater theater = await db.Theaters.FindAsync(id);
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
                db.Entry(theater).State = EntityState.Modified;
                await db.SaveChangesAsync();
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
            Theater theater = await db.Theaters.FindAsync(id);
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
            Theater theater = await db.Theaters.FindAsync(id);
            db.Theaters.Remove(theater);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        // GET: AddMovie/AddMovie/5
        public async Task<ActionResult> AddMovie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theater theater = await db.Theaters.FindAsync(id);
            if (theater == null)
            {
                return HttpNotFound();
            }
            var movies = db.Movies
                .Include(r => r.Rates)
                .Where(m => m.MovieName != m.Rates.FirstOrDefault(r => r.TheaterName == theater.Name && r.MovieName == m.MovieName).MovieName)
                .Select(m => new
            {
                name = m.MovieName
            }).ToList();

            List<SelectListItem> movieList = new List<SelectListItem>();

            foreach (var movie in movies)
            {

                movieList.Add(new SelectListItem
                {
                    Value = movie.name,
                    Text = movie.name
                });
            }
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
        public async Task<ActionResult> AddMovie( AddMovie movie)
        {
            string movieName = movie.Rate.MovieName;
            Movie movieId = await db.Movies.SingleOrDefaultAsync(m => m.MovieName == movieName);
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
                     db.Rates.Add(rate);
                     await db.SaveChangesAsync();
                     return RedirectToAction("Index");
            }

            return View();
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
