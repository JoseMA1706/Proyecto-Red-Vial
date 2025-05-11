using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;
using Proyecto1.Services;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<RedVialContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Servicios personalizados
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<RedVial>();
builder.Services.AddScoped<PdfGeneratorService>();
builder.Services.AddScoped<PdfHistorialService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapGet("/descargar-reporte", async (HttpContext http, PdfGeneratorService pdf, RedVialContext db) =>
{
    var nodos = new ListaNodos();
    var mapa = new Dictionary<string, Nodo>();

    foreach (var n in db.Nodos)
    {
        nodos.Agregar(n);
        mapa[n.Id] = n;
    }

    foreach (var n in nodos.ObtenerTodos())
    {
        if (!string.IsNullOrEmpty(n.IdNorte) && mapa.ContainsKey(n.IdNorte)) n.Norte = mapa[n.IdNorte];
        if (!string.IsNullOrEmpty(n.IdSur) && mapa.ContainsKey(n.IdSur)) n.Sur = mapa[n.IdSur];
        if (!string.IsNullOrEmpty(n.IdEste) && mapa.ContainsKey(n.IdEste)) n.Este = mapa[n.IdEste];
        if (!string.IsNullOrEmpty(n.IdOeste) && mapa.ContainsKey(n.IdOeste)) n.Oeste = mapa[n.IdOeste];
    }

    Nodo? masCongestionado = null;
    var cuellos = new ListaNodos();

    foreach (var n in nodos.ObtenerTodos())
    {
        if (masCongestionado == null || n.VehiculosEnEspera > masCongestionado.VehiculosEnEspera)
            masCongestionado = n;

        if (n.VehiculosEnEspera >= 10 && n.EstadoSemaforo == "Rojo" && n.TiempoPromedioCruce >= 30)
            cuellos.Agregar(n);
    }

    var contenido = pdf.GenerarPdf(nodos, masCongestionado, cuellos);

    var nombreArchivo = $"reporte-redvial-{DateTime.Now:yyyy-MM-dd_HH-mm}.pdf";
    http.Response.Headers.ContentDisposition = $"attachment; filename={nombreArchivo}";
    http.Response.ContentType = "application/pdf";
    await http.Response.Body.WriteAsync(contenido);
});


app.MapGet("/descargar-historial", async (HttpContext http, PdfHistorialService pdf, RedVialContext db) =>
{
    var reportesDb = db.Reportes.OrderBy(r => r.FechaGeneracion);
    var historial = new ListaReportes();

    foreach (var r in reportesDb)
        historial.Agregar(r);

    var contenido = pdf.GenerarHistorialPdf(historial);

    var nombreArchivo = $"historial-reportes-{DateTime.Now:yyyy-MM-dd_HH-mm}.pdf";
    http.Response.Headers.ContentDisposition = $"attachment; filename={nombreArchivo}";
    http.Response.ContentType = "application/pdf";
    await http.Response.Body.WriteAsync(contenido);
});

app.Run();
