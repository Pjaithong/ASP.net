using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDojoBO;

namespace TPDojo.Models
{
    public class SamouraiViewModel
    {
        public Samourai Samourai { get; set; }
        public List<SelectListItem> Armes { get; set; } = new List<SelectListItem>();
        public long? IdArme { get; set; }

        [System.ComponentModel.DisplayName("Arts Martiaux Maitrisés")]
        public List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public List<long> IdsArtMartials { get; set; } = new List<long>();


    }
}