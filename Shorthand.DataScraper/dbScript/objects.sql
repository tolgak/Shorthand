create table Stock (
    id   int identity(1,1)
  , name varchar(20)
  , constraint PK_StockId primary key clustered(id)
)
go

create table dbo.StockQuote(
  id          int identity(1,1)
, name        varchar(200)      not null
, quote_date  smalldatetime     not null
, open_price  float 
, close_price float             not null
, high_price  float 
, low_price   float 
, volume      float 
, dateOfEntry smalldatetime     not null

, constraint PK_StockQuote primary key clustered (id asc) )
go


-- 30.06.2019
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
-- 01.07.2019
alter table Equity add Type int
go