using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURAÇÃO DO CONTEXTO ORACLE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// INJEÇÃO DE DEPENDÊNCIAS - SERVICES
builder.Services.AddScoped<InterfaceEndereco, ServiceEndereco>();
builder.Services.AddScoped<InterfaceNotaFiscal, ServiceNotaFiscal>();
builder.Services.AddScoped<InterfaceStatusMoto, ServiceStatusMoto>();
builder.Services.AddScoped<InterfaceStatusOperacao, ServiceStatusOperacao>();
builder.Services.AddScoped<InterfaceTipoMoto, ServiceTipoMoto>();
builder.Services.AddScoped<InterfaceFilial, ServiceFilial>();
builder.Services.AddScoped<InterfacePatio, ServicePatio>();
builder.Services.AddScoped<InterfaceZonaPatio, ServiceZonaPatio>();
builder.Services.AddScoped<InterfaceMoto, ServiceMoto>();
builder.Services.AddScoped<InterfaceMotociclista, ServiceMotociclista>();
builder.Services.AddScoped<InterfaceLocalizacaoMoto, ServiceLocalizacaoMoto>();
builder.Services.AddScoped<InterfaceHistoricoLocalizacao, ServiceHistoricoLocalizacao>();

// CONTROLLERS E SWAGGER
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ATIVA SWAGGER EM DESENVOLVIMENTO
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

