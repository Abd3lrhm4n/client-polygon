# Integration with polygon.io and Kendo UI for Angular

This solution is composed of multiple projects using a clean architecture design pattern. It includes a .NET 8 backend with a Web API and a Background worker, as well as an Angular 16 frontend. The backend communicates with a T-SQL database using Entity Framework Core with a code-first approach, and it integrates Hangfire for background processing.

## Solution Structure

- **Application**: Contains the business logic and application services.
- **Background**: A background worker project that uses Hangfire to handle scheduled and recurring tasks. This project is integrated with the Polygon.io API for job execution.
- **Client-App**: An Angular 16 frontend application used for managing clients, with features such as filtering, paging, and CRUD operations.
- **ClientCRUD**: A Web API built with .NET 8 that serves as the backend for the Client-App and provides endpoints for managing client data.
- **Domain**: Contains the core domain models and logic of the application.
- **Infrastructure**: Implements the infrastructure layer, including database access via Entity Framework Core, external service integrations, and the Hangfire setup.

## Prerequisites

1. **.NET 8 SDK**
2. **Node.js & npm**
3. **Angular CLI**
4. **SQL Server**
5. **Docker (Optional for containerization)**
6. **Hangfire (Configured in the Background project)**

## Setup Instructions

### Step 1: Clone the Repository

- ```bash
  git clone <repository-url>
  cd TechForceInterviewSolution
  ```

### Step 2: Configure the Database

1. Update the connection strings in the appsettings.json files located in the ClientCRUD and Background projects.
2. Run the following command to apply migrations and update the database:

   ```bash
   dotnet ef database update --project Infrastructure
   ```

### Step 3: Run the Backend Projects

1. Navigate to the solution root.
2. Open a terminal and run the Web API project:

   ```bash
   cd ClientCRUD
   dotnet run
   ```

3. In another terminal, run the Background worker project:
   ```bash
   cd Background
   dotnet run
   ```

### Step 4: Run the Angular Client-App

1. Navigate to the client-app directory:
   ```bash
   cd client-app
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the Angular development server:
   ```bash
   ng serve
   ```
   The app will be accessible at http://localhost:4200.

# Publishing to IIS

### Prerequisites

1. Install IIS: Make sure IIS is installed on your server or local machine.
2. Install the .NET Hosting Bundle: Download and install the .NET 8 Hosting Bundle from the .NET website to enable ASP.NET Core hosting on IIS.

### Publish the Web API Project

1. Open a terminal and navigate to the ClientCRUD project directory:
   ```bash
   cd ClientCRUD
   ```
2. Publish the project:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
3. Copy the published output (./publish directory) to your IIS server. You can use FTP, a file share, or other methods to transfer the files.

## Configure IIS

1. **Open IIS Manager:** Launch IIS Manager from the Start menu or by typing inetmgr in the Run dialog.

2. **Create a New Site:**

   - Right-click on the "Sites" node and select "Add Website..."
   - Set the "Site name" (e.g., TechForceClientCRUD).
   - Set the "Physical path" to the directory where you copied the published files.
   - Configure the "Binding" settings (e.g., port 80 for HTTP or port 443 for HTTPS).

3. **Configure Application Pool:**

   - Go to "Application Pools" in IIS Manager.
   - Ensure that the application pool for your site is running under the "No Managed Code" - - pipeline mode (since ASP.NET Core runs in its own process).

4. **Set Up Permissions:**

   - Ensure that the application pool identity has read and write permissions to the folder where - the application is published.
   - Adjust folder permissions if necessary.

5. **Restart IIS:**

- To apply the changes, restart IIS or the specific site by right-clicking on it and selecting "Restart".

## Publish the Angular Client-App

1. Build the Angular application for production:
   ```bash
   cd client-app
   ng build --prod
   ```
2. Copy the contents of the dist folder to the web directory of your IIS server.

3. Configure IIS to serve static files from the directory where you copied the Angular build output.

### Key Features

- **Client Management:** Angular 16 frontend with filtering, paging, and CRUD operations.
- **Background Jobs:** Hangfire-based background job processing, integrated with Polygon.io for job execution.
- **Clean Architecture:** Separation of concerns with different layers for Domain, Application, Infrastructure, and Presentation.
- **Entity Framework Core:** Code-first approach for database management with a T-SQL database.

## Project Details

#### **Application**

- Handles the business logic of the application and provides services for the Web API and Background worker.

### **Background**

- A worker service project that processes background jobs using Hangfire. This project is responsible for executing tasks that interact with Polygon.io APIs and other scheduled jobs.

### **ClientCRUD**

- A Web API project built with .NET 8, providing RESTful endpoints for managing client data. This API serves as the backend for the Angular frontend and interacts with the T-SQL database via Entity Framework Core.

### **Client-App**

- An Angular 16 application that provides a user interface for managing clients. The app features CRUD operations, filtering, paging, and form validations with Kendo UI components.

### **Domain**

Contains core domain entities and domain logic. This project is responsible for defining the core business rules and models that are used across the solution.

### **Infrastructure**

- Implements the infrastructure services such as database access (Entity Framework Core), external API integrations (Polygon.io), and Hangfire configuration for background job scheduling.
