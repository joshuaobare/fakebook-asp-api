using fakebook_asp_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Dependency injection for DB configured 
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("FakebookDatabase"));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});
// The Service classes are registered with DI to support construcor injection in consuming classes
// MongoClient must be registered in DI with a singleton service lifetime
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<CommentService>();
builder.Services.AddSingleton<UserService>();

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
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
