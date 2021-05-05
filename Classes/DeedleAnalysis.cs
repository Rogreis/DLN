using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DLN.Classes
{
    /// <summary>
    /// Implements series analysis using Deedle
    /// <see href="http://bluemountaincapital.github.io/Deedle/"/>
    /// <seealso href="https://bluemountaincapital.github.io/Deedle/csharpintro.html"/>
    /// </summary>
    public class DeedleAnalysis
    {

        public List<int> CreatePeriodHistrogram(DadosEntrada Dados, DateTime startDate, DateTime endDate)
        {
            Calculos calc = new Calculos();
            var luminance = new List<double>();

            for (DateTime data = startDate; data <= endDate; data = data.AddDays(1))
            {
                Dados.Data = data;

                for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                {
                    Dados.horas = hour;
                    calc.Calcula(Dados, hour);
                    luminance.Add(calc.Resultados.TotalLightCC);
                    luminance.Add(calc.Resultados.TotalLightPE);
                    luminance.Add(calc.Resultados.TotalLightCE);
                }
            }

            var qtyHour = new List<int>();
            //Func<string, bool> longBooks = delegate (double light) { return book.Length > 5; };
            for (double lumen = 0; lumen <= 200; lumen += 20.0)
            {
                qtyHour.Add(luminance.Count(k => k > lumen && k <= (lumen + 20.0)));
            }
            return qtyHour;
        }

        public PlotModel ColumnSeries()
        {
            var model = new PlotModel
            {
                Title = "ColumnSeries",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var s1 = new ColumnSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new ColumnItem { Value = 25 });
            s1.Items.Add(new ColumnItem { Value = 137 });
            s1.Items.Add(new ColumnItem { Value = 18 });
            s1.Items.Add(new ColumnItem { Value = 40 });

            var s2 = new ColumnSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new ColumnItem { Value = 12 });
            s2.Items.Add(new ColumnItem { Value = 14 });
            s2.Items.Add(new ColumnItem { Value = 120 });
            s2.Items.Add(new ColumnItem { Value = 26 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return model;
        }


    } // end class DeedleAnalysis
}