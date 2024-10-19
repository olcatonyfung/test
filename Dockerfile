FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore WebApplication1/WebApplication1.csproj
RUN dotnet publish WebApplication1/WebApplication1.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish .

EXPOSE 5100
ENV ASPNETCORE_HTTP_PORTS 5100

ENTRYPOINT ["dotnet", "WebApplication1.dll"]