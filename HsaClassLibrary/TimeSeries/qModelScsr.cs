using System.Collections.Generic;

namespace TimeSeries
{
    public class qTableSs
    {
        public bool getE()
        {
            return Exists;
        }
        public List<int> PointForPor = new List<int>();

        //void set(); // чтение файла СС

        private List<List<qMData>> MultiSs = new List<List<qMData>>();
        private bool Exists = false;

        public qTableSs()
        {
            PointForPor = new List<int>(5);
            PointForPor[0] = 3;
            PointForPor[1] = 5;
            PointForPor[2] = 5;
            PointForPor[3] = 7;
            PointForPor[4] = 7;
            set();
        }
        public void set()
        {
            /*
             ifstream @in = new ifstream("TableSs.dat");
            Exists = false;
            if(@in.is_open())
            {
               //      ShowMessage("TableSs.dat");
               MultiSs = new List< List<qMData > > (5, List<qMData > (21));
               qMData TmpMData = new qMData();
               List<List<double> > TmpMtrx = new List<List<double> >();
               string TmpStr;
               int NPoint;
               //--------------------------
               for(int i = 0; i < 5; i++)
               {
               @in >> TmpStr; //порядок;
               NPoint = PointForPor[i];
                  for(int j = NPoint-1; j < 21; j+=2)
                  {
                     @in >> TmpStr; //точек;
                     TmpMtrx = read(@in, j+1);
                     TmpMData = qMData(TmpMtrx);
                     MultiSs[i][j] = TmpMData;
                     //----------------------------
                     TmpMtrx = new List<List<double> > (10, List<double> (10,0));
                     TmpMData.set(TmpMtrx);
                     TmpMData = MultiSs[i][j];
                     TmpMtrx = TmpMData.get();
                  }
               }
               //--------------------------      
               Exists = true;
            }
            if(!Exists)
            {
               throw new System.ApplicationException("Не найден файл TableSs.dat");
               Exists = false;
            }
            @in.close();
             */
            throw new System.NotImplementedException();
        }
        /*
        public List<List<double> > read(ref std.ifstream @in, int NPoint)
        {
              double TmpValue;
              int ColCount;
              List<List<double> > TmpMtrx = new List<List<double> >();
              TmpMtrx = new List<List<double> > (NPoint +2, List<double> (NPoint +2,0));
	
              ColCount = (NPoint/2) + 2;
              for(int i = 0; i< ColCount; i++)
              {
                 @in >> TmpValue;
              }
              //--------------------------
              for(int i = 0; i < NPoint +2; i++)
              {
                 for(int j = 0; j< ColCount; j++)
                 {
                    @in >> TmpValue;
                    TmpMtrx[i][j] = TmpValue;
                 }
              }
              for(int j = 0; j < ColCount-1; j++)
              {
                 List<double> TmpVctr = new List<double>(NPoint);
                 for(int i = 0; i < NPoint; i++)
                 {
                    TmpVctr[NPoint-1-i] = TmpMtrx[i][j];
                 }
                 for(int i = 0; i < NPoint; i++)
                 {
                    TmpMtrx[i][NPoint+2-1-j] = TmpVctr[i];
                 }
                 TmpMtrx[NPoint][NPoint+2-1-j] = TmpMtrx[NPoint][j];
                 TmpMtrx[NPoint+1][NPoint+2-1-j] = TmpMtrx[NPoint+1][j];
              }
              //--------------------------
              return TmpMtrx;
        }
         */

        public bool get(int a, int b, ref qMData aTableSs)
        {
            //проверка соответствия параметров
            //да то ветнуть таблицу
            //иначе "по минимуму"
            try
            {
                //aTableSs = MultiSs[a-1].at(b-1);
                aTableSs = MultiSs[a - 1][b - 1];
                return true;
            }
            catch
            {
                aTableSs = MultiSs[0][2];
                return false;
            }
        }
    }
    public class qModelSs : qModel
    {
        /// <summary>
        /// // не использую
        /// </summary>
        /// <param name="Metod"></param>
        public override void mkModel(int Metod)
        {
            ;
        }
        private int Poryd;
        private int Point;
        private qMData TblSs = new qMData();

