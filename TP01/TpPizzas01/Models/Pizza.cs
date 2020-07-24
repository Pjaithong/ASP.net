using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TpPizzas01.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength =5)]
        public string Nom { get; set; }

        public Pate Pate { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }
}