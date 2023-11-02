using System;

namespace CapaNegocio
{
    public class EstudianteDto
    {
        public int Carnet { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime? FechaNac { get; set; }

        public int? Nota { get; set; }
    }
}
