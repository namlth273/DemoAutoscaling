FROM mcr.microsoft.com/dotnet/core/sdk:3.1.300-focal AS build-env
WORKDIR /app

COPY *.sln ./
COPY /*/*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore
COPY . ./

RUN dotnet publish MyConsumer/MyConsumer.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-focal
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MyConsumer.dll"]
