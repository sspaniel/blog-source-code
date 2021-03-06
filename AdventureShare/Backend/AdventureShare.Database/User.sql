CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] NVARCHAR(MAX) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [LastLoginUtc] DATETIME NULL, 
    [LastUpdateUtc] DATETIME NULL, 
    [CreatedUtc] DATETIME NULL DEFAULT GetUtcDate()
)
