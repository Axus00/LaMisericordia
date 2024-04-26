// Importamos los paquetes y clases necesarios para nuestro controlador

using Microsoft.AspNetCore.Mvc; 
using LaMisericordia.Data; 
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; 
using LaMisericordia.Models; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Speech.Synthesis;

// Definimos el espacio de nombres y la clase para nuestro controlador de empleados

namespace LaMisericordia.Controllers 
{
    public class UsuarioController : Controller 
    {
        // Declaramos los campos para acceder al contexto de la base de datos y la clase de ayuda para subir archivos
        public readonly BaseContext _BaseContext; 


        // Constructor para inicializar los campos
        public UsuarioController(BaseContext BaseContext) 
        {
            _BaseContext = BaseContext; 
        }

        public IActionResult Index() 
        {
            return View();
        }
        
        //View User TicketScreen
        

        public IActionResult FormIndex()        
        {
            return View();
        }
        public ActionResult OptionIndex()
        {
            return View();
        }

        public async Task<ActionResult> Ticket(string servicio)
        {
            int numeroTurno = ObtenerNumeroTurno(); 

            string codigoTurno = servicio + numeroTurno.ToString().PadLeft(3, '0');

            ViewBag.CodigoTurno = codigoTurno;

            // _BaseContext.Turnos.Add(codigoTurno);

            await _BaseContext.SaveChangesAsync();

            return View();
        }
        
        private int ObtenerNumeroTurno()
        {
            
            int numeroTurnoActual = Convert.ToInt32(HttpContext.Request.Cookies["NumeroTurno"]);

            numeroTurnoActual++;

            Response.Cookies.Append("NumeroTurno", numeroTurnoActual.ToString());

            return numeroTurnoActual;
            
        }



        public async Task<IActionResult> TicketScreen(){
            
            return View();
        }


    

    }

}