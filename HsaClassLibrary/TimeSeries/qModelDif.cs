using System.Collections.Generic;
using System;

namespace TimeSeries
{
    /// <summary>
    ///  this.getData().copyToSGCol(ref window.StringGrid1, 1); // данные
    ///  this.getA().copyToSGCol(ref window.StringGrid1, 2); // разность текущяя
    ///  this.getDataPrognos().copyToSGCol(ref window.StringGrid1, 3); // восстановленный прогноз
    ///  window.Memo1.Lines.Add("Оценки статистических параметров рада разностей.");
    ///  window.Memo1.Lines.Add("Математическое ожидание: "+doubleToStr(stParA[0]));
    ///  window.Memo1.Lines.Add("Дисперсия: "+doubleToStr(stParA[1]));
    ///  window.Memo1.Lines.Add("Среднее квадратическое отклонение: "+doubleToStr(stParA[2]));
    /// </summary>
    public class qModelDif : qModel
    {
        public qModelDif()
        {
            ModelName = "Вычислеие разностей.";
            ModelNum = 0;
            ModelType = 0;
        }
        //---------------------------------------------------------------------------
        public qModelDif(qModelDif aDiff)
        {
            //   setDif(aDiff);
            if (this != aDiff)
            {
                //this = aDiff;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public qModelDif(ref qData aData, int d)
        {
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: setDif(aData, d);
            setDif(new qData(aData), d);
        }
        //---------------------------------------------------------------------------
        public void setDif(qModelDif aDiff)
        {
            //   D = aDiff.getD();  //   DiffD = aDiff.getDiffD();
            if (this != aDiff)
            {
                //this = aDiff;
                throw new System.NotImplementedException();
            }
        }

        //--------------------------------------
        // использование:
        // 1 вычисление разности:
        // установить исходные данные Дата методом set
        // установить порядок разности и вычислить разность методом mkModel
        // в А зипишет полученную разность
        // 2 восстановление значения
        // установить исходные преобразованные данные Дата методом setDataTrans
        // вычислить восстаноленное значение методом mkDataPrognos
        // в DataPrognos зипишет полученный прогноз из разности Дата
        //--------------------------------------
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelDif tModel;
                tModel = new qModelDif();
                return tModel;
            }
            catch
            {
                throw new System.NotImplementedException("Could not allocate. Bye ...");
            }
        }
        //---------------------------------------------------------------------------
        public new qModel clonModel()
        {
            qModelDif tModel;
            tModel = newModel() as qModelDif;
            tModel = (this);
            return tModel;
        }

