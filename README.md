# Arya Healthcare - Digital Healthcare Platform

[![Live Demo](https://img.shields.io/badge/demo-live-success)](https://arya-healthcare.vercel.app)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

> A comprehensive full-stack digital healthcare platform built with Angular and .NET microservices, demonstrating enterprise-grade HealthTech architecture, modern healthcare interoperability standards (FHIR R4, HL7 v2, OMOP CDM), and clinical terminologies (SNOMED CT, LOINC, ICD-10, RxNorm).

![Arya Healthcare](https://via.placeholder.com/1200x400?text=Arya+Healthcare+Platform)

## 🏥 Healthcare Interoperability Standards

This platform implements the four pillars typically evaluated in modern HealthTech engineering:

| Pillar | Implementation | Folder |
|--------|----------------|--------|
| **FHIR R4** | RESTful resource APIs for Patient, Observation, Encounter, Condition, MedicationRequest, plus `$everything` operation. Built on the Firely .NET SDK. | [`backend/src/FullHealth.Interop/Fhir/`](backend/src/FullHealth.Interop/Fhir/) |
| **HL7 v2.5** | NHapi-based pipe parser for ADT (admission), ORU (results), ORM (orders) with MSA ACK responses. Sample messages and HTTP ingest endpoint included. | [`backend/src/FullHealth.Interop/Hl7v2/`](backend/src/FullHealth.Interop/Hl7v2/) |
| **OMOP CDM v5.4** | Full PostgreSQL DDL for clinical event tables (PERSON, VISIT_OCCURRENCE, CONDITION_OCCURRENCE, DRUG_EXPOSURE, MEASUREMENT) and vocabulary tables (CONCEPT, CONCEPT_RELATIONSHIP, CONCEPT_ANCESTOR), with FHIR → OMOP ETL mapping documentation and reference analytical SQL. | [`omop/`](omop/) |
| **Clinical terminologies** | SNOMED CT, LOINC, ICD-10-CM, RxNorm, CPT-4 reference catalogues with code-system URIs, FHIR coding patterns, and OMOP `concept_id` resolution rules. | [`terminology/`](terminology/) |

See [`docs/INTEROPERABILITY_STANDARDS.md`](docs/INTEROPERABILITY_STANDARDS.md) for the architecture diagram, endpoint inventory, and standards version matrix.

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
                 ┌─────────────────────────────────┐
                 │      Angular 19 Frontend        │ ← Patient + clinician views (4200)
                 └────────────────┬────────────────┘
                                  │ HTTPS / REST / FHIR+JSON
                                  ▼
   ┌─────────────────────────────────────────────────────────────┐
   │                  .NET 8 Microservices                       │
   ├──────────────┬──────────────┬──────────────┬────────────────┤
   │  Provider    │  Pharmacy    │ Diagnostics  │   Interop      │
   │  (5001)      │  (5002)      │  (5003)      │   (5050)       │
   │              │              │              │  FHIR + HL7v2  │
   ├──────────────┼──────────────┼──────────────┼────────────────┤
   │  Patient     │  Appointment │   Auth       │  (more...)     │
   │  (5004)      │  (5005)      │  (5006)      │                │
   └──────┬───────┴──────┬───────┴──────┬───────┴────────┬───────┘
          │              │              │                │
          ▼              ▼              ▼                ▼
   ┌─────────────────────────────────────────────────────────────┐
   │                       Data Layer                            │
   │  PostgreSQL (OLTP + Bronze/Silver) | OMOP CDM v5.4 (Gold)  │
   └─────────────────────────────────────────────────────────────┘
                                  │
                                  ▼
   ┌─────────────────────────────────────────────────────────────┐
   │            Analytics: ATLAS / HADES / Superset             │
   └─────────────────────────────────────────────────────────────┘
```

External integrations land via two channels:
- **HL7 v2 MLLP / HTTP** — legacy hospital EHR systems (Epic, Cerner) push ADT, ORU, ORM messages
- **FHIR R4 REST** — modern client apps and partner systems exchange Patient, Observation, Encounter, Condition, MedicationRequest

Both streams are normalized into the OMOP Common Data Model for cross-institution analytics.

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
├── frontend/                                # Angular 19 application
│   └── src/app/{core,features,services}/
├── backend/
│   ├── src/
│   │   ├── FullHealth.Provider/             # Doctors / specialties
│   │   ├── FullHealth.Pharmacy/             # Medication catalogue
│   │   ├── FullHealth.Diagnostics/          # Lab tests / packages
│   │   ├── FullHealth.Auth/                 # JWT auth
│   │   ├── FullHealth.Patient/              # Patient records
│   │   ├── FullHealth.Appointment/          # Booking
│   │   ├── FullHealth.Core/                 # Shared kernel
│   │   └── FullHealth.Interop/              # ⭐ FHIR R4 + HL7 v2 interop service
│   │       ├── Fhir/Controllers/            #    PatientController, ObservationController,
│   │       │                                #    EncounterController, ConditionController,
│   │       │                                #    MedicationRequestController
│   │       ├── Fhir/FhirSampleData.cs       #    Firely SDK resource builders
│   │       ├── Hl7v2/Hl7MessageParser.cs    #    NHapi ADT/ORU parsers, ACK builder
│   │       ├── Hl7v2/Controllers/           #    /hl7v2/adt, /hl7v2/oru endpoints
│   │       └── Hl7v2/Samples/*.hl7          #    Sample ADT^A01, ORU^R01, ORM^O01
│   ├── tests/
│   │   └── FullHealth.Interop.Tests/        # xUnit + FluentAssertions test suite
│   └── FullHealth.sln
├── omop/                                    # ⭐ OMOP Common Data Model v5.4
│   ├── schema/                              #    Core + vocabulary DDL (PostgreSQL)
│   ├── seed/sample_concepts.sql             #    Cross-vocabulary seed data
│   ├── queries/common_analytics.sql         #    Reference cohort / outcomes SQL
│   ├── etl/fhir_to_omop_mapping.md          #    FHIR R4 → OMOP per-resource mapping
│   └── README.md
├── terminology/                             # ⭐ Clinical vocabulary references
│   ├── snomed_examples.md
│   ├── loinc_examples.md
│   ├── icd10_examples.md
│   ├── rxnorm_examples.md
│   ├── code_systems.json                    #    FHIR system URI → OMOP vocabulary_id
│   └── README.md
├── infra/                                   # Terraform (AKS, ACR, network)
├── docs/
│   ├── INTEROPERABILITY_STANDARDS.md        # ⭐ Standards architecture + endpoint inventory
│   ├── SYSTEM_ARCHITECTURE.md
│   ├── FEATURE_SYNC_MAP.md
│   └── 12_functional_gap_analysis/
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

For complete API documentation, see [API_DOCUMENTATION.md](backend/API_DOCUMENTATION.md).

### FHIR R4 Interop Endpoints (FullHealth.Interop, port 5050)

```bash
# Read a Patient
curl http://localhost:5050/fhir/Patient/MRN12345 \
     -H "Accept: application/fhir+json"

# Patient $everything operation - all related resources
curl http://localhost:5050/fhir/Patient/MRN12345/\$everything \
     -H "Accept: application/fhir+json"

# Observation search by patient + LOINC code (Blood pressure panel)
curl "http://localhost:5050/fhir/Observation?patient=MRN12345&code=http://loinc.org|85354-9"

# Condition search (returns SNOMED + ICD-10 dual-coded resources)
curl "http://localhost:5050/fhir/Condition?patient=MRN12345"

# MedicationRequest (RxNorm-coded medications)
curl "http://localhost:5050/fhir/MedicationRequest?patient=MRN12345"
```

### HL7 v2 Ingest Endpoints

```bash
# POST an ADT^A01 (admit patient) message - returns MSA ACK
curl -X POST http://localhost:5050/hl7v2/adt \
     -H "Content-Type: text/plain" \
     --data-binary @backend/src/FullHealth.Interop/Hl7v2/Samples/sample_adt_a01.hl7

# POST an ORU^R01 (observation result) message
curl -X POST http://localhost:5050/hl7v2/oru \
     -H "Content-Type: text/plain" \
     --data-binary @backend/src/FullHealth.Interop/Hl7v2/Samples/sample_oru_r01.hl7
```

See [`docs/INTEROPERABILITY_STANDARDS.md`](docs/INTEROPERABILITY_STANDARDS.md) for the full endpoint inventory and protocol notes.

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

- [x] FHIR R4 Patient / Observation / Encounter / Condition / MedicationRequest resources
- [x] HL7 v2 ADT / ORU / ORM parser with ACK
- [x] OMOP CDM v5.4 schema + FHIR-to-OMOP ETL mapping
- [x] SNOMED / LOINC / ICD-10 / RxNorm code-system handling
- [ ] HL7 v2 MLLP TCP listener (currently HTTP-only)
- [ ] FHIR Bulk Data Access ($export) for population-level export
- [ ] SMART-on-FHIR OAuth 2.0 launch flow
- [ ] FHIR Subscriptions (R4) for real-time event push
- [ ] Full OHDSI Athena vocabulary load
- [ ] ACHILLES data-quality dashboard
- [ ] CONDITION_ERA and DRUG_ERA derivation jobs
- [ ] Real-time notifications (SignalR)
- [ ] Telemedicine video calls
- [ ] Mobile app (React Native)
- [ ] AI-powered symptom checker
- [ ] Multi-language support

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

Made with ❤️ by Ashish Pandey
