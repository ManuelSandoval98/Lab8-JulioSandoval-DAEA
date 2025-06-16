using Lab8_JulioSandoval.DTOs;
using Lab8_JulioSandoval.Models;
using Lab8_JulioSandoval.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Lab8_JulioSandoval.DTOs;

namespace Lab8_JulioSandoval.Services
{
    public class LinqService
    {
        private readonly LinqExampleContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LinqService(IUnitOfWork unitOfWork, LinqExampleContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // EJERCICIO 1
        public async Task<IEnumerable<client>> GetClientsByNameStartAsync(string nameStart)
        {
            var clients = await _unitOfWork.Repository<client>().GetAllAsync();
            return clients.Where(c => c.name.StartsWith(nameStart, StringComparison.OrdinalIgnoreCase));
        }

        // EJERCICIO 2
        public async Task<IEnumerable<product>> GetProductsByPriceGreaterThanAsync(decimal minPrice)
        {
            var products = await _unitOfWork.Repository<product>().GetAllAsync();
            return products.Where(p => p.price > minPrice);
        }

        // EJERCICIO 3
        public async Task<IEnumerable<object>> GetProductsByOrderIdAsync(int orderId)
        {
            var orders = await _context.orders
                .Include(o => o.product)
                .Where(o => o.orderid == orderId)
                .ToListAsync();

            return orders.Select(o => new
            {
                ProductName = o.product.name,
                OrderDate = o.orderdate
            });
        }

        // EJERCICIO 4
        public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
        {
            var orders = await _unitOfWork.Repository<order>().GetAllAsync();
            return orders.Count(o => o.orderid == orderId);
        }

        // EJERCICIO 5
        public async Task<product?> GetMostExpensiveProductAsync()
        {
            var products = await _unitOfWork.Repository<product>().GetAllAsync();
            return products.OrderByDescending(p => p.price).FirstOrDefault();
        }

        // EJERCICIO 6
        public async Task<IEnumerable<order>> GetOrdersAfterDateAsync(DateTime date)
        {
            var orders = await _unitOfWork.Repository<order>().GetAllAsync();
            return orders.Where(o => o.orderdate > date);
        }

        // EJERCICIO 7
        public async Task<decimal> GetAverageProductPriceAsync()
        {
            var products = await _unitOfWork.Repository<product>().GetAllAsync();
            return products.Any() ? products.Average(p => p.price) : 0;
        }

        // EJERCICIO 8
        public async Task<IEnumerable<product>> GetProductsWithoutDescriptionAsync()
        {
            var products = await _unitOfWork.Repository<product>().GetAllAsync();
            return products.Where(p => string.IsNullOrEmpty(p.name));
        }

        // EJERCICIO 9
        public async Task<object?> GetClientWithMostOrdersAsync()
        {
            var orders = await _unitOfWork.Repository<order>().GetAllAsync();

            var query = orders
                .GroupBy(o => o.clientid)
                .Select(g => new
                {
                    ClientId = g.Key,
                    OrdersCount = g.Count()
                })
                .OrderByDescending(x => x.OrdersCount)
                .FirstOrDefault();

            return query;
        }

        // EJERCICIO 10
        public async Task<IEnumerable<object>> GetOrdersWithProductDetailsAsync()
        {
            var orders = await _context.orders
                .Include(o => o.product)
                .ToListAsync();

            return orders.Select(o => new
            {
                OrderId = o.orderid,
                ProductName = o.product.name,
                Quantity = 1 // Asumimos 1 porque no existe campo Quantity
            });
        }

        // EJERCICIO 11
        public async Task<IEnumerable<object>> GetProductsSoldToClientAsync(int clientId)
        {
            var orders = await _context.orders
                .Include(o => o.product)
                .Where(o => o.clientid == clientId)
                .ToListAsync();

            return orders.Select(o => new
            {
                ProductName = o.product.name,
                OrderDate = o.orderdate
            });
        }

        // EJERCICIO 12
        public async Task<IEnumerable<object>> GetClientsWhoBoughtProductAsync(int productId)
        {
            var orders = await _context.orders
                .Include(o => o.client)
                .Where(o => o.productid == productId)
                .ToListAsync();

            return orders.Select(o => new
            {
                ClientName = o.client.name,
                OrderDate = o.orderdate
            });
        }
        
        // PROPUESTA DE MEJORA - Clientes con productos comprados
        public async Task<IEnumerable<ClientProductsDto>> GetClientsWithProductNamesAsync()
        {
            return await _context.clients
                .AsNoTracking()
                .Include(c => c.orders)
                .ThenInclude(o => o.product)
                .Select(c => new ClientProductsDto
                {
                    ClientName = c.name,
                    Products = c.orders
                        .Select(o => o.product.name)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<OrderDetailsDto>> GetOrderProductDetailsDtoAsync()
        {
            return await _context.orders
                .AsNoTracking()
                .Include(o => o.product)
                .Select(order => new OrderDetailsDto
                {
                    OrderId = order.orderid,
                    OrderDate = order.orderdate,
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            ProductName = order.product.name,
                            Quantity = 1,
                            Price = order.product.price
                        }
                    }
                }).ToListAsync();
        }
        
        public async Task<IEnumerable<EnhancedOrderDetailsDto>> GetEnhancedOrderProductDetailsAsync()
        {
            return await _context.orders
                .AsNoTracking()
                .Include(o => o.product)
                .GroupBy(o => o.orderid)
                .Select(g => new EnhancedOrderDetailsDto
                {
                    OrderId = g.Key,
                    OrderDate = g.First().orderdate,
                    Products = g.Select(o => new EnhancedProductDto
                    {
                        ProductName = o.product.name,
                        Quantity = 1,
                        Price = o.product.price
                    }).ToList()
                }).ToListAsync();
        }
        
        // PASO 4 - Versión mejorada: productos distintos por cliente
        public async Task<IEnumerable<ClientProductCountDto>> GetClientDistinctProductCountAsync()
        {
            return await _context.clients
                .AsNoTracking()
                .Select(client => new ClientProductCountDto
                {
                    ClientName = client.name,
                    TotalProducts = client.orders
                        .Select(o => o.productid)
                        .Distinct()
                        .Count()
                })
                .ToListAsync();
        }
        
        // PASO 5 - Versión mejorada: ventas por cliente en un mes
        public async Task<IEnumerable<SalesByClientDto>> GetSalesByClientInMonthAsync(int year, int month)
        {
            return await _context.orders
                .AsNoTracking()
                .Include(o => o.client)
                .Include(o => o.product)
                .Where(o => o.orderdate.Year == year && o.orderdate.Month == month)
                .GroupBy(o => o.clientid)
                .Select(group => new SalesByClientDto
                {
                    ClientName = group.First().client.name,
                    TotalSales = group.Sum(order => order.product.price) // Asume 1 producto
                })
                .OrderByDescending(s => s.TotalSales)
                .ToListAsync();
        }
    }
}








