using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class Usuarios
    {
        public Guid Id { get; set; }

        public int TipoUsuario { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Telefono { get; set; }

    }
}
