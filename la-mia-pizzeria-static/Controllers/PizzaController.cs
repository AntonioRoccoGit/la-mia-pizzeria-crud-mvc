using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            List<PizzaItem> pizzas;
            using (PizzaContext db = new PizzaContext())
            {
                pizzas = db.Pizzas.ToList<PizzaItem>();
            }

                return View(pizzas);
        }

        public IActionResult UserIndex()
        {

            List<PizzaItem> pizzas;
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
    }
}
