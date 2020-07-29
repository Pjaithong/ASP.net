using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDojoBO
{
    public class ArtMartial : DbItem
    {
        private long id;

        public String Nom { get; set; }
        public long Id { get => this.id; set => this.id=value; }
    }
}
