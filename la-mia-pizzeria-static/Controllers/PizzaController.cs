using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILoggerMs _logger;

        private readonly PizzaContext _db = new();

        public PizzaController(ILoggerMs logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<PizzaItem> pizzas = _db.Pizzas.Include(p => p.Category).ToList<PizzaItem>();
            return View(pizzas);
        }

        public IActionResult PizzaDetails(int id)
        {
            PizzaItem? pizza = _db.Pizzas.Find(id);
            
            if (pizza == null) 
                return View("ProductNotFound");

            _logger.Log($"Visualizzati dettagli pizza id {pizza.PizzaItemId}");
            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            List<Category> categories = _db.Categories.ToList();
            ViewData["categories"] = categories;
            return View("PizzaCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaItem pizza) {
          
            if(!ModelState.IsValid)
            {
                List<Category> categories = _db.Categories.ToList();
                ViewData["categories"] = categories;
                return View("PizzaCreate",pizza);
            }

            
            _db.Pizzas.Add(pizza);
            _db.SaveChanges();

            _logger.Log($"Creata pizza id: {pizza.PizzaItemId}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PizzaItem? pizza = _db.Pizzas.Find(id);
            if (pizza == null)
            {
                _logger.Log($"Tentativovisualizzazione edit pizza id: {id} fallito");
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            }
            List<Category> categories = _db.Categories.ToList();
            
            (PizzaItem item, List<Category> cat) data;
            data.item = pizza;
            data.cat = categories;

            _logger.Log($"Visualizzato edit per pizza id: {pizza.PizzaItemId}");
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit([Bind(Prefix = "Item1")]PizzaItem item, int id)
        {
            PizzaItem? pizzaToEdit = _db.Pizzas.Find(id);
            if (pizzaToEdit == null)
            {
                _logger.Log($"Tentativo modifica pizza id: {id} fallito");
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            }

            EntityEntry<PizzaItem> updatePizza = _db.Entry(pizzaToEdit);
            updatePizza.CurrentValues.SetValues(item);
            _db.SaveChanges();

            _logger.Log($"Modificati dati di pizza id: {pizzaToEdit.PizzaItemId}");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            PizzaItem? pizza = _db.Pizzas.Find(id);

            if (pizza == null)
            {
                _logger.Log($"Tentativo eliminazione pizza id: {id} fallito");
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            }
            _db.Remove(pizza);
            _db.SaveChanges();

            _logger.Log($"Rimossi dati di pizza id: {pizza.PizzaItemId}");
            return RedirectToAction("Index");
        }
    }
}
