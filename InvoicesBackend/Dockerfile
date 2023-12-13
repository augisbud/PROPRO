FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY ["InvoicesBackend.csproj", "./"]
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

EXPOSE 8080

ENTRYPOINT ["./entrypoint.sh"]