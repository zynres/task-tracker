FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY src/API/API.csproj src/API/
COPY src/Inf/Inf.csproj src/Inf/
COPY src/App/App.csproj src/App/
COPY src/Domain/Domain.csproj src/Domain/

COPY test/Test/Test.csproj test/Test/

RUN dotnet restore src/API/API.csproj
RUN dotnet restore test/Test/Test.csproj

COPY . .

RUN dotnet publish src/API/API.csproj -c Release -o /app/publish

FROM build AS test-runner
RUN dotnet test test/Test.csproj -c Release


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]