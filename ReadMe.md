# Simple Transaction: Microservices sample architecture for .Net Core Application
### .Net Core 2.2 Sample With C#.Net, EF and SQL Server
* [Introduction](#Introduction)
* [Application Architecture](#Application-Architecture)
* [Design of Microservice](#Design-of-Microservice)
* [Security : JWT Token based Authentication](#Security--JWT-Token-based-Authentication)
* [Development Environment](#Development-Environment)
* [Technologies](#Technologies)
* [Opensource Tools Used](#Opensource-Tools-Used)
* [Cloud Platform Services](#Cloud-Platform-Services)
* [Database Design](#Database-Design)
* [WebApi Endpoints](#WebApi-Endpoints)
* [Solution Structure](#Solution-Structure)
* [Exception Handling](#Exception-Handling)
* [Db Concurrency Handling](#Db-Concurrency-Handling)
* [Azure AppInsights: Logging and Monitoring](#Azure-AppInsights-Logging-and-Monitoring)
* [Swagger: API Documentation](#Swagger-API-Documentation)
* [Postman Collection](#Postman-Collection)
* [How to run the application](#How-to-run-the-application)
* [Console App - Gateway Client](#Console-App---Gateway-Client)
---
## Introduction
This is a .Net Core sample application and an example of how to build and implement a microservices based back-end system for a simple automated banking feature like Balance, Deposit, Withdraw in ASP.NET Core Web API with C#.Net, Entity Framework and SQL Server. 

## Application Architecture

The sample application is build based on the microservices architecture. There are serveral advantages in building a application using Microservices architecture like Services can be developed, deployed and scaled independently.The below diagram shows the high level design of Back-end architecture.

- **Identity Microservice** - Authenticates user based on username, password and issues a JWT Bearer token which contains Claims-based identity information in it.
- **Transaction Microservice** - Handles account transactions like Get balance, deposit, withdraw
- **API Gateway** - Acts as a center point of entry to the back-end application, Provides data aggregation and communication path to microservices.

![Application Architecture](https://8dmbiq.dm.files.1drv.com/y4mKz6TDtiwhrfo2mdUgvzle36Bnj7PMCvY6fP6kixwU3c3_CMb_rnnYOxg9WKn8LMmc5F__p2w3NWJc0o1vmCFmhHd5hRbr0S4MnMFnx09qvdSHE_E_40H0pQOxE0om2T2czVDOAInkTXn4xgdx_FmRgo8OaBh2XYqFHTf2zmYmF71tqRqlLzlsYBo1x1_CvdCt8U6AbjMhYznbgeBkGUKPQ?width=625&height=243&cropmode=none)

## Design of Microservice
This diagram shows the internal design of the Transaction Microservice. The business logic and data logic related to transaction service is written in a seperate transaction processing framework. The framework receives input via Web Api and process those requests based on some simple rules. The transaction data is stored up in SQL database.

![Microservice design](https://8dk2lg.dm.files.1drv.com/y4md899yaH9aFP7Z1qhi_kCicZwQMYJWDA4SAdihporow8okXYUFcl-lp-2Awv5ldmlGmOEqwrxv3je-XaQqM7fnZZLzJKFzv7WDrC7Hyd2QLLglJfjNhWaFiCRJXzaXjghqK8y1XZJUuHAJiVdfl3_90NuPyNV-zsb5UOKBpRBbeFx3LpI0gPivXhIRBtFq6ZdInV5ub8r5U-Ibw9Zb-0YzQ?width=631&height=617&cropmode=none)


## Security : JWT Token based Authentication
JWT Token based authentication is implementated to secure the WebApi services. **Identity Microservice** acts as a Auth server and issues a valid token after validating the user credentitals. The API Gateway sends the token to the client. The client app uses the token for the subsequent request.

![JWT Token based Security](https://h9yrga.dm.files.1drv.com/y4mCbiAcoeieS5tBZu_z1z1z42C8eoVGWUmC_re1VkLWpxWtywvsOBH73brVXA4gzKm6G59h3b3vbUVF1C3jbYRlpf-7t-faZE4m8-wYplZusss5Fm-71AH87c1aXlKoULtFoUNl5Oh9h6nZDDfgLXeo_LKOH8Q0b4BGVTpg1w7TcCZQPkX5tBZtSiQj67JGqsg4lySz2ghzB9R9ArGtaA7wA?width=702&height=422&cropmode=none)

## Development Environment

- [.Net Core 2.2 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio .Net 2017](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Management Studio 17.9.1](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)

## Technologies
- C#.NET
- ASP.NET WEB API Core
- SQL Server

## Opensource Tools Used
- Automapper (For object-to-object mapping)
- Entity Framework Core (For Data Access)
- Swashbucke (For API Documentation)
- XUnit (For Unit test case)
- Ocelot (For API Gateway Aggregation)

## Cloud Platform Services
- Azure App Insights (For Logging and Monitoring)
- Azure SQL Database (For Data store)

## Database Design
![Database Design](https://8dmprw.dm.files.1drv.com/y4mFOnWFyfydK_fr_Cm6EA0spR2Ms8gG4XHVs01AO_pJ04-rHLI_GL6gWbEcXL2FIIDJN4AmAHs4K7o_BzO22oAuTP1hyT1wJdeOx_5kRI9J0t-XLRSkIHenLo0n7xbEcanL2luIHM16WawZ5__xJmMATY5YK8O8UT8P8W8CBp_bWnbsxcndOtUBN4E34tJHGqNb1GC2-hCW2fKKkHFMXo56w?width=1226&height=428&cropmode=none)

## WebApi Endpoints
The application has four API endpoints configured in the API Gateway to demonstrate the features with token based security options enabled. These routes are exposed to the client app to cosume the back-end services. 

### End-points configured and accessible through API Gateway

1. Route: **"/user/authenticate"** [HttpPost] - To authenticate user and issue a token
2. Route: **"/account/balance"** [HttpGet] - To retrieve account balance.
3. Route: **"/account/deposit"** [HttpPost] - To deposit amount in an account.
4. Route: **"/account/withdraw"** [HttpPost] - To withdraw amount from an account.

### End-points implemented at the Microservice level

1. Route: **"/api/user/authenticate"** [HttpPost]- To authenticate user and issue a token
2. Route: **"/api/account/balance"** [HttpGet]- To retrieve account balance.
3. Route: **"/api/account/deposit"** [HttpPost]- To deposit amount in an account.
4. Route: **"/api/account/withdraw"** [HttpPost]- To withdraw amount from an account.

## Solution Structure
![Solution Structure](https://h9x1lw.dm.files.1drv.com/y4mWm9vkzmf7oSUvJGiXl7JvkGz2FFOPs2-A3d2qH1PieYR4Wb55NseX4yOFl5igZTB5gzLW3guxaBqyyuhk6cyHEVjVy3vHt_cYHzwGg6jKvAPW2HN1evRmFWZkSii082F00RBDUozTBslIzMH6wEgyRWJmNpb14WEYWOZjBV7SG4WdkC9RZ7DKvGDNHoiBFctU9b3yEZnAo-RhL2Hm-ED-g?width=448&height=940&cropmode=none)

- **Identity.WebApi**
    - Handles the authentication part using username, password as input parameter and issues a JWT Bearer token with Claims-Identity info in it.
- **Transaction.WebApi** 
    - Supports three http methods 'Balance', 'Deposit' and 'Withdraw'. Receives http request for these methods.
    - Handles exception through a middleware
    - Reads Identity information from the Authorization Header which contains the Bearer token
    - Calls the appropriate function in the 'Transaction' framework
    - Returns the transaction response result back to the client
- **Transaction.Framework** 
    - Defines the interface for the repository (data) layer and service (business) layer
    - Defines the domain model (Business Objects) and Entity Model (Data Model)
    - Defines the business exceptions and domain model validation
    - Defines the required data types for the framework 'Struct', 'Enum', 'Consants'
    - Implements the business logic to perform the required account transactions
    - Implements the data logic to read and update the data from and to the SQL database
    - Performs the task of mapping the domain model to entity model and vice versa 
    - Handles the db update concurrency conflict
    - Registers the Interfaces and its Implementation in to Service Collection through dependency injection
- **Gateway.WebApi** 
    - Validates the incoming Http request by checking for authorized JWT token in it.
    - Reroute the Http request to a downstream service.
- **SimpleBanking.ConsoleApp**
    - A console client app that connects to Api Gateway, can be used to login with username, password and perform transactions like 'Balance', 'Deposit' and 'Withdraw' against a account.

## Exception Handling
A Middleware is written to handle the exceptions and it is registered in the startup to run as part of http request. Every http request, passes through this exception handling middleware and then executes the Web API controller action method. 

* If the action method is successfull then the success response is send back to the client. 
* If any exception is thrown by the action method, then the exception is caught and handled by the Middleware and appropriate response is sent back to the client.

![Exception Handler Middleware](https://h9wqda.dm.files.1drv.com/y4mgc5I1iveH8tv63QAu-nSpHVmAAHNFMb9J4KRpywPRZsM7orJiFBKAKEG-wV9r1-Ox7gsODTJZFlnMajsyedcfccUWU25GTswug3z47cr9S4itzbCkCuSG_SHVhZG91uwxvQMLnhg6TaHwOwvBrKrTI3XMzLt86TwjZHyKw4ow6vZ5372OenRnyOtfUFhFtbzThwKD2V3N2GX9v8DrLDJZw?width=431&height=371&cropmode=none)

```
public async Task InvokeAsync(HttpContext context, RequestDelegate next)
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        var message = CreateMessage(context, ex);
        _logger.LogError(message, ex);

        await HandleExceptionAsync(context, ex);
    }
}
```
## Db Concurrency Handling

Db concurrency is related to a conflict when multiple transactions trying to update the same data in the database at the same time. In the below diagram, if you see that Transaction 1 and Transaction 2 are against the same account, one trying to deposit amount into account and the other system tring to Withdraw amount from the account at the same time. The framework contains two logical layers, one handles the Business Logic and the other handles the Data logic. 

![Db Concurrency Update Exception](https://h9wjxq.dm.files.1drv.com/y4mQaKTurkARMHvgn2Btinv1zGVwyGPEiDN8n2hoiNkj6dqFsrkVRBD1ivMGlyCw_wyS2uIB5acPPZHGGJfYMZHtKJ7_ipf7Ltj--wdVJmQYiZP3tsR7CSEq_JVTz8eoA86pBrmy6aYT6quacE5PReiczk71Q11NUjnGSnbuN_0LuvheU5L2ns0scFeEBy3Szu9IoUq-uvDVUJ2Fagj8Ftlsw?width=921&height=536&cropmode=none)

When a data is read from the DB and when business logic is applied to the data, at this context, there will be three different states for the values relating to the same record.

- **Database values** are the values currently stored in the database.
- **Original values** are the values that were originally retrieved from the database
- **Current values** are the new values that application attempting to write to the database.

The state of the values in each of the transaction produces a conflict when the system attempts to save the changes and identifies using the concurrency token that the values being updated to the database are not the Original values that was read from the database and it throws DbUpdateConcurrencyException.

[Reference: docs.microsoft.com](https://docs.microsoft.com/en-us/ef/core/saving/concurrency)

The general approach to handle the concurrency conflict is:

1. Catch **DbUpdateConcurrencyException** during SaveChanges
2. Use **DbUpdateConcurrencyException.Entries** to prepare a new set of changes for the affected entities.
3. **Refresh the original values** of the concurrency token to reflect the current values in the database.
4. **Retry the process** until no conflicts occur.

```
while (!isSaved)
{
    try
    {
        await _dbContext.SaveChangesAsync();
        isSaved = true;
    }
    catch (DbUpdateConcurrencyException ex)
    {
        foreach (var entry in ex.Entries)
        {
            if (entry.Entity is AccountSummaryEntity)
            {
                var databaseValues = entry.GetDatabaseValues();

                if (databaseValues != null)
                {
                    entry.OriginalValues.SetValues(databaseValues);
                    CalculateNewBalance();

                    void CalculateNewBalance()
                    {
                        var balance = (decimal)entry.OriginalValues["Balance"];
                        var amount = accountTransactionEntity.Amount;

                        if (accountTransactionEntity.TransactionType == TransactionType.Deposit.ToString())
                        {
                            accountSummaryEntity.Balance =
                            balance += amount;
                        }
                        else if (accountTransactionEntity.TransactionType == TransactionType.Withdrawal.ToString())
                        {
                            if(amount > balance)
                                throw new InsufficientBalanceException();

                            accountSummaryEntity.Balance =
                            balance -= amount;
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}  
```

## Azure AppInsights: Logging and Monitoring

Azure AppInsights integrated into the "Transaction Microservice" for collecting the application Telemetry.

```
public void ConfigureServices(IServiceCollection services)
{           
   services.AddApplicationInsightsTelemetry(Configuration);           
}
```

AppInsights SDK for Asp.Net Core provides an extension method AddApplicationInsights on ILoggerFactory to configure logging. All transactions related to Deposit and Withdraw are logged through ILogger into AppInsights logs.

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{
   log.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);     
}
```

To use AppInsights, you need to have a Azure account and  create a AppInsights instance in the Azure Portal for your application, that will give you an instrumentation key which should be configured in the appsettings.json

```
 "ApplicationInsights": {
    "InstrumentationKey": "<Your Instrumentation Key>"
  },
```
---
## Swagger: API Documentation

Swashbuckle Nuget package added to the "Transaction Microservice" and Swagger Middleware configured in the startup.cs for API documentation. when running the WebApi service, the swagger UI can be accessed through the swagger endpoint "/swagger".

```
public void ConfigureServices(IServiceCollection services)
{            
     services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new Info { Title = "Simple Transaction Processing", Version = "v1" });
     });
}
```

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{           
     app.UseSwagger();
     app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Transaction Processing v1");
     });           
}
```

![Swagger API Doc](https://h9wqrg.dm.files.1drv.com/y4m_QKAwbWqvjgqVire-cVSue_EKgCJsNEwUca9MztPMbsMlDBmFtaIQbllXjiLVwZCJnittfbHBYQ3DiQwnyZrJ2Tz_qf8Paqms91ZVgWUm8_Wcc9b39y9_PbYSxtQ0KhA5u9E4FBkg6aNDgL207ZODhVQl8UgQyYpAV1II_UVmSnEfD5SionZ0kwbSZCnsMyv7kK-e7ym00Nk5sjbvlODiw?width=1892&height=874&cropmode=none)

---
## Postman Collection

Download the postman collection from [here](https://1drv.ms/u/s!AshsJMsiN-avgQCgr5NIFpZCoM5r) to run the api endpoints through gateway

![Postman](https://h9ykoq.dm.files.1drv.com/y4mROgcn_on2fHOuDePdeG-CfHybIpOETMHlCfLqoBJIUqLa-PFKk8InJ2Ei6XLkHyrS7dXtF0htZQnHyjFX3fZ-lDHAi93QY568uJRbPuJGHBANeBpd3rt9A4iBGCXf3akpCHEumuB7kLPSILOww_U6HFHp5UBF8aylwj5DcCSxJPt161RfVKDFKjyNuK5yxYqc--M9XbTTJ12mqZL-dtrbA?width=1898&height=1026&cropmode=none)

## How to run the application

1. Download the Sql script from [here](https://1drv.ms/u/s!AshsJMsiN-avgQILUXHhHHp1oOVa), 
2. Run the scirpt against SQL server to create the necessary tables and sample data
3. Open the solution (.sln) in Visual Studio 2017 or later version
4. Configure the SQL connection string in Transaction.WebApi -> Appsettings.json file
5. Configure the AppInsights Instrumentation Key in Transaction.WebApi -> Appsettings.json file. If you dont  have a key or don't require logs then comment the AppInsight related code in Startup.cs file 
6. Check the Identity.WebApi -> UserService.cs file for Identity info. User details are hard coded for few accounts in Identity service which can be used to run the app. Same details shown in the below table.
7. Run the following projects in the solution
    - Identity.WebApi
    - Transaction.WebApi
    - Gateway.WebApi
    - SimpleBanking.ConsoleApp
8. Gateway host and port should be configured correctly in the ConsoleApp
9. Idenity and Transaction service host and port should be configured correctly in the gateway -> configuration.json 

- Sample data to test

| Account Number | Currency | Username | Password |
| -------- | -------- | -------- | -------- |
| 3628101    | EUR     | speter    | test@123     |
| 3637897    | EUR     | gwoodhouse| pass@123     |
| 3648755    | EUR     | jsmith    | admin@123    |


## Console App - Gateway Client


![Console App](https://ulccxq.dm.files.1drv.com/y4m672RTTTxmbasRn2Ve8aElEASEJ282kImiYfISbt541JIB9x-LzJGYMWgFwRqGPA7F0OZEuR4OCJ4Vs6JhcSoy1min8jZQ5jjqeW2J_Ayd50dVYFh20qHCXQsuSe6lPuBk1MgiEDnaf7dOdXihgxi-qfbv2Ezg83g_g2hZOHnsi8kO4XMJyOEIFjILi4bkN4OTz1cuXZwNWfFY_VUQc9iNw?width=812&height=939&cropmode=none)