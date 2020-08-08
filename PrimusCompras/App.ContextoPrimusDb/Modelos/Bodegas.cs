using System;
using System.Collections.Generic;

namespace App.ContextoPrimusDb.Entidades
{
    public class Bodegas
    {
        public Guid Id { get; set; }

        public IList<Productos> IdProducto { get; set; }

        public int Cantidad { get; set; }
    }
}
