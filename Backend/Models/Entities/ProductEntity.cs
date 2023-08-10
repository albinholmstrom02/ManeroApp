using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
        public ProductType ProductType { get; set; }
    }

    public enum ProductType
    {
        Shirts, Pants, Accessories, Shoes
    }
}
