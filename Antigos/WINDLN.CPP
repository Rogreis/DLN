/****************************************************************

 MODULO......: DLN

 DESCRIÇÃO...: Cálculo da disponibilidade de luz natural

 DATA........: março/ 1993

 AUTOR.......: Rogério Reis da Silva

 OBS.........: definir DEBUG_DLN para se gerar um programa isolado
               (não DLL para objct vision)


 *****************************************************************/


#define WIN31


#include <windows.h>

#include "math.h"
#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include "wdln.hpp"

// Arquivo usado para saida de resultados (rotinas para OV)
FILE *Arq= NULL;

#ifndef DEBUG_DLN

#pragma argsused
// Ponto de entrada da DLL
int FAR PASCAL LibMain( HINSTANCE hInstance, WORD wDataSegment,
                                   WORD wHeapSize, LPSTR lpszCmdLine )
 {
  return 1;
 }

// Ponto de saída da DLL
#pragma argsused
int FAR PASCAL WEP ( int bSystemExit )
{
    return 1;
}

#endif




//
// = = = = = = = = Rotinas para setar dados = = = = = = = = = = = = = = = =
// ************************************************************************


// seta valores, menos data e hora do calculo
void DLN::SetaValores(LPSTR Tipo, int Graus, int Minutos, int Segundos)
 {

  char TipoAux[30];
  strncpy(TipoAux, Tipo, 29);
  TipoAux[29]= 0;
  strupr(TipoAux);

  if (!strcmp(TipoAux, "LATITUDE"))
    LAT=((double)Graus
         + (double)Minutos/60.0
         + (double)Segundos/3600.0 + (double)Segundos/3600.0)  * PI180;

  if (!strcmp(TipoAux, "LONGITUDE"))
    LONGI=((double)Graus
          + (double)Minutos/60.0
          + (double)Segundos/3600.0) * PI180;

  if (!strcmp(TipoAux, "AZIMUTE"))
   {
    AE= ( (double)Graus
        + (double)Minutos/60.0
	+ (double)Segundos/3600.0)  * PI180;
    // (Rogério) Compensa a diferenca para hemisferio sul (22/1/94)
    AE= AE + pi;
   }

  if (!strcmp(TipoAux, "MERIDIANO"))
    MP=((double)Graus
        + (double)Minutos/60.0
        + (double)Segundos/3600.0)  * PI180;

  if (!strcmp(TipoAux, "ANGULOP"))  // Zeta
    Zeta=((double)Graus
        + (double)Minutos/60.0
        + (double)Segundos/3600.0) * PI180;

  if (!strcmp(TipoAux, "AZIMUTEP")) // Alfa
    Alfa=((double)Graus
        + (double)Minutos/60.0
        + (double)Segundos/3600.0) * PI180;

 };

// seta data informada em modo normal mais hora
void DLN::SetaDataHora(int Dia, int Mes, int Hora, int _Legal)
 {

  // Transforma a data em data juliana
  int DiasMes[] = {31,28,31,30,31,30,31,31,30,31,30,31};
  int TotalDias= Dia;
  for (int i= 0; i < (Mes-1); i++) TotalDias+= DiasMes[i];
  J= (double)TotalDias;
  Legal= _Legal;

  // Hora padrao em decimal (minutos e segundos ignorados)
  HP= (double)Hora;

 };

// seta data informada em valor juliano e hora
void DLN::SetaDataHora(int DataJuliana, int Hora)
 {

  J= (double)DataJuliana;

  // Hora padrao em decimal (minutos e segundos ignorados)
  HP= (double)Hora;

 };

// Executa os cálculos básicos necessários
void DLN::Calculos(void)
 {
  if (Legal)
    {
     // (07) Equacao do tempo
     ET= 0.170L * sin(4.0L*pi/373.0L * (J - 80.0L)) -
         0.129L * sin(2.0L*pi/355.0L *(J - 8.0L));

     // (08) Tempo Solar
     T= HP + ET + ((12.0L*(MP - LONGI)) / pi);
    }
  else T= HP;

  // (09) declinacao solar
  D= 0.4093L * sin( (2.0L*pi/368.0L) * (J - 81.0L));

  // (10) altitude solar
  AT= asin( sin(LAT)*sin(D) - cos(LAT)*cos(D) * cos(pi*T/12.0L) );
  // Correção feita 12/5/93 Rogério
  // AT não pode estar fora do intervalo acima, pois indica
  // sol abaixo da linha do horizonte
  if ( (AT > pi) || (AT < 0.0)  ) AT= 0.0;

  // (11) Azimute solar
  AS= atan2( -(cos(D) * sin(pi*T/12.0L)),
             -((cos(LAT)*sin(D) + sin(LAT)*cos(D)*cos(pi*T/12.0L))));

  // (12) Azimute de elevacao solar
  AZ= AS - AE;

  // (13) Angulo de incidencia
  AI= acos( cos(AT) * cos(AZ) );

  // (14) Angulo de perfil
  // Correcao feita 27/7/91 Paulo e Rogerio
  if ( (AI > pi/2.0L)  )
       {
        AI= pi/2.0L;
        AP= 0.0l;
       }
   else AP= atan( sin(AS) / cos(AI) );

  // (15) Iluminancia solar extraterrestre
  ISE= CIS * ( 1.0L + 0.034L * cos(2*pi*(J-2)/365.0L));

  // (16) Massa de ar otico
  // Coreção feita em 12/5/93 por Rogerio
  if ( AT == 0.0  )
         {
          M= 0.0L;
          // (19) Iluminancia solar direta normal (ISDN)
          ISDNCC= 0.0;
          ISDNPE= 0.0;
         }
     else
         {
          M= 1.0L / sin(AT);
          // (19) Iluminancia solar direta normal (ISDN)
          ISDNCC= ISE * exp(-0.21 * M);
          ISDNPE= ISE * exp(-0.80 * M);
         }

  // (17) Angulo entre o sol e o zenite
  Z= pi/2.0L - AT;

  // (18) Angulo entre o sol e ponto P no ceu
  PS= acos( cos(Z) * cos(Zeta) - sin(Z) * sin(Zeta) * cos(Alfa) );


 }


