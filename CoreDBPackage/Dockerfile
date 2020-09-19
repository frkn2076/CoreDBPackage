#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

WORKDIR /app
#ADD ./FileLog ./mnt/		# COPY ./src /some_dir/dest_dir/ => Copy the content of the src directory to /some_dir/dest_dir directory
COPY ./Config/settings.xml ./Config/
COPY ./FileLog ./FileLog/

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoreDBPackage.csproj", "./"]
RUN dotnet restore "./CoreDBPackage.csproj" # Restore dependencies and tools for the project in the current directory:
COPY . .
WORKDIR "/src/."
RUN dotnet build "CoreDBPackage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreDBPackage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreDBPackage.dll"]


