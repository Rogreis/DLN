// -----------------------------------------------------------------------
// <copyright file="CalculaPeriodo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DLN
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public delegate void dlMensagem(string mensagem);


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CalculaPeriodo
    {
        public event dlMensagem Mensagem = null;
        public DadosEntrada Dados { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        /// <summary>
        /// Calculated typical day light for the given period
        /// </summary>
        public DateTime TypicalDay { get; private set; }

        private List<Resultados> _listaResultados = new List<Resultados>();
        private int _totalDiasCalculados = 0;

        public CalculaPeriodo(DadosEntrada dados, DateTime dataInicial, DateTime dataFinal)
        {
            Dados = dados;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
          }

        private void EnviaMensagem(string mensagem)
        {
            if (Mensagem != null)
                Mensagem(mensagem);
        }

        public bool Calcula(ref string mensagemErro)
        {
            try
            {
                if (DataInicial > DataFinal)
                {
                    mensagemErro = "Final date before start.";
                    return false;
                }
                int maxDay= 365;
                if ((DateTime.IsLeapYear(DataInicial.Year) && DataInicial.Month <= 2) ||
                     (DateTime.IsLeapYear(DataFinal.Year) && DataFinal.Month >= 3))
                    maxDay = 366;
                if ((DataFinal.Subtract(DataInicial)).TotalDays > maxDay)
                {
                    mensagemErro = "Maximum one year for typical day calculation.";
                    return false;
                }

                Calculos calc = new Calculos();
                _totalDiasCalculados = 0;
                _listaResultados = new List<Resultados>();
                for (DateTime data = DataInicial; data <= DataFinal; data= data.AddDays(1))
                {
                    Dados.Data = data;
                    EnviaMensagem("Calculating " + data.ToString("dd - MMM"));
                    for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                    {
                        Dados.horas = hour;
                        calc.Calcula(Dados, hour);
                        _listaResultados.Add(calc.Resultados);
                    }
                    _totalDiasCalculados++;
                }

                // Calculate each time average in the period
                List<Resultados> averageResult = new List<Resultados>();
                for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                {
                    List<Resultados> hourResult = _listaResultados.FindAll(delegate (Resultados r) { return r.Hora == hour; });
                    Resultados resultAverage = new Resultados();
                    foreach (Resultados result in hourResult)
                        resultAverage.Adiciona(result);
                    resultAverage.CalculaMedia(_totalDiasCalculados);
                    averageResult.Add(resultAverage);
                }

                // Calculate moda: real day close to average result
                double smallDtandardDeviation = double.MaxValue;
                for (DateTime data = DataInicial; data <= DataFinal; data = data.AddDays(1))
                {
                    Dados.Data = data;
                    EnviaMensagem("Recalculating " + data.ToString("dd - MMM"));
                    double varianceCC = 0, variancePE = 0, varianceCE = 0;
                    for (int hour = Program.objParametros.StartTime; hour <= Program.objParametros.EndTime; hour++)
                    {
                        Dados.horas = hour;
                        calc.Calcula(Dados, hour);
                        Resultados hourResult = averageResult.Find(delegate (Resultados r) { return r.Hora == hour; });

                        double Aux = calc.Resultados.TotalLightCC - hourResult.TotalLightCC;
                        varianceCC += (Aux * Aux);

                        Aux = calc.Resultados.TotalLightPE - hourResult.TotalLightPE;
                        variancePE += (Aux * Aux);

                        Aux = calc.Resultados.TotalLightCE - hourResult.TotalLightCE;
                        varianceCE += (Aux * Aux);
                    }
                    double standardDeviationCC = Math.Sqrt(varianceCC);
                    double standardDeviationPE = Math.Sqrt(variancePE);
                    double standardDeviationCE = Math.Sqrt(varianceCE);

                    // Using only standard CC deviation to choose typical day
                    if (standardDeviationCC < smallDtandardDeviation)
                    {
                        smallDtandardDeviation = standardDeviationCC;
                        TypicalDay = data;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                TypicalDay = DateTime.MinValue;
                mensagemErro = "Erro em CalculaPeriodo.Calcula: " + ex.Message;
                return false;
            }
        }



    }
}
