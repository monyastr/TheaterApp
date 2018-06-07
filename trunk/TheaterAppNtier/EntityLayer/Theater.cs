using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace EntityLayer
{
    public class Theater
    {
        public int TheaterID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<SelectListItem> Movies { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }


        
        
    }
}