void DLN::Iluminancias(void)
 {

  // (20) Iluminancia solar direta horizontal (ISDH)
  ISDHCC= ISDNCC * sin(AT);
  ISDHPE= ISDNPE * sin(AT);

  // (21) Iluminancia solar direta vertical (ISDV)
  ISDVCC= ISDNCC * cos(AI);
  ISDVPE= ISDNPE * cos(AI);


  // (22) Iluminancia do ceu horizontal
  ICHCC= 0.8L + 15.5L * sqrt(fabs(sin(AT))); //??? fabs e' correcao minha
  ICHPE= 0.3L + 45.0L * sin(AT);
  ICHCE= 0.3L + 21.0L * sin(AT);

  // (23) Iluminancia do ceu vertical
  ICVCC= (4.0L * pow(AT, 1.3L)
          + (12.0L * pow(sin(AT), 0.3L) *
          pow(cos(AT), 1.3L) ) )                   *
          ( (2.0L + cos(AZ)) /
            (3.0L - cos(AZ)) );
  ICVPE= 12.0L * AT +
          (30.2L * pow(sin(AT), 0.8L) *
                   cos(AT)              ) *
          ( (1.0L + cos(AZ))           /
            (3.0L - cos(AZ)));
  ICVCE= 8.5L * sin(AT);

  // (24) Iluminancia proveniente do meio ceu horizontal
  IMCHCC= 8.2L * pow(sin(AT), 0.5L) +
            6.9L * sin(AT) * cos(AT) * cos(AZ);
  IMCHPE= 22.7L * sin(AT) + 14.1L*pow(sin(AT), 1.3L) * cos(AT) * cos(AZ);
  IMCHCE= 10.7L * sin(AT);

  // (27) iluminancias totais
  ITHCC= ISDHCC + ICHCC;
  ITVCC= ISDVCC + ICVCC;

  ITHPE= ISDHPE + ICHPE;
  ITVPE= ISDVPE + ICVPE;

  ITHCE= ICHCE;
  ITVCE= ICVCE;

 } // fim de Iluminancias()



// Fatores para luminancia no zenite (ceu claro)
double FatLumCC[]= {3.248, 2.591, 2.086, 1.698, 1.398,
                    1.165, 0.985, 0.844, 0.734, 0.648,
                    0.581, 0.530, 0.491, 0.464, 0.446,
                    0.436, 0.435, 0.440, 0.452};

// Fatores para luminancia no zenite (ceu parcialmente nublado)
double FatLumPE[]= {2.002, 1.782, 1.595, 1.434, 1.296,
                    1.178, 1.076, 0.990, 0.916, 0.854,
                    0.802, 0.758, 0.723, 0.694, 0.672,
                    0.656, 0.644, 0.636, 0.632};

void DLN::Luminancias(void)
 {
  // (25) Luminancia do zenite
  /*
     A luminancia no zenite e' a iluminancia do ceu multiplicada
     por um fator relativo `a altitude
  */
  int ind= 18 - ( int(LAT) / 5);

  LZCC= ICHCC * FatLumCC[ind];
  LZPE= ICHPE * FatLumPE[ind];
  LZCE= ICHCE * 1.286;

  // (26) Luminancia do ponto P (L)

  LCC= LZCC * ( ( (0.91+10.0*exp(-3*PS) + 0.45*cos(PS)*cos(PS)) * (1.0-exp(-0.32/cos(Zeta))) ) /
                ( (0.91+10.0*exp(-3*Z)  + 0.45*cos(Z)*cos(Z)  ) * (1.0-exp(-0.32))         )

              );

  LPE= LZPE * ( ( (0.526+5.0*exp(-1.5*PS)) * (1.0-exp(-0.80/cos(Zeta))) ) /
                ( (0.526+5.0*exp(-1.5*Z)) * (1.0-exp(-0.80))          )

              );

  LCE= LZCE * ( 0.864 * (exp(-0.52/cos(Zeta))) / (exp(-0.52))   +
                0.136 * (1.0 - exp(-0.52/cos(Zeta))) / (1.0 - exp(-0.52))
              );

  // 26.3.1 é uma fórmula redundante (valor ja calculado acima)

 } // fim de Luminancias()







//
// Rotinas para comunicação de dados
// *********************************
//

DLN Calc;

