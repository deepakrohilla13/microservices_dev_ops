from microsoft/dotnet:latest
copy . /app
workdir /app
run ["dotnet", "restore"]
run ["dotnet", "build"]
expose 5000/tcp
entrypoint ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]

