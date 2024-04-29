using System.ComponentModel.DataAnnotations;
namespace LaMisericordia.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Por favor ingresa tu número de documento.")]
    public string DocumentoIdentidad { get; set; }
    [Required(ErrorMessage = "Por favor selecciona tu tipo de documento.")]
    public string typeDocument { get; set; }
}