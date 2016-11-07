if exists(select 1 from sys.objects where name like 'spAdminPanel_Ping')
  drop procedure dbo.spAdminPanel_Ping
go
create procedure dbo.spAdminPanel_Ping
as begin
  set nocount on
   
  select ConnectionProperty('client_net_address')              as client_net_address
       , SYSTEM_USER                                           as connected_user  
       , ConnectionProperty('local_net_address')               as server_net_address       
       , cast(ConnectionProperty('local_tcp_port') as varchar) as server_tcp_port
       --, replace(@@version, char(10), char(13) + char(10))     as dbVersion

exit_Proc:

  set nocount off
end
go

grant exec on dbo.spAdminPanel_Ping to sis_admin
go


