--So, I decided to do something completely different from my .NET II final like two weeks before it was due, and this was the best option.

--Also, I did all of the keys inline, to see what that would be like.
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'VideoGameDatabase')
BEGIN
	print '' print '*** dropping database VideoGameDatabase'
	DROP DATABASE [VideoGameDatabase]
END

print '' print '*** creating database VideoGameDatabase'
GO
	CREATE DATABASE [VideoGameDatabase]
GO

print '' print '*** using database VideoGameDatabase'
GO
	USE [VideoGameDatabase]
GO

print '' print '***Creating table USER***'
GO
CREATE TABLE [dbo].[USER](
    USER_ID NVARCHAR(440) CONSTRAINT pk_USERNAME PRIMARY KEY,
)
GO


print'' print'***Inserting sample USER data***'
GO
INSERT INTO [dbo].[USER]
	(USER_ID)
VALUES
	("zhugelianftw@gmail.com")
GO


--This is living lookup table; it is meant to be extended later, but the default should be extensive.
--Basically, this is to fill a drop-down can also be added to later.
print'' print'***Creating table CONSOLE***'
GO
CREATE TABLE [dbo].[CONSOLE](
	CONSOLE_ID NVARCHAR(200) CONSTRAINT pk_CONSOLE_ID PRIMARY KEY,
	IS_APPROVED BIT DEFAULT 1
)
GO

print'' print'***Inserting sample CONSOLE data***'
GO
INSERT INTO [dbo].[CONSOLE]
	(CONSOLE_ID)
VALUES
	("Atari 2600"),
	("ColecoVision"),
	("Intellivision"),
	("Nintendo Entertainment System"),
	("Sega Genesis"),
	("Super Nintendo Entertainment System"),
	("Sega CD"),
	("Sega 32X"),
	("Sega 32X CD"),
	("Nintendo 64"),
	("Playstation"),
	("Playstation 2"),
	("GameCube"),
	("Xbox"),
	("Wii"),
	("Playstation 3"),
	("Xbox 360"),
	("Wii U"),
	("Playstation 4"),
	("Xbox One"),
	--Don't forget hand-helds!
	("Game Boy"),
	("Game Gear"),
	("Atari Lynx"),
	("Game Boy Color"),
	("Game Boy Advance"),
	("DS"),
	("PSP"),
	("3DS"),
	("PS Vita"),
	("New 3DS"),
	--Or PC games... These could probably be broken down by specific OS version or something.
	("DOS"),
	("Windows"),
	("Mac"),
	("Linux")
GO

--Just like the console table, but not as useful.
print'' print'***Creating table DEVELOPER'
GO
CREATE TABLE [dbo].[DEVELOPER](
	DEVELOPER_ID NVARCHAR(200) CONSTRAINT pk_DEVELOPER_ID PRIMARY KEY,
	IS_APPROVED BIT DEFAULT 1
)
GO

--https://en.wikipedia.org/wiki/List_of_video_game_developers
--Most of these came from the following link.

print'' print'***Inserting sample DEVELOPER data'
GO
INSERT INTO [dbo].[DEVELOPER]
	(DEVELOPER_ID)
