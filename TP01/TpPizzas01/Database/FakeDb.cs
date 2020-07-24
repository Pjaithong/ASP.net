using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpPizzas01.Models;

namespace TpPizzas01.Database
{
    public class FakeDb
    {
        private static FakeDb _instance;
        static readonly object instanceLock = new object();

        private FakeDb()
        {
            this.IngredientsDisponibles = this.InitIngredientsDisponibles;
            this.PatesDisponibles = this.InitPatesDisponibles;
            this.Pizzas = new List<Pizza>();
        }

        public static FakeDb Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDb();
                    }
                }
                return _instance;
            }
        }

        private List<Ingredient> ingredients;
        public List<Ingredient> IngredientsDisponibles
        {
            get { return ingredients; }
            private set { this.ingredients = value; }
        }

        private List<Pate> pates;
        public List<Pate> PatesDisponibles 
        {
            get { return pates; }
            private set { this.pates = value; }
        }

        private List<Pizza> pizzas;

        public List<Pizza> Pizzas 
        { 
            get { return pizzas; }
            private set { pizzas = value; }
        }


        private List<Ingredient> InitIngredientsDisponibles { get; } = new List<Ingredient>
        {
            new Ingredient{Id=1,Nom="Mozzarella"},
            new Ingredient{Id=2,Nom="Jambon"},
            new Ingredient{Id=3,Nom="Tomate"},
            new Ingredient{Id=4,Nom="Oignon"},
            new Ingredient{Id=5,Nom="Cheddar"},
            new Ingredient{Id=6,Nom="Saumon"},
            new Ingredient{Id=7,Nom="Champignon"},
            new Ingredient{Id=8,Nom="Poulet"}
        };

        private List<Pate> InitPatesDisponibles { get; } = new List<Pate>
        {
            new Pate{ Id=1,Nom="Pate fine, base crême"},
            new Pate{ Id=2,Nom="Pate fine, base tomate"},
            new Pate{ Id=3,Nom="Pate épaisse, base crême"},
            new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
        };
    }
}