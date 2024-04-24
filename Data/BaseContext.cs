using Microsoft.EntityFrameworkCore;
using LaMisericordia.Models;

namespace LaMisericordia.Data;

public class BaseContext : DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options)
    {
        
    }
    
    //conexicón con modelos
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<AsesorRecepcion> AsesorRecepciones { get; set; }
} 