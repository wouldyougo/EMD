using System.Collections.Generic;

namespace TimeSeries
{
    public partial class qACorr
    {
        public qACorr()
        {
            this.KSgm = 1;
        }

        public qACorr(qACorr aACorr)
        {
            this.set(aACorr);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public qACorr(ref qData aData)
        {
            this.KSgm = 1;
            this.set(aData, 1);
        }
        /// <summary>
        /// копирует
        /// </summary>
        /// <param name="aACorr"></param>   
        public void set(qACorr aACorr)
        {
            this.Acorr = aACorr.Acorr;
            this.AcorrSgm = aACorr.AcorrSgm;
            this.AcorrSgmM = aACorr.AcorrSgmM;
            this.KSgm = aACorr.KSgm;
        }
        //---------------------------------------------------------------------------
        public virtual void set(qData aData, double k)
        {
            this.Acorr.set(aData.clcACorrelation());
            this.setSgm(k);
        }
        //---------------------------------------------------------------------------
        public virtual void setSgm(double k)
        {
            //   if(KSgm != k){
            KSgm = (int)k;
            this.AcorrSgm.set(this.Acorr.clcACorrSgm(k));
            this.AcorrSgmM.set(this.AcorrSgm.clcACorrSgmMin(this.AcorrSgm));
            //   }
        }

        //---------------------------------------------------------------------------
        public virtual void set(qData aData, double k, double procent)
        {
            this.Acorr.set(aData.clcACorrelation(procent));
            this.setSgm(k, procent);
        }
        //---------------------------------------------------------------------------
        public virtual void setSgm(double k, double procent)
        {
            //   if(KSgm != k){
            KSgm = (int)k;
            this.AcorrSgm.set(this.Acorr.clcACorrSgm(k, procent));
            this.AcorrSgmM.set(this.AcorrSgm.clcACorrSgmMin(this.AcorrSgm));
            //   }
        }
        //other
        //---------------------------------------------------------------------------
        public void clear()
        {
            this.Acorr.clear();
            this.AcorrSgm.clear();
            this.AcorrSgmM.clear();
        }
        //---------------------------------------------------------------------------

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData get() const
        public qData get()
        {
            qData tdt = new qData();
            tdt = this.Acorr;
            return tdt;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData getAcorrSgm() const
        public qData getAcorrSgm()
        {
            qData tdt = new qData();
            tdt = this.AcorrSgm;
            return tdt;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData getAcorrSgmM() const
        public qData getAcorrSgmM()
        {
            qData tdt = new qData();
            tdt = this.AcorrSgmM;
            return tdt;
        }

        //---------------------------------------------------------------------------

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int size() const
        public int size()
        {
            return this.Acorr.size();
        }
        //copy
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //functions      //Mat_Disp      //difference      //aCovar_aCorrel    //make
        //------
        protected qData Acorr = new qData();
        protected qData AcorrSgm = new qData();
        protected qData AcorrSgmM = new qData();
        protected int KSgm;
    }
    //---------------------------------------------------------------------------

    //#include "UCorrel.h"
    //#include "UMatrix.h"

    //---------------------------------------------------------------------------
    public class qModelACor : qModel
    {
        public qModelACor()
        {
            ModelName = "Автокорреляционная функция.";
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
        //#pragma package(smart_init)
        //---------------------------------------------------------------------------
        //qModelACor::
        //---------------------------------------------------------------------------
        public qModelACor(qModelACor aModelACor)
        {
            if (this != aModelACor)
            {
                //this = aModelACor;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setACor(ref qModelACor aModelACor)
        {
            if (this != aModelACor)
            {
                //this = aModelACor;
                throw new System.NotImplementedException();
            }
        }
        //++++++++++++++++++++++++++
        //void clear(void);
        //void set(qData aData);
        //qData getA(void);            // вернет Sgm-
        //qData getPrognos(void){;}    // вернет Sgm+
        public override void mkModel(int Metod)
        {
            ;
        }
        public new void mkPrognos(int NumPrognos)
        {
            ;
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void clcACor()
        {
            ACorr.set(this.Data, Koeff, Procent);
            this.Data.set(ACorr.get());
            this.Prognos.set(ACorr.getAcorrSgm());
            this.A.set(ACorr.getAcorrSgmM());
            ACorr.clear();
        }
        //---------------------------------------------------------------------------
        public void setKoeff(double aK)
        {
            Koeff = aK;
            //   if( (Koeff < 0) ){
            //      ShowMessage("qModelACor::setKoeff задан неверно");
            //      Koeff = 1;   }
        }
        //---------------------------------------------------------------------------
        public void setProcent(double aP)
        {
            Procent = aP;
            //   double dataSize = Data.size();
            //SizeACor = dataSize*Procent;
            //   if( (Procent < 0)||(Procent > 1) ){
            //      ShowMessage("qModelACor::setProcent задан неверно");
            //      Procent = 0.5;   }
        }
        //---------------------------------------------------------------------------
        public int getIndex()
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int size = tVctr.Count;
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                if ((tSgmP[size - 1 - i] < tVctr[size - 1 - i]) || (tVctr[size - 1 - i] < tSgmM[size - 1 - i]))
                {
                    index = size - 1 - i;
                    break;
                }
            }
            return index;
        }
        //---------------------------------------------------------------------------
        public int getIndex(int aNum)
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int index = 0;
            int count = 0;
            int size = tVctr.Count;
            for (int i = 1; i < size; i++)
            {
                if ((tSgmP[i] < tVctr[i]) || (tVctr[i] < tSgmM[i]))
                {
                    index = i;
                }
                if ((tSgmP[i] > tVctr[i]) && (tVctr[i] > tSgmM[i]))
                {
                    count++;
                }
                if (count == aNum)
                {
                    break;
                }
            }
            return index;
        }

        private qACorr ACorr = new qACorr();
        private double Koeff; // множитель ошибки
        private double Procent; // процент длины ряда - длина результата
        //int SizeACor;                 // длина результата - число коэффициентов
        //int getPorydACor();          // вычислить порядок автокорреляции
        // вычисляется одним изметодов по настройке глядя
    }
    //---------------------------------------------------------------------------
    //---------------------------------------------------------------------------
    //#include "UCorrel.h"
    //---------------------------------------------------------------------------
    public partial class qPCorr : qACorr
    {
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qPCorr()
        {
            this.KSgm = 1;
        }
        //---------------------------------------------------------------------------
        public qPCorr(qPCorr aPCorr)
        {
            this.set(aPCorr);
        }
        //   ~qPCorr(){;}
        //   qPCorr(qData &aData); //вычисляет из входа
        //set

        /// <summary>
        /// копирует
        /// </summary>
        /// <param name="aPCorr"></param>
        public void set(qPCorr aPCorr)
        {
            this.Acorr = aPCorr.Acorr;
            this.AcorrSgm = aPCorr.AcorrSgm;
            this.AcorrSgmM = aPCorr.AcorrSgmM;
            this.KSgm = aPCorr.KSgm;
            this.F = aPCorr.F;
        }

        //---------------------------------------------------------------------------
        public override void set(qData aData, double k)
        {
            this.Acorr.set(aData.clcACorrelation());
            this.F = this.Acorr.clcPCorrelation(this.Acorr); //присвоил Acorr = Pcorr
            this.setSgm(k);
        }
        //---------------------------------------------------------------------------
        public override void setSgm(double k)
        {
            //   if(KSgm != k){
            KSgm = (int)k;
            this.AcorrSgm.set(this.Acorr.clcPCorrSgm(k));
            this.AcorrSgmM.set(this.Acorr.clcPCorrSgmMin(k));
            //   }
        }

        //---------------------------------------------------------------------------
        public override void set(qData aData, double k, double procent)
        {
            this.Acorr.set(aData.clcACorrelation(procent));
            // в Ф матрицу в Acorr результат
            this.F = this.Acorr.clcPCorrelation(this.Acorr); //присвоил Acorr = Pcorr
            this.setSgm(k, procent);
        }
        //---------------------------------------------------------------------------
        public override void setSgm(double k, double procent)
        {
            //   if(KSgm != k){
            KSgm = (int)k;
            this.AcorrSgm.set(this.Acorr.clcPCorrSgm(k, procent));
            this.AcorrSgmM.set(this.Acorr.clcPCorrSgmMin(k, procent));
            //   }
        }

        //---------------------------------------------------------------------------
        public qData getF(int k)
        {
            qData tData = new qData();
            tData.set(F[k]);
            return tData;
        }
        //---------------------------------------------------------------------------
        public List<List<double>> getFAll()
        {
            return F;
        }

        //---------------------------------------------------------------------------

        private List<List<double>> F = new List<List<double>>();
    }
    //---------------------------------------------------------------------------

    //#include "UPCorr.h"

    //---------------------------------------------------------------------------
    public class qModelPCor : qModel
    {
        public qModelPCor()
        {
            ModelName = "Частная автокорреляционная функция.";
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qModelPCor(qModelPCor aModelPCor)
        {
            if (this != aModelPCor)
            {
                //this = aModelPCor;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setPCor(ref qModelPCor aModelPCor)
        {
            if (this != aModelPCor)
            {
                //this = aModelPCor;
                throw new System.NotImplementedException();
            }
        }
        //++++++++++++++++++++++++++
        //void clear(void);
        //void set(qData aData);
        //qData getA(void);            // вернет Sgm-
        //qData getPrognos(void){;}    // вернет Sgm+
        public override void mkModel(int Metod)
        {
            throw new System.NotImplementedException();
        }
        public new void mkPrognos(int NumPrognos)
        {
            throw new System.NotImplementedException();
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void clcPCor()
        {
            PCorr.set(this.Data, Koeff, Procent);
            F.set(PCorr.getFAll());
            this.Data.set(PCorr.get());
            this.Prognos.set(PCorr.getAcorrSgm());
            this.A.set(PCorr.getAcorrSgmM());
            PCorr.clear();
        }
        //---------------------------------------------------------------------------
        public void setKoeff(double aK)
        {
            Koeff = aK;
            //   if( (Koeff < 0) ){
            //      ShowMessage("qModelPCor::setKoeff задан неверно");
            //      Koeff = 1;   }
        }
        //---------------------------------------------------------------------------
        public void setProcent(double aP)
        {
            Procent = aP;
            //   double dataSize = Data.size();
            //   SizePCor = dataSize*Procent;
            //   if( (Procent < 0)||(Procent > 1) ){
            //      ShowMessage("qModelPCor::setProcent задан неверно");
            //      Procent = 0.5;   }
        }
        //---------------------------------------------------------------------------
        public int getIndex()
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int size = tVctr.Count;
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                if ((tSgmP[size - 1 - i] < tVctr[size - 1 - i]) || (tVctr[size - 1 - i] < tSgmM[size - 1 - i]))
                {
                    index = size - 1 - i;
                    break;
                }
            }
            return index;
        }
        //---------------------------------------------------------------------------
        public int getIndex(int aNum)
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int index = 0;
            int count = 0;
            int size = tVctr.Count;
            for (int i = 1; i < size; i++)
            {
                if ((tSgmP[i] < tVctr[i]) || (tVctr[i] < tSgmM[i]))
                {
                    index = i;
                }
                if ((tSgmP[i] > tVctr[i]) && (tVctr[i] > tSgmM[i]))
                {
                    count++;
                }
                if (count == aNum)
                {
                    break;
                }
            }
            return index;
        }

        private qPCorr PCorr = new qPCorr();
        private double Koeff; // множитель ошибки
        private double Procent; // процент длины ряда - длина результата
        /// <summary>
        /// Таблица коэффициентов Фij
        /// </summary>
        private qMData F = new qMData();
        //   int SizePCor;                 // длина результата - число коэффициентов
        //int getPorydPCor();          // вычислить порядок автокорреляции
        // вычисляется одним изметодов по настройке глядя
    }
    //---------------------------------------------------------------------------

    //#include "UCorrel.h"
    //#include "UMatrix.h"

    //---------------------------------------------------------------------------
    public class qModelMCor : qModel
    {
        public qModelMCor()
        {
            ModelName = "Взаимная kорреляционная функция.";
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qModelMCor(qModelMCor aModelMCor)
        {
            if (this != aModelMCor)
            {
                //this = aModelMCor;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setMCor(ref qModelMCor aModelMCor)
        {
            if (this != aModelMCor)
            {
                //this = aModelMCor;
                throw new System.NotImplementedException();
            }
        }
        //++++++++++++++++++++++++++
        //void clear(void);
        //void set(qData aData);       // не используется  
        //qData getA(void);            // вернет Sgm-
        //qData getPrognos(void){;}    // вернет Sgm+
        public override void mkModel(int Metod)
        {
            throw new System.NotImplementedException();
        }
        public new void mkPrognos(int NumPrognos)
        {
            throw new System.NotImplementedException();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void clcMCor()
        {
            qData tData = new qData();
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: tData = tData.clcMCorelation(Procent, X, Y);
            tData = tData.clcMCorelation(Procent, new qData(X), new qData(Y));
            List<double> tVctr = new List<double>();
            tVctr = tData.get();
            this.Data.set(tVctr);

            qACorr ACorr = new qACorr();
            ACorr.set(X, Koeff, Procent);
            this.Prognos.set(ACorr.getAcorrSgm());
            this.A.set(ACorr.getAcorrSgmM());
            ACorr.clear();
        }
        //---------------------------------------------------------------------------
        public void clcMCov()
        {
            qData tData = new qData();
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: tData = tData.clcMCovariation(Procent, X, Y);
            tData = tData.clcMCovariation(Procent, new qData(X), new qData(Y));
            List<double> tVctr = new List<double>();
            tVctr = tData.get();
            this.Data.set(tVctr);

            //   qACorr ACorr;
            //   ACorr.set( X, Koeff, Procent);
            //   this->Prognos.set(ACorr.getAcorrSgm());
            //   this->A.set(ACorr.getAcorrSgmM());
            //   ACorr.clear();
        }
        //---------------------------------------------------------------------------
        public void setKoeff(double aK)
        {
            Koeff = aK;
            //   if( (Koeff < 0) ){
            //      ShowMessage("qModelMCor::setKoeff задан неверно");
            //      Koeff = 1;   }
        }
        //---------------------------------------------------------------------------
        public void setProcent(double aP)
        {
            Procent = aP;
            //   double dataSize = Data.size();
            //SizeMCor = dataSize*Procent;
            //   if( (Procent < 0)||(Procent > 1) ){
            //      ShowMessage("qModelMCor::setProcent задан неверно");
            //      Procent = 0.5;   }
        }
        //---------------------------------------------------------------------------
        public void setMCor(qData aX, qData aY)
        {
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: X = aX;
            //X.CopyFrom(aX);
            X.set(aX);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: Y = aY;
            //Y.CopyFrom(aY);
            Y.set(aY);
        }
        //   qData getSgm1(void){return sgmXY1;}
        //   qData getSgm2(void){return sgmXY2;}
        //---------------------------------------------------------------------------
        public int getIndex()
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int size = tVctr.Count;
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                if ((tSgmP[size - 1 - i] < tVctr[size - 1 - i]) || (tVctr[size - 1 - i] < tSgmM[size - 1 - i]))
                {
                    index = size - 1 - i;
                    break;
                }
            }
            return index;
        }
        //---------------------------------------------------------------------------
        public int getIndex(int aNum)
        {
            List<double> tVctr = new List<double>();
            List<double> tSgmP = new List<double>();
            List<double> tSgmM = new List<double>();
            tVctr = Data.get();
            tSgmP = Prognos.get();
            tSgmM = A.get();
            int index = 0;
            int count = 0;
            int size = tVctr.Count;
            for (int i = 1; i < size; i++)
            {
                if ((tSgmP[i] < tVctr[i]) || (tVctr[i] < tSgmM[i]))
                {
                    index = i;
                }
                if ((tSgmP[i] > tVctr[i]) && (tVctr[i] > tSgmM[i]))
                {
                    count++;
                }
                if (count == aNum)
                {
                    break;
                }
            }
            return index;
        }


        private qData X = new qData();
        private qData Y = new qData();
        private qData sgmXY1 = new qData();
        private qData sgmXY2 = new qData();

        private double Koeff; // множитель ошибки
        private double Procent; // процент длины ряда - длина результата
        //int SizeMCor;                 // длина результата - число коэффициентов
        //int getPorydMCor();          // вычислить порядок автокорреляции
        // вычисляется одним изметодов по настройке глядя
    }
}