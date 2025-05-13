# How to run

Download and install:

- [Docker](https://docs.docker.com/get-started/get-docker/)
- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [Postman](https://www.postman.com/downloads/) (for integration tests only)

Navigate to folder `template\backend` and run the following command:

```
docker compose up -d
```

After the containers are up you should be able to access the application on https://localhost:8081/swagger/index.html

## Running the unit tests

Navigate to folder `template\backend` and run the following command:

```
dotnet test Ambev.DeveloperEvaluation.sln
```

## Running the integration tests

- Open `Postman`, click on the `Import` button and select or drag and drop the file `template\backend\Developer Evaluation.postman_collection.json`.
- In the imported collection, click on `Run` and then `Run Developer Evaluation`.

This will execute each request of the collection sequentially, you can also execute the requests manually and check the `Test Results` tab.

## Executing with Docker Compose in Visual Studio

Before executing the application in Visual Studio make sure to clean up existing containers to avoid conflicts with Visual Studio Container Tools. To do that you can execute the following command in the folder `template\backend`:

```
docker compose down
```

After that you should be able to use `Debug > Run` to start the application. Make sure to select `Docker Compose` as the launch profile.

# Architecture

The project follows most of the initial template which applies a `Clean Architecture` approach, using `Mediatr`, `AutoMapper`, `FluentValidation`, `Postgre` with `Entity Framework` for database management and `Faker` and `NSubstitute` for unit testing, a few changes were applied to simplify and speed up the solution development considering the available time to complete it:

- Mapping is applied only in the Application layer using shared classes (`SaleDto` and `SaleItemDto`) across the `Sales` endpoints. I decided to do that to also minimize the use of mappings and improve reusability.

- `Postman` is used for integration/functional tests, another possibility would be using [Testcontainers](https://dotnet.testcontainers.org/`), but I decided to go with `Postman` since is a more easy to set up and straightforward approach.


## Business rules implementation

The project implements all the business rules mentioned + generation of the events `SaleCreated`, `SaleModifeid`, `SaleCancelled` using a logging approach for demostration purposes (this would be replaced by a message broker like Kafka or RabbitMQ  in a real application).

For the "discount tier" rules I decided to implement it both as a validation and also as a business rule. That is, if the client sends a payload with a `discount` field present this is validated to make sure it adheres with the rules. The client can also omit this field - since `discount` is `nullable` - and in this case backend will automatically apply the discount following the same rules. This was done to given flexibility to the implementation.

## Assumptions

Some rules are open to interpretation, so the following assumptions were made:

- `Cancelled` items are disconsidered when calculating the sale total value.
- `SaleCancelled` is fired when all the items of the Sale are cancelled (can only happens when the sale is changed).

## Considerations

This solution is considering the context of a evaluation project, so external entities, user authentication and security concerns are not covered, for the sake of clarity some changes would be necessary if considering a real production application:

- `Price` is currently received in the payload and not validated (is exposed so any user can change it), a more suitable approach will be receiving only the `ProductId` in the payload and always fetching the price from the database (like a `Products` table). 
- Related entities ID's, like `UserId` (customer), `BranchId` and `ProductId` are fictional and not validated, in a real app this would be validated in this service itself (if the entity is part of the domain context) or coming from another database/service (if the entity is from a external service/another domain).