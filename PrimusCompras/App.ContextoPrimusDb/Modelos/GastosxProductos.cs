using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class GastosxProductos
    {
        public Guid Id { get; set; }

        public int idProducto { get; set; }

        public int idGastos { get; set; }
    }
}
