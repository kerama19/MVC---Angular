CREATE TABLE [dbo].[Address]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CompanyName] VARCHAR(50) NOT NULL, 
    [AddressLine1] VARCHAR(50) NOT NULL, 
    [AddressLine2] VARCHAR(50) NULL, 
    [State] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NULL, 
    [Country] VARCHAR(50) NULL, 
    [Zip] VARCHAR(50) NULL
)
