# OnlineForum

Purpose of this project was to create a similar type of forum as Reddit and Hackernews.

The front-end is built using ASP.NET Core 1.1 with Razor and Boostrap 3.3.7 (OnlineForum.Web). The data is stored and retrieved using the business layer services (OnlineForum.Core). The data is managed by using Entity Framework Core Code-First approach (OnlineForum.DAL).

### Running

As this is a practice project, it requires LocalDB to be installed.

1. Open solution
2. Open NuGet Package Manager Console
3. Type "update-database", and wait for it to finish
4. Hit F5