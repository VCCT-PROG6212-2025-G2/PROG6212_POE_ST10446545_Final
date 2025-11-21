# CMCS Claim Management System  

## 📌 Project Overview  
The **CMCS – Claim Management and Coordination System** is a web-based system built for university lecturers to submit monthly work-hour claims, while coordinators, managers, and HR review and manage these claims.

The system provides:  
- ✔️ Lecturer claim submissions  
- ✔️ Coordinator verification workflow  
- ✔️ Manager approval workflow  
- ✔️ HR claim summary reporting  
- ✔️ Automated rule-checking (Hours > 160, Rate > R1000)  
- ✔️ File uploads stored in Azure Blob Storage  
- ✔️ Dashboard tracking of claim statuses  

Built using **ASP.NET Core MVC (.NET 8), Entity Framework Core, Azure SQL, Azure Blob Storage, Bootstrap 5**.

---

# 🧩 System Components  

| Role | Responsibilities |
|------|------------------|
| **Lecturer** | Submit claims, upload supporting documents, track own claims |
| **Coordinator** | Verify or reject pending claims, rule-check violations |
| **Manager** | Approve or reject verified claims |
| **HR** | Generate summary reports of approved claims |
| **System** | Tracks statuses, enforces rules, displays dashboards |

---

# ⚙️ Technologies Used  
- ASP.NET Core MVC (.NET 8)  
- Entity Framework Core  
- Azure SQL Database  
- Azure Blob Storage  
- xUnit Test Framework  
- Bootstrap 5  
- Razor Pages (`.cshtml`)  

---

# 🚀 Features (Part 1 + Part 2 + Final POE Enhancements)

## 1️⃣ Claim Submission (Lecturer)
- Auto-calculated total amount *(Hours × Rate)*  
- Real-time validation using DataAnnotations  
- Upload supporting documents (`.pdf`, `.docx`, `.xlsx`)  
- Max file size: **5 MB**  
- Errors displayed inline with highlighted fields  
- Claim stored with linked document(s)

---

## 2️⃣ File Upload Management  
- Only approved file types allowed  
- Oversized/invalid files show friendly errors  
- Files stored in **Azure Blob Storage**  
- Each file linked to its associated claim  

---

## 3️⃣ Coordinator Automation (NEW – Final POE)  
- Sees only **Pending** claims  
- System performs automatic rule checking:
  - Hours > **160**  
  - Hourly Rate > **R1000**  
- Displays:
  - 🟩 **OK** badge when valid  
  - 🟥 **Violation** badge when rules fail  
- Available actions: **Verify** or **Reject**

---

## 4️⃣ Manager Automation (NEW – Final POE)  
- Sees only **Verified** claims  
- Same automatic rule evaluation displayed  
- Available actions: **Approve** or **Reject**

---

## 5️⃣ HR Summary Reporting (NEW – Final POE)  
- Shows:
  - Number of approved claims per lecturer  
  - Total hours  
  - Total amount  
- Date-range filtering  
- “**Print Report**” option for PDF-style printing  

---

## 6️⃣ My Claims (Lecturer View)  
- Lecturer can filter and view their own claim history  
- Status, hours, and amount shown  
- Helps lecturers track progress of submitted claims  

---

## 7️⃣ Dashboard Enhancements  
- Displays:
  - Monthly total amount  
  - List of recent claims  
  - Status badges (Pending, Verified, Approved, Rejected)  
- Acts as an overview for all activity  

---

## 8️⃣ Error Handling & User Feedback  
- Global error handler with fallback `/Home/Error`  
- Success and error alerts via Bootstrap + TempData  
- Clear user feedback on every action (submit → verify → approve)  

---

# 🧪 Unit Testing (xUnit)  
Dedicated **CMCSProject.Tests** project includes:

- ✔️ `Verify_PendingClaim_ChangesStatusToVerified_AndAddsApproval`  
- ✔️ `Approve_VerifiedClaim_ChangesStatusToApproved`  
- ✔️ `Reject_FromPending_SetsStatusToRejected`  
- ✔️ `Reject_FromVerified_SetsStatusToRejected`  

All tests: **PASSED (4/4)** 

---

# 🧭 Navigation Overview  

| Page | Description |
|------|-------------|
| `/Home/Index` | Landing page |
| `/CMCS/Dashboard` | Dashboard with claim summary |
| `/CMCS/SubmitClaim` | Lecturer claim submission |
| `/CMCS/Documents` | Uploaded document list |
| `/CMCS/Approvals` | Shows all claims |
| `/CMCS/Coordinator` | Coordinator pending claims |
| `/CMCS/Manager` | Manager verified claims |
| `/CMCS/HR` | HR approved-claim summary |
| `/CMCS/MyClaims` | Lecturer personal claim history |
| `/CMCS/Profile` | Profile page |

---

# 📝 Version Summary  
| Version | Description |
|---------|-------------|
| **Part 1** | Base claim submission system + workflow |
| **Part 2** | Validation, file restriction, error handling, unit tests |
| **Part 3 (Final POE)** | Coordinator & Manager automation, HR reports, My Claims, improved UI, final workflow |

---

# 👤 Student Details  
**Name:** ST10446545  
**Module:** PROG6212  
**POE:** CMCS Claim Management System  

---

# 🎥 YouTube Link  
https://youtu.be/sA-Ps6q-45I
