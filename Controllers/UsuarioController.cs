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

<<<<<<< HEAD
        public IActionResult FormIndex() 
=======
        public ActionResult OptionIndex()
>>>>>>> bcb12de21aadca55e07dda622f50d810388964ca
        {
            return View();
        }

    }

}