extern "C"
 {

  // Funções para setar dados
  // ************************
  void FAR PASCAL _export seta_latitude(int Graus, int Minutos, int Segundos);
  void FAR PASCAL _export seta_longitude(int Graus, int Minutos, int Segundos);
  void FAR PASCAL _export seta_azimute(int Graus, int Minutos, int Segundos);
  void FAR PASCAL _export seta_meridiano(int Graus, int Minutos, int Segundos);


  void FAR PASCAL _export seta_latitude(int Graus, int Minutos, int Segundos)
   {
    Calc.LAT=((double)Graus + (double)Minutos/60.0 + (double)Segundos/3600.0 )
                          * PI180;
   }

  void FAR PASCAL _export seta_longitude(int Graus, int Minutos, int Segundos)
   {
    Calc.LONGI=((double)Graus
          + (double)Minutos/60.0
          + (double)Segundos/3600.0) * PI180;

   }

  // Azimute
  void FAR PASCAL _export seta_azimute(int Graus, int Minutos, int Segundos)
   {
    Calc.AE= ( (double)Graus
             + (double)Minutos/60.0
 	          + (double)Segundos/3600.0)  * PI180;
    // (Rogério) Compensa a diferenca para hemisferio sul (22/1/94)
    Calc.AE= Calc.AE + pi;
   }

  // Meridiano padrão
  void FAR PASCAL _export seta_meridiano(int Graus, int Minutos, int Segundos)
   {
    Calc.MP=((double)Graus
            + (double)Minutos/60.0
            + (double)Segundos/3600.0)  * PI180;
   }


  // seta valores, menos data e hora do calculo
  int FAR PASCAL _export seta_valores(LPSTR Tipo, int Graus, int Minutos, int Segundos);
  int FAR PASCAL _export seta_valores(LPSTR Tipo, int Graus, int Minutos, int Segundos)
   {
    Calc.SetaValores(Tipo, Graus, Minutos, Segundos);
    return(0);
   };

  // seta data informada em modo normal mais hora
  int FAR PASCAL _export SETADTHORA(int Dia, int Mes, int Hora, int Legal);
  int FAR PASCAL _export SETADTHORA(int Dia, int Mes, int Hora, int Legal)
   {
    Calc.SetaDataHora(Dia, Mes, Hora, Legal);
    return 0;
   };

  // seta data informada em valor juliano e hora
  int FAR PASCAL _export setadtjulianaHora(int DataJuliana, int Hora);
  int FAR PASCAL _export setadtjulianaHora(int DataJuliana, int Hora)
   {
    Calc.SetaDataHora(DataJuliana, Hora);
    return 0;
   };

typedef double far * LPDOUBLE;

  // Funções para recuperar dados
  void FAR PASCAL _export Horizontal(LPDOUBLE LuzCeuCC,
                                     LPDOUBLE LuzCeuPE,
                                     LPDOUBLE LuzCeuCE,
                                     LPDOUBLE LuzSolCC,
                                     LPDOUBLE LuzSolPE);

  void FAR PASCAL _export Horizontal(LPDOUBLE LuzCeuCC,
                                     LPDOUBLE LuzCeuPE,
                                     LPDOUBLE LuzCeuCE,
                                     LPDOUBLE LuzSolCC,
                                     LPDOUBLE LuzSolPE)
   {
    *LuzCeuCC= Calc.ICHCC;
    *LuzCeuPE= Calc.ICHPE;
    *LuzCeuCE= Calc.ICHCE;

    *LuzSolCC= Calc.ISDHCC;
    *LuzSolPE= Calc.ISDHPE;

   }

  void FAR PASCAL _export Vertical(LPDOUBLE LuzCeuCC,
                                   LPDOUBLE LuzCeuPE,
                                   LPDOUBLE LuzCeuCE,
                                   LPDOUBLE LuzSolCC,
                                   LPDOUBLE LuzSolPE);
  void FAR PASCAL _export Vertical(LPDOUBLE LuzCeuCC,
                                   LPDOUBLE LuzCeuPE,
                                   LPDOUBLE LuzCeuCE,
                                   LPDOUBLE LuzSolCC,
                                   LPDOUBLE LuzSolPE)
   {

    *LuzCeuCC= Calc.ICVCC;
    *LuzCeuPE= Calc.ICVPE;
    *LuzCeuCE= Calc.ICVCE;

    *LuzSolCC= Calc.ISDVCC;
    *LuzSolPE= Calc.ISDVPE;
   }



  void FAR PASCAL _export SETA_ANGP(int Graus, int Minutos, int Segundos);
  void FAR PASCAL _export SETA_ANGP(int Graus, int Minutos, int Segundos)
   {
    Calc.Zeta=((double)Graus
              + (double)Minutos/60.0
              + (double)Segundos/3600.0) * PI180;
   }

  void FAR PASCAL _export SETA_AZIMUTEP(int Graus, int Minutos, int Segundos);
  void FAR PASCAL _export SETA_AZIMUTEP(int Graus, int Minutos, int Segundos)
   {
    Calc.Alfa=((double)Graus
              + (double)Minutos/60.0
              + (double)Segundos/3600.0) * PI180;
   }



  void FAR PASCAL _export LUMINANCIA(LPDOUBLE LumLZCC,
                                     LPDOUBLE LumLZPE,
                                     LPDOUBLE LumLZCE,
                                     LPDOUBLE LumLCC,
                                     LPDOUBLE LumLPE,
                                     LPDOUBLE LumLCE);
  void FAR PASCAL _export LUMINANCIA(LPDOUBLE LumLZCC,
                                     LPDOUBLE LumLZPE,
                                     LPDOUBLE LumLZCE,
                                     LPDOUBLE LumLCC,
                                     LPDOUBLE LumLPE,
                                     LPDOUBLE LumLCE)
   {

    *LumLZCC= Calc.LZCC;
    *LumLZPE= Calc.LZPE;
    *LumLZCE= Calc.LZCE;
    *LumLCC=  Calc.LCC;
    *LumLPE=  Calc.LPE;
    *LumLCE=  Calc.LCE;
   }



};


// Rotina para cálculo
// Retorno: 0 erro ou 1 OK
extern "C"
int FAR PASCAL _export CALCDIARIO(void);
int FAR PASCAL _export CALCDIARIO(void)
 {
  Calc.Calculos();
  Calc.Iluminancias();
  Calc.Luminancias();

  return 1;
 }





//
// Rotinas para devolução de resultados
// ====================================

// Luz Horizontal
extern "C"
LPDOUBLE FAR PASCAL _export ISDHCC(void);
LPDOUBLE FAR PASCAL _export ISDHCC(void)
 {
  return (LPDOUBLE)&Calc.ISDHCC;
 }

