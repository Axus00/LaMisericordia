namespace LaMisericordia.Models;

public class AsesorRecepcion
{
    public int Id { get; set; }
    public string? Correo { get; set; }
    public string? Contrasena { get; set; }
    public string? Modulo { get; set; }
    
    public string[] Roles { get; set; }
}