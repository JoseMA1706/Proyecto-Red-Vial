using Proyecto1.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Proyecto1.Services
{
    public class PdfHistorialService
    {
        public byte[] GenerarHistorialPdf(ListaReportes reportes)
        {
            var fecha = DateTime.Now;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Historial de Reportes - {fecha:dd/MM/yyyy HH:mm}")
                            .Bold().FontSize(16);
                        col.Item().Text(" ");

                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // ID
                                columns.RelativeColumn(2); // Fecha
                                columns.RelativeColumn(2); // Intersección
                                columns.RelativeColumn(3); // Cuellos
                                columns.RelativeColumn(4); // Nodos
                            });

                            t.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID");
                                header.Cell().Element(CellStyle).Text("Fecha");
                                header.Cell().Element(CellStyle).Text("Intersección Congestionada");
                                header.Cell().Element(CellStyle).Text("Cuellos de Botella");
                                header.Cell().Element(CellStyle).Text("Nodos");

                                static IContainer CellStyle(IContainer container) =>
                                    container.Padding(5).Background(Colors.Grey.Lighten2).Border(1).BorderColor(Colors.Grey.Darken1);
                            });

                            foreach (var reporte in reportes.ObtenerTodos())
                            {
                                t.Cell().Element(CellStyle).Text(reporte.Id.ToString());
                                t.Cell().Element(CellStyle).Text(reporte.FechaGeneracion.ToString("dd/MM/yyyy HH:mm"));
                                t.Cell().Element(CellStyle).Text(reporte.InterseccionMasCongestionada);
                                t.Cell().Element(CellStyle).Text(reporte.CuellosBotellaTexto);
                                t.Cell().Element(CellStyle).Text(reporte.NodosTexto);

                                static IContainer CellStyle(IContainer container) =>
                                    container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5);
                            }
                        });
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}

