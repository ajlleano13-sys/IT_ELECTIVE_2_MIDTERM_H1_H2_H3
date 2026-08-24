using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories
{
    public static class ProductRepository
    {
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                Id = 1,
                Name = "Premium Dog Food",
                Price = 550.00m,
                StockQuantity = 10
            },

            new Product
            {
                Id = 2,
                Name = "Premium Cat Food",
                Price = 480.00m,
                StockQuantity = 8
            },

            new Product
            {
                Id = 3,
                Name = "Dog Shampoo",
                Price = 250.00m,
                StockQuantity = 6
            },

            new Product
            {
                Id = 4,
                Name = "Cat Toy",
                Price = 180.00m,
                StockQuantity = 12
            },

            new Product
            {
                Id = 5,
                Name = "Dog Collar",
                Price = 220.00m,
                StockQuantity = 5
            },

            new Product
            {
                Id = 6,
                Name = "Pet Bowl",
                Price = 150.00m,
                StockQuantity = 0
            },

            new Product
            {
                Id = 7,
                Name = "Dog Treats",
                Price = 120.00m,
                StockQuantity = 15
            },

            new Product
            {
                Id = 8,
                Name = "Cat Treats",
                Price = 130.00m,
                StockQuantity = 9
            }
        };

        public static List<Product> GetAll()
        {
            return _products;
        }
    }
}