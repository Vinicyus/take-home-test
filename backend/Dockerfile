FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LoanManagement.API/LoanManagement.API.csproj", "LoanManagement.API/"]
COPY ["LoanManagement.Domain/LoanManagement.Domain.csproj", "LoanManagement.Domain/"]
COPY ["LoanManagement.Infrastructure/LoanManagement.Infrastructure.csproj", "LoanManagement.Infrastructure/"]
RUN dotnet restore "LoanManagement.API/LoanManagement.API.csproj"
COPY . .
WORKDIR "/src/LoanManagement.API"
RUN dotnet build "LoanManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanManagement.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanManagement.API.dll"]