using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private PizzaContext _db = new PizzaContext();
        public IActionResult Index()
        {
            List<PizzaItem> pizzas = new List<PizzaItem>();
            
                pizzas = _db.Pizzas.ToList<PizzaItem>();

            return View(pizzas);
        }

        public IActionResult PizzaDetails(int id)
        {
            PizzaItem? pizza;
            pizza = _db.Pizzas.Where(pizza => pizza.PizzaItemId == id).FirstOrDefault();
            
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

            
                _db.Pizzas.Add(pizza);
                _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PizzaItem? pizza = _db.Pizzas.Find(id);
            if (pizza == null)
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");

            return View(pizza);
        }

        [HttpPost]
        public IActionResult Edit(PizzaItem item, int id)
        {
            PizzaItem? pizzaToEdit = _db.Pizzas.Find(id);
            if (pizzaToEdit == null)
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");

            EntityEntry<PizzaItem> updatePizza = _db.Entry(pizzaToEdit);
            updatePizza.CurrentValues.SetValues(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            PizzaItem? pizza = _db.Pizzas.Find(id);

            if (pizza == null)
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            _db.Remove(pizza);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
