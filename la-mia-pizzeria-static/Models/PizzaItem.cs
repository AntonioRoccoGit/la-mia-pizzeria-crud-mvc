using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaItem
    {
        [Column("id")]
        public int PizzaItemId { get; set; }

        [Column("name"), MaxLength(50)]
        public string Name { get; set; }

        [Column("description"), MaxLength(255)]
        public string Description { get; set; }

        [Column("thumbnail"), MaxLength(255), DefaultValue("ciaocome srtai")]
        public string Thumbnail { get; set; }

        [Column("price")]
        public double Price { get; set; }

        public PizzaItem() { }
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
