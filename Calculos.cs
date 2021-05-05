// -----------------------------------------------------------------------
// <copyright file="Calculos.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DLN
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;
    using CoordinateSharp;

    /// <summary>
    /// Elenca os dados de entrada para um cálculo DLN
    /// </summary>
    public class DadosEntrada
    {
        public string Nome;

        public DateTime Data;

        public Coordinate CoordenadasLocal { get; set; }

        public double Azimuth = 0;
        public double PointPAngle = 0;
        public double PointPAzimuth = 0;
        public double Meridian = 0;
        public int horas;

        /// <summary>
        /// First day time used in calculus
        /// </summary>
        public int StartTime { get; set; } = 5;

        /// <summary>
        /// First day time used in calculus
        /// </summary>
        public int EndTime { get; set; } = 19;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="Dia"></param>
        /// <param name="Mes"></param>
        /// <param name="Hora"></param>
        public DadosEntrada(string nome)
        {
            Nome = nome;
        }

        public void SetAzimuth(int degrees, int minutes, int second)
        {
            Azimuth = ((double)degrees + (double)minutes / 60.0 + (double)second / 3600.0) * (Math.PI / 180);
        }
        public void SetPintPAngle(int degrees, int minutes, int second)
        {
            PointPAngle = ((double)degrees + (double)minutes / 60.0 + (double)second / 3600.0) * (Math.PI / 180);
        }

        public override string ToString()
        {
            return CoordenadasLocal.ToString() + " Meridian: " + Meridian.ToString() + " Azimuth: " + Azimuth.ToString() + " Point P Angle: " + PointPAngle.ToString() + " Point P Azimuth: " + PointPAzimuth.ToString();
        }

    };

    public class Resultados
    {
        public int Hora;


        public double
            IluminanciaCE_H,  // Iluminância total horizontal céu escuro
            IluminanciaPE_H,  // Iluminância total horizontal céu parcialmente nublado
            IluminanciaCC_H,  // Iluminância total horizontal céu claro
            IluminanciaCE_V,
            IluminanciaPE_V,
            IluminanciaCC_V,
            LuminanciaCC,    // luminancia do ponto P
            LuminanciaPE,
            LuminanciaCE;

        public double TotalLightCC
        {
            get
            {
                return LuminanciaCC + IluminanciaCC_V + IluminanciaCC_H;
            }
        }
        public double TotalLightPE
        {
            get
            {
                return LuminanciaPE + IluminanciaPE_V + IluminanciaPE_H;
            }
        }

        public double TotalLightCE
        {
            get
            {
                return LuminanciaCE + IluminanciaCE_V + IluminanciaCE_H;
            }
        }


        public Resultados()
        {
            IluminanciaCE_H = 0;
            IluminanciaPE_H = 0;
            IluminanciaCC_H = 0;
            IluminanciaCE_V = 0;
            IluminanciaPE_V = 0;
            IluminanciaCC_V = 0;
            LuminanciaCC = 0;
            LuminanciaPE = 0;
            LuminanciaCE = 0;
        }

        public static Resultados operator +(Resultados r1, Resultados r2)
        {
            r1.IluminanciaCE_H += r2.IluminanciaCE_H;
            r1.IluminanciaPE_H += r2.IluminanciaPE_H;
            r1.IluminanciaCC_H += r2.IluminanciaCC_H;
            r1.IluminanciaCE_V += r2.IluminanciaCE_V;
            r1.IluminanciaPE_V += r2.IluminanciaPE_V;
            r1.IluminanciaCC_V += r2.IluminanciaCC_V;
            r1.LuminanciaCC += r2.LuminanciaCC;
            r1.LuminanciaPE += r2.LuminanciaPE;
            r1.LuminanciaCE += r2.LuminanciaCE;
            return r1;
        }

        public void Adiciona(Resultados r2)
        {
            Hora = r2.Hora;
            IluminanciaCE_H += r2.IluminanciaCE_H;
            IluminanciaPE_H += r2.IluminanciaPE_H;
            IluminanciaCC_H += r2.IluminanciaCC_H;
            IluminanciaCE_V += r2.IluminanciaCE_V;
            IluminanciaPE_V += r2.IluminanciaPE_V;
            IluminanciaCC_V += r2.IluminanciaCC_V;
            LuminanciaCC += r2.LuminanciaCC;
            LuminanciaPE += r2.LuminanciaPE;
            LuminanciaCE += r2.LuminanciaCE;
        }


        public void CalculaMedia(int _totalDias)
        {
            IluminanciaCE_H /= _totalDias;
            IluminanciaPE_H /= _totalDias;
            IluminanciaCC_H /= _totalDias;
            IluminanciaCE_V /= _totalDias;
            IluminanciaPE_V /= _totalDias;
            IluminanciaCC_V /= _totalDias;
            LuminanciaCC /= _totalDias;
            LuminanciaPE /= _totalDias;
            LuminanciaCE /= _totalDias;
        }


    }


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Calculos
    {
        private const double PI180 = Math.PI / 180;
        private const double CIS = 127.5;
        private JulianCalendar _julianCalendar = new JulianCalendar();


        // Dados de entrada
        private double
            J,     // DataJuliana
            HP,    // HoraPadrao,         /* hora decimal  */
            LAT,   // Latitude          em radianos
            LONG,  // Longitude         "
            AE,    // AzimuteElevacao   "
            MP,    // MeridianoPadrao   "
            PZ,    // Angulo zenital do ponto P em radianos
            PA;    // Azimute do ponto P em radianos

        // Dados calculados
        private double
            ET,     // equacao do tempo
            T,      // Tempo solar
            D,      // declinacao solar
            AT,     // altitude solar
            AS,     // azimute solar
            AZ,     // azimute solar da elevacao
            AI,     // angulo de incidencia
            AP,     // angulo de perfil
            ISE,    // iluminacia solar extraterrestre
            M,      // massa de ar otico
            Z,      // angulo entre sol e zenite
            PS,     // angulo entre o sol e o ponto P no ceu
            // CC ceu claro CE ceu encoberto PE aprcialmente encoberto

            ISDNCC, // iluminancia solar direta normal
            ISDNPE,

            ISDHCC, // iluminancia solar direta horizontal
            ISDHPE,

            ISDVCC, // iluminancia solar direta vertical
            ISDVPE,

            ICHCC,  // iluminancia do ceu horizontal
            ICHPE,
            ICHCE,

            ICVCC,  // iluminancia do ceu vertical
            ICVPE,
            ICVCE,


            IMCHCC, // iluminancia proveniente do meio ceu horizontal
            IMCHPE,
            IMCHCE,

            LZCC,   // luminancia do zenite
            LZPE,
            LZCE;

        public Resultados Resultados = null;


        // Fatores para luminancia no zenite (ceu claro)
        double[] FatLumCC = {3.248, 2.591, 2.086, 1.698, 1.398,
		      1.165, 0.985, 0.844, 0.734, 0.648,
		      0.581, 0.530, 0.491, 0.464, 0.446,
		      0.436, 0.435, 0.440, 0.452};

        // Fatores para luminancia no zenite (ceu parcialmente nublado)
        double[] FatLumPE = {2.002, 1.782, 1.595, 1.434, 1.296,
		      1.178, 1.076, 0.990, 0.916, 0.854,
		      0.802, 0.758, 0.723, 0.694, 0.672,
		      0.656, 0.644, 0.636, 0.632};


        public Calculos()
        {

        }




        // Seta valores
        public void SetaValores(DadosEntrada dados, double hora)
        {
            LAT= dados.CoordenadasLocal.Latitude.ToRadians();
            LONG = dados.CoordenadasLocal.Longitude.ToRadians();
            //LAT = (dados.CoordenadasLocal.Latitude.Degrees +
            //          dados.CoordenadasLocal.Latitude.Minutes / 60.0 +
            //            dados.CoordenadasLocal.Latitude.Seconds / 3600.0) * PI180;
            //LONG = (dados.CoordenadasLocal.Longitude.Degrees +
            //          dados.CoordenadasLocal.Longitude.Minutes / 60.0 +
            //            dados.CoordenadasLocal.Longitude.Seconds / 3600.0) * PI180;
            AE = dados.Azimuth;
            MP = dados.Meridian;
            PZ = dados.PointPAngle;
            PA = dados.PointPAzimuth;
            J = (double)_julianCalendar.GetDayOfYear(dados.Data);
            // Hora padrao em decimal (minutos e segundos ignorados)
            HP = hora;
            Resultados = new Resultados();
        }


        // Executa os calculos basicos
        public void Calcula(DadosEntrada dados, double hora)
        {
            SetaValores(dados, hora);

            // (07) Equacao do tempo
            ET = 0.170 * Math.Sin(4.0 * Math.PI / 373.0 * (J - 80.0)) - 0.129 * Math.Sin(2.0 * Math.PI / 355.0 * (J - 8.0));

            // (08) Tempo Solar
            T = HP + ET + ((12.0 * (MP - LONG)) / Math.PI);

            // (09) declinacao solar
            D = 0.4093 * Math.Sin((2.0 * Math.PI / 368.0) * (J - 81.0));

            // (10) altitude solar
            AT = Math.Asin(Math.Sin(LAT) * Math.Sin(D) - Math.Cos(LAT) * Math.Cos(D) * Math.Cos(Math.PI * T / 12.0));

            // Correcao feita 12/5/93 Rogerio
            // AT nao pode estar fora do intervalo acima, pois indica
            // sol abaixo da linha do horizonte
            //if ((AT > Math.PI) || (AT < 0.0))
            //    AT = 0.0;

            // (11) Azimute solar ???????????????????????????
            AS = Math.Atan2(-(Math.Cos(D) * Math.Sin(Math.PI * T / 12.0)), -((Math.Cos(LAT) * Math.Sin(D) + Math.Sin(LAT) * Math.Cos(D) * Math.Cos(Math.PI * T / 12.0))));

            // (12) Azimute de elevacao solar
            AZ = AS - AE;

            // (13) Angulo de incidencia
            AI = Math.Acos(Math.Cos(AT) * Math.Cos(AZ));

            // (14) Angulo de perfil
            // Correcao feita 27/7/91 Paulo e Rogerio
            if ((AI > Math.PI / 2.0))
            {
                AI = Math.PI / 2.0;
                AP = 0.0;
            }
            else AP = Math.Atan(Math.Sin(AS) / Math.Cos(AI));

            // (15) Iluminancia solar extraterrestre
            ISE = CIS * (1.0 + 0.034 * Math.Cos(2 * Math.PI * (J - 2) / 365.0));

            // (16) Massa de ar otico
            // Coreccao feita em 12/5/93 por Rogerio
            if (AT == 0.0)
                M = 1.0;
            else M = 1.0 / Math.Sin(AT);

            // (17) Angulo entre o sol e o zenite
            Z = Math.PI / 2.0 - AT;

            // (18) Angulo entre o sol e ponto P no ceu
            PS = Math.Acos(Math.Cos(Z) * Math.Cos(PZ));

            // (19) Iluminancia solar direta normal (ISDN)
            ISDNCC = ISE * Math.Exp(-0.21 * M);
            ISDNPE = ISE * Math.Exp(-0.80 * M);

            // (20) Iluminancia solar direta horizontal (ISDH)
            ISDHCC = ISDNCC * Math.Sin(AT);
            ISDHPE = ISDNPE * Math.Sin(AT);

            // (21) Iluminancia solar direta vertical (ISDV)
            ISDVCC = ISDNCC * Math.Cos(AI);
            ISDVPE = ISDNPE * Math.Cos(AI);

            Iluminancias();

            Luminancias();
        }

        private void Iluminancias()
        {

            // (22) Iluminancia do ceu horizontal

            ICHCC = 0.8 + 15.5 * Math.Sqrt(Math.Abs(Math.Sin(AT))); //??? Math.Abs e' correcao minha
            ICHPE = 0.3 + 45.0 * Math.Sin(AT);
            ICHCE = 0.3 + 21.0 * Math.Sin(AT);

            // (23) Iluminancia do ceu vertical
            ICVCC = (4.0 * Math.Pow(AT, 1.3)
                + (12.0 * Math.Pow(Math.Sin(AT), 0.3) *
                Math.Pow(Math.Cos(AT), 1.3))) *
                ((2.0 + Math.Cos(AZ)) /
                  (3.0 - Math.Cos(AZ)));
            ICVPE = 12.0 * AT +
                (30.2 * Math.Pow(Math.Sin(AT), 0.8) *
                     Math.Cos(AT)) *
                ((1.0 + Math.Cos(AZ)) /
                  (3.0 - Math.Cos(AZ)));
            ICVCE = 8.5 * Math.Sin(AT);

            // (24) Iluminancia proveniente do meio ceu horizontal
            IMCHCC = 8.2 * Math.Pow(Math.Sin(AT), 0.5) +
                  6.9 * Math.Sin(AT) * Math.Cos(AT) * Math.Cos(AZ);
            IMCHPE = 22.7 * Math.Sin(AT) + 14.1 * Math.Pow(Math.Sin(AT), 1.3) * Math.Cos(AT) * Math.Cos(AZ);
            IMCHCE = 10.7 * Math.Sin(AT);

            // (27) iluminancias totais
            Resultados.Hora = Convert.ToInt32(HP);
            Resultados.IluminanciaCC_H = ISDHCC + ICHCC;
            Resultados.IluminanciaCC_V = ISDVCC + ICVCC;

            Resultados.IluminanciaPE_H = ISDHPE + ICHPE;
            Resultados.IluminanciaPE_V = ISDVPE + ICVPE;

            Resultados.IluminanciaCE_H = ICHCE;
            Resultados.IluminanciaCE_V = ICVCE;
        }

        private void Luminancias()
        {
            // (25) Luminancia do zenite
            /*
               A luminancia no zenite e' a iluminancia do ceu multiplicada
               por um fator relativo `a altitude
            */
            int ind = 18 - (((int)LAT) / 5);

            LZCC = ICHCC * FatLumCC[ind];
            LZPE = ICHPE * FatLumPE[ind];
            LZCE = ICHCE * 1.286;

            // (26) Luminancia do ponto P (L)

            Resultados.LuminanciaCC = LZCC * (((0.91 * 10.0 * Math.Exp(-3 * PS) + 0.45 * Math.Cos(PS) * Math.Cos(PS)) * (1.0 - Math.Exp(-0.32 / Math.Cos(PZ)))) /
                  ((0.91 + 10.0 * Math.Exp(-3 * Z) + 0.45 * Math.Cos(Z) * Math.Cos(Z)) * (1.0 - Math.Exp(-0.32)))

                    );

            Resultados.LuminanciaPE = LZPE * (((0.526 * 5.0 * Math.Exp(-1.5 * PS)) * (1.0 - Math.Exp(-0.80 / Math.Cos(PZ)))) /
                  ((0.526 + 5.0 * Math.Exp(-1.5 * Z)) * (1.0 - Math.Exp(-0.80)))

                        );

            Resultados.LuminanciaCE = LZCE * (0.864 * (Math.Exp(-0.52 / Math.Cos(PZ))) / (Math.Exp(-0.52)) +
                          0.136 * (1.0 - Math.Exp(-0.52 / Math.Cos(PZ))) / (1.0 - Math.Exp(-0.52))
                        );

            // 26.3.1 e uma formula redundante (valor ja calculado acima)
        }



    }
}
