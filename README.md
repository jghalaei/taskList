
# Task List Project

## Description
The Task List project is a comprehensive application designed with a microservice architecture. It's built using a variety of technologies primarily focusing on C#, JavaScript, and Docker.

## Project Structure
- **Docs**: Contains documentation files like 'apis.xlsx' for project overview and testing.
- **Pipelines**: Includes YAML files for CI/CD pipelines (`task-gateway.yml`, `task.api.yml`, `task.gateway.yml`, `user.api.yml`).
- **src (Source Code)**:
  - **.dockerignore & docker-Compose Files**: For Docker configurations.
  - **.vscode**: VSCode-specific settings and configurations.
  - **ApiGateWays**: Code for API gateways in the application.
  - **Client**: Front-end client application code.
  - **Generic**: Generic libraries or utilities used across services.
  - **Identity**: Code related to user identity and authentication.
  - **Services**: Individual microservices of the application.
  - **k8s**: Kubernetes configuration files.
  - **src.sln**: Solution file for the source code.
- **taskList.sln**: Main solution file for the entire project.

## Microservice Architecture
The application is structured into multiple microservices, each responsible for a specific function. This includes services for user identity, data handling, and API gateways. Kubernetes and Docker are used for containerization and orchestration.

## Installation & Setup
To set up the project:
1. Ensure Docker and Kubernetes are installed and configured.
2. Clone the repository.
3. Use `docker-compose` to build and run the services.

## Usage
After setting up, you can access the various services through their respective endpoints. The API documentation (apis.xlsx) in the Docs folder provides details on available APIs and their usage.

## Contributing
Contributions to the project are welcome. Please follow the standard fork-and-pull request workflow.

-----------------------------------------
## The tech stack of the project
1. Microservices
2. Clean Architecture
3. Ocelot API
4. MassTransit And RabbitMQ
5. Serilog
6. Mediatr
7. PostgreSQL and Cosmos DBd
8. Docker containers
9. Docker compose
10. CI/CD Pipelines using Azure DevOps
11. Kubernetes
12. React
13. Minimal APIs

### To be added:
- gRPC
- GraphQL
- Dapr
- And so On
