# Redeem Beenz – Applied Software Engineering Assignment

## Overview
This repository contains a C# console application developed as part of the **Applied Software Engineering** module.

The application implements a *Redeem Beenz* use case derived from the Hill & Knowlton knowledge management system case study. The project focuses on applying core software engineering concepts such as object-oriented design, UML modelling, Object Constraint Language (OCL), unit testing, and component-based architecture.

The system allows an employee to view their beenz balance, browse a reward catalogue, and redeem rewards while enforcing formal business rules and constraints.

---

## Features
- Console-based interactive application
- Object-oriented design (encapsulation, abstraction, separation of concerns)
- Component-based architecture using interfaces
- OCL-style invariants, preconditions, and postconditions
- Runtime contract enforcement with graceful error handling
- Unit testing using NUnit
- In-memory data stores for simplicity and testability

---

## Technologies Used
- Language: C#
- Framework: .NET
- Testing: NUnit
- Design Techniques: UML, OCL
- Architecture: Component-based design

---

## Project Structure
```text
RedeemBeenz/
│
├── Program.cs                  # Console UI and user interaction
│
├── Models/                     # Domain models (Reward, Redemption, DeliveryOption, etc.)
│   ├── Reward.cs
│   ├── Redemption.cs
│   ├── DeliveryOption.cs
│   └── Enums.cs
│
├── Interfaces/                 # Component interfaces
│   ├── IRewardsAPI.cs
│   ├── IAccountStore.cs
│   ├── ICatalogue.cs
│   └── IRedemptionStore.cs
│
├── Component/                  # Core business logic and components
│   ├── BeenzRewards.cs
│   ├── AccountStore.cs
│   ├── Catalogue.cs
│   └── RedemptionStore.cs
│
├── Tests/                      # NUnit unit tests
│   ├── AccountStoreTests.cs
│   ├── BeenzRewardsTests.cs
│   ├── CatalogueTests.cs
│   ├── DeliveryOptionTests.cs
│   └── RedemptionStoreTests.cs
│
└── README.md
```

---

## Running the Application
1. Clone the repository:
   ```bash
   git clone https://github.com/ViniUK00/redeem-beenz.git
   ```

2. Open the solution in Visual Studio or a compatible .NET IDE.

3. Run the application:
   ```bash
   dotnet run
   ```

4. Follow the on-screen menu to browse rewards and redeem beenz.

---

## Running Unit Tests
The project includes automated unit tests written using NUnit.

To run the tests:
```bash
dotnet test
```

---

## Object Constraint Language (OCL)
Formal business rules are enforced using OCL-style contracts, including:

### Invariants
- Beenz balance must never be negative

### Preconditions
- Employee must have sufficient beenz before redemption
- Reward must have sufficient stock
- Delivery option must be valid

### Postconditions
- Beenz balance is reduced after redemption
- Reward stock is updated correctly
- Redemption status is updated appropriately

---

## Software Engineering Practices
This project demonstrates:
- Encapsulation of business rules
- Separation of concerns between UI and business logic
- Interface-driven development
- Test-driven development concepts
- Maintainable and sustainable software design

---

## Disclaimer
This project was developed **for academic purposes only** as part of a university assignment and is not intended for production use.

---

## Author
Developed by Ervin
Applied Software Engineering – University Coursework
