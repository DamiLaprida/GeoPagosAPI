#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Define la etapa base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Define la etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura las dependencias
COPY GeoPagos.API.csproj .
RUN dotnet restore

# Copia el resto de los archivos y construye la aplicación
COPY . .
WORKDIR "/src/GeoPagos.API/GeoPagos.API"
RUN dotnet build "GeoPagos.API.csproj" -c Release -o /app/build

# Define la etapa de publicación
FROM build AS publish
RUN dotnet publish "GeoPagos.API.csproj" -c Release -o /app/publish

# Define la etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoPagos.API.dll"]
