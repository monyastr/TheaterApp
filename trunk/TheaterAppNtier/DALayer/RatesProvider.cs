using System.Linq;
using System.Web.Mvc;
using EntityLayer;

namespace DALayer
{
    public class RatesProvider : Controller
    {
        private Context db = new Context();
        
        public void Create(Rate rate)
        {
           db.Rates.Add(rate);
           db.SaveChanges();
            
        }
               
        public bool Delete(int? id)
        {
            if (id == null)
            {
                return false;
            }
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
            db.SaveChanges();
            return true ;
        }

        public Rate CheckExists (int? TheaterId, string MovieName)
        {
            Rate rate = db.Rates.SingleOrDefault(r => (r.TheaterID == TheaterId) && (r.MovieName == MovieName) );
            return rate;
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
