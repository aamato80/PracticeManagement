
using DossierManagement.Api.Attachments;
using DossierManagement.Dal.Repositories;
using DossierManagement.Api.Services;
using DossierManagement.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DossierManagement.Api;
using Serilog;
using FluentValidation;
using System;
using DossierManagement.Api.DTOs;
using DossierManagement.Api.Validators;
using DossierManagement.Api.CustomHttpClient;
using FluentMigrator.Runner;
using System.Reflection;
using System.Xml.Linq;
using DossierManagement.Dal.Migrations;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, logConf) => logConf.WriteTo.Console());


builder.Services.AddScoped<IUnitOfWork>(context =>
{
    var connstring = builder.Configuration.GetConnectionString(AppConfigConst.SqlConnString);
    return new SQLUnitOfWork(connstring);
});

builder.Services.AddTransient<IDossierService, DossierService>();
builder.Services.AddTransient<IAttachmentManager>(context =>
{
    var path = builder.Configuration.GetValue<string>(AppConfigConst.FileSaverPath);
    return new LocalAttachmentManager(path);
});
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IValidator<DossierDto>, DossierDtoValidator>();
builder.Services.AddSingleton<ICustomHttpClient, CustomHttpClient>();
builder.Services.AddTransient<IChangeNotifier, ChangeNotifier>();


builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(context =>
{
    context.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    context.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>(AppConfigConst.JwtKey));
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

builder.Services.AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddSqlServer2016()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString(AppConfigConst.MasterConnString))
        .ScanIn(typeof(InitialMigration).Assembly).For.Migrations());

builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

 
void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var configuration = scope.ServiceProvider.GetService<IConfiguration>();
    Database.EnsureDatabase(configuration.GetConnectionString(AppConfigConst.MasterConnString), "DossierManagement");
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    migrator?.MigrateUp();
}

ApplyMigrations(app);

app.Run();
