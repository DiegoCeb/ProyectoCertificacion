using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class VentasxProductos
    {
        public Guid Id { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }
    }
}
