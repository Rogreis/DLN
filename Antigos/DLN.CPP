/****************************************************************

 MODULO......: DLN

 DESCRICAO...: Calculo da disponibilidade de luz natural

 DATA........: novembro/91

 OBS.........:

 *****************************************************************/

#include "math.h"
#include "stdio.h"
#include "string.h"
#include "dln.hpp"


// seta valores, menos data e hora do calculo
void DLN::SetaValores(DADOS_ENTRADA &dados)
 {

  LAT=  ((double)dados.latgraus  +
            (double)dados.latmin/60.0  +
              (double)dados.latseg/3600.0)  * PI180;
  LONG= ((double)dados.longgraus +
            (double)dados.longmin/60.0 +
              (double)dados.longseg/3600.0)  * PI180;
  AE=   ((double)dados.azigraus  +
            (double)dados.azimin/60.0  +
              (double)dados.aziseg/3600.0)  * PI180;
  MP=   ((double)dados.mergraus  +
            (double)dados.mermin/60.0  +
              (double)dados.merseg/3600.0)  * PI180;
  PZ=   ((double)dados.angPgraus +
            (double)dados.angPmin/60.0 +
              (double)dados.angPseg/3600.0)  * PI180;
  PA=   ((double)dados.aziPgraus +
            (double)dados.aziPmin/60.0 +
              (double)dados.aziPseg/3600.0)  * PI180;
 };

// seta data informada em modo normal mais hora
void DLN::SetaDataHora(int Dia, int Mes, int Hora)
 {

  // Transforma a data em data juliana
  int DiasMes[] = {31,28,31,30,31,30,31,31,30,31,30,31};
  int TotalDias= Dia;
  for (int i= 0; i < (Mes-1); i++) TotalDias+= DiasMes[i];
  J= (double)TotalDias;

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


void DLN::Calculos(void)
 {
  // (07) Equacao do tempo
  ET= 0.170L * sin(4.0L*pi/373.0L * (J - 80.0L)) -
       0.129L * sin(2.0L*pi/355.0L *(J - 8.0L));

  // (08) Tempo Solar
  T= HP + ET + ((12.0L*(MP - LONG)) / pi);

  // (09) declinacao solar
  D= 0.4093L * sin( (2.0L*pi/368.0L) * (J - 81.0L));

  // (10) altitude solar
  AT= asin( sin(LAT)*sin(D) - cos(LAT)*cos(D) * cos(pi*T/12.0L) );
  // Correcao feita 12/5/93 Rogerio
  // AT nao pode estar fora do intervalo acima, pois indica
  // sol abaixo da linha do horizonte
  if ( (AT > pi) || (AT < 0.0)  ) AT= 0.0;

  // (11) Azimute solar ???????????????????????????
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
  // Coreccao feita em 12/5/93 por Rogerio
  if ( AT == 0.0  )
	  M= 1.0L;
     else M= 1.0L / sin(AT);

  // (17) Angulo entre o sol e o zenite
  Z= pi/2.0L - AT;

  // (18) Angulo entre o sol e ponto P no ceu
  PS= acos( cos(Z) * cos(PZ) );

  // (19) Iluminancia solar direta normal (ISDN)
  ISDNCC= ISE * exp(-0.21 * M);
  ISDNPE= ISE * exp(-0.80 * M);

  // (20) Iluminancia solar direta horizontal (ISDH)
  ISDHCC= ISDNCC * sin(AT);
  ISDHPE= ISDNPE * sin(AT);

  // (21) Iluminancia solar direta vertical (ISDV)
  ISDVCC= ISDNCC * cos(AI);
  ISDVPE= ISDNPE * cos(AI);

  Iluminancias();

  Luminancias();

 }


void DLN::Iluminancias(void)
 {
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

 }



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

  LCC= LZCC * ( ( (0.91*10.0*exp(-3*PS) + 0.45*cos(PS)*cos(PS)) * (1.0-exp(-0.32/cos(PZ))) ) /
		( (0.91+10.0*exp(-3*Z)  + 0.45*cos(Z)*cos(Z)  ) * (1.0-exp(-0.32))         )

	      );

  LPE= LZPE * ( ( (0.526*5.0*exp(-1.5*PS)) * (1.0-exp(-0.80/cos(PZ))) ) /
		( (0.526+5.0*exp(-1.5*Z)) * (1.0-exp(-0.80))          )

              );

  LCE= LZCE * ( 0.864 * (exp(-0.52/cos(PZ))) / (exp(-0.52))   +
                0.136 * (1.0 - exp(-0.52/cos(PZ))) / (1.0 - exp(-0.52))
              );

  // 26.3.1 e uma formula redundante (valor ja calculado acima)
 }


#define DEBUG
#ifdef DEBUG

#include "string.h"

DADOS_ENTRADA dados;
DLN c;

void main(void)
 {
   strcpy(dados.Nome, "Teste");
   dados.latgraus= 25; dados.latmin=23;  dados.latseg=13;
   dados.longgraus=-45; dados.longmin=10; dados.longseg=0;
   dados.mergraus=-45;  dados.mermin=1;  dados.merseg=10;
   dados.azigraus=23; dados.azimin=34;  dados.aziseg=10;
   dados.aziPgraus=0;  dados.aziPmin=0; dados.aziPseg=0;
   dados.angPgraus=0;  dados.angPmin=0; dados.angPseg=0;
   dados.diaini= 30;    dados.mesini=10;  dados.anoini=93;
   dados.diafim=31;    dados.mesfim=4;  dados.anofim=93;
   dados.horas=8;      dados.minutos=0; dados.segundos=0;

  c.SetaValores(dados);


  c.SetaDataHora(dados.diaini, dados.mesini, 18);
  c.Calculos();

  /*
  double TOTLUZSOLCC, TOTLUZSOLPE, TOTLUZCEUCC,
         TOTLUZCEUPE, TOTLUZCEUCE;
    TOTLUZSOLCC= c.ISDVCC;
    TOTLUZSOLPE= c.ISDVPE;
    TOTLUZCEUCC= c.ICVCC;
    TOTLUZCEUPE= c.ICVPE;
    TOTLUZCEUCE= c.ICVCE;
  */
 }
#endif



