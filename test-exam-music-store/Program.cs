using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeHandler.Client.MongoDb;
using RecipeHandler.Infrastructure.MongoDbPersistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<RecipesRepository>();

var assemblies = new[]
{
    typeof(RecipeHandler.AssemblyMarker).Assembly,
    typeof(RecipeHandler.Infrastructure.AssemblyMarker).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SongStore.API v1"); });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
