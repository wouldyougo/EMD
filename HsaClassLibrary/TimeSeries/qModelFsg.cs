using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries
{
    /// <summary>
    ///   void setPoryd(unsigned aPoryd);     // устанавливает число гармоник
    ///   unsigned getPoryd(void);            // читает число гармоник
    ///   void clcBestPoryd(void);            // подбор наилучшего числа гармоник
    /// </summary>
    public class qModelFsg : qModel
    {
        public qModelFsg()
        {
            ModelName = "Фурье сглаживание.";
            ModelNum = 3;
            ModelType = 1;
        }
        //---------------------------------------------------------------------------
        public qModelFsg(qModelFsg aModelFsg)
        {
            //if(this != aModelFsg)
            {
                // this = aModelFsg;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setFsg(ref qModelFsg aModelFsg)
        {
            //if(this != aModelFsg)
            {
                // this = aModelFsg;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// устанавливает число гармоник 
        /// </summary>
        /// <param name="aPoryd">число гармоник </param>
        public void setPoryd(int aPoryd)
        {
            Poryd = aPoryd + 1;
            if ((Poryd > 17))
            {
                Poryd = 1;
                throw new System.ApplicationException("qModelFsg::setPoryd порядок задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// читает число гармоник
        /// </summary>
        /// <returns>число гармоник</returns>
        public int getPoryd()
        {
            return Poryd;
        }

        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelFsg tModel;
                tModel = new qModelFsg();
                return tModel;
            }
            catch
            {
                throw new System.ApplicationException("Could not allocate. Bye ...");
            }
        }
        //---------------------------------------------------------------------------
        public new qModel clonModel()
        {
            qModelFsg tModel;
            tModel = newModel() as qModelFsg;
            tModel = (this);
            return tModel;
        }

        //void clear(void);
        //void set(qData aData);
        //qData getA(void);
        //qData getPrognos(void){;}
        public override void mkModel(int Metod) // не использую////////////
        {
            ;
        }
        //---------------------------------------------------------------------------
        public new void mkPrognos(int NumPrognos)
        {
            setNumPoPr(NumPrognos);
            clcFsg(true);
        }
        //---------------------------------------------------------------------------
        public void clcFsgParam()
        {
            //   std::vector<double> kA;
            //   std::vector<double> kB;
            double PI = 3.14159265358979323846264338;
            List<double> data = new List<double>();
            int dataSize;
            dataSize = Data.size();
            data = Data.get();
            kA = new List<double>(Poryd);
            kB = new List<double>(Poryd);
            double Tmp;
            double Ccos;
            double Csin;
            double SumA = 0;
            double SumB = 0;
            for (int i = 0; i < Poryd; i++)
            {
                for (int t = 0; t < dataSize; t++)
                {
                    Tmp = (2 * PI * i * t);
                    Tmp = Tmp / ((double)dataSize);
                    Ccos = Math.Cos(Tmp);
                    Csin = Math.Sin(Tmp);
                    SumA += data[t] * Ccos;
                    SumB += data[t] * Csin;
                }
                kA[i] = (SumA * 2) / dataSize;
                kB[i] = (SumB * 2) / dataSize;
                SumA = 0;
                SumB = 0;
            }
            kA[0] = kA[0] / 2;
            State = true;
            //
            //      kA[i] = (SumA);
            //      kB[i] = (SumB);
            //      SumA = 0;
            //      SumB = 0;
            //   }
            //
        }
        //   State = true;  
        //---------------------------------------------------------------------------
        public void clcFsg(bool prognos)
        {
            double PI = 3.14159265358979323846264338;

            List<double> VData = new List<double>();
            List<double> VPrognos = new List<double>();
            int dataSize = 0;
            int PrognosSize = 0;

            clcFsgParam();

            VData = this.Data.get();
            dataSize = VData.Count;
            PrognosSize = dataSize;
            if (prognos)
            {
                PrognosSize += this.NumPointPrognos;
            }
            VPrognos = new List<double>(PrognosSize);

            double Tmp;
            double Ccos;
            double Csin;
            double SumA = 0;
            double SumB = 0;
            for (int t = 0; t < PrognosSize; t++)
            {
                for (int i = 0; i < Poryd; i++)
                {
                    Tmp = (2 * PI * i * t);
                    Tmp = Tmp / ((double)dataSize);
                    Ccos = Math.Cos(Tmp);
                    Csin = Math.Sin(Tmp);
                    SumA += kA[i] * Ccos;
                    SumB += kB[i] * Csin;
                }
                VPrognos[t] = SumA + SumB;
                SumA = 0;
                SumB = 0;
            }
            Prognos.set(VPrognos);
            mkA();
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// Подбор числа гармоник
        /// Устанавливает Наилучший порядок
        /// </summary>
        /// <param name="prognos">флаг делать прогноз</param>
        public void clcBest(bool prognos)
        {
            List<double> tmpVcrt = new List<double>();
            double BestErr = double.MaxValue;
            double Err = 0;
            int BestPor = 0;
            int MaxI = 10;
            for (int i = 0; i < MaxI; i++)
            {
                setPoryd(i);
                clcFsg(false);
                tmpVcrt = A.get();
                Err = A.clcAmountSquare(ref tmpVcrt);
                if (BestErr > Err)
                {
                    BestErr = Err;
                    BestPor = i;
                }
              //out << "Текущий порядок:";   out << "\t";  out << i;  out << "\n";
              //out << "Сумма квадратов отклонений:";   out << "\t";  out << Err;  out << "\n";
           }
           //out << "Наилучший порядок:";   out << "\t";  out << BestPor;  out << "\n";
           //out << "Минимальная сумма квадратов отклонений:";   out << "\t";  out << BestErr;  out << "\n";
            setPoryd(BestPor);
            clcFsg(prognos);
        }
        //---------------------------------------------------------------------------
        public qData getParamA()
        {
            qData tData = new qData();
            tData.set(kA);
            return tData;
        }
        //---------------------------------------------------------------------------
        public qData getParamB()
        {
            qData tData = new qData();
            tData.set(kB);
            return tData;
        }
        private int Poryd;
        private List<double> kA = new List<double>(); //коэффиц А
        private List<double> kB = new List<double>(); //коэффиц А
    }
}
