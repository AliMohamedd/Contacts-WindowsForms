USE [ContactsDB];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- Create Countries table first
CREATE TABLE [dbo].[Countries]
(
    [CountryID] INT IDENTITY(1,1) NOT NULL,
    [CountryName] NVARCHAR(50) NULL,
    [Code] NVARCHAR(3) NULL,
    [PhoneCode] NVARCHAR(3) NULL,

    CONSTRAINT [PK_Countries]
        PRIMARY KEY CLUSTERED ([CountryID] ASC)
);
GO

-- Create Contacts table
CREATE TABLE [dbo].[Contacts]
(
    [ContactID] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [Phone] NVARCHAR(20) NOT NULL,
    [Address] NVARCHAR(200) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    [CountryID] INT NOT NULL,
    [ImagePath] NVARCHAR(500) NULL,

    CONSTRAINT [PK_Contacts]
        PRIMARY KEY CLUSTERED ([ContactID] ASC)
);
GO

-- Create Foreign Key
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_Contacts_Countries]
FOREIGN KEY ([CountryID])
REFERENCES [dbo].[Countries] ([CountryID]);
GO