CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Claims" (
    "ClaimId" INTEGER NOT NULL CONSTRAINT "PK_Claims" PRIMARY KEY AUTOINCREMENT,
    "LecturerName" TEXT NOT NULL,
    "HoursWorked" TEXT NOT NULL,
    "HourlyRate" TEXT NOT NULL,
    "ClaimDate" TEXT NOT NULL,
    "Notes" TEXT NULL,
    "Status" INTEGER NOT NULL
);

CREATE TABLE "Approvals" (
    "ApprovalId" INTEGER NOT NULL CONSTRAINT "PK_Approvals" PRIMARY KEY AUTOINCREMENT,
    "ClaimId" INTEGER NOT NULL,
    "ApproverRole" INTEGER NOT NULL,
    "ApproverName" TEXT NOT NULL,
    "ApprovalDate" TEXT NOT NULL,
    "IsApproved" INTEGER NOT NULL,
    "Comment" TEXT NULL,
    CONSTRAINT "FK_Approvals_Claims_ClaimId" FOREIGN KEY ("ClaimId") REFERENCES "Claims" ("ClaimId") ON DELETE CASCADE
);

CREATE TABLE "SupportingDocuments" (
    "SupportingDocumentId" INTEGER NOT NULL CONSTRAINT "PK_SupportingDocuments" PRIMARY KEY AUTOINCREMENT,
    "ClaimId" INTEGER NOT NULL,
    "FileName" TEXT NOT NULL,
    "ContentType" TEXT NOT NULL,
    "FilePath" TEXT NOT NULL,
    "UploadDate" TEXT NOT NULL,
    CONSTRAINT "FK_SupportingDocuments_Claims_ClaimId" FOREIGN KEY ("ClaimId") REFERENCES "Claims" ("ClaimId") ON DELETE CASCADE
);

CREATE INDEX "IX_Approvals_ClaimId" ON "Approvals" ("ClaimId");

CREATE INDEX "IX_SupportingDocuments_ClaimId" ON "SupportingDocuments" ("ClaimId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251021211530_InitialCreate', '9.0.10');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251022180907_Part2_Schema', '9.0.10');

COMMIT;

