using ConsoleApp1.DTOs;
using ConsoleApp1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductRepository.GetAll();

            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var products = ProductRepository.GetAll();

                return View("Index", products);
            }

            var product = ProductRepository.GetAll()
                .FirstOrDefault(p => p.Id == dto.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Product not found.");

                return View("Index", ProductRepository.GetAll());
            }

            if (dto.Quantity > product.StockQuantity)
            {
                ModelState.AddModelError(
                    "",
                    $"Only {product.StockQuantity} item(s) are available."
                );

                return View("Index", ProductRepository.GetAll());
            }

            var cart = CartRepository.GetCart();

            var existingItem = cart.Items
                .FirstOrDefault(item => item.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + dto.Quantity;

                if (newQuantity > product.StockQuantity)
                {
                    ModelState.AddModelError(
                        "",
                        $"You cannot add that many. Only {product.StockQuantity} item(s) are available."
                    );

                    return View("Index", ProductRepository.GetAll());
                }

                existingItem.Quantity = newQuantity;
            }
            else
            {
                cart.Items.Add(new ConsoleApp1.Models.CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price
                });
            }

            return RedirectToAction("Index");
        }
    }
}