using Backend.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        public List<CartItemEntity> CartItems { get; set; }
    }
}
