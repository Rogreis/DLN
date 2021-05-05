using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace DLN
{
    static class Program
    {

        public static Parametros objParametros = new Parametros();
        private static string _pathParametros = "";


        public static void Parametros_Deserialize()
        {
            Stream fileStream = null;
            try
            {
                XmlSerializer XMLFormatter = new XmlSerializer(objParametros.GetType());
                fileStream = new FileStream(_pathParametros, FileMode.Open, FileAccess.Read, FileShare.None);

                objParametros = (Parametros)XMLFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                objParametros = new Parametros();
            }
            if (fileStream != null) fileStream.Close();
        }

        public static void Parametros_Serialize()
        {
            XmlSerializer XMLFormatter = new XmlSerializer(objParametros.GetType());
            Stream fileStream = new FileStream(_pathParametros, FileMode.Create, FileAccess.Write, FileShare.None);
            XMLFormatter.Serialize(fileStream, objParametros);
            fileStream.Close();
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _pathParametros = Path.Combine(Application.StartupPath, "GeradorCodigo.ini");
            Parametros_Deserialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            Parametros_Serialize();
        }
    }
}
