# Contacts Management System

A Windows Forms application built with C# and SQL Server for managing contacts. The project follows a multi-layer architecture that separates the user interface, business logic, and data access layers, making the application easier to maintain and extend.

## Features

* Add new contacts
* Update existing contacts
* Delete contacts
* Search contacts
* View detailed contact information
* SQL Server database integration
* Layered architecture (Presentation, Business, and Data Access Layers)

## Technologies Used

* C#
* Windows Forms
* SQL Server
* ADO.NET
* Visual Studio

## Architecture

The solution is organized into three layers:

```text
Presentation Layer
        ↓
Business Layer
        ↓
Data Access Layer
        ↓
SQL Server Database
```

### Presentation Layer

Handles the user interface and user interactions.

### Business Layer

Contains business logic and validation rules.

### Data Access Layer

Responsible for communicating with the database and executing SQL operations.

## Project Structure

```text
Contacts.sln
│
├── Contacts_PresentationLayer
│
├── Contacts_BusinessLayer
│
└── Contacts_DataAccessLayer
```

## Getting Started

### Prerequisites

* Visual Studio
* SQL Server
* .NET Framework

### Installation

1. Clone the repository:

```bash
git clone https://github.com/AliMohamedd/Contacts-WindowsForms.git
```

2. Open the solution in Visual Studio.

3. Create the database in SQL Server.

4. Configure the connection string.

5. Build and run the application.

## Database Configuration

Before running the application, update the connection string to match your SQL Server instance.

Example:

```csharp
Server=YOUR_SERVER_NAME;
Database=ContactsDB;
Trusted_Connection=True;
```

## Screenshots

Add screenshots of the application here.

### Main Screen

![Main Screen](screenshots/main-win.png)

### Contact Details

![Contact Details](screenshots/contact-details.png)

## Learning Objectives

This project was developed to practice and demonstrate:

* Object-Oriented Programming (OOP)
* Layered Architecture
* ADO.NET
* SQL Server Integration
* Windows Forms Development
* Software Design Principles

## Author

Ali Mohamed

GitHub: https://github.com/AliMohamedd

## License

This project is for educational and learning purposes.
