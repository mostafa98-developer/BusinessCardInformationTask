using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.IRepository;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Validators;
using BusinessCardInformation.Infrastructure.Data;
using BusinessCardInformation.Infrastructure.Repositories;
using BusinessCardInformation.Infrastructure.Services;
using BusinessCardInformation.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<BusinessCardValidator>());


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
    builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Allow Angular dev server
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Optional if you're using credentials (cookies, etc.)
    });
});

// Register DbContext for SQL Server
builder.Services.AddDbContext<BusinessCardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BusinessCardInformation")));

// Register generic repository and services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBusinessCardRepository), typeof(BusinessCardRepository));

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IBusinessCardService, BusinessCardService>();

// Register BusinessCardService specifically
builder.Services.AddScoped<BusinessCardService>();

var app = builder.Build();
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();


//// Enable Swagger for all environments
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//    options.RoutePrefix = string.Empty; // Makes Swagger UI the default page
//});

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();

