## Chat Room App
In order to run the applications (Identity Provider and API) you need to meet the following requirements:

 - Visual Studio 2019 (this in order to facilitate the execution instead
   of running with `dotnet run`)
 - Microsoft SQL Server 2016+

**Before running:**

 1. Update connectionstrings in appsettings.json in ChatRoom.IdentityProvider and ChatRoom.Api
 2. Update RabbitMQ host address in connectionstrings  in ChatRoom.Api

To use the application there are two test users, we need to use those since I didn't add how to register users. The users are:

**TestUser1**
Username: testuser1
Password: Password@123

**TestUser2**
Username: testuser2
Password: Password@123

To gain some development time I used the QuickstartUI from IdentityServer4 which provide views and controllers for the Identity Provider.

Some configurations are fixed in some places, like CORS allowed origins, they should be part of the appsettings configuration file but for time lacking I didn't add them.

The functionality I decided to test (using NUnit and Moq) is the `StockProcessor`in the ChatRoom.StockBot project.

I didn't spend time in the UI, so it is rustic and it is not friendly. But the idea was to focus in the backend.

### Bonus
In addition to mandatory features, I completed the following aspects:

 1. Use .Net Identity for users authentication. For this matter I created an identity provider server to secure to secure the API using IdentityServer4 and used ASP.NET Core Identity for the authentication system.
 2. Handle messages that are not understood or any exceptions raised within the bot.
