--------------------
Smile Movies Web API
--------------------

The Smile.Movies.Api solution represents simple web base api for:
1) Fetch movies from the third party datasource
2) Return movies in a sorted order by any of the movie attributes. (Except for the field “Cast”)
3) Search across all fields within the movie
4) Insert new movie
5) Update existing movie.

Solution file (VS2017): MoviesWebApi\src\MoviesWebApi.sln
Source code: MoviesWebApi\src
MoviesLibrary: MoviesWebApi\lib\MoviesLibrary.dll

Solution projects:
 - Smile.Movies.Api - .NET Core 2.0 web api with mvc approach
 - Smile.Movies.Backend - .Net Core 2.0 class library includes backend services such as DataService, CachingService, SearchingService, SortingService
 - Smile.Movies.Backend.Interfaces - .Net Core 2.0 class library includes backend interfaces
 - Smile.Movies.Dto - .Net Core 2.0 class library includes models (DTOs).
 - Smile.Movies.Shared - .Net Core 2.0 class library includes shared helpers and utilities classes.
 - Smile.Movies.Web - .Net Core 2.0 Mvc with Angular front-end.
 
 - Smile.AngularCli.Client - Angular CLI project. Some details regarding it see below.
 
 
 Test projects (MSTEST):
 - Smile.Movies.Api.Tests
 - Smile.Movies.Backend.Tests

--------
WEB API
--------

URL: <host:port>/api/movies (http://localhost:50590/api/movies)

---------
METHODS:
---------
GET /api/movies - Fetch all movies
GET /api/movies/{movieid} - Fetch movie with movie id
POST /api/movies - create new movie
PUT /api/movies - update existing movie

* GET (READ)
-------------

GET /api/movies - Fetch all movies in json format

Optional parameters:

search=[string] - Search across all fields within the movie

sort=[-field1,field2] - Sorting by provided fields. Sing "-" before field specified sort direction descending. By default it is ascending.

GET /api/movies/{movieid:int} - Fetch movie with movie id. /api/movies/7 - returns movie with movie id = 7.

Response:
Code:200 OK
Content: 
[{
  "classification": "string",
  "genre": "string",
  "movieId": 0,
  "rating": 0,
  "releaseDate": 0,
  "title": "string",
  "cast": [
    "string"
  ]
}]

Code: 404 (NotFound) if result is null or empty
Code: 500 (InternalServerError) If exception was thrown during fetching movies

* POST (CREATE)
---------------

POST /api/movies - create new movie with provided attributes. A movieId will be generated and new created movie return
Parameters: 
{
  "classification": "string",
  "genre": "string",
  "movieId": 0,
  "rating": 0,
  "releaseDate": 0,
  "title": "string",
  "cast": [
    "string"
  ]
}

Response:
Code:201 Created
Content: 
{
  "classification": "string",
  "genre": "string",
  "movieId": 0,
  "rating": 0,
  "releaseDate": 0,
  "title": "string",
  "cast": [
    "string"
  ]
}

Code: 404 (NotFound) - if result is null or empty
Code: 400 (BadReuest) - if model is not valid
Code: 500 (InternalServerError) - If exception was thrown during creating movie


* PUT (UPDATE)
--------------

PUT /api/movies - update existing movie with provided attributes

Parameters: 
{
  "classification": "string",
  "genre": "string",
  "movieId": 0,
  "rating": 0,
  "releaseDate": 0,
  "title": "string",
  "cast": [
    "string"
  ]
}


Response:

Code: 200 OK
Code: 400 (BadRequest) - if model is not valid
Code: 500 (InternalServerError) - If exception was thrown during updating movie

------
 NOTE
------
The Smile.Movies.Api project includes Swagger tools for documenting APIs built on ASP.NET Core API
It could be useful for starting familiar with API and also for simple tests.
To fetch doc: http://localhost:50590/swagger/

----------------------------
Cache configuration settings 
----------------------------
Cache expiration time could be configured by ExpirationTimeHours setting in appsettings.json configuration file
The default is 24 hours.

-----------------
Design Decisions 
-----------------
Most of design decisions in this project are related to Microsoft Best Practice approaches with following SOLID principles.
The ASP.NET Core dependency injection container was used in this case.

The Solution includes set of services which represents particular logic:
  * DataService - encapsulates logic related to fetching, creating, updating data
  * CachingService - includes a logic related to caching data to MemoryCache
  * SearchingService - represents search logic across all fields within the movie
  * SortingService - represents sorting strategies and allows to sort by multi fields with (default) ascending and ('-')descending direction

Design Patterns are included:
  * Adapter - DataSourceAdapter.cs - Wraps the MoviewLibrary.MovieDataSource class with the IDataSourceAdapter interface
  * Strategy - SortingStrategies (SortingService) - Represents sorting strategies by particular fields. One strategy one field. 

------------------
 Web Clients
------------------
1) Smile.Movies.Web - .Net Core 2.0 Mvc with Angular front-end.
 1.1 Initialize node_modules run the next command inside the project folder: npm install
 1.2 Rebuild solution. Could be required 2-3 times due nuget dependencies.
 1.3 Set multi projects start order: first - Smile.Movies.Api, second - Smile.Movies.Web
 1.4 Run solution.

2) Smile.AngularCli.Client - Angular CLI project.
   2.1 Initialize node_modules run the next command inside the project folder: npm install
   2.2 Run under CLI. The Angular CLI should be installed on PC https://angular.io/guide/quickstart
       Cmd command: ng serve --open
	   
----------------------------------------------------------------------------------------------------------------------

