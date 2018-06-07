using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EntityLayer;
using System.Web;
using System.Collections;

namespace DALayer
{
    public class MoviesProvider : Controller
    {
        private Context db = new Context();

        
        public IList GetList()
        {
            try
            {               
                var movieList = db.Movies.Include(m => m.Rates).
                    Select(l => new
                    {
                        l.MovieName,                       
                        l.Description,
                        l.Rates,
                        l.MovieID
                    }).ToList();                             

                return movieList;
            }
            catch (Exception)
            {
                throw;
            }
           
            
        }  
        
        public void Create( Movie movie)        {
            db.Movies.Add(movie);
            db.SaveChangesAsync();
        }

        public Movie Get(int? id)
        {
            Movie movie = db.Movies.Find(id);            
            return movie;
        }

        public void Edit(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();           
        }
        
        
        public void Delete(int? id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();            
        }
        public byte[] RetrieveImage(int id)

        {
            byte[] cover = db.Movies.FirstOrDefault(i => i.MovieID == id).Logo; 
            return cover;           
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
