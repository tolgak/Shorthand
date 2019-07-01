if object_id( 'spEquity_SaveOrUpdate') is not null
  drop procedure dbo.spEquity_SaveOrUpdate
go
create procedure dbo.spEquity_SaveOrUpdate
  @Name         varchar(100)
, @Last         decimal(10, 2)
, @Yesterday    decimal(10, 2)
, @Percentage   decimal(10, 2)
, @High         decimal(10, 2)
, @Low          decimal(10, 2)
, @VolumeInLots int
, @VolumeInTL   int
, @DateOfValue  smalldatetime
, @Type         int
as begin
-- 30.06.2019
-- 01.07.2019

  set nocount on

  if exists(select 1 from Equity where Name = @Name and DateOfValue = @DateOfValue and Type = @Type)
    update Equity set Last         = @Last        
                    , Yesterday    = @Yesterday   
                    , Percentage   = @Percentage  
                    , High         = @High        
                    , Low          = @Low         
                    , VolumeInLots = @VolumeInLots
                    , VolumeInTL   = @VolumeInTL
					          , DateOfEntry  = getdate()  
	  where Name = @Name and DateOfValue = @DateOfValue and Type = @Type
  else
    insert into Equity (Name, Last, Yesterday, Percentage, High, Low, VolumeInLots, VolumeInTL, DateOfValue, Type, DateOfEntry)
      values(@Name, @Last, @Yesterday, @Percentage, @High, @Low, @VolumeInLots, @VolumeInTL, @DateOfValue, @Type, getdate())


exit_Proc:

  set nocount off
end
go
