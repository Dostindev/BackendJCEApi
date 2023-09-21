using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using UniversidadJCE1.Dto;
using BackendApi.Models;
using BackendApi.Services;
using BackendApi.Dto;

namespace BackendApi.Controllers
{
    [Route("api/profesor")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        private readonly ICursoService _cursoService;


        public ProfesorController(IProfesorService profesorService, ICursoService cursoService)
        {
            _profesorService = profesorService;
            _cursoService = cursoService;
        }

        [HttpGet]
        public IActionResult GetProfesores()
        {
            var profesores = _profesorService.GetProfesores();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfesor(int id)
        {
            var profesor = _profesorService.GetProfesorById(id);
            if (profesor == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return Ok(JsonSerializer.Serialize(profesor, options));
        }

        [HttpPost]
        public IActionResult CreateProfesor(ProfesorDto profesorDTO)
        {

            if (profesorDTO.IdProfesor != 0)
            {
                var profesorGuardado = _profesorService.GetProfesorById((int)profesorDTO.IdProfesor);

                if (profesorGuardado != null)
                {
                    profesorGuardado.Nombres = profesorDTO.Nombres;
                    profesorGuardado.Apellidos = profesorDTO.Apellidos;
                    profesorGuardado.Activo = profesorDTO.Activo;

                    _profesorService.UpdateProfesor(profesorGuardado);
                }

                return Ok("Profesor actualizado correctamente.");

            }
            else
            {
                var profesor = new Profesor
                {
                    Nombres = profesorDTO.Nombres,
                    Apellidos = profesorDTO.Apellidos,
                    Activo = profesorDTO.Activo,
                };

                _profesorService.CreateProfesor(profesor); // Reemplaza con tu método de servicio para crear profesores.
                return Ok("Profesor creado correctamente.");
            }

        }

        [HttpPost("asignar-cursos")]
        public IActionResult AsignarCursosAProfesor(AsignarCursosProfesorDto asignarCursos)
        {
            // Verifica si el profesor existe
            var profesor = _profesorService.GetProfesorById(asignarCursos.idProfesor);
            if (profesor == null)
            {
                return NotFound("Profesor no encontrado.");
            }

            // Verifica si los cursos existen y no están asignados a otro profesor
            foreach (var idCurso in asignarCursos.idsCursos)
            {
                var cursoEncontrado = _cursoService.GetByIdCurso(idCurso);
                if (cursoEncontrado == null)
                {
                    return NotFound($"Curso con ID {idCurso} no encontrado.");
                }

                if (cursoEncontrado.ProfesorId != null)
                {
                    return BadRequest($"El curso con ID {idCurso} ya está asignado a otro profesor.");
                }
            }

            // Asigna los cursos al profesor
            foreach (var idCurso in asignarCursos.idsCursos)
            {
                var cursoGuardar = _cursoService.GetByIdCurso(idCurso);
                profesor.Cursos.Add(cursoGuardar);

                _cursoService.UpdateCurso(cursoGuardar.IdCurso, cursoGuardar.Descripcion, cursoGuardar.Fecha, asignarCursos.idProfesor);
            }

            // Actualiza el profesor en la base de datos
            _profesorService.UpdateProfesor(profesor);

            return Ok("Cursos asignados correctamente al profesor.");
        }

    }
}
