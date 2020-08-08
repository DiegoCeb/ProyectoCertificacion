using App.ContextoPrimusDb.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.API.Data.EfCore
{
    public class ProductosRepository : EfCoreRepository<Productos, ContextoPrimusDb.AppDbContext>
    {
        private readonly ContextoPrimusDb.AppDbContext _appDbContext;

        public ProductosRepository(ContextoPrimusDb.AppDbContext context)
            : base(context)
        {
            _appDbContext = context;
        }

        internal async Task<bool> UserOwnsProductosAsync(Guid productoId, string userId)
        {
            var producto = await _appDbContext.Productos.SingleOrDefaultAsync(x => x.Id == productoId);

            return producto != null && producto.UserId == userId;
        }
    }
}
