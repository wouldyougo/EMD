using System.Collections.Generic;

namespace TimeSeries
{
    public class qModelPol : qModel
    {
        public qModelPol()
        {
            ModelName = "Полиномиальная модель.";
            ModelNum = 2;
            ModelType = 1;

            Poryd = 0;
        }
        //---------------------------------------------------------------------------
        public qModelPol(qModelPol aModelPol)
        {
            if (this != aModelPol)
            {
                //this = aModelPol;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setPol(ref qModelPol aModelPol)
        {
            if (this != aModelPol)
            {
                //this = aModelPol;
                throw new System.NotImplementedException();
            }
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void setPoryd(int aPoryd)
        {
            Poryd = aPoryd;
            if ((Poryd > 16))
            {
                Poryd = 1;
                throw new System.ApplicationException("qModelPol::setPoryd порядок задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// порядок модели
        /// </summary>
        /// <returns></returns>
        public int getPoryd()
        {
            return Poryd;
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// Параметры модели
        /// </summary>
        /// <returns></returns>
        public qData getParam()
        {
            qData tData = new qData();
            tData.set(Param);
            return tData;
        }
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelPol tModel;
                tModel = new qModelPol();
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
            qModelPol tModel;
            tModel = newModel() as qModelPol;
            tModel = (this);
            return tModel;
        }
        //void clear(void);
        //void set(qData aData);
        //qData getA(void);
        //qData getPrognos(void){;}

        /// <summary>
        /// не использую
        /// </summary>
        /// <param name="Metod"></param>
        public override void mkModel(int Metod) //
        {
            ;
        }
        //---------------------------------------------------------------------------
        public new void mkPrognos(int NumPrognos)
        {
            setNumPoPr(NumPrognos);
            clcPol(true);
        }
        //void mkDataPrognos(void); 
        //---------------------------------------------------------------------------
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        public void clcPol(bool prognos)
        {
            List<double> VData = new List<double>();
            List<double> VPrognos = new List<double>();
            int DataSize = 0;
            int PrognosSize = 0;

            clcPolParam();

            VData = this.Data.get();
            DataSize = VData.Count;
            PrognosSize = DataSize;
            if (prognos)
            {
                PrognosSize += this.NumPointPrognos;
            }
            //PrognosSize = this->NumPointPrognos + DataSize;
            VPrognos = new List<double>(PrognosSize);

            for (int i = 0; i < PrognosSize; i++)
            {
                double summa = 0;
                for (int j = 0; j < Poryd + 1; j++)
                {
                    double multipl = 1;
                    for (int k = 0; k < j; k++)
                    {
                        multipl *= i;
                    }
                    summa += Param[j] * multipl;
                }
                VPrognos[i] = summa;
            }
            Prognos.set(VPrognos);
            mkA();
            State = true;
        }
        //   State = true;  
        //---------------------------------------------------------------------------
        public void clcBestPoryd(bool prognos)
        {
            List<double> tmpVcrt = new List<double>();
            double BestErr = double.MaxValue;
            double Err = 0;
            int BestPor = 0;
            int MaxI = 10;
            for (int i = 0; i < MaxI; i++)
            {
                setPoryd(i);
                clcPol(false);
                tmpVcrt = A.get();
                Err = A.clcAmountSquare(ref tmpVcrt);
                if (BestErr > Err)
                {
                    BestErr = Err;
                    BestPor = i;
                }
            }
            setPoryd(BestPor);
            clcPol(prognos);
        }
        private int Poryd;
        private List<double> Param = new List<double>(); //модель размерностью Poryd

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private void clcPolParam()
        {
            int Low = 0; // Минимальная степень полинома
            int Degree = Poryd; // Степень полинома
            int n; // Число пар значений
            int Code; // Код ошибки
            //   double arg[] = {5,15,30,45,50};	      // Массив абсцисс
            //   double data[] = {37,34,33,29,20};	   // Массив ординат
            //   double *coeff;	                        // Массив коэффициентов
            //   double err = 0;	                     // Ошибка
            //   double buf;	                           // Рабочая переменная

            List<double> arg = new List<double>();
            List<double> data = new List<double>();
            List<double> coeff = new List<double>(Degree + 1);
            List<double> VData = new List<double>();

            VData = this.Data.get();
            //data.AddRange(VData.GetEnumerator(),VData.end());
            data.AddRange(VData);
            arg = new List<double>(data.Count);
            int MaxI = arg.Count + 1;
            for (int i = 1; i < MaxI; i++)
            {
                arg[i - 1] = i;
            }
            n = data.Count;
            qMData tMtrx = new qMData();
            //Code = tMtrx.Polynomial(data[0], arg[0], Low, Degree, n, coeff[0]);
            Code = tMtrx.Polynomial(data, arg, Low, Degree, n, coeff);
            //throw new System.NotImplementedException();
            if ((Code == -1) || (Code == -2))
            {
                //ShowMessage("Ошибка при построении полинома");
                throw new System.ApplicationException("Ошибка при построении полинома");
            }
            //результат
            Param.AddRange(coeff);
        }
    }
}