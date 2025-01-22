# Custom-Middlewares-in-.NET


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
  ### NOT RECOMMENDED!
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
