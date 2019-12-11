if object_id('fnIND_MACD') is not null
  drop function dbo.fnIND_MACD
go
create function dbo.fnIND_MACD(@name varchar(200))
returns @data table ( name        varchar(200)
                    , n           int
                    , quote_date  smalldatetime
                    , close_price float
                    , ema12       float
                    , ema26       float
                    , macd        as ema12 - ema26
                    , index IX_Result clustered (name, n) )
as begin

-- https://www.sqlservercentral.com/articles/calculating-moving-averages-with-t-sql
-- https://www.mssqltips.com/sqlservertip/5541/using-two-samples-to-validate-macd-with-tsql
-- https://www.investopedia.com/ask/answers/122414/what-moving-average-convergence-divergence-macd-formula-and-how-it-calculated.asp

-- v1.0 [tolga 9.12.2019 11:57:21]
-- select * from dbo.fnIND_MACD('GOOG')

  declare @ema_1_intervals int = 12
        , @ema_2_intervals int = 26

  declare @K1 decimal(4,3) = 2 / (1 + @ema_1_intervals + .000)
  declare @K2 decimal(4,3) = 2 / (1 + @ema_2_intervals + .000)

  declare @prev_ema_1    float
        , @prev_ema_2    float
        , @initial_sma_1 float
        , @initial_sma_2 float

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
    from @data t1
    option (maxdop 1)

exit_func:
   return
end
go 




