using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project.Api.Api.DependencyMap;
using Project.Api.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// 🧩 Dependency Injection
builder.Services.ServiceMap(builder.Configuration);
builder.Services.RepositoryMap();

// 💾 DataBase
builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🎯 MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Project.Api.Application"));

// 📌 Other Services
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddAutoMapperSetup();
builder.Services.AddHttpContextAccessor();

// 📄 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.Api", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// 🧱 Database creation
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.EnsureCreated();
}

// 🌐 Pipeline de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.Api.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// ❌ No authentication

app.MapControllers();

app.Run();