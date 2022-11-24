
using PracticeManagement.Api.Attachments;
using PracticeManagement.Dal.Repositories;
using PracticeManagement.Api.Services;
using PracticeManagement.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PracticeManagement.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context,logConf)=> logConf.WriteTo.Console());


builder.Services.AddScoped<IUnitOfWork>(context =>
{
    var connstring = builder.Configuration.GetConnectionString(AppConfigConst.SqlConnString);
    return new SQLUnitOfWork(connstring);
});

builder.Services.AddTransient<IPracticeService, PracticeService>();
builder.Services.AddTransient<IAttachmentManager>(context =>
{
    var path = builder.Configuration.GetValue<string>(AppConfigConst.FileSaverPath);
    return new LocalAttachmentManager(path);
});
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddControllers();

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

app.Run();
