using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class ProveedoresxProducto
    {
        public Guid id { get; set; }

        public int IdProveedor { get; set; }

        public int IdProducto { get; set; }
    }
}
