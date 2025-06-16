//ReportController.cs
using Microsoft.AspNetCore.Mvc;
using Lab8_JulioSandoval.Services;
using Lab8_JulioSandoval.Reports;

namespace Lab8_JulioSandoval.Controllers
{
    public class ReportController : Controller
    {
        private readonly LinqService _linqService;
        private readonly ExcelReportService _reportService;

        public ReportController(LinqService linqService)
        {
            _linqService = linqService;
            _reportService = new ExcelReportService(); // Puedes inyectarlo si usas DI
        }

        // Endpoint para exportar reporte de pedidos por cliente
        [HttpGet("/report/client-orders")]
        public async Task<IActionResult> GetExportClientOrders()
        {
            // Obtener los datos usando LinqService
            var data = await _linqService.GetClientsWithProductNamesAsync();

            // Llamamos al servicio para generar el reporte
            _reportService.GetExportClientOrders(data);

            return Ok("Reporte generado en Descargas.");
        }

        // Endpoint para exportar reporte de productos distintos por cliente
        [HttpGet("/report/client-product-count")]
        public async Task<IActionResult> GetExportClientProductCounts()
        {
            // Obtener los datos usando LinqService
            var data = await _linqService.GetClientDistinctProductCountAsync();

            // Llamamos al servicio para generar el reporte
            _reportService.GetExportClientProductCounts(data);

            return Ok("Reporte generado en Descargas.");
        }
    }
    
}