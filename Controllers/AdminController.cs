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

        //Turno General
        var contadoGeneral = _context.Turnos.Where(g => g.typeServicio.ToLower().Equals("General")).Count();
        @ViewBag.contadoGeneral = contadoGeneral;

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

    [Authorize(Roles = "Admin")]
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


    [Authorize(Roles = "Admin")]
    //Details
    public async Task<IActionResult> Details(int? id)
    {
        return View(await _context.AsesoresRecepcion.FirstOrDefaultAsync(a => a.Id == id) );
    }


    public async Task<IActionResult> Usuarios() // usuarios
    {
        return View(await _context.Usuarios.ToListAsync()); // 
    }

    public IActionResult DeleteUser(int? id)
    {
        var User = _context.Usuarios.FirstOrDefault(d => d.Id == id);
        _context.Usuarios.Remove(User);
        _context.SaveChanges();
        return RedirectToAction("Usuarios");

    }

    public IActionResult DeleteUser(int? id)
    {
        var User = _context.Usuarios.FirstOrDefault(d => d.Id == id);
        _context.Usuarios.Remove(User);
        _context.SaveChanges();
        return RedirectToAction("Usuarios");

    }

    public async Task<IActionResult> Turnos()
    {
        return View(await _context.Turnos.ToListAsync());
    }

    public IActionResult DeleteTurnos(int? id)
    {
        var Turno = _context.Turnos.FirstOrDefault(d => d.Id == id);

        _context.Turnos.Remove(Turno);
        _context.SaveChanges();
        return RedirectToAction("Turnos");
    }

    public IActionResult DeleteTurnos(int? id)
    {
        var Turno = _context.Turnos.FirstOrDefault(d => d.Id == id);

        _context.Turnos.Remove(Turno);
        _context.SaveChanges();
        return RedirectToAction("Turnos");
    }

    public async Task<IActionResult> Empleados()
    {
        return View(await _context.AsesoresRecepcion.ToListAsync());
    }

    public IActionResult DeleteEmpleados(int? id)
    {
        var Empleado = _context.AsesoresRecepcion.FirstOrDefault(d => d.Id == id);

        _context.AsesoresRecepcion.Remove(Empleado);
        _context.SaveChanges();
        return RedirectToAction("Empleados");
    }

    public IActionResult Edit(int? id )
    {
        var Empleado = _context.AsesoresRecepcion.FirstOrDefault(d => d.Id == id);
        return View(Empleado);
    }

    [HttpPost]
    public IActionResult Actualizar(int? id,AsesorRecepcion A)
    {
        _context.AsesoresRecepcion.Update(A);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    

    
}