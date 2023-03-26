# Software architecture (R4.01)
__Author:__ Franco Nicolas           
__Group:__ PM2    
__Year:__ 2A BUT      

This project was developed as part of the "__R4.01 Software Architecture__" course  taught by Mr. Chevaldonne, Ms. Millet, and Mr. Raymmond. This README file contains some documentation referring to my understanding of the project and what was done.

Table of contents :page_with_curl:
=================

<!--ts-->
   * [Installation and Running the solution](#installation)
   * [Project Structure](#structure)
      * [General Description](#general-description)
   * [Model, Business and Logic](#model)
   * [Database with Entity Framework](#database-with-entity-framework)
   * [Restful API](#restful-api)
   * [CI](#continuous-integration)
   * [Possible improvements](#possible-improvements)
   * [Conclusion](#conclusion)





<!--te-->

## Structure
 ![Project's architecture diagram](./Documentation/architecture.png "Project's architecture diagram")

### General Description
This project supports three different types of __clients__: <u>mobile</u>, <u>web</u>, and <u>desktop</u>. All of these clients communicate with a centralized __model__, which is responsible for handling <u>data</u> and <u>business</u> logic. 
Initially, the model uses fake data, as indicated by the green __Stub__ area. However, as the project progressed, the model was updated to use data from an API client. 
This client connects to a __Web API__, which serves as a bridge between the model and the data storage layer. The Web API interacts with an Entity Framework __(EF) database__, which is responsible for storing and managing data. In its early development, the API would also use fake data, hence the connection with the Stub. Both the model and the API interact with this database to retrieve and store data as needed. Additionally, the Web API is hosted on a __server__ [1], which allows clients to easily access it and interact with the model and data.
By using a central model and a Web API, we have designed a flexible architecture that can accommodate different types of clients and data storage systems.

[1]  _To be more precise, the Web API is deployed on a Docker container, the purpose of this representation is simply to convey the idea that the API is hosted on a separate system._

## Diagram Details
_In this next section, I will try to detail the different connections you see in the diagram while giving some code snippets from the project's source to demonstrate how every bit was implemented._  

### Client-Model Communication 

Let's take a more technical look at how the client and the model communicate. I invite you to first read about the [model](#model) and how it is structured to better understand this part.  
The client communicates with the model through an instance of the `IDataManager` class. Thanks to our abstraction layer with the `IDataManager`, everything is easily accessible and all layers are _interchangeable_. 


As I previously explained, in the early stages of development, the Model used fake data provided by the Stub. The green Stub box in the diagram represents this data source. The Model has a reference to the Stub, which is indicated by a green arrow connecting the two elements. In terms of code, this happens at the `MauiProgram.cs` class:

> MauiPorgram.cs
```c#
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
			});
		builder.Services.AddSingleton<IDataManager, StubData>()
						.AddSingleton<ChampionsMgrVM>()
						.AddSingleton<SkinsMgrVM>()
						.AddSingleton<ApplicationVM>()
						.AddSingleton<ChampionsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
```

* As you can see, we are declaring a _dependecy injection_ for the IDataManager interface to use the `StubData` as its implementation: `builder.Services.AddSingleton<IDataManager, StubData>()`. The `AddSingleton` method is used to say that we will have a singleton instance of the `IDataManager` (which is ideal here).

* As the project progressed and we wanted to use real data in our app, we simply had to update this line of code, replacing `StubData` with `EfData` (detailed [here](#efdata)), the new interface we've implemented that _directly consumes our [Entity Framework database](#database-with-entity-framework):_

```
builder.Services.AddSingleton<IDataManager, EfData>()
```
### Model's API Client and Web API
Instead of directly consuming our database, using `EfData`, the client can also chose to query the data from [our API](#restful-api) (represented by the double-sided arrow in the diagram). To enable this, we will use yet another interface implmenting `IDataManager`, which is the red box you see attached to the model on the diagram, the `HttpManager` class.

> HttpManager.cs
```c# 
public partial class HttpManager : IDataManager
{
   public HttpManager(HttpClient httpClient)
   {
      HttpClient = httpClient;
      ChampionsMgr = new ChampionMgr(this);
   }

   public IChampionsManager ChampionsMgr { get; }

   public ISkinsManager SkinsMgr { get; }

   public IRunesManager RunesMgr { get; }

   public IRunePagesManager RunePagesMgr { get; }

   protected HttpClient HttpClient { get; set; }
}
```

To better understand how this class is used for the model to communicate with the API, we can look at the two methods we use in the partial class `ChampionsMgr`:

> HttpManager.Champion.cs
```c#
public partial class HttpManager
{
   public class ChampionMgr : IChampionsManager
   {
      private readonly HttpManager parent;
      public ChampionMgr(HttpManager parent)
            => this.parent = parent;

      // GET
      public async Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
      {
         var response = await parent.HttpClient.GetAsync($"api/Champions?index={index}&count={count}");
         if (!response.IsSuccessStatusCode)
         {
            throw new HttpRequestException($"Error while getting champions, status code: {response.StatusCode}");
         }
         var page = await response.Content.ReadFromJsonAsync<Page>();
         return page.MyChampions.Select(c => c.ToModel());
      }

      // GET by NAME
      public async Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
      {
         var response = await parent.HttpClient.GetAsync($"api/Champions/{substring}");
         if (!response.IsSuccessStatusCode)
         {
            throw new HttpRequestException($"Error while getting champion {substring}, status code: {response.StatusCode}");
         }
         var champion = await response.Content.ReadFromJsonAsync<Champion>();
         var champions = new List<Champion>
         {
            champion
         };
         return champions;
      }

      // Other methods ...
   }
}
```
In this particular case, the `HttpManager` class is used to manage champion data through the `ChampionMgr` class, which implements the `IChampionsManager` interface.

Through the `ChampionMgr` the client will be able to perform CRUD operations on champions data. For example, the `GetItems` method sends an HTTP GET request to the API to retrieve a list of champions, while the `GetItemsByName` method sends an HTTP GET request a specific champion.

The _parent parameter_ in each of these methods is an instance of the `HttpManager` class that is used to make the HTTP requests. The `HttpClient` attribute is used to create and send the requests.

The goal of this API client is to allow us to abstract away the details of the database so that, we can retrieve or manipulate data from the API without needing to understand the underlying database or infrastructure.

_You'll find more details about this API's structure [here](#restful-api)._

### API and Database
To clarify, the data that is requested through the API is __also__ retrieved from our Entity Framework (EF) database. This is why the diagram shows a connection between the __Web API__ and the __EF Database__.

In our program, each controller in the API has an `IDataManager` attribute. This allows us to access a single instance of the `IDataManager` that we define in our __Program.cs__ file, similarly to what we did on the __MauiProgram.cs__. Here is an example with the `Get` method from the `ChampionsController` class:

> ChampionsController.cs
```c#
[Route("api/[controller]")]
[ApiController]
public class ChampionsController : ControllerBase
{
   public IDataManager dataManager;

   private readonly ILogger<ChampionsController> _logger;

   public ChampionsController(IDataManager d, ILogger<ChampionsController> log)
   {
      dataManager = d;
      _logger = log;
   }

   // GET: api/<Champion>
   [HttpGet]
   public async Task<IActionResult> Get([FromQuery]PageRequest pageRequest)
   {
      _logger.LogInformation($"Request to get champions with index " +
            $"{pageRequest.Index} and count {pageRequest.Count}");

      if(pageRequest.Count > 10)
      {
         _logger.LogWarning("too many champions requested");
         // return erreur ?
      }
      try
      {
         var champions = (await dataManager.ChampionsMgr.GetItems(pageRequest.Index,
            pageRequest.Count)).Select(champion => champion?.ToDto());

         var page = new Page()
         {
            MyChampions = champions,
            Index = pageRequest.Index,
            Count = pageRequest.Count,
            TotalCount = await dataManager.ChampionsMgr.GetNbItems()
         };
         return Ok(page);
      }
      catch(Exception ex)
      {
         _logger.LogError(ex, "error while trying to get champions");

         return BadRequest(ex.Message);
      }

      // Other methods ...
   }
}
```

Here you can see the `IDataManager` class beeing used to fetch the list of champions : 
```
var champions = (await dataManager.ChampionsMgr.GetItems(pageRequest.Index,
            pageRequest.Count)).Select(champion => champion?.ToDto());
```

And just like with the __MauiProgram.cs__, we can modify the implementation type of `IDataManager` with _dependecy injection_ through the __Program.cs__ file:

> Program.cs
```c#
var builder = WebApplication.CreateBuilder(args);

// ...

/* Dependecy injection */
builder.Services.AddScoped<IDataManager, EfData>();

// ...

app.Run();
```

In the beginning of the project, when we still had no real data, we used the stub:

```
builder.Services.AddScoped<IDataManager, StubData>();
```