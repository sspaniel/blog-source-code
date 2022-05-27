CREATE TABLE [dbo].[UserPermission]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PermissionId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_UserPermission_User] FOREIGN KEY (UserId) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY (PermissionId) REFERENCES [Permission]([PermissionId])
)
