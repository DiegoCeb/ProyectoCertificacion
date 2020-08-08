namespace App.API.Contracts.V1.Requests
{
    public class CreateProductoRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioProducto { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
    }
}
