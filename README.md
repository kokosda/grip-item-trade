### API for transfer things between accounts

#### Scenario
Let's say, we have two accounts belonging to 2 different customers.
Customer Alice transfers things from her account to Bob's one.
A transfer is completed in the unit of work.
The result is 2 transactional operations, debit and credit, correspondingly. Debit charges Alice's account, credit deposits Bob's one.
Debit transactional operation is parent one. Credit is considered to be a dependent one.

#### Entities
- **Customer**, may have multiple Accounts.
- **Account**, may belong to only one customer and have multiple BalanceEntries.
- **BalanceEntry**, a list of items belonging to the Account. For instance, 5 BROOMS belong to Alice.
- **TransactionalOperation**, a representation of charging and depositing BalanceEntries and an overall total of such operation.
- **TransactionalOperationEntry**, an entry in TransactionalOperation that describes the amount of the BalanceEntry was charged from or deposited to the account given.

#### Endpoints
- `api/v1/accounts`
    - **POST** transfers things and returns debit transactional operation ID in the Location header.
- `api/v1/transactionaloperations/{{id}}`
    - **GET** returns JSON representing transational operation entity.

#### Technologies and frameworks used
- .NET 5
- Visual Studio 2019 v. 16.11.1
- MS SQL Server 2019
- Entity Framework Core
- Docker & docker-compose
- NUnit
- Swagger

#### How to run
- Build the solution.
- Set docker-compose as a startup project and click Run in the Visual Studio 2019.
- Optionally: import `artifacts/GripItemTrade.postman_collection.json` collection to the Postman and setup `{{base_url}}` variable to be like `https://localhost:XXXX/api/v1/`. Alternatively, you can request via Swagger, which is opened by default, when starting the app.

#### TODOs
- Unit tests are containerized but need to be added to the pipeline.
- To run unit tests, the connection string host needs to be replaced by `localhost`. They should be able to run in a container as well as within Visual Studio.
- Secrets needs to be protected with Azure KeyVault.
- Authentication and authorization.
