using BackendApi.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Data.SqlClient;
using System.Data;

public interface ICursoService
{
    void CreateCurso(string descripcion, DateTime fecha, int idProfesor);
    IEnumerable<Curso> GetCursos();
    Curso GetByIdCurso(int idCurso);
    void UpdateCurso(int idCurso, string descripcion, DateTime fecha, int profesorId);

    List<Curso> ObtenerCursosSinProfesor();

    public List<Estudiante> GetByIdCursoListaEstudiantes(int idCurso);

}

public class CursoService : ICursoService
{
    private readonly DataContext _context;

    private readonly String connectionString = "server=DESKTOP-0AG0BLJ\\SQLEXPRESS;Database=UniversidadJCE; Integrated Security=true; TrustServerCertificate=true;";


    public CursoService(DataContext context)
    {
        _context = context;
    }

    public void CreateCurso(string descripcion, DateTime fecha, int idProfesor)
    {

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("CreateCurso", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@Fecha", fecha);

                command.ExecuteNonQuery();
            }
        }
    }

    public IEnumerable<Curso> GetCursos()
    {
        List<Curso> cursos = new List<Curso>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("GetCursos", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Curso curso = new Curso
                        {
                            IdCurso = reader.GetInt32(reader.GetOrdinal("IdCurso")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha"))

                        };
                          
                        cursos.Add(curso);
                    }
                }
            }
        }

        return cursos;
    }



    public Curso GetByIdCurso(int idCurso)
    {
        Curso curso = null;

        using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("GetByIdCurso", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Configura el parámetro @IdCurso
                cmd.Parameters.Add(new SqlParameter("@IdCurso", idCurso));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        curso = new Curso
                        {
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        };

                        return curso;

                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }    

    public void UpdateCurso(int idCurso, string descripcion, DateTime fecha, int profesorId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            if (profesorId != 0)
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCursoProfesorId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Configura los parámetros del procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@IdCurso", idCurso));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", fecha));
                    cmd.Parameters.Add(new SqlParameter("@ProfesorId", profesorId));

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                    using (SqlCommand cmd = new SqlCommand("UpdateCurso", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Configura los parámetros del procedimiento almacenado
                        cmd.Parameters.Add(new SqlParameter("@IdCurso", idCurso));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@Fecha", fecha));

                        cmd.ExecuteNonQuery();
                    }
                }
            }

           
        }
    

    public List<Curso> ObtenerCursosSinProfesor()
    {
        return _context.Cursos
            .Where(c => c.ProfesorId == null)
            .ToList();
    }


    public List<Estudiante> GetByIdCursoListaEstudiantes(int idCurso)
    {
        List<Estudiante> listaEstudiantes = new List<Estudiante>();

        var curso =  _context.Cursos
        .Include(e => e.Estudiantes)
               .FirstOrDefault(e => e.IdCurso == idCurso);

        listaEstudiantes = curso.Estudiantes;

        return listaEstudiantes;

    }

}
