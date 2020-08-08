using App.ContextoPrimusDb.Entidades;
using System;
using System.Collections.Generic;

namespace App.API.Services
{
    public interface IProductoService
    {
        List<Productos> GetAll();
        Productos GetById(Guid id);
        bool Create(Productos newProduct);
        bool Update(Productos productToUpdate, Guid id);
    }
}
