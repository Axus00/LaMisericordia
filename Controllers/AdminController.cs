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

    

    public async Task<IActionResult>  Index()
    {

        var viewModel = new IndexViewModel();

        //guardamos contador de turnos totoales
        viewModel.TotalTurnos = await _context.Turnos.CountAsync();

        //Cantidad de medicamentos
        viewModel.TotalMedicamentos = await _context.Turnos.Where(m => m.typeServicio == "Medicamentos").CountAsync();

        //Turno General
        viewModel.TotalGeneral = await _context.Turnos.Where(g => g.typeServicio.ToLower() == "general").CountAsync();

        //Total asesores
        viewModel.TotalAsesores = await _context.AsesoresRecepcion.CountAsync();

        //Admins
        viewModel.TotalAdmins =  await _context.AsesoresRecepcion.Where(a => a.Roles.Equals("Admin")).CountAsync();

        //Total Usuarios
        viewModel.TotalUsuarios = await _context.Usuarios.CountAsync();

        return View(viewModel);
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

    public async Task<IActionResult> Turnos()
    {
        return View(await _context.Turnos.ToListAsync());
    }

    public IActionResult DeleteTurnos(int? id)
    {
        var Turno = _context.Turnos.FirstOrDefault(d => d.Id == id);

        //agregamos mensaje al momento de eliminar
        TempData["Eliminado"] = "El turno ha sido eliminado";

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