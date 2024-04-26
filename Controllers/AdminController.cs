using LaMisericordia.Data;
using LaMisericordia.Models;
using BCrypt.Net;
using LaMisericordia.Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LaMisericordia.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly BaseContext _context;

    private readonly Bcrypt _bcrypt;

    public AdminController(BaseContext context, Bcrypt bcrypt)
    {
        _context = context;
        _bcrypt = bcrypt;
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

    //Create
    [HttpPost]
    public async Task<IActionResult> Create(AsesorRecepcion asesor)
    {
        
        asesor.Contrasena = BCrypt.Net.BCrypt.HashPassword(asesor.Contrasena);
        _context.AsesoresRecepcion.Add(asesor);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Empleados");
    }


    //Details
    public async Task<IActionResult> Details(int? id)
    {
        return View(await _context.AsesoresRecepcion.FirstOrDefaultAsync(a => a.Id == id) );
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