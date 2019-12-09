

--insert into StockQuote (
-- name
--, quote_date
--, open_price
--, close_price
--, high_price
--, low_price
--, dateOfEntry )
--select 'GOOG', *, getdate() from google_stock




--Create our historical data table
create table google_stock
(
quote_date [datetime],
open_price [decimal](6,2),
close_price [decimal](6,2),
high_price [decimal](6,2),
low_price [decimal](6,2)
)

INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091026', 555.75, 554.21, 561.64, 550.89) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091027', 550.97, 548.29, 554.56, 544.16) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091028', 547.87, 540.30, 550.00, 538.25) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091029', 543.01, 551.05, 551.83, 541.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091030', 550.00, 536.12, 550.17, 534.24) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091102', 537.08, 533.99, 539.46, 528.24) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091103', 530.01, 537.29, 537.50, 528.30) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091104', 540.80, 540.33, 545.50, 536.42) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091105', 543.49, 548.65, 549.77, 542.66) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091106', 547.72, 551.10, 551.78, 545.50) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091109', 555.45, 562.51, 562.58, 554.23) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091110', 562.73, 566.76, 568.78, 562.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091111', 570.48, 570.56, 573.50, 565.86) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091112', 569.56, 567.85, 572.90, 565.50) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091113', 569.29, 572.05, 572.51, 566.61) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091116', 575.00, 576.28, 576.99, 572.78) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091117', 574.87, 577.49, 577.50, 573.72) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091118', 576.65, 576.65, 578.78, 572.07) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091119', 573.77, 572.99, 574.00, 570.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091120', 569.50, 569.96, 571.60, 569.40) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091123', 576.49, 582.35, 586.60, 575.86) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091124', 582.52, 583.09, 584.29, 576.54) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091125', 586.41, 585.74, 587.06, 582.69) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091127', 572.00, 579.76, 582.46, 570.97) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091130', 580.63, 583.00, 583.67, 577.11) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091201', 588.13, 589.87, 591.22, 583.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091202', 591.00, 587.51, 593.01, 586.22) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091203', 589.04, 585.74, 591.45, 585.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091204', 593.02, 585.01, 594.83, 579.18) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091207', 584.21, 586.25, 588.69, 581.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091208', 583.50, 587.05, 590.66, 582.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091209', 587.50, 589.02, 589.33, 583.58) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091210', 590.44, 591.50, 594.71, 590.41) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091211', 594.68, 590.51, 594.75, 587.73) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091214', 595.35, 595.73, 597.31, 592.61) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091215', 593.30, 593.14, 596.38, 590.99) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091216', 598.60, 597.76, 600.37, 596.64) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091217', 596.44, 593.94, 597.64, 593.76) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091218', 596.03, 596.42, 598.93, 595.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091221', 597.61, 598.68, 599.84, 595.67) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091222', 601.34, 601.12, 601.50, 598.85) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091223', 603.50, 611.68, 612.87, 602.85) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091224', 612.93, 618.48, 619.52, 612.27) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091228', 621.66, 622.87, 625.99, 618.48) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091229', 624.74, 619.40, 624.84, 618.29) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091230', 618.50, 622.73, 622.73, 618.01) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20091231', 624.75, 619.98, 625.40, 619.98) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100104', 626.95, 626.75, 629.51, 624.24) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100105', 627.18, 623.99, 627.84, 621.54) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100106', 625.86, 608.26, 625.86, 606.36) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100107', 609.40, 594.10, 610.00, 592.65) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100108', 592.00, 602.02, 603.25, 589.11) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100111', 604.46, 601.11, 604.46, 594.04) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100112', 597.65, 590.48, 598.16, 588.00) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100113', 576.49, 587.09, 588.38, 573.90) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100114', 583.90, 589.85, 594.20, 582.81) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100115', 593.34, 580.00, 593.56, 578.04) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100119', 581.20, 587.62, 590.42, 576.29) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100120', 585.98, 580.41, 585.98, 575.29) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100121', 583.44, 582.98, 586.82, 572.25) 
INSERT INTO google_stock (quote_date, open_price, close_price, high_price, low_price) VALUES ('20100122', 564.50, 550.01, 570.60, 534.86)
CREATE CLUSTERED INDEX ix_goog on google_stock(quote_date)