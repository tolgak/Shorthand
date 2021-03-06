if object_id('fnIBU_SplitString') is not null
  drop function dbo.fnIBU_SplitString
go
create function dbo.fnIBU_SplitString (@str nvarchar(max), @separator char(1))
returns table
as return (
  
  with tokens(p, a, b) as ( select cast(1 as bigint), cast(1 as bigint), charindex(@separator, @str)
                            union all
                            select p + 1, b + 1, charindex(@separator, @str, b + 1)
                              from tokens
                              where b > 0  )
  
  select p-1 ItemIndex
       , substring(@str, a, case when b > 0 then b-a else len(@str) end) Item
    from tokens
)
go

grant select on dbo.fnIBU_SplitString to AppUser
go
