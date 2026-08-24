namespace ConsoleApp1.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal GrandTotal
        {
            get
            {
                return Items.Sum(item => item.LineTotal);
            }
        }
    }
}