VALUES
	("0verflow"),
	("1st Playable Productions"),
	("2K Czech"),
	("2K Games"),
	("Illusion Softworks"),
	("2XL Games"),
	("343 Industries"),
	("38 Studios"),
	("3D Realms"),
	("3G Studios"),
	("Battleborne Entertainment"),
	("42 Entertainment"),
	("4A Games"),
	("5pb. Inc."),
	("5th Cell"),
	("989 Studios"),
	("Abstraction Games"),
	("Acclaim Entertainment"),
	("Accolade"),
	("Access Games"),
	("Access Software"),
	("ACE Team"),
	("Aces Studio"),
	("Acheron Design"),
	("Action Forms"),
	("Active Gaming Media"),
	("Activision"),
	("Activision Blizzard"),
	("Adventure Soft"),
	("Akella"),
	("Aki Corporation"),
	("Alfa System"),
	("Amazon Game Studios"),
	("Ancient"),
	("Anino Games"),
	("Ankama Games"),
	("AQ Interactive"),
	("Arc System Works"),
	("Arkane Studios"),
	("Arkedo Studio"),
	("ArenaNet"),
	("Arika"),
	("Armor Project"),
	("Art Co., Ltd"),
	("Artdink"),
	("ArtePiazza"),
	("Artifical Studios"),
	("Artoon"),
	("Ascaron"),
	("Asobo Studio"),
	("Atlus"),
	("Atomic Planet Entertainment"),
	("Attic Entertainment Software"),
	("Avalanche Software"),
	("Avalanche Studios"),
	("Aventurine SA"),
	("Babaroga"),
	("Backbone Entertainment"),
	("Bandai Namco Studios"),
	("Banpresto"),
	("Bauhaus Entertainment"),
	("Beenox"),
	("Behaviour Interactive"),
	("Bethesda Softworks"),
	("Big Huge Games"),
	("Binary Hammer"),
	("BioWare"),
	("Bird Studio"),
	("The Bitmap Brothers"),
	("Bits Studios"),
	("Bizarre Creations"),
	("Black Forest Games"),
	("Black Isle Studios"),
	("Black Rock Studio"),
	("Black Shell Games"),
	("Black Wing Foundation"),
	("Blitz Games Studios"),
	("Blizzard Entertainment"),
	("Blue Byte Software"),
	("Blue Fang Games"),
	("Blue Tongue Entertainment"),
	("Bohemia Interactive"),
	("Boss Fight Entertainment"),
	("Boss Key Productions"),
	("BreakAway Games"),
	("Brøderbund"),
	("Brownie Brown"),
	("Bugbear Entertainment"),
	("Buka Entertainment"),
	("Bullfrog Productions"),
	("Bungie"),
	("Capcom"),
	("Capcom Vancouver"),
	("Blue Castle Games"),
	("Carbine Studios"),
	("Cauldron"),
	("Cave"),
	("Cavia"),
	("CCP Games"),
	("CD Projekt RED"),
	("Centuri"),
	("Certain Affinity"),
	("Chair Entertainment"),
	("Chunsoft"),
	("Spike"),
	("Spike Chunsoft"),
	("Cing"),
	("Clap Hanz"),
	("Climax Entertainment"),
	("Climax Studios"),
	("Cloud Imperium Games"),
	("Clover Studio"),
	("Coded Illusions"),
	("Codemasters"),
	("Coktel Vision"),
	("Colossal Order"),
	("Compile Heart"),
	("Compulsion Games"),
	("Core Design"),
	("Crafts & Meister"),
	("Creat Studios"),
	("Creative Assembly"),
	("Criterion Games"),
	("Croteam"),
	("Cryo Interactive"),
	("Cryptic Studios"),
	("Crstal Dynamics"),
	("Crytek"),
	("Free Radical Design"),
	("Crtyek UK"),
	("Cyan Worlds"),
	("Cyanide"),
	("Cyberdrams"),
	("CyberConnect2"),
	("Cyberlore Studios"),
	("CyberStep"),
	("D-Pad Studio"),
	("Dambuster Studios"),
	("Day 1 Studios"),
	("Deadline Games"),
	("Deck13"),
	("Demiurge Studios"),
	("devCAT Studios"),
	("Dhruva Interactive"),
	("Die Gute Fabrik"),
	("Digital Illusions CE"),
	("DICE"),
	("Digital Reality"),
	("Dimps"),
	("Disney Interactive Studios"),
	("Double Fine Productions"),
	("Double Helix Games"),
	("Dynamix"),
	("Egosoft"),
	("Eidos Interactive"),
	("Electronic Arts"),
	("Elemental Games"),
	("Elev8 Games"),
	("Engine Software"),
	("Ensemble Studios"),
	("Epic Games"),
	("Epic Games Poland"),
	("People Can Fly"),
	("Epicenter Studios"),
	("Epyx"),
	("Etranges Libellules"),
	("Eugen Systems"),
	("Eurocom"),
	("Evolution Studios"),
	("Eyedentity Games"),
	("F4"),
	("Facepunch Studios"),
	("FarSight Studios"),
	("Fatshark"),
	("feelplus"),
	("Firaxis Games"),
	("Firefly Studios"),
	("First Star Software"),
	("Flagship Studios"),
	("Flying Wild Hog"),
	("Foundation 9 Entertainment"),
	("Fox Interactive"),
	("Frictional Games"),
	("Frogwares"),
	("FromSoftware"),
	("Frontier Developments"),
	("Frozenbyte"),
	("FUN Labs"),
	("Funcom"),
	("Futuremark"),
	("Gaijin Entertainment"),
	("Game Arts"),
	("Game Freak"),
	("GameHouse"),
	("Games2win"),
	("Gearbox Software"),
	("Geewa"),
	("Genki"),
	("Giant Interactive"),
	("Glu Mobile"),
	("Gogii Games"),
	("Good-Feel"),
	("Grasshopper Manufacture"),
	("Gravity"),
	("Gremlin Interactive"),
	("Grezzo"),
	("Griptonite Games"),
	("GSC Game World"),
	("Guerrilla Cambridge"),
	("Guerrilla Games"),
	("GungHo Online Entertainment"),
	("Gust Corporation"),
	("Haemimont Games"),
	("HappySoft"),
	("HAL Laboratory"),
	("Halfbrick"),
	("Hanaho"),
	("Hangar 13"),
	("Harebrained Schemes"),
	("Harmonix Music Systems"),
	("Hasbro Interactive"),
	("HB Studios"),
	("HeroCraft"),
	("High Moon Studios"),
	("High Voltage Software"),
	("Hoplon Infotainment"),
	("Hothead Games"),
	("Housemarque"),
	("Hudson Soft"),
	("Human Head Studios"),
	("Humongous Entertainment"),
	("Hyperion Entertainment"),
	("id Software"),
	("Idea Factory"),
	("Idol Minds"),
	("Imageepoch"),
	("Infinity Ward"),
	("Infocom"),
	("Infogrames"),
	("Incredible Technologies"),
	("indieszero"),
	("Innerloop Studios"),
	("Insomniac Games"),
	("Intelligent Systems"),
	("Interceptor Entertainment"),
	("Interplay Entertainment"),
	("Inti Creates"),
	("Introversion Software"),
	("IO Interactive"),
	("Ion Storm"),
	("Ion Storm Austin"),
	("Irem"),
	("Iron Galaxy Studios"),
	("Irrational Games"),
	("Jadestone Group"),
	("Jagex"),
	("Jaleco"),
	("Javaground"),
	("JoWood Entertainment AG"),
	("Juice Games"),
	("Jupiter"),
	("JV Games"),
	("Kairosoft"),
	("Kalypso Media"),
	("Kaos Studios"),
	("Keen Software House"),
	("Kiloo Games"),
	("King Digital Entertainment"),
	("Koei"),
	("KOG Studios"),
	("Konami"),
	("Kongzhong"),
	("Krome Studios"),
	("Krome Studios Melbourne"),
	("Kuju Entertainment"),
	("Kush Games"),
	("Kuma Reality Games"),
	("Larian Studios"),
	("Legacy Interactive"),
	("Legend Entertainment"),
	("Legendo Entertainment"),
	("LEGO Group"),
	
	("Nintendo")
