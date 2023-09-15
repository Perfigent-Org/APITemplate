# API Core Template

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Version](https://img.shields.io/badge/version-1.0.0-green.svg)

The API Core Template is a versatile and dynamic solution for quickly building API projects using .NET Core and .NET Standard. It provides a set of essential components and templates to accelerate your API development process, allowing you to focus on implementing your business logic without the hassle of setting up boilerplate code.

## Project Components

### 1. Core Repository using Dapper

The Core Repository is a foundational component that leverages the Dapper micro ORM to interact with your database. It provides a streamlined and efficient way to perform database operations, making data access straightforward.

#### Key Features

- **CRUD Operations:** The Core Repository includes a set of base methods for common CRUD (Create, Read, Update, Delete) operations, allowing you to work with your data effortlessly.

  - `Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel);` - Retrieve all records asynchronously.
  
  - `Task<IEnumerable<T>> GetAllAsync(int[] ids, CancellationToken cancel);` - Retrieve records by their IDs asynchronously.
  
  - `Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);` - Retrieve records based on a property's value asynchronously.
  
  - `Task<IEnumerable<T>> GetAllByOffsetAsync(int pageNumber, int pageSize, CancellationToken cancel);` - Retrieve records by offset and page size asynchronously.
  
  - `Task<IEnumerable<T>> GetAllByOffsetAsync(string columnName, string value, int pageNumber, int pageSize, CancellationToken cancel);` - Retrieve records by a specific column value, offset, and page size asynchronously.
  
  - `Task<IEnumerable<T>> SearchAsync(string columnName, string value, CancellationToken cancel);` - Search for records based on a column's value asynchronously.
  
  - `Task<T> GetAsync(int id, CancellationToken cancel);` - Get a single record by its ID asynchronously.
  
  - `Task<T> GetAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);` - Get a single record by a property's value asynchronously.
  
  - `Task<int> GetTotalCountAsync(CancellationToken cancel);` - Get the total count of records asynchronously.
  
  - `Task<int> GetTotalCountAsync(string columnName, string search, CancellationToken cancel);` - Get the total count of records based on a column's value and search term asynchronously.
  
  - `Task<IEnumerable<IdName>> GetNamesWithIdAsync(CancellationToken cancel);` - Retrieve record IDs and names asynchronously.
  
  - `Task DeleteAsync(int id, CancellationToken cancel);` - Delete a record by its ID asynchronously.
  
  - `Task DeleteAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);` - Delete records based on a property's value asynchronously.

**Note:** To access these repository methods, it's essential to set the "int Id" as the primary key in your database table. These methods rely on the presence of a primary key to perform data operations efficiently.

### 2. Core History Repository using Dapper

Building upon the Core Repository, the Core History Repository extends its functionality to include historical data tracking. It enables you to manage and query historical records efficiently.

#### Key Features

- **Historical Data Tracking:** The Core History Repository provides a set of pre-defined methods for efficiently managing and querying historical records.

  - `Task<IEnumerable<T>> GetAsync(int id, int pageNumber, int pageSize, CancellationToken cancel);` - Retrieve historical records by ID, paginated for easy navigation.
  
  - `Task<IEnumerable<T>> GetAsync(int id, string startTime, string endTime, CancellationToken cancel);` - Retrieve historical records by ID within a specified time range.
  
  - `Task<IEnumerable<T>> GetAsync(int[] ids, string startTime, string endTime, CancellationToken cancel);` - Retrieve historical records for multiple IDs within a specified time range.
  
  - `Task<int> GetTotalCountAsync(int id, CancellationToken cancel);` - Get the total count of historical records for a specific ID.
  
**Note:** To access the History Repository, users must convert the corresponding table to a temporal table. Additionally, ensure that the table schema matches `[History.TableName]` to enable efficient historical data tracking.

The History Repository simplifies historical data management, making it easier to access and query past records without extensive manual configuration.

### 3. Core Server Data Factory

The Core Server Data Factory is a crucial component that streamlines the creation and management of server data objects within your API project. It simplifies the process of working with server data, making it easy to integrate into your API.

#### Key Features

- **Server Data Object Management:** The Core Server Data Factory simplifies the creation and management of server data objects. It provides a structured and efficient approach to working with data within your API.

**Note:** To access the newly created repositories within the Server Data Factory, it's essential to initialize them in the ServerData class. Initializing repositories in this class allows seamless integration and usage within the factory.

This note highlights the importance of initializing repositories in the ServerData class for smooth access and usage within the Core Server Data Factory.

### 4. Core API Template

The Core API Template forms the core of your API project. It provides a comprehensive set of API endpoints, controllers, and routing configurations. This template serves as a robust starting point for your API development, allowing you to swiftly adapt it to your specific use case by implementing your business logic.

#### Key Features

- **API Endpoints and Controllers:** The Core API Template includes a variety of pre-built API endpoints and controllers that cater to common use cases. These endpoints are designed to facilitate interactions with your data and business logic.

  - **Pre-Built APIs:** Below is a list of some of the pre-built APIs available in the Core API Template:

    - `public async Task<IActionResult> GetAllAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancel = default)` - Retrieve all records asynchronously.

    - `public async Task<IActionResult> GetAllBySearchAsync(string columnName, string searchValue, int? pageNumber = null, int? pageSize = null, CancellationToken cancel = default)` - Retrieve records based on a search value asynchronously.

    - `public async Task<IActionResult> GetTotalCountAsync(CancellationToken cancel)` - Get the total count of records asynchronously.

    - `public async Task<IActionResult> GetTotalCountBySearchAsync(string columnName, string searchValue, CancellationToken cancel)` - Get the total count of records based on a search value asynchronously.

    - `public async Task<IActionResult> GetAsync(int id, CancellationToken cancel)` - Get a single record by its ID asynchronously.

    - `public async Task<IActionResult> GetHistoryTotalCountAsync(int id, CancellationToken cancel)` - Get the total count of historical records for a specific ID asynchronously.

    - `public async Task<IActionResult> GetHistoryAsync(int id, int pageNumber, int pageSize, CancellationToken cancel = default)` - Retrieve historical records by ID, paginated for easy navigation.

**Note:** When creating APIs within the Core API Template, it's imperative to include the Swagger attribute. Without this attribute, NSwag will not be able to generate the client code. Ensure that your API methods are decorated with Swagger attributes, as demonstrated in the example in a previous note.

```csharp
[HttpGet, Route("GetAllAsync", Order = 1)]
[SwaggerResponse((int)HttpStatusCode.OK, null, typeof(IEnumerable<UsersDetailsModel>))]
public async Task<IActionResult> GetAllAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancel = default)
```

Including these pre-built APIs in your Core API Template accelerates your development process by providing common functionality that can be easily customized to meet your specific requirements.

### 5. Auto-Generated API Client (using NSwag)

This template includes an auto-generated API client powered by NSwag. NSwag simplifies the process of generating API clients, ensuring that consumers can easily interact with your API without having to handle complex HTTP requests and responses.

#### Key Features

- **Auto-Generated API Client:** The API client provided in this template is auto-generated by NSwag. It offers a seamless way for clients to interact with your API, abstracting away the underlying HTTP complexities.

  - **Simplified API Client Management:** We've included an `ApiClient` class and `IApiClient` interface to simplify the management of auto-generated clients. Users can easily integrate newly created clients into these classes and interfaces for convenient access to their APIs.

  - **Support for TypeScript Clients:** In addition to C# clients, users have the option to generate TypeScript files to access the API from TypeScript-based applications. This flexibility ensures that developers can choose the client technology that best suits their project needs.

**Configuring NSwag for Client Generation:**

Before you can generate the client using the provided PowerShell script (PS1 *GenerateApiClient.ps1*), you need to configure NSwag on your local machine. Here are the steps to set up NSwag:

Verifying NSwag Installation and Version

Before generating API clients with NSwag, it's important to verify that NSwag is installed on your machine and check its version. Follow these steps to ensure NSwag is ready for use.

To configure NSwag for client generation, follow these steps:

**1. Verify NSwag Installation:**

Open a command prompt or terminal window and enter the following command to check if NSwag is installed:

```bash
  nswag --version
```

**2. Updating NSwag (If Already Installed):***

```bash
  npm update nswag -g
```

**3. Install NSwag Globally:**

NSwag is a Swagger 2.0 API (OpenAPI) toolchain for .NET and other platforms. It can be used to generate client code from Swagger specifications. To install NSwag globally, run the following npm command:

```bash
  npm install nswag -g
```

**4. Change Runtime:**

Depending on your project's runtime, you may need to specify the runtime version when using NSwag. For example, to use .NET Core 2.1, run the following command:

```bash
  nswag version /runtime:NetCore21
```
   
**5. Additional Details:**

For more detailed information on NSwag and its capabilities, you can visit the official npm page for NSwag:

[NSwag on npm](https://www.npmjs.com/package/nswag)

**4. Download NSwagStudio:**

To simplify the configuration and usage of NSwag, you can download NSwagStudio, which provides a graphical user interface for NSwag. You can find the NSwagStudio MSI installer here:

Download [NSwagStudio.msi](https://github.com/RicoSuter/NSwag/releases)

By following these steps and using NSwag in combination with our template, you can quickly generate API clients and streamline interactions with your API.

### 6. API Models

API Models provide a standardized way to define the data structures used within your API. They help maintain consistency and clarity in your API's data representation.

### 7. API Client Test Cases

To ensure the reliability and robustness of your API client, this template includes a set of test cases. These test cases cover various scenarios and can serve as a starting point for your API client testing.

## Getting Started

To get started with the API Core Template, follow these steps:

1. Clone this repository to your local development environment.
2. Customize the Core API Template by implementing your business logic, defining your API routes, and configuring your database connections.
3. Utilize the Core Repository and Core History Repository for efficient data access.
4. Use the auto-generated API client to consume your API in other applications.
5. Run the provided API client test cases to validate your API client's functionality.

## Contribution

Contributions to the API Core Template project are welcomed! Whether it's bug fixes, new features, or improvements, your contributions can help make this template even more valuable to the community. Please review our [contribution guidelines](CONTRIBUTING.md) for more information on how to contribute.

## License

This project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute it in your projects.

---

Simplify your API development process with the API Core Template. Build APIs quickly and efficiently, focusing on what matters most—your unique business logic.

## Contact Information

If you have any questions, feedback, or encounter issues, please don't hesitate to reach out:

- Email: [hello@perfigent.com](mailto:hello@perfigent.com)

---

**LIFE RUNS ON CODE**
