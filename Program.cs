using DevEvents.API.Entities;
using DevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddSingleton<EventosDbContext>();
builder.Services.AddDbContext<EventosDbContext>(
    o => o.UseInMemoryDatabase("DevEventosDb")
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevEvents API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Pedro",
            Email = "pdro5k@outlook.com",
            Url = new Uri("https://github.com/pedroferreiiraa")
        }
    });
    
    
    // Pega o caminho do arquivo XML que foi gerado
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Inclui o arquivo XML para o Swagger
    options.IncludeXmlComments(xmlPath);
});

// builder.Services.AddDbContext<EventosDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
// );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