extern "C"
LPDOUBLE FAR PASCAL _export ISDHPE(void);
LPDOUBLE FAR PASCAL _export ISDHPE(void)
 {
  return (LPDOUBLE)&Calc.ISDHPE;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICHCC(void);
LPDOUBLE FAR PASCAL _export ICHCC(void)
 {
  return (LPDOUBLE)&Calc.ICHCC;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICHPE(void);
LPDOUBLE FAR PASCAL _export ICHPE(void)
 {
  return (LPDOUBLE)&Calc.ICHPE;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICHCE(void);
LPDOUBLE FAR PASCAL _export ICHCE(void)
 {
  return (LPDOUBLE)&Calc.ICHCE;
 }



// Luz Vertical
extern "C"
LPDOUBLE FAR PASCAL _export ISDVCC(void);
LPDOUBLE FAR PASCAL _export ISDVCC(void)
 {
  return (LPDOUBLE)&Calc.ISDVCC;
 }

extern "C"
LPDOUBLE FAR PASCAL _export ISDVPE(void);
LPDOUBLE FAR PASCAL _export ISDVPE(void)
 {
  return (LPDOUBLE)&Calc.ISDVPE;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICVCC(void);
LPDOUBLE FAR PASCAL _export ICVCC(void)
 {
  return (LPDOUBLE)&Calc.ICVCC;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICVPE(void);
LPDOUBLE FAR PASCAL _export ICVPE(void)
 {
  return (LPDOUBLE)&Calc.ICVPE;
 }


extern "C"
LPDOUBLE FAR PASCAL _export ICVCE(void);
LPDOUBLE FAR PASCAL _export ICVCE(void)
 {
  return (LPDOUBLE)&Calc.ICVCE;
 }



// Luminâncias
extern "C"
LPDOUBLE FAR PASCAL _export LCC(void);
LPDOUBLE FAR PASCAL _export LCC(void)
 {
  return (LPDOUBLE)&Calc.LCC;
 }

extern "C"
LPDOUBLE FAR PASCAL _export LPE(void);
LPDOUBLE FAR PASCAL _export LPE(void)
 {
  return (LPDOUBLE)&Calc.LPE;
 }

extern "C"
LPDOUBLE FAR PASCAL _export LCE(void);
LPDOUBLE FAR PASCAL _export LCE(void)
 {
  return (LPDOUBLE)&Calc.LCE;
 }






// Rotinas auxiliares

extern "C"
int FAR PASCAL _export STRVAL(LPSTR String);
int FAR PASCAL _export STRVAL(LPSTR String)
 {
  if (!stricmp(String, "Janeiro"))   return 1;
  if (!stricmp(String, "Fevereiro")) return 2;
  if (!stricmp(String, "Março"))     return 3;
  if (!stricmp(String, "Abril"))     return 4;
  if (!stricmp(String, "Maio"))      return 5;
  if (!stricmp(String, "Junho"))     return 6;
  if (!stricmp(String, "Julho"))     return 7;
  if (!stricmp(String, "Agosto"))    return 8;
  if (!stricmp(String, "Setembro"))  return 9;
  if (!stricmp(String, "Outubro"))   return 10;
  if (!stricmp(String, "Novembro"))  return 11;
  if (!stricmp(String, "Dezembro"))  return 12;

  return(atoi(String));
 }


// Converte uma data julinana para mes/dia
void ConvDataJuliana(int DiaJuliano, int *Dia, int *Mes)
 {
  // Tranforma data Juliana em dia/mes
  int DiasMes[] = {31,28,31,30,31,30,31,31,30,31,30,31};
  int TotalDias= 0, NrMes= 0;

  if (DiaJuliano < 1) DiaJuliano= 1;
  if (DiaJuliano > 365) DiaJuliano= 365;

  while ( (TotalDias + DiasMes[NrMes]) < DiaJuliano)
     {
      TotalDias+= DiasMes[NrMes];
      NrMes++;
     }
  // Seta informação de retorno
  *Dia= DiaJuliano - TotalDias;
  *Mes= NrMes + 1;
 }


void ExportaPlan(FILE *ArqPlan, char *linha)
 {
  for (int i= 0; i<strlen(linha); i++)
    if (linha[i] == '.') linha[i]= ',';
  fwrite(linha, strlen(linha), 1, ArqPlan);
 }


// Imprime comparativo da moda
void Comparativo(int dia_ini,
                 int dia_fim,
                 int dia_moda,
                 FILE *Arq,
                 FILE *ArqPlan)
 {
  int DiaAux, Dia;
  char Linha[200];
  // variaveis para guardarem o valor da moda para ate as 19 horas do dia
  double moda_CC[19], moda_PE[19], moda_CE[19];
  double dia20_CC[19], dia20_PE[19], dia20_CE[19];
  int d, m;

  ConvDataJuliana(dia_ini, &d, &m);

  // Calcula os valores para o dia da moda
  for(int Hora= 8; Hora<= 18; Hora+=2)
   {

    Calc.SetaDataHora(dia_moda, Hora);
    Calc.Calculos();
    Calc.Iluminancias();
    moda_CC[Hora]= Calc.ISDHCC + Calc.ICHCC;
    moda_PE[Hora]= Calc.ISDHPE + Calc.ICHPE;
    moda_CE[Hora]= Calc.ICHCE;

    Calc.SetaDataHora(20, m, Hora, Calc.Legal);
    Calc.Calculos();
    Calc.Iluminancias();
    dia20_CC[Hora]= Calc.ISDHCC + Calc.ICHCC;
    dia20_PE[Hora]= Calc.ISDHPE + Calc.ICHPE;
    dia20_CE[Hora]= Calc.ICHCE;

   }

  // Calcula e imprime o percentual de diferenca entra cada dia do
  // periodo e a moda
  sprintf(Linha, "\n\nVariacoes percentuais em relacao ao dia luminoso tipico e dia 20\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  sprintf(Linha, "            ====== MODA =====    ===== DIA 20 ====\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  sprintf(Linha, "            =================    =================\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  sprintf(Linha, " Dia  Hora     CC    PE    CE       CC    PE    CE\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  sprintf(Linha, "===== ===== ===== ===== =====    ===== ===== =====\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  if (ArqPlan)
   {
    sprintf(Linha, "\n\nVariacoes percentuais em relacao ao dia luminoso tipico e dia 20\n");
    fwrite(Linha, strlen(Linha), 1, ArqPlan);

    sprintf(Linha, " ; ;====== MODA =====; ; ;===== DIA 20 ====\n");
    fwrite(Linha, strlen(Linha), 1, ArqPlan);

    sprintf(Linha, " ; ;=================; ; ;=================\n");
    fwrite(Linha, strlen(Linha), 1, ArqPlan);

    sprintf(Linha, "Dia;Hora;CC;PE;CE;CC;PE;CE\n");
    fwrite(Linha, strlen(Linha), 1, ArqPlan);

    sprintf(Linha, "=====;=====;=====;=====;=====;=====;=====;=====\n");
    fwrite(Linha, strlen(Linha), 1, ArqPlan);
   }


  for (DiaAux = dia_ini; DiaAux <= dia_fim; DiaAux++)
   {
    if (DiaAux <= 0) Dia= 365 + DiaAux; else Dia= DiaAux;
    for (Hora= 8; Hora<= 18; Hora+=2)
     {
      double pcCC, pcPE, pcCE;
      double pc20CC, pc20PE, pc20CE;

      Calc.SetaDataHora(Dia, Hora);
      Calc.Calculos();
      Calc.Iluminancias();

      pcCC= 100.0 * fabs(Calc.ISDHCC + Calc.ICHCC - moda_CC[Hora]) / moda_CC[Hora];
      pcPE= 100.0 * fabs(Calc.ISDHPE + Calc.ICHPE - moda_PE[Hora]) / moda_PE[Hora];
      pcCE= 100.0 * fabs(Calc.ICHCE- moda_CE[Hora])                / moda_CE[Hora];

      pc20CC= 100.0 * fabs(Calc.ISDHCC + Calc.ICHCC - dia20_CC[Hora]) / dia20_CC[Hora];
      pc20PE= 100.0 * fabs(Calc.ISDHPE + Calc.ICHPE - dia20_PE[Hora]) / dia20_PE[Hora];
      pc20CE= 100.0 * fabs(Calc.ICHCE- dia20_CE[Hora])                / dia20_CE[Hora];

      ConvDataJuliana(Dia, &d, &m);
      sprintf(Linha, "%2d/%02d %02d:00 %5.1lf %5.1lf %5.1lf    %5.1lf %5.1lf %5.1lf\n",
                d, m, Hora, pcCC, pcPE, pcCE, pc20CC, pc20PE, pc20CE);
      fwrite(Linha, strlen(Linha), 1, Arq);

      if (ArqPlan)
       {
        sprintf(Linha, "%02d/%02d;%02d:00;%5.1lf;%5.1lf;%5.1lf;%5.1lf;%5.1lf;%5.1lf\n",
                  d, m, Hora, pcCC, pcPE, pcCE, pc20CC, pc20PE, pc20CE);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);
       }


     };
   }

 }


// Imprime o resultado do dia calculado
void ImprimeResultado(FILE *Arq,
                      FILE *ArqPlan,
                      char *tit,
                      int Dia,
                      double Desvio)
 {
  int DiaIni, MesIni;
  char Linha[200];

  ConvDataJuliana(Dia, &DiaIni, &MesIni);

  if (DiaIni < 10)
         sprintf(Calc.DiaMedia, "%1d", DiaIni);
   else  sprintf(Calc.DiaMedia, "%2d", DiaIni);

  switch (MesIni)
     {
      case  1: strcpy(Calc.MesMedia, "Janeiro"); break;
      case  2: strcpy(Calc.MesMedia, "Fevereiro"); break;
      case  3: strcpy(Calc.MesMedia, "Março"); break;
      case  4: strcpy(Calc.MesMedia, "Abril"); break;
      case  5: strcpy(Calc.MesMedia, "Maio"); break;
      case  6: strcpy(Calc.MesMedia, "Junho"); break;
      case  7: strcpy(Calc.MesMedia, "Julho"); break;
      case  8: strcpy(Calc.MesMedia, "Agosto"); break;
      case  9: strcpy(Calc.MesMedia, "Setembro"); break;
      case 10: strcpy(Calc.MesMedia, "Outubro"); break;
      case 11: strcpy(Calc.MesMedia, "Novembro"); break;
      case 12: strcpy(Calc.MesMedia, "Dezembro"); break;
     }

  sprintf(Linha, "\n\n%s: %s/%s (Juliano=%d)\n"
     "                        Desvio padrao de %11.3f\n\n",
             tit, Calc.DiaMedia, Calc.MesMedia,
             Dia, Desvio);
  fwrite(Linha, strlen(Linha), 1, Arq);

  if (ArqPlan)
   {
    sprintf(Linha, "\n\n%s:;%s/%s;(Juliano=;%d)\n"
       "                        Desvio padrao de;%11.3f\n\n",
               tit, Calc.DiaMedia, Calc.MesMedia,
               Dia, Desvio);
    //fwrite(Linha, strlen(Linha), 1, ArqPlan);
    ExportaPlan(ArqPlan, Linha);
   }



 }




//                                                           M E D I A
// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


// Cálculo da média (rotina para uso interno apenas)
// O Resultado é retonado em DiaIni e MesIni
int Calc_Media( int& DiaIni, int& MesIni, int DiaFim, int MesFim, int Dump)
 {

  // Converte os valores informados para o calendário Juliano
  int JulianaIni, JulianaFim;
  // Transforma datas em valores julianos
  Calc.SetaDataHora(DiaIni, MesIni, 8.0, Calc.Legal);
  JulianaIni= (int)Calc.J;

  Calc.SetaDataHora(DiaFim, MesFim, 8.0, Calc.Legal);
  JulianaFim= (int)Calc.J;

  char Linha[200];
  int DeveImprimir= 0;

  // Arquivo usado dump da média
  FILE *Arq= NULL;
  FILE *ArqPlan= NULL;

  if (Dump)
    {
     Arq= fopen("DLNMODA.TXT", "wt");
     ArqPlan= fopen("DLNXLS.TXT", "wt");
    }

  DeveImprimir= Arq != NULL;

  if (DeveImprimir)
       {

        sprintf(Linha, "Calculo do dia Luminoso Tipico\n");
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "******************************\n\n");
        fwrite(Linha, strlen(Linha), 1, Arq);

        sprintf(Linha, "Latitude....: %12.4f rad\n", Calc.LAT);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Longitude...: %12.4f rad\n", Calc.LONGI);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Meridiano...: %12.4f rad\n", Calc.MP);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Azimute.....: %12.4f rad\n", Calc.AE);
        fwrite(Linha, strlen(Linha), 1, Arq);

        sprintf(Linha, "\n\nPeriodo: %d/%d a %d/%d\n\n", DiaIni, MesIni, DiaFim, MesFim );
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "\n Dia   Hora Ilum.H.CC Ilum.H.PE Ilum.H.CE\n");
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "===== ===== ========= ========= =========\n");
        fwrite(Linha, strlen(Linha), 1, Arq);
      }

  if (ArqPlan)
       {

        sprintf(Linha, "Calculo do dia Luminoso Tipico\n");
        fwrite(Linha, strlen(Linha), 1, ArqPlan);
        sprintf(Linha, "******************************\n\n");
        fwrite(Linha, strlen(Linha), 1, ArqPlan);

        sprintf(Linha, "Latitude;%12.4f;rad\n", Calc.LAT);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);

        sprintf(Linha, "Longitude;%12.4f;rad\n", Calc.LONGI);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);

        sprintf(Linha, "Meridiano;%12.4f;rad\n", Calc.MP);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);

        sprintf(Linha, "Azimute;%12.4f;rad\n", Calc.AE);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);

        sprintf(Linha, "\n\nPeriodo;%d/%d;a;%d/%d\n\n", DiaIni, MesIni, DiaFim, MesFim );
        fwrite(Linha, strlen(Linha), 1, ArqPlan);
        sprintf(Linha, "\nDia;Hora;Ilum.H.CC;Ilum.H.PE;Ilum.H.CE\n");
        fwrite(Linha, strlen(Linha), 1, ArqPlan);
        sprintf(Linha, "=====;=====;=========;=========;=========\n");
        fwrite(Linha, strlen(Linha), 1, ArqPlan);
      }




  int ValHora []= { 8, 10, 12, 14, 16, 18};
  int TotHoras= 6;

  // Zera acumuladores
  double TCC[10], TCE[10], TPE[10];
  for (int Hora= 0; Hora<TotHoras; Hora++)
     {
      TCC[Hora]= 0.0;
      TCE[Hora]= 0.0;
      TPE[Hora]= 0.0;
     }


  if (JulianaIni > JulianaFim) JulianaIni-= 365;

  // Primeiro cálculo: busca a média do período
  int Dia;
  for (int DiaAux = JulianaIni; DiaAux <= JulianaFim; DiaAux++)
   {

     // Permite calcular períodos que começam num ano e terminam em outro
     if (DiaAux <= 0) Dia= 365 + DiaAux; else Dia= DiaAux;

    for (Hora= 0; Hora<TotHoras; Hora++)
     {
      Calc.SetaDataHora(Dia, ValHora[Hora]);
      Calc.Calculos();
      // Média pelo total de iluminância horizontal
      Calc.Iluminancias();
      TCC[Hora]+= Calc.ISDHCC + Calc.ICHCC;
      TCE[Hora]+= Calc.ICHCE;
      TPE[Hora]+= Calc.ISDHPE + Calc.ICHPE;
      if (DeveImprimir)
        {
         int d, m;
         ConvDataJuliana(Dia, &d, &m);

         sprintf(Linha, "%2d/%02d %02d:00 %9.1f %9.1f %9.1f\n", d, m, ValHora[Hora],
                        (Calc.ISDHCC + Calc.ICHCC),
                        (Calc.ISDHPE + Calc.ICHPE),
                        (Calc.ICHCE)                   );
         fwrite(Linha, strlen(Linha), 1, Arq);
        }

      if (ArqPlan)
        {
         int d, m;
         ConvDataJuliana(Dia, &d, &m);

         sprintf(Linha, "%02d/%02d;%02d:00;%9.1f;%9.1f;%9.1f\n", d, m, ValHora[Hora],
                        (Calc.ISDHCC + Calc.ICHCC),
                        (Calc.ISDHPE + Calc.ICHPE),
                        (Calc.ICHCE)                   );
         //fwrite(Linha, strlen(Linha), 1, ArqPlan);
         ExportaPlan(ArqPlan, Linha);
        }

     };
   }

  // Cálculo da média

  double tot_dias= fabs(double(JulianaFim - JulianaIni + 1));

  if (DeveImprimir)
    {
     sprintf(Linha, "\nTotal: %d dias\n\n", abs(JulianaFim - JulianaIni + 1) );
     fwrite(Linha, strlen(Linha), 1, Arq);
     strcpy(Linha, "Medias para cada hora:\n");
     fwrite(Linha, strlen(Linha), 1, Arq);
     sprintf(Linha, "Hora  Ilum.H.CC Ilum.H.PE Ilum.H.CE\n");
     fwrite(Linha, strlen(Linha), 1, Arq);
     sprintf(Linha, "===== ========= ========= =========\n");
     fwrite(Linha, strlen(Linha), 1, Arq);
    }


  if (ArqPlan)
    {
     sprintf(Linha, "\nTotal:;%d;dias\n\n", abs(JulianaFim - JulianaIni + 1) );
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
     strcpy(Linha, "Medias para cada hora:\n");
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
     sprintf(Linha, "Hora;Ilum.H.CC;Ilum.H.PE;Ilum.H.CE\n");
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
     sprintf(Linha, "=====;=========;=========;=========\n");
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
    }


  for (Hora= 0; Hora<TotHoras; Hora++)
    {
     TCC[Hora]/= tot_dias;
     TCE[Hora]/= tot_dias;
     TPE[Hora]/= tot_dias;
     if (DeveImprimir)
       {
        sprintf(Linha, "%02d:00 %9.1f %9.1f %9.1f\n",
             ValHora[Hora], TCC[Hora],TPE[Hora],TCE[Hora]);
        fwrite(Linha, strlen(Linha), 1, Arq);
       }
     if (ArqPlan)
       {
        sprintf(Linha, "%02d:00;%9.1f;%9.1f;%9.1f\n",
             ValHora[Hora], TCC[Hora],TPE[Hora],TCE[Hora]);
        //fwrite(Linha, strlen(Linha), 1, ArqPlan);
        ExportaPlan(ArqPlan, Linha);
       }
    }

  if (DeveImprimir)
    {
     fwrite("\nVariancia e Desvio-Padrao diarios:\n", 1, 1, Arq);
     sprintf(Linha, "\n Dia      Desvio CC     Desvio PE     Desvio CE\n");
     fwrite(Linha, strlen(Linha), 1, Arq);
     sprintf(Linha,   "===== ============= ============= =============\n");
     fwrite(Linha, strlen(Linha), 1, Arq);
    }

  if (ArqPlan)
    {
     fwrite("\nVariancia e Desvio-Padrao diarios:\n", 1, 1, Arq);
     sprintf(Linha, "\n;Dia;Desvio CC;Desvio PE;Desvio CE\n");
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
     sprintf(Linha,   "=====;=============;=============;=============\n");
     fwrite(Linha, strlen(Linha), 1, ArqPlan);
    }


  // Segundo cálculo: busca dia mais próximo da média
  double VarianciaCC, VarianciaCE, VarianciaPE,
         MenorDesvioCC= 300.0,
         MenorDesvioCE= 300.0,
         MenorDesvioPE= 300.0;
  int DiaMediaCC= 0,
      DiaMediaCE= 0,
      DiaMediaPE= 0;

  for (DiaAux = JulianaIni; DiaAux <= JulianaFim; DiaAux++)
   {
    if (DiaAux <= 0) Dia= 365 + DiaAux; else Dia= DiaAux;

    VarianciaCC= VarianciaCE= VarianciaPE= 0.0;

    for (Hora= 0; Hora<TotHoras; Hora++)
     {
      Calc.SetaDataHora(Dia, ValHora[Hora]);
      Calc.Calculos();
      Calc.Iluminancias();

      double Aux= ((Calc.ISDHCC + Calc.ICHCC) - TCC[Hora] );
      VarianciaCC+= (Aux * Aux);

      Aux= ((Calc.ICHCE) - TCE[Hora] );
      VarianciaCE+= (Aux * Aux);

      Aux= ((Calc.ISDHPE + Calc.ICHPE) - TPE[Hora] );
      VarianciaPE+= (Aux * Aux);

     };


    double Desvio= sqrt(VarianciaCC);
    int d, m;

    if (Desvio < MenorDesvioCC)
      {
       MenorDesvioCC= Desvio;
       DiaMediaCC= Dia;
      }


    Desvio= sqrt(VarianciaCE);
    if (Desvio < MenorDesvioCE)
      {
       MenorDesvioCE= Desvio;
       DiaMediaCE= Dia;
      }

    Desvio= sqrt(VarianciaPE);
    if (Desvio < MenorDesvioPE)
      {
       MenorDesvioPE= Desvio;
       DiaMediaPE= Dia;
      }

    ConvDataJuliana(Dia, &d, &m);

    if (DeveImprimir)
      {
       sprintf(Linha, "%2d/%02d %13.3f %13.3f %13.3f\n", d, m,
            sqrt(VarianciaCC),
            sqrt(VarianciaPE),
            sqrt(VarianciaCE)  );

       fwrite(Linha, strlen(Linha), 1, Arq);
      }
    if (ArqPlan)
      {
       sprintf(Linha, "%2d/%02d;%13.3f;%13.3f;%13.3f\n", d, m,
            sqrt(VarianciaCC),
            sqrt(VarianciaPE),
            sqrt(VarianciaCE)  );
       ExportaPlan(ArqPlan, Linha);
      }
   }


  // Salva no objeto o resultado do cálculo da média
  Calc.DiaMediaJuliano= (double)DiaMediaCC;

  if (DeveImprimir)
   {
    ImprimeResultado(Arq, ArqPlan, "Dia tipico para CC", DiaMediaCC, MenorDesvioCC);
    Comparativo(JulianaIni, JulianaFim, DiaMediaCC, Arq, ArqPlan);

    ImprimeResultado(Arq, ArqPlan, "Dia tipico para PE", DiaMediaPE, MenorDesvioPE);
    Comparativo(JulianaIni, JulianaFim, DiaMediaPE, Arq, ArqPlan);

    ImprimeResultado(Arq, ArqPlan, "Dia tipico para CE", DiaMediaCE, MenorDesvioCE);
    Comparativo(JulianaIni, JulianaFim, DiaMediaCE, Arq, ArqPlan);

    fclose(Arq);
    Arq= NULL;
    if (ArqPlan) fclose(ArqPlan);

   }

  return 1;

 } // fim de Calc_Media()


