using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class OrderViewModel
    {
        public int UserID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public int TotalPrice { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }

        public OrderViewModel()
        {
            CartItems = new List<CartItemViewModel>();
        }
    }
}
