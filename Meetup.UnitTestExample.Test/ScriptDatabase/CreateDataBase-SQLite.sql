CREATE TABLE [User](
	[Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	[FirstName] nvarchar(50) NOT NULL,
	[LastName] nvarchar(50) NOT NULL,
	[Username] nvarchar(50) NOT NULL,
	[IsBot] bit NULL
 );