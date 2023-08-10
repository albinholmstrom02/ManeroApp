using Frontend.Interfaces;
using Frontend.ViewModels;
using System.Collections.Generic;
using System.Linq;

public class CartService : ICartService
{
    private readonly List<CartItemViewModel> _cartItems = new List<CartItemViewModel>();

    public void AddToCart(ProductsViewModel product, int quantity)
    {
        var existingCartItem = _cartItems.FirstOrDefault(item => item.ProductId == product.Id);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += quantity;
        }
        else
        {
            _cartItems.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                ProductTitle = product.Title,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = quantity
            });
        }
    }

    public void ClearCart()
    {
        _cartItems.Clear();
    }

    public List<CartItemViewModel> GetCartItems()
    {
        return _cartItems;
    }
}
