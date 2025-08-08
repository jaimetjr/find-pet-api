# Find Pet API 🐾

A comprehensive .NET 8 Web API for managing lost and found pets, built with Clean Architecture principles. This API provides authentication, pet management, real-time chat functionality, and push notifications.

## 📋 Table of Contents

- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Database Schema](#-database-schema)
- [Authentication](#-authentication)
- [Configuration](#-configuration)
- [Deployment](#-deployment)
- [Contributing](#-contributing)

## ✨ Features

### 🔐 Authentication & Authorization
- **Clerk Integration**: JWT-based authentication using Clerk
- **Role-based Authorization**: User roles and permissions
- **Self-or-Elevated Authorization**: Custom authorization policies

### 🐕 Pet Management
- **CRUD Operations**: Create, read, update, and delete pets
- **Image Management**: Upload and manage pet images via Azure Blob Storage
- **Pet Profiles**: Comprehensive pet information including breed, type, size, age, gender
- **Location Tracking**: Address and neighborhood information for lost/found pets

### 💬 Real-time Chat
- **SignalR Integration**: Real-time messaging between users
- **Chat Rooms**: Organized conversations for pet-related discussions
- **Message Status**: Read/unread message tracking

### 📱 Push Notifications
- **Expo Push Tokens**: Integration with Expo for mobile push notifications
- **Notification Preferences**: User-configurable notification settings

### 🌐 Internationalization
- **Multi-language Support**: English and Portuguese (Brazil)
- **Localized Validation Messages**: Culture-specific error messages

### 📊 Logging & Monitoring
- **Serilog Integration**: Structured logging with file and console sinks
- **Request Logging**: Middleware for tracking API requests
- **Global Exception Handling**: Centralized error handling

## 🏗️ Architecture

This project follows **Clean Architecture** principles with the following layers:

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                       │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────┐ │
│  │   API Controllers│  │   SignalR Hubs  │  │  Middleware │ │
│  └─────────────────┘  └─────────────────┘  └─────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                        │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────┐ │
│  │   Services      │  │   DTOs          │  │  Validators │ │
│  └─────────────────┘  └─────────────────┘  └─────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Domain Layer                             │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────┐ │
│  │   Entities      │  │   Enums         │  │  Interfaces │ │
│  └─────────────────┘  └─────────────────┘  └─────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                       │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────┐ │
│  │   Repositories  │  │   Data Context  │  │  Services   │ │
│  └─────────────────┘  └─────────────────┘  └─────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

## 🛠️ Technology Stack

### Core Framework
- **.NET 8**: Latest LTS version
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core 8**: ORM for database operations

### Database
- **PostgreSQL**: Primary database
- **Entity Framework Migrations**: Database versioning

### Authentication & Security
- **Clerk**: Authentication provider
- **JWT Bearer Tokens**: Stateless authentication
- **FluentValidation**: Input validation

### Cloud Services
- **Azure Blob Storage**: File storage for images
- **SignalR**: Real-time communication

### Logging & Monitoring
- **Serilog**: Structured logging
- **File & Console Sinks**: Multiple logging destinations

### Development Tools
- **Swagger/OpenAPI**: API documentation
- **AutoMapper**: Object mapping
- **FluentValidation**: Input validation

## 📁 Project Structure

```
find-pet-api/
├── API/                          # Presentation Layer
│   ├── Controllers/              # API Controllers
│   │   ├── AuthController.cs     # Authentication endpoints
│   │   ├── PetController.cs      # Pet management endpoints
│   │   ├── ChatController.cs     # Chat endpoints
│   │   └── BaseController.cs     # Base controller with common functionality
│   ├── Hubs/                     # SignalR Hubs
│   │   └── ChatHub.cs           # Real-time chat functionality
│   ├── Middleware/               # Custom middleware
│   │   ├── GlobalExceptionHandler.cs
│   │   └── RequestLoggingMiddleware.cs
│   ├── Configuration/            # API configuration
│   │   └── JwtSettings.cs
│   └── Program.cs               # Application entry point
├── Application/                  # Application Layer
│   ├── DTOs/                    # Data Transfer Objects
│   │   ├── Pet/                 # Pet-related DTOs
│   │   ├── User/                # User-related DTOs
│   │   └── Chat/                # Chat-related DTOs
│   ├── Services/                # Application services
│   │   ├── PetService.cs
│   │   ├── UserService.cs
│   │   ├── ChatService.cs
│   │   └── PushService.cs
│   ├── Interfaces/              # Service and repository interfaces
│   ├── Validators/              # FluentValidation validators
│   ├── Security/                # Security-related services
│   │   ├── JwtTokenGenerator.cs
│   │   └── PasswordHasher.cs
│   ├── Mapping/                 # AutoMapper profiles
│   └── Resources/               # Localization resources
├── Domain/                      # Domain Layer
│   ├── Entities/                # Domain entities
│   │   ├── Pet.cs
│   │   ├── User.cs
│   │   ├── Chat/
│   │   │   ├── ChatMessage.cs
│   │   │   └── ChatRoom.cs
│   │   └── Entity.cs
│   └── Enums/                   # Domain enums
│       ├── PetGender.cs
│       ├── PetSize.cs
│       ├── UserRole.cs
│       └── ApprovalStatus.cs
├── Infrastructure/              # Infrastructure Layer
│   ├── Data/                    # Data access
│   │   └── AppDataContext.cs    # EF Core DbContext
│   ├── Repositories/            # Repository implementations
│   ├── Configurations/          # EF Core configurations
│   ├── Migrations/              # Database migrations
│   ├── Storage/                 # File storage services
│   │   └── AzureBlobStorageService.cs
│   └── Services/                # Infrastructure services
├── IoC/                         # Dependency Injection
│   └── DependencyInjection.cs   # Service registration
└── Shared/                      # Shared components
```

## 🚀 Getting Started

### Prerequisites

- **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **PostgreSQL**: [Download here](https://www.postgresql.org/download/)
- **Visual Studio 2022** or **VS Code**: For development
- **Azure Account**: For blob storage (optional for local development)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/find-pet-api.git
   cd find-pet-api
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection**
   
   Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=findpetdb;Username=your_username;Password=your_password"
     }
   }
   ```

4. **Run database migrations**
   ```bash
   cd Infrastructure
   dotnet ef database update
   ```

5. **Configure Azure Blob Storage** (optional for local development)
   
   Add your Azure storage settings to `appsettings.json`:
   ```json
   {
     "AzureBlobStorage": {
       "ConnectionString": "your_connection_string",
       "ContainerName": "pet-images"
     }
   }
   ```

6. **Configure Clerk Authentication**
   
   Update the JWT settings in `appsettings.json`:
   ```json
   {
     "Jwt": {
       "Authority": "https://your-clerk-instance.clerk.accounts.dev"
     }
   }
   ```

7. **Run the application**
   ```bash
   cd API
   dotnet run
   ```

8. **Access the API**
   - **API Base URL**: `https://localhost:7125`
   - **Swagger Documentation**: `https://localhost:7125/swagger`

## 📚 API Documentation

### Authentication Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: multipart/form-data

{
  "email": "user@example.com",
  "name": "John Doe",
  "phone": "+1234567890",
  "notifications": true,
  "cpf": "12345678901"
}
```

#### Get User Profile
```http
GET /api/auth/me/{clerkId}
Authorization: Bearer {token}
```

#### Update User Profile
```http
PUT /api/auth/update/{userId}
Authorization: Bearer {token}
Content-Type: multipart/form-data

{
  "name": "Updated Name",
  "phone": "+1234567890",
  "bio": "User bio",
  "birthDate": "1990-01-01",
  "cpf": "12345678901",
  "address": "123 Main St",
  "neighborhood": "Downtown",
  "cep": "12345-678",
  "state": "CA",
  "city": "Los Angeles",
  "number": "123",
  "complement": "Apt 4B",
  "notifications": true,
  "contactType": "Phone"
}
```

#### Update Expo Push Token
```http
PATCH /api/auth/update-expo-push-token
Authorization: Bearer {token}
Content-Type: application/json

{
  "clerkId": "user_clerk_id",
  "expoPushToken": "ExponentPushToken[xxxxxxxxxxxxxxxxxxxxxx]"
}
```

### Pet Management Endpoints

#### Create Pet
```http
POST /api/pet
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Buddy",
  "typeId": "550e8400-e29b-41d4-a716-446655440000",
  "breedId": "550e8400-e29b-41d4-a716-446655440001",
  "gender": "Male",
  "size": "Medium",
  "age": 3,
  "bio": "Friendly golden retriever",
  "history": "Found in downtown area",
  "address": "123 Main St",
  "neighborhood": "Downtown",
  "cep": "12345-678",
  "state": "CA",
  "city": "Los Angeles",
  "number": "123",
  "complement": "Near the park"
}
```

#### Upload Pet Images
```http
POST /api/pet/images
Authorization: Bearer {token}
Content-Type: multipart/form-data

{
  "petId": "550e8400-e29b-41d4-a716-446655440000",
  "images": [file1, file2, file3]
}
```

#### Get All Pets
```http
GET /api/pet
Authorization: Bearer {token}
```

#### Get User's Pets
```http
GET /api/pet/GetAllPetsByUser
Authorization: Bearer {token}
```

#### Get Pet by ID
```http
GET /api/pet/{petId}
Authorization: Bearer {token}
```

#### Update Pet
```http
PUT /api/pet/{petId}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Updated Buddy",
  "bio": "Updated bio",
  "age": 4
}
```

#### Delete Pet
```http
DELETE /api/pet/{petId}
Authorization: Bearer {token}
```

#### Delete Pet Image
```http
DELETE /api/pet/{petId}/images/{imageId}
Authorization: Bearer {token}
```

### Chat Endpoints

#### Get All Chats
```http
GET /api/chat
Authorization: Bearer {token}
```

#### Mark Message as Seen
```http
POST /api/chat/MarkMessageAsSeen
Authorization: Bearer {token}
Content-Type: application/json

{
  "roomId": "550e8400-e29b-41d4-a716-446655440000"
}
```

### SignalR Chat Hub

Connect to the SignalR hub for real-time chat:
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start().then(() => {
    console.log("Connected to chat hub");
});
```

## 🗄️ Database Schema

### Core Entities

#### User
- `Id` (Guid, PK)
- `ClerkId` (string) - External Clerk user ID
- `Email` (string)
- `Name` (string)
- `BirthDate` (DateTime)
- `CPF` (string) - Brazilian tax ID
- `Phone` (string)
- `Address` (string)
- `Neighborhood` (string)
- `CEP` (string) - Brazilian postal code
- `State` (string)
- `City` (string)
- `Number` (string)
- `Complement` (string, nullable)
- `Bio` (string)
- `Avatar` (string, nullable)
- `Notifications` (bool)
- `ExpoPushToken` (string, nullable)
- `ContactType` (enum)
- `ApprovalStatus` (enum)
- `Role` (enum)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

#### Pet
- `Id` (Guid, PK)
- `ClerkId` (string) - Owner's Clerk ID
- `Name` (string)
- `TypeId` (Guid, FK) - References PetType
- `BreedId` (Guid, FK) - References PetBreed
- `Gender` (enum)
- `Size` (enum)
- `Age` (int)
- `Bio` (string)
- `History` (string)
- `Address` (string)
- `Neighborhood` (string)
- `CEP` (string)
- `State` (string)
- `City` (string)
- `Number` (string)
- `Complement` (string, nullable)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

#### ChatRoom
- `Id` (Guid, PK)
- `Name` (string)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

#### ChatMessage
- `Id` (Guid, PK)
- `RoomId` (Guid, FK) - References ChatRoom
- `SenderId` (string) - Clerk ID of sender
- `Content` (string)
- `IsSeen` (bool)
- `CreatedAt` (DateTime)

### Supporting Entities

#### PetType
- `Id` (Guid, PK)
- `Name` (string)
- `Description` (string)

#### PetBreed
- `Id` (Guid, PK)
- `Name` (string)
- `TypeId` (Guid, FK) - References PetType

#### PetImages
- `Id` (Guid, PK)
- `PetId` (Guid, FK) - References Pet
- `ImageUrl` (string)
- `CreatedAt` (DateTime)

#### Provider
- `Id` (Guid, PK)
- `UserId` (Guid, FK) - References User
- `Type` (enum)
- `Name` (string)
- `Description` (string)
- `ContactInfo` (string)

## 🔐 Authentication

This API uses **Clerk** for authentication with JWT Bearer tokens.

### JWT Configuration
- **Authority**: Clerk authentication server
- **Token Validation**: Validates issuer, lifetime, and signing key
- **Audience**: Not validated (configured for flexibility)

### Authorization Policies
- **Self-or-Elevated**: Users can only access their own resources unless they have elevated permissions
- **Role-based**: Different access levels based on user roles

### Getting a Token
1. Register/login through Clerk
2. Obtain JWT token from Clerk
3. Include token in Authorization header: `Bearer {token}`

## ⚙️ Configuration

### Environment Variables

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=findpetdb;Username=your_username;Password=your_password"
  },
  "Jwt": {
    "Authority": "https://your-clerk-instance.clerk.accounts.dev"
  },
  "AzureBlobStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=your_account;AccountKey=your_key;EndpointSuffix=core.windows.net",
    "ContainerName": "pet-images"
  },
  "Localization": {
    "DefaultLocalization": "en",
    "SupportedCultures": ["en", "pt-BR"]
  }
}
```

### Logging Configuration

The application uses Serilog with the following sinks:
- **Console**: Development logging
- **File**: Production logging with rolling files
- **Structured Logging**: JSON format for better parsing

## 🚀 Deployment

### Docker Deployment

1. **Build the Docker image**
   ```bash
   docker build -t find-pet-api .
   ```

2. **Run the container**
   ```bash
   docker run -p 8080:80 find-pet-api
   ```

### Azure Deployment

1. **Create Azure App Service**
2. **Configure connection strings and app settings**
3. **Deploy using Azure DevOps or GitHub Actions**

### Environment-Specific Configurations

- **Development**: Local PostgreSQL, local file storage
- **Staging**: Azure SQL Database, Azure Blob Storage
- **Production**: Azure SQL Database, Azure Blob Storage, SSL certificates

## 🤝 Contributing

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Commit your changes**: `git commit -m 'Add amazing feature'`
4. **Push to the branch**: `git push origin feature/amazing-feature`
5. **Open a Pull Request**

### Development Guidelines

- Follow Clean Architecture principles
- Write unit tests for business logic
- Use FluentValidation for input validation
- Follow C# coding conventions
- Add XML documentation for public APIs

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

For support and questions:
- Create an issue in the GitHub repository
- Contact the development team
- Check the [Wiki](https://github.com/yourusername/find-pet-api/wiki) for additional documentation

---

**Made with ❤️ for helping pets find their way home**
