using System.Collections.Generic;

namespace TimeSeries
{
    public class qModelExp : qModel
    {
        public qModelExp()
        {
            ModelName = "Ёкспоненциальное сглаживание.";
            ModelNum = 5;
            ModelType = 2;

            Betta = 0.3;
            Poryd = 0;
            Accuracy = 0.1;
        }
        //---------------------------------------------------------------------------
        public qModelExp(qModelExp aModelExp)
        {
            if (this != aModelExp)
            {
                //this = aModelExp;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setExp(ref qModelExp aModelExp)
        {
            if (this != aModelExp)
            {
                //this = aModelExp;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setBetta(double aBetta)
        {
            Betta = aBetta;
            if ((Betta < 0) || (Betta > 1))
            {
                Betta = 0.3;
                throw new System.ApplicationException("if((Betta < 0)||(Betta > 1)) qModelExp::setBetta Betta задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        public void setAccuracy(double aAccuracy)
        {
            Accuracy = aAccuracy;
            if ((Accuracy < 0) || (Accuracy > 0.1))
            {
                Accuracy = 0.1;
                throw new System.ApplicationException("if((Accuracy < 0)||(Accuracy > 0.1)) qModelExp::setAccuracy Accuracy задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        public void setPoryd(int aPoryd)
        {
            Poryd = aPoryd;
            if (Poryd > 2)
            {
                Poryd = 0;
                throw new System.ApplicationException("if(Poryd > 2) qModelExp::setPoryd пор€док задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        public double getBetta()
        {
            return Betta;
        }
        //---------------------------------------------------------------------------
        public int getPoryd()
        {
            return Poryd;
        }
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelExp tModel;
                tModel = new qModelExp();
                return tModel;
            }
            catch
            {
                throw new System.ApplicationException("Could not allocate. Bye ...");
                return null;
            }
        }
        //---------------------------------------------------------------------------
        public new qModel clonModel()
        {
            qModelExp tModel;
            tModel = newModel() as qModelExp;
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
            int StPr = getNumStPr();
            clcExp(StPr);
        }

        //---------------------------------------------------------------------------

        public void clcExp0(int StPr)
        {
            List<double> VData = new List<double>();
            VData = Data.get();
            int DataSize = 0;
            DataSize = VData.Count;
            int PrognosSize = this.NumPointPrognos + DataSize;
            List<double> VPrognos = new List<double>(PrognosSize);
            VPrognos[0] = VData[0];

            int MaxI = DataSize;
            int MinI = 1;
            if (StPr > 0)
            {
                if (MinI < StPr)
                {
                    MinI = StPr;
                }
                if (MinI >= MaxI)
                {
                    MinI = MaxI - 1;
                }
            }
            for (int i = 0; i < MinI; i++)
            {
                VPrognos[i] = VData[i];
            }

            for (int i = MinI; i < DataSize; i++)
            {
                //      VPrognos[i] = ( Betta )*VData[i-1] + ( 1-Betta )*VPrognos[i-1];
                VPrognos[i] = (1 - Betta) * VData[i - 1] + (Betta) * VPrognos[i - 1];
            }
            if (NumPointPrognos > 0)
            {
                //      VPrognos[DataSize] = ( Betta )*VData[DataSize-1] + ( 1-Betta )*VPrognos[DataSize-1];
                VPrognos[DataSize] = (1 - Betta) * VData[DataSize - 1] + (Betta) * VPrognos[DataSize - 1];
            }
            for (int i = 1; i < NumPointPrognos; i++)
            {
                int index;
                index = DataSize + i;
                //      VPrognos[index] = ( Betta )*VPrognos[index-1] + ( 1-Betta )*VPrognos[index-1];
                VPrognos[index] = (1 - Betta) * VPrognos[index - 1] + (Betta) * VPrognos[index - 1];
            }
            //записать рез.
            Prognos.set(VPrognos);
        }
        //---------------------------------------------------------------------------
        public void clcExp1(int StPr)
        {
            List<double> VData = new List<double>();
            VData = Data.get();
            int DataSize = 0;
            DataSize = VData.Count;
            int PrognosSize = this.NumPointPrognos + DataSize;
            List<double> VPrognos = new List<double>(PrognosSize);
            List<double> A0 = new List<double>();
            List<double> A1 = new List<double>();
            A0 = new List<double>(PrognosSize);
            A1 = new List<double>(PrognosSize);

            int MaxI = DataSize;
            int MinI = 1;
            if (StPr > 0)
            {
                if (MinI < StPr)
                {
                    MinI = StPr;
                }
                if (MinI >= MaxI)
                {
                    MinI = MaxI - 1;
                }
            }
            for (int i = 0; i < MinI; i++)
            {
                VPrognos[i] = VData[i];
            }
            A0[MinI - 1] = VData[MinI - 1];
            //VPrognos.at(0) = VData.at(0);
            //A0.at(0) = VData.at(0);
            for (int i = MinI; i < DataSize; i++)
            {
                A0[i] = (1 - Betta * Betta) * VData[i - 1] + (Betta * Betta) * A0[i - 1] + (Betta * Betta) * A1[i - 1];
                A1[i] = (1 - Betta) * (1 - Betta) * VData[i - 1] - (1 - Betta) * (1 - Betta) * A0[i - 1] + A1[i - 1] - (1 - Betta) * (1 - Betta) * A1[i - 1];
                VPrognos[i] = A0[i] + A1[i];
            }

            if (NumPointPrognos > 0)
            {
                A0[DataSize] = (1 - Betta * Betta) * VData[DataSize - 1] + (Betta * Betta) * A0[DataSize - 1] + (Betta * Betta) * A1[DataSize - 1];
                A1[DataSize] = (1 - Betta) * (1 - Betta) * VData[DataSize - 1] - (1 - Betta) * (1 - Betta) * A0[DataSize - 1] + A1[DataSize - 1] - (1 - Betta) * (1 - Betta) * A1[DataSize - 1];
                VPrognos[DataSize] = A0[DataSize] + A1[DataSize];
            }
            for (int i = 1; i < NumPointPrognos; i++)
            {
                int index;
                index = DataSize + i;
                //-----------------внимание прогноз от прогноза
                A0[index] = (1 - Betta * Betta) * VPrognos[index - 1] + (Betta * Betta) * A0[index - 1] + (Betta * Betta) * A1[index - 1];
                A1[index] = (1 - Betta) * (1 - Betta) * VPrognos[index - 1] - (1 - Betta) * (1 - Betta) * A0[index - 1] + A1[index - 1] - (1 - Betta) * (1 - Betta) * A1[index - 1];
                VPrognos[index] = A0[index] + A1[index];
                //-----------------
            }
            //записать рез.
            Prognos.set(VPrognos);
        }
        //---------------------------------------------------------------------------
        public void clcExp2(int StPr)
        {
            List<double> VData = new List<double>();
            VData = Data.get();
            int DataSize = 0;
            DataSize = VData.Count;
            int PrognosSize = this.NumPointPrognos + DataSize;
            List<double> VPrognos = new List<double>(PrognosSize);
            List<double> A0 = new List<double>();
            List<double> A1 = new List<double>();
            List<double> A2 = new List<double>();
            A0 = new List<double>(PrognosSize);
            A1 = new List<double>(PrognosSize);
            A2 = new List<double>(PrognosSize);
            List<double> VErr = new List<double>(PrognosSize);

            int MaxI = DataSize;
            int MinI = 1;
            if (StPr > 0)
            {
                if (MinI < StPr)
                {
                    MinI = StPr;
                }
                if (MinI >= MaxI)
                {
                    MinI = MaxI - 1;
                }
            }
            for (int i = 0; i < MinI; i++)
            {
                VPrognos[i] = VData[i];
            }
            A0[MinI - 1] = VData[MinI - 1] * 0.85;
            //VPrognos.at(0) = VData.at(0);
            //A0.at(0) = VData.at(0)*0.85;
            for (int i = MinI; i < DataSize; i++)
            {
                VErr[i - 1] = VData[i - 1] - (A0[i - 1] + A1[i - 1] + A2[i - 1]);
                A0[i] = A0[i - 1] + A1[i - 1] + A2[i - 1] + VErr[i - 1] * (1 - Betta * Betta * Betta);
                A1[i] = A1[i - 1] + 2 * A2[i - 1] + VErr[i - 1] * (1 - Betta) * (1 - Betta * Betta) * 1.5;
                A2[i] = A2[i - 1] + VErr[i - 1] * (1 - Betta) * (1 - Betta) * (1 - Betta) * 0.5;
                VPrognos[i] = A0[i] + A1[i] + A2[i];
            }

            if (NumPointPrognos > 0)
            {
                VErr[DataSize - 1] = VData[DataSize - 1] - (A0[DataSize - 1] + A1[DataSize - 1] + A2[DataSize - 1]);
                A0[DataSize] = A0[DataSize - 1] + A1[DataSize - 1] + A2[DataSize - 1] + VErr[DataSize - 1] * (1 - Betta * Betta * Betta);
                A1[DataSize] = A1[DataSize - 1] + 2 * A2[DataSize - 1] + VErr[DataSize - 1] * (1 - Betta) * (1 - Betta * Betta) * 1.5;
                A2[DataSize] = A2[DataSize - 1] + VErr[DataSize - 1] * (1 - Betta) * (1 - Betta) * (1 - Betta) * 0.5;
                VPrognos[DataSize] = A0[DataSize] + A1[DataSize] + A2[DataSize];
            }
            for (int i = 1; i < NumPointPrognos; i++)
            {
                int index;
                index = DataSize + i;
                //-----------------внимание прогноз от прогноза
                VErr[index - 1] = VPrognos[index - 1] - (A0[index - 1] + A1[index - 1] + A2[index - 1]);
                A0[index] = A0[index - 1] + A1[index - 1] + A2[index - 1] + VErr[index - 1] * (1 - Betta * Betta * Betta);
                A1[index] = A1[index - 1] + 2 * A2[index - 1] + VErr[index - 1] * (1 - Betta) * (1 - Betta * Betta) * 1.5;
                A2[index] = A2[index - 1] + VErr[index - 1] * (1 - Betta) * (1 - Betta) * (1 - Betta) * 0.5;
                VPrognos[index] = A0[index] + A1[index] + A2[index];
                //-----------------
            }
            //записать рез.
            Prognos.set(VPrognos);
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// вычислить дл€ текущего пор€дка
        /// </summary>
        /// <param name="StPr"></param>
        public void clcExp(int StPr)
        {
            if (Poryd == 2)
            {
                clcExp2(StPr);
            }
            else
                if (Poryd == 1)
                {
                    clcExp1(StPr);
                }
                else
                {
                    clcExp0(StPr);
                }
            this.mkA();
            State = true;
        }
        //   State = true;  
        //---------------------------------------------------------------------------

        /// <summary>
        /// подобрать лучшее бетта дл€ текущго пор€дка
        /// </summary>
        public void clcBestBetta()
        {
            List<double> tmpVcrt = new List<double>();
            double BestErr = double.MaxValue;
            double Err = 0;
            double BestBetta = 0;
            for (int i = 1; i < 10; i++)
            {
                Betta = ((double)i) / 10;
                this.clcExp(0);
                //      this->mkA();
                tmpVcrt = A.get();
                Err = A.clcAmountSquare(ref tmpVcrt);
                if (BestErr > Err)
                {
                    BestErr = Err;
                    BestBetta = Betta;
                }
            }
            if (Accuracy < 0.1)
            {
                double MinI = BestBetta - 0.1;
                double MaxI = BestBetta + 0.1;
                if (MinI < 0)
                {
                    MinI = 0;
                }
                if (MaxI > 1)
                {
                    MaxI = 1;
                }
                for (double i = MinI; i < MaxI; i += 0.01)
                {
                    Betta = i;
                    this.clcExp(0);
                    //       this->mkA();
                    tmpVcrt = A.get();
                    Err = A.clcAmountSquare(ref tmpVcrt);
                    if (BestErr > Err)
                    {
                        BestErr = Err;
                        BestBetta = Betta;
                    }
                }
            }
            if (Accuracy < 0.01)
            {
                double MinI = BestBetta - 0.01;
                double MaxI = BestBetta + 0.01;
                if (MinI < 0)
                {
                    MinI = 0;
                }
                if (MaxI > 1)
                {
                    MaxI = 1;
                }
                for (double i = MinI; i < MaxI; i += 0.001)
                {
                    Betta = i;
                    this.clcExp(0);
                    //       this->mkA();
                    tmpVcrt = A.get();
                    Err = A.clcAmountSquare(ref tmpVcrt);
                    if (BestErr > Err)
                    {
                        BestErr = Err;
                        BestBetta = Betta;
                    }
                }
            }
            Betta = BestBetta;
            clcExp(0);
        }
        
        /// <summary>
        /// коэффициент экспоненциального сглаживани€
        /// </summary>
        private double Betta;
        /// <summary>
        /// текущий пор€дк
        /// </summary>
        private int Poryd;
        /// <summary>
        /// точность подобора бетта дл€ текущго пор€дка
        /// </summary>
        private double Accuracy;
    }
}