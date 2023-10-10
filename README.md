# Backend BP

Implementing APIs with microservices using Dockers.

## Requeriments

- Split the application into 2 microservices:
    - Customer and Person entities.
    - Account and Transaction entities.
- Ensure asynchronous communication between the 2 microservices.
- Develop API CRUD operations for the following entities:
    - Customer
    - Account
    - Transaction
- Validation
    - prevent transactions when funds are insufficient.
    - Update the available balance.
    - Log each transaction and attach it to the respective account.
- Report
    - An endpoint to generate reports (date range and customer)
- Unit Test
    - Customer
- Infra
    - Dockers


## Project Structure

```
- Bp.Client
    - Entities
        - Person.cs
        - Customer.cs
- Bp.Transaction
    - Entities
        - Account.cs
        - TransactionBalance.cs
- Bp.Common
    - IRepository.cs
    - MssqlDB
        - SqlDataRepository.cs
- Bp.Infra
    - docker
        - Bp.Client
            - Dockerfile
        - Bp.Transaction
            - Dockerfile
    - docker-compose.yml
- Bp.Test
    - Controllers
        - CustomersControllerTests.cs
    - Entities
        - CustomerTests.cs
- README.md
- Database.sql
- BackendBp.postman_collection.json
```

## How to run

```
user@Mi-Pc BackendBp % cd Bp.Infra
user@Mi-Pc BackendBp\Bp.Infra % docker-compose up --build -d
```

## Setting Database

For development purposes, you can execute `dotnet ef database update` within each project. Ensure to configure your Docker network to handle either `Data Source=localsql` or `Data Source=localhost`.

```
user@Mi-Pc BackendBp\Bp.Common\src\Bp.Common.Service %  dotnet ef database update
user@Mi-Pc BackendBp\Bp.Client\src\Bp.Client.Service %  dotnet ef database update
user@Mi-Pc BackendBp\Bp.Transaction\src\Bp.Transaction.Service %  dotnet ef database update
```
or run the file "Database.sql" 

```
- BackendBp 
    - Database.sql
```

### Consume API service

```
- BackendBp 
    - BackendBp.postman_collection.json
```

or try with swagger ```http://localhost:{port}/swagger/index.html```


# How to Contribute 

Fork repository and Run your Own Migration

```
    user@Mi-Pc Bp.Common.Service % dotnet ef dbcontext info
    user@Mi-Pc Bp.Common.Service % dotnet ef migrations add "name of migration"
    user@Mi-Pc Bp.Common.Service % dotnet ef database update    
```

