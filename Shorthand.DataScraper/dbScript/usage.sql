select s.*
     --, R.*, M.*
     , R.n, R.quote_date, R.close_price
     , case when R.rsi > 70 then 1 else 0 end overbought
     , case when R.rsi < 30 then 1 else 0 end oversold
     , R.rsi, M.macd, C.cci
from Stock S
cross apply dbo.fnIND_RSI(S.name)  R
cross apply dbo.fnIND_MACD(S.name) M
cross apply dbo.fnIND_CCI(S.Name)  C
where R.n = M.n
  and R.n = C.n
order by S.name, R.quote_date