
#include "math.h"

#define e 2.718281828L
#define pi 3.141592654L
#define CIS 127.5L
#define PI180 pi/180.0L


typedef
 struct
  {
    char Nome[31];

    // dados para a entrada de dados
    int latgraus,  latmin,  latseg,
        longgraus, longmin, longseg,
        mergraus,  mermin,  merseg,
        azigraus,  azimin,  aziseg,
        aziPgraus, aziPmin, aziPseg,
        angPgraus, angPmin, angPseg,
        diaini,    mesini,  anoini,
        diafim,    mesfim,  anofim,
        horas,     minutos, segundos;
   } DADOS_ENTRADA;

class DLN
 {
  public:
   // Seta valores
   //void SetaValores(char *Tipo, int Graus, int Minutos, int Segundos);
   void SetaValores(DADOS_ENTRADA &dados);
   void SetaDataHora(int DataJuliana, int Hora);
   void SetaDataHora(int Dia, int Mes, int Hora);

   // Executa os calculos basicos
   void Calculos(void);

   void Iluminancias(void);
   void Luminancias(void);



   // Dados de entrada
   double
     J,     // DataJuliana
     HP,    // HoraPadrao,         /* hora decimal  */
     LAT,   // Latitude          em radianos
     LONG,  // Longitude         "
     AE,    // AzimuteElevacao   "
     MP,    // MeridianoPadrao   "
     PZ,    // Angulo zenital do ponto P em radianos
     PA;    // Azimute do ponto P em radianos

    // Dados calculados
    double
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

     ITHCE,  // valores totais
     ITHPE,
     ITHCC,
     ITVCE,
     ITVPE,
     ITVCC,

     IMCHCC, // iluminancia proveniente do meio ceu horizontal
     IMCHPE,
     IMCHCE,

     LZCC,   // luminancia do zenite
     LZPE,
     LZCE,

     LCC,    // luminancia do ponto P
     LPE,
     LCE;

 };


// Rotinas comuns
void Imprime(DADOS_ENTRADA &DadosEntrada, double _datajuliana);



