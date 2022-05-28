CREATE TABLE [dbo].[UserPermission]
(
	[UserId] INT NOT NULL , 
    [PermissionId] INT NOT NULL, 
    CONSTRAINT [FK_UserPermission_User] FOREIGN KEY (UserId) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY (PermissionId) REFERENCES [Permission]([PermissionId]), 
    PRIMARY KEY ([UserId], [PermissionId])
)
