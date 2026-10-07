# Invelop coding task

## Create a simple web app for managing personal contacts and their phone numbers

### Required data:
- first name
- surname
- D.O.B
- Address
- phone numbers
- IBAN

### Business rules:
- A contact must be at least 16 years old.
- A contact must have at least one phone number.
- A contact can have only one primary phone number.

There are a few additional paradigms/technologies/techniques we are interested to see.

### .NET Backend:
- EF Core for DB communication (any DB)
- FluentValidation
- CQRS pattern
- Rich Domain Model

### Angular frontend:
- Angular
- NGXS
- Usage of a design framework, like PrimeNG or similar
- Playwright or Cypress E2E test (1 is enough)

### Notes
- Database Migrations

Go to the solution root folder:
```
backend/ContactsApp/
```

Create migration
```
dotnet ef migrations add InitialMigration --project ./ContactsApp.Infrastructure/ --startup-project ./ContactsApp.API/
```

Apply all pending migrations to the database
```
dotnet ef database update --project ./ContactsApp.Infrastructure/ --startup-project ./ContactsApp.API/
```