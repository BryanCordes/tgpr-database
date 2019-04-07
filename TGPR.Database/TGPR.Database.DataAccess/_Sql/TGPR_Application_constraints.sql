-- GUID Primary Keys
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_User_UserId')
BEGIN
	ALTER TABLE [User]
		ADD CONSTRAINT DF_User_UserId
		DEFAULT NEWID() FOR [UserId]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_Role_RoleId')
BEGIN
	ALTER TABLE [Role]
		ADD CONSTRAINT DF_Role_RoleId
		DEFAULT NEWID() FOR [RoleId]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_UserRole_UserRoleId')
BEGIN
	ALTER TABLE [UserRole]
		ADD CONSTRAINT DF_UserRole_UserRoleId
		DEFAULT NEWID() FOR [UserRoleId]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_RoleSecurityActivity_RoleSecurityActivityId')
BEGIN
	ALTER TABLE [RoleSecurityActivity]
		ADD CONSTRAINT DF_RoleSecurityActivity_RoleSecurityActivityId
		DEFAULT NEWID() FOR [RoleSecurityActivityId]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_UserRefreshToken_UserRefreshTokenId')
BEGIN
	ALTER TABLE [UserRefreshToken]
		ADD CONSTRAINT DF_UserRefreshToken_UserRefreshTokenId
		DEFAULT NEWID() FOR [UserRefreshTokenId]
END

-- Auto generated dates

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_UserCreatedOn')
BEGIN
	ALTER TABLE [User]
		ADD CONSTRAINT DF_UserCreatedOn
		DEFAULT GETUTCDATE() FOR [CreatedOn]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_UserRefreshTokenCreatedOn')
BEGIN
	ALTER TABLE UserRefreshToken
		ADD CONSTRAINT DF_UserRefreshTokenCreatedOn
		DEFAULT GETUTCDATE() FOR [CreatedOn]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_ApplicationCreatedOn')
BEGIN
	ALTER TABLE Application
		ADD CONSTRAINT DF_ApplicationCreatedOn
		DEFAULT GETUTCDATE() FOR [CreatedOn]
END

IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_ApplicationTemplateCreatedOn')
BEGIN
	ALTER TABLE ApplicationTemplate
		ADD CONSTRAINT DF_ApplicationTemplateCreatedOn
		DEFAULT GETUTCDATE() FOR [CreatedOn]
END

-- This is done due to how EF Core requires raw SQL to be executed
SELECT 1 [VersionId]
	 , 1 [Major]
	 , 1 [Minor]
	 , 1 [Patch]	 