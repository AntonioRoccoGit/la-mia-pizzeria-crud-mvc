using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
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
    }
}
