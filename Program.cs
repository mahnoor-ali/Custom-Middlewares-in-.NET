using Middlewares.Middlewares;

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
        // Manipulate headers in Response (not good approach - just an example of usage)
        context.Response.OnStarting(() => {
            context.Response.Headers["header-secret"] = "secret";
            return Task.CompletedTask;
        });

        await next();
    });


if (builder.Configuration.GetValue<bool>("MiddlewareSettings:EnableIpLogging"))
{
    app.UseIPLoggingMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
