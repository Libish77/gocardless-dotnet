# .NET Client for the GoCardless API

For full details of the GoCardless API, see the [API docs](https://developer.gocardless.com/).

[![NuGet](https://img.shields.io/nuget/v/GoCardless.svg)](https://www.nuget.org/packages/GoCardless/)
[![AppVeyor](https://img.shields.io/appveyor/ci/jacobpgn/gocardless-dotnet.svg)](https://ci.appveyor.com/project/jacobpgn/gocardless-dotnet)

- ["Getting started" guide](https://developer.gocardless.com/getting-started/api/introduction/)
- [API Reference](https://developer.gocardless.com/api-reference)
- [NuGet Package](https://www.nuget.org/packages/GoCardless/)

## Installation

To install `GoCardless`, run the following command in the [Package Manager Console](https://docs.microsoft.com/en-us/nuget/tools/package-manager-console)

`Install-Package GoCardless -Version 2.0.3`


## Usage

> Note: This README will use "customers" in examples throughout, but every endpoint in the API is available in this library.

### Initialising the client

The client is initialised with an access token and an environment.

```cs
String accessToken = "your_access_token";
GoCardlessClient gocardless = GoCardlessClient.Create(accessToken, Environment.SANDBOX);
```

### GET requests

#### Individual resources

You can retrieve individual resources by ID using that resource's `GetAsync` method:

```cs
var customerResponse = await gocardless.Customers.GetAsync("CU0123");
GoCardless.Resources.Customer customer = customerResponse.Customer;
```

#### Lists of resources

There are two ways to list resources. You can either make single requests for lists with `ListAsync`:

```cs
var customerRequest = new GoCardless.Services.CustomerListRequest()
{
    Limit = 100
};

var customerListResponse = await gocardless.Customers.ListAsync(customerRequest);

foreach (GoCardless.Resources.Customer customer in customerListResponse.Customers)
{
    Console.WriteLine(customer.GivenName);
}

Console.WriteLine("Next page cursor: " +  customerListResponse.Meta.Cursors.After);
```

or use the lazy pagination offered by `All`:

```cs
var customerRequest = new GoCardless.Services.CustomerListRequest()
{
    Limit = 100
};

var customerListResponse = gocardless.Customers.All(customerRequest);
foreach (GoCardless.Resources.Customer customer in customerListResponse)
{
    Console.WriteLine(customer.GivenName);
}
```

The lazy pagination approach is generally simpler - it automatically makes as many API requests as needed to return
the whole list as an enumerable, and you don't need to take care of pagination manually.

### POST/PUT requests

These work in a similar way to GET requests, and you can initialize a request object with all of the available
parameters for each endpoint.

*Creating a customer*

```cs
var customerRequest = new GoCardless.Services.CustomerCreateRequest()
{
    Email = "user@example.com",
    GivenName = "Frank",
    FamilyName = "Osborne",
    AddressLine1 = "27 Acer Road",
    AddressLine2 = "Apt 2",
    City = "London",
    PostalCode = "E8 3GX",
    CountryCode = "GB",
    Metadata = new Dictionary<string, string>()
    {
      {"salesforce_id", "ABCD1234"}
    }
};

var customerResponse = await gocardless.Customers.CreateAsync(customerRequest);
GoCardless.Resources.Customer customer = customerResponse.Customer;
```

*Updating a customer*

```cs
var customerRequest = new GoCardless.Services.CustomerUpdateRequest()
{
    Metadata = new Dictionary<string, string>()
    {
        {"custom_reference", "NEWREFERENCE001"}
    }
};
var customerResponse = await gocardless.Customers.UpdateAsync("CU0123", customerRequest);
```

When creating a resource, the library will automatically include a randomly-generated
[idempotency key](https://developer.gocardless.com/api-reference/#making-requests-idempotency-keys)
- this means that if a request appears to fail but is in fact successful (for example due
to a timeout), you will not end up creating multiple duplicates of the resource.

You can provide your own key for any endpoints that support idempotency keys by setting it in
the request object:

```cs
var customerRequest = new GoCardless.Services.CustomerCreateRequest()
{
    Email = "user@example.com",
    IdempotencyKey = "unique_customer_reference"
};
```

### Accessing raw response data

You can retrieve the `System.Net.Http.HttpResponseMessage` from any resource or resource list response:

```cs
var customerRequest = new GoCardless.Services.CustomerUpdateRequest()
{
    Metadata = new Dictionary<string, string>()
    {
        {"custom_reference", "NEWREFERENCE001"}
    }
};
var customerResponse = await gocardless.Customers.UpdateAsync("CU0123", customerRequest);

HttpResponseMessage responseMessage = customerResponse.ResponseMessage;
```

### Errors

If the API returns an error response, the client will raise a corresponding Exception.
The exceptions are all subclasses of `GoCardless.Exceptions.ApiException`:

- `InternalException`
- `InvalidApiUsageException`
- `InvalidStateException`
- `ValidationFailedException`

These errors are fully documented in the [API documentation](https://developer.gocardless.com/api-reference/#overview-errors).

The exceptions have the following properties to facilitate access to information in the API response:

- `String Type`
- `String DocumentationUrl`
- `String RequestId`
- `int Code`
- `IReadOnlyList<GoCardless.Errors.Error> Errors`

`GoCardless.Errors.Error` provides more specific error messages and reasons from the response.

When the API returns an `invalid_state` error due to an `idempotent_creation_conflict` the library will, where possible,
automatically retrieve the existing record which was created using the same idempotency key.

If a timeout occurs, and the request being made is idempotent, the library will automatically retry the request up to 2 more times.

### Support and feedback

- [Developer documentation](https://developer.gocardless.com/)
- [Developer support](https://support.gocardless.com/hc/en-us/categories/115000140449)
- api@gocardless.com