/*
  Cálculo da média de um período
  Rotina para ser chamada de dentro do Object Vision
  Obs.: mês é fornecido em formato string (por extenso)
*/


extern "C"
int FAR PASCAL _export MEDIA(HWND ParentHWnd, LPSTR sDiaIni, LPSTR sMesIni,
                             LPSTR sDiaFim, LPSTR sMesFim, int Dump);
int FAR PASCAL _export MEDIA(HWND ParentHWnd, LPSTR sDiaIni, LPSTR sMesIni,
                             LPSTR sDiaFim, LPSTR sMesFim, int Dump)
 {


  int DiaIni, MesIni, DiaFim, MesFim;
  DiaIni= atoi(sDiaIni);
  DiaFim= atoi(sDiaFim);
  MesIni= STRVAL(sMesIni);
  MesFim= STRVAL(sMesFim);


  if (DiaIni == 0 || DiaFim == 0 || MesIni == 0 || MesFim == 0)
      return(0);

  return Calc_Media(DiaIni, MesIni, DiaFim, MesFim, Dump);
 }


// Cálculo da Média com retorno em variáveis inteiras
extern "C"
int FAR PASCAL _export MEDIAINT(int &DiaIni, int &MesIni,
                                int DiaFim, int MesFim, int Dump);
int FAR PASCAL _export MEDIAINT(int &DiaIni, int &MesIni,
                                int DiaFim, int MesFim, int Dump)
 {

  if (DiaIni == 0 || DiaFim == 0 || MesIni == 0 || MesFim == 0)
      return(0);

  return Calc_Media(DiaIni, MesIni, DiaFim, MesFim, Dump);
 }



