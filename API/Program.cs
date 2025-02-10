using AutoMapper;
using API.Mapping; // Ajuste o namespace conforme necessário

var builder = WebApplication.CreateBuilder(args);

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Registra o perfil de mapeamento

// Outros serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();