# Phonebook System

## Overview
This is a simple Phonebook System that allows an admin to manage contacts stored in a PostgreSQL database. The system consists of two separate projects:
- **API Project**: Developed using .NET Core, applying the Onion Architecture.
- **Web Project**: Built with Angular 16, featuring a minimal yet functional UI.

## Features
- **Add Contact**: Admin can add new contacts.
- **Remove Contact**: Admin can delete existing contacts.
- **Edit Contact**: Admin can update contact details.
- **Search Contact**: Search by any part of the name, phone number, or email.
- **List Contacts**: Display all saved contacts from the database.

## Technology Stack
### Backend (API)
- **.NET Core**
- **Entity Framework Core**
- **PostgreSQL**
- **Onion Architecture**
- **JWT Authentication (if applicable)**

### Frontend (Web)
- **Angular 16**
- **Bootstrap (for styling)**
- **Angular Services for API communication**

## API Endpoints
| Method | Endpoint         | Description |
|--------|----------------|-------------|
| `GET`  | `/api/contacts` | Get all contacts |
| `POST` | `/api/contacts` | Add a new contact |
| `PUT`  | `/api/contacts/{id}` | Update a contact by ID |
| `DELETE` | `/api/contacts/{id}` | Remove a contact by ID |
| `GET`  | `/api/contacts/search?query={value}` | Search contacts by name, phone, or email |

## Installation & Setup
### Backend (API Project)
1. Clone the repository.
2. Navigate to the API project directory.
3. Install dependencies:
   ```sh
   dotnet restore
   ```
4. Configure the PostgreSQL connection string in `appsettings.json`.
5. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
6. Run the API:
   ```sh
   dotnet run
   ```

### Frontend (Web Project)
1. Navigate to the Web project directory.
2. Install dependencies:
   ```sh
   npm install
   ```
3. Run the Angular application:
   ```sh
   ng serve
   ```
4. Open the browser and navigate to `http://localhost:4200`.

## Notes
- Ensure PostgreSQL is running and accessible.
- Modify API base URL in the Angular project (`environment.ts`) if needed.

## Contact
For any questions, feel free to reach out.

---

This README is designed to be professional and informative for HR review. Let me know if you need any modifications! 🚀

