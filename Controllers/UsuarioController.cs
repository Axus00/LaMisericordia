// Importamos los paquetes y clases necesarios para nuestro controlador

using Microsoft.AspNetCore.Mvc; 
using LaMisericordia.Data; 
using Microsoft.EntityFrameworkCore; 
using LaMisericordia.Models; 
using QRCoder;


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
            await RecargarPagina();
            return View(await _BaseContext.Turnos.ToListAsync());

        }

        public async Task RecargarPagina(){
            await Task.Delay(TimeSpan.FromSeconds(0));
            RedirectToAction("TicketScreen");
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
            int numeroTurno = ObtenerNumeroTurno(); 

            int selectModulo;
            string service = "";
            string codigoTurno = "";

            if (servicio == "SM"){
                codigoTurno = "SM" + numeroTurno.ToString().PadLeft(3, '0');
                service = "Solicitar Medicamento";
                selectModulo = 1;
            }
            else if (servicio == "VF") {
                codigoTurno = "VF" + numeroTurno.ToString().PadLeft(3, '0');
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
            ViewBag.user = HttpContext.Request.Cookies["numeroDocumento"];

            // Generar QR.
            string texto  = $"{service} - {codigoTurno}";
            
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(50);

            var imgBase64 = Convert.ToBase64String(qrCodeImage);

            ViewBag.QRCodeImage = imgBase64;


            var nuevoTurno = new Turno
            {
                UsuariosId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]),
                Estado = "En espera",
                typeServicio = service,
                NameTurno = codigoTurno, 
                FechaHoraInicio = fechaActualInicio,
                Modulo = selectModulo.ToString()
            };

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
        public async Task<IActionResult> UpInfoUser(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, vuelve a mostrar el formulario con los mensajes de error
                return View("FormIndex", usuario);
            }

            Response.Cookies.Append("numeroDocumento", usuario.DocumentoIdentidad);

            _BaseContext.Usuarios.Add(usuario);
            await _BaseContext.SaveChangesAsync();

            var usuarioGuardado = await _BaseContext.Usuarios.FirstOrDefaultAsync(u => u.DocumentoIdentidad == usuario.DocumentoIdentidad);

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

