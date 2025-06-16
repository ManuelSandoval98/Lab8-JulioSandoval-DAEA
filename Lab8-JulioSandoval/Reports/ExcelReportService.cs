//ExcelReportService.cs
using ClosedXML.Excel;
using System.IO;
using Lab8_JulioSandoval.DTOs;

namespace Lab8_JulioSandoval.Reports
{
    public class ExcelReportService
    {
        private readonly string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        // Método para exportar pedidos por cliente
        public void GetExportClientOrders(IEnumerable<ClientProductsDto> data)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Pedidos por Cliente");

            sheet.Cell(1, 1).Value = "Cliente";
            sheet.Cell(1, 2).Value = "Total de Pedidos";

            for (int i = 0; i < data.Count(); i++)
            {
                sheet.Cell(i + 2, 1).Value = data.ElementAt(i).ClientName;
                sheet.Cell(i + 2, 2).Value = data.ElementAt(i).TotalProducts;
            }

            workbook.SaveAs(Path.Combine(downloadsPath, "Reporte_PedidosCliente.xlsx"));
        }

        // Método para exportar productos por cliente
        public void GetExportClientProductCounts(IEnumerable<ClientProductCountDto> data)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Productos por Cliente");

            sheet.Cell(1, 1).Value = "Cliente";
            sheet.Cell(1, 2).Value = "Producto";
            sheet.Cell(1, 3).Value = "Cantidad";

            for (int i = 0; i < data.Count(); i++)
            {
                sheet.Cell(i + 2, 1).Value = data.ElementAt(i).ClientName;
                sheet.Cell(i + 2, 2).Value = data.ElementAt(i).TotalProducts;
            }

            workbook.SaveAs(Path.Combine(downloadsPath, "Reporte_ProductosCliente.xlsx"));
        }
    }
}