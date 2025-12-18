# Arya Healthcare - Digital Healthcare Platform

[![Live Demo](https://img.shields.io/badge/demo-live-success)](https://arya-healthcare.vercel.app)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

> A comprehensive full-stack digital healthcare platform built with Angular and .NET microservices, demonstrating enterprise-grade architecture and modern development practices.

![Arya Healthcare](https://via.placeholder.com/1200x400?text=Arya+Healthcare+Platform)

## 🌟 Features

### Patient-Facing Features
- 🏥 **Provider Search** - Find and book appointments with specialists
- 💊 **Online Pharmacy** - Order medicines with home delivery
- 🧪 **Diagnostics** - Book lab tests and health packages
- 🌍 **International Patients** - Dedicated services for global patients
- 🏆 **Centres of Excellence** - Specialized care departments
- 📚 **Health Library** - Educational content and articles
- 🤖 **AI Chat Assistant** - 24/7 patient support
- 📱 **Responsive Design** - Works on all devices

### Technical Features
- ⚡ **Microservices Architecture** - Scalable and maintainable
- 🔐 **Secure Authentication** - JWT-based auth system
- 📊 **RESTful APIs** - Well-documented with Swagger
- 🐳 **Containerized** - Docker-ready for deployment
- ☁️ **Cloud-Native** - Designed for Azure Kubernetes Service
- 📈 **Observable** - Integrated monitoring and logging

## 🏗️ Architecture

```
┌─────────────┐
│   Angular   │ ← Frontend (Port 4200)
│   Frontend  │
└──────┬──────┘
       │ HTTP/REST
       ▼
┌─────────────────────────────────────┐
│        Microservices Layer          │
├──────────┬──────────┬──────────────┤
│ Provider │ Pharmacy │ Diagnostics  │
│ Service  │ Service  │  Service     │
│ (5001)   │ (5002)   │  (5003)      │
└────┬─────┴────┬─────┴──────┬───────┘
     │          │            │
     ▼          ▼            ▼
┌─────────────────────────────────────┐
│         Database Layer              │
│  (In-Memory/SQL Server/PostgreSQL)  │
└─────────────────────────────────────┘
```

## 🚀 Tech Stack

### Frontend
- **Framework**: Angular 19
- **Language**: TypeScript
- **Styling**: Vanilla CSS
- **Build**: Angular CLI

### Backend
- **Framework**: .NET 9 Web API
- **Language**: C#
- **ORM**: Entity Framework Core
- **Database**: In-Memory (dev) / SQL Server (prod)
- **API Docs**: Swagger/OpenAPI

### Infrastructure
- **Containerization**: Docker
- **Orchestration**: Kubernetes (AKS)
- **CI/CD**: GitHub Actions
- **Hosting**: Vercel (Frontend), Azure (Backend)
- **IaC**: Terraform

## 📦 Project Structure

```
arya-healthcare/
├── frontend/                 # Angular application
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/        # Layout, guards, interceptors
│   │   │   ├── features/    # Feature modules
│   │   │   └── services/    # API services
│   │   └── styles.css       # Global styles
│   └── package.json
├── backend/                  # .NET microservices
│   ├── src/
│   │   ├── FullHealth.Provider/
│   │   ├── FullHealth.Pharmacy/
│   │   ├── FullHealth.Diagnostics/
│   │   ├── FullHealth.Auth/
│   │   ├── FullHealth.Patient/
│   │   └── FullHealth.Appointment/
│   └── FullHealth.sln
├── infra/                    # Terraform configs
│   ├── main.tf
│   ├── variables.tf
│   └── outputs.tf
├── docs/                     # Documentation
│   ├── SYSTEM_ARCHITECTURE.md
│   ├── API_DOCUMENTATION.md
│   └── FEATURE_SYNC_MAP.md
└── README.md
```

## 🛠️ Getting Started

### Prerequisites
- Node.js 18+ and npm
- .NET 9 SDK
- Git

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/ashishserco/arya-healthcare.git
cd arya-healthcare
```

2. **Setup Frontend**
```bash
cd frontend
npm install
ng serve --port 4200
```
Frontend will be available at `http://localhost:4200`

3. **Setup Backend Services**

Open 3 separate terminals:

```bash
# Terminal 1 - Provider Service
cd backend/src/FullHealth.Provider
dotnet run --urls="http://localhost:5001"

# Terminal 2 - Pharmacy Service
cd backend/src/FullHealth.Pharmacy
dotnet run --urls="http://localhost:5002"

# Terminal 3 - Diagnostics Service
cd backend/src/FullHealth.Diagnostics
dotnet run --urls="http://localhost:5003"
```

4. **Access Swagger Documentation**
- Provider API: `http://localhost:5001/swagger`
- Pharmacy API: `http://localhost:5002/swagger`
- Diagnostics API: `http://localhost:5003/swagger`

## 📚 API Documentation

### Provider Service (Doctors)

**GET** `/api/doctors` - Get all doctors
```bash
curl http://localhost:5001/api/doctors
```

**GET** `/api/doctors?specialty=Cardiology` - Filter by specialty
```bash
curl http://localhost:5001/api/doctors?specialty=Cardiology
```

**GET** `/api/doctors/{id}` - Get specific doctor
```bash
curl http://localhost:5001/api/doctors/1
```

### Pharmacy Service (Products)

**GET** `/api/products` - Get all products
```bash
curl http://localhost:5002/api/products
```

**GET** `/api/products?category=Medicines` - Filter by category
```bash
curl http://localhost:5002/api/products?category=Medicines
```

### Diagnostics Service (Lab Tests)

**GET** `/api/labpackages` - Get health packages
```bash
curl http://localhost:5003/api/labpackages
```

**GET** `/api/labtests` - Get individual tests
```bash
curl http://localhost:5003/api/labtests
```

For complete API documentation, see [API_DOCUMENTATION.md](backend/API_DOCUMENTATION.md)

## 🎨 Screenshots

### Landing Page
![Landing Page](https://via.placeholder.com/800x400?text=Landing+Page)

### Provider Search
![Provider Search](https://via.placeholder.com/800x400?text=Provider+Search)

### Pharmacy
![Pharmacy](https://via.placeholder.com/800x400?text=Pharmacy)

## 🧪 Testing

```bash
# Frontend tests
cd frontend
npm test

# Backend tests
cd backend
dotnet test
```

## 🚢 Deployment

### Frontend (Vercel)
```bash
# Install Vercel CLI
npm i -g vercel

# Deploy
cd frontend
vercel --prod
```

### Backend (Azure)
```bash
# Build Docker images
docker build -t aryahealthacr.azurecr.io/provider:v1 ./backend/src/FullHealth.Provider
docker build -t aryahealthacr.azurecr.io/pharmacy:v1 ./backend/src/FullHealth.Pharmacy

# Push to ACR
az acr login --name aryahealthacr
docker push aryahealthacr.azurecr.io/provider:v1

# Deploy to AKS
kubectl apply -f k8s/
```

## 📊 Project Statistics

- **Frontend Components**: 15+
- **Backend Services**: 6
- **API Endpoints**: 20+
- **Database Tables**: 8+
- **Lines of Code**: ~4,500
- **Documentation Pages**: 10+

## 🗺️ Roadmap

- [ ] Real-time notifications (SignalR)
- [ ] Payment gateway integration
- [ ] Mobile app (React Native)
- [ ] Telemedicine video calls
- [ ] AI-powered symptom checker
- [ ] Multi-language support
- [ ] FHIR compliance

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Ashish Sharma**
- GitHub: [@ashishserco](https://github.com/ashishserco)
- LinkedIn: [Your LinkedIn](https://linkedin.com/in/yourprofile)

## 🙏 Acknowledgments

- Inspired by leading healthcare platforms: Practo, Apollo 24/7, Fortis Healthcare
- Built as a portfolio project to demonstrate full-stack development skills
- Special thanks to the open-source community

## 📞 Contact

For questions or feedback, please open an issue or contact me directly.

---

**⭐ If you find this project useful, please consider giving it a star!**

Made with ❤️ by Ashish Sharma