        public qModelSs()
        {
            ModelName = "Скользящее среднее.";
            ModelNum = 4;
            ModelType = 1;
        }
        public qModelSs(qModelSs aModelSs)
        {
            if (this != aModelSs)
            {
                //this = aModelSs;
                throw new System.NotImplementedException();
            }
        }
        public override qModel newModel()
        {
            try
            {
                qModelSs tModel;
                tModel = new qModelSs();
                return tModel;
            }
            catch
            {
                return null;
                throw new System.ApplicationException("Could not allocate. Bye ...");
            }
        }
        public override qModel clonModel()
        {
            qModelSs tModel;
            tModel = newModel() as qModelSs;
            tModel = (this);
            return tModel;
        }
        public qMData getTbl()
        {
            return TblSs;
        }
        public void setTbl(ref qMData aMData)
        {
            this.TblSs = aMData;
        }
        public void setSs(ref qModelSs aModelSs)
        {
            if (this != aModelSs)
            {
                //this = aModelSs;
                throw new System.NotImplementedException();
            }
        }
        public List<double> getVTblSs(int point)
        {
            List<List<double>> TblSs = new List<List<double>>();
            //TblSs = qModelSs.TblSs.get();
            TblSs = this.TblSs.get();
            List<double> VTblSs = new List<double>(TblSs.Count);
            for (int i = 0; i < TblSs.Count; i++)
            {
                VTblSs[i] = TblSs[i][point];
            }
            return VTblSs;
        }
        public double clcSsPoint(int point, List<double> tbl)
        {
            double Summa = 0;
            List<double> VData = new List<double>();
            VData = Data.get();
            int IMax = 0;
            IMax = tbl.Count - 2;
            for (int i = 0; i < IMax; i++)
            {
                Summa += VData[point + i] * tbl[i];
            }
            Summa /= tbl[IMax];
            return Summa;
        }
        public override void mkPrognos(int NumPrognos)
        {
            setNumPoPr(NumPrognos);
            clcSs(true);
        }
        public void clcSs(bool prognos)
        {
            //   std::vector<std::vector<double> > TblSs;
            //   TblSs = qModelSs::TblSs.get();
            List<double> VPrgns = new List<double>(Data.size());
            List<double> VTblSs = new List<double>();
            int NumPoint = 0;
            int CentrPoint = 0;

            NumPoint = TblSs.getCol(); // 5 0-4
            NumPoint -= 2; // 5-2 = 3
            CentrPoint = NumPoint / 2; // 3/2 = 1
            VTblSs = this.getVTblSs(CentrPoint + 1);

            for (int i = CentrPoint; i < Data.size() - CentrPoint; i++)
            {
                VPrgns[i] = clcSsPoint(i - CentrPoint, VTblSs);
            }
            for (int i = 0; i < CentrPoint; i++)
            {
                VTblSs = this.getVTblSs(i + 1);
                //      VPrgns[i] = clcSsPoint( i, VTblSs );
                VPrgns[i] = clcSsPoint(0, VTblSs);
            }
            for (int i = Data.size() - CentrPoint; i < Data.size(); i++)
            {
                int Num = 0;
                Num = Data.size();
                Num = 1 + NumPoint - (Data.size() - i);
                VTblSs = this.getVTblSs(Num);
                Num = Data.size() - NumPoint;
                VPrgns[i] = clcSsPoint(Num, VTblSs);
            }
            Prognos.set(VPrgns);

            mkA();
            State = true;
            if (prognos)
            {
                clcPrognosSs();
            }
            //   qData Data;            // данные для анализа
            //   qData Prognos;         // прогноз данных
            //   qData A;
        }
        public void clcPrognosSs()
        {
            int NumCol = 0;
            NumCol = TblSs.getCol();

            List<double> VTblSs = new List<double>();
            VTblSs = this.getVTblSs(NumCol - 1);

            int NumDataPoint = 0;
            NumDataPoint = VTblSs.Count - 2;

            List<double> VData = new List<double>();
            VData = Data.get();
            List<double> VPrognos = new List<double>();
            VPrognos = Prognos.get();

            List<double> VDataPoint = new List<double>(); //вектор для расчета
            for (int i = 0; i < NumPointPrognos; i++)
            {
                // формирование вектора расчета
                VDataPoint.Clear();
                int JMin = 0;
                int JMax = 0;
                JMin = 0;
                JMax = NumDataPoint - i;
                for (int j = JMin; j < JMax; j++)
                {
                    int index = 0;
                    index = Data.size() - (JMax - j);
                    VDataPoint.Add(VData[index]);
                }
                JMin = 0;
                JMax = NumDataPoint - VDataPoint.Count;
                for (int j = JMin; j < JMax; j++)
                {
                    int index = 0;
                    index = VPrognos.Count - (JMax - j);
                    VDataPoint.Add(VPrognos[index]);
                }
                //вычисление значения точки
                double Summa = 0;
                JMax = VTblSs.Count - 2;
                for (int j = 0; j < JMax; j++)
                {
                    Summa += VDataPoint[j] * VTblSs[j];
                }
                Summa /= VTblSs[JMax];
                VPrognos.Add(Summa);
            }
            Prognos.set(VPrognos);
        }
        public void clcBest(bool prognos)
        {
            /*
            List<double> tmpVcrt = new List<double>();
            double BestErr = double.MaxValue;
            double Err = 0;
            int BestPor = 0;
	
            setPoint(7);
            for(int i = 1; i < 5+1; i++)
            {
               setPoryd(i);
               MainForm.TblSs.get(Poryd, Point, TblSs);
               clcSs(0);
               tmpVcrt = A.get();
               Err = A.clcAmountSquare(ref tmpVcrt);
               if(BestErr > Err)
               {
                  BestErr = Err;
                  BestPor = i;
               }
            }
            setPoryd(BestPor);
            BestPor = 7; // число точек
            int MinI;
            MinI = MainForm.TblSs.PointForPor[Poryd-1];
            for(int i = MinI; i < 21+1; i+=2)
            {
               setPoint(i);
               MainForm.TblSs.get(Poryd, Point, TblSs);
               clcSs(0);
               tmpVcrt = A.get();
               Err = A.clcAmountSquare(ref tmpVcrt);
               if(BestErr > Err)
               {
                  BestErr = Err;
                  BestPor = i;
               }
            }
            setPoint(BestPor);
            clcSs(prognos);	
            */
        }
        public void setPoryd(int aPoryd)
        {
            Poryd = aPoryd;
            if ((Poryd > 5))
            {
                Poryd = 1;
                throw new System.ApplicationException("if((Poryd > 5)) qModelSs::setPoryd порядок задан неверно");
            }
        }
        public int getPoryd()
        {
            return Poryd;
        }
        public void setPoint(int aPoint)
        {
            Point = aPoint;
            if ((Point > 21))
            {
                Point = 7;
                throw new System.ApplicationException("if((Point > 21)) qModelSs::setPoryd порядок задан неверно");
            }
        }
        public int getPoint()
        {
            return Point;
        }
    }
}