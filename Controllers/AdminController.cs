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

    public async Task  <IActionResult> Usuarios()
    {
        return View(await _context.Usuarios.ToListAsync());
    }

    public async Task <IActionResult> Turnos()
    {
        return View(await _context.Turnos.ToListAsync());
    }

    public async Task <IActionResult> Empleados()
    {
        return View(await _context.AsesoresRecepcion.ToListAsync());
    }
}