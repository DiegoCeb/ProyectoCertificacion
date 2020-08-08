using App.API.Contracts.V1;
using App.API.Contracts.V1.Requests;
using App.API.Contracts.V1.Responses;
using App.API.Data.EfCore;
using App.API.Extensions;
using App.ContextoPrimusDb.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController : Controller
    {
        private readonly ProductosRepository _productosRepository;

        public ProductosController(ProductosRepository productosService)
        {
            _productosRepository = productosService;
        }

        [HttpGet(ApiRoutes.Productos.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var productos = from producto in await _productosRepository.GetAll()
                            select new ProductosResponse
                            {
                                Id = producto.Id,
                                Nombre = producto.Nombre,
                                Descripcion = producto.Descripcion,
                                PrecioProducto = producto.PrecioProducto,
                                Categoria = producto.Categoria,
                                Marca = producto.Marca
                            };

            return Ok(productos);
        }

        [HttpGet(ApiRoutes.Productos.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid productoId)
        {
            var producto = await _productosRepository.Get(productoId);

            if (producto == null) return NotFound();

            return Ok(new ProductosResponse
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioProducto = producto.PrecioProducto,
                Categoria = producto.Categoria,
                Marca = producto.Marca
            });
        }

        [HttpPost(ApiRoutes.Productos.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductoRequest productoRequest)
        {
            List<Marcas> marcas = new List<Marcas> { new Marcas { Id = Guid.NewGuid(), NombreMarca = productoRequest.Marca } };

            var newProducto = new Productos
            {
                Nombre = productoRequest.Nombre,
                Categoria = new Categorias { Id = Guid.NewGuid(), NombreCategoria = productoRequest.Categoria },
                Descripcion = productoRequest.Descripcion,
                PrecioProducto = productoRequest.PrecioProducto,
                Marca = marcas,
                UserId = HttpContext.GetUserId()
            };

            newProducto.Id = Guid.NewGuid();

            Productos result = await _productosRepository.Add(newProducto);

            if (result == null) return BadRequest();

            var response = new ProductosResponse
            {
                Id = newProducto.Id,
                Nombre = newProducto.Nombre,
                Descripcion = newProducto.Descripcion,
                PrecioProducto = newProducto.PrecioProducto,
                Categoria = newProducto.Categoria,
                Marca = newProducto.Marca
            };

            string urlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            string locationUrl = $"{urlBase}/{ApiRoutes.Productos.Get.Replace("{productoId}", newProducto.Id.ToString())}";

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Productos.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid productoId, [FromBody] UpdateProductoRequest productoRequest)
        {
            var userOwnsProductos = await _productosRepository.UserOwnsProductosAsync(productoId, HttpContext.GetUserId());

            if (!userOwnsProductos)
            {
                return BadRequest(new { error = "You dont own this Product" });
            }

            var producto = await _productosRepository.Get(productoId);
            producto.Nombre = productoRequest.Nombre;
            producto.Descripcion = productoRequest.Descripcion;
            producto.PrecioProducto = productoRequest.PrecioProducto;

            var productoUpdated = await _productosRepository.Update(producto);

            if (productoUpdated == null) return BadRequest();

            return Ok(productoUpdated);

        }


        [HttpDelete(ApiRoutes.Productos.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid productoId)
        {
            var producto = await _productosRepository.Get(productoId);

            if (producto == null) return NotFound();

            var eliminado = await _productosRepository.Delete(productoId);

            return Ok(new ProductosResponse 
            {
                Id = productoId,
                Nombre = eliminado.Nombre,
                Descripcion = eliminado.Descripcion,
                PrecioProducto = eliminado.PrecioProducto,
                Categoria = eliminado.Categoria,
                Marca = eliminado.Marca
            });
        }

    }
}
