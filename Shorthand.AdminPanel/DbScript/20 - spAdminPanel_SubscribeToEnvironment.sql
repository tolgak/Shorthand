if exists(select 1 from sys.objects where name like 'spAdminPanel_Subscribe')
  drop procedure dbo.spAdminPanel_Subscribe
go
create procedure dbo.spAdminPanel_Subscribe @macAddress varchar(250), @encryptedCredential varchar(250) --@sisUserName varchar(250), @sisPassword varchar(250)
as begin
  set nocount on
   
  

exit_Proc:

  set nocount off
end
go

grant exec on dbo.spAdminPanel_Subscribe to sis_admin
go

