#!/bin/bash
set -e

exec dotnet ef database update --project InvoicesBackend.csproj

exec dotnet InvoicesBackend.dll