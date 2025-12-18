# Backend API Documentation

## Overview
This document provides API endpoints for the Arya Healthcare backend services. All services use In-Memory databases for development and can be switched to SQL Server/PostgreSQL for production.

## Services & Ports (Recommended)
- **Provider Service**: `http://localhost:5001`
- **Pharmacy Service**: `http://localhost:5002`
- **Diagnostics Service**: `http://localhost:5003`

---

## 1. Provider Service (Doctors)

### Base URL: `/api/doctors`

#### GET /api/doctors
Get all doctors with optional specialty filter.

**Query Parameters:**
- `specialty` (optional): Filter by specialty (e.g., "Cardiology", "Neurology")

**Response:**
```json
[
  {
    "id": 1,
    "name": "Dr. Sarah Smith",
    "specialty": "Cardiology",
    "qualification": "MD, DM",
    "experience": "15+ Years",
    "consultationFee": 0,
    "imageUrl": "https://images.unsplash.com/...",
    "bio": "Expert in Interventional Cardiology.",
    "languages": "",
    "isAvailable": true
  }
]
```

#### GET /api/doctors/{id}
Get specific doctor by ID.

**Response:** Single Doctor object

#### POST /api/doctors
Create a new doctor (Admin only in production).

**Request Body:**
```json
{
  "name": "Dr. John Doe",
  "specialty": "Orthopedics",
  "qualification": "MS (Ortho)",
  "experience": "10+ Years",
  "consultationFee": 1000,
  "imageUrl": "https://...",
  "bio": "Joint replacement specialist",
  "languages": "English, Hindi",
  "isAvailable": true
}
```

---

## 2. Pharmacy Service (Products)

### Base URL: `/api/products`

#### GET /api/products
Get all products with optional category filter.

**Query Parameters:**
- `category` (optional): Filter by category (e.g., "Medicines", "Supplements", "Devices")

**Response:**
```json
[
  {
    "id": 1,
    "name": "Paracetamol 500mg",
    "category": "Medicines",
    "price": 20,
    "discountPrice": 18,
    "imageUrl": "https://via.placeholder.com/150",
    "prescriptionRequired": false,
    "description": "Fever and pain relief."
  }
]
```

#### GET /api/products/{id}
Get specific product by ID.

#### POST /api/products
Create a new product.

**Request Body:**
```json
{
  "name": "Vitamin D3",
  "category": "Supplements",
  "price": 500,
  "discountPrice": 450,
  "imageUrl": "https://...",
  "prescriptionRequired": false,
  "description": "Bone health supplement"
}
```

---

## 3. Diagnostics Service (Lab Tests & Packages)

### Base URL: `/api/labpackages` and `/api/labtests`

#### GET /api/labpackages
Get all lab packages with optional category filter.

**Query Parameters:**
- `category` (optional): Filter by category (e.g., "Popular", "Women Health", "Condition Based")

**Response:**
```json
[
  {
    "id": 1,
    "name": "Complete Health Checkup",
    "category": "Popular",
    "price": 2500,
    "discountPrice": 1999,
    "description": "Comprehensive health screening",
    "testsIncluded": 85,
    "imageUrl": ""
  }
]
```

#### GET /api/labtests
Get all individual lab tests.

**Response:**
```json
[
  {
    "id": 1,
    "name": "Complete Blood Count (CBC)",
    "price": 300,
    "description": "Hemoglobin, WBC, Platelets",
    "fastingRequired": false
  }
]
```

#### POST /api/labpackages
Create a new lab package.

#### POST /api/labtests
Create a new lab test.

---

## Running the Services

### Start Individual Services:
```bash
# Provider Service (Port 5001)
cd backend/src/FullHealth.Provider
dotnet run --urls="http://localhost:5001"

# Pharmacy Service (Port 5002)
cd backend/src/FullHealth.Pharmacy
dotnet run --urls="http://localhost:5002"

# Diagnostics Service (Port 5003)
cd backend/src/FullHealth.Diagnostics
dotnet run --urls="http://localhost:5003"
```

### Swagger UI Access:
- Provider: `http://localhost:5001/swagger`
- Pharmacy: `http://localhost:5002/swagger`
- Diagnostics: `http://localhost:5003/swagger`

---

## Frontend Integration

### Angular Service Example:

```typescript
// services/provider.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ProviderService {
  private apiUrl = 'http://localhost:5001/api/doctors';

  constructor(private http: HttpClient) {}

  getDoctors(specialty?: string) {
    const params = specialty ? { specialty } : {};
    return this.http.get<Doctor[]>(this.apiUrl, { params });
  }

  getDoctor(id: number) {
    return this.http.get<Doctor>(`${this.apiUrl}/${id}`);
  }
}
```

### CORS Configuration
All services are configured to allow CORS from `http://localhost:4200` (Angular dev server). Update `Program.cs` for production URLs.

---

## Database Migration (Production)

To switch from In-Memory to SQL Server:

1. Install EF Core SQL Server package:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

2. Update `Program.cs`:
```csharp
builder.Services.AddDbContext<ProviderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

3. Add connection string to `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AryaHealthProvider;Trusted_Connection=True;"
  }
}
```

4. Run migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
