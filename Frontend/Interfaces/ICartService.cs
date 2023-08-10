using Frontend.ViewModels;

namespace Frontend.Interfaces
{
    public interface ICartService
    {
        void AddToCart(ProductsViewModel product, int quantity);
        void ClearCart();
        List<CartItemViewModel> GetCartItems();
    }

}
