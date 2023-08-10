namespace Frontend.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
