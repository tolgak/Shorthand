if object_id('fnIND_MACD') is not null
  drop function dbo.fnIND_MACD
go

create function dbo.fnIND_MACD(@name varchar(200))
returns @data table ( n           int
                    , name        varchar(200)
                    , quote_date  smalldatetime
                    , close_price decimal(6,2)
                    , ema12       decimal(6,2)
                    , ema26       decimal(6,2)
                    , macd  as ema12 - ema26 )
as begin

-- https://www.sqlservercentral.com/articles/calculating-moving-averages-with-t-sql
-- https://www.mssqltips.com/sqlservertip/5541/using-two-samples-to-validate-macd-with-tsql
-- https://www.investopedia.com/ask/answers/122414/what-moving-average-convergence-divergence-macd-formula-and-how-it-calculated.asp

-- select * from dbo.fnIND_MACD('GOOG')

  declare @ema_1_intervals int = 12
        , @ema_2_intervals int = 26

  declare @K1 decimal(4,3) = 2 / (1 + @ema_1_intervals + .000)
  declare @K2 decimal(4,3) = 2 / (1 + @ema_2_intervals + .000)

  declare @prev_ema_1    decimal(8,2)
        , @prev_ema_2    decimal(8,2)
        , @initial_sma_1 decimal(8,2)
        , @initial_sma_2 decimal(8,2)

  declare @anchor int

  insert into @data (n, name, quote_date, close_price)
    select row_number() over (order by quote_date) n
         , name
         , quote_date
         , close_price
      from StockQuote
      where name = @name
      order by quote_date

  select @initial_sma_1 = avg(case when n < @ema_1_intervals then close_price else null end)
       , @initial_sma_2 = avg(case when n < @ema_2_intervals then close_price else null end)
    from @data
    where n < @ema_1_intervals
       or n < @ema_2_intervals

-- Carry over update statement
  update t1 set @prev_ema_1 = case when n < @ema_1_intervals then null
                                   when n = @ema_1_intervals then t1.close_price * @K1 + @initial_sma_1 * (1 - @K1)
                                   when n > @ema_1_intervals then t1.close_price * @K1 + @prev_ema_1    * (1 - @K1)
                              end
              , @prev_ema_2 = case when n < @ema_2_intervals then null
                                   when n = @ema_2_intervals then t1.close_price * @K2 + @initial_sma_2 * (1 - @K2)
                                   when n > @ema_2_intervals then t1.close_price * @K2 + @prev_ema_2    * (1 - @K2)
                              end
              , ema12       = @prev_ema_1
              , ema26       = @prev_ema_2
              , @anchor     = n --anchor so that carryover works properly
    from @data t1 --with (TABLOCKX)
    option (maxdop 1)

exit_func:
   return
end
go 


--insert into StockQuote (
-- name
--, quote_date
--, open_price
--, close_price
--, high_price
--, low_price
--, dateOfEntry )
--select 'GOOG', *, getdate() from google_stock





