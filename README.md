# CMCS Claim Management System 

##  Project Overview  
This project is a **Claim Management System** developed as part of the CMCS PROG6212 coursework. The system allows:  
- **Lecturers** to submit claims  
- **Coordinators** and **Managers** to review and approve/reject claims  
- **Support for document uploads**  
- **Tracking claim statuses** through a dashboard  

The project was designed using **ASP.NET Core MVC**, **C#**, and **SQL Database** integration.  

---

##  Features  
- Lecturer claim submission form  
- Approval workflows for Coordinator and Manager  
- Uploading and storing supporting documents  
- Dashboard to view claim statuses (Pending, Approved, Rejected)  
- GUI prototype designed with `.cshtml` pages  
- Gantt Chart & project plan created using **Jira**  

---

##  Technologies Used  
- **Frontend**: ASP.NET Core MVC, Razor Pages (`.cshtml`), CSS  
- **Backend**: C#  
- **Tools**: Jira (for project planning), draw.io (for UML diagrams), GitHub (version control)  

---

##  Project Planning  
- Gantt Chart and planning created in **Jira**  
- UML diagrams designed in **draw.io**  


---


#  CMCS Project – Part 2 (Enhanced Functionality & Unit Tests)

##  Overview
The **Claim Management and Coordination System (CMCS)** allows university lecturers to submit monthly claims for hours worked, and enables coordinators and managers to review, verify, and approve those claims.  
This version extends the functionality developed in **Part 1** by implementing **validation, error handling, file upload management, and unit testing** based on lecturer feedback.

---

##  System Components
| Role | Responsibilities |
|------|------------------|
| **Lecturer** | Submit claims, upload supporting documents |
| **Coordinator** | Verify or reject pending claims |
| **Manager** | Approve or reject verified claims |
| **System** | Tracks status (Pending → Verified → Approved/Rejected), displays summaries on the dashboard |

---

## ⚙️ Technologies Used
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- Azure SQL Database
- Azure Blob Storage
- xUnit Test Framework
- Bootstrap 5 (for responsive UI)

---

##  Features Added in Part 2

###  1. Input Validation (Lecturer Feedback)
- Implemented `DataAnnotations` in models (e.g. `[Required]`, `[Range]`, `[StringLength]`).
- Added validation messages on claim submission forms.
- Invalid submissions now redisplay the form with highlighted errors.

###  2. File Upload Restrictions
- Only `.pdf`, `.docx`, and `.xlsx` files are allowed.
- Max file size: **5 MB per file**.
- Invalid or oversized uploads trigger friendly validation messages.
- Files continue to be stored in **Azure Blob Storage** and linked to each claim.

### 3. Functionality
- Implemented new claim submission features allowing users to upload supporting documents.
- Improved the claim approval workflow to ensure data validation and better user feedback.
- Added dynamic form validation to enhance the user experience when submitting claims.
- Enhanced the admin dashboard to display claim details with document links and statuses.

###  4. Error Handling & Feedback
- Configured a global error handler with a user-friendly fallback page (`/Home/Error`).
- All pages display success/error notifications using Bootstrap alerts and `TempData`.

###  5. Unit Testing Implementation
Created a dedicated **xUnit Test Project (`CMCSProject.Tests`)** to ensure reliability:
- ✅ `Verify_PendingClaim_ChangesStatusToVerified_AndAddsApproval`
- ✅ `Approve_VerifiedClaim_ChangesStatusToApproved`
- ✅ `Reject_FromPending_SetsStatusToRejected`
- ✅ `Reject_FromVerified_SetsStatusToRejected`
- ✅ All tests now pass successfully (`4 Passed, 0 Failed`).

###  6. Improvements from Lecturer Feedback
- Better naming conventions for models and view models.
- Controller logic refactored for clarity and testability.
- User feedback on every action (submission, verification, approval, rejection).
- Enhanced dashboard summaries reflecting updated claim statuses.

---

##  Running the Tests
all unit tests where successful 

✅ You should see:
```
Test run finished: 4 Passed, 0 Failed
```

---

##  Navigation Overview
| Page | Description |
|------|--------------|
| `/Home/Index` | Landing page |
| `/CMCS/SubmitClaim` | Lecturer claim submission |
| `/CMCS/Coordinator` | Coordinator review dashboard |
| `/CMCS/Manager` | Manager approval dashboard |
| `/CMCS/Documents` | View uploaded files |
| `/CMCS/Dashboard` | Summary view of all claim statuses |

---

##  Version Summary
| Version | Description |
|----------|-------------|
| **Part 1** | Base system (Claim submission + workflow) |
| **Part 2** | Added validation, error handling, file restrictions, and unit tests based on lecturer feedback |


## Student  
- **ST10446545** 

