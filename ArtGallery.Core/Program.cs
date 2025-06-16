using ArtGallery.Data;
using ArtGallery.Interfaces.Repositories;
using ArtGallery.Interfaces.ServicesInterfaces;
using ArtGallery.Repositories.Repositories;
using ArtGallery.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Чтение строки подключения и настройка DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
}

builder.Services.AddDbContext<GalleryDbContext>(options =>
    options.UseNpgsql(connectionString)
           .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

// Регистрация сервисов
builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Art Gallery API", Version = "v1" });
});

// Регистрация репозиториев
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPainterRepository, PainterRepository>();
builder.Services.AddScoped<IPaintingRepository, PaintingRepository>();
builder.Services.AddScoped<ICounterpartyRepository, CounterpartyRepository>();
builder.Services.AddScoped<IExpenseArticleRepository, ExpenseArticleRepository>();
builder.Services.AddScoped<IMoneyExpenseRepository, MoneyExpenseRepository>();
builder.Services.AddScoped<IMoneyIncomeRepository, MoneyIncomeRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IPaintingMovementRepository, PaintingMovementRepository>();

// Регистрация сервисов
builder.Services.AddScoped<IPainterService>(provider =>
    new PainterService(
        provider.GetRequiredService<IPainterRepository>(),
        provider.GetRequiredService<GalleryDbContext>()
    ));

builder.Services.AddScoped<IPaintingService>(provider =>
    new PaintingService(
        provider.GetRequiredService<IPaintingRepository>(),
        provider.GetRequiredService<GalleryDbContext>()
    ));

builder.Services.AddScoped<IRentalService>(provider =>
    new RentalService(
        provider.GetRequiredService<IRentalRepository>(),
        provider.GetRequiredService<GalleryDbContext>()
    ));

builder.Services.AddScoped<ICounterpartyService>(provider =>
    new CounterpartyService(
        provider.GetRequiredService<ICounterpartyRepository>(),
        provider.GetRequiredService<GalleryDbContext>()
    ));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Art Gallery API v1"));
}
else
{
    app.UseExceptionHandler("/error"); 
    app.UseHsts(); 
}

app.UseHttpsRedirection(); 
app.UseRouting();
app.UseAuthorization(); 
app.MapControllers();

// Перенаправление с корневого URL на Swagger в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

app.Run();