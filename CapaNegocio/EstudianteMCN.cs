using CapaDatos;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio
{
    public class EstudianteMCN
    {
        private readonly SIRAEntities db;

        public EstudianteMCN()
        {
            db = new SIRAEntities();
        }

        public EstudianteDto BuscarPorCarnet(int carnet)
        {
            return (from e in db.Estudiante
                    where e.Carnet == carnet
                    select new EstudianteDto
                    {
                        Carnet = e.Carnet,
                        Nombres = e.Nombres,
                        Apellidos = e.Apellidos,
                        FechaNac = e.FechaNac,
                        Nota = e.Nota
                    }).FirstOrDefault();
        }

        public bool Insertar(EstudianteDto est)
        {
            try
            {
                Estudiante nuevoEst = new Estudiante()
                {
                    Carnet = est.Carnet,
                    Nombres = est.Nombres,
                    Apellidos = est.Apellidos,
                    FechaNac = est.FechaNac,
                    Nota = est.Nota
                };

                db.Estudiante.Add(nuevoEst);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Actualizar(EstudianteDto actualEst)
        {
            try
            {
                Estudiante est = db.Estudiante.Find(actualEst.Carnet);

                if (est is null)
                    return false;

                est.Nombres = actualEst.Nombres;
                est.Apellidos = actualEst.Apellidos;
                est.FechaNac = actualEst.FechaNac;
                est.Nota = actualEst.Nota;

                db.Entry(est).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int carnet)
        {
            try
            {
                Estudiante est = db.Estudiante.Find(carnet);

                if (est is null)
                    return false;

                db.Estudiante.Remove(est);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EstudianteDto> ObtenerTodos()
        {
            return db.Estudiante
                .Select(x => new EstudianteDto
                {
                    Carnet = x.Carnet,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    FechaNac = x.FechaNac,
                    Nota = x.Nota
                }).ToList();
        }
    }
}
