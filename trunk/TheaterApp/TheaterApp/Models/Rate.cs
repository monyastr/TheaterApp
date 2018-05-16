using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace TheaterApp.Models
{
    public class Rate
    {
        
        public int RateID { get; set; }
        
        public int TheaterID { get; set; }
       
        public int MovieID { get; set; }
        [Required]
        [Remote("CheckExists", "Rates", ErrorMessage = "Movie is already added.")]
        public string MovieName { get; set; }
        
        public string TheaterName { get; set; }
        [Required]
        [Range(0, 10)]      
        public int MovieRate { get; set;}
        [JsonIgnore]
        public virtual Theater Theater { get; set; }
        [JsonIgnore]
        public virtual Movie Movie { get; set; }

    }
}