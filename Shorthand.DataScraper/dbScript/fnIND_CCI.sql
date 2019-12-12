if object_id('fnIND_CCI') is not null
  drop function dbo.fnIND_CCI
go
create function dbo.fnIND_CCI(@name varchar(200))
returns @result table ( name varchar(200)
                      , n int
                      , quote_date  smalldatetime
                      , close_price   float
                      , typical_price float
                      , ma_20         float
                      , deviation     float
                      , cci           float
                      , index IX_Result clustered (name, n))
as begin
-- v1.0 [tolga 11.12.2019 15:50:21] Commodity Channel Index (CCI)
-- select * from dbo.fnIND_CCI('GOOG')

; with _base      as ( select Q.*
                            , row_number()       over (order by Q.name, Q.quote_date) n
                            , (Q.close_price + Q.high_price + Q.low_price) / 3 typical_price
                            , avg(Q.close_price) over (order by Q.name, Q.quote_date
                                                       rows between 19 preceding and current row) ma_20
                         from StockQuote Q
                         where Q.name = @name )

  insert into @result ( name
                      , n
                      , quote_date
                      , close_price
                      , typical_price
                      , ma_20
                      , Deviation
                      , cci )
    select name
         , n
         , quote_date
         , close_price
         , typical_price
         , ma_20
         , typical_price- ma_20 Deviation
         , case when n < 20 then null
                else avg ( abs(typical_price - ma_20) ) over (order by name, quote_date rows between 19 preceding and current row)
           end cci
     from _base

exit_func:
   return
end
go 


