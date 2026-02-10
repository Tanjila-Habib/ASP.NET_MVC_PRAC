using IntroCFAPI.EF.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NC_DbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