// As duas rotinas abaixo devem ser usadas imediatamente após o cálculo
// da média, pois o resultado é guardado em Calc.J

extern "C"
LPSTR FAR PASCAL _export DIA_CALC(void);
LPSTR FAR PASCAL _export DIA_CALC(void)
 {
  return (LPSTR)Calc.DiaMedia;
 }

extern "C"
LPSTR FAR PASCAL _export MES_CALC(void);
LPSTR FAR PASCAL _export MES_CALC(void)
 {
  return (LPSTR)Calc.MesMedia;
 }





//                                                           G R A F I C O
// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


//
// Rotinas para geração de um arquivo texto para exportação de
// resultados de cálculos (montagem de gráficos)
// *********************************
//




// Abre arquivo retornando 1 se OK ou 0
extern "C"
int FAR PASCAL _export abre_arquivos(LPSTR Path);
int FAR PASCAL _export abre_arquivos(LPSTR Path)
 {
  Arq= fopen(Path, "wt");
  if (Arq) return(1); else return(0);
 };


extern "C"

// Fecha arquivo retornando 1 se OK ou 0
int FAR PASCAL _export fecha_arquivo(void);
int FAR PASCAL _export fecha_arquivo(void)
 {
  if (Arq)
       {
        fclose(Arq);
        Arq= NULL;
        return(1);
       }
     else return(0);
 };

