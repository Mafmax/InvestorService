#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mafmax.InvestorService.Api/Mafmax.InvestorService.Api.csproj", "Mafmax.InvestorService.Api/"]
COPY ["Mafmax.InvestorService.Services/Mafmax.InvestorService.Services.csproj", "Mafmax.InvestorService.Services/"]
COPY ["Mafmax.InvestorService.Model/Mafmax.InvestorService.Model.csproj", "Mafmax.InvestorService.Model/"]
COPY ["Mafmax.InvestorService.Model.Migrations/Mafmax.InvestorService.Model.Migrations.csproj", "Mafmax.InvestorService.Model.Migrations/"]
RUN dotnet restore "Mafmax.InvestorService.Api/Mafmax.InvestorService.Api.csproj"
COPY . .
WORKDIR "/src/Mafmax.InvestorService.Api"
RUN dotnet build "Mafmax.InvestorService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mafmax.InvestorService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mafmax.InvestorService.Api.dll"]