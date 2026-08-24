using ConsoleApp1.DTOs;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = CartRepository.GetCart();
            return View(cart);
        }

        
        [HttpPost]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var product = ProductRepository.GetAll()
                .FirstOrDefault(p => p.Id == dto.ProductId);

            if (product == null)
            {
                return NotFound();
            }

          
            if (dto.Quantity > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot add more than available stock ({product.StockQuantity}).";
                return RedirectToAction("Index", "Home");
            }

            var cart = CartRepository.GetCart();
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                
                if (existingItem.Quantity + dto.Quantity > product.StockQuantity)
                {
                    TempData["ErrorMessage"] = $"Cannot exceed total available stock ({product.StockQuantity}).";
                    return RedirectToAction("Index", "Home");
                }
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
             
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = dto.Quantity
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(UpdateCartDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", CartRepository.GetCart());
            }

            var product = ProductRepository.GetAll()
                .FirstOrDefault(p => p.Id == dto.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Product not found.");
                return View("Index", CartRepository.GetCart());
            }

            if (dto.Quantity > product.StockQuantity)
            {
                ModelState.AddModelError(
                    "",
                    $"Only {product.StockQuantity} item(s) are available.");

                return View("Index", CartRepository.GetCart());
            }

            var cart = CartRepository.GetCart();

            var item = cart.Items
                .FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (item == null)
            {
                ModelState.AddModelError(
                    "",
                    "The product is not in the cart.");

                return View("Index", cart);
            }

            item.Quantity = dto.Quantity;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = CartRepository.GetCart();

            var item = cart.Items
                .FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            return RedirectToAction("Index");
        }
    }
}