if not exists(select 1 from sys.objects where name like 'Environment')
  create table dbo.Environment (
	  Id int IDENTITY(1,1) NOT NULL,
	  Name varchar(250) NOT NULL,
	  ServerName varchar(250) NOT NULL,
	  DatabaseName varchar(250) NOT NULL,
	  Value varchar(1000) NULL,
    CONSTRAINT PK_ConnectionString PRIMARY KEY CLUSTERED (Id ASC) 
  )
go

truncate table dbo.Environment
go
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'sis_production', N'pandora', N'ibu')
GO
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'els_production', N'pandora', N'els')
GO
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'sis_test', N'pandoradev', N'ibu_test')
GO
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'els_test', N'pandoradev', N'els_test')
GO
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'sis_development', N'pandoradev', N'ibu_back')
GO
INSERT [dbo].[Environment] ([Name], [ServerName], [DatabaseName]) VALUES (N'els_development', N'pandoradev', N'els_back')
GO
