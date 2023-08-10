using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Entities
{
    public class CartItemEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
