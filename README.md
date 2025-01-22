# Custom-Middleware:
app.Use(async (context, next) =>
    {
        // request
        await next();
        // response
    });

  ## Before next() 
  - Process, manipulate or apply some business logic to upcoming REQUEST

   ## After next()
   - Process, manipulate or apply some business logic to RESPONSE
  #### NOT RECOMMENDED!
  -  To interact with response if it has already been sent to user.. 
   So, When to write the logic after next()? When you are not going to change the response, rather doing some logging mechanism, or applying some logic that is related to response but not directly changing the response..
 ```
app.Use(async (context, next) =>
    {

        await next();

        // Two NOT RECOMMENDED approaches:
        context.Response.Headers["header-secret"] = "secret";

        if (!context.Response.HasStarted)
        {
            context.Response.Headers["header-secret"] = "secret";
        }

    });
```

  #### RECOMMENDED!

 ```
app.Use(async (context, next) =>
    {
    // callbacks registered in OnStarting runs in reverse order (i.e control will come on this callback after coming back from controller)
   context.Response.OnStarting(() => {
     context.Response.Headers["header-secret"] = "secret";
     return Task.CompletedTask;
 });
        await next();

    });
```

## Terminal middleware
- Middleware that terminates the application i.e this is the last middleware that will be executed in program.cs (just like app.Run() is our last middleware in our Program.cs

- Not using next()

  OR below code terminates the execution in program.cs

app.Run(async (context) =>
{
	await context.Response.WriteAsync("this is a short-circuit middleware / Terminal middleware");
});
