using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Services;
using MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection (VQPhong - 14/07/2022)
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodGroupRepository, FoodGroupRepository>();
builder.Services.AddScoped<IFoodPlaceRepository, FoodPlaceRepository>();
builder.Services.AddScoped<IFoodUnitRepository, FoodUnitRepository>();
builder.Services.AddScoped<IFavorServiceRepository, FavorServiceRepository>();
builder.Services.AddScoped<IFoodFavorServiceRepository, FoodFavorServiceRepository>();
builder.Services.AddScoped<IFoodServices, FoodServices>();
builder.Services.AddScoped<IFoodGroupServices, FoodGroupServices>();
builder.Services.AddScoped<IFoodPlaceServices, FoodPlaceServices>();
builder.Services.AddScoped<IFoodUnitServices, FoodUnitServices>();
builder.Services.AddScoped<IFavorServiceServices, FavorServiceServices>();
builder.Services.AddScoped<IFoodFavorServiceServices, FoodFavorServiceServices>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));

// CORS Policy (VQPhong - 14/07/2022)
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

// Newtonsoft JSON (VQPhong - 14/07/2022)
builder.Services.AddMvc().AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// CORS Policy (VQPhong - 14/07/2022)
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.Run();
