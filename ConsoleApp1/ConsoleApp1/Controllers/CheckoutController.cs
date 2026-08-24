using Microsoft.AspNetCore.Mvc;
using ConsoleApp1.Models;
using ConsoleApp1.Models.DTOs;
using ConsoleApp1.Repositories;

namespace ConsoleApp1.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: /Checkout/Index
        [HttpGet]
        public IActionResult Index()
        {
            var cart = CartRepository.GetCart();
            if (cart.Items == null || !cart.Items.Any())
            {
                TempData["ErrorMessage"] = "Cannot proceed to checkout with an empty cart.";
                return RedirectToAction("Index", "Cart");
            }

            ViewBag.CartTotal = cart.GrandTotal;
            return View(new CheckoutFormDTO());
        }

        // POST: /Checkout/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(CheckoutFormDTO dto)
        {
            var cart = CartRepository.GetCart();

            if (!cart.Items.Any())
            {
                ModelState.AddModelError("", "Cannot process a checkout with 0 items.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CartTotal = cart.GrandTotal;
                return View("Index", dto);
            }

            var transaction = new Transaction
            {
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                TotalAmount = cart.GrandTotal,
                PurchasedItems = new List<CartItem>(cart.Items)
            };

            // Nabawas ang stock gamit ang LINQ para maiwasan ang CS0117 Error
            foreach (var item in cart.Items)
            {
                var product = ProductRepository.GetAll().FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                }
            }

            // Gagamana na ito dahil static na ang TransactionRepository
            TransactionRepository.Add(transaction);
            CartRepository.ClearCart();

            return RedirectToAction("Success", new { id = transaction.TransactionId });
        }

        public IActionResult Success(Guid id)
        {
            var transaction = TransactionRepository.GetById(id);
            if (transaction == null) return NotFound();

            return View(transaction);
        }

        public IActionResult History()
        {
            var transactions = TransactionRepository.GetAll();
            return View(transactions);
        }

        public IActionResult Details(Guid id)
        {
            var transaction = TransactionRepository.GetById(id);
            if (transaction == null) return NotFound();

            return View(transaction);
        }
    }
}