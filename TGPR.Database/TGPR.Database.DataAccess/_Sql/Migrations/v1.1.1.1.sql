IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.ApplicationQuestionAnswer') 
			   AND name = 'Text'
			   AND is_nullable = 1)
BEGIN
	ALTER TABLE [ApplicationQuestionAnswer]
	ALTER COLUMN [Text] [nvarchar](MAX) NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.ApplicationQuestion') 
			   AND name = 'Width')
BEGIN
	ALTER TABLE [ApplicationQuestion]
	ADD [Width] [int] DEFAULT 12 NOT NULL
END

-- ===========================================================
-- ADD NEW APPLICATION COLUMNS
-- ===========================================================

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'IsTest')
BEGIN
	ALTER TABLE Application
	ADD IsTest [bit] DEFAULT 0 NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'FirstName')
BEGIN
	ALTER TABLE Application
	ADD FirstName [nvarchar](100) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'LastName')
BEGIN
	ALTER TABLE Application
	ADD LastName [nvarchar](100) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'Email')
BEGIN
	ALTER TABLE Application
	ADD Email [nvarchar](150) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'Address')
BEGIN
	ALTER TABLE Application
	ADD Address [nvarchar](300) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'Address2')
BEGIN
	ALTER TABLE Application
	ADD Address2 [nvarchar](100) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'City')
BEGIN
	ALTER TABLE Application
	ADD City [nvarchar](300) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'State')
BEGIN
	ALTER TABLE Application
	ADD [State] [nvarchar](2) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'Zip')
BEGIN
	ALTER TABLE Application
	ADD Zip [nvarchar](10) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'Phone')
BEGIN
	ALTER TABLE Application
	ADD Phone [nvarchar](15) DEFAULT '' NOT NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'UpdatedOn')
BEGIN
	ALTER TABLE Application
	ADD UpdatedOn datetime NULL
END

IF NOT EXISTS (SELECT 1 FROM sys.columns 
			   WHERE OBJECT_ID = OBJECT_ID('dbo.Application') 
			   AND name = 'UpdatedById')
BEGIN
	ALTER TABLE Application
	ADD UpdatedById uniqueidentifier NULL
END

-- ===========================================================
-- ADD NEW PERMISSIONS
-- ===========================================================

IF NOT EXISTS (SELECT 1 FROM [SecurityActivity]
			   WHERE SecurityActivityId = 7)
BEGIN
	INSERT SecurityActivity (SecurityActivityId, [Name])
	VALUES (7, 'CompanionApplicationRead')
		  ,(8, 'CompanionApplicationWrite')

	INSERT RoleSecurityActivity (RoleId, SecurityActivityId)
	VALUES ('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 7)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 8)
END

-- This is done due to how EF Core requires raw SQL to be executed
SELECT 1 [VersionId]
	 , 1 [Major]
	 , 1 [Minor]
	 , 1 [Patch]	 
