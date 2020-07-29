using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        
        [NotMapped]
        public int Potentiel
        {
            get 
            {
                int potentiel = this.Force;
                if (this.Arme !=null)
                {
                    potentiel += this.Arme.Degats;
                }
                potentiel *= (this.ArtMartials.Count + 1);
                return potentiel; 
            }
        }


    }
}
