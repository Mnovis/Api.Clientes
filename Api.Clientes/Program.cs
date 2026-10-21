using Api.Clientes.Entities;
using Microsoft.OpenApi;
using Microsoft.OpenApi.MicrosoftExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Configurando a geração da documentação do swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de clientes - COTI Informática",
        Version = "v1",
        Description = "API ASP.NET CORE com Dapper e SqlServer para CRUD de clientes"
    });
});

#endregion

#region Injeções de dependência

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(map => new ClienteRepository(connectionString!));

#endregion

#region Configuração do CORS

var allowedOrigins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

#endregion

var app = builder.Build();

#region Executar a documentação

app.UseSwagger();
app.UseSwaggerUI();

#endregion

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
