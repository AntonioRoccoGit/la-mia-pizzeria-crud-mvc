using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaItem
    {
        [Column("id")]
        public int PizzaItemId { get; set; }

        [Column("name"), MaxLength(50)]
        public string Name { get; private set; }

        [Column("description"), MaxLength(255)]
        public string Description { get; private set; }

        [Column("thumbnail"), MaxLength(255)]
        public string Thumbnail { get; private set; }

        [Column("price")]
        public double Price { get; private set; }

        public PizzaItem(string name, string description, string thumbnail, double price)
        {
            Name = name;
            Description = description;
            Thumbnail = thumbnail;
            Price = price;
        }


        public string GetStringPrice()
        {
            return Price.ToString("0.00");
        }
    }
}
