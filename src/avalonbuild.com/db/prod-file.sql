IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [File] (
    [Name] nvarchar(450) NOT NULL,
    [Data] varbinary(max),
    [MimeType] nvarchar(max),
    CONSTRAINT [PK_File] PRIMARY KEY ([Name])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170329160423_Initial', N'1.1.1');

GO

