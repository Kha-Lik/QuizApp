FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QuizApp.API/QuizApp.API.csproj", "QuizApp.API/"]
RUN dotnet restore "QuizApp.API/QuizApp.API.csproj"
COPY . .
WORKDIR "/src/QuizApp.API"
RUN dotnet build "QuizApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizApp.API.dll"]
