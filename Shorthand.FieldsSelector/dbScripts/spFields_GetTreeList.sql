if object_id( 'spFields_GetTreeList') is not null
  drop procedure dbo.spFields_GetTreeList
go
create procedure dbo.spFields_GetTreeList
as begin
  set nocount on

--declare @cardName varchar(100) = 'Department'
--declare @cardName varchar(100) = 'Course'
--declare @cardName varchar(100) = 'Instructor'
declare @cardName varchar(100) = 'Student'

;with CardFields as ( select ID, ParentID, OrderCode, CardName, ItemType, GroupComplete, ItemName, ItemTitle, ItemSQL, AppCardFieldID
                           , formatmessage('%d', ID) SortKey
                           , 0 Indent
                        from TAppCardFields
                        where CardName = @cardName and ParentID is null
union all
  select e.ID, e.ParentID, e.OrderCode, e.CardName, e.ItemType, e.GroupComplete, e.ItemName, e.ItemTitle, e.ItemSQL, e.AppCardFieldID
       , formatmessage('%s.%d', F.SortKey, e.ID) SortKey
       , 1 + F.Indent
    from TAppCardFields e    
    join CardFields F ON F.ID = e.ParentID and f.CardName = e.CardName
)

select ID
     , ParentID
     , OrderCode
     , CardName
     , ItemType
     , GroupComplete
     , ItemName
     , ItemTitle
     , ItemSQL
     , AppCardFieldID
  from CardFields
  order by SortKey

exit_Proc:

  set nocount off
end
go

grant exec on dbo.spFields_GetTreeList to AppUser
go
grant exec on dbo.spFields_GetTreeList to web_app
go

