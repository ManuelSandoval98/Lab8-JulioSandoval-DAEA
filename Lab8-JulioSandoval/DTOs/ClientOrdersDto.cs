// DTOs/ClientProductsDto.cs
namespace Lab8_JulioSandoval.DTOs
{
    public class ClientProductsDto
    {
        public string ClientName { get; set; } = "";
        public List<string> Products { get; set; } = new();
        
        // Nueva propiedad que calcula el total de productos
        public int TotalProducts => Products.Count;
    }
}