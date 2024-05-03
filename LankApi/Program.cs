using Domain.Core.Entities;
using Domain.Core.Repository;
using Domain.Core.Services;
using Infra.Data;
using Infra.Data.Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<LankContext>();

builder.Services.AddCors();

var app = builder.Build();



// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();




app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
       .AllowAnyMethod()
          .AllowAnyHeader());

app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", () =>
{

    return Results.Ok("Hello world");
}).WithTags("Hello world");

app.MapPost("/notas", async (Notas notas, IEmailService _emailService) =>
{
    await _emailService.SendEmailAsync(notas);
    return notas;
}).WithTags("Notas");

app.MapPost("/clientes", async (Cliente cliente, IClienteRepository clienteRepository) =>
{
    await clienteRepository.CreateAsync(cliente);
    return cliente;
}).WithTags("Clientes");

app.MapDelete("/clientes/{id}", async (IClienteRepository ClienteRepository, string id) =>
{
    var isSuccess = await ClienteRepository.DeleteAsync(id);
    return isSuccess ? Results.NoContent() : Results.BadRequest("Ocorreu um erro ao deletar o cliente");
    
}).WithTags("Clientes");

app.MapGet("/clientes", async (IClienteRepository clienteRepository) =>
{
    
    return await clienteRepository.GetAllAsync();
}).WithTags("Clientes");



app.Run();

