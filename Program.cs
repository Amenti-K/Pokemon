var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Use global exception handler for APIs (Optional)
app.UseExceptionHandler("/error");

// Middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map controllers (API endpoints)
app.MapControllers();

app.Run();
