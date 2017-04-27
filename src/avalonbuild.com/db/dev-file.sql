CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "File" (
    "Name" TEXT NOT NULL CONSTRAINT "PK_File" PRIMARY KEY,
    "Data" BLOB,
    "MimeType" TEXT
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170329160423_Initial', '1.1.1');

