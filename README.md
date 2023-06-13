# Project README

## Demo

https://github.com/coder-koob/testprojectcleanarchitecture/blob/a979ff7e40476877ce603ff58e27972fe0c4ae9c/Test%20Project.mp4

## Project Overview

This project follows the principles of Clean Architecture, using .NET and CQRS patterns. The architecture consists of four main layers:

- **Web**: This is the entry point of the application. It handles HTTP requests and responses, and depends on the Application layer. (The only reason that the Web project is aware of the Infrastructure layer is for DI purposes).

- **Application**: This layer contains the business logic of the application, encapsulated in Commands and Queries following the CQRS pattern. It depends on the Domain layer.

- **Domain**: This layer contains the core business entities and logic of the application.

- **Infrastructure**: This layer contains classes for accessing external resources such as databases, file systems, web services, etc.

The project also uses Claims-Based Authentication managed by IdentityServer. Ideally in a real world scenario this would use an authorization code flow or implicit flow, but for the intent of this demo, M2M type tokens were used.

## State Management

The application uses Event Sourcing to store its state. Each change to the state of an application is captured in an event object. These event objects are persisted in a MongoDB event store. This way, the application's state is fully represented as a sequence of events.

The application separates read operations from write operations, with reads being served from a Redis cache. This pattern provides the benefits of high availability and horizontal scalability.

## Running the Project

To run this project locally in a Docker container, follow the steps below:

1. **Build the Docker image:**

    ```sh
    docker compose build
    ```

2. **Run the Docker container:**

    ```sh
    docker compose up
    ```

3. **Run the Application:**

    Navigate to the Web directory.

    ```sh
    cd Web
    ```

    Start the application.
    ```sh
    dotnet run
    ```

    This will start the application and all its dependencies (MongoDB and Redis) in their own Docker containers.

    The docker file to build the application has been setup, however there's an issue with the cert on the Identity server that still needs to be sorted out.

## Note on HTTPS:

The application uses HTTPS for secure communication. This requires an SSL/TLS certificate. For development purposes, a self-signed certificate is included with the application. 

To trust the self-signed certificate, run:

```sh
dotnet dev-certs https --trust
```

Remember, for production environments, you should use a certificate issued by a trusted Certificate Authority.

## Conclusion

The project demonstrates a modern application architecture that's scalable and maintainable. It uses some of the best practices of .NET development, such as CQRS for clearly separating concerns, Clean Architecture for decoupling business logic from infrastructure and UI, and Docker for creating a reproducible environment that's easy to set up on any machine. The use of MongoDB as an event store and Redis as a read model store illustrate patterns of state management that are suitable for complex, high-load applications.