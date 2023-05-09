using Microsoft.EntityFrameworkCore;
using WebShop.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebShopDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WebShopDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
