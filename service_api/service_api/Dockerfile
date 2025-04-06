FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["service_api.csproj", "./"]
RUN dotnet restore "service_api.csproj"

COPY . .
RUN dotnet build "service_api.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "service_api.dll"]
