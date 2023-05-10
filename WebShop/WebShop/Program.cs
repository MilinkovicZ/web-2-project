using Microsoft.EntityFrameworkCore;
using WebShop.Context;
using WebShop.Interfaces;
using WebShop.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebShopDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WebShopDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, WebShopDBContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
