using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpPizzas01.Database;
using TpPizzas01.Models;

namespace TpPizzas01.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDb.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            /*Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza!=null)
            {
                return View(pizza);
            }
            else
            {
                return RedirectToAction("Index");
            }*/
            return View(FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id));
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Ingredients = FakeDb.Instance.IngredientsDisponibles;
            vm.Pates = FakeDb.Instance.PatesDisponibles;
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateViewModel vm)
        {
            try
            {
                Pizza pizza = vm.Pizza;
                // TODO: Add insert logic here
                pizza.Pate = FakeDb.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                pizza.Ingredients = FakeDb.Instance.IngredientsDisponibles.Where(i => vm.IdIngredient.Contains(i.Id)).ToList();
                pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(p => p.Id) + 1;
                FakeDb.Instance.Pizzas.Add(pizza);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(FakeDb.Instance.Pizzas);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Ingredients = FakeDb.Instance.IngredientsDisponibles;
            vm.Pates = FakeDb.Instance.PatesDisponibles;
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
            if (vm!=null)
            {
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateViewModel vm)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == vm.Pizza.Id);
                pizza.Nom = vm.Pizza.Nom;
                pizza.Pate = FakeDb.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                pizza.Ingredients = FakeDb.Instance.IngredientsDisponibles.Where(i => vm.IdIngredient.Contains(i.Id)).ToList();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FakeDb.Instance.Pizzas.FirstOrDefault(p=>p.Id==id));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
                FakeDb.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
