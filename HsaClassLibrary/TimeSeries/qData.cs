using System.Collections.Generic;
using System;
using System.Diagnostics;

//throw new System.NotImplementedException();
//throw new System.ApplicationException();
namespace TimeSeries
{
    public class qData
    {
        public qData()
        {
            ;
        }
        //---------------------------------------------------------------------------
        public qData(int size)
        {
            Vctr = new List<double>(size);
        }
        //---------------------------------------------------------------------------
        public qData(qData aData)
        {
            //    int size = 0;
            //    size = aData.get().size();
            this.Vctr = aData.get();
            //    size = this->Vctr.size();
        }
        //---------------------------------------------------------------------------
        public qData(List<double> aVctr)
        {
            Vctr = aVctr;
        }

        //---------------------------------------------------------------------------
        public void clear()
        {
            Vctr.Clear();
        }
        //---------------------------------------------------------------------------
        public List<double> get()
        {
            return Vctr;
        }
        //---------------------------------------------------------------------------
        public int size()
        {
            return (int)Vctr.Count;
        }
        //copy
        //---------------------------------------------------------------------------

        public qData AddRange(qData aData)
        {
            List<double> tVctr1 = new List<double>();
            List<double> tVctr2 = new List<double>();
            tVctr1 = aData.get();
            tVctr2 = this.Vctr;
            tVctr2.AddRange(tVctr1);
            qData tData = new qData();
            tData.set(tVctr2);
            return tData;
        }
        //---------------------------------------------------------------------------
        //functions
        //Mat_Disp
        //---------------------------------------------------------------------------
        public double clcMid()
        {
            if (Vctr.Count != 0)
            {
                double M = 0;
                for (int i = 0; i < Vctr.Count; i++)
                {
                    M += Vctr[i];
                }
                M /= Vctr.Count;
                return M;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------
        public double clcVar()
        {
            if (Vctr.Count != 0)
            {
                double M;
                M = clcMid();
                double D = 0;

                for (int i = 0; i < Vctr.Count; i++)
                {
                    D += (Vctr[i] - M) * (Vctr[i] - M);
                }
                D /= Vctr.Count;
                return D;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------

        public double clcAmountSquare(ref List<double> a)
        {
            double tmp = 0;
            for (int i = 0; i < a.Count; i++)
            {
                tmp += a[i] * a[i];
            }
            return tmp;
        }
        //-------------------------------------

        //difference
        //-------------------------------------

        public qData clcDifference()
        {
            int size = this.Vctr.Count;
            List<double> tVctr = new List<double>(size);
            for (int i = 1; i < size; i++)
            {
                tVctr[i] = Vctr[i] - Vctr[i - 1];
            }
            tVctr[0] = Vctr[0];
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }
        //-------------------------------------
        public qData clcDifference(int d)
        {
            qData tData = new qData();
            tData = this;
            for (int i = 0; i < d; i++)
            {
                tData = tData.clcDifference();
            }
            return tData;
        }
        //-------------------------------------
        public qData clcDiffSeason(int s)
        {
            int size = this.Vctr.Count;
            List<double> tVctr = new List<double>(size);
            for (int i = s; i < size; i++)
            {
                tVctr[i] = Vctr[i] - Vctr[i - s];
            }
            for (int i = 0; i < s; i++)
            {
                tVctr[i] = Vctr[i];
            }
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }
        //-------------------------------------

        public List<List<double>> clcDiffMatrix(int d)
        {
            List<List<double>> tM = new List<List<double>>(d + 1);
            qData tData = new qData();
            tData = this;
            tM[0] = Vctr;
            for (int i = 1; i <= d; i++)
            {
                tData = tData.clcDifference();
                tM[i] = tData.get();
            } // tData.difference().get();
            return tM;
        }
        //-------------------------------------

        /// <summary>
        /// вычислит сезонную разность  
        /// </summary>
        /// <param name="s">s - порядок сезона</param>
        /// <param name="d">d - порядок разности</param>
        /// <returns></returns>
        public List<List<double>> clcDiffMatrix(int s, int d)
        {
            List<List<double>> tM = new List<List<double>>(d + 1);
            qData tData = new qData();
            tData = this;
            tM[0] = Vctr;
            for (int i = 1; i <= d; i++)
            {
                tData = tData.clcDiffSeason(s);
                tM[i] = tData.get();
            } // tData.difference().get();
            return tM;
        }
        //     
        //
        //-------------------------------------
        /// <summary>
        /// Автоковариационная функция
        /// </summary>
        /// <returns></returns>
        public qData clcACovariation()
        {
            List<double> tVctr = new List<double>(Vctr.Count); //вектор результата
            if (Vctr.Count != 0)
            {
                double M;
                M = this.clcMid();
                List<double> dif = new List<double>(Vctr); //отклонение от среднего
                int size = this.Vctr.Count;
                for (int i = 0; i < size; i++)
                {
                    dif[i] = dif[i] - M;
                }
                for (int k = 0; k != dif.Count - 1; k++) //по всем задержкам
                {
                    double C = 0;
                    for (int i = 0; i < size - k; i++)
                    {
                        C += dif[i] * dif[i + k];
                    }
                    C /= dif.Count;
                    tVctr[k] = C;
                }
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------
        /// <summary>
        /// Автокорреляционная функция
        /// </summary>
        /// <returns></returns>
        public qData clcACorrelation()
        {
            List<double> tVctr = new List<double>(); //вектор результата
            tVctr = this.clcACovariation().get();
            if (tVctr.Count > 0)
            {
                if (tVctr[0] != 0)
                {
                    //List<double>.iterator p = new List<double>.iterator();
                    double D = tVctr[0];
                    //for(p = tVctr.GetEnumerator(); p != tVctr.end(); p++)
                    int size = this.Vctr.Count;
                    for (int i = 0; i < size; i++)
                    {
                        tVctr[i] = tVctr[i] / D;
                    }
                }
                else
                {
                    tVctr = new List<double>(tVctr.Count);
                    tVctr[0] = 1;
                }
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        public qData clcACorrSgm(double k)
        {
            List<double> tVctr = new List<double>(); //результат
            tVctr = this.clcACorrVar().get();
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = (double)Math.Sqrt(k * tVctr[i]);
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        public qData clcACorrSgmMin(qData aACSgm)
        {
            List<double> tVctr = new List<double>(); //результат
            tVctr = aACSgm.get();
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = -tVctr[i];
            }
            qData tData = new qData(tVctr);
            return tData;
        }

        //-------------------------------------
        /// <summary>
        /// Частная автокорреляционная функция
        /// </summary>
        /// <returns></returns>
        public qData clcPCorrelation()
        {
            int size = 0;
            size = this.Vctr.Count;
            //   std::vector<std::vector<double> > F( size, std::vector<double> (size,0));
            List<List<double>> F = new List<List<double>>(size);
            for (int i = 1; i < size; i++)
            {
                F[i] = new List<double>(i + 1);
            }
            F[0] = new List<double>(size);

            F[1][1] = Vctr[1];
            F[0][1] = F[1][1];
            for (int p = 1; p < size - 1; p++)
            {
                double S1 = 0;
                double S2 = 0;
                for (int j = 1; j <= p; j++)
                {
                    S1 = S1 + F[p][j] * Vctr[p + 1 - j];
                }
                for (int j = 1; j <= p; j++)
                {
                    S2 = S2 + F[p][j] * Vctr[j];
                }
                F[p + 1][p + 1] = (Vctr[p + 1] - S1) / (1 - S2);
                for (int j = 1; j <= p; j++)
                {
                    F[p + 1][j] = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                    S1 = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                }
                F[0][p + 1] = F[p + 1][p + 1];
            }
            //qData tData = new qData( ref F[0] );
            List<double> tmp = F[0];
            qData tData = new qData(tmp);
            return tData;
        }
        //-------------------------------------
        /// <summary>
        /// Частная автокорреляционная функция
        /// </summary>
        /// <param name="Fkk"></param>
        /// <returns></returns>
        public List<List<double>> clcPCorrelation(qData Fkk)
        {
            int size = 0;
            size = this.Vctr.Count;
            List<List<double>> F = new List<List<double>>(size);
            for (int i = 1; i < size; i++)
            {
                F[i] = new List<double>(i + 1);
            }
            F[0] = new List<double>(size);

            F[1][1] = Vctr[1];
            F[0][1] = F[1][1];
            for (int p = 1; p < size - 1; p++)
            {
                double S1 = 0;
                double S2 = 0;
                for (int j = 1; j <= p; j++)
                {
                    S1 = S1 + F[p][j] * Vctr[p + 1 - j];
                }
                for (int j = 1; j <= p; j++)
                {
                    S2 = S2 + F[p][j] * Vctr[j];
                }
                F[p + 1][p + 1] = (Vctr[p + 1] - S1) / (1 - S2);
                for (int j = 1; j <= p; j++)
                {
                    F[p + 1][j] = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                    S1 = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                }
                F[0][p + 1] = F[p + 1][p + 1];
            }
            List<double> tmp = F[0];
            qData tData = new qData(tmp);
            Fkk.set(tData);
            return F;
        }
        //-------------------------------------

        /// <summary>
        /// Частная автокорреляционная функция
        /// </summary>
        /// <param name="a">похоже не нужен )</param>
        /// <returns></returns>
        public List<List<double>> clcPCorrelation(bool a)
        {
            int size = 0;
            size = this.Vctr.Count;
            //   std::vector<std::vector<double> > F( size, std::vector<double> (size,0));
            List<List<double>> F = new List<List<double>>(size);
            for (int i = 1; i < size; i++)
            {
                F[i] = new List<double>(i + 1);
            }
            F[0] = new List<double>(size);

            F[1][1] = Vctr[1];
            F[0][1] = F[1][1];
            for (int p = 1; p < size - 1; p++)
            {
                double S1 = 0;
                double S2 = 0;
                for (int j = 1; j <= p; j++)
                {
                    S1 = S1 + F[p][j] * Vctr[p + 1 - j];
                }
                for (int j = 1; j <= p; j++)
                {
                    S2 = S2 + F[p][j] * Vctr[j];
                }
                F[p + 1][p + 1] = (Vctr[p + 1] - S1) / (1 - S2);
                for (int j = 1; j <= p; j++)
                {
                    F[p + 1][j] = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                    S1 = F[p][j] - F[p + 1][p + 1] * F[p][p + 1 - j];
                }
                F[0][p + 1] = F[p + 1][p + 1];
            }
            return F; // qData tData(F[0]); Fkk = tData;
        }
        //-------------------------------------

        public qData clcPCorrSgm(double k)
        {
            int size = 0;
            size = this.Vctr.Count;
            double value = Math.Sqrt((double)(k) / (double)(size));
            //!!!!!!!!!!!!!!!
            //List<double> tVctr = new List<double>(size,value); //результат
            List<double> tVctr = new List<double>(size); //результат
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        public qData clcPCorrSgmMin(double k)
        {
            int size = 0;
            size = this.Vctr.Count;
            double value = -Math.Sqrt((double)(k) / (double)(size));
            //!!!!!!!!!!!111
            //List<double> tVctr = new List<double>(size,value); //результат
            List<double> tVctr = new List<double>(size);
            qData tData = new qData(tVctr);
            return tData;
        }
        //Вычисление параметров скользящего среднего
        //make
        //-------------------------------------
        public void mkDifference()
        {
            for (int i = 1; i < Vctr.Count; i++)
            {
                Vctr[i] = Vctr[i] - Vctr[i - 1];
            }
        }
        //-------------------------------------
        public void mkDifference(int s)
        {
            for (int i = 0; i < s; i++)
            {
                this.mkDifference();
            }
        }
        //-------------------------------------
        public void mkDiffSeason(int d)
        {
            for (int i = d; i < Vctr.Count; i++)
            {
                Vctr[i] = Vctr[i] - Vctr[i - d];
            }
            for (int i = 0; i < d; i++)
            {
                Vctr[i] = Vctr[i];
            }
        }

        //-------------------------------------
        public qData clcMCovariation(double procent, qData aX, qData aY)
        {
            int sizeX;
            int sizeY;
            int size;
            sizeX = aX.size();
            sizeY = aY.size();
            size = (sizeX <= sizeY) ? sizeX : sizeY;
            size = (int)(((double)size) * procent + 0.5);
            List<double> tVctr = new List<double>(size); //вектор результата
            double C = 0;
            for (int i = 0; i < size; i++)
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: C = clcMCovariation(i, aX, aY);
                C = clcMCovariation(i, new qData(aX), new qData(aY));
                tVctr[i] = C;
            }
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }
        //-------------------------------------
        public qData clcMCorelation(double procent, qData aX, qData aY)
        {
            int sizeX;
            int sizeY;
            int size;
            sizeX = aX.size();
            sizeY = aY.size();
            size = (sizeX <= sizeY) ? sizeX : sizeY;
            size = (int)(((double)size) * procent + 0.5);
            List<double> tVctr = new List<double>(size); //вектор результата
            double C = 0;
            for (int i = 0; i < size; i++)
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: C = clcMCorelation(i, aX, aY);
                C = clcMCorelation(i, new qData(aX), new qData(aY));
                tVctr[i] = C;
            }
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }

        //-------------------------------------

        public qData clcACorrelation(double procent)
        {
            List<double> tVctr = new List<double>(); //вектор результата
            tVctr = this.clcACovariation().get();
            int size;
            size = tVctr.Count;
            size = (int)(((double)size) * procent + 0.5);
            if (tVctr.Count > 0)
            {
                if (tVctr[0] != 0)
                {
                    double D = tVctr[0];
                    for (int i = 0; i < size; i++)
                    {
                        tVctr[i] = tVctr[i] / D;
                    }
                }
                else
                {
                    tVctr = new List<double>(tVctr.Count);
                    tVctr[0] = 1;
                }
            }
            List<double> tVctr2 = new List<double>();
            tVctr2.AddRange(tVctr);
            qData tData = new qData(tVctr2);
            return tData;
        }
        //-------------------------------------

        public qData clcACorrSgm(double k, double procent)
        {
            List<double> tVctr = new List<double>(); //результат
            tVctr = this.clcACorrVar(procent).get();
            //int size = tVctr.size()
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = Math.Sqrt(k * tVctr[i]);
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        public qData clcPCorrSgm(double k, double procent)
        {
            int size = 0;
            size = this.Vctr.Count;
            int sizeAll = 0;
            sizeAll = (int)(((double)size) / procent);
            double value = Math.Sqrt((double)(k) / (double)(sizeAll));
            //List<double> tVctr = new List<double>(size,value); //результат
            List<double> tVctr = new List<double>(size);
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = value;
            }
            //throw new System.NotImplementedException();

            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        public qData clcPCorrSgmMin(double k, double procent)
        {
            int size = 0;
            size = this.Vctr.Count;
            int sizeAll = 0;
            sizeAll = (int)(((double)size) / procent);
            double value = -Math.Sqrt((double)(k) / (double)(sizeAll));
            //List<double> tVctr = new List<double>(size,value); //результат
            List<double> tVctr = new List<double>(size);
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = value;
            }
            throw new System.NotImplementedException();
            qData tData = new qData(tVctr);
            return tData;
        }

        //-------------------------------------

        public qData clcMulPolinoms(qData aD1, qData aD2)
        {
            /*
            List<List<double> > Mtrx = new List<List<double> >();
            List<double> Poly1 = new List<double>();
            List<double> Poly2 = new List<double>();
            Poly1 = aD1.get();
            Poly2 = aD2.get();
     //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
            Poly1.insert(Poly1.GetEnumerator(), 0);
     //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
            Poly2.insert(Poly2.GetEnumerator(), 0);
            int size1;
            int size2;
            size1 = Poly1.Count;
            size2 = Poly2.Count;
            int sizeM; // столбцов
            sizeM = size1 + size2 - 1;
            int sizeN; // строк
            sizeN = size1;
            Mtrx = new List<List<double> > (sizeN,List<double> (sizeM, 0));
            // сумма
	
            for(int i = 0; i < size1; i++)
            {
               Mtrx[0][i] = Poly1[i];
            }
            for(int i = 0; i < size2; i++)
            {
               Mtrx[0][i] = Mtrx[0][i] + Poly2[i];
            }
            // умножение на j множитель
            for(int j = 1; j < sizeN; j++)
            {
               for(int i = 1; i < size2; i++)
               {
                  Mtrx[j][j+i] = Poly1[j]*Poly2[i];
               }
            }
            // сумма по столбцам
            for(int i = 1; i < sizeM; i++)
            {
               double summa = 0;
               for(int j = 0; j < sizeN; j++)
               {
                  summa += Mtrx[j][i];
               }
               Mtrx[0][i] = summa;
            }
            Poly1 = Mtrx[0];
     //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
            Poly1.erase(Poly1.GetEnumerator());
            qData tData = new qData();
            tData.set(Poly1);
            return tData;
             */
            throw new System.NotImplementedException();
        }
        //-------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double clcMCovariation(int k, qData aX, qData aY)const
        public double clcMCovariation(int k, qData aX, qData aY)
        {
            int sizeX;
            int sizeY;
            int size;
            sizeX = aX.size();
            sizeY = aY.size();
            size = (sizeX <= sizeY) ? sizeX : sizeY;
            if (size != 0)
            {
                double MX;
                double MY;
                List<double> X = new List<double>();
                List<double> Y = new List<double>();
                if (k >= 0)
                {
                    X = aX.get();
                    Y = aY.get();
                    MX = aX.clcMid();
                    MY = aY.clcMid();
                }
                else
                {
                    k = -k;
                    X = aY.get();
                    Y = aX.get();
                    MX = aY.clcMid();
                    MY = aX.clcMid();
                }
                //int sizeX;
                //int sizeY;
                //int size;
                sizeX = X.Count;
                sizeY = Y.Count;
                size = (sizeX <= sizeY) ? sizeX : sizeY;
                double C = 0;
                for (int i = 0; i < size - k; i++)
                {
                    C += (X[i] - MX) * (Y[i + k] - MY);
                }
                C /= (double)size;
                return C;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------
        public double clcMCorelation(int k, qData aX, qData aY)
        {
            int sizeX;
            int sizeY;
            int size;
            sizeX = aX.size();
            sizeY = aY.size();
            size = (sizeX <= sizeY) ? sizeX : sizeY;
            if (size != 0)
            {
                double C = 0;
                C = clcMCovariation(k, new qData(aX), new qData(aY));
                double R = 0;
                double sgmX = 0;
                double sgmY = 0;
                sgmX = aX.clcVar();
                sgmY = aY.clcVar();
                sgmX = Math.Sqrt(sgmX);
                sgmY = Math.Sqrt(sgmY);
                if (sgmX * sgmY > 0)
                {
                    R = C / (sgmX * sgmY);
                }
                else
                {
                    R = 0;
                }
                return R;
            }
            else
            {
                return 0;
            }
        }

        private List<double> Vctr = new List<double>();
        //aCovar_aCorrel
        //-------------------------------------
        private double clcACovariation(int k)
        {
            if (Vctr.Count != 0)
            {
                double M;
                M = this.clcMid();

                double C = 0;

                for (int i = 0; i < Vctr.Count - k; i++)
                {
                    C += (Vctr[i] - M) * (Vctr[i + k] - M);
                }
                C /= Vctr.Count;
                return C;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double clcACovariation2(int k, double aM) const
        private double clcACovariation2(int k, double aM)
        {
            if (Vctr.Count != 0)
            {
                double C = 0;
                for (int i = 0; i < Vctr.Count - k; i++)
                {
                    C += (Vctr[i] - aM) * (Vctr[i + k] - aM);
                }
                C /= Vctr.Count;
                return C;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcACovariation2() const
        private qData clcACovariation2()
        {
            List<double> tVctr = new List<double>(Vctr.Count); //вектор результата
            if (Vctr.Count != 0)
            {
                double M;
                M = this.clcMid();
                for (int k = 0; k != Vctr.Count - 1; k++) //по всем задержкам
                {
                    double C = 0;
                    C = this.clcACovariation2(k, M);
                    tVctr[k] = C;
                }
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double clcACorrelation(int k) const
        private double clcACorrelation(int k)
        {
            double D = this.clcACovariation(0);
            if (D != 0)
            {
                return this.clcACovariation(k) / D;
            }
            else if ((D == 0) && (k == 0))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //-------------------------------------
        private double clcACorrVar(int k, qData aData)
        {
            double Var = 0;
            if (k < aData.size())
            {
                List<double> tVctr = new List<double>(aData.get());
                for (int i = 1; i < k; i++)
                {
                    Var += (tVctr[i]) * (tVctr[i]);
                }
                Var = (1 + 2 * Var) / tVctr.Count;
            }
            else
            {
                //ShowMessage("Недопустимая операция 2 qData");
                ;
            }
            return Var;
        }
        //-------------------------------------
        private qData clcACorrVar(qData aData)
        {
            List<double> tVctr = new List<double>(aData.size()); //результат
            for (int i = 0; i < tVctr.Count; i++)
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: tVctr[i] = this->clcACorrVar(i, aData);
                tVctr[i] = this.clcACorrVar(i, new qData(aData));
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        private qData clcACorrVar()
        {
            List<double> tVctr = new List<double>(Vctr.Count); //результат
            qData tData = new qData();
            tData = this;
            for (int i = 0; i < tVctr.Count; i++)
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: tVctr[i] = this->clcACorrVar(i, tData);
                tVctr[i] = this.clcACorrVar(i, new qData(tData));
            }
            tData.set(tVctr);
            return tData;
        }
        //-------------------------------------
        private qData clcACorrVar(double procent)
        {
            List<double> tVctr = new List<double>(Vctr.Count); //результат
            qData tData = new qData();
            tData = this;
            int size = tData.size();
            size = (int)(((double)size) / procent);
            for (int i = 0; i < tVctr.Count; i++)
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: tVctr[i] = this->clcACorrVar(i, tData, size);
                tVctr[i] = this.clcACorrVar(i, new qData(tData), size);
            }
            tData.set(tVctr);
            return tData;
        }
        //-------------------------------------

        private double clcACorrVar(int k, qData aData, int size)
        {
            double Var = 0;
            if (k < aData.size())
            {
                List<double> tVctr = new List<double>(aData.get());
                for (int i = 1; i < k; i++)
                {
                    Var += (tVctr[i]) * (tVctr[i]);
                }
                Var = (1 + 2 * Var) / size;
            }
            else
            {
                //ShowMessage("Недопустимая операция 2 qData");
                ;
            }
            return Var;
        }
        //-------------------------------------

        private qData clcACorrSgm(double k, qData aData)
        {
            List<double> tVctr = new List<double>(); //результат
            tVctr = aData.clcACorrVar().get();
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = Math.Sqrt(k * tVctr[i]);
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //-------------------------------------

        private qData clcACorrSgm(qData aCorr, double k)
        {
            List<double> tVctr = new List<double>(); //результат
            tVctr = aCorr.get();
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = Math.Sqrt(k * tVctr[i]);
            }
            qData tData = new qData(tVctr);
            return tData;
        }
        //tmp
        //-------------------------------------
        private List<double> get_Normal_Vector(int n, double m, double s)
        {
            //n  // Численность тестового массива
            //m  // Среднее
            //s  // Среднее квадратичное
            List<double> b = new List<double>(n);
            // Генерация случайной величины, равномерно
            // распределенной в интервале [0,1]
            for (int i = 0; i < n; i++)
            {
                //	b[i] = RandomNumbers.NextNumber();
                //	b[i] /= RAND_MAX;
                //Если вам нужно дробные числа от 0 до 1, то можно сделать так:
                //Random rand = new Random();
                //double temp;
                //temp = 
                Random rand = new Random();
                b[i] = Convert.ToDouble(rand.Next(100)) / 100;
                //throw new System.NotImplementedException();
            }
            // Генерация случайной величины, нормально
            // распределенной с параметрами [0,1]
            for (int i = 0; i < n; i++)
            {
                b[i] = InverseNormalDistribution(b[i]);
            }
            // Генерация случайной величины, нормально
            // распределенной с параметрами [m,s*s]
            for (int i = 0; i < n; i++)
            {
                b[i] = m + s * b[i];
            }
            return b;
        }
        //---------------------------------------------------------------------------
        private double InverseNormalDistribution(double p)
        {
            // Функция вычисления обратной функции стандартного нормального
            // распределения.
            // Обозначения:
            //  p — доверительный уровень.
            // Возвращаемое значение:
            //  функция распределения.
            double q;
            double t;
            if (p <= 0)
            {
                p = 0;
                p = p + 1E-16;
            }
            if (p >= 1)
            {
                p = 1;
                p = p - 1E-16;
            }
            Debug.Assert((p > 0) && (p < 1));
            t = p < 0.5 ? p : 1 - p;
            t = Math.Sqrt(-2 * Math.Log(t));
            q = t - ((0.010328 * t + 0.802853) * t + 2.515517) / (((0.001308 * t + 0.189269) * t + 1.432788) * t + 1);
            return p > 0.5 ? q : -q;
        }
        //---------------------------------------------------------------------------
        private double get_Normal_Point(double m, double s)
        {
            //m  // Среднее
            //s  // Среднее квадратичное
            double b = 0;
            // Генерация случайной величины, равномерно
            // распределенной в интервале [0,1]
            //	b = RandomNumbers.NextNumber();
            //	b /= RAND_MAX;
            //Если вам нужно дробные числа от 0 до 1, то можно сделать так:
            //Random rand = new Random();
            //double temp;
            //temp = 
            Random rand = new Random();
            b = Convert.ToDouble(rand.Next(100)) / 100;

            //throw new System.NotImplementedException();

            // Генерация случайной величины, нормально
            // распределенной с параметрами [0,1]
            b = InverseNormalDistribution(b);
            // Генерация случайной величины, нормально
            // распределенной с параметрами [m,s*s]
            b = m + s * b;
            return b;
        }

        public void set(List<double> aVctr)
        {
            Vctr = aVctr;
        }
        public void set(qData aData)
        {
            this.Vctr = aData.get();
        }
    }
}