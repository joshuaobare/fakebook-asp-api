using fakebook_asp_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Dependency injection for DB configured 
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("FakebookDatabase"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
