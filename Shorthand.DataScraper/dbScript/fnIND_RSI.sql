if object_id('fnIND_RSI') is not null
  drop function dbo.fnIND_RSI
go
create function dbo.fnIND_RSI(@name varchar(200))
returns @result table ( name varchar(200)
                      , n int
                      , close_price float
                      , gain        float
                      , avgGain     float
                      , avgLoss     float
                      , rs          as avgGain / avgLoss
                      , rsi         as 100 * avgGain / (avgGain + avgLoss)
                      , index IX_Result clustered (name, n))
as begin

-- select * from dbo.fnIND_RSI('goog')

  insert into @result (name, n, close_price, gain)
    select T.Name
         , row_number() over(order by T.Name, T.quote_date)
         , T.close_price
         , close_price - lag(close_price) over(order by name, quote_date)
    from dbo.StockQuote T

  update T set avgGain = A.aG, avgLoss = A.aL
    from @result T
    join ( select name
                 , n
                 , avg( iif(gain >= 0,   gain, 0) ) over (partition by name order by name, n rows between 13 preceding and current row) aG
                 , avg( iif(gain <  0, - gain, 0) ) over (partition by name order by name, n rows between 13 preceding and current row) aL
            from @result ) A on A.Name = T.Name and A.n = T.n
    where T.n >= 14

exit_func:
   return
end
go

