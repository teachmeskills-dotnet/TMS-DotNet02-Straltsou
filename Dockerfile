FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/LearnApp.WebAPI/LearnApp.WebAPI.csproj", "src/LearnApp.WebAPI/"]
COPY ["src/LearnApp.Common/LearnApp.Common.csproj", "src/LearnApp.Common/"]
COPY ["src/LearnApp.BLL/LearnApp.BLL.csproj", "src/LearnApp.BLL/"]
COPY ["src/LearnApp.DAL/LearnApp.DAL.csproj", "src/LearnApp.DAL/"]
RUN dotnet restore "src/LearnApp.WebAPI/LearnApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/LearnApp.WebAPI"
RUN dotnet build "LearnApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearnApp.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet LearnApp.WebAPI.dll