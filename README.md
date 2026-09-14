# Smart-X IoT Mesh Ecosystem - Part 1

## Project Overview
Smart-X is a hybrid Internet of Things (IoT) ecosystem designed to monitor and manage distributed environments, such as hydroponic farms and smart grid installations. This repository contains the Part 1 implementation: the core data ingestion engine and validation gateway, built using a decoupled .NET 10 architecture.

## Architecture Structure
The solution (`SmartX_Ecosystem`) is divided into three components:
*   **SmartX.Core:** A shared class library containing advanced object-oriented C# concepts (Generics, Operator Overloading, Recursion, and Advanced Arrays) for uniform data processing.
*   **SmartX.Api:** An ASP.NET Core Minimal API acting as the primary receiver for telemetry data and handling file uploads. 
*   **SmartX.Dashboard:** A modern Windows Forms client application (.NET 10) acting as the technical management interface, featuring a dynamic visual telemetry engagement strategy.

## Prerequisites
*   [Visual Studio](https://visualstudio.microsoft.com/) (Version supporting .NET 10)
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Optional, but included for container support)

## Setup and Compilation Instructions
To restore dependencies and compile the project locally:

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/YOUR_USERNAME/YOUR_REPOSITORY_NAME.git](https://github.com/YOUR_USERNAME/YOUR_REPOSITORY_NAME.git)
