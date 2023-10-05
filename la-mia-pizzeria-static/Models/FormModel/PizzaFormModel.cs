using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models.FormModel
{
    public class PizzaFormModel
    {
        public PizzaItem Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        
        public List<SelectListItem>? Ingredients { get; set; }
        public List<Ingredient>? IngredientItem { get; set; }
        public List<int>? IngredientId { get; set; }

        public List<string>? SelectedIngredients { get; set; }
    }
}
