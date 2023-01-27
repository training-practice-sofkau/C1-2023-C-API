using System.ComponentModel.DataAnnotations;

namespace example.Model
{
    public class Jugador
    {
        public Guid Id { get; set; }
        [RegularExpression("^[1-9]{1}[0-9]{7}$",
            ErrorMessage = "Ingresar DNI sin puntos")]

        public string? name { get; set; }
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]

        public string? pais { get; set; }
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]

        public string? equipo { get; set; }
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]

        public string? posicion { get; set; }
       
    }
}