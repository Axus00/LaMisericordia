using Microsoft.AspNetCore.Mvc;

namespace LaMisericordia.Controllers;

public class EmpleadosController : Controller
{
    //vistas
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Home()
    {
        return View();
    }
}

