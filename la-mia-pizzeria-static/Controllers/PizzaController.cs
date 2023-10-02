using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            List<PizzaItem> pizzas = new List<PizzaItem>();
            using (PizzaContext db = new PizzaContext())
            {
                pizzas = db.Pizzas.ToList<PizzaItem>();
            }

            return View(pizzas);
        }

        public IActionResult PizzaDetails(int id)
        {
            PizzaItem pizza;
            using(PizzaContext db = new PizzaContext())
            {
                pizza = db.Pizzas.Where(pizza => pizza.PizzaItemId == id).FirstOrDefault();
            }
            if (pizza == null) 
                return View("ProductNotFound");
            
            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View("PizzaCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaItem pizza) {
          
            if(!ModelState.IsValid)
            {
                return View("PizzaCreate",pizza);
            }

            using(PizzaContext db = new PizzaContext())
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
