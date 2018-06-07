using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EntityLayer
{
    public class Rate
    {
        
        public int RateID { get; set; }
        
        public int TheaterID { get; set; }
       
        public int MovieID { get; set; }
        [Required]        
        public string MovieName { get; set; }
        
        public string TheaterName { get; set; }
        [Required]
        [Range(0, 10)]      
        public int MovieRate { get; set;}        
        public virtual Theater Theater { get; set; }        
        public virtual Movie Movie { get; set; }

    }
}