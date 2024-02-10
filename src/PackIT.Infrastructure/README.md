# Migrations

## Install dotnet-ef tool
```bash
dotnet tool install --global dotnet-ef
```

## Example: Add initial migration to ReadDbContext
```bash
dotnet ef migrations add Init_Read --context ReadDbContext --project ./PackIT.Infrastructure --startup-project ./PackIT.API -o EF/Migrations
```
