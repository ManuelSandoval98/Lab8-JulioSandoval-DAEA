using Lab8_JulioSandoval.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_JulioSandoval.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController : ControllerBase
    {
        private readonly LinqService _linqService;

        public LinqController(LinqService linqService)
        {
            _linqService = linqService;
        }

        // EJERCICIO 1
        [HttpGet("clients-by-name")]
        public async Task<IActionResult> GetClientsByNameStart([FromQuery] string nameStart)
        {
            var clients = await _linqService.GetClientsByNameStartAsync(nameStart);
            return Ok(clients);
        }

        // EJERCICIO 2
        [HttpGet("products-by-min-price")]
        public async Task<IActionResult> GetProductsByPriceGreaterThan([FromQuery] decimal minPrice)
        {
            var products = await _linqService.GetProductsByPriceGreaterThanAsync(minPrice);
            return Ok(products);
        }

        // EJERCICIO 3
        [HttpGet("products-by-order")]
        public async Task<IActionResult> GetProductsByOrderId([FromQuery] int orderId)
        {
            var products = await _linqService.GetProductsByOrderIdAsync(orderId);
            return Ok(products);
        }

        // EJERCICIO 4
        [HttpGet("total-quantity-by-order")]
        public async Task<IActionResult> GetTotalQuantityByOrderId([FromQuery] int orderId)
        {
            var quantity = await _linqService.GetTotalQuantityByOrderIdAsync(orderId);
            return Ok(quantity);
        }

        // EJERCICIO 5
        [HttpGet("most-expensive-product")]
        public async Task<IActionResult> GetMostExpensiveProduct()
        {
            var product = await _linqService.GetMostExpensiveProductAsync();
            return Ok(product);
        }

        // EJERCICIO 6
        [HttpGet("orders-after-date")]
        public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime date)
        {
            var orders = await _linqService.GetOrdersAfterDateAsync(date);
            return Ok(orders);
        }

        // EJERCICIO 7
        [HttpGet("average-product-price")]
        public async Task<IActionResult> GetAverageProductPrice()
        {
            var average = await _linqService.GetAverageProductPriceAsync();
            return Ok(average);
        }

        // EJERCICIO 8
        [HttpGet("products-without-description")]
        public async Task<IActionResult> GetProductsWithoutDescription()
        {
            var products = await _linqService.GetProductsWithoutDescriptionAsync();
            return Ok(products);
        }

        // EJERCICIO 9
        [HttpGet("client-with-most-orders")]
        public async Task<IActionResult> GetClientWithMostOrders()
        {
            var client = await _linqService.GetClientWithMostOrdersAsync();
            return Ok(client);
        }

        // EJERCICIO 10
        [HttpGet("orders-with-product-details")]
        public async Task<IActionResult> GetOrdersWithProductDetails()
        {
            var details = await _linqService.GetOrdersWithProductDetailsAsync();
            return Ok(details);
        }

        // EJERCICIO 11
        [HttpGet("products-sold-to-client")]
        public async Task<IActionResult> GetProductsSoldToClient([FromQuery] int clientId)
        {
            var products = await _linqService.GetProductsSoldToClientAsync(clientId);
            return Ok(products);
        }

        // EJERCICIO 12
        [HttpGet("clients-who-bought-product")]
        public async Task<IActionResult> GetClientsWhoBoughtProduct([FromQuery] int productId)
        {
            var clients = await _linqService.GetClientsWhoBoughtProductAsync(productId);
            return Ok(clients);
        }
        
        [HttpGet("clients-with-products")]
        public async Task<IActionResult> GetClientsWithProducts()
        {
            var result = await _linqService.GetClientsWithProductNamesAsync();
            return Ok(result);
        }
        
        [HttpGet("orders-with-product-details-dto")]
        public async Task<IActionResult> GetOrderProductDetailsDto()
        {
            var result = await _linqService.GetOrderProductDetailsDtoAsync();
            return Ok(result);
        }
        
        [HttpGet("orders-with-product-details-enhanced")]
        public async Task<IActionResult> GetEnhancedOrderProductDetails()
        {
            var result = await _linqService.GetEnhancedOrderProductDetailsAsync();
            return Ok(result);
        }
        
        [HttpGet("clients-with-distinct-products")]
        public async Task<IActionResult> GetClientsWithDistinctProducts()
        {
            var result = await _linqService.GetClientDistinctProductCountAsync();
            return Ok(result);
        }
        
        [HttpGet("sales-by-client-in-month")]
        public async Task<IActionResult> GetSalesByClientInMonth([FromQuery] int year, [FromQuery] int month)
        {
            var result = await _linqService.GetSalesByClientInMonthAsync(year, month);
            return Ok(result);
        }
    }
}



















