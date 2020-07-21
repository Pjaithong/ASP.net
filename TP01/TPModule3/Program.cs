﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPModule3.BO;

namespace TPModule3
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiserDatas();
            //Afficher la liste des prénoms des auteurs dont le nom commence par "G" 
            var prenomAvecNomParG = ListeAuteurs.Where(a => a.Nom.StartsWith("G")).Select(a => a.Prenom);
            Console.WriteLine("Afficher la liste des prénoms des auteurs dont le nom commence par G");
            foreach(var prenom in prenomAvecNomParG)
            {
                Console.WriteLine(prenom);
            }

            //Afficher l’auteur ayant écrit le plus de livres 
            var auteurPlusDeLivres = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(c => c.Count()).FirstOrDefault().Key;
            Console.WriteLine();
            Console.WriteLine("Afficher l’auteur ayant écrit le plus de livres :");
            Console.WriteLine($"{auteurPlusDeLivres.Prenom} {auteurPlusDeLivres.Nom}");

            //Afficher le nombre moyen de pages par livre par auteur 
            var nbPagesParLivreParAuteur = ListeLivres.GroupBy(l => l.Auteur);
            Console.WriteLine();
            Console.WriteLine("Afficher le nombre moyen de pages par livre par auteur :");
            foreach (var nb in nbPagesParLivreParAuteur)
            {
                Console.WriteLine($"{nb.Key.Prenom} {nb.Key.Prenom} Moyennes des pages= {nb.Average(l=>l.NbPages)}");
            }

            //Afficher le titre du livre avec le plus de pages
            var titreLivreMaxPage = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine();
            Console.WriteLine("Afficher le titre du livre avec le plus de pages :");
            Console.WriteLine(titreLivreMaxPage.Titre);

            //Afficher combien ont gagné les auteurs en moyenne (moyenne des factures)
            var moyenGagne = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine();
            Console.WriteLine("Afficher combien ont gagné les auteurs en moyenne (moyenne des factures) :");
            Console.WriteLine(moyenGagne);

            //Afficher les auteurs et la liste de leurs livres 
            var livresParAuteur = ListeLivres.GroupBy(l => l.Auteur);
            Console.WriteLine();
            Console.WriteLine("Afficher les auteurs et la liste de leurs livres  :");
            foreach(var livres in livresParAuteur)
            {
                Console.WriteLine($"Auteur : {livres.Key.Prenom} {livres.Key.Nom}");
                foreach(var livre in livres)
                {
                    Console.WriteLine($"livres : {livre.Titre}");
                }
            }

            //Afficher les titres de tous les livres triés par ordre alphabétique 
            var livresParAlphabétique = ListeLivres.Select(l => l.Titre).OrderBy(t => t);
            Console.WriteLine();
            Console.WriteLine("Afficher les titres de tous les livres triés par ordre alphabétique :");
            foreach (var livre in livresParAlphabétique)
            {
                Console.WriteLine(livre);
            }

            //Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            Console.WriteLine();
            
            var moyennePages = ListeLivres.Average(l => l.NbPages);
            Console.WriteLine($"Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne ({moyennePages} pages) : ");
            var livresNbPageSupMoyenne = ListeLivres.Where(l => l.NbPages>moyennePages);
            foreach (var livre in livresNbPageSupMoyenne)
            {
                Console.WriteLine($"{livre.Titre} : {livre.NbPages} pages");
            }

            //Afficher l'auteur ayant écrit le moins de livres
            var auteurMoinsLivres = ListeAuteurs.OrderBy(a => ListeLivres.Count(l => l.Auteur == a)).First();
            Console.WriteLine();
            Console.WriteLine($"Afficher l'auteur ayant écrit le moins de livres : {auteurMoinsLivres.Prenom} {auteurMoinsLivres.Nom} : ");

            Console.ReadKey();
        }

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

    }
}
