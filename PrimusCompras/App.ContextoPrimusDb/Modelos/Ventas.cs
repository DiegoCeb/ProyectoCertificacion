using System;

namespace App.ContextoPrimusDb.Entidades
{
    public class Ventas
    {
        public Guid Id { get; set; }

        public DateTime FechaVenta { get; set; }

        public double TotalVenta { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
