using ContactManagementAPI.Data;
using ContactManagementAPI.Data.Repositories.Implementations;
using ContactManagementAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ContactManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContactManagmentConnection")));

// Register repositories
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IFundContactRepository, FundContactRepository>();
builder.Services.AddScoped<IContactsManagmentService, ContactsManagmentService>();

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
