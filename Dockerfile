FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build-env

RUN apt update -y && \
    apt install nodejs -y &&  \
    npm install --global yarn
WORKDIR /app

# Copy everything
COPY ./src/ForetoBot.Api ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ForetoBot.Api.dll"]