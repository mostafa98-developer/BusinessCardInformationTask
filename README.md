# BusinessCardInformationTask

## Overview

This is a .NET Core API built with ASP.NET Core 8 and Entity Framework Core. The API provides functionalities to manage [your main entity, e.g., "Business Cards"], allowing users to perform CRUD operations and other related tasks.

## Features

- **RESTful API**: Follows REST principles for resource management.
- **Entity Framework Core**: Uses EF Core for data access and manipulation.
- **Database Support**:  SQL Server
- **Unit test**: Uses Moq For mocking the service layer (IBusinessCardService) and validator (IValidator<BusinessCard>).
- **Error Handling**: Proper error handling and responses.

## Prerequisites

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)
- [Database system (SQL Server)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (if applicable)
- [Visual Studio / Visual Studio Code / JetBrains Rider](https://visualstudio.microsoft.com/) for development (optional)

## Getting Started

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/mostafa98-developer/BusinessCardInformationTask.git
   cd your-repo-name

   Restore the dependencies:

2. dotnet restore
3. Update the database connection string in appsettings.json:
4. apply migrations
   dotnet ef database update
5. use iis express and set the BusinessCardInformation project as startup project


## API Endpoints

| Method | Endpoint                     | Description                         |
|--------|------------------------------|-------------------------------------|
| GET    | `/api/businesscards`         | Get all business cards              |
| GET    | `/api/businesscards/{id}`    | Get a specific business card        |
| POST   | `/api/businesscards`         | Create a new business card          |
| PUT    | `/api/businesscards/{id}`    | Update an existing business card    |
| DELETE | `/api/businesscards/{id}`    | Delete a specific business card     |
| POST   | `/api/businesscards/import`  | import a specific business card from file csv, xml and QRcode |
| GET    | `/api/businesscards/export/xml`   | export  business cards as  xml file  |
| GET    | `/api/businesscards/export/csv`   | export  business cards as  csv file    |
| POST   | `/api/businesscards/QRCodeReader`   | QRCodeReader to reading business cards from Qr code    |
| POST   | `/api/businesscards/bulk`   | Create a new business cards as bulk    |


## DataBase file .bak
  Restore the DB dump file inside the repository is unnecessary because we are using Entity Framework (EF) ORM, which will create the tables when the project runs.

## BusinessCardInformationWebApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.2.7.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Angular 18 Requirement
 Node. js version 18.19 or above

## Screenshots folder
  Contains a video recording of the project run