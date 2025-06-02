using SQLite;

namespace AgendaPersonal.Modelos
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(80)]
        public string NombreUsuario { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
    }
}
