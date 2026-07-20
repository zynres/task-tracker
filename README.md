# Task Tracker

Task Tracker is a REST API for managing employee requests. The project was developed as a technical assessment for `PTMK` based on the requirements described in [Task Description](Task-Description.md).

### Business Process

Each request goes through a predefined lifecycle:

```text
New => InProgress => Completed
```

Only valid status transitions are allowed.

| Allowed Transitions | Invalid Transitions |
|---------------------|---------------------|
| `New → In Progress` | `New → Completed` |
| `In Progress → Completed` | Any transition that skips an intermediate status |

### Database Design

The database schema follows normalization principles to eliminate unnecessary data duplication and maintain data consistency through foreign key relationships.
 
#### Request:

| Id | AuthorId | AssigneeId | Description | CompletedDate | CreatedDate | DeadLine | Status |
|----|----------|------------|-------------|---------------|-------------|----------|--------|
| int | FK | FK | text | timestamp | timestamp | timestamp | enum |

#### Employee: 

| Id | Name | LastName | Patronymic | DepartmentId | PositionId |
|----|------|----------|------------|--------------|------------|
| int | text | text | text | FK | FK |
  
#### Department and Position:

| Id | Name |
|----|------|
| int | text |
---

#### Relationships:

> One `Department and Position` has many `Employee`

> One `Employee` can author many `Request`

> One `Employee` can be assigneed to many `Request`

### API

The application exposes a RESTful API for managing employees, departments, positions, and requests. The API follows REST conventions All request and response bodies are exchanged in JSON format. by using resource-oriented endpoints, standard HTTP methods, and JSON payloads.

#### Employees

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/employees | Creates one or more employees. Each employee is associated with a department and a position. |
| GET | /api/employees/{employeeId} | Retrieves an employee together with all assigned requests. |

#### Departments

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/departments | Creates one department. |
| GET | /api/departments/{departmentId} | Retrieves a department by its identifier. |

#### Positions

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/positions | Creates one position. |
| GET | /api/positions/{positionId} | Retrieves a position by its identifier. |

#### Requests

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/requests | Creates one or more requests. |
| PATCH | /api/requests/{requestId} | Changes the request status according to the defined business rules. |
| PATCH | /api/requests/{requestId}/assignee/{assigneeId} | Assigns or reassigns a request to another employee. |
| GET | /api/requests/filter | Retrieves requests using one or more filtering criteria. |
| GET | /api/requests/report | Returns aggregated statistics for requests. |

#### Filtering

The filtering endpoint supports combining multiple query parameters within a single request.

| Query Parameter | Description |
|-----------------|-------------|
| Status | Filters requests by status. |
| AssigneeId | Filters requests assigned to a specific employee. |
| DepartmentId | Filters requests by the assignee's department. |
| IsOverdue | Filters requests based on whether they are overdue. |

#### Reports

> The reporting endpoint returns the number of requests for each status, the total number of overdue requests, and the number of completed requests grouped by assignee.
---

## Tech Stack

- Backend: `ASP.NET Core (.NET 10)`
- Database: `PostgreSQL`
- ORM: `Entity Framework Core`
- Testing: `NBomber`, `xUnit`
- Containerization: `Docker`, `Docker Compose`

## How to run

> Note: On the first startup the application automatically seeds the database with test data. To disable this behavior, remove DataSeeder registration from Program.cs.

### 1. Clone repository
```bash
git clone https://github.com/zynres/task-tracker.git
cd task-tracker
```
### 2. Create .env
```env
DB_USER=postgres
DB_PASSWORD=password
DB_NAME=name
```
### 3. Run Docker Compose
```bash
docker compose up --build
```
### 4. Open Swagger
```text
http://localhost:5100/swagger
```
