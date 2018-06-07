using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EntityLayer
{
    public class Movie
    {
        public class Movies { 
}
        public int MovieID { get; set; }
        [Required]
        public string MovieName { get; set; }
        //[Required]
        public byte[] Logo { get; set; }
        [Required]
        public string Description { get; set; }
        public IEnumerable<SelectListItem> Theaters { get; set; }
        public virtual ICollection<Rate> Rates { get; set; } 

    }
}