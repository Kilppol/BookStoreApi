using BookStoreApi.Models;
using BookStoreApi.Services;
var builder = WebApplication.CreateBuilder(args);

//DataBase connection 
    builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
    builder.Services.AddSingleton<BooksService>();
    builder.Services.AddSingleton<CoberturaService>();


// Add services to the container.

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

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/",()=> "Hello World!");

app.MapGet("/Test",async context =>{
    //logger.LogInformation("Testing logging in Program.cs");
    await context.Response.WriteAsync("Testing");
});

app.Run();
