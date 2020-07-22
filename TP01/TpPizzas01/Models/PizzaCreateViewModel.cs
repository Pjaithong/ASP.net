using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpPizzas01.Models
{
    public class PizzaCreateViewModel
    {
        public Pizza Pizza { get; set; }
        public List<Pate> Pates { get; set; }
        public int IdPate { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<int> IdIngredient { get; set; } = new List<int>();
    }
}