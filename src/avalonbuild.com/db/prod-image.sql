IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Galleries] (
    [ID] int NOT NULL IDENTITY,
    [Description] nvarchar(max),
    [Name] nvarchar(max),
    [Title] nvarchar(max),
    CONSTRAINT [PK_Galleries] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Images] (
    [ID] int NOT NULL IDENTITY,
    [Description] nvarchar(max),
    [FileName] nvarchar(max),
    [ThumbnailFileName] nvarchar(max),
    [Name] nvarchar(max) NOT NULL,
    [Title] nvarchar(max),
    CONSTRAINT [PK_Images] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [GalleryImage] (
    [GalleryID] int NOT NULL,
    [ImageID] int NOT NULL,
    CONSTRAINT [PK_GalleryImage] PRIMARY KEY ([GalleryID], [ImageID]),
    CONSTRAINT [FK_GalleryImage_Galleries_GalleryID] FOREIGN KEY ([GalleryID]) REFERENCES [Galleries] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_GalleryImage_Images_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Images] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_GalleryImage_ImageID] ON [GalleryImage] ([ImageID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170329160412_Initial', N'1.1.1');

GO

