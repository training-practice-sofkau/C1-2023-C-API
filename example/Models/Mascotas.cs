namespace example.Models
{
    public class Mascotas
    {
        public Guid Id { get; set; }
        public string NombreDeLaMascota { get; set; }
        public string TipoDeMascota { get; set; }
        public string NombreDelTutor { get; set; }
        public string CorreoDelTutor { get; set; }
        public long Celular { get; set; }
        public string Direccion { get; set; }

    }
}
