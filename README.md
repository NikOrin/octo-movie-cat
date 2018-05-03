# Movie Rental Example API
This is a movie rental API made as an example for basic API design.

## Implemented Technology
* ASP.NET Web API
* Microsoft SQL Server

## How to set-up your database
If you wish to test this web API yourself, you will have to set-up your database as well.

1. If you don't have SQL Server, download and install SQL Server on your machine. Express works.

2. Create new database called "Movie"

3. Run all scripts in **octo-movie-cat.Database** project.
Run them in folder order.

## How to set-up your config file
The database connection settings for the web api are located in *Web.config* in the **octo-movie-cat** project

Snippet from *Web.config*:
```xml
<appSettings>
  ...
  <add key="DatabaseServer" value="DESKTOP-04RQJMA\SQLEXPRESS" />
  <add key="DatabaseName" value="Movies" />
</appSettings>
```

Just change that *DatabaseServer* value to whatever your server name is for your instance of SQL Server.

## Why did you make this?
Just for fun and practicing end-to-end web API creation using ASP.NET.

## Nuget Packages
None so far. Considering AutoMapper for mapping of the DTO to contract stuff.
