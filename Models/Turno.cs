namespace LaMisericordia.Models;

public class Turno
{
    public int Id { get; set; }
    public int UsuariosId { get; set; }
    public string? typeServicio{ get; set;}
    public string? NameTurno { get; set;}
    public DateTime? FechaHoraInicio { get; set; }
    public DateTime? FechaHoraFin { get; set; }
    public string? Estado { get; set; }
    public string? Modulo {get; set; }
}