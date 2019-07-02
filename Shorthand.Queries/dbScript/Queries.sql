if not exists (select * from sys.objects where object_id = object_id(N'dbo.Query') and type in (N'U')) BEGIN
  create table dbo.Query(
    Id       int           identity(1,1) not null
  , ParentId int    
  , Name     varchar(100)  not null
  , SqlText  varchar(3000) null
  , constraint PK_Query primary key clustered (Id asc) )
END
GO

if not exists (select * from sys.objects where object_id = object_id(N'dbo.QueryParam') and type in (N'U')) BEGIN
  create table dbo.QueryParam(
    Id            int identity(1,1) not null
  , QueryId       int               not null
  , Name          varchar(100)      not null
  , SqlDbType     int               not null
  , LookupSqlText varchar(1000)
  , DefaultValue  varchar(100)
  , constraint PK_QueryParam primary key clustered (Id asc))
END
GO

if not exists (select * from sys.foreign_keys where object_id = object_id(N'dbo.FK_Query_Query') and parent_object_id = object_id(N'dbo.Query'))
  alter table dbo.Query with check add constraint FK_Query_Query 
  foreign key(ParentId) references dbo.Query(Id)
GO

if exists (select * from sys.foreign_keys where object_id = object_id(N'dbo.FK_Query_Query') and parent_object_id = object_id(N'dbo.Query'))
  alter table dbo.Query check constraint FK_Query_Query
GO

if not exists (select * from sys.foreign_keys where object_id = object_id(N'dbo.FK_QueryParam_Query') and parent_object_id = object_id(N'dbo.QueryParam'))
  alter table dbo.QueryParam with check add  constraint FK_QueryParam_Query
  foreign KEY(QueryId) REFERENCES dbo.Query (Id)
GO

if  exists (select * from sys.foreign_keys where object_id = object_id(N'dbo.FK_QueryParam_Query') and parent_object_id = object_id(N'dbo.QueryParam'))
  alter table dbo.QueryParam check constraint FK_QueryParam_Query
GO
