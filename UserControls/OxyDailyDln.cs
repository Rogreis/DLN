using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using TickStyle = OxyPlot.Axes.TickStyle;
using System.Globalization;
using System.Collections.ObjectModel;
using DLN.Classes;

namespace DLN.UserControls
{
    /// <summary>
    /// Show daily DLN data
    /// base on <see href="https://github.com/oxyplot"/>
    /// <seealso href="http://www.oxyplot.org/"/>
    /// <seealso href="http://docs.oxyplot.org/en/latest/"/>
    /// </summary>
    public partial class OxyDailyDln : UserControl
    {

        public OxyDailyDln()
        {
            InitializeComponent();


        }

        private class LuminanceData
        {
            public DateTime Data { get; set; }
            public double Hour { get; set; }
            public double Value { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Open { get; set; }
            public double Close { get; set; }
        }

        private PlotModel plotModelDln(DadosEntrada dados)
        {
            Calculos calc = new Calculos();

            // Create the data collection
            var dataCC = new Collection<LuminanceData>();
            var dataPE = new Collection<LuminanceData>();
            var dataCE = new Collection<LuminanceData>();


            double maxValue = double.MinValue;
            for (double hora = dados.StartTime; hora <= dados.EndTime; hora += 0.2)
            {
                calc.Calcula(dados, hora);
                double value = calc.Resultados.TotalLightCC;
                maxValue = Math.Max(maxValue, value);
                dataCC.Add(new LuminanceData { Hour = hora, Value = value });
                dataPE.Add(new LuminanceData { Hour = hora, Value = calc.Resultados.TotalLightPE });
                dataCE.Add(new LuminanceData { Hour = hora, Value = calc.Resultados.TotalLightCE });
            }
            var lineSeriesCC = new LineSeries()
            {
                DataFieldY = "Value",
                Title = "CC",
                DataFieldX = "Hour",
                ItemsSource = dataCC,
                Smooth = true,
            };
            //string xx= lineSeriesCC.TrackerFormatString;
            //{ 0}
            //{ 1}: { 2}
            //{ 3}: { 4}


            var lineSeriesPE = new LineSeries()
            {
                Title = "PE",
                DataFieldY = "Value",
                DataFieldX = "Hour",
                ItemsSource = dataPE,
                Smooth = true,
            };
            var lineSeriesCE = new LineSeries()
            {
                Title = "CE",
                DataFieldY = "Value",
                DataFieldX = "Hour",
                ItemsSource = dataCE,
                Smooth = true,
            };


            var tmp = new PlotModel { Title = dados.Data.ToShortDateString() };
            tmp.Axes.Add(new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TickStyle = TickStyle.Outside,
                Title = "Day Light",
                Position = AxisPosition.Left,
                Maximum = 200, //maxValue + 10,
                Minimum = 0,
            });

            tmp.Axes.Add(new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TickStyle = TickStyle.Outside,
                Title = "Hour",
                Position = AxisPosition.Bottom,
            });

            tmp.Series.Add(lineSeriesCC);
            tmp.Series.Add(lineSeriesPE);
            tmp.Series.Add(lineSeriesCE);

            return tmp;
        }



        //public PlotModel Example2()
        //{
        //    var tmp = new PlotModel("Test");
        //    tmp.Axes.Add(new LinearAxis(AxisPosition.Left) { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, TickStyle = TickStyle.Outside });
        //    DateTime dt = new DateTime(2010, 1, 1);
        //    tmp.Axes.Add(new DateTimeAxis(dt, dt.AddDays(1), AxisPosition.Bottom, null, null, DateTimeIntervalType.Hours)
        //    {
        //        MajorGridlineStyle = LineStyle.Solid,
        //        Angle = 90,
        //        StringFormat = "HH:mm",
        //        MajorStep = 1.0 / 24 / 2, // 1/24 = 1 hour, 1/24/2 = 30 minutes
        //        IsZoomEnabled = true,
        //        MaximumPadding = 0,
        //        MinimumPadding = 0,
        //        TickStyle = TickStyle.None
        //    });

        //    var ls = new LineSeries("Line1") { DataFieldX = "X", DataFieldY = "Y" };
        //    List<Item> ii = new List<Item>();

        //    for (int i = 0; i < 24; i++)
        //        ii.Add(new Item { X = dt.AddHours(i), Y = i * i });
        //    ls.ItemsSource = ii;
        //    tmp.Series.Add(ls);
        //    return tmp;
        //}


        //// Make a new plotmodel
        //private PlotModel model = new PlotModel();

        //// Create the OxyPlot graph for Salt Split
        //private OxyPlot.Wpf.PlotView plot = new OxyPlot.Wpf.PlotView();

        //// Function to plot data
        //private void plotData(double numWeeks, double startingSS)
        //{
        //    List<LineSeries> listPointAray = new List<LineSeries>();

        //    // Initialize new Salt Split class for acess to data variables
        //    Salt_Split_Builder calcSS = new Salt_Split_Builder();
        //    calcSS.compute(numWeeks, startingSS, maxDegSS);

