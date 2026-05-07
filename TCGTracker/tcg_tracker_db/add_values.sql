print '' print '*** using tcg_tracker_db'
GO
USE [tcg_tracker_db]
GO

print '' print '' print 'Adding values to Tables in tcg_tracker_db'

print '*** adding to Role Table ***'
GO
INSERT INTO [dbo].[Role]
	([RoleID],[Description])
VALUES
	('System_Admin','Can manage components needed to make cards. As well as make new games.'),
	
	('Site_Admin','Has the same access as a site manager, but can also deactivate or reactivate
					games, series, boosters, and cards.'),
	
	('Site_Manager','Can create and edit boosters,boosters,and cards.'),
	
	('General','Standard user that can create decks and collections. Can view card.
				Can not create new cards or anything related to cards.')
GO

print '*** adding to Users Table ***'
GO
INSERT INTO [dbo].[Users]
	([GivenName],[Surname],[Email])
VALUES
	('john','stewart','john@mail.com'),
	('ceedee','lamb','lamb@mail.com'),
	('daniel','jones','daniel@mail.com'),
	('harrison','mevis','mevis@mail.com'),
	('tom','ford','tom@mail.com')
GO

print '*** adding to UserRole Table ***'
GO
INSERT INTO [dbo].[UserRole]
	([RoleID],[UserID])
VALUES
	('System_Admin',10000),
	('Site_Admin',10001),
	('General',10001),
	('Site_Manager',10002),
	('General',10003),
	('General',10004)
GO