        //---------------------------------------------------------------------------
        public new void clear()
        {
            D = 0;
            DifD.Clear();
        }
        //void set(qData aData);                     // установит данные которые могут быть исп
        // для разности и для восстановления
        // qData getData(void) const;                  // вернет исходные данные из вектора разности
        // а не то что находится в дата
        //qData getA(void);                          // находится разность
        //qData getDataPrognos(void){;}              // вернет восстановленные данные
        //---------------------------------------------------------------------------
        public override void mkModel(int Metod)
        {
            D = Metod;
            setDif(this.Data, D); //вычислил разность
            this.A = this.getDif(); //записал результат - разность
            State = true;
        }
        //   State = true;                           // метод - порядок разности
        public new void mkPrognos(int NumPrognos) // не используется
        {
            ;
        }
        //---------------------------------------------------------------------------
        public new void mkDataPrognos()
        {
            qData tData = new qData();
            tData = clcPrognos(this.DataTrans); // вычисление восстановленного значения
            this.DataPrognos = tData; // записал прогноз данных
            //   this->Prognos = tData;              // записал прогноз данных
        }
        //---------------------------------------------------------------------------
        public void mkDataPrognosR()
        {
            qData tData = new qData();
            tData = clcPrognosR(this.DataTrans); // вычисление восстановленного значения
            this.DataPrognos = tData; // записал прогноз данных
            //   this->Prognos = tData;              // записал прогноз данных
        }
        public new void mkA() // чтобы не портить А
        {
            ;
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public new List<double> clcStParA()
        {
            //std::vector<double>
            stParA = new List<double>(3);
            stParA[0] = A.clcMid();
            stParA[1] = A.clcVar();
            stParA[2] = Math.Sqrt(stParA[1]);
            return stParA;
        }
        //---------------------------------------------------------------------------
        // запишет результат в Prognos
        //---------------------------------------------------------------------------
        public int size()
        {
            if (this.DifD.Count != 0)
            {
                return this.DifD[0].Count;
            }
            else
            {
                return 0;
            }
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getD() const
        public int getD()
        {
            return this.D;
        }

        private int D; // порядок разности
        private List<List<double>> DifD = new List<List<double>>(); //вектор разностей порядка D

        //---------------------------------------------------------------------------

        private void setDif(qData aData, int d)
        {
            this.DifD = aData.clcDiffMatrix(d);
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcPrognos(qData DiffFor) const
        private qData clcPrognos(qData DiffFor)
        {
            List<List<double>> tDiffD = new List<List<double>>(DifD);
            List<List<double>> tDiffN = new List<List<double>>(DifD);
            List<double> tVDiffFor = DiffFor.get();
            if (D > 0)
            {
                int sizeF = DiffFor.get().Count;
                int sizeD = tDiffD[D].Count;
                int sizeDmD = sizeD - D;
                int distance = sizeF - (sizeD - D);
                if (sizeF >= sizeDmD)
                {
                    //         for(unsigned i = D; i < sizeD+1; i++){
                    for (int i = D; i < sizeD; i++)
                    {
                        tDiffN[D][i] = tVDiffFor[i - D];
                        for (int j = 1; j <= D; j++)
                        {
                            tDiffN[D - j][i] = tDiffD[D - j][i - 1] + tDiffN[D - j + 1][i];
                        }
                    }

                    for (int i = 0; i < distance; i++)
                    {
                        tDiffN[D].Add(tVDiffFor[sizeDmD + i]);
                        for (int j = 1; j <= D; j++)
                        {
                            //               tDiffN[D-j].push_back( tDiffD[D-j].back()+ tDiffN[D-j+1].back() );
                            tDiffN[D - j].Add(tDiffN[D - j][tDiffN[D - j].Count - 1] + tDiffN[D - j + 1][tDiffN[D - j + 1].Count - 1]);
                        }
                    }
                }
                qData tData = new qData();
                tData.set(tDiffN[0]);
                return tData;
            }
            else
            {
                return DiffFor;
            }
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcPrognosR(qData DiffFor) const
        private qData clcPrognosR(qData DiffFor)
        {
            List<List<double>> tDiffD = new List<List<double>>(DifD);
            List<List<double>> tDiffN = new List<List<double>>(DifD);
            List<double> tVDiffFor = DiffFor.get();
            if (D > 0)
            {
                int sizeF = DiffFor.get().Count;
                int sizeD = tDiffD[D].Count;
                int sizeDmD = sizeD - D;
                int distance = sizeF - (sizeD - D);
                if (sizeF >= sizeDmD)
                {
                    //         for(unsigned i = D; i < sizeD+1; i++){
                    for (int i = D; i < sizeD; i++)
                    {
                        tDiffN[D][i] = tVDiffFor[i - D];
                        for (int j = 1; j <= D; j++)
                        {
                            tDiffN[D - j][i] = tDiffN[D - j][i - 1] + tDiffN[D - j + 1][i];
                        }
                    }

                    for (int i = 0; i < distance; i++)
                    {
                        tDiffN[D].Add(tVDiffFor[sizeDmD + i]);
                        for (int j = 1; j <= D; j++)
                        {
                            //               tDiffN[D-j].push_back( tDiffD[D-j].back()+ tDiffN[D-j+1].back() );
                            tDiffN[D - j].Add(tDiffN[D - j][tDiffN[D - j].Count - 1] + tDiffN[D - j + 1][tDiffN[D - j + 1].Count - 1]);
                        }
                    }
                }
                qData tData = new qData();
                tData.set(tDiffN[0]);
                return tData;
            }
            else
            {
                return DiffFor;
            }
        }
        //---------------------------------------------------------------------------
        private qData getDif()
        {
            List<double> tVctr = new List<double>();
            tVctr = DifD[DifD.Count - 1];
            for (int i = 1; i < DifD.Count; i++)
            {
                //tVctr.erase(tVctr.GetEnumerator());
                tVctr.RemoveAt(0);
            }
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }
        //---------------------------------------------------------------------------
        //qData qModelDif::getData(void) const{
        //   qData tData;
        //   tData.set( DifD.front() );
        //   return tData;
        //}
        //---------------------------------------------------------------------------
        private List<List<double>> getDifD()
        {
            return this.DifD;
        }

        //qData getDifD(unsigned d) const;           // вернет разность из расчитанных
        //void mkUpdataRow(std::vector<double> aVctr); //добавление значения
        //void mkAddDif(void);                      //добавление разности D++

        // Data     - данные для анализа
        // Prognos  - прогноз данных, восстановленных по входному прогнозу разности
        // А        - разность
    }

    //---------------------------------------------------------------------------

    //#include "UData.h"
    //#include "UMatrix.h"

    //---------------------------------------------------------------------------
    public class qModelDis : qModel
    {
        //---------------------------------------------------------------------------

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qModelDis()
        {
            ModelName = "Вычислеие разностей.";
            ModelNum = 1;
            ModelType = 0;
        }
        //---------------------------------------------------------------------------
        public qModelDis(qModelDis aDis)
        {
            //   setDis(aDis);
            if (this != aDis)
            {
                //this = aDis;
                throw new System.NotImplementedException();
            }
        }
        //qModelDis(qData &aData,unsigned d);               //вычисляет из входа
        public void Dispose()
        {
            ;
        }
        //---------------------------------------------------------------------------
        public void setDis(qModelDis aDis)
        {
            //   D = aDis.getD();  //   DisD = aDis.getDisD();
            if (this != aDis)
            {
                //this = aDis;
                throw new System.NotImplementedException();
            }
        }

        //--------------------------------------
        // использование:
        // 1 вычисление разности:
        // установить исходные данные Дата методом set
        // установить порядок разности ивычислить разность методом mkModel
        // в А зипишет полученную разность
        // 2 восстановление значения
        // установить исходные преобразованные данные Дата методом setDataTrans
        // вычислить восстаноленное значение методом mkPrognos
        // в Prognos зипишет полученный прогноз из разности Дата
        //--------------------------------------
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        //
        //qModelDis::qModelDis(qData &aData, unsigned d){
        //   setDis(aData, d);
        //}
        //
        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelDis tModel;
                tModel = new qModelDis();
                return tModel;
            }
            catch
            {
                throw new System.NotImplementedException("Could not allocate. Bye ...");
            }
        }
        //---------------------------------------------------------------------------
        public new qModel clonModel()
        {
            qModelDis tModel;
            tModel = newModel() as qModelDis;
            tModel = (this);
            return tModel;
        }

        //---------------------------------------------------------------------------
        public new void clear()
        {
            D = 0;
            S = 0;
            DisD.Clear();
        }
        //void set(qData aData);                     // установит данные которые могут быть исп
        // для разности и для восстановления
        // qData getData(void) const;                  // вернет исходные данные из вектора разности
        // а не то что находится в дата
        //qData getA(void);                          // находится разность
        //qData getPrognos(void){;}                  // вернет восстановленные данные
        //---------------------------------------------------------------------------
        public override void mkModel(int Metod)
        {
            D = Metod;
            setDis(this.Data, D); //вычислил разность
            this.A = this.getDis(); //записал результат - разность
            State = true;
        }
        //   State = true;                           // метод - порядок разности
        // перед этим необходимо указать поряк сезона /setS
        public new void mkPrognos(int NumPrognos) // не используется
        {
            ;
        }
        //---------------------------------------------------------------------------
        public new void mkDataPrognos()
        {
            qData tData = new qData();
            tData = clcPrognos(this.DataTrans); // вычисление восстановленного значения
            this.DataPrognos = tData; // записал прогноз данных
            //   this->Prognos = tData;              // записал прогноз данных
        }
        public new void mkA() // чтобы не портить А
        {
            ;
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public new List<double> clcStParA()
        {
            //std::vector<double>
            stParA = new List<double>(3);
            stParA[0] = A.clcMid();
            stParA[1] = A.clcVar();
            stParA[2] = Math.Sqrt(stParA[1]);
            return stParA;
        }
        //---------------------------------------------------------------------------
        // запишет результат в Prognos
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int size() const
        public int size()
        {
            if (this.DisD.Count != 0)
            {
                return this.DisD[0].Count;
            }
            else
            {
                return 0;
            }
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getD() const
        public int getD()
        {
            return this.D;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getS() const
        public int getS()
        {
            return this.S;
        }
        //---------------------------------------------------------------------------
        public void setS(int @as)
        {
            this.S = @as;
        }

        private int D; // порядок разности
        private int S; // порядок сезона
        private List<List<double>> DisD = new List<List<double>>(); //вектор разностей порядка D

        //---------------------------------------------------------------------------

        private void setDis(qData aData, int d)
        {
            this.DisD = aData.clcDiffMatrix(S, d);
            //clcDisMatrix( S, d );
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcPrognos(qData DisFor) const
        private qData clcPrognos(qData DisFor)
        {
            List<List<double>> tDisD = new List<List<double>>(DisD);
            List<List<double>> tDisN = new List<List<double>>(DisD);
            List<double> tVDisFor = DisFor.get();
            if (D > 0)
            {
                int sizeF = DisFor.get().Count;
                int sizeD = tDisD[D].Count;
                int sizeDmD = sizeD - D * S;
                int distance = sizeF - (sizeD - D * S);
                if (sizeF >= sizeDmD)
                {
                    for (int i = D * S; i < sizeD; i++)
                    {
                        tDisN[D][i] = tVDisFor[i - D * S];
                        for (int j = 1; j <= D; j++)
                        {
                            tDisN[D - j][i] = tDisD[D - j][i - S] + tDisN[D - j + 1][i];
                        }
                    }

                    for (int i = 0; i < distance; i++)
                    {
                        tDisN[D].Add(tVDisFor[sizeDmD + i]);
                        for (int j = 1; j <= D; j++)
                        {
                            //               tDisN[D-j].push_back( tDisD[D-j].back()+ tDisN[D-j+1].back() );
                            int ind = tDisD[D - j].Count - S;
                            tDisN[D - j].Add(tDisN[D - j][ind] + tDisN[D - j + 1][tDisN[D - j + 1].Count - 1]);
                        }
                    }
                }
                qData tData = new qData();
                tData.set(tDisN[0]);
                return tData;
            }
            else
            {
                return DisFor;
            }
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData getDis() const
        private qData getDis()
        {
            List<double> tVctr = new List<double>();
            tVctr = DisD[DisD.Count - 1];
            for (int i = 1; i < DisD.Count; i++)
            {
                for (int j = 0; j < S; j++)
                {
                    // erase the 6th element
                    //myvector.erase(myvector.begin() + 5);
                    // erase the first 3 elements:
                    //myvector.erase(myvector.begin(), myvector.begin() + 3);

                    //tVctr.erase(tVctr.GetEnumerator());
                    tVctr.RemoveAt(0);
                }
            }
            qData tData = new qData();
            tData.set(tVctr);
            return tData;
        }
        //---------------------------------------------------------------------------
        //qData qModelDis::getData(void) const{
        //   qData tData;
        //   tData.set( DisD.front() );
        //   return tData;
        //}
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: ClassicVector<ClassicVector<double> > getDisD() const
        private List<List<double>> getDisD()
        {
            return this.DisD;
        }

        //qData getDisD(unsigned d) const;           // вернет разность из расчитанных
        //void mkUpdataRow(std::vector<double> aVctr); //добавление значения
        //void mkAddDis(void);                      //добавление разности D++

        // Data     - данные для анализа
        // Prognos  - прогноз данных, восстановленных по входному прогнозу разности
        // А        - разность
    }
}