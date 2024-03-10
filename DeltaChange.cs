using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using ATAS.Indicators.Technical.Properties;
using OFT.Attributes;

namespace ATAS.Indicators.Technical
{
    [DisplayName("Delta Imbalance")]
    [HelpLink("https://github.com/rgutmen")]
    public class DeltaChange : Indicator
    {
        #region Properties
        #region uptrend
        [Display(Name = "Negative to positive", GroupName = "Settings", Description ="Constant to evaluate reversals from downtrend-to-uptrend.", Order = 10)]
        [Range(1, 10000)]
        public int DeltaNegativeToPositive
        {
            get
            {
                return _deltaNegativeToPositive;
            }
            set
            {
                if (_deltaNegativeToPositive != value && value > 0)
                {
                    _deltaNegativeToPositive = value;
                    RecalculateValues();
                }
            }
        }
        private readonly ValueDataSeries _n2p_color = new ValueDataSeries("N2P_color", "UpTrend - Negative to positive")
        {
            Color = Colors.Orange,
            VisualType = VisualMode.UpArrow
        };
        [Display(Name = "Positive to positive", Description = "Constant to evaluate strenght in the uptrend.", GroupName = "Settings", Order = 20)]
        [Range(1, 10000)]
        public int DeltaPositiveToPositive
        {
            get
            {
                return _deltaPositiveToPositive;
            }
            set
            {
                if (_deltaPositiveToPositive != value && value > 0)
                {
                    _deltaPositiveToPositive = value;
                    RecalculateValues();
                }
            }
        }
        private readonly ValueDataSeries _p2p_color = new ValueDataSeries("P2P_color", "UpTrend - Strenght")
        {
            Color = Colors.Green,
            VisualType = VisualMode.UpArrow
        };
        #endregion
        #region downtrend
        [Display(Name = "Positive to negative", GroupName = "Settings", Description = "Constant to evaluate reversals from uptrend-to-downtrend.", Order = 30)]
        [Range(-10000, -1)]
        public int DeltaPositiveToNegative
        {
            get
            {
                return _deltaPositiveToNegative;
            }
            set
            {
                if (_deltaPositiveToNegative != value && value < 0)
                {
                    _deltaPositiveToNegative = value;
                    RecalculateValues();
                }
            }
        }
        private readonly ValueDataSeries _p2n_color = new ValueDataSeries("P2N_color", "DownTrend - Positive to negative")
        {
            Color = Colors.Yellow,
            VisualType = VisualMode.DownArrow
        };

        [Display(Name = "Negative to negative", Description = "Constant to evaluate strenght in the downtrend.", GroupName = "Settings", Order = 40)]
        [Range(-10000, -1)]
        public int DeltaNegativeToNegative
        {
            get
            {
                return _deltaNegativeToNegative;
            }
            set
            {
                if (_deltaNegativeToNegative != value && value < 0)
                {
                    _deltaNegativeToNegative = value;
                    RecalculateValues();
                }
            }
        }
        private readonly ValueDataSeries _n2n_color = new ValueDataSeries("N2N_color", "DownTrend - Strenght")
        {
            Color = Colors.Red,
            VisualType = VisualMode.DownArrow
        };
        
        #endregion
        #endregion

        public DeltaChange() : base(useCandles: true)
        {
            base.DenyToChangePanel = true;
            base.DataSeries[0] = _n2p_color;
            base.DataSeries.Add(_n2n_color);
            base.DataSeries.Add(_p2n_color);
            base.DataSeries.Add(_p2p_color);
        }

        protected override void OnApplyDefaultColors()
        {
            if (base.ChartInfo != null)
            {
                _n2p_color.Color = base.ChartInfo!.ColorsStore.UpCandleColor.Convert();
                _p2p_color.Color = base.ChartInfo!.ColorsStore.UpCandleColor.Convert();
                _p2n_color.Color = base.ChartInfo!.ColorsStore.DownCandleColor.Convert();
                _n2n_color.Color = base.ChartInfo!.ColorsStore.DownCandleColor.Convert(); 
            }
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar >= 2)
            {
                IndicatorCandle candle = GetCandle(bar);
                IndicatorCandle candle2 = GetCandle(bar - 1);
                IndicatorCandle candle3 = GetCandle(bar - 2);
                decimal deltaDiff = candle.Delta - candle2.Delta;

                // Uptrend reversal
                if (candle2.Close - candle2.Open < 0m && candle3.Close - candle3.Open < 0m && candle.Close - candle.Open > 0m && candle.Low <= candle2.Low && candle.Delta > 0m && deltaDiff >= DeltaNegativeToPositive)
                {
                    _n2p_color[bar] = candle.Low - base.InstrumentInfo!.TickSize * 3m;
                    _p2p_color[bar] = 0m;
                }
                // Uptrend strength
                else if (candle3.Low <= candle2.Low && candle2.Low <= candle.Low && deltaDiff >= DeltaPositiveToPositive)
                {
                    _n2p_color[bar] = 0m;
                    _p2p_color[bar] = candle.Low - base.InstrumentInfo!.TickSize * 3m;
                }
                else
                {
                    _n2p_color[bar] = 0m;
                    _p2p_color[bar] = 0m;
                }

                // Downtrend reversal
                if (candle2.Close - candle2.Open > 0m && candle3.Close - candle3.Open > 0m && candle.Close - candle.Open < 0m && candle.High >= candle2.High && candle.Delta < 0m && deltaDiff <= DeltaPositiveToNegative)
                {
                    _p2n_color[bar] = candle.High + base.InstrumentInfo!.TickSize * 3m;
                    _n2n_color[bar] = 0m;
                }
                // Downtrend strength
                else if (candle3.High > candle2.High && candle2.High > candle.High && deltaDiff <= DeltaNegativeToNegative)
                {
                    _p2n_color[bar] = 0m;
                    _n2n_color[bar] = candle.High + base.InstrumentInfo!.TickSize * 3m;
                }
                else
                {
                    _p2n_color[bar] = 0m;
                    _n2n_color[bar] = 0m;
                }
            }
        }
        #region private vars

        // Negative to positive
        private int _deltaNegativeToPositive = 1000;
        
        // Negative to negative
        private int _deltaNegativeToNegative = -300;
 
        // Positive to negative
        private int _deltaPositiveToNegative = -1000;

        // Positive to positive
        private int _deltaPositiveToPositive = 300;

        #endregion

    }
}
