# Trip Management System

## Overview
The Trip Management System is a web application designed to manage travel-related data, including trips, expenses, participants, and useful links. The application is built using ASP.NET Core and Entity Framework Core.

## Features
- Manage trips and related information
- Track expenses associated with trips
- Manage participants for each trip
- Store and retrieve useful links related to trips
- User authentication and authorization

## Technologies Used
- ASP.NET Core 8
- Entity Framework Core
- Microsoft SQL Server
- OData for querying

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server

### Installation
1. Clone the repository:

git clone https://github.com/yourusername/trip-management-system.git
cd trip-management-system

2. Set up the database:
    - Update the connection string in `appsettings.json` to point to your SQL Server instance.
    - Run the following command to apply migrations and create the database:
            dotnet ef database update

    3. Run the application:
dotnet run
