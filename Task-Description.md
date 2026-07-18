# Task Description

## Overview

Develop an employee request management application (console or web).

The system is intended to manage employee requests through a predefined workflow, enforce business rules, provide reporting capabilities, and demonstrate efficient database design and optimization.

---

# Business Process

Each request follows the workflow below:

```text
New => In Progress => Completed
```

Only valid status transitions are allowed.

| Allowed Transitions | Invalid Transitions |
|---------------------|---------------------|
| `New → In Progress` | `New → Completed` |
| `In Progress → Completed` | Any transition that skips an intermediate status |

---

# Functional Requirements

## Employee Management

Implement an employee directory containing:

- Full Name (first/last name and patronymic)
- Department
- Position

## Request Management

Each request must contain the following information:

- Number
- Created Date
- Author
- Assignee
- Description
- Dead Line
- Status

The system must support:

- Changing the request status while validating allowed transitions.
- Reassigning a request to another employee.
- Listing requests with filtering by:
  - Status
  - Assignee
  - Department
  - Overdue requests

## Reporting

Generate reports showing:

- Number of requests grouped by status.
- Number of overdue requests.
- Number of completed requests per assignee.

---

# Database Requirements

Design the database with the following goals:

- Avoid unnecessary data duplication.
- Use appropriate relationships between entities.
- Follow database normalization principles.

Include a brief explanation of the database design decisions.

---

# Performance

Populate the database with:

- At least **1,000,000** requests.
- At least **1,000** employees.

Execute the following query:

> Retrieve all overdue requests assigned to a specific employee that are currently **In Progress**, ordered by due date.

Measure the query execution time.

Then optimize the database by implementing and documenting one or more optimization techniques.

Repeat the benchmark and describe:

- The changes that were made.
- Why those changes improve performance.
- Query execution time before and after optimization.

---

# Technical Requirements

- Use any object-oriented programming language.
- Use either an SQL database or MongoDB.
- Implement a domain model that represents the business entities.
- Keep the code well-structured, maintainable, and easy to extend.

---

# Deliverables

The final submission must include:

- Source code.
- Setup and run instructions.
- A brief description of the implemented business process.
- A description of the implemented business rules.
- A summary of the database optimizations and performance benchmark results.

---

# Optional Task

Describe how the system would need to be extended to support the following requirements:

- Requests require manager approval before processing.
- A request may have multiple assignees.
- The system stores the complete history of status changes.
- Due dates depend on the request type.
- Role-based access control (Employee, Manager, Administrator).