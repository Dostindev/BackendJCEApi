
using System.ComponentModel.DataAnnotations;
using UniversidadJCE1.Dto;

namespace BackendApi.Models
{
    public class Profesor
    {
        [Key]
        public int IdProfesor { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Activo { get; set; }
        public List<Curso>? Cursos { get; set; }
    }


}

