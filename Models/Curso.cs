using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackendApi.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int? ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        [JsonIgnore]
        public List<Estudiante>? Estudiantes { get; set; }

    }
}
