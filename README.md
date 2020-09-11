# Learn English Application

The main idea of application is to provide appropriate API for interaction with the front-end part and also provide needed functionality for studing English by learning the diffrent words and meaning of them.

# Getting Started

The application is free-to-use.
For using current API you need to register or login by the [following link](https://learn-app.netlify.app).

You can also find repository with front-end part of this application by the [following link](https://github.com/green1971weekend/TMS-DotNet02-Straltsou-FrontEnd).

# Application settings
For the correct deploy, it is necessary to create the appsettings.json in the project WebUI directory according to the template below.

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "ApplicationConnection": "Server=YourDatabaseHost;Port=5432;Database=NameOfDatabase;User Id=UserIdentity;Password=YourPassword;"
    },
    "AppSettings": {
        "SecretEncryptionKey": "You need to write your own key for JWT encryption",
        "EmailFrom": "Your SMTP email for mailing verification letter",
        "SmtpHost": "SMTP Host",
        "SmtpPort": 465,
        "SmtpUser": "Your SMTP email for mailing verification letter",
        "SmtpPass": "SMTP email password"
    },
    "API": {
        "TranslateAPI": "API key for translation objects",
        "UnsplashAPI": "API key for picture objects"
    }
}
```
"ConnectionStrings" section contains necessary information for connection to database. In the current case - string for connection to PostgreSQL.
"AppSettings" section contains necessary information for SMTP mailing and "SecretEncryptionKey" for JWT encryption.
"API" section contains information for outer support API. Needed API keys you can obtain by the following links:
* [TranslateAPI](https://rapidapi.com/systran/api/systran-io-translation-and-nlp/endpoints)
* [UnsplashAPI](https://unsplash.com/documentation#search-photos)

# Deployment to Heroku
After you install the Heroku CLI, run the following login commands:

```
heroku login
heroku container:login
```

For the application (HEROKU_APP) correct work, a database is required. To add PortgreSQL database on [Heroku](https://dashboard.heroku.com/), use the following command:

```
heroku addons:create heroku-postgresql:hobby-dev --app:HEROKU_APP
```
Or add it manually after creating the container in the Resourses tab.

To start the deployment to Heroku, run the following commands from the project folder:
```
docker build -t HEROKU_APP .
docker tag HEROKU_APP registry.heroku.com/HEROKU_APP/web
heroku container:push web -a HEROKU_APP
heroku container:release web -a HEROKU_APP
```
When deploying from Linux, use sudo for the commands above.

# Build with

* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
* [Three-Tier Architecture Model](https://docs.microsoft.com/en-us/windows/win32/cossdk/using-a-three-tier-architecture-model)
* Deployment to [Heroku](https://dashboard.heroku.com/apps) with support of [Docker](https://www.docker.com/)
* [JWT Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Automapper](https://automapper.org/)
* [MailKit](https://www.nuget.org/packages/MailKit/)
* [Flurl.HTTP](https://www.nuget.org/packages/Flurl.Http/3.0.0-pre3)
* [BCrypt](https://www.nuget.org/packages/BCrypt.Net-Next/)
* [Health check](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1)
* Unit tests with [xUnit](https://xunit.net/) & [Moq](https://github.com/Moq/moq4/wiki/Quickstart)

# Author
[Maksim Straltsou](https://github.com/green1971weekend) - Software Developer

# License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/green1971weekend/TelegramBot/blob/master/LICENSE) file for details.