        //    // Create new Line Series
        //    LineSeries linePoints = new LineSeries()
        //    { StrokeThickness = 1, MarkerSize = 1, Title = numWeeks.ToString() + " weeks" };


        //    // Add each point to the new series
        //    foreach (var point in calcSS.saltSplitCurve)
        //    {
        //        DataPoint XYpoint = new DataPoint();
        //        XYpoint = new DataPoint(point.Key, point.Value * 100);
        //        linePoints.Format("%", XYpoint.Y);
        //        linePoints.Points.Add(XYpoint);
        //    }

        //    listPointAray.Add(linePoints);

        //    // Add Chart Title
        //    model.Title = "Salt Split Degradation";

        //    // Add Each series to the
        //    foreach (var series in listPointAray)
        //    {
        //        // Define X-Axis
        //        OxyPlot.Axes.LinearAxis Xaxis = new OxyPlot.Axes.LinearAxis();
        //        Xaxis.Maximum = numWeeks;
        //        Xaxis.Minimum = 0;
        //        Xaxis.Position = OxyPlot.Axes.AxisPosition.Bottom;
        //        Xaxis.Title = "Number of Weeks";
        //        model.Axes.Add(Xaxis);

        //        //Define Y-Axis
        //        OxyPlot.Axes.LinearAxis Yaxis = new OxyPlot.Axes.LinearAxis();
        //        Yaxis.MajorStep = 15;
        //        Yaxis.Maximum = calcSS.saltSplitCurve.Last().Value * 100;
        //        Yaxis.MaximumPadding = 0;
        //        Yaxis.Minimum = 0;
        //        Yaxis.MinimumPadding = 0;
        //        Yaxis.MinorStep = 5;
        //        Yaxis.Title = "Percent Degradation";
        //        //Yaxis.StringFormat = "{0.00} %";
        //        model.Axes.Add(Yaxis);

        //        model.Series.Add(series);
        //    }


        //    // Add the plot to the window

        //    plot.Model = model;
        //    plot.InvalidatePlot(true);
        //    SaltSplitChartGrid.Children.Clear();
        //    SaltSplitChartGrid.Children.Add(plot);

        //}


        #region Line plot model
        private PlotModel plotModelDln(DadosEntrada dados, DateTime day)
        {
            dados.Data = day;
            return plotModelDln(dados);
        }


        private PlotModel plotModelDln(DadosEntrada dados, int julianDate)
        {
            JulianCalendar julianCalendar = new JulianCalendar();
            DateTime data = new DateTime(DateTime.Now.Year, 1, 1);
            dados.Data = julianCalendar.AddDays(data, julianDate - 1);
            return plotModelDln(dados);
        }



        DadosEntrada _dados = null;
        public void UpdateGraph(DadosEntrada dados, int julianDate)
        {
            if (dados == null)
                return;
            _dados = dados;
            plotDln.Model = plotModelDln(dados, julianDate);
            plotDln.Invalidate();
        }

        public void UpdateGraph(DadosEntrada dados, DateTime day)
        {
            if (dados == null)
                return;
            _dados = dados;
            plotDln.Model = plotModelDln(_dados, day);
            plotDln.Invalidate();
        }

        #endregion

        private void EnviaMensagem(string mesage)
        {

        }

        private PlotModel plotModelCAndleDln(DadosEntrada Dados)
        {
            string mensagemErro = "";
            try
            {
                var candlePlot = new PlotModel { Title = "Candle" };
                DateTime DataInicial = Program.objParametros.UltimaDataInicial;
                DateTime DataFinal = Program.objParametros.UltimaDataFinal;

                // Create two axis
                candlePlot.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = DateTimeAxis.ToDouble(DataInicial), Maximum = DateTimeAxis.ToDouble(DataFinal), StringFormat = "d/M" });
                LinearAxis linearAxis1 = new LinearAxis()
                {
                    Position = AxisPosition.Right,
                    MajorGridlineStyle = LineStyle.Dot,
                    MinorGridlineStyle = LineStyle.Dot,
                    MajorGridlineColor = OxyColor.FromRgb(44, 44, 44),
                    TicklineColor = OxyColor.FromRgb(82, 82, 82)
                };
                candlePlot.Axes.Add(linearAxis1);


                Calculos calc = new Calculos();

                // Create the data collection
                var luminanceData = new Collection<LuminanceData>();


                for (DateTime data = DataInicial; data <= DataFinal; data = data.AddDays(1))
                {
                    Dados.Data = data;
                    EnviaMensagem("Calculating " + data.ToString("dd - MMM"));
                    double maxValue = double.MinValue, minValue = double.MaxValue;
                    LuminanceData luminance = new LuminanceData();
                    for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                    {
                        Dados.horas = hour;
                        calc.Calcula(Dados, hour);
                        maxValue = Math.Max(maxValue, calc.Resultados.TotalLightCC);
                        minValue = Math.Min(minValue, calc.Resultados.TotalLightCE);
                    }
                    luminance.Data = data;
                    luminance.Open = 1.3 * minValue;
                    luminance.Close = 0.7 * maxValue;
                    luminance.High = maxValue;
                    luminance.Low = minValue;
                    luminanceData.Add(luminance);
                }


