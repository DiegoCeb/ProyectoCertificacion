using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class DevolucionxProducto
    {
        public Guid Id { get; set; }

        public int IdDevolucion { get; set; }

        public int IdProducto { get; set; }
    }
}
