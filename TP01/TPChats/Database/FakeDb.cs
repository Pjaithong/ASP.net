using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPChats.Models;

namespace TPChats.Database
{
    public class FakeDb
    {
        private static readonly Lazy<FakeDb> lazy =
            new Lazy<FakeDb>(() => new FakeDb());

        /// <summary>
        /// FakeDb singleton access.
        /// </summary>
        public static FakeDb Instance { get { return lazy.Value; } }
        private List<Chat> chats;
        private FakeDb()
        {
            chats = this.GetMeuteDeChats();
            
        }

        
        public List<Chat> Chats { get { return chats; } }
        private List<Chat> GetMeuteDeChats()
        {
            var i = 1;
            return new List<Chat>
            {
                new Chat{Id=i++,Nom = "Felix",Age = 3,Couleur = "Roux"},
                new Chat{Id=i++,Nom = "Minette",Age = 1,Couleur = "Noire"},
                new Chat{Id=i++,Nom = "Miss",Age = 10,Couleur = "Blanche"},
                new Chat{Id=i++,Nom = "Garfield",Age = 6,Couleur = "Gris"},
                new Chat{Id=i++,Nom = "Chatran",Age = 4,Couleur = "Fauve"},
                new Chat{Id=i++,Nom = "Minou",Age = 2,Couleur = "Blanc"},
                new Chat{Id=i,Nom = "Bichette",Age = 12,Couleur = "Rousse"}
            };
        }
    }
}