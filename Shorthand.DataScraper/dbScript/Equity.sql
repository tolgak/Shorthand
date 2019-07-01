if object_id('Equity') is not null
  drop table dbo.Equity
go
create table Equity (
  Id           int identity(1,1)
, Name         varchar(100) not null
, Last         decimal(10, 2)
, Yesterday    decimal(10, 2)
, Percentage   decimal(10, 2)
, High         decimal(10, 2)
, Low          decimal(10, 2)
, VolumeInLots int
, VolumeInTL   int
, DateOfValue  smalldatetime
, DateOfEntry  smalldatetime
, constraint PK_EquityId primary key clustered(Id)
)
go
create nonclustered index [Equity_Name_DateOfValue] ON [dbo].[Equity](Name asc, DateOfValue asc)
go