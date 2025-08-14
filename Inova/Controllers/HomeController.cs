using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inova.Models;
using Inova.Helper;
using Inova.Filters;

namespace Inova.Controllers;

[PaginaParaUsuarioLogado]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
    [PaginaRestritaSomenteAdmin]
    public IActionResult IndexAdm()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
