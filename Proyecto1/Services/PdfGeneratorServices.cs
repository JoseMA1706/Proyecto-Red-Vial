using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Proyecto1.Models;

namespace Proyecto1.Services
{
    public class PdfGeneratorService
    {
        public byte[] GenerarPdf(ListaNodos nodos, Nodo? masCongestionado, ListaNodos cuellos)
        {
            var fecha = DateTime.Now;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Reporte de Red Vial - {fecha:dd/MM/yyyy HH:mm}").Bold().FontSize(16);
                        col.Item().Text(" ");

                        col.Item().Text("Intersección más congestionada:").Bold();
                        if (masCongestionado != null)
                            col.Item().Text($"{masCongestionado.Id} con {masCongestionado.VehiculosEnEspera} vehículos");

                        col.Item().Text(" ");

                        col.Item().Text("Intersecciones con Cuello de Botella:").Bold();
                        if (!cuellos.EstaVacia())
                        {
                            foreach (var n in cuellos.ObtenerTodos())
                            {
                                col.Item().Text($"- {n.Id}: {n.VehiculosEnEspera} vehículos, {n.EstadoSemaforo}, {n.TiempoPromedioCruce}s");
                            }
                        }
                        else
                        {
                            col.Item().Text("Ninguna.");
                        }

                        col.Item().Text(" ");
                        col.Item().Text("Tabla de Intersecciones:").Bold();

                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                for (int i = 0; i < 8; i++) c.RelativeColumn();
                            });

                            t.Header(h =>
                            {
                                h.Cell().Element(CellStyle).Text("ID");
                                h.Cell().Element(CellStyle).Text("Vehículos");
                                h.Cell().Element(CellStyle).Text("Semáforo");
                                h.Cell().Element(CellStyle).Text("Tiempo");
                                h.Cell().Element(CellStyle).Text("Norte");
                                h.Cell().Element(CellStyle).Text("Sur");
                                h.Cell().Element(CellStyle).Text("Este");
                                h.Cell().Element(CellStyle).Text("Oeste");

                                static IContainer CellStyle(IContainer container) =>
                                    container.Padding(5).Background(Colors.Grey.Lighten2).Border(1).BorderColor(Colors.Grey.Darken1);
                            });

                            foreach (var nodo in nodos.ObtenerTodos())
                            {
                                t.Cell().Element(CellStyle).Text(nodo.Id);
                                t.Cell().Element(CellStyle).Text(nodo.VehiculosEnEspera.ToString());
                                t.Cell().Element(CellStyle).Text(nodo.EstadoSemaforo);
                                t.Cell().Element(CellStyle).Text(nodo.TiempoPromedioCruce.ToString());
                                t.Cell().Element(CellStyle).Text(nodo.Norte?.Id ?? "-");
                                t.Cell().Element(CellStyle).Text(nodo.Sur?.Id ?? "-");
                                t.Cell().Element(CellStyle).Text(nodo.Este?.Id ?? "-");
                                t.Cell().Element(CellStyle).Text(nodo.Oeste?.Id ?? "-");

                                static IContainer CellStyle(IContainer container) =>
                                    container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5);
                            }
                        });
                    });
                });
            });

            return documento.GeneratePdf();
        }
    }
}

