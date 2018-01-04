#Build stage
FROM microsoft/aspnetcore-build:2 as build-new

WORKDIR /api

COPY . .

RUN dotnet restore

RUN dotnet build -o /publish

#expose 5000/tcp

FROM microsoft/aspnetcore:2 

WORKDIR /publish

COPY --from=build-new /publish .

ENTRYPOINT ["dotnet", "TodoAPI.dll"]