GO

--Just like the developer table, but not as useful.
print'' print'***Creating table PUBLISHER'
GO
CREATE TABLE [dbo].[PUBLISHER](
	PUBLISHER_ID NVARCHAR(200) CONSTRAINT pk_PUBLISHER_ID PRIMARY KEY,
	IS_APPROVED BIT DEFAULT 1
)
GO

print'' print'***Inserting sample PUBLISHER data***'
GO
INSERT INTO [dbo].[PUBLISHER]
	(PUBLISHER_ID)
VALUES
	("2K Games"),
	("Microsoft"),
	("Nintendo"),
	("Sony")
GO

print '' print '***Creating table GAME***'
GO
CREATE TABLE [dbo].[GAME](
	GAME_ID INTEGER IDENTITY(10000, 1) CONSTRAINT pk_GAME_ID PRIMARY KEY,
	NAME NVARCHAR(200) NOT NULL,
	CONSOLE_ID NVARCHAR(200) CONSTRAINT fk_GAME_CONSOLE_ID REFERENCES CONSOLE(CONSOLE_ID) NOT NULL,
	DEVELOPER_ID NVARCHAR(200) CONSTRAINT fk_GAME_DEVELOPER_ID REFERENCES DEVELOPER(DEVELOPER_ID),
	PUBLISHER_ID NVARCHAR(200) CONSTRAINT fk_GAME_PUBLISHER_ID REFERENCES PUBLISHER(PUBLISHER_ID),
	
	IS_APPROVED BIT DEFAULT 1
)
GO

