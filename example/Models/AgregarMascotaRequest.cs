using System.ComponentModel.DataAnnotations;

namespace example.Models
{
    public class AgregarMascotaRequest
    { 
            [Required]
            public string NombreDeLaMascota { get; set; }
            [Required]
            public string TipoDeMascota { get; set; }
            [Required]
            public string NombreDelTutor { get; set; }
            [Required]  
            public string CorreoDelTutor { get; set; }
            [Required]
            public long Celular { get; set; }
            [Required]
            public string Direccion { get; set; }   
    }
}
