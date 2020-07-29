using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDojoBO
{
    public class Samourai : DbItem
    {
        private long id;

        public int Force { get; set; }
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; }
        public long Id { get => this.id; set => this.id = value; }

        public List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();

    }
}
