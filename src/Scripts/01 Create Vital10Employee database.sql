USE [Vital10Employees]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Partner]') AND type in (N'U'))
DROP TABLE [dbo].[Partner]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO



CREATE TABLE [dbo].[Partner](
	[Id] [int]  IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL, 
	[PartnerEmployeeId] [int] NULL, 
	PRIMARY KEY ([Id])
);


CREATE TABLE [dbo].[Employee](
	[Id] [int]  IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL
	PRIMARY KEY ([Id])
);

ALTER TABLE [dbo].[Partner]
ADD FOREIGN KEY ([EmployeeId]) REFERENCES [Employee](Id);

ALTER TABLE [dbo].[Partner]
ADD FOREIGN KEY ([PartnerEmployeeId]) REFERENCES [Employee](Id);




                       


					