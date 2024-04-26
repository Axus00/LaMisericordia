using LaMisericordia.Data;
using LaMisericordia.Models;
using System.Linq;

namespace LaMisericordia.Clases;

public class Servicios
{
    //Llamamos a la BaseContext de la Data
    private readonly BaseContext _context;

    //Constructor
    public Servicios (BaseContext context)
    {
        _context = context;
    }

    //Creamos clase con el inner join
    public List<Turno> ServicioInner(int usuariosId)
    {
        var peticion = from Turno in _context.Turnos
                        join Usuario in _context.Usuarios on Turno.UsuariosId equals usuariosId
                        where usuariosId == usuariosId
                        select Turno;
        
        return peticion.ToList();
    }

}