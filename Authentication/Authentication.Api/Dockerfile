FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Authentication.Api/Authentication.Api.csproj Authentication.Api/
RUN dotnet restore Authentication.Api/Authentication.Api.csproj
COPY . .
WORKDIR /src/Authentication.Api
RUN dotnet build Authentication.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Authentication.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]
