using Microsoft.EntityFrameworkCore;

namespace BackendApi.Dto
{
    public class EstudianteDto
    {
        public int IdEstudiante { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }

    }
}
