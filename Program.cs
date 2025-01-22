var builder = WebApplication.CreateBuilder(args);

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

app.Use(async (context, next) =>
    {
        context.Response.OnStarting(() => {
            context.Response.Headers["header-secret"] = "secret";
            return Task.CompletedTask;
        });
        // request
        await next();
        // response


    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
