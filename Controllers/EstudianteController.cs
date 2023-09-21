using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using UniversidadJCE1.Dto;
using BackendApi.Models;
using BackendApi.Services;
using BackendApi.Dto;

namespace BackendApi.Controllers
{
    [Route("api/estudiante")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly ICursoService _cursoService;


        public EstudianteController(IEstudianteService  estudianteService, ICursoService cursoService)
        {
            _estudianteService = estudianteService;
            _cursoService = cursoService;
        }

        [HttpGet]
        public IActionResult GetEstudiantes()
        {
            var estudiantes = _estudianteService.GetEstudiantes();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public IActionResult GetEstudiante(int id)
        {
            var estudiante = _estudianteService.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return Ok(JsonSerializer.Serialize(estudiante, options));
        }

        [HttpPost]
        public IActionResult CreateEstudiante(EstudianteDto estudianteDto)
        {

            if(estudianteDto.IdEstudiante != 0)
            {
                var estudianteGuardado = _estudianteService.GetEstudianteById(estudianteDto.IdEstudiante);

                if(estudianteGuardado != null)
                {
                    estudianteGuardado.Nombres = estudianteDto.Nombres;
                    estudianteGuardado.Apellidos = estudianteDto.Apellidos;
                    estudianteGuardado.Activo = estudianteDto.Activo;
                    estudianteGuardado.FechaNacimiento = estudianteDto.FechaNacimiento;

                    _estudianteService.UpdateEstudiante(estudianteGuardado);
                }

                return Ok("Estudiante actualizado correctamente.");

            }
            else
            {
                var estudiante = new Estudiante
                {
                    Nombres = estudianteDto.Nombres,
                    Apellidos = estudianteDto.Apellidos,
                    Activo = estudianteDto.Activo,
                    FechaNacimiento = estudianteDto.FechaNacimiento
                };

                _estudianteService.CreateEstudiante(estudiante);
                return Ok("Estudiante creado correctamente.");
            }


        }

        [HttpPost("asignarcurso")]
        public IActionResult AsignarCurso(AsignarCursoEstudiante asignarCursoEstudiante)
        {
            var estudiante  = _estudianteService.GetEstudianteById(asignarCursoEstudiante.idEstudiante);

            if (estudiante == null)
            {
                return NotFound();
            }
            else { 
            }

            var curso = _cursoService.GetByIdCurso(asignarCursoEstudiante.idCurso);
            if (curso == null)
            {
                return NotFound();
            }

            estudiante.Curso = curso;

            _estudianteService.UpdateEstudiante(estudiante);

            return Ok();
        }


    }
}
