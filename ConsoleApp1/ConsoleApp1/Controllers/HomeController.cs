using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories;

namespace ConsoleApp1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var products = ProductRepository.GetAll();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