print'' print'***Inserting sample GAME data***'
GO
INSERT INTO [dbo].[GAME]
	(NAME, CONSOLE_ID, DEVELOPER_ID, PUBLISHER_ID)
VALUES
	("Super Mario Bros.", "Nintendo Entertainment System", "Nintendo", "Nintendo"),
	("Super Mario Bros. 2", "Nintendo Entertainment System", "Nintendo", "Nintendo"),
	("Super Mario Bros. 3", "Nintendo Entertainment System", "Nintendo", "Nintendo"),
	("Super Mario World", "Super Nintendo Entertainment System", "Nintendo", "Nintendo"),
	("Super Mario 64", "Nintendo 64", "Nintendo", "Nintendo"),
	("Super Mario Sunshine", "GameCube", "Nintendo", "Nintendo"),
	("Super Mario Galaxy", "Wii", "Nintendo", "Nintendo"),
	("Super Mario Galaxy 2", "Wii", "Nintendo", "Nintendo"),
	("Super Mario 3D Land", "3DS", "Nintendo", "Nintendo"),
	("Super Mario 3D World", "Wii U", "Nintendo", "Nintendo"),
	("Borderlands 2", "Xbox 360", "Gearbox Software", "2K Games"),
	("Borderlands 2", "Playstation 3", "Gearbox Software", "2K Games"),
	("Gears of War", "Xbox 360", "Epic Games", "Microsoft")
GO

print '' print '***Creating table USER_GAME'
GO
CREATE TABLE [dbo].[USER_GAME](
	GAME_ID INTEGER CONSTRAINT fk_USER_GAME_GAME_ID REFERENCES GAME(GAME_ID) ,
	USER_ID NVARCHAR(440) CONSTRAINT fk_USER_GAME_USER_ID REFERENCES [USER](USER_ID) ON DELETE CASCADE,
	
	CONSTRAINT pk_USER_GAME_ID PRIMARY KEY(GAME_ID ASC, USER_ID ASC)
)
GO

--Most User stuff is done with EF, including adding 
ALTER TABLE USER_GAME NOCHECK CONSTRAINT fk_USER_GAME_USER_ID

print'' print'***Inserting sample USER_GAME data***'
GO
INSERT INTO [dbo].[USER_GAME]
	(GAME_ID, USER_ID)
VALUES
	(10000, "zhugelianftw@gmail.com"),
	(10001, "zhugelianftw@gmail.com"),
	(10003, "zhugelianftw@gmail.com")
GO

ALTER TABLE USER_GAME CHECK CONSTRAINT fk_USER_GAME_USER_ID

print '' print '***Creating procedure vsp_retrieve_user_games'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_user_games]
(
	@USERNAME NVARCHAR(440)
)
AS
	BEGIN
		SELECT GAME.GAME_ID, NAME, CONSOLE_ID, DEVELOPER_ID, PUBLISHER_ID
		FROM GAME, USER_GAME
		WHERE USER_ID = @USERNAME
		AND GAME.GAME_ID = USER_GAME.GAME_ID
	END
GO

print '' print '***Creating procedure vsp_retrieve_games'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_games]
(
	@IS_APPROVED BIT
)
AS
	BEGIN
		SELECT GAME_ID, NAME, CONSOLE_ID, DEVELOPER_ID, PUBLISHER_ID
		FROM GAME
		WHERE IS_APPROVED = @IS_APPROVED
	END
GO

