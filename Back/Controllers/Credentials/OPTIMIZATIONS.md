# Shit that is terrible and I have to Optimize

This block can be moved to a parent class within the controllers, as it's a general response to getting an empty array.<br />

```cs
if (result is null)
{
    return NotFound();
}
return Ok(result);
```

Move the connector to the Driver method and pass the connection to the methods that use it, rather than create a new connection for every function.
Close the Connection once you're done with it.
Do this by creating a containing class for the Connection that stores the Connection object as well as the IMongoCollection so we can avoid having to jump around collections unless we need to.