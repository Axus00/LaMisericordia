// Importamos los paquetes y clases necesarios para nuestro controlador

using Microsoft.AspNetCore.Mvc; 
using LaMisericordia.Data; 
using Microsoft.EntityFrameworkCore; 
using LaMisericordia.Models; 

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
        public async Task<IActionResult> TicketScreen()
        {
            return View();
        }

        public IActionResult FormIndex()        
        {
            return View();
        }
        public ActionResult OptionIndex()
        {
            return View();
        }

        public ActionResult Ticket(string servicio)
        {
            int numeroTurno = ObtenerNumeroTurno(); 

            string codigoTurno = servicio + numeroTurno.ToString().PadLeft(3, '0');

            ViewBag.CodigoTurno = codigoTurno;

            return View();
        }
        
        private int ObtenerNumeroTurno()
        {
            
            int numeroTurno = 0;

            numeroTurno++;

            return numeroTurno;
        }

    

    }

}