﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDojoBO
{
    public class Arme : DbItem
    {
        private long id;

        public string Nom { get; set; }
        public int Degats { get; set; }
        public long Id { get => this.id; set => this.id = value; }
    }
}
