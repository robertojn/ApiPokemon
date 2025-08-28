
using PokemonApi.Application.Queries;
using PokemonApi.Domain.Repositories;
using PokemonApi.Infrastructure.PokeApi;
using PokemonApi.Infrastructure.Repositories;
using PokemonApi.WebApi;
using PokemonApi.WebApi.Endpoints;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetPokemonByNameHandler).Assembly));
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

//Minimal
app.MapPokemonEndpoints();

app.Run();


