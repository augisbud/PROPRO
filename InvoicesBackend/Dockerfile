FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /InvoicesBackend

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /InvoicesBackend
COPY --from=build-env /InvoicesBackend/out .
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

EXPOSE 8080
ENTRYPOINT ["./entrypoint.sh"]
