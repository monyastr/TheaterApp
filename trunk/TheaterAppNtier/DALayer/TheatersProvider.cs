using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EntityLayer;
using System.Collections;

namespace DALayer
{
    public class TheatersProvider : Controller
    {
        private Context db = new Context();

        
        public IList GetList()
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

                return theaterList.ToList();
            }
            catch (Exception)
            {
                throw;
            }


        }
       
        public bool  Create(Theater theater)
        {
            if (ModelState.IsValid)
            {
                db.Theaters.Add(theater);
                db.SaveChangesAsync();
                return true;              
            }
            return false;
        }

        public Theater Get(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Theater theater = db.Theaters.Find(id);            
            return theater;
        }

        
        public bool Edit(Theater theater)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theater).State = EntityState.Modified;
                db.SaveChangesAsync();
                return true;
            }
            return false;

        }

       
                
        public bool Delete(int? id)
        {
            if (id == null)
            {
                return false;
            }
            Theater theater = db.Theaters.Find(id);
            if (theater == null)
            {
                return false;
            }
            return true;

        }       
        
        public List<SelectListItem> GetMovieList(Theater theater)
        {
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
            return movieList;
        }
        
        public Movie GetMovieId(AddMovie movie)
        {
            string movieName = movie.Rate.MovieName;
            Movie movieId = db.Movies.SingleOrDefault(m => m.MovieName == movieName);
            return movieId;

        }

        public void AddMovie( Rate rate)
        {
            db.Rates.Add(rate);
            db.SaveChanges();           
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
