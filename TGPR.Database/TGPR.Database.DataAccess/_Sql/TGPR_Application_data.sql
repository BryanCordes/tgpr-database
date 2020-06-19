IF NOT EXISTS (SELECT 1 FROM ApplicationType)
BEGIN
	INSERT INTO ApplicationType (ApplicationTypeId, Name)
	VALUES (1, 'Companion')
		  ,(2, 'LGD')
		  ,(3, 'Hobby Farm')
		  ,(4, 'Volunteer')
		  ,(5, 'Foster')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationQuestionType)
BEGIN
	INSERT INTO ApplicationQuestionType (ApplicationQuestionTypeId, Name)
	VALUES (1, 'Text')
		  ,(2, 'Single Selection')
		  ,(3, 'Multiple Selection')
		  ,(4, 'Dropdown')
		  ,(5, 'Information')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationStatus)
BEGIN
	INSERT INTO ApplicationStatus (ApplicationStatusId, Name)
	VALUES (1, 'New')
		  ,(2, 'In Progress')
		  ,(3, 'Approved')
		  ,(4, 'Closed')
		  ,(5, 'Do Not Adopt')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationCategoryReviewStatus)
BEGIN
	INSERT INTO ApplicationCategoryReviewStatus (ApplicationCategoryReviewStatusId, Name)
	VALUES (1, 'Not Started')
		  ,(2, 'In Progress')
		  ,(3, 'Satisfactory')
		  ,(4, 'Unsatisfactory')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationOptionStatus)
BEGIN
	INSERT INTO ApplicationOptionStatus (ApplicationOptionStatusId, Name)
	VALUES (1, 'None')
		  ,(2, 'Mark For Review')
		  ,(3, 'Prompt For Exit')		  
END

IF NOT EXISTS (SELECT 1 FROM [Role])
BEGIN
	INSERT INTO [Role] (RoleId, Name)
	VALUES (N'CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 'SuperUser')
END

IF NOT EXISTS (SELECT 1 FROM [User] WHERE UserId = 'F354A96B-8BBE-4267-A9B0-F02FC7280C63')
BEGIN	
	INSERT [User] (UserId, Email, PasswordHash, FirstName, LastName)
	VALUES (N'F354A96B-8BBE-4267-A9B0-F02FC7280C63', N'bryan.cordes@gmail.com', N'$TGPR$DB$10000$yqU2BoHFkKHpCQywBeHHzm/w5bkUSBJLHt/Nlaj12rfmWaqv$', 'Bryan', 'Cordes')		
END

IF NOT EXISTS (SELECT 1 FROM UserRole WHERE UserId='F354A96B-8BBE-4267-A9B0-F02FC7280C63' AND RoleId='CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE')
BEGIN
	INSERT UserRole (UserId, RoleId)
	VALUES (N'F354A96B-8BBE-4267-A9B0-F02FC7280C63', N'CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE')
END

IF NOT EXISTS (SELECT 1 FROM SecurityActivity)
BEGIN
	INSERT SecurityActivity (SecurityActivityId, [Name])
	VALUES (1, 'ApplicationTemplateRead')
		  ,(2, 'ApplicationTemplateWrite')
		  ,(3, 'UserRead')
		  ,(4, 'UserWrite')
		  ,(5, 'UserRoleRead')
		  ,(6, 'UserRoleWrite')		 
		  ,(7, 'CompanionApplicationRead')
		  ,(8, 'CompanionApplicationWrite')
END

IF NOT EXISTS (SELECT 1 FROM RoleSecurityActivity WHERE RoleId='CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE')
BEGIN
	INSERT RoleSecurityActivity (RoleId, SecurityActivityId)
	VALUES ('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 1)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 2)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 3)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 4)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 5)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 6)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 7)
		  ,('CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE', 8)
END

IF NOT EXISTS (SELECT 1 FROM Version)
BEGIN
	INSERT Version (Major, Minor, Patch)
	VALUES (1, 1, 0)
END


-- This is done due to how EF Core requires raw SQL to be executed
SELECT 1 [VersionId]
	 , 1 [Major]
	 , 1 [Minor]
	 , 1 [Patch]	 