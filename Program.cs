using Microsoft.EntityFrameworkCore;
using SistemaBoletos.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddControllersWithViews();

// Configuraci�n de la base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuraci�n de sesi�n (opcional pero �til)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Si usas sesi�n

// Configuraci�n de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ventas}/{action=Index}/{id?}");

// Inicializaci�n de la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Aplicar migraciones autom�ticamente
        context.Database.Migrate();

        // Verificar y crear cooperativas si no existen
        if (!context.Cooperativas.Any())
        {
            context.Cooperativas.AddRange(
                new Cooperativa { Id = 1, Nombre = "Imbaburapac", CapacidadMaxima = 45 },
                new Cooperativa { Id = 2, Nombre = "Lagos", CapacidadMaxima = int.MaxValue }
            );
            await context.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error durante la inicializaci�n de la base de datos");
    }
}

app.Run();