print '' print'*** dropping the database tcg_tracker_db ***'
GO
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'tcg_tracker_db')
BEGIN
	DROP DATABASE [tcg_tracker_db]
END
GO

print '' print'*** creating the database tcg_tracker_db'
GO
CREATE DATABASE [tcg_tracker_db]
GO

print '' print'*** using the database tcg_tracker_db'
GO
USE [tcg_tracker_db]
GO

PRINT '' PRINT '' PRINT 'Creating Tables in tcg_tracker_db'
/*
Used to store roles that a user could have
*/
PRINT '*** creating Role Table ***'
GO
CREATE TABLE [dbo].[Role]
(
	[RoleID]				[nvarchar](50)		NOT NULL	DEFAULT 'Unassigned',
	[Description]			[nvarchar](250)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_role_roleid] PRIMARY KEY ([RoleID] ASC)
)
GO

/*
Used to store user information
connects to roles to check what the user can do

Could make a have image field so a user can select one of a few options
the options could be stored in a different table to make it easier for an admin to add profile pictures
DONT'T LET ALL USERS ADD PROFILE PICTURES
*/
PRINT '*** creating Users Table ***'
GO
CREATE TABLE [dbo].[Users]
(
	[UserID]				[int]				NOT NULL	IDENTITY(10000,1),
	[GivenName]				[nvarchar](50)		NOT NULL,
	[Surname]				[nvarchar](100)		NOT NULL,
	[PasswordHash]			[nvarchar](100)		NOT NULL	DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Email]					[nvarchar](250)		NOT NULL,
	[Active]				[bit]				NOT NULL 	DEFAULT 1,
	
	CONSTRAINT [pk_users_userid] PRIMARY KEY ([UserID] ASC),
	CONSTRAINT [ak_users_email] UNIQUE ([Email] ASC)
)
GO

/*
Used so the roleId dose not directly appear in the users table
this can also be used to assign more than one role to a user
*/
PRINT '*** creating UserRole Table ***'
GO
CREATE TABLE [dbo].[UserRole]
(
	[RoleID]		[nvarchar](50)		NOT NULL,
	[UserID]		[int]				NOT NULL,
	
	CONSTRAINT [pk_userrole_userroleid] PRIMARY KEY([UserID], [RoleID]),
	CONSTRAINT [fk_userrole__roleid] FOREIGN KEY([RoleID]) REFERENCES [Role]([RoleID]),
	CONSTRAINT [fk_userrole_userid] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])

)
GO