using BackendApi.Dto;
using Microsoft.AspNetCore.Mvc;

[Route("api/curso")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpPost]
    public IActionResult CrearCurso([FromBody] CursoDto cursoDto)
    {
        if(cursoDto.IdCurso != 0)
        {
            _cursoService.UpdateCurso(cursoDto.IdCurso, cursoDto.Descripcion, cursoDto.Fecha, cursoDto.IdProfesor);

            return Ok("Curso actualizado correctamente");

        }
        else
        {
            _cursoService.CreateCurso(cursoDto.Descripcion, cursoDto.Fecha, cursoDto.IdProfesor);

            return Ok("Curso creado correctamente");

        }

    }

    [HttpGet]
    public IActionResult ObtenerCursos()
    {
        var cursos = _cursoService.GetCursos();
        return Ok(cursos);
    }

    [HttpGet("{idCurso}")]
    public IActionResult ObtenerCursoPorId(int idCurso)
    {
        var curso = _cursoService.GetByIdCurso(idCurso);
        if (curso == null)
        {
            return NotFound();
        }
        return Ok(curso);
    }

    [HttpGet("estudiantesporcurso/{idCurso}")]
    public IActionResult ObtenerEstudiantePorCurso(int idCurso)
    {
        var estudiantes = _cursoService.GetByIdCursoListaEstudiantes(idCurso);
        if (estudiantes.Count == 0)
        {
            return NotFound();
        }
        return Ok(estudiantes);
    }

  
    [HttpGet("cursosdisponibles")]
    public ActionResult<IEnumerable<Curso>> GetCursosSinProfesor()
    {
        var cursosSinProfesor = _cursoService.ObtenerCursosSinProfesor();
        return Ok(cursosSinProfesor);
    }

}
