# Delta Imbalance (DeltaChange)

After the important areas are considered this indicator helps you identify a possible reversal, based on delta.

## Requirements
    
Atas: 

    https://atas.net

Knowledge about:
- Delta -> The most important point for this indicator.
- Orderflow
- The crossing order method
    
## How to install?
1 - Copy the file on:
        
        C:\Users\<user_name>\Documents\Atas\Indicators
    
2 - Then open Atas.

3 - And add "Delta Imbalance" indicator from the list of indicators.
<div style="text-align: center;">
![image](https://github.com/rgutmen/DeltaChange/assets/67153853/e0c0c993-495d-4d4d-9e7d-ad8ce121e814)
</div>

## Functionality
This indicator does the difference between the currentCandle Delta and previousCandle Delta. If the difference is higher or lower than the one that at the settings, it will draw a symbol.
## Configuration and explanation
There are four pairs of settings to configure in this indicator, one numeric under "Settings" section, and other under "Drawing".

- Reversals: Useful for entries.
	- From Downtrend to Uptrend: Assuming that a downtrend matchs with negative deltas, if the current candle has a positive delta, it will means that more buyers are participating than in previous candles, which it could means a possible reversal.
		- Negative to positive: It will draw a symbol if delta is higher than this parameter.
		- UpTrend - Negative to positive: Symbol to be drawn below the candle showing possible reversal.
	- From Uptrend to Downtrend: Assuming that an uptrend matchs with positive deltas, if the current candle has a negative delta, it will means that more sellers are participating than in previous candles, which it could means a possible reversal.
		- Positive to negative: It will draw a symbol if delta is lower than this parameter.
		- Downtrend - Positive to negative: Symbol to be drawn above the candle showing possible reversal.
- Continuity: Useful to extend the Take Profit.
	- 📉 Downtrend: Assuming that a downtrend matchs with negative deltas, if the current candle has a negative delta, it will means that even more sellers are participating than in previous candles, which it could means strenght. 
		- Negative to negative: It will draw a symbol if delta is lower than this parameter.
		- Downtrend - Strenght: Symbol to be drawn above the candle showing downtrend strenght.
	- 📈 Uptrend: Assuming that an uptrend matchs with positive deltas, if the current candle has a positive delta, it will means that even more buyers are participating than in previous candles, which it could means strenght. 
		- Positive to positive: It will draw a symbol if delta is higher than this parameter.
		- Uptrend - Strenght: Symbol to be drawn below the candle showing uptrend strenght.

## What I have learn?
I have learn about:
* C#
* Atas (Codding)
* Trading.

## Improvements
* Ability to determine automatically or based on a percentage the right values to get good settings parameters.
