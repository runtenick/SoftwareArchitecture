# Software architecture (R4.01)
__Author:__ Franco Nicolas           
__Group:__ PM2    
__Year:__ 2A BUT      

This project was developed as part of the "__R4.01 Software Architecture__" course  taught by Mr. Chevaldonne, Ms. Millet, and Mr. Raymmond. This README file contains some documentation referring to my understanding of the project and what was done.

Table of contents :page_with_curl:
=================

<!--ts-->
   * [Introduction](#introduction)
   * [Installation and Running the solution](#installation)
   * [Project Structure](#structure)
      * [General Description](#general-description)
   * [Model, Business and Logic](#model)
   * [Database with Entity Framework](#database)
   * [Restful API](#restful-api)
   * [Tests](#tests)
   * [CI](#ci)
   * [Possible improvements](#possible-improvements)





<!--te-->

## Introduction
Throughout this project, I worked independently to maximize my learning. By working alone, I was able to ensure that I thoroughly understood every aspect of the project, rather than skipping over certain parts. To keep the master branch clean, I created separate branches for each feature I worked on, and merged most of them through the command line. However, a couple of branches were explicitly merged using the merge request feature in Gitea, as it was part of the project's requirement. You can find a complete history of all the branches in the image below.

![Branches](./Documentation/branches.png")

To make it easy to understand the project's evolution, I made sure to write clear and explicit commit messages. This also helped me revisit specific points of the project when necessary.

While I haven't provided full documentation for every class in every file, I did leave several comments throughout the code to help clarify certain aspects and for personnal use (mostly to remeber things between classes). In this README, I have made a concerted effort to provide detailed documentation explaining how each part of the project was implemented and my choices.

## Structure :building_construction:
 ![Project's architecture diagram](./Documentation/architecture.png "Project's architecture diagram")

### General Description
This project supports three different types of __clients__: <u>mobile</u>, <u>web</u>, and <u>desktop</u>. All of these clients communicate with a centralized __model__, which is responsible for handling <u>data</u> and <u>business</u> logic. 
Initially, the model uses fake data, as indicated by the green __Stub__ area. However, as the project progressed, the model was updated to use data from an API client. 
This client connects to a __Web API__, which serves as a bridge between the model and the data storage layer. The Web API interacts with an Entity Framework __(EF) database__, which is responsible for storing and managing data. In its early development, the API would also use fake data, hence the connection with the Stub. Both the model and the API interact with this database to retrieve and store data as needed. Additionally, the Web API is hosted on a __server__ [1], which allows clients to easily access it and interact with the model and data.
By using a central model and a Web API, we have designed a flexible architecture that can accommodate different types of clients and data storage systems.

[1]  _To be more precise, the Web API is deployed on a Docker container, the purpose of this representation is simply to convey the idea that the API is hosted on a separate system._

## Diagram Details :mag:
_[In this next section, I will try to detail the different connections you see in the diagram while giving some code snippets from the project's source to demonstrate how every bit was implemented.](./Documentation/architecture_global_description.md)_  



_[Table of contents](#table-of-contents-📃)_

---

## Database :minidisc:

### Entity Framework: Why Use It
Entity Framework (EF) is an Object-Relational Mapper (ORM) that simplifies data access as developers can work with relational data using domain-specific objects. With EF we didn't have to write any explicit SQL to build the database or the query it.

### Mappers and Entities
__Entities__ are classes with no logic that represent database tables. They consist of properties that define the columns of the table and relationships with other entities. In our code we have the following `ChampionEntity`, `SkinEntity`, `SkillEntity`, `RunePage` and `RuneEntity`.

For each entity we also have __mappers__, classes that handle the conversion between domain models and entities. They offer a way to map the properties of a domain model to corresponding properties of an entity and vice versa, ensuring data consistency after it moves around from database to model etc.

### How This Database Was Built
 The `ChampDbContext` class, derived from DbContext, is the main connection point between EF and the database. It contains `DbSet` properties (Champions, Skins, Skills, Runes, and RunePages) that represent the different tables in the database.

The `OnModelCreating` method in `ChampDbContext` sets up the relationships between the entities and configures the behavior of the database.

### Different Relationships Implemented
With this model, we had to implement two types of relationships in the database:

__One-to-many__: One `ChampionEntity` is related to multiple SkinEntity objects. This relationship is configured using the `HasOne` and `WithMany` methods in `OnModelCreating`.

__Many-to-many__: The `ChampionEntity` and `SkillEntity` classes have a many-to-many relationship, meaning each champion has multiple skills and each skill has multiple champions. A similar relationship exists between `RuneEntity` and `RunePageEntity`, as well as `ChampionEntity` and `RunePageEntity` (__many-to-many-to-many__). These relationships are configured using the `HasMany` and `WithMany` methods in `OnModelCreating`.

### Why I used Fluent API Instead of Data Annotations or Naming Conventions
Frst I tried using naming convention but finally fluent API seemed to be the most practical one, as well as my professor's recommendation.

Using Fluent API keeps the configuration separate from the entity classes, which keeps the entity classes cleaner. Also the configuration is centralized on the OnModelCreating method which made it easier to manage and modify the database configuration.

### Constantly Using Migrations and Recreating Them
Throughout the development process, migrations were used to keep the database schema up to date with changes in the code. Whenever a new table was added or a significant change was made to the database, the previous migration was deleted and a new one was recreated to ensure that the database schema stayed in sync with the latest code changes.

_[Table of contents](#table-of-contents-📃)_

---

## Restful API :globe_with_meridians:

### RESTful API: Why use it?
A RESTful API is used to manipulate data through well known __HTTP requests__ such as  GET, PUT, POST, and DELETE. It follows a set of constraints or principles that make it scalable, flexible, and easy to maintain. REST is an acronym for Representational State Transfer, which means that the data is transferred in a _stateless_ manner between the __client__ and the __server__.

We used a RESTful API because they provided a standard way of accessing data and allow multiple clients (which is the case as we've seen here), to access the same data without having to know the underlying implementation details. 

### Implementation of the API and why Swagger was used
Our Web API was implemented using .NET 6.0 and the ASP.NET Core framework. We used Swagger to document the API because it provides a simple and easy-to-use interface.

### Routes and RESTfulness in the Champions Controller
For this project we would have idealy at least 5 controllers, for Champion, Skin, Rune, RunePage and Skill. However, due to time constraints and being solo, I've only implemented the Champion controller. I did my best to follow the RESTful principles, and we can imagine that the other controller would follow a very similar strucuture to what I did with the `ChampionsController` class. 

This controller provides several routes that allow clients to interact with the champion data. _The routes are designed to follow RESTful principles._

```
 GET __api/<Champion>__
```
* This route returns a list of champions. It accepts a `PageRequest` object as a query parameter, which allows clients to specify the page number and the number of champions to return per page. In the case of very large databases and high scale projects, this is almost essencial, here it was done as a learning process.

```
GET __api/<Champion>/Akali__  
```
* This route returns a specific champion by name. It takes the name as parameter.

```
POST api/<Champion>
```
* This route adds a new champion to the database. It takes a ChampionDto object in the request body.

```
PUT api/<Champion>/Akali
```
* This route updates an existing champion. It takes the champion's name as parameter and a ChampionDto object in the request body.

```
DELETE api/<Champion>/Akali
```
* This route deletes an existing champion. It accepts the champion's name as a URL parameter.

```
GET api/<Champion>/Akali/skins
```
* This route returns the skins of a specific champion. It takes the champion's name as parameter.

> __It is worth noticing that for the "uniqueness" of the champion, it's name was used, which in many cases might not be ideal, as we can imagine two objects having the same name. However, in order to keep the model unchanged, I chose to use the name attribute as a way to identify a specif champion for this project."__

### Loggers and Pagination
In the Champions Controller, I used loggers to keep track of requests and errors. I also used pagination as previously mentioned, to limit the number of champions returned per page which should improve the API's performance (in this example it might not make much difference). Here are some code snippets to illustrate how these features were implemented:
```c#
// Loggers
public ChampionsController(IDataManager d, ILogger<ChampionsController> log)
{
    dataManager = d;
    _logger = log;
}

// ...
_logger.LogInformation($"Request to get champions with index " +
        $"{pageRequest.Index} and count {pageRequest.Count}");

// ...
_logger.LogWarning("too many champions requested");

// ...
_logger.LogError(ex, "error while trying to get champions");
```
* I used different levels of warnings for their respective purposes.

```c#
// Pagination
public async Task<IActionResult> Get([FromQuery]PageRequest pageRequest)
{
    // ...
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
    // ...
}
```

### DTOs and why we used them
DTOs, or Data Transfer Objects, are objects that represent data in a format that can be easily transferred between different systems or layers of an application. In this cased, we used DTOs to represent champion data in a format that can be easily serialized and deserialized.

Here is an example of a ChampionDto class:
```
public class SkinDto
{
   public string? Name { get; set; }

   public string? Image { get; set; }

   public float? Price { get; set; }

   public string? Description { get; set; }

   public string? Icon { get; set; }

   public string? Champion { get; set; }

}
```
* As you can see the attributes are all strings, easy to serialize/deserialize

</br>

_[Table of contents](#table-of-contents-📃)_

---

## Tests :test_tube:
As I did this project by myself, in addition to time constraints, I wasn't able to achieve a perfect test coverage. However, I did my best to have the necessary tests for the implementation I did, as well as explore the different technologies we saw throughout the course.

### ApiController Unit Tests
For the ApiController, a unit test class was created, which includes test methods for all the API operations (all routes that I implemented). `ChampionControllerTest` class tests the Get, Post, Put, Delete, GetByName, and GetSkins methods. While the tests cover the essential functionality, __there is room for improvement__ in terms of testing edge cases and error handling.

### Database Tests
For database tests, I used __in-memory SQLite database__. This approach had several benefits:
* The tests run quickly, as there is no disk I/O.
* The tests are isolated, as the in-memory database is on RAM memory and does not interfere with other tests or the production database.
* It integrates well with Entity Framework, simplifying the setup and execution of tests.

### xUnit Test Types
As I mentioned previously, I tried different types of xUnit tests were employed  including:

* __Fact tests:__ These tests check basic functionality and do not require any input parameters.
* __Theory + InlineData:__ These tests allow parameterized testing, with input data provided directly within the test method.
* __Theory + DataMember:__ These tests also use parameterized testing, but the input data is separated from the test method and stored in a separate data member.

Each test type has its advantages and use cases. Fact tests are suitable for simple, straightforward tests. Theory tests with InlineData or DataMember are ideal for more complex tests that require various input parameters to validate different scenarios.

Overall, tests are currently limited to CRUD operations, it could be expanded to include additional scenarios and edge cases.

</br>

_[Table of contents](#table-of-contents-📃)_

---

## CI :infinity:
Continuous Integration (CI) is a development practice that allows each integration to be verified by an automated build and automated tests. CI aims to detect and fix integration issues as quickly as possible, improving software quality and reducing the time it takes to validate and release new updates.

### Drone and Sonar
In this project, we used Drone, an open-source container-native Continuous Integration and Continuous Delivery (CI/CD) platform, to automate our build, test, and deployment processes. Drone works with a simple YAML configuration file (.drone.yml) that defines the pipeline steps, you can find the one used for this project [here](.drone.yml).

Sonar is a code quality analysis tool that helps maintain code quality over time by providing insights into code issues like code smells. It can be integrated with the CI/CD pipeline.

### Pipeline Steps Overview

* Build: Restore, build, and publish the .NET project using the dotnet command.
* Tests: Run the .NET tests with code coverage.
* Code Analysis: Perform code analysis with SonarScanner, generate code coverage reports with ReportGenerator, and upload the results to SonarQube.
* Docker Image Build and Store: Build a Docker image using the provided Dockerfile, then push it to the project's container registry.
* Deploy Container: Deploy the Docker container to the production environment using the Codefirst Dockerproxy client.

</br>

_[Table of contents](#table-of-contents-📃)_

---

## Model :file_folder:
In order to better understand the code and architecture of the application, we will now take a closer look at the structure of the model, including its classes and interfaces. __It is important to note that the model was developed by our professor, Mr. Chevaldonne__.

### Class Diagram
```mermaid
classDiagram
class LargeImage{
    +/Base64 : string
}
class Champion{
    +/Name : string
    +/Bio : string
    +/Icon : string
    +/Characteristics : Dictionary~string, int~
    ~ AddSkin(skin : Skin) bool
    ~ RemoveSkin(skin: Skin) bool
    + AddSkill(skill: Skill) bool
    + RemoveSkill(skill: Skill) bool
    + AddCharacteristics(someCharacteristics : params Tuple~string, int~[])
    + RemoveCharacteristics(label : string) bool
    + this~label : string~ : int?
}
Champion --> "1" LargeImage : Image
class ChampionClass{
    <<enumeration>>
    Unknown,
    Assassin,
    Fighter,
    Mage,
    Marksman,
    Support,
    Tank,
}
Champion --> "1" ChampionClass : Class
class Skin{
    +/Name : string    
    +/Description : string
    +/Icon : string
    +/Price : float
}
Skin --> "1" LargeImage : Image
Champion "1" -- "*" Skin 
class Skill{
    +/Name : string    
    +/Description : string
}
class SkillType{
    <<enumeration>>
    Unknown,
    Basic,
    Passive,
    Ultimate,
}
Skill --> "1" SkillType : Type
Champion --> "*" Skill
class Rune{
    +/Name : string    
    +/Description : string
}
Rune --> "1" LargeImage : Image
class RuneFamily{
    <<enumeration>>
    Unknown,
    Precision,
    Domination
}
Rune --> "1" RuneFamily : Family
class Category{
    <<enumeration>>
    Major,
    Minor1,
    Minor2,
    Minor3,
    OtherMinor1,
    OtherMinor2
}
class RunePage{
    +/Name : string
    +/this[category : Category] : Rune?
    - CheckRunes(newRuneCategory : Category)
    - CheckFamilies(cat1 : Category, cat2 : Category) bool?
    - UpdateMajorFamily(minor : Category, expectedValue : bool)
}
RunePage --> "*" Rune : Dictionary~Category,Rune~
```

Thid model provides a representation to different elements of the game [League of Legends](https://www.leagueoflegends.com/fr-fr/).

### Data Access Management Interfaces Diagram
The `IGenericDataManager<T>` interface provides methods for _getting_, _updating_, _adding_, and _deleting_ items of type T. The `IChampionsManager`, `IRunesManager`, `IRunePagesManager`, and `ISkinsManager` interfaces add category-specific methods for filtering and sorting the data.

```mermaid
classDiagram
direction LR;
class IGenericDataManager~T~{
    <<interface>>
    GetNbItems() Task~int~
    GetItems(index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~T~~
    GetNbItemsByName(substring : string)
    GetItemsByName(substring : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~T~~
    UpdateItem(oldItem : T, newItem : T) Task~T~~
    AddItem(item : T) Task~T~
    DeleteItem(item : T) Task~bool~
}
class IChampionsManager{
    <<interface>>
    GetNbItemsByCharacteristic(charName : string)
    GetItemsByCharacteristic(charName : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsByClass(championClass : ChampionClass)
    GetItemsByClass(championClass : ChampionClass, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsBySkill(skill : Skill?)
    GetItemsBySkill(skill : Skill?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsBySkill(skill : string)
    GetItemsBySkill(skill : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsByRunePage(runePage : RunePage?)
    GetItemsByRunePage(runePage : RunePage?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
}
class ISkinsManager{
    <<interface>>
    GetNbItemsByChampion(champion : Champion?)
    GetItemsByChampion(champion : Champion?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Skin?~~
}
class IRunesManager{
    <<interface>>
    GetNbItemsByFamily(family : RuneFamily)
    GetItemsByFamily(family : RuneFamily, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Rune?~~
}
class IRunePagesManager{
    <<interface>>
    GetNbItemsByRune(rune : Rune?)
    GetItemsByRune(rune : Rune?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~RunePage?~~
    GetNbItemsByChampion(champion : Champion?)
    GetItemsByChampion(champion : Champion?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~RunePage?~~
}

IGenericDataManager~Champion?~ <|.. IChampionsManager : T--Champion?
IGenericDataManager~Skin?~ <|.. ISkinsManager : T--Skin?
IGenericDataManager~Rune?~ <|.. IRunesManager : T--Rune?
IGenericDataManager~RunePage?~ <|.. IRunePagesManager : T--RunePage?
class IDataManager{
    <<interface>>
}
IChampionsManager <-- IDataManager : ChampionsMgr
ISkinsManager <-- IDataManager : SkinsMgr
IRunesManager <-- IDataManager : RunesMgr
IRunePagesManager <-- IDataManager : RunePagesMgr
```

### Simplified Stub Class Diagram
Here the `ChampionsManager`, `RunesManager`, `RunePagesManager`, and `SkinsManager` classes implement their respective _manager interfaces_ and are responsible for calling the appropriate methods on the `StubData` object to manipulate the data.

```mermaid
classDiagram
direction TB;

IDataManager <|.. StubData

ChampionsManager ..|> IChampionsManager
StubData --> ChampionsManager

RunesManager ..|> IRunesManager
StubData --> RunesManager

RunePagesManager ..|> IRunePagesManager
StubData --> RunePagesManager

SkinsManager ..|> ISkinsManager
StubData --> SkinsManager

StubData --> RunesManager
StubData --> "*" Champion
StubData --> "*" Rune
StubData --> "*" RunePages
StubData --> "*" Skins
```

</br>

_[Table of contents](#table-of-contents-📃)_

---
