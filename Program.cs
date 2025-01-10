using Pokemon.Api.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pokemon.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPokemonService, PokemonService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use global exception handler for APIs (Optional)
app.UseExceptionHandler("/error");

// Middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map controllers (API endpoints)
app.MapControllers();

app.Run();
