FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY TraineeRecords.sln .
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Web/Web.csproj Web/

RUN dotnet restore

COPY . .

WORKDIR /source/Web
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Web.dll"]
