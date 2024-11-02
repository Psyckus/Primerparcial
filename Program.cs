using Microsoft.EntityFrameworkCore;
using Parcial_1.Controllers;
using Parcial_1.Data;
using Parcial_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<CaracteristicaProductoService>();
builder.Services.AddHttpClient<CategoriaProductoService>();
builder.Services.AddHttpClient<ClienteService>();
builder.Services.AddHttpClient<EstadoOrdenService>();
builder.Services.AddHttpClient<ItemOrdenService>();
builder.Services.AddHttpClient<ItemProductoService>();

builder.Services.AddHttpClient<MetodoEnvioService>();
builder.Services.AddHttpClient<MetodoPagoService>();
builder.Services.AddHttpClient<OpcionCaracteristicaService>();
builder.Services.AddHttpClient<OrdenCompraService>();
builder.Services.AddHttpClient<PaisService>();
builder.Services.AddHttpClient<ProductoService>();
builder.Services.AddHttpClient<PromocionService>();
builder.Services.AddHttpClient<ResenaService>();
builder.Services.AddHttpClient<TipoPagoService>();
// Habilitar CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuración de Swagger (opcional)
builder.Services.AddSwaggerGen();

// Configuración de la conexión a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Inyecta HttpClient para CaracteristicaProductoController
builder.Services.AddHttpClient<CaracteristicaProductoController>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5177/"); // Cambia esto por la URL base de tu API
});

var app = builder.Build();

// Configuración del pipeline de la solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); // Habilitar rutas para los controladores API

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
