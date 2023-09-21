using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendApi.Dto
{
    public class ProfesorDto
    {
        public int? IdProfesor { get; set; } = null!;
        public string? Nombres { get; set; } = null!;
        public string? Apellidos { get; set; } = null!;
        public bool Activo { get; set; } = false;

        public List<int>? IdsCursos { get; set; } = null;

}
}
