#Build stage
FROM microsoft/aspnetcore-build:2 as build-new

WORKDIR /api

COPY . .

RUN dotnet restore

RUN dotnet publish -o /publish

EXPOSE 5000/tcp

EXPOSE 9898/tcp

FROM microsoft/aspnetcore:2 

WORKDIR /publish

COPY --from=build-new /publish .

ENTRYPOINT ["dotnet", "TodoAPI.dll"]

