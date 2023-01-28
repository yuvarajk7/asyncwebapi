using Books.API.DbContexts;
using Books.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Throttle the thread pool (set available threads to amount of processors)
ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// register the DbContext on the container 
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:BooksDBConnectionString"]));

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
