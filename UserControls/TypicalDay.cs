using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace DLN.UserControls
{
    public partial class TypicalDay : UserControl
    {
        public event dlMensagem Mensagem = null;

        private void SendMessage(string mensagem)
        {
            Mensagem?.Invoke(mensagem);
        }


        public TypicalDay()
        {
            InitializeComponent();
        }

        public DadosEntrada EntryData { set; get; }

 
        private void OxyDailyDlnTypicalDay_Load(object sender, EventArgs e)
        {

        }

        private void TypicalDay_Load(object sender, EventArgs e)
        {
            if (Program.objParametros.UltimaDataInicial == DateTime.MinValue)
                Program.objParametros.UltimaDataInicial = DateTime.Now.AddYears(-1);
            dateTimePickerInicial.Value = Program.objParametros.UltimaDataInicial;
            if (Program.objParametros.UltimaDataFinal == DateTime.MinValue)
                Program.objParametros.UltimaDataFinal = DateTime.Now;
            dateTimePickerFinal.Value = Program.objParametros.UltimaDataFinal;
        }

        private void BtCalculaDiaTipico_Click(object sender, EventArgs e)
        {
            string mensagemErro = "";
            DadosEntrada dados = EntryData;

            dados.Data = dateTimePickerInicial.Value;
            dados.horas = 8;

            Program.objParametros.UltimaDataInicial = dateTimePickerInicial.Value;
            Program.objParametros.UltimaDataFinal = dateTimePickerFinal.Value;

            CalculaPeriodo calcDia = new CalculaPeriodo(dados, Program.objParametros.UltimaDataInicial, Program.objParametros.UltimaDataFinal);
            calcDia.Mensagem += new dlMensagem(SendMessage);

            if (!calcDia.Calcula(ref mensagemErro))
            {
                MessageBox.Show(mensagemErro);
                return;
            }
            oxyDailyDlnTypicalDay.UpdateGraph(dados, calcDia.TypicalDay);
        }


        private void BtTestNewGraph_Click(object sender, EventArgs e)
        {
            DadosEntrada dados = EntryData;

            dados.Data = dateTimePickerInicial.Value;
            dados.horas = 8;

            Program.objParametros.UltimaDataInicial = dateTimePickerInicial.Value;
            Program.objParametros.UltimaDataFinal = dateTimePickerFinal.Value;

            oxyDailyDlnTypicalDay.UpdateCandleGraph(dados);
        }
    }
}
