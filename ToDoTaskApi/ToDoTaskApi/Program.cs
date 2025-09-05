using ToDoTaskApi.Application;
using ToDoTaskApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add infrastructure and application layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configure controllers to use JSON serialization and add a converter
// that allows enums to be serialized and deserialized as strings instead of integers.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Add support for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Enable Swagger UI only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
