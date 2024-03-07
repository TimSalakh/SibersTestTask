using DAL.Contexts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var congiguration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompanyDbContext>(
    options =>
    {
        options.UseSqlServer(congiguration
            .GetConnectionString(nameof(CompanyDbContext)));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    dbContext.Database.EnsureCreated(); // Only in development, consider migrations in production
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
