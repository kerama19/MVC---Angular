-- Insert User Data
INSERT INTO 
	[dbo].[User] (username, email, password, Active) 
VALUES 
	('kerama',
	'kenneth.ramirez@proximitycr.com', 
	'202CB962AC59075B964B07152D234B70', 
	1)

--Insert Permission Data
INSERT INTO
	Permission ([Function], Rights, UserId)
VALUES
	('Users', 'Read', (SELECT TOP 1 Id FROM [User]))

INSERT INTO
	Permission ([Function], Rights, UserId)
VALUES
	('Users', 'Create', (SELECT TOP 1 Id FROM [User]))

INSERT INTO
	Permission ([Function], Rights, UserId)
VALUES
	('Users', 'Update', (SELECT TOP 1 Id FROM [User]))

INSERT INTO
	Permission ([Function], Rights, UserId)
VALUES
	('Users', 'Delete', (SELECT TOP 1 Id FROM [User]))



GO