print '' print '***Creating procedure vsp_retrieve_game'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_game]
(
	@GAME_ID INT
)
AS
	BEGIN
		SELECT GAME_ID, NAME, CONSOLE_ID, DEVELOPER_ID, PUBLISHER_ID
		FROM GAME
		WHERE GAME_ID = @GAME_ID
	END
GO

print '' print '***Creating procedure vsp_retrieve_consoles'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_consoles]
(
	@IS_APPROVED BIT
)
AS
	BEGIN
		SELECT CONSOLE_ID
		FROM CONSOLE
		WHERE IS_APPROVED = @IS_APPROVED
	END
GO

print '' print '***Creating procedure vsp_retrieve_developers'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_developers]
(
	@IS_APPROVED BIT
)
AS
	BEGIN
		SELECT DEVELOPER_ID
		FROM DEVELOPER
		WHERE IS_APPROVED = @IS_APPROVED
	END
GO

print '' print '***Creating procedure vsp_retrieve_publishers'
GO
CREATE PROCEDURE [dbo].[vsp_retrieve_publishers]
(
	@IS_APPROVED BIT
)
AS
	BEGIN
		SELECT PUBLISHER_ID
		FROM PUBLISHER
		WHERE IS_APPROVED = @IS_APPROVED
	END
GO

print '' print '***Creating procedure vsp_create_user'
GO
CREATE PROCEDURE [dbo].[vsp_create_user]
(
	@USER_ID NVARCHAR(440)
)
AS
	INSERT INTO [dbo].[USER]
		(USER_ID)
	VALUES
		(@USER_ID)
	RETURN @@ROWCOUNT
GO


print'' print'***Creating procedure vsp_create_console'
GO
CREATE PROCEDURE [dbo].[vsp_create_console]
(
	@NAME VARCHAR(200),
	@IS_APPROVED BIT
)
AS
	INSERT INTO [dbo].[CONSOLE]
		(CONSOLE_ID, IS_APPROVED)
	VALUES
		(@NAME, @IS_APPROVED)
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_create_developer'
GO
CREATE PROCEDURE [dbo].[vsp_create_developer]
(
	@NAME VARCHAR(200),
	@IS_APPROVED BIT
)
AS
	INSERT INTO [dbo].[DEVELOPER]
		(DEVELOPER_ID, IS_APPROVED)
	VALUES
		(@NAME, @IS_APPROVED)
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_create_publisher'
GO
CREATE PROCEDURE [dbo].[vsp_create_publisher]
(
	@NAME VARCHAR(200),
	@IS_APPROVED BIT
)
AS
	INSERT INTO [dbo].[PUBLISHER]
		(PUBLISHER_ID, IS_APPROVED)
	VALUES
		(@NAME, @IS_APPROVED)
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_create_game'
GO
CREATE PROCEDURE [dbo].[vsp_create_game]
(
	@NAME NVARCHAR(200),
	@CONSOLE NVARCHAR(200),
	@DEVELOPER NVARCHAR(200),
	@PUBLISHER NVARCHAR(200),
	@IS_APPROVED BIT
)
AS
	INSERT INTO [dbo].[GAME]
		(NAME, CONSOLE_ID, DEVELOPER_ID, PUBLISHER_ID, IS_APPROVED)
	VALUES
		(@NAME, @CONSOLE, @DEVELOPER, @PUBLISHER, @IS_APPROVED)
	SELECT SCOPE_IDENTITY()
GO

print'' print'***Creating procedure vsp_create_user_game'
GO
CREATE PROCEDURE [dbo].[vsp_create_user_game]
(
	@GAME_ID INT,
	@USER_ID NVARCHAR(440)
)
AS
	--Add it if and only if it doesn't already exist...
	IF NOT EXISTS(SELECT * 
				  FROM USER_GAME 
				  WHERE USER_ID = @USER_ID 
				  AND GAME_ID = @GAME_ID)
	BEGIN
		INSERT INTO [dbo].[USER_GAME]
			(USER_ID, GAME_ID)
		VALUES
			(@USER_ID, @GAME_ID)
		RETURN @@ROWCOUNT
	END
GO

