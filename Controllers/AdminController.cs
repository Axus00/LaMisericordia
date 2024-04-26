using LaMisericordia.Data;
using LaMisericordia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LaMisericordia.Controllers;


public class AdminController : Controller
{
    private readonly BaseContext _context;

    public AdminController(BaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        //guardamos contador de turnos totoales
        var contadorTurno = _context.Turnos.Count();
        @ViewBag.contador = contadorTurno;

        //Cantidad de medicamentos
        var contadorMedicamento = _context.Turnos.Where(m => m.typeServicio.Equals("Medicamento")).Count();
        @ViewBag.contador2 = contadorMedicamento;

        //Total asesores
        var contadorAsesor = _context.AsesoresRecepcion.Count();
        @ViewBag.contadorAsesor = contadorAsesor;

        //Admins
        var contadorAdmins = _context.AsesoresRecepcion.Where(a => a.Roles.Equals("Admin")).Count();
        @ViewBag.contadorAdmins = contadorAdmins;

        //Total Usuarios
        var contadorUsuarios = _context.Usuarios.Count();
        @ViewBag.contadorUsuarios = contadorUsuarios;

        return View();
    }

    //crear Asesores
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(AsesorRecepcion asesor)
    {
        _context.AsesoresRecepcion.Add(asesor);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }

    public async Task<IActionResult> Usuarios() // usuarios
    {
        return View(await _context.Usuarios.ToListAsync()); // 
    }

    public async Task<IActionResult> Turnos()
    {
        return View(await _context.Turnos.ToListAsync());
    }

    public async Task<IActionResult> Empleados()
    {
        return View(await _context.AsesoresRecepcion.ToListAsync());
    }
}