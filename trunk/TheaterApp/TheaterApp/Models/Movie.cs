using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheaterApp.Models
{
    public class Movie
    {
        public class Movies { 
}
        public int MovieID { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public byte[] Logo { get; set; }
        [Required]
        public string Description { get; set; }
        public IEnumerable<SelectListItem> Theaters { get; set; }
        public virtual ICollection<Rate> Rates { get; set; } 

    }
}