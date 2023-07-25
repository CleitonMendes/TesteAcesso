#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CCM.TesteAcesso.API/CCM.TesteAcesso.API.csproj", "CCM.TesteAcesso.API/"]
COPY ["CCM.TesteAcesso.Application/CCM.TesteAcesso.Application.csproj", "CCM.TesteAcesso.Application/"]
COPY ["CCM.TesteAcesso.Domain/CCM.TesteAcesso.Domain.csproj", "CCM.TesteAcesso.Domain/"]
COPY ["CCM.TesteAcesso.Infra/CCM.TesteAcesso.Infra.csproj", "CCM.TesteAcesso.Infra/"]
RUN dotnet restore "CCM.TesteAcesso.API/CCM.TesteAcesso.API.csproj"
COPY . .
WORKDIR "/src/CCM.TesteAcesso.API"
RUN dotnet build "CCM.TesteAcesso.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CCM.TesteAcesso.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CCM.TesteAcesso.API.dll"]