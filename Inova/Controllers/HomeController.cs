using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inova.Models;

namespace Inova.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult IndexDeslogada()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
