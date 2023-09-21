using BackendApi.Context;
using BackendApi.Models;
using System;
using UniversidadJCE1.Dto;

namespace BackendApi.Services
{
    public interface IEstudianteService
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteById(int id);
        void CreateEstudiante(Estudiante estudiante);
        void UpdateEstudiante(Estudiante estudiante);
    }

    public class EstudianteService : IEstudianteService
    {
        private readonly DataContext _context;


        public EstudianteService(DataContext context)
        {
            _context = context;

        }
      

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return _context.Estudiantes.Include(e => e.Curso).ToList();
        }

        public Estudiante GetEstudianteById(int id)
        {
            return _context.Estudiantes
            .Include(e => e.Curso)
                .FirstOrDefault(e => e.IdEstudiante == id);
        }

        public void CreateEstudiante(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            _context.SaveChanges();
        }

        public void UpdateEstudiante(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            _context.SaveChanges();
        }

       
    }
}
