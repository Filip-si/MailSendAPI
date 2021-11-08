#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MailSendAPI/MailSendAPI.csproj", "MailSendAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "MailSendAPI/MailSendAPI.csproj"
COPY . .
WORKDIR "/src/MailSendAPI"
RUN dotnet build "MailSendAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MailSendAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MailSendAPI.dll"]