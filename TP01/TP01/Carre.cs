using System;

namespace TP01
{
    public class Carre : Forme
    {
        public int Longueur { get; set; }

        public override double Aire => Longueur*Longueur;

        public override double Perimetre => 4*Longueur;

        public override string ToString()
        {
            return $"Carré de coté {Longueur}" + Environment.NewLine + base.ToString();
        }
    }
}