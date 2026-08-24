using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories
{
    public static class CartRepository
    {
        private static readonly ShoppingCart _cart = new ShoppingCart();

        public static ShoppingCart GetCart()
        {
            return _cart;
        }

        public static void ClearCart()
        {
            _cart.Items.Clear();
        }
    }
}