using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPChats.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Age { get; set; }
        public string Couleur { get; set; }
    }
}