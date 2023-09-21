using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendApi.Dto
{
    public class CursoDto
    {
        public int IdCurso { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public int IdProfesor { get; set; }


    }
}
