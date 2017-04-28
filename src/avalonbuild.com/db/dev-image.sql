CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "Galleries" (
    "ID" INTEGER NOT NULL CONSTRAINT "PK_Galleries" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT,
    "Name" TEXT,
    "Title" TEXT
);

CREATE TABLE "Images" (
    "ID" INTEGER NOT NULL CONSTRAINT "PK_Images" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT,
    "FileName" TEXT,
    "ThumbnailFileName" TEXT,
    "Name" TEXT NOT NULL,
    "Title" TEXT
);

CREATE TABLE "GalleryImage" (
    "GalleryID" INTEGER NOT NULL,
    "ImageID" INTEGER NOT NULL,
    CONSTRAINT "PK_GalleryImage" PRIMARY KEY ("GalleryID", "ImageID"),
    CONSTRAINT "FK_GalleryImage_Galleries_GalleryID" FOREIGN KEY ("GalleryID") REFERENCES "Galleries" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_GalleryImage_Images_ImageID" FOREIGN KEY ("ImageID") REFERENCES "Images" ("ID") ON DELETE CASCADE
);

CREATE INDEX "IX_GalleryImage_ImageID" ON "GalleryImage" ("ImageID");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170329160412_Initial', '1.1.1');