print'' print'***Creating procedure vsp_delete_user_game'
GO
CREATE PROCEDURE [dbo].[vsp_delete_user_game]
(
	@GAME_ID INT,
	@USER_ID NVARCHAR(440)
)
AS
	DELETE FROM [dbo].[USER_GAME]
	WHERE GAME_ID = @GAME_ID
	AND   USER_ID = @USER_ID
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_update_game'
GO
CREATE PROCEDURE [dbo].[vsp_update_game]
(
	@GAME_ID INT,
	
	@OLD_NAME NVARCHAR(200),
	@OLD_CONSOLE NVARCHAR(200),
	@OLD_DEVELOPER NVARCHAR(200),
	@OLD_PUBLISHER NVARCHAR(200),
	
	@NEW_NAME NVARCHAR(200),
	@NEW_CONSOLE NVARCHAR(200),
	@NEW_DEVELOPER NVARCHAR(200),
	@NEW_PUBLISHER NVARCHAR(200)
)
AS
	UPDATE [dbo].[GAME]
	SET NAME = @NEW_NAME, CONSOLE_ID = @NEW_CONSOLE, DEVELOPER_ID = @NEW_DEVELOPER, PUBLISHER_ID = @NEW_PUBLISHER
	WHERE NAME = @OLD_NAME AND CONSOLE_ID = @OLD_CONSOLE AND DEVELOPER_ID = @OLD_DEVELOPER AND PUBLISHER_ID = @OLD_PUBLISHER
	AND GAME_ID = @GAME_ID
GO

print'' print'***Creating procedure vsp_delete_developer'
GO
CREATE PROCEDURE [dbo].[vsp_delete_developer]
(
	@NAME NVARCHAR(200)
)
AS
	DELETE FROM [dbo].[DEVELOPER]
	WHERE DEVELOPER_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_delete_console'
GO
CREATE PROCEDURE [dbo].[vsp_delete_console]
(
	@NAME NVARCHAR(200)
)
AS
	DELETE FROM [dbo].[CONSOLE]
	WHERE CONSOLE_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_delete_publisher'
GO
CREATE PROCEDURE [dbo].[vsp_delete_publisher]
(
	@NAME NVARCHAR(200)
)
AS
	DELETE FROM [dbo].[PUBLISHER]
	WHERE PUBLISHER_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_delete_game'
GO
CREATE PROCEDURE [dbo].[vsp_delete_game]
(
	@GAME_ID INT
)
AS
	--This transaction is actually just the same as ON DELETE CASCADE, but I somehow forgot about that...
	/*BEGIN TRANSACTION
	
	DELETE FROM [dbo].[USER_GAME]
	WHERE GAME_ID = @GAME_ID
	*/
	DELETE FROM [dbo].[GAME]
	WHERE GAME_ID = @GAME_ID
	/*
	COMMIT TRANSACTION
	*/
GO

print'' print'***Creating procedure vsp_approve_publisher'
GO
CREATE PROCEDURE [dbo].[vsp_approve_publisher]
(
	@NAME NVARCHAR(200)
)
AS
	UPDATE [dbo].[PUBLISHER]
	SET IS_APPROVED = 1
	WHERE PUBLISHER_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_approve_console'
GO
CREATE PROCEDURE [dbo].[vsp_approve_console]
(
	@NAME NVARCHAR(200)
)
AS
	UPDATE [dbo].[CONSOLE]
	SET IS_APPROVED = 1
	WHERE CONSOLE_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_approve_developer'
GO
CREATE PROCEDURE [dbo].[vsp_approve_developer]
(
	@NAME NVARCHAR(200)
)
AS
	UPDATE [dbo].[DEVELOPER]
	SET IS_APPROVED = 1
	WHERE DEVELOPER_ID = @NAME
	RETURN @@ROWCOUNT
GO

print'' print'***Creating procedure vsp_approve_game'
GO
CREATE PROCEDURE [dbo].[vsp_approve_game]
(
	@GAME_ID INT
)
AS
	UPDATE [dbo].[GAME]
	SET IS_APPROVED = 1
	WHERE GAME_ID = @GAME_ID
	RETURN @@ROWCOUNT
GO

--One final print to make it look better.
print''
GO
