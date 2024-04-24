using LaMisericordia.Data;
using LaMisericordia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost]
    public async Task<IActionResult> Login(string correo, string contrasena)
    {
        var asesor = await _context.AsesoresRecepcion.FirstOrDefaultAsync(a => a.Correo == correo && a.Contrasena == contrasena);

        //Inicio configuración cookie para los roles
        if(asesor != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, asesor.Correo),
                new Claim("Correo", asesor.Correo)
            };

            foreach (string rol in asesor.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            //Fin configuración cookie para los roles
            
            //seguridad para las cookies
            var cookiesOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(20),
                HttpOnly = true,
                Secure = true
            };
                
            HttpContext.Response.Cookies.Append("Asesor", asesor.Id.ToString(), cookiesOptions);

            TempData["Message"] = "Login is already";
            //Guardamos y redireccionamos
            _context.SaveChanges();
            return RedirectToAction("Home", "Empleados");
        }
        else
        {
            TempData["MessageError"] = "Assesor isn't registered";
            return RedirectToAction("Index", "Empleados");// si  no existe lo devolvemos al Login
        }
        
    }


    [Authorize(Roles = "Asesor")]
    
    public async Task <IActionResult> Home()
    {
        return View(await _context.Turnos.ToListAsync());
    }
}

