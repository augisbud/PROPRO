#!/bin/bash
set -e

dotnet ef database update --project InvoicesBackend.csproj

exec dotnet InvoicesBackend.dll