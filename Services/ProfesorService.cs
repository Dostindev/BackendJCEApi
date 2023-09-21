using BackendApi.Context;
using BackendApi.Models;
using System;
using UniversidadJCE1.Dto;

namespace BackendApi.Services
{
    public interface IProfesorService
    {
        IEnumerable<Profesor> GetProfesores();
        Profesor GetProfesorById(int id);
        void CreateProfesor(Profesor profesor);
        void UpdateProfesor(Profesor profesor);
    }

    public class ProfesorService : IProfesorService
    {
        private readonly DataContext _context;

        public ProfesorService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Profesor> GetProfesores()
        {
            return _context.Profesores.ToList();
        }

        public Profesor GetProfesorById(int id)
        {
            return _context.Profesores
     .Include(p => p.Cursos) 
     .FirstOrDefault(p => p.IdProfesor == id);

        }

        public void CreateProfesor(Profesor profesor)
        {
            _context.Profesores.Add(profesor);
            _context.SaveChanges();
        }

        public void UpdateProfesor(Profesor profesor)
        {
            _context.Profesores.Update(profesor);
            _context.SaveChanges();
        }

    }
}
