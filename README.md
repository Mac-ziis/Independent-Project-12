# _Local Parks Finder_

#### By _**Mac Granger**_

#### _This Local Parks Finder API utilizes .Net Core 6.0, Identity, Pagination, and Tokening to help users find and document their local parks._

## Technologies Used

* _C#_
* _Entity Framework Core_
* _ASP .NET Core Identity_
* _Swashbuckle_
* _Pomelo_
* _JWT Bearer_
* _.NET 6.0_
* _Visual Studio Code_
* _GitHub_

## Description

_This project is called "LocalParks" and it allows the user to view seeded information that is stored within the API with MySql and while using ASP.NET Core. The API will store information about Local Parks including: ID, Name, Location, and Summary. Users will then be able to call on this information in various ways as well as add their own information to the database. Pagination has been added to the Swagger page, allowing the viewer to choose how many responses and pages are seen at a time. Tokening and authentication will add a layer of security to this API, in the case of the user having a client side application made, this will allow the API to remain secure and will require users who call upon the information to have a JWT Bearer token._

## Setup/Installation Requirements

Install the tools that are introduced in [this series of lessons on LearnHowToProgram.com](https://part-time-evening.learnhowtoprogram.com/c-and-net/building-an-api/adding-a-model-and-database).

If you have not already, install the `dotnet-ef` tool by running the following command in your terminal:

```
dotnet tool install --global dotnet-ef --version 6.0.0
```

Also, if working off of the second branch "WithAuth" Install these tools for the implimentation of Pagination and Authentication:

```
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.0

```


### Set Up and Run Project

1. Clone this repo.
2. Open the terminal and navigate to this project's main directory called "LocalParks.Solution".
3. Within the production directory "LocalParks", create two new files: `appsettings.json` and `appsettings.Development.json`.
4. Within `appsettings.json`, put in the following code. Make sure to replacing the `uid` and `pwd` values in the MySQL database connection string with your own username and password for MySQL. For the LearnHowToProgram.com lessons, we always assume the `uid` is `root` and the `pwd` is `epicodus`.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=cretaceous_api;uid=[YOUR_USERNAME];pwd=[YOUR_MYSQL_PASSWORD];"
  }
}
```

5. Within `appsettings.Development.json`, add the following code:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Trace",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

6. Install all necessary packages by running `dotnet restore` in the shell while within the production directory "LocalParks".
6. Create the database using the migrations in the LocalParks project. Open your shell (e.g., Terminal or GitBash) to the production directory "LocalParks", and run `dotnet ef database update`. 
7. Within the production directory "LocalParks", run `dotnet watch run` in the command line to start the project server and open the webpage within your browser. 
9. Use your program of choice to make API calls. In your API calls, use the domain _http://localhost:7099_. 

## Known Bugs

* _"WithAuth" branch currently under construction._

## License

Copyright (c) 2023 Mac Granger

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.