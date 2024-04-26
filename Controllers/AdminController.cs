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
        return View();
    }

    public async Task  <IActionResult> Usuarios() // usuarios
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

    public async Task <IActionResult> Turnos()
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

    public async Task <IActionResult> Empleados()
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