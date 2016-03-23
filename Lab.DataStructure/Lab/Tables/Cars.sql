CREATE TABLE [dbo].[Cars]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Manufacturer] VARCHAR(50) NOT NULL, 
    [Model] VARCHAR(50) NOT NULL, 
    [Color] VARCHAR(50) NULL, 
    [Year] INT NULL, 
    [Category] VARCHAR(50) NULL
)
