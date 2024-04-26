using LaMisericordia.Data;
using LaMisericordia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Speech.Synthesis;

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

            Response.Cookies.Append("Modulo",asesor.Modulo);
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
        var modulo = HttpContext.Request.Cookies["Modulo"];
        return View(await _context.Turnos.ToListAsync());
    }

    public IActionResult Liberar(int id)
    {

        var turno = _context.Turnos.FirstOrDefault(t => t.Id == id);
        turno.Estado = "Finalizado";
        turno.FechaHoraFin =DateTime.Now;
        _context.Turnos.Update(turno);
        _context.SaveChanges();
        return RedirectToAction("Home");

    }


    public IActionResult Ausente(int id)
    {
        var turno  = _context.Turnos.FirstOrDefault(d => d.Id == id);
        turno.Estado = "Ausente";
        turno.FechaHoraFin = DateTime.Now;
        _context.Turnos.Update(turno);
        _context.SaveChanges();
        return RedirectToAction("Home");

    }

    public IActionResult SubirTurno(int id )
    {
        var modulo = HttpContext.Request.Cookies["Modulo"];
        var turno = _context.Turnos.FirstOrDefault(d => d.Id == id);

        turno.Estado = "En proceso";
        turno.Modulo = modulo;
        turno.FechaHoraFin = DateTime.Now;
        TempData ["Update"] = "Se ha Agregado un nuevo turno";

        _context.Turnos.Update(turno);
        _context.SaveChanges();
        return RedirectToAction("Home");        
    }


    public async Task <IActionResult> Medicamentos()
    {
        var Medicamentos = _context.Turnos.Where(c => c.typeServicio == "Medicamentos");
        
        return View("Home",await Medicamentos.ToListAsync());
    }

    public async Task <IActionResult> Pagos()
    {
        var Pagos = _context.Turnos.Where(c => c.typeServicio == "Pagos");
        return View("Home",await Pagos.ToListAsync());
    }

    public async Task <IActionResult> General()
    {
        var General = _context.Turnos.Where(c => c.typeServicio == "General");
        return View("Home",await General.ToListAsync());
    }

    public async Task<IActionResult> Llamar(int id){
        var llamado = _context.Turnos.FirstOrDefault(d => d.Id == id);
        await RepetirTurno(llamado.NameTurno);
        return RedirectToAction("Home");
    }
    private async Task RepetirTurno(string Turno)
    {
        //Primera llamada
        await Task.Delay(TimeSpan.FromSeconds(0));
        Speak(Turno);
        //Segunda llamada
        await Task.Delay(TimeSpan.FromSeconds(2));
        Speak(Turno);
    }
    private void Speak(string Turno)
    {
        var VozTurno = new SpeechSynthesizer();
        VozTurno.SpeakAsync(Turno);
    }

    
}

