using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            PizzaFormModel model = new();
            model.Categories = _db.Categories.ToList();
            List<Ingredient> ingredients = _db.Ingredients.ToList();
            model.Ingredients = new();
            foreach(var item in ingredients)
            {
                model.Ingredients.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            return View("PizzaCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel model) {
          
            if(!ModelState.IsValid)
            {
                List<Category> categories = _db.Categories.ToList();
                model.Categories = categories;
                return View("PizzaCreate", model);
            }

            if(model.SelectedIngredients != null)
            {
                model.Pizza.Ingredients = new();
                foreach (var item in model.SelectedIngredients)
                {
                    int ingredientId = int.Parse(item);
                    Ingredient ingredient = _db.Ingredients.Find(ingredientId);
                    if(ingredient != null)
                        model.Pizza.Ingredients.Add(ingredient);
                }
            }

            _db.Pizzas.Add(model.Pizza);
            _db.SaveChanges();

            _logger.Log($"Creata pizza id: {model.Pizza.PizzaItemId}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PizzaFormModel model = new();
            model.Pizza = _db.Pizzas.Include(p => p.Ingredients).FirstOrDefault(pizza => pizza.PizzaItemId == id);
            
            if (model.Pizza == null)
            {
                _logger.Log($"Tentativovisualizzazione edit pizza id: {id} fallito");
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            }
            
            model.Categories = _db.Categories.ToList();
            model.IngredientItem = _db.Ingredients.ToList();
            if(model.Pizza.Ingredients  != null)
                model.IngredientId = model.Pizza.Ingredients.Select(i => i.Id).ToList();
            

            _logger.Log($"Visualizzato edit per pizza id: {model.Pizza.PizzaItemId}");
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PizzaFormModel model, int id)
        {

            PizzaItem? pizzaToEdit = _db.Pizzas.Find(id);
            if (pizzaToEdit == null)
            {
                _logger.Log($"Tentativo modifica pizza id: {id} fallito");
                return NotFound("Spiacenti, l'elemento selezionato non è stato trovato");
            }

            EntityEntry<PizzaItem> updatePizza = _db.Entry(pizzaToEdit);
            updatePizza.CurrentValues.SetValues(model.Pizza);
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
