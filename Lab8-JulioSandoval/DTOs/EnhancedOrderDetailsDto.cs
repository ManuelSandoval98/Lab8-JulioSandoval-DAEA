// DTOs/EnhancedOrderDetailsDto.cs
namespace Lab8_JulioSandoval.DTOs
{
    public class EnhancedOrderDetailsDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<EnhancedProductDto> Products { get; set; } = new();
    }

    public class EnhancedProductDto
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}