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
            return View(FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id));
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Nom }).ToList();
            vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
             return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid && ValidateVm(vm) )
                {
                    Pizza pizza = vm.Pizza;
                    pizza.Pate = FakeDb.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                    pizza.Ingredients = FakeDb.Instance.IngredientsDisponibles.Where(i => vm.IdIngredient.Contains(i.Id)).ToList();
                    pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(p => p.Id) + 1;
                    FakeDb.Instance.Pizzas.Add(pizza);
                    return RedirectToAction("Index");
                }
                else
                {
                    //retour les choix et la saisie qui ont été fait
                    vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                    vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                    return View(vm);
                }
                
            }
            catch
            {
                //retour les choix et la saisie qui ont été fait
                vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                return View(vm);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }
            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdIngredient = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }
            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid && ValidateVm(vm))
                {
                    Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == vm.Pizza.Id);
                    pizza.Nom = vm.Pizza.Nom;
                    pizza.Pate = FakeDb.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                    pizza.Ingredients = FakeDb.Instance.IngredientsDisponibles.Where(i => vm.IdIngredient.Contains(i.Id)).ToList();

                    return RedirectToAction("Index");
                }
                else
                {
                    //retour les choix et la saisie qui ont été fait
                    vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                    vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                    return View(vm);
                }
            }
            catch
            {
                //retour les choix et la saisie qui ont été fait
                vm.Pates = FakeDb.Instance.PatesDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                vm.Ingredients = FakeDb.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                return View(vm);
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

        private bool ValidateVm(PizzaCreateViewModel vm)
        {
            bool isValidate = true;
            //Une pizza doit avoir entre 2 et 5 ingrédients
            if (vm.IdIngredient.Count < 2 || vm.IdIngredient.Count > 5)
            {
                ModelState.AddModelError("IdIngredient", "Une pizza doit avoir entre 2 et 5 ingrédients");
                isValidate = false;
            }
            //Le nom d'une pizza est unique
            
            if (FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Nom == vm.Pizza.Nom) != null
                && FakeDb.Instance.Pizzas.FirstOrDefault(p => p.Id == vm.Pizza.Id) == null )
            {
                ModelState.AddModelError("Pizza.Nom.AlreadyExists", "Il existe déjà une pizza portant ce nom");
                isValidate = false;
            }
            //2 pizzas ou plus ne peuvent pas avoir la meme liste d'ingredients
            foreach (var p in FakeDb.Instance.Pizzas)
            {
                if (vm.IdIngredient.Count == p.Ingredients.Count)
                {
                    bool isDifferent = false;
                    List<Ingredient> ingDb = p.Ingredients.OrderBy(i => i.Id).ToList();
                    vm.IdIngredient = vm.IdIngredient.OrderBy(i => i).ToList();
                    for (int i = 0; i < vm.IdIngredient.Count; i++)
                    {
                        if (vm.IdIngredient.ElementAt(i) != ingDb.ElementAt(i).Id)
                        {
                            isDifferent = true;
                            break;
                        }

                    }
                    if (isDifferent == false)
                    {
                        ModelState.AddModelError("Ingredient.AlreadyExists", "Il existe déjà une pizza avec ces ingrédients");
                        isValidate = false;
                    }
                }
            }
            return isValidate;
        }
    }
}
