using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace App.ContextoPrimusDb.Entidades
{
    public class Productos
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioProducto { get; set; }
        public Categorias Categoria { get; set; }
        public IEnumerable<Marcas> Marca { get; set; }


        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
