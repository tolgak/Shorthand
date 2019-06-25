/*
update TAppCardFields set ItemTitle = '[Previous_StudentID_Dikey_Geçiþ/Yeniden_Giriþ]' where ID = 21
update TAppCardFields set ItemTitle = '[New_IBU_ID_IBU_Transfer_Students]' where ID = 24
update TAppCardFields set ItemTitle = '[Can_Add/Drop]' where ID = 239
go
*/

if object_id( 'spFields_BuildSql') is not null
  drop procedure dbo.spFields_BuildSql
go
create procedure dbo.spFields_BuildSql @selected varchar(max) = ''
as begin
  set nocount on
-- exec spFields_BuildSql ''

  declare @selectFields varchar(max) = 'S.StudentID, P.First_Name, P.Last_Name'
  declare @joinClause   varchar(max) = 'JOIN PersonalInfo    P      ON P.PersonID = S.PersonID
  LEFT JOIN StatusCode Status ON Status.StatusCodeID = S.StatusCodeID'

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

  select @selectFields += ', ' + ItemSQL + ' ' + ItemTitle + char(13) + char(10)
    from CardFields CF
    join dbo.fnIBU_SplitString(@selected, ',') S on S.Item = CF.ID
    where ItemSql   is not null 
      and ItemTitle is not null 
      and ItemType <> 'G'
      and ID not in (125, 126)
    order by SortKey


  if charindex('TermType1.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN TerminationType TermType1 ON TermType1.TerminationTypeID = S.TerminationTypeID '
  if charindex('TermType2.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN TerminationType TermType2 ON TermType2.TerminationTypeID = S.TerminationTypeIDUP '
  if charindex('D.', @selectFields) > 0
    select @joinClause += ' JOIN Department D ON  D.DepartmentID = S.DepartmentID'
  if charindex('F.', @selectFields) > 0
    select @joinClause += ' JOIN Faculty    F ON  F.FacultyID    = D.FacultyID '
  if charindex('CI.', @selectFields) > 0
    select @joinClause += ' JOIN ContactInfo CI ON  CI.ContactID = P.ContactID '
  if charindex('CI2.', @selectFields) > 0
    select @joinClause += ' JOIN ContactInfo CI2 ON  CI2.ContactID = P.SecondaryContactID '
  if charindex('CI3.', @selectFields) > 0
    select @joinClause += ' Left JOIN ContactInfo CI3 ON  CI3.ContactID = P.MernisContactID '
  if charindex('HE.', @selectFields) > 0
    select @joinClause += ' JOIN V_HASEXEMPT HE ON  HE.StudentID = S.StudentID '
  if charindex('RegType.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN RegistrationType RegType ON  RegType.RegistrationTypeID = S.RegistrationTypeID '
  if charindex('OSYMPT.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN OSYMPuanTuru OSYMPT ON  OSYMPT.OSYMPuanTuruID = S.OSYMPuanTuruID '
  if charindex('TTUniv.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN University TTUniv ON  TTUniv.ID = S.TransferredToUniversity '
  if charindex('StudentDetail.', @selectFields) > 0
    select @joinClause += ' LEFT JOIN StudentDetail StudentDetail ON  StudentDetail.StudentID = S.StudentID '
  if charindex('PrepTermination.', @selectFields) > 0
    select @joinClause += ' left join StudentPrepTermination PrepTermination on PrepTermination.StudentId = S.StudentID left join DocumentType           DocType         on DocType.Id                = PrepTermination.DocumentTypeId '
  if charindex('ASAL.', @selectFields) > 0
    select @joinClause += ' left join StudentAsalStatus ASAL on ASAL.StudentId = S.StudentID '

  declare @sql varchar(max)
  select @sql = 'select top 100 ' + @selectFields + ' from Student S ' + @joinClause

  select @sql sqlScript

  execute ( @sql )

exit_Proc:

  set nocount off
end
go

grant exec on dbo.spFields_BuildSql to AppUser
go
grant exec on dbo.spFields_BuildSql to web_app
go