extern "C"
// Grava uma string no arquivo (retorna 1 se OK ou 0)
// Obs.: grava fim de linha se solicitado (FimLinha != 0)
//       ou um tabuladoe separando campo
int FAR PASCAL _export grava_linha(LPSTR Linha, int FimLinha);
int FAR PASCAL _export grava_linha(LPSTR Linha, int FimLinha)
 {
  if (Arq)
       {
        fwrite(Linha, strlen(Linha), 1, Arq);
	if (FimLinha)
              fwrite("\n", 1, 1, Arq);
         else fwrite("\t", 1, 1, Arq);
	return(1);
       }
     else return(0);
 };

extern "C"
// Grava uma linha no arquivo com todos os dados do último cálculo
int FAR PASCAL _export grava_calc(void);
int FAR PASCAL _export grava_calc(void)
 {
  char Linha[200];
  if (Arq)
       {
  sprintf(Linha, "%3.0f\t%5.2f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f\t%6.1f",
                Calc.J, Calc.HP,
              // Luz Horizontal
		Calc.ISDHCC+Calc.ICHCC,
		Calc.ISDHPE+Calc.ICHPE,
                Calc.ICHCC,
                Calc.ICHPE,
                Calc.ICHCE,

              // Luz Vertical
                Calc.ISDVCC,
                Calc.ISDVPE,
                Calc.ICVCC,
                Calc.ICVPE,
                Calc.ICVCE,
                Calc.LCC,
                Calc.LPE,
                Calc.LCE,
                Calc.ISDVCC + Calc.ICVCC,
                Calc.ISDVPE + Calc.ICVPE,
                Calc.ICVCE                 );

        fwrite(Linha, strlen(Linha), 1, Arq);
        fwrite("\n", 1, 1, Arq);
        return(1);
       }
     else return(0);
 };


