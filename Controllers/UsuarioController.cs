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
        public async Task<IActionResult> TicketScreen()
        {
            return View(await _BaseContext.Turnos.ToListAsync());
        }

        public IActionResult FormIndex()        
        {
            return View();
        }
        public async Task<ActionResult> OptionIndex()
        {
            var usuario = await _BaseContext.Usuarios.FirstOrDefaultAsync();
            return View();
        }

        public async Task<ActionResult> Ticket(string servicio)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            int numeroTurno = ObtenerNumeroTurno(); 
            int selectModulo;
            string service = "";
            string codigoTurno = "";

            if (servicio == "MC"){
                codigoTurno = "MC" + numeroTurno.ToString().PadLeft(3, '0');
                service = "Medicamentos";
                selectModulo = 1;
            }
            else if (servicio == "SM") {
                codigoTurno = "SM" + numeroTurno.ToString().PadLeft(3, '0');
                service = "Realizar Pagos";
                selectModulo = 2;
            }
            else {
                codigoTurno = "SC" + numeroTurno.ToString().PadLeft(3, '0');
                service = "Cita General";
                selectModulo = 3;
            }

            DateTime fechaActualInicio = DateTime.Now; 
            
            ViewBag.CodigoTurno = codigoTurno;

            ViewBag.FechaActualInicio = fechaActualInicio;


            var nuevoTurno = new Turno
            {
                UsuariosId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]),
                Estado = "En espera",
                typeServicio = servicio,
                NameTurno = codigoTurno, 
                FechaHoraInicio = fechaActualInicio,
                Modulo = selectModulo.ToString()
            };

            ViewBag.user = HttpContext.Request.Cookies["numeroDocumento"];

            _BaseContext.Turnos.Add(nuevoTurno);

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


        [HttpPost]
        public IActionResult ReiniciarTurno()
        {

            Response.Cookies.Append("NumeroTurno", "0");

            return RedirectToAction("Ticket"); 
            
        }

        [HttpPost]
        public async Task<IActionResult> UpInfoUser(string tipoDocumento, string numeroDocumento)
        {
            Response.Cookies.Append("numeroDocumento", numeroDocumento);

            var nuevoUsuario = new Usuario
            {
                DocumentoIdentidad = numeroDocumento,
                typeDocument = tipoDocumento
            };

            ViewBag.numDocumento = numeroDocumento;
            ViewBag.tipoDocumento = tipoDocumento;

            _BaseContext.Usuarios.Add(nuevoUsuario); 
            await _BaseContext.SaveChangesAsync(); 

            var usuarioGuardado = await _BaseContext.Usuarios.FirstOrDefaultAsync(u => u.DocumentoIdentidad == numeroDocumento);
            
            if (usuarioGuardado != null)
            {
                int userId = usuarioGuardado.Id;
                Response.Cookies.Append("userId", userId.ToString());
            }
            else
            {
                ViewBag.IDuser = "Usuario no encontrado";
            }

            return RedirectToAction(nameof(OptionIndex)); 
        }


       


    }

}