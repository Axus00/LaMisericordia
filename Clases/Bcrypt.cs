using BCrypt.Net;

namespace LaMisericordia.Clases;

public class Bcrypt
{   
    //Agregamos hash a la password del Asesor
    public string HashContrasena(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool verifyContrasena(string password, string hashedContrasena)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedContrasena);
    }
}