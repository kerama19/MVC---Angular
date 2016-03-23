CREATE TABLE [dbo].[Permission]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Function] VARCHAR(50) NOT NULL, 
    [Rights] VARCHAR(10) NOT NULL, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [FK_Permission_ToUser] FOREIGN KEY (UserId) REFERENCES [User]([id])
)
