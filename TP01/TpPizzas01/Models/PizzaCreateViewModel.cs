using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TpPizzas01.Models
{
    public class PizzaCreateViewModel
    {
        public Pizza Pizza { get; set; }
        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

        [Required]
        public int? IdPate { get; set; }

        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public List<int> IdIngredient { get; set; } = new List<int>();
    }
}