using LaMisericordia.Data;
using Microsoft.AspNetCore.Mvc;
using LaMisericordia.Models;
using Microsoft.EntityFrameworkCore;

namespace LaMisericordia.Controllers;

public class EmpleadosController : Controller
{
    private readonly BaseContext _context;
    public EmpleadosController(BaseContext context)
    {
        _context = context;
    }

    //vistas
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login(string correo, string contrasena)
    {
        var asesor = await _context.AsesoresRecepcion.FirstOrDefaultAsync(a => a.Correo == correo && a.Contrasena == contrasena);

        if(asesor != null)
        {
            
            //seguridad para las cookies
            var cookiesOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(20),
                HttpOnly = true,
                Secure = true
            };
                
            HttpContext.Response.Cookies.Append("Asesor", asesor.Id.ToString(), cookiesOptions);

            _context.SaveChanges();
            return RedirectToAction("Home", "Empleados");
        }

        return RedirectToAction("Index", "Empleados");
    }


    public IActionResult Home()
    {
        return View();
    }
}

