IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO
CREATE TABLE [Image] (
    [ID] int NOT NULL IDENTITY,
    [Data] varbinary(max),
    [Name] nvarchar(max),
    CONSTRAINT [PK_Image] PRIMARY KEY ([ID])
);
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170217205200_ImageCreate', N'1.0.1');
GO