extern "C"
// Grava dados do cálculo
int FAR PASCAL _export grava_dados(void);
int FAR PASCAL _export grava_dados(void)
 {
  char Linha[200];
  if (Arq)
       {
        sprintf(Linha, "Latitude: %12.4f\n", Calc.LAT);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Longitude:  %12.4f\n", Calc.LONGI);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Meridiano: %12.4f\n", Calc.MP);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Azimute: %12.4f\n", Calc.AE);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Azimute-P: %12.4f\n", Calc.Alfa);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Angulo-P: %12.4f\n", Calc.Zeta);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Data:  %5.0f\n", Calc.J);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Hora: %8.4f\n", Calc.HP);
        fwrite(Linha, strlen(Linha), 1, Arq);
        return(1);
       }
     else return(0);
 };


 extern "C"
// Gera a tabela para ser passada ao programa que traça o gráfico
int FAR PASCAL _export gera_tab(void);
int FAR PASCAL _export gera_tab(void)
 {
  char Linha[200];
  FILE *Arq= fopen("TABELA.TXT","wt");
  if (!Arq) return(0);
  if (Arq)
       {
        sprintf(Linha, "Latitude: %12.4f\n", Calc.LAT);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Longitude:  %12.4f\n", Calc.LONGI);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Meridiano: %12.4f\n", Calc.MP);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Azimute: %12.4f\n", Calc.AE);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Azimute-P: %12.4f\n", Calc.Alfa);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Angulo-P: %12.4f\n", Calc.Zeta);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Data:  %5.0f\n", Calc.J);
        fwrite(Linha, strlen(Linha), 1, Arq);
        sprintf(Linha, "Hora: %8.4f\n", Calc.HP);
        fwrite(Linha, strlen(Linha), 1, Arq);
        fclose(Arq);
       }
  return(1);
 };




//                                                           T A B E L A
// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
//     TABELAS TOTAIS (4 estações com Lumin. + 8 azimutes
// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
// Gera uma tabela com todos os horários para o dia da média
//   que se supõe já calculado
// Parâmetros: Titulo1    --> string qq. a ser impressa como título
//             Azimute    --> novo azimute (em graus, valor inteiro)
//             Hotizontal --> = 1 então calcula iluminância horizontal
// OBS.: supõe o arquivo de saída já inicializado
void Tabela_Diaria(char *Titulo1,
                   int Azimute,
                   int Horizontal,
                   double DiaMedia)
 {
  char Linha[200];

  sprintf(Linha, "\n%s\n", Titulo1);
  fwrite(Linha, strlen(Linha), 1, Arq);

  sprintf(Linha, "Hora\tCC\tPN\tCE\n");
  fwrite(Linha, strlen(Linha), 1, Arq);

  // Compensa a diferenca para hemisferio sul (22/1/94)
  Calc.AE= (double)Azimute  * PI180 + pi;

  // Seta o dia pelo valor da média suposto já calculado
  Calc.J= DiaMedia;

  for (int hora= 5; hora<20; hora++)
    {
     // Hora padrao em decimal (minutos e segundos ignorados)
     Calc.HP= (double)hora;

     // Executa os cálculos básicos + iluminâncias
     Calc.Calculos();
     Calc.Iluminancias();

     // Coloca no arquivo somente os dados solicitados
     if (Horizontal)
          {
           // Luz Horizontal
           sprintf(Linha, "%5.2f\t%6.1f\t%6.1f\t%6.1f",
                Calc.HP,
		Calc.ISDHCC + Calc.ICHCC ,
		Calc.ISDHPE + Calc.ICHPE,
                Calc.ICHCE);
          }
     else
          {
           // Luz Vertical
           sprintf(Linha, "%5.2f\t%6.1f\t%6.1f\t%6.1f",
                Calc.HP,
                Calc.ISDVCC + Calc.ICVCC,
                Calc.ISDVPE + Calc.ICVPE,
                Calc.ICVCE);
          }

     fwrite(Linha, strlen(Linha), 1, Arq);
     fwrite("\n", 1, 1, Arq);
    }

  return;

 } // fim de Tabela_Diaria()


// Controla os cálculos para uma estação (8 azimutes principais)
// São fornecidos os dias inicial e final da estação
void Calc_Estacao( char *Estacao,
                   int _DiaIni,
                   int _MesIni,
                   int _DiaFim,
                   int _MesFim)
 {

  char Linha[200];
  int DiaIni= _DiaIni;
  int MesIni= _MesIni;
  int DiaFim= _DiaFim;
  int MesFim= _MesFim;

  Calc_Media(DiaIni, MesIni, DiaFim, MesFim, 1);

  double DiaMedia= Calc.DiaMediaJuliano;

  sprintf(Linha,
          "Dia Luminoso Típico de %s\nIluminâncias (Klux) em %d/%d\n",
	  Estacao, DiaIni, MesIni);
  fwrite(Linha, strlen(Linha), 1, Arq);

  // Iluminâncias
  Tabela_Diaria("Plano Horizontal",          0, 1, DiaMedia);
  Tabela_Diaria("Plano Vertical Norte",      0, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Nordeste",  45, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Leste",     90, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Sudoeste", 135, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Sul",      180, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Sudoeste", 225, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Oeste",    270, 0, DiaMedia);
  Tabela_Diaria("Plano Vertical Noroeste", 315, 0, DiaMedia);

 } // fim de Calc_estacao()


// Abre o arquivo de exportação de dados, imprime o cabec
// com os dados já setados para o cálculo
// Erros: 1 arquivo já aberto
//        2 falha na abertura do arquivo
// OBS.: função exportável para Object Vision
extern "C"
int  FAR PASCAL _export GERATAB(LPSTR Titulo);
int  FAR PASCAL _export GERATAB(LPSTR Titulo)
 {

  Arq= fopen("TABELA.TXT", "wt");
  if (!Arq) return 2;;

  fwrite(Titulo, strlen(Titulo), 1, Arq);
  fwrite("\n", 1, 1, Arq);

  // Cálculos e tabelas para cada estação do ano
  // (para cada estação são informados dia inicial e dia final
  //  cf. valores do calendário Juliano)
  Calc_Estacao("Verão",     22, 12, 19,  3);
  Calc_Estacao("Outono",    20,  3, 20,  6);
  Calc_Estacao("Inverno",   21,  6, 22,  9);
  Calc_Estacao("Primavera", 23,  9, 21, 12);

  fclose(Arq);

  return 1;

 } // fim de GERATAB()


