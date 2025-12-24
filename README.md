# Redeem Beenz – Applied Software Engineering Assignment

## Overview
This project is a **C# console application** developed as part of the **Applied Software Engineering** module coursework.

The application implements a *Redeem Beenz* use case based on the Hill & Knowlton knowledge management system case study. It demonstrates the application of **software engineering principles**, **UML-driven design**, **Object Constraint Language (OCL)**, **unit testing**, and **component-based architecture**.

The system allows an employee to:
- View their beenz balance
- Browse a reward catalogue
- Redeem rewards using accumulated beenz
- Enforce formal business rules and constraints

---

## Key Features
- Console-based interactive application
- Object-oriented design with clear separation of concerns
- Component-based architecture using interfaces
- OCL-style preconditions, postconditions, and invariants enforced at runtime
- Unit testing with **NUnit**
- In-memory data stores for simplicity and testability

---

## Technologies Used
- **Language:** C#
- **Framework:** .NET
- **Testing:** NUnit
- **Design:** UML, OCL
- **Architecture:** Component-based, Interface-driven design

---

## Project Structure
RedeemBeenz/
│
├── Program.cs # Console UI and user interaction
│
├── Models/ # Domain models (Reward, Redemption, DeliveryOption, etc.)
├── Interfaces/ # Component interfaces
├── Component/ # Core business logic and components
│ ├── BeenzRewards.cs
│ ├── AccountStore.cs
│ ├── Catalogue.cs
│ └── RedemptionStore.cs
│
├── Tests/ # NUnit unit tests
│ ├── AccountStoreTests.cs
│ ├── BeenzRewardsTests.cs
│ ├── CatalogueTests.cs
│ ├── DeliveryOptionTests.cs
│ └── RedemptionStoreTests.cs
│
└── README.md

---

## Running the Application
1. Clone the repository:
   ```bash
   git clone https://github.com/ViniUK00/redeem-beenz.git
Open the project in Visual Studio or another .NET IDE.

Build and run the project:

bash
Copy code
dotnet run
Follow the on-screen menu to browse rewards and redeem beenz.

Running Unit Tests
The project includes automated unit tests using NUnit.

To run the tests:
dotnet test
The tests verify:

Business logic correctness

Enforcement of OCL constraints

Error handling and edge cases

Successful and failed redemption scenarios

Object Constraint Language (OCL)
Formal business rules are implemented using OCL-style contracts, including:

Invariants (e.g. beenz balance must not be negative)

Preconditions (e.g. sufficient balance, valid delivery details)

Postconditions (e.g. balance and stock updates after redemption)

Contract violations are detected at runtime and handled gracefully in the UI layer.

Software Engineering Practices
This project demonstrates:

Object-oriented principles (encapsulation, abstraction, separation of concerns)

Component-based design

Test-driven development concepts

Maintainable and sustainable software design

Disclaimer
This project was developed for academic purposes only as part of a university assignment. It is not intended for production use.

Author
Developed by [Your Name]
Applied Software Engineering – University Coursework

yaml
Copy code

---

If you want, I can also:
- tailor it exactly to **UWL marking language**,  
- add **screenshots section**, or  
- shorten it to a **1-page README** if your lecturer prefers minimal repos.

Just tell me 👍






You said:
give me everything on a code style


ChatGPT said:
Got it 👍
Below is everything presented purely in code style, ready to paste directly into your repository as README.md.

md
Copy code
# Redeem Beenz – Applied Software Engineering Assignment

## Overview
This repository contains a C# console application developed for the **Applied Software Engineering** module.

The application implements a **Redeem Beenz** use case derived from the Hill & Knowlton knowledge management system case study. The project focuses on applying core software engineering concepts such as object-oriented design, UML modelling, Object Constraint Language (OCL), unit testing, and component-based architecture.

The system allows an employee to view their beenz balance, browse a reward catalogue, and redeem rewards while enforcing formal business rules.

---

## Features
- Console-based interactive user interface
- Object-oriented design with encapsulation and abstraction
- Component-based architecture using interfaces
- OCL-style invariants, preconditions, and postconditions
- Runtime contract enforcement with graceful error handling
- Unit testing using NUnit
- In-memory data stores for simplicity and testability

---

## Technologies
- Language: C#
- Framework: .NET
- Testing Framework: NUnit
- Design Techniques: UML, OCL
- Architecture: Component-based design

---

## Project Structure
RedeemBeenz/
│
├── Program.cs
│
├── Models/
│ ├── Reward.cs
│ ├── Redemption.cs
│ ├── DeliveryOption.cs
│ └── Enums.cs
│
├── Interfaces/
│ ├── IRewardsAPI.cs
│ ├── IAccountStore.cs
│ ├── ICatalogue.cs
│ └── IRedemptionStore.cs
│
├── Component/
│ ├── BeenzRewards.cs
│ ├── AccountStore.cs
│ ├── Catalogue.cs
│ └── RedemptionStore.cs
│
├── Tests/
│ ├── AccountStoreTests.cs
│ ├── BeenzRewardsTests.cs
│ ├── CatalogueTests.cs
│ ├── DeliveryOptionTests.cs
│ └── RedemptionStoreTests.cs
│
└── README.md

yaml
Copy code

---

## Running the Application
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/redeem-beenz.git
Open the solution in Visual Studio or a compatible .NET IDE.

Run the application:

bash
Copy code
dotnet run
Follow the console menu to browse rewards and redeem beenz.

Running Unit Tests
The project includes unit tests written using NUnit.

To execute the tests:

bash
Copy code
dotnet test
The tests validate:

Core business logic

OCL constraints and invariants

Error handling and edge cases

Successful and failed redemption scenarios

Object Constraint Language (OCL)
Formal business rules are enforced using OCL-style contracts, including:

Invariants

Beenz balance must never be negative

Preconditions

Employee must have sufficient beenz

Reward must have sufficient stock

Delivery option must be valid

Postconditions

Beenz balance is reduced after redemption

Reward stock is updated correctly

Contract violations are detected at runtime and handled in the user interface layer.

Software Engineering Practices
This project demonstrates:

Encapsulation of business rules

Separation of concerns between UI and logic

Interface-driven development

Test-driven development concepts

Maintainable and sustainable software design

Disclaimer
This project was developed for academic purposes only as part of a university assignment and is not intended for production use.

Author
Developed by Ervin
Applied Software Engineering – University Coursework
