using Lab8_JulioSandoval.Models;
using Lab8_JulioSandoval.UnitOfWork;
using Lab8_JulioSandoval.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar conexión a PostgreSQL desde appsettings.json o variables de entorno (Render)
builder.Services.AddDbContext<LinqExampleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyectar servicios de negocio
builder.Services.AddScoped<LinqService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Verificar si la conexión a la base de datos funciona
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LinqExampleContext>();
    try
    {
        db.Database.CanConnect();
        Console.WriteLine("✅ Conexión a la base de datos establecida correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al conectar con la base de datos: {ex.Message}");
    }
}

// Configurar el middleware HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets(); // Esto aplica si tienes assets como CSS/JS

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();