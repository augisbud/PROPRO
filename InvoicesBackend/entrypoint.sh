#!/bin/bash
set -e

# Run migrations
dotnet ef database update --project InvoicesBackend

# Start the main process
exec dotnet InvoicesBackend.dll