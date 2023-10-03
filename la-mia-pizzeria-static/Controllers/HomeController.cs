using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {

        private ILoggerMs _logger;
        public HomeController(ILoggerMs log)
        {
            this._logger = log;
        }
        public IActionResult Index()
        {
            List<PizzaItem> pizzas = new List<PizzaItem>();
            using (PizzaContext db = new PizzaContext())
            {
                pizzas = db.Pizzas.ToList<PizzaItem>();
            }
            this._logger.Log("Ciao sono index");
            return View(pizzas);
        }
    }
}
