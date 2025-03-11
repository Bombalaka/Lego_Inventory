# Lego_Inventory
# LEGO Inventory System

## 📌 Overview
The **LEGO Inventory System** is an **ASP.NET Core MVC application** that allows users to manage a collection of LEGO sets. The app uses **MongoDB** for persistent storage and falls back to **in-memory storage** if MongoDB is unavailable.

## 🛠️ Features
- **Add LEGO Sets**: Users can add new LEGO sets with names and descriptions.
- **View LEGO Collection**: A list of all LEGO sets stored in the database.
- **Edit LEGO Sets**: Modify existing LEGO set details.
- **Delete LEGO Sets**: Remove LEGO sets from the inventory.
- **MongoDB Integration**: Uses MongoDB for data storage, with automatic fallback to in-memory storage if MongoDB is down.

## 🏗️ Tech Stack
- **Backend**: ASP.NET Core MVC (.NET 9)
- **Database**: MongoDB (Docker-based)
- **Frontend**: Razor Views (HTML, CSS, Bootstrap)
- **Dependency Injection**: ASP.NET Core Services

## 🚀 Getting Started

### 1️⃣ Prerequisites
- Install **.NET 9 SDK**: [Download here](https://dotnet.microsoft.com/download)
- Install **Docker**: [Download here](https://www.docker.com/get-started)
- Install **MongoDB Compass** (Optional): [Download here](https://www.mongodb.com/try/download/compass)

### 2️⃣ Clone the Repository
```bash
git clone https://github.com/yourusername/lego-inventory.git
cd lego-inventory
```

### 3️⃣ Set Up MongoDB in Docker
```bash
docker run -d -p 27017:27017 --name mongodb mongo
```
Verify MongoDB is running:
```bash
docker ps
```

### 4️⃣ Update `appsettings.json`
Modify the MongoDB connection string if necessary:
```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "LegoInventoryDb"
  }
}
```

### 5️⃣ Run the Application
```bash
dotnet build
dotnet run
```
Open **http://localhost:5132** in your browser.

---

## 📜 API Endpoints
| HTTP Method | Endpoint | Description |
|------------|---------|-------------|
| **GET** | `/Lego/LegoList` | View all LEGO sets |
| **GET** | `/Lego/Create` | Show form to add a new LEGO set |
| **POST** | `/Lego/Create` | Save new LEGO set to database |
| **GET** | `/Lego/Edit/{id}` | Show form to edit an existing LEGO set |
| **POST** | `/Lego/Edit` | Save updated LEGO set |
| **POST** | `/Lego/Delete/{id}` | Delete a LEGO set |

---

## ⚙️ Project Structure
```
Lego_Inventory/
│── Controllers/
│   ├── LegoController.cs
│── Data/
│   ├── ILegoRepository.cs
│   ├── LegoRepository.cs
│── Models/
│   ├── LegoSet.cs
│── Views/
│   ├── Lego/
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── LegoList.cshtml
│   │   ├── Index.cshtml
│── appsettings.json
│── Program.cs
│── README.md
```

---

## 🛠️ Troubleshooting
### ❌ MongoDB Connection Fails
**Error:** "TimeoutException: A timeout occurred after 30000ms selecting a server"

**Fix:** Make sure MongoDB is running.
```bash
docker start mongodb
```
If running in Docker, check the MongoDB container's IP:
```bash
docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' mongodb
```
Then update `appsettings.json`:
```json
"ConnectionString": "mongodb://<MongoDB-IP>:27017"
```

### ❌ App Uses In-Memory Storage Unexpectedly
Check the console output:
```bash
❌ Failed to connect to MongoDB. Running in-memory mode.
```
**Fix:** Verify that MongoDB is reachable and update connection settings.

---

## ✨ Future Improvements
- Add user authentication (login/logout).
- Implement search and filtering.
- Improve UI with AJAX for a better experience.
- Deploy to cloud services (Azure, AWS, Google Cloud).

---

## 📜 License
This project is open-source under the **MIT License**.

---

## 📧 Contact
For questions, feel free to reach out:
- GitHub Issues: [Open an Issue](https://github.com/yourusername/lego-inventory/issues)
- Email: your-email@example.com

