using System;
using System.Linq;
using System.Data.Entity;
using EntityLayer;
using System.Collections;

namespace DALayer
{
    public class HomeProvider
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
                        l.MovieID,                        
                        l.Rates
                    }).ToList();               

                return movieList;
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}