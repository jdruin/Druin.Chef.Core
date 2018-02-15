# Druin.Chef.Core

This is an update to the code maintained at https://github.com/lholman/dotnet-chef-api by Lloyd Holman.

I have used this code and its predecesor for over a year but needed to extend it for the environments I work in.  

This is just a core that allows you to make raw connections to the Chef Server including the Chef Bookshelf.

The usage is simple. 

```csharp

var conn = new ChefConnection()
            {
                PrivateKey = privateKey,
                UserId = userId,
                ChefServer = "https://api.opscode.com:443"
            }


var request = new Requester(conn);

var request = Requester.GetRequestAsync("/organizations/test/roles");

/// The Requesting methods are Async.  Some of these upload/ downloads take some time.

Console.Write(request.Result.Content.ReadAsStringAsync().Result);

```