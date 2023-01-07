using System.Reflection;
using System.Security.Claims;
using System.Text;
using _2___CadastroAlunos.Domain.Notification;
using ApiCadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.Repositories;
using ApiCadastroAlunos.ViewModel;
using AutoMapper;
using CadastroAlunos.Core.DTOs;
using CadastroAlunos.Core.Interfaces;
using CadastroAlunos.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.
NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling 
    = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

#region Documentar o swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Cadastro de Alunos/Professores",
        Version = "V1",
        Contact = new OpenApiContact
        {
            Name = "Email",
            Email = "lucas.carvalho@clear.sale"
        }
    });

    //serve para documentar controladores (adiconar mensagem/descricao)

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);


    // Documentar o token no swaager
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",

        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espaço] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
    });

    c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });

});

#endregion

#region registrar serviços
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))).AddScoped<AppDbContext>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<INotificationContext, NotificationContext>();
#endregion


#region Autenticação e Autorização

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("School", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role,
         "Admin", "Member"
        );
    });
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Member", policy =>
//    {
//        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireRole("Member");
//    });

//    options.AddPolicy("Admin", policy =>
//    {
//        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireRole("Admin");
//    });
//});


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
            ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(
           Encoding.ASCII.GetBytes(builder.Configuration["Jwt:key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

#endregion

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<AlunoViewModel, Aluno>().ReverseMap();
    cfg.CreateMap<ResultViewModel, Aluno>().ReverseMap();
    cfg.CreateMap<UserDTO, IdentityUser>().ReverseMap();
    //cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

var app = builder.Build();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
