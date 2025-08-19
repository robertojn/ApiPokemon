
using PokemonApi.Application.Queries;
using PokemonApi.Domain.Repositories;
using PokemonApi.Infrastructure.PokeApi;
using PokemonApi.Infrastructure.Repositories;
using PokemonApi.WebApi;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetPokemonByNameHandler).Assembly));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<IPokeApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    });

builder.Services.AddScoped<IPokemonRepository, PokeApiRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


