namespace LaMisericordia.Models;

public class Turno
{
    public int Id { get; set; }
    public int UsuariosId { get; set; }
    public int ServicioId { get; set; }
    public DateTime? FechaHoraInicio { get; set; }
    public DateTime? FechaHoraFin { get; set; }
    public string? Estado { get; set; }
}