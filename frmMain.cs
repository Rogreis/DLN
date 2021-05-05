using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using DLN.Classes;
using LatLongNet;
using CoordinateSharp;

namespace DLN
{
    /// <summary>
    /// Coordininate from CoordinateSharp, <see href="https://coordinatesharp.com/DeveloperGuide"/>
    /// </summary>
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            tabControlMain.TabPages.Remove(tabOxyGraph);
            tabControlMain.TabPages.Remove(tabPageData); 
            tabControlMain.TabPages.Remove(tabPageTypicalDay);
            tabControlMain.TabPages.Remove(tabPageGraphicCommands);
        }


        #region Calculation

        private string GetDataValue(string strValue, ref double value)
        {
            value = double.MinValue;
            double.TryParse(strValue, out value);
            if (value == double.MinValue)
            {
                value = 0;
                return "0";
            }
            return strValue;
        }

        private DadosEntrada DataEntry()
        {

            if (string.IsNullOrEmpty(textBoxLocal.Text))
            {
                MessageBox.Show("Please, informe a local name");
                return null;
            }



            //Program.objParametros.UltimaDataInicial = dateTimePickerInicial.Value;
            //Program.objParametros.UltimaDataFinal = dateTimePickerFinal.Value;

            //JulianCalendar myCal = new JulianCalendar();

            //for (double dia = Program.objParametros.UltimaDataInicial.ToOADate(); dia < Program.objParametros.UltimaDataFinal.ToOADate(); dia++)
            //{
            //    DateTime d = DateTime.FromOADate(dia);
            //    Debug.WriteLine(d.ToString("dd/MM/yyyy") + "   " + myCal.GetDayOfYear(d).ToString());
            //}



            //DateTime dataIniciaoAno = new DateTime(Program.objParametros.UltimaDataInicial.Year, 1, 1);
            //int juliano = Convert.ToInt32(Math.Floor(Program.objParametros.UltimaDataInicial.ToOADate() - dataIniciaoAno.ToOADate())) + 1;



            Coordinate c = new Coordinate();


            double latitude, longitude;
            DadosEntrada dados = new DadosEntrada("");
            try
            {
                latitude = Convert.ToDouble(numericUpDownLatitude.Value);
                longitude = Convert.ToDouble(numericUpDownLongitude.Value);
                dados.CoordenadasLocal = new Coordinate(latitude, longitude);
                int factor = Convert.ToInt32(Math.Abs(longitude) / 15);
                if ((double)(factor * 15) - Math.Abs(longitude) > 7.5)
                    factor += 1;
                dados.Meridian = Convert.ToInt32(Math.Sign(longitude) * (double)(factor * 15));
            }
            catch
            {
                MessageBox.Show("Please, informe valid latitude and longitude");
                return null;
            }

            dados.Data = new DateTime(DateTime.Now.Year, 1, 1);
            dados.Nome = textBoxLocal.Text;


            dados.Azimuth = Convert.ToDouble(numericUpDownAzimuth.Value);
            dados.PointPAngle = Convert.ToDouble(numericUpDownPointPAngle.Value);
            dados.PointPAzimuth = Convert.ToDouble(numericUpDownPointPAzimuth.Value);
            dados.horas = 8;
            dados.StartTime = Convert.ToInt32(numericUpDownStartHour.Value);
            dados.EndTime = Convert.ToInt32(numericUpDownEndHour.Value);

            Program.objParametros.LatitudeCenterMap = dados.CoordenadasLocal.Latitude.ToDouble();
            Program.objParametros.LongitudeCenterMap = dados.CoordenadasLocal.Longitude.ToDouble();
            Program.objParametros.Azimuth = dados.Azimuth;
            Program.objParametros.PointPAngle = dados.PointPAngle;
            Program.objParametros.PointPAzimuth = dados.PointPAzimuth;
            Program.objParametros.StartTime= dados.StartTime;
            Program.objParametros.EndTime= dados.EndTime;
            

            lblData.Text = dados.ToString(); ;
            return dados;
        }

        private void ShowCalculatedValues()
        {

            DadosEntrada dados = DataEntry();
            if (dados != null)
            {
                Calculos calc = new Calculos();


                DataSet dsDln = new DataSet();
                DataTable tabCalculos = new DataTable();
                tabCalculos.Columns.Add("Hora", typeof(double));
                tabCalculos.Columns.Add("IluminanciaCC_H", typeof(double));
                tabCalculos.Columns.Add("IluminanciaPE_H", typeof(double));
                tabCalculos.Columns.Add("IluminanciaCE_H", typeof(double));
                tabCalculos.Columns.Add("IluminanciaCC_V", typeof(double));
                tabCalculos.Columns.Add("IluminanciaPE_V", typeof(double));
                tabCalculos.Columns.Add("IluminanciaCE_V", typeof(double));
                tabCalculos.Columns.Add("LuminanciaCC", typeof(double));
                tabCalculos.Columns.Add("LuminanciaPE", typeof(double));
                tabCalculos.Columns.Add("LuminanciaCE", typeof(double));


                for (int hora = 6; hora < 19; hora++)
                {
                    calc.Calcula(dados, hora);
                    DataRow linha = tabCalculos.NewRow();
                    linha["Hora"] = hora;

                    linha["IluminanciaCC_H"] = calc.Resultados.IluminanciaCC_H;
                    linha["IluminanciaPE_H"] = calc.Resultados.IluminanciaPE_H;
                    linha["IluminanciaCE_H"] = calc.Resultados.IluminanciaCE_H;

                    linha["IluminanciaCC_V"] = calc.Resultados.IluminanciaCC_V;
                    linha["IluminanciaPE_V"] = calc.Resultados.IluminanciaPE_V;
                    linha["IluminanciaCE_V"] = calc.Resultados.IluminanciaCE_V;

                    linha["LuminanciaCC"] = calc.Resultados.LuminanciaCC;
                    linha["LuminanciaPE"] = calc.Resultados.LuminanciaPE;
                    linha["LuminanciaCE"] = calc.Resultados.LuminanciaCE;
                    tabCalculos.Rows.Add(linha);
                }
                dataGridViewDln.DataSource = tabCalculos;
            }
        }


        private void DayCalculation()
        {

        }

        #endregion



        private void Form1_Load(object sender, EventArgs e)
        {
            ucGoogleMapCoordenadas.PontoMapa += UcGoogleMapCoordenadas_PontoMapa;
            ucGoogleMapCoordenadas.Inicia();
            hScrollBarDailyLigth.Value = 1;

            textBoxLocal.Text = Program.objParametros.LastLocal;
            numericUpDownLatitude.Value = Convert.ToDecimal(Program.objParametros.LatitudeCenterMap);
            numericUpDownLongitude.Value = Convert.ToDecimal(Program.objParametros.LongitudeCenterMap);
            numericUpDownAzimuth.Value = Convert.ToDecimal(Program.objParametros.Azimuth);
            numericUpDownPointPAngle.Value = Convert.ToDecimal(Program.objParametros.PointPAngle);
            numericUpDownPointPAzimuth.Value = Convert.ToDecimal(Program.objParametros.PointPAzimuth);

            numericUpDownStartHour.Value = Convert.ToDecimal(Program.objParametros.StartTime);
            numericUpDownEndHour.Value = Convert.ToDecimal(Program.objParametros.EndTime);

            
            


            //oxyDailyDln1.UpdateGraph(dados, hScrollBarDay.Value);

            ////webBrowser1.ScriptErrorsSuppressed = true;
            //string url = "file:///" + Application.StartupPath + "/latitude.htm";
            //url = url.Replace("\\", "/");
            //// file:///C:/Trabalho/TFS/Freelancer2014/DLN/DLN_WinForms/bin/Debug/latitude.htm
            ////webBrowser1.Url = new Uri(url);
            //webBrowser1.Url = new Uri("file:///C:/Trabalho/TFS/Freelancer2014/DLN/DLN_WinForms/bin/Debug/latitude.htm");
            ////webBrowser1.Navigate(url);


        }

        private void UcGoogleMapCoordenadas_PontoMapa(double latitude, double longitude)
        {
            numericUpDownLatitude.Value = Convert.ToDecimal(latitude);
            numericUpDownLongitude.Value = Convert.ToDecimal(longitude);
       }

        private void btCalculaDiaTipico_Click(object sender, EventArgs e)
        {
        }

        void ExibeMensagem(string mensagem)
        {
            toolStripStatusLabelMensagens.Text = mensagem;
            Application.DoEvents();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabOxyGraph)
            {

            }
            if (tabControlMain.SelectedTab == tabPageData)
            {

            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucGoogleMapCoordenadas.ObtemCentroMapa();
        }

        private void HScrollBarDay_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void LinkLabelLatiLong_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.latlong.net/");
        }



        #region Find city

        private void FindLocal()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxLocal.Text))
                    return;
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                LatLongNetResult res = LatLongNet.LatLongNetClient.Search(textBoxLocal.Text);
                CoordinatePart lat = new CoordinatePart(Convert.ToDouble(res.Latitude, CultureInfo.GetCultureInfo("en-Us").NumberFormat), CoordinateType.Lat);
                CoordinatePart longi = new CoordinatePart(Convert.ToDouble(res.Longitude, CultureInfo.GetCultureInfo("en-Us").NumberFormat), CoordinateType.Long);
                numericUpDownLatitude.Value = Convert.ToDecimal(lat.ToDouble());
                numericUpDownLongitude.Value = Convert.ToDecimal(longi.ToDouble());
                ucGoogleMapCoordenadas.SetPosition(lat.ToDouble(), longi.ToDouble());
                Cursor = Cursors.Default;
                Program.objParametros.LastLocal = textBoxLocal.Text;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtFind_Click(object sender, EventArgs e)
        {
            FindLocal();
        }
        private void TextBoxLocal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindLocal();
            }
        }

        #endregion

        private void BtDLN_Click(object sender, EventArgs e)
        {
            if (!tabControlMain.TabPages.Contains(tabOxyGraph))
                tabControlMain.TabPages.Add(tabOxyGraph);
            if (!tabControlMain.TabPages.Contains(tabPageData))
                tabControlMain.TabPages.Add(tabPageData);
            if (!tabControlMain.TabPages.Contains(tabPageTypicalDay))
                tabControlMain.TabPages.Add(tabPageTypicalDay);
            if (!tabControlMain.TabPages.Contains(tabPageGraphicCommands))
                tabControlMain.TabPages.Add(tabPageGraphicCommands);
            

            oxyDailyDln1.UpdateGraph(DataEntry(), hScrollBarDailyLigth.Value);
            typicalDay1.EntryData = DataEntry();
            ShowCalculatedValues();
            tabControlMain.SelectedTab = tabOxyGraph;
            Application.DoEvents();
        }

        bool alreadyActivated = false;
        private void FrmMain_Activated(object sender, EventArgs e)
        {
            if (alreadyActivated)
                return;
            //FindLocal();
            Cursor = Cursors.WaitCursor;


            double latitude = Convert.ToDouble(numericUpDownLatitude.Value);
            double longitude = Convert.ToDouble(numericUpDownLongitude.Value);
            ucGoogleMapCoordenadas.SetPosition(latitude, longitude);
            Cursor = Cursors.Default;
            Application.DoEvents();
            alreadyActivated = true;
        }

        private void hScrollBarDailyLigth_Scroll(object sender, ScrollEventArgs e)
        {
            oxyDailyDln1.UpdateGraph(DataEntry(), hScrollBarDailyLigth.Value);
            Application.DoEvents();
        }
    }
}
