FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
#directory in docker file system
WORKDIR /app
#port 80
EXPOSE 80

# copy all .csproj files and restore as distinct layers. use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
# copy files from project directory to docker directory
COPY Carsties.sln Carsties.sln
COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj

#Restore package deps
RUN dotnet restore Carsties.sln

# COPY the app folder over
COPY src/GatewayService src/GatewayService
#workdir is just like terminal current dir
WORKDIR /app/src/GatewayService
RUN dotnet publish -c Release -o /app/src/out

#after publish switch to runtime image to lighten data
#BUILD runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "GatewayService.dll"]