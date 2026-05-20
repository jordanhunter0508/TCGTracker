print '' print '*** using tcg_tracker_db'
GO
USE [tcg_tracker_db]
GO

print '' print '*************** Start User SPs ***************' 

print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
		@Email				[nvarchar](250),
		@PasswordHash		[nvarchar](256)
	)
AS
	BEGIN
		SELECT	COUNT([Users].[UserID])
		FROM	[Users]
		WHERE	[Users].[Email] = @Email
			AND	[Users].[PasswordHash] = @PasswordHash
			AND	[Users].[Active] = 1;
	END
GO

print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[UserID],[GivenName],[Surname],[Email],[Active]
		FROM	[Users]
		WHERE	[Email] = @Email;
	END
GO

print '*** creating sp_select_roles_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[UserRole].[RoleID]
		FROM	[UserRole] JOIN [Users] ON [UserRole].[UserID] = [Users].[UserID]
		WHERE	[Users].[Email] = @Email;
	END
GO

print '*************** End User SPs ***************' 










print '' print '*************** Start Game SPs ***************' 

print '*** creating sp_select_game ***'
GO
CREATE PROCEDURE [dbo].[sp_select_game]
(
	@GameID					[int]
)
AS
	BEGIN
		SELECT	[GameID],[Name],[Publisher],
				[OfficialWebsite],[Active]
		FROM	[Game]
		WHERE	[GameID] = @GameID;
	END
GO

print '*** creating sp_select_all_games ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_games]
AS
	BEGIN
		SELECT	[GameID],[Name],[Publisher],
				[OfficialWebsite],[Active]
		FROM	[Game];
	END
GO

print '*** creating sp_insert_game ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_game]
(
	@Name					[nvarchar](100),
	@Publisher				[nvarchar](100),
	@OfficialWebsite		[nvarchar](250)
)
AS
	BEGIN
		INSERT INTO [dbo].[Game]
		([Name],[Publisher],[OfficialWebsite])
		VALUES
		(@Name,@Publisher,@OfficialWebsite);
		
		SELECT SCOPE_IDENTITY();
	END
GO

print '*** creating sp_update_game ***'
GO
CREATE PROCEDURE [dbo].[sp_update_game]
(
	@GameID					[int],				
	@Name					[nvarchar](100),
	@Publisher				[nvarchar](100),
	@OfficialWebsite		[nvarchar](250)
)
AS
	BEGIN
		UPDATE	[dbo].[Game]
		SET		[Name] = @Name,
				[Publisher] = @Publisher,
				[OfficialWebsite] = @OfficialWebsite
		WHERE	[GameID] = @GameID
		RETURN	@@ROWCOUNT;
	END
GO

print '*************** End Game SPs ***************' 