                //string xx= lineSeriesCC.TrackerFormatString;
                //{ 0}
                //{ 1}: { 2}
                //{ 3}: { 4}

                var candleSerie = new CandleStickSeries()
                {
                    Title = "Day Light",
                    Color = OxyColors.Black,
                    IncreasingColor = OxyColor.FromRgb(0, 197, 49),
                    DecreasingColor = OxyColor.FromRgb(255, 95, 95),
                    DataFieldX = "Data",
                    DataFieldHigh = "High",
                    DataFieldLow = "Low",
                    DataFieldClose = "Close",
                    DataFieldOpen = "Open",
                    TrackerFormatString = "Date: {2}\nOpen: {5:0.00000}\nHigh: {3:0.00000}\nLow: {4:0.00000}\nClose: {6:0.00000}",
                    ItemsSource = luminanceData
                };
                candlePlot.Series.Add(candleSerie);

                return candlePlot;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private int indValue(double value)
        {
            return Convert.ToInt32(value / 20.0);
        }

        private PlotModel plotModelHistogram(DadosEntrada dados)
        {
           
            string mensagemErro = "";
            try
            {
                DeedleAnalysis dataAnalysis = new DeedleAnalysis();
                DateTime startDate = Program.objParametros.UltimaDataInicial;
                DateTime endDate = Program.objParametros.UltimaDataFinal;
                //int maxCount = Convert.ToInt32(endDate.Subtract(startDate).TotalDays) * (Program.objParametros.EndTime - Program.objParametros.StartTime + 1); 

                Calculos calc = new Calculos();
                var luminance = new List<double>();
                List<int> dataCC = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                List<int> dataPE = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                List<int> dataCE = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                for (DateTime data = startDate; data <= endDate; data = data.AddDays(1))
                {
                    dados.Data = data;

                    for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                    {
                        dados.horas = hour;
                        calc.Calcula(dados, hour);
                        dataCC[indValue(calc.Resultados.TotalLightCC)]++;
                        dataPE[indValue(calc.Resultados.TotalLightPE)]++;
                        dataCE[indValue(calc.Resultados.TotalLightCE)]++;
                    }
                }
                int maxCount = Math.Max(Math.Max(dataCC.Max(), dataCE.Max()), dataPE.Max());
                List <ColumnItem> itemsCC = dataCC.Select(x => new ColumnItem(x)).ToList();
                List<ColumnItem> itemsPE = dataPE.Select(x => new ColumnItem(x)).ToList();
                List<ColumnItem> itemsCE = dataCE.Select(x => new ColumnItem(x)).ToList();


                var histogramPlot = new PlotModel { Title = "Hours with same light"};


                //return dataAnalysis.ColumnSeries();

                // Add 2 axis
                var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
                categoryAxis.Labels.Add("20");
                categoryAxis.Labels.Add("40");
                categoryAxis.Labels.Add("60");
                categoryAxis.Labels.Add("80");
                categoryAxis.Labels.Add("100");
                categoryAxis.Labels.Add("120");
                categoryAxis.Labels.Add("140");
                categoryAxis.Labels.Add("160");
                categoryAxis.Labels.Add("180");
                categoryAxis.Labels.Add("200");
                categoryAxis.Labels.Add("220");

                histogramPlot.Axes.Add(categoryAxis);

                histogramPlot.Axes.Add(new LinearAxis()
                {
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Maximum= ((maxCount/100) + 1) * 100,
                    Minimum= 0,
                    TickStyle = TickStyle.Outside,
                    Title = "Qty Hours",
                    Position = AxisPosition.Left,
                });


                var serieCC = new ColumnSeries()
                {
                    Title = "Day Light CC",
                    //TrackerFormatString = "Date: {2}\nOpen: {5:0.00000}\nHigh: {3:0.00000}\nLow: {4:0.00000}\nClose: {6:0.00000}",
                    ItemsSource = itemsCC,
                    //LabelPlacement = LabelPlacement.Inside,
                    //LabelFormatString = "{0} times",
                    //FillColor = OxyColor.FromRgb(44, 44, 44),
                };
                histogramPlot.Series.Add(serieCC);

                var seriePE = new ColumnSeries()
                {
                    Title = "Day Light PE",
                    ItemsSource = itemsPE,
                    LabelPlacement = LabelPlacement.Inside,
                };
                histogramPlot.Series.Add(seriePE);

                var serieCE = new ColumnSeries()
                {
                    Title = "Day Light CE",
                    ItemsSource = itemsCE,
                    LabelPlacement = LabelPlacement.Inside,
                };
                histogramPlot.Series.Add(serieCE);

                return histogramPlot;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Shows a candle series graphics
        /// Based on <see href="https://www.investopedia.com/trading/candlestick-charting-what-is-it/"/>
        /// </summary>
        /// <param name="dados"></param>
        public void UpdateCandleGraph(DadosEntrada dados)
        {
            if (dados == null)
                return;
            _dados = dados;
            plotDln.Model = plotModelHistogram(_dados);
            
            plotDln.Invalidate();
        }


    }



}
