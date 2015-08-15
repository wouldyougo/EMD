using System.Collections.Generic;
using System;

namespace TimeSeries
{
    public class qModelArm : qModel
    {
        //~qModelArm(){;}
        //---------------------------------------------------------------------------
        public qModelArm(qModelArm aModelArm)
        {
            if (this != aModelArm)
            {
                //this = aModelArm;
                throw new System.NotImplementedException();
            }
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
        //#pragma package(smart_init)
        //---------------------------------------------------------------------------
        public qModelArm()
        {
            ModelName = "Модель АРПСС.";
            ModelNum = 6;
            ModelType = 2;

            kASgm = 1;
            kPSgm = 1;
            D = 0; // порядок разности
            Ds = 0; // порядок разности сезонной
            Ss = 0; // период разности сезонной

            //   Ps = 0;
            //   Qs = 0;
            P = 0;
            Q = 0;
            Np = 0;
            Nq = 0;

            flagDs = false;
        }
        //---------------------------------------------------------------------------
        public void setArm(qModelArm aModelArm)
        {
            if (this != aModelArm)
            {
                //this = aModelArm;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void updateModel(qModelArm aModelArm)
        {
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: this->ModelAR = this->ModelAR.clcMulPolinoms(this->ModelAR, aModelArm.ModelAR);
            this.ModelAR = this.ModelAR.clcMulPolinoms(new qData(this.ModelAR), aModelArm.ModelAR);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: this->ModelMA = this->ModelMA.clcMulPolinoms(this->ModelMA, aModelArm.ModelMA);
            this.ModelMA = this.ModelMA.clcMulPolinoms(new qData(this.ModelMA), aModelArm.ModelMA);
            //this->Model = ModelAR.add(ModelMA);

            this.P += aModelArm.getP();
            this.Q += aModelArm.getQ();

            this.MaskAR = this.clcMulMask(this.MaskAR, aModelArm.getMaskAR());
            this.MaskMA = this.clcMulMask(this.MaskMA, aModelArm.getMaskMA());

            this.updateMask(); // создал маску
            this.mkMaskModel(); // умножил маски на модели //this->Model = ModelAR.add(ModelMA);

            this.D += aModelArm.getD();
            this.Ds += aModelArm.getDs();
            this.Ss += aModelArm.getSs();

            this.mkUpdate();
        }

        //---------------------------------------------------------------------------
        public void setParamD(int aD)
        {
            D = aD;
        }
        //---------------------------------------------------------------------------
        public void setParamDs(int aDs)
        {
            Ds = aDs;
        }
        //---------------------------------------------------------------------------
        public void setParamSs(int aSs)
        {
            Ss = aSs;
        }

        //---------------------------------------------------------------------------
        public int getD()
        {
            return D;
        }
        //---------------------------------------------------------------------------
        public int getDs()
        {
            return Ds;
        }
        //---------------------------------------------------------------------------
        public int getSs()
        {
            return Ss;
        }

        //---------------------------------------------------------------------------
        public void setParamPs(int aPs)
        {
            Np = aPs;
            P = Np * Ss;
            MaskAR = new List<int>(P);
            for (int i = 0; i < Np; i++)
            {
                int ind;
                ind = (i + 1) * Ss - 1;
                MaskAR[ind] = 1;
            }
            List<double> modARS = new List<double>(P);
            for (int i = 0; i < Np; i++)
            {
                int ind;
                ind = (i + 1) * Ss - 1;
                modARS[ind] = 0.5 * Math.Pow(-1, i);
            }
            ModelAR.set(modARS);
        }
        //---------------------------------------------------------------------------
        public void setParamQs(int aQs)
        {
            Nq = aQs;
            Q = Nq * Ss;
            MaskMA = new List<int>(Q);
            for (int i = 0; i < Nq; i++)
            {
                int ind;
                ind = (i + 1) * Ss - 1;
                MaskMA[ind] = 1;
            }
            List<double> modMAS = new List<double>(Q);
            for (int i = 0; i < Nq; i++)
            {
                int ind;
                ind = (i + 1) * Ss - 1;
                modMAS[ind] = 0.5 * Math.Pow(-1, i);
            }
            ModelMA.set(modMAS);
        }

        //   unsigned getQs(void)const;
        //   unsigned getPs(void)const;

        //---------------------------------------------------------------------------
        public void setParamP(int aP)
        {
            throw new System.NotImplementedException();
            P = aP;
            Np = P;
            //MaskAR = new List<int> (P, 1);
        }
        //---------------------------------------------------------------------------
        public void setParamQ(int aQ)
        {
            throw new System.NotImplementedException();
            Q = aQ;
            Nq = Q;
            //MaskMA = new List<int> (Q, 1);
        }

        //---------------------------------------------------------------------------
        public void setParamP(List<double> mP)
        {
            Np = 0;
            int size;
            size = mP.Count;
            P = size;
            MaskAR = new List<int>(size);
            List<double> modAR = new List<double>(size);
            for (int i = 0; i < size; i++)
            {
                modAR[i] = mP[i];
                if (mP[i] != 0)
                {
                    MaskAR[i] = 1;
                    Np++;
                }
            }
            ModelAR.set(modAR);
        }
        //---------------------------------------------------------------------------
        public void setParamQ(List<double> mQ)
        {
            Nq = 0;
            int size;
            size = mQ.Count;
            Q = size;
            MaskMA = new List<int>(size);
            List<double> modMA = new List<double>(size);
            for (int i = 0; i < size; i++)
            {
                modMA[i] = mQ[i];
                if (mQ[i] != 0)
                {
                    MaskMA[i] = 1;
                    Nq++;
                }
            }
            ModelMA.set(modMA);
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getQ()const
        public int getQ()
        {
            return Q;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getP()const
        public int getP()
        {
            return P;
        }

        //---------------------------------------------------------------------------
        public void setParamSgm(int kA, int kP)
        {
            kASgm = kA;
            kPSgm = kP;
        }
        //---------------------------------------------------------------------------
        public qACorr getACorr()
        {
            return ACorr;
        }
        //---------------------------------------------------------------------------
        public qPCorr getPCorr()
        {
            return PCorr;
        }
        //++++++++++++++++++++++++++
        //---------------------------------------------------------------------------
        public new qModel newModel()
        {
            try
            {
                qModelArm tModel;
                tModel = new qModelArm();
                return tModel;
            }
            catch
            {
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public new qModel clonModel()
        {
            qModelArm tModel;
            tModel = newModel() as qModelArm;
            tModel = (this);
            return tModel;
        }
        //qData getA(void) const;
        //---------------------------------------------------------------------------
        public new void clear()
        {
            kASgm = 1;
            kPSgm = 1;
            D = 0; // порядок разности
            Ds = 0; // порядок разности сезонной
            Ss = 0; // период разности сезонной

            //   Ps = 0;
            //   Qs = 0;
            P = 0;
            Q = 0;
            Np = 0;
            Nq = 0;

            flagDs = false;
        }
        //void set(qData &aData);
        //qData getPrognos(void){;}
        public override void mkModel(int Metod) // не исп.
        {
            ;
        }
        //---------------------------------------------------------------------------
        public new void mkPrognos(int NumPrognos)
        {
            setNumPoPr(NumPrognos);
            List<double> tVctr = new List<double>();
            int tmp;
            tmp = getNumStPr();
            Prognos = forecast(Model.get(), ZDM.get(), ref tVctr, tmp);
            //A.set( tVctr );
            // суммировать разность
            if (Ds > 0)
            {
                Dis.setDataTrans(Prognos);
                Dis.mkDataPrognos();
                Prognos = Dis.getDataPrognos();
            }
            // суммировать разность
            if (D > 0)
            {
                Dif.setDataTrans(Prognos);
                Dif.mkDataPrognos();
                Prognos = Dif.getDataPrognos();
            }
            // прибавить МО
            double M = Tetta0;
            tVctr = Prognos.get();
            int size = tVctr.Count;
            for (int i = 0; i < size; i++)
            {
                tVctr[i] = tVctr[i] + M;
            }
            Prognos.set(tVctr);

            mkA();
            State = true;
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------

        //---------------------------------------------------------------------------
        public qData getTetta0()
        {
            List<double> tVctr = new List<double>();
            //List<double> tVctr = new List<double>(1, Tetta0);
            throw new System.NotImplementedException();
            qData tData = new qData(tVctr);
            return tData;
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double getM()const
        public double getM()
        {
            return Tetta0;
        }

        //---------------------------------------------------------------------------
        public List<int> getMask()
        {
            return Mask;
        }
        //---------------------------------------------------------------------------
        public List<int> getMaskAR()
        {
            return MaskAR;
        }
        //---------------------------------------------------------------------------
        public List<int> getMaskMA()
        {
            return MaskMA;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: ClassicVector<int> clcMulMask(ClassicVector<int> aD1, ClassicVector<int> aD2)const
        public List<int> clcMulMask(List<int> aD1, List<int> aD2)
        {
            List<List<int>> Mtrx = new List<List<int>>();
            List<int> Poly1 = new List<int>();
            List<int> Poly2 = new List<int>();
            Poly1 = aD1;
            Poly2 = aD2;
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
            //Poly1.insert(Poly1.GetEnumerator());
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
            //Poly2.insert(Poly2.GetEnumerator());
            throw new System.NotImplementedException();

            int size1;
            int size2;
            size1 = Poly1.Count;
            size2 = Poly2.Count;
            int sizeM; // столбцов
            sizeM = size1 + size2 - 1;
            int sizeN; // строк
            sizeN = size1;
            //Mtrx = new List<List<int> > (sizeN,List<int> (sizeM, 0));
            Mtrx = new List<List<int>>(sizeN);
            for (int i = 0; i < sizeN; i++)
            {
                Mtrx[i] = new List<int>(sizeM);
            }

            // сумма
            for (int i = 0; i < size1; i++)
            {
                Mtrx[0][i] = Poly1[i];
            }
            for (int i = 0; i < size2; i++)
            {
                Mtrx[0][i] = Mtrx[0][i] + Poly2[i];
            }
            // умножение на j множитель
            for (int j = 1; j < sizeN; j++)
            {
                for (int i = 1; i < size2; i++)
                {
                    Mtrx[j][j + i] = Poly1[j] * Poly2[i];
                }
            }
            // сумма по столбцам
            for (int i = 1; i < sizeM; i++)
            {
                double summa = 0;
                for (int j = 0; j < sizeN; j++)
                {
                    summa += Mtrx[j][i];
                }
                Mtrx[0][i] = (int)summa;
            }
            Poly1 = Mtrx[0];
            for (int i = 0; i < sizeM; i++)
            {
                if (Poly1[i] > 0)
                {
                    Poly1[i] = 1;
                }
                else
                {
                    Poly1[i] = 0;
                }
            }
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
            Poly1.RemoveAt(0);
            return Poly1;
        }
        //---------------------------------------------------------------------------
        public void mkMaskModel()
        {
            List<double> mdl = new List<double>();
            List<double> mdlAR = new List<double>();
            List<double> mdlMA = new List<double>();

            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: this->Model = ModelAR.add(ModelMA);
            this.Model = ModelAR.AddRange(new qData(ModelMA));
            mdl = Model.get();
            mdlAR = ModelAR.get();
            mdlMA = ModelMA.get();

            try
            {
                int size;
                size = Mask.Count;
                for (int i = 0; i < size; i++)
                {
                    mdl[i] = mdl[i] * Mask[i];
                }
                size = MaskAR.Count;
                for (int i = 0; i < size; i++)
                {
                    mdlAR[i] = mdlAR[i] * MaskAR[i];
                }
                size = MaskMA.Count;
                for (int i = 0; i < size; i++)
                {
                    mdlMA[i] = mdlMA[i] * MaskMA[i];
                }

                Model.set(mdl);
                ModelAR.set(mdlAR);
                ModelMA.set(mdlMA);

            }
            catch
            {
                throw new System.ApplicationException("Размер модели и маски не совпадает");
            }
        }
        //---------------------------------------------------------------------------
        public void updateMask()
        {
            int size;
            int sizeAR;
            int sizeMA;
            sizeAR = MaskAR.Count;
            sizeMA = MaskMA.Count;
            size = sizeAR + sizeMA;

            Mask = new List<int>(size);
            for (int i = 0; i < sizeAR; i++)
            {
                Mask[i] = MaskAR[i];
            }
            for (int i = 0; i < sizeMA; i++)
            {
                int ind = i + sizeAR;
                Mask[ind] = MaskMA[i];
            }
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: ClassicVector<double> addZero(ClassicVector<double> aDelZer, ClassicVector<int> aMask)const
        public List<double> addZero(List<double> aDelZer, List<int> aMask)
        {
            List<double> delZer = new List<double>();
            List<int> Mask = new List<int>();
            delZer = aDelZer;
            Mask = aMask;
            int size = Mask.Count;
            List<double> Zer = new List<double>(size);
            try
            {
                for (int i = 0, j = 0; i < size; i++)
                {
                    if (Mask[i] != 0)
                    {
                        Zer[i] = delZer[j];
                        j++;
                    }
                }
            }
            catch
            {
                throw new System.ApplicationException("DEBUG: возможно число параметров не соответствует размеру маски");
            }
            return Zer;
        }
        // заполнит модель по маске   // из входной модели без нулей
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: ClassicVector<double> delZero(ClassicVector<double> aZer, ClassicVector<int> aMask)const
        public List<double> delZero(List<double> aZer, List<int> aMask)
        {
            List<double> Zer = new List<double>();
            List<int> Mask = new List<int>();
            Zer = aZer;
            Mask = aMask;

            int size = Zer.Count;
            if ((int)size != Mask.Count)
            {
                throw new System.ApplicationException("DEBUG: Размер модели не соответствует размеру ее маски");
            }

            List<double> delZer = new List<double>();

            for (int i = 0; i < size; i++)
            {
                if (Mask[i] != 0)
                {
                    delZer.Add(Zer[i]);
                }
            }
            return delZer;
        }
        // создаст модель без нулей   // удалит нули из модели

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qData getModel()
        {
            return Model;
        }
        //---------------------------------------------------------------------------
        public qData getModelAR()
        {
            return ModelAR;
        }
        //---------------------------------------------------------------------------
        public qData getModelMA()
        {
            return ModelMA;
        }
        //---------------------------------------------------------------------------
        public qData getZDM()
        {
            return ZDM;
        }
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //   qData getZpls();

        //---------------------------------------------------------------------------

        public void mkUpdate()
        {
            double M = 0;
            M = Data.clcMid();
            List<double> tVctr = new List<double>();
            tVctr = Data.get();
            for (int i = 0; i < tVctr.Count; i++)
            {
                tVctr[i] = tVctr[i] - M;
            }
            ZDM.set(tVctr);
            Tetta0 = 0;
            Tetta0 = M;
            //ZDM = Data;///
            if (D > 0)
            {
                Dif.set(ZDM);
                Dif.mkModel(D);
                ZDM = Dif.getA();
            }
            if (Ds > 0)
            {
                Dis.set(ZDM);
                Dis.setS(Ss);
                Dis.mkModel(Ds);
                ZDM = Dis.getA();
            }
            //---------------------------
            this.ACorr.set(ZDM, kASgm);
            this.PCorr.set(ZDM, kPSgm);
        }
        //---------------------------------------------------------------------------
        public void clcModel(int metod)
        {
            ModelMA.clear();
            if (Q > 0)
            {
                if (metod == 0) // квадратически сход.
                {
                    ModelMA = clcParamMAK(P, Q, this.PCorr.getF(P));
                }
                else
                    if (metod == 1) // линейно сход.
                    {
                        ModelMA = clcParamMAL(P, Q, this.PCorr.getF(P));
                    }
                    else
                        if (metod == 2) // линейно сход.
                        {
                            ModelMA = clcParamMAL2(P, Q, this.PCorr.getF(P));
                        }
            }
            // убрать первый 0 из модели АР
            ModelAR.clear();
            if (P > 0)
            {
                ModelAR = this.PCorr.getF(P);
                List<double> tVctr = new List<double>();
                tVctr = ModelAR.get();
                //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
                tVctr.RemoveAt(0);
                ModelAR.set(tVctr);
            }
            else
            {
                ModelAR.clear();
            }
            //this->Model = ModelAR.add(ModelMA);
            this.updateMask(); // создал маску /????????????????????????????
            this.mkMaskModel(); // умножил маски на модели/????????????????????????????
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------

        public qData revisionParam()
        {
            //для каждого Betta вычислить производную
            //сформировать матрицы А и g
            // нормировать матрицы А и g
            // найти h
            // вычислить поправки
            //oooooooooooooooooooooooooooooo
            //ofstream @out = new ofstream("tmpModArm.rpt");
            //@out << "Вычисление параметров модели АРСС";
            //@out << "\n";
            //oooooooooooooooooooooooooooooo

            double delta = 0.1;
            double cf_P = 0.1;
            // double cf_P = 2.0;
            double cf_E = 0.0001;
            double cf_F2 = 1.8;
            int p;
            int q;
            int k;
            int n;
            List<double> Zdm = new List<double>(); // ряд исхлдный

            p = this.Np;
            q = this.Nq;
            k = p + q;

            //   p = ModelAR.size();
            //   q = ModelMA.size();
            //   k = Model.size();

            Zdm = this.ZDM.get();
            n = Zdm.Count;
            List<double> A0 = new List<double>(n);
            List<double> Ab = new List<double>(n);
            List<double> B0 = new List<double>();
            List<double> Bd = new List<double>();
            List<double> BZero = new List<double>();
            BZero = this.Model.get();
            B0 = delZero(BZero, Mask);
            //Удалить 3 посл. значения - tetta0 SgmA2, M;   //B0.pop_back();   B0.pop_back();   B0.pop_back();
            //создал Х
            //List<List<double> > X = new List<List<double> >(k, List<double> (n, 0));
            List<List<double>> X = new List<List<double>>(k);
            for (int i = 0; i < k; i++)
            {
                X[i] = new List<double>(n);
            }

            // матрицы
            qMData MA = new qMData();
            qMData MG = new qMData(); // MG.set( MG.clcMtrx( G, G.size() ) );
            qMData MH = new qMData(); // MH.set( MH.clcMtrx( H, H.size() ) );
            try
            {
                for (int c = 0; c <= 100; c++)
                {
                    // заполнение Х-------------
                    BZero = addZero(B0, Mask);
                    this.forecast(BZero, Zdm, ref A0, 0);

                    for (int i = 0; i < k; i++)
                    {
                        Bd = B0;
                        Bd[i] = Bd[i] + delta;
                        BZero = addZero(Bd, Mask);
                        this.forecast(BZero, Zdm, ref Ab, 0);
                        for (int j = 0; j < n; j++)
                        {
                            X[i][j] = (A0[j] - Ab[j]) / delta;
                        }
                    }
                    // Заполнение А
                    //List<List<double> > A = new List<List<double> >(k, List<double> (k, 0));
                    List<List<double>> A = new List<List<double>>(k);
                    for (int i = 0; i < k; i++)
                    {
                        A[i] = new List<double>(k);
                    }

                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            double S = 0;
                            for (int t = 0; t < n; t++)
                            {
                                S += X[i][t] * X[j][t];
                            }
                            A[i][j] = S;
                        }
                    }
                    // Заполнение G
                    List<double> G = new List<double>(k);
                    for (int i = 0; i < k; i++)
                    {
                        double S = 0;
                        for (int t = 0; t < n; t++)
                        {
                            S += X[i][t] * A0[t]; //+++++++++++++++++++++++
                        }
                        G[i] = S;
                    }
                    // нормирующие величины
                    List<double> D = new List<double>(k);
                    for (int i = 0; i < k; i++)
                    {
                        D[i] = Math.Sqrt(A[i][i]);
                    }
                    // модифицировать
                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            if (i != j)
                            {
                                double delitel;
                                delitel = D[i] * D[j];
                                if (delitel != 0)
                                {
                                    A[i][j] = A[i][j] / delitel;
                                }
                            }
                            else
                            {
                                A[i][j] = 1 + cf_P;
                            }
                        }
                    }
                    for (int i = 0; i < k; i++)
                    {
                        if (D[i] != 0)
                        {
                            G[i] = G[i] / D[i];
                        }
                    }
                    //решение ур-Ыния Ah = g   // заполнить матр А и обратить   // умножить на g: H = Аобр*G
                    MA.set(A);
                    MG.set(MG.clcMtrx(G, G.Count));
                    //MA = MA.clcPIMatrix(MA.get());
                    throw new System.NotImplementedException();
                    MH = MA * MG;
                    List<double> H = new List<double>(k);
                    H = MH.clcVctr(MH.get());
                    for (int i = 0; i < k; i++)
                    {
                        if (D[i] != 0)
                        {
                            H[i] = H[i] / D[i];
                        }
                    }
                    for (int i = 0; i < k; i++)
                    {
                        Bd[i] = B0[i] + H[i];
                    }
                    // оценка остаточных ошибок
                    double Sb = 0;
                    double S0 = 0;
                    BZero = addZero(Bd, Mask);
                    this.forecast(BZero, Zdm, ref Ab, 0);
                    qData tClc = new qData();
                    S0 = tClc.clcAmountSquare(ref A0);
                    Sb = tClc.clcAmountSquare(ref Ab);

                    /*
                    @out << "Итерация:";
                    @out << "\t";
                    @out << c;
                    @out << "\n";
                    @out << "Текущие параметры:";
                    @out << "\n";
                    for(int i = 0; i < k; i++)
                    {
                       @out << "K[";
                       @out << i;
                       @out << "]:\t";
                       @out << B0[i];
                       @out << "\n";
                    }
                    @out << "Var(a): ";
                    @out << S0;
                    @out << "\n";
                    @out << "Вычисленные параметры:";
                    @out << "\n";
                    for(int i = 0; i < k; i++)
                    {
                       @out << "K[";
                       @out << i;
                       @out << "]:\t";
                       @out << Bd[i];
                       @out << "\n";
                    }
                    @out << "Var(a): ";
                    @out << Sb;
                    @out << "\n";
                       @out << "k_p: ";
                       @out << cf_P;
                       @out << "\n";
                       @out << "k_d: ";
                       @out << delta;
                       @out << "\n";
                    */

                    if (Sb < S0)
                    {
                        B0 = Bd;
                        //oooooooooooooooooooooooooooooo
                        //@out << "Параметры обновлены";
                        //@out << "\n";
                        //oooooooooooooooooooooooooooooo
                        // достигнута сходимость если: H[i] < e
                        bool flag = true;
                        for (int i = 0; i < k; i++)
                        {
                            if (!((H[i] > -cf_E) && (H[i] < cf_E)))
                            {
                                flag = false;
                            }
                        }
                        if (flag == true)
                        {
                            //++++++++++++++++
                            // запомнить А матрицу
                            // запомнить а для расчета автокорреляций
                            //ShowMessage("Число итераций:"+IntToStr(c));
                            break;
                        }
                        else
                        {
                            cf_P = cf_P / cf_F2;
                            if (cf_P < 0.001)
                            {
                                cf_P = 0.001;
                            }
                            //qwooooooooooooooooo
                            //delta = delta/cf_F2;
                            //if( delta < 0.001){
                            //    delta = 0.001;
                            //}//qwooooooooooooooooo
                        }
                    }
                    else
                    {
                        cf_P = cf_P * cf_F2;
                        if (cf_P > 8)
                        {
                            //cf_P = 5;
                            break;
                        }
                        //qwooooooooooooooooo
                        //delta = delta*cf_F2;
                        //if( delta > 0.1){
                        //   delta = 0.1;
                        //}//qwooooooooooooooooo
                    }
                }
                //------------------------------
            }
            catch
            {
                throw new System.ApplicationException("Исключение при уточнении параметров");
                //oooooooooooooooooooooooooooooo
                //@out.close();
                //oooooooooooooooooooooooooooooo
                return Model;
            }
            //------------------------
            List<double> PPar = new List<double>();
            List<double> QPar = new List<double>();
            BZero = addZero(B0, Mask);
            //PPar.AddRange(BZero.GetEnumerator(), BZero.GetEnumerator() + P);
            //QPar.AddRange(BZero.GetEnumerator()+P, BZero.GetEnumerator() + P+Q);
            throw new System.NotImplementedException();
            ModelAR.set(PPar);
            ModelMA.set(QPar);
            //------------------------
            //oooooooooooooooooooooooooooooo
            //@out.close();
            //oooooooooooooooooooooooooooooo
            qData tData = new qData();
            tData.set(BZero);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: Model = tData;
            Model.set(tData);
            return tData;
        }



        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcParamMAK(int ap, int aq, qData aF) const
        private qData clcParamMAK(int ap, int aq, qData aF)
        {
            int p = ap;
            int q = aq;

            List<double> F = new List<double>();
            F = aF.get();

            List<double> newACov = new List<double>(q + 1);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: newACov = clcNewCov(p,q,aF);
            newACov = clcNewCov(p, q, new qData(aF));
            /*
            ofstream @out = new ofstream("tmpModArm.rpt");
            @out << "Предварительные оценки параметров скользящего среднего";
            @out << "\n";
            @out << "Квадратически сходящийся процесс";
            @out << "\n";
            */
            //------------------------------    //------------------------------
            //квадратически сходящийся процесс
            //------------------------------    // newACov[0] = 1.25; // newACov[1] = -0.5;
            List<double> Tau = new List<double>(q + 1);
            Tau[0] = Math.Sqrt(newACov[0]); // Tau[0] = Sgm(a)
            List<double> f = new List<double>(q + 1);
            //List<List<double> > T1 = new List<List<double> >(q+1, List<double> (q+1,0));
            List<List<double>> T1 = new List<List<double>>(q + 1);
            for (int i = 0; i < q + 1; i++)
            {
                T1[i] = new List<double>(q + 1);
            }
            //List<List<double> > T2 = new List<List<double> >(q+1, List<double> (q+1,0));
            List<List<double>> T2 = new List<List<double>>(q + 1);
            for (int i = 0; i < q + 1; i++)
            {
                T2[i] = new List<double>(q + 1);
            }
            qMData TAU = new qMData();
            TAU.set(TAU.clcMtrx(Tau, Tau.Count));
            qMData t1 = new qMData();
            qMData t2 = new qMData();
            qMData T = new qMData();
            qMData FF = new qMData();
            //------------------------------
            List<double> Tetta;
            try
            {
                int MaxCount = 30;
                for (int i = 0; i < MaxCount; i++)
                {
                    //------------------------------
                    for (int j = 0; j <= q; j++)
                    {
                        double s = 0;
                        for (int k = 0; k <= q - j; k++)
                        {
                            s += Tau[k] * Tau[k + j];
                        }
                        f[j] = s - newACov[j];
                    }
                    FF.set(FF.clcMtrx(f, f.Count));
                    //------------------------------
                    //T1 = new List<List<double> > (q+1, List<double> (q+1,0));
                    T1 = new List<List<double>>(q + 1);
                    for (int j = 0; j < q + 1; j++)
                    {
                        T1[j] = new List<double>(q + 1);
                    }
                    //T2 = new List<List<double> > (q+1, List<double> (q+1,0));
                    T2 = new List<List<double>>(q + 1);
                    for (int j = 0; j < q + 1; j++)
                    {
                        T2[j] = new List<double>(q + 1);
                    }

                    for (int j = 0; j <= q; j++)
                    {
                        //std.copy(Tau.GetEnumerator()+j,Tau.end(), T1[j].GetEnumerator());
                        //std.copy(Tau.GetEnumerator(), Tau.end()-i,T2[j].GetEnumerator()+j);
                        throw new System.NotImplementedException();
                        ;
                    }
                    t1.set(T1);
                    t2.set(T2);
                    T = t1 + t2;
                    //T = T.clcPIMatrix(T.get()); // T1 = T.get();//++++++++++++++
                    throw new System.NotImplementedException();


                    ////T = T.clcInverseMatrix( T.get() );   //      T1 = T.get();//++++++++++++++
                    //------------------------------
                    FF = T * FF; // h = FF // T1 = FF.get();//++++++++++++++
                    TAU = TAU - FF; // Tau = Tau - h // T1 = TAU.get();//++++++++++++++
                    Tau = TAU.clcVctr(TAU.get());
                    //------------------------------
                    /*
                    @out << "Итерация:";
                    @out << "\t";
                    @out << i;
                    @out << "\n";
                     */
                    Tetta = new List<double>(q + 1);
                    for (int j = 1; j <= q; j++)
                    {
                        Tetta[j] = -(Tau[j] / Tau[0]);
                        /*
                        @out << "Тетта[";
                        @out << i;
                        @out << "]:\t";
                        @out << Tetta[i];
                        @out << "\n";
                         */
                    }
                    //@out << "Var(a): ";
                    //@out << Tau[0]*Tau[0];
                    //@out << "\n";
                    //oooooooooooooooooooooooooooooo
                    bool flag = true;
                    for (int j = 0; j <= q; j++)
                    {
                        if (!((f[j] > -0.005) && (f[j] < 0.005)))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        //ShowMessage("КСП число итераций:"+IntToStr(i)); ////////////////////////////
                        break;
                    }
                    if (i == MaxCount - 1)
                    {
                        throw new System.ApplicationException();
                    }
                }
                //------------------------------
            }
            catch
            {
                //ShowMessage("clcParamMAK исключение: квадратически сходящийся процесс");///
                //oooooooooooooooooooooooooooooo
                //@out.close();
                //oooooooooooooooooooooooooooooo
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: return clcParamMAL2(p,q,aF);
                return clcParamMAL2(p, q, new qData(aF));
            }
            //------------------------------   //------------------------------
            Tetta = new List<double>(q + 1);
            Tetta = new List<double>(q + 1);
            for (int i = 1; i <= q; i++)
            {
                Tetta[i] = -(Tau[i] / Tau[0]);
            }
            //------------------------------   //------------------------------
            //oooooooooooooooooooooooooooooo
            //@out.close();
            //oooooooooooooooooooooooooooooo
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
            Tetta.RemoveAt(0); // удалить Tetta0
            qData tData = new qData(Tetta);
            return tData;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcParamMAL(int ap, int aq, qData aF) const
        private qData clcParamMAL(int ap, int aq, qData aF)
        {
            int p = ap;
            int q = aq;

            List<double> F = new List<double>();
            F = aF.get();

            List<double> newACov = new List<double>(q + 1);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: newACov = clcNewCov(p,q,aF);
            newACov = clcNewCov(p, q, new qData(aF));
            /*
            ofstream @out = new ofstream("tmpModArm.rpt");
            @out << "Предварительные оценки параметров скользящего среднего";
            @out << "\n";
            @out << "Линейно сходящийся процесс";
            @out << "\n";
            */
            //------------------------------    //------------------------------
            //линейно сходящийся процесс
            //Sgm(a)2
            List<double> Tetta = new List<double>(q + 1);
            List<double> TettaBk = new List<double>(q + 1);
            try
            {
                int MaxCount = 20;
                for (int i = 0; i < MaxCount; i++)
                {
                    //-------------------------
                    // Выч. SgmA2
                    double S = 0;
                    double SgmA2 = 0;
                    for (int j = 1; j <= q; j++)
                    {
                        S += Tetta[j] * Tetta[j];
                    }
                    SgmA2 = newACov[0] / (1 + S);
                    //-------------------------
                    // Выч. TeTTы
                    for (int j = q; j > 0; j--)
                    {
                        double S1 = 0;
                        for (int k = 1; k <= q - j; k++)
                        {
                            S1 += Tetta[k] * Tetta[j + k]; ///????????????
                        }
                        double drob = 0;
                        if (SgmA2 != 0)
                        {
                            drob = newACov[j] / SgmA2;
                        }
                        Tetta[j] = -(drob - S1);
                    }
                    //-------------------------
                    bool flag = true;
                    for (int j = 1; j <= q; j++)
                    {
                        double delta = 0;
                        delta = TettaBk[j] - Tetta[j];
                        if (!((delta > -0.0001) && (delta < 0.0001)))
                        {
                            flag = false;
                            break;
                        }
                    }
                    TettaBk = Tetta;
                    /*
                       @out << "Итерация:";
                       @out << "\t";
                       @out << i;
                       @out << "\n";
                    for(int j = 1; j <= q; j++)
                    {
                       @out << "Тетта[";
                       @out << j;
                       @out << "]:\t";
                       @out << Tetta[j];
                       @out << "\n";
                    }
                       @out << "Var(a): ";
                       @out << SgmA2;
                       @out << "\n";
                    */
                    if (flag)
                    {
                        //ShowMessage("ЛСП число итераций:"+IntToStr(i)); ///////////////////////////
                        break;
                    }
                    if (i == MaxCount - 1)
                    {
                        throw new System.ApplicationException();
                    }
                }
                //------------------------------    //------------------------------
            }
            catch
            {
                //ShowMessage("clcParamMAL исключение: линейно сходящийся процесс"); ///////
                //oooooooooooooooooooooooooooooo
                //@out.close();
                //oooooooooooooooooooooooooooooo
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: return clcParamMAL2(p,q,aF);
                return clcParamMAL2(p, q, new qData(aF));
            }
            //------------------------------    //------------------------------
            //oooooooooooooooooooooooooooooo
            //@out.close();
            //oooooooooooooooooooooooooooooo
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
            Tetta.RemoveAt(0); // удалить Tetta0
            qData tData = new qData(Tetta);
            return tData;
        }
        //---------------------------------------------------------------------------
        private List<double> clcNewCov(int p, int q, qData aF)
        {
            List<double> aACov = new List<double>();
            List<double> F = new List<double>();
            aACov = ZDM.clcACovariation().get();
            F = aF.get();
            if (F.Count < p + 1)
            {
                throw new System.ApplicationException("Недопустимая операция qModelArm::clcParamMAK");
            }
            //вычислениe новых ковариаций
            List<double> newACov = new List<double>(q + 1); // в алгоритме С'
            if (p == 0)
            {
                newACov = aACov;
            }
            else
            {
                F[0] = -1;
                for (int j = 0; j <= q; j++)
                {
                    double S1 = 0;
                    for (int i = 0; i <= p; i++)
                    {
                        int index = 0;
                        double S2 = 0;
                        for (int k = 0; k <= p; k++)
                        {
                            index = j + i - k;
                            if (index < 0)
                            {
                                index = -index;
                            }
                            S2 += F[i] * F[k] * aACov[index];
                        }
                        S1 += S2;
                    }
                    newACov[j] = S1;
                }
            }
            //------------------------------   //------------------------------
            //    s += F[i]*aACov[i]; } SgmA2 = aACov[0] - s; }else{ SgmA2 = newACov[0]; }
            // оценка SgmA2
            if (q == 0)
            {
                double s = 0;
                for (int i = 1; i <= p; i++)
                {
                    s += F[i] * newACov[i];
                }
                stSgmA2 = newACov[0] - s;
            }
            else
            {
                stSgmA2 = newACov[0];
            }
            //------------------------------   //------------------------------
            // оценка Tetta0
            if (p == 0)
            {
                stTetta = ZDM.clcMid();
            }
            else
            {
                double Wstrh = ZDM.clcMid();
                double s = 0;
                for (int i = 1; i <= p; i++)
                {
                    s += F[i];
                }
                stTetta = Wstrh * (1 - s);
            }
            //------------------------------   //------------------------------
            return newACov;
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qData clcParamMAL2(int p, int q, qData aF) const
        private qData clcParamMAL2(int p, int q, qData aF)
        {
            List<double> F = new List<double>();
            F = aF.get();
            List<double> newACov = new List<double>(q + 1);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: newACov = clcNewCov(p,q,aF);
            newACov = clcNewCov(p, q, new qData(aF));
            //------------------------------    //------------------------------
            //линейно сходящийся процесс
            //Sgm(a)2
            List<double> Tetta = new List<double>(q + 1); // Tetta[0] = Sgm(a)2
            Tetta[0] = newACov[0];
            for (int j = q; j > 0; j--)
            {
                double S1 = 0;
                for (int i = 1; i <= q - j; i++)
                {
                    S1 = Tetta[i] * Tetta[j + i];
                }
                Tetta[j] = -(newACov[j] / Tetta[0] - S1);
            }
            //------------------------------    //------------------------------
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
            Tetta.RemoveAt(0);
            qData tData = new qData(Tetta);
            return tData;
        }
        //---------------------------------------------------------------------------
        private List<double> clcNewCov2(int p, int q, qData aF)
        {
            List<double> aACov = new List<double>();
            List<double> F = new List<double>();
            aACov = ZDM.clcACovariation().get();
            F = aF.get();
            if (F.Count < p + 1)
            {
                throw new System.ApplicationException("Недопустимая операция qModelArm::clcParamMAK");
            }

            //вычислени новых ковариаций
            List<double> newACov = new List<double>(q + 1);
            if (p == 0)
            {
                newACov = aACov;
            }
            else
            {
                F[0] = -1;
                for (int j = 0; j <= q; j++)
                {
                    double S1 = 0;
                    for (int i = 0; i <= p; i++)
                    {
                        S1 += F[i] * F[i] * aACov[j];
                    }
                    double S2 = 0;
                    for (int i = 1; i <= p; i++)
                    {
                        double S3 = 0;
                        for (int k = 0; k <= p - i; k++)
                        {
                            S3 += F[k] * F[i + k];
                        }
                        S3 *= (aACov[j + i] + aACov[j - i]);
                        S2 += S3;
                    }
                    newACov[j] = S1 + S2;
                }
            }
            //------------------------------   //------------------------------
            //    s += F[i]*aACov[i]; } SgmA2 = aACov[0] - s; }else{ SgmA2 = newACov[0]; }
            // оценка SgmA2
            if (q == 0)
            {
                double s = 0;
                for (int i = 1; i <= p; i++)
                {
                    s += F[i] * newACov[i];
                }
                stSgmA2 = newACov[0] - s;
            }
            else
            {
                stSgmA2 = newACov[0];
            }
            //------------------------------   //------------------------------
            // оценка Tetta0
            if (p == 0)
            {
                stTetta = ZDM.clcMid();
            }
            else
            {
                double Wstrh = ZDM.clcMid();
                double s = 0;
                for (int i = 1; i <= p; i++)
                {
                    s += F[i];
                }
                stTetta = Wstrh * (1 - s);
            }
            //------------------------------   //------------------------------
            return newACov;
        }

        //---------------------------------------------------------------------------
        private qData forecast(List<double> aModel, List<double> ZDM, ref List<double> aA, int StPr)
        {
            int L = 0; //куда прогнозировать
            List<double> Zdm = new List<double>(); // ряд исхлдный
            List<double> W = new List<double>(); // ряд прогнозов
            List<double> A = new List<double>(); // ряд ошибок
            List<double> ZW = new List<double>(); // ряд данных и прогнозов
            List<double> Mod = new List<double>();
            Zdm = ZDM;
            W = ZDM;
            ZW = ZDM;
            //W = std::vector<double> (Zdm.size(),0);
            A = new List<double>(Zdm.Count);
            Mod = aModel;
            int p;
            p = this.P;
            int q;
            q = this.Q;

            try
            {
                for (int i = 0; i < q; i++)
                {
                    A[i] = (Zdm[i] / 100.0) * ((i + 3) % 10);
                    //      A[i] = (Zdm[i]/100.0)*15.0;
                }
                int MaxI = W.Count;
                int MinI = (p > q != false ? p : q);

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

                for (int i = MinI; i < MaxI; i++)
                {
                    double S1 = 0;
                    for (int j = 0; j < p; j++)
                    {
                        //   S1 += (-Mod[j])*Zdm[i-(j+1)];
                        S1 += Mod[j] * Zdm[i - (j + 1)];
                    }
                    double S2 = 0;
                    for (int j = 0; j < q; j++)
                    {
                        int ind;
                        ind = j + p;
                        S2 += (-Mod[ind]) * A[i - (1 + j)];
                    }

                    //      try{
                    W[i] = S1 + S2;
                    A[i] = Zdm[i] - W[i];
                    //      }catch(...){
                    //      int I = i;      //      }
                }
                L = this.NumPointPrognos;
                for (int i = MaxI; i < MaxI + L; i++)
                {
                    double S1 = 0;
                    for (int j = 0; j < p; j++)
                    {
                        //   S1 += (-Mod[j])*W[i-(j+1)];
                        //   S1 += Mod[j]*W[i-(j+1)];
                        S1 += Mod[j] * ZW[i - (j + 1)];
                    } //переписать: W заменить на ZDM для тех индексов где возможно
                    double S2 = 0;
                    for (int j = 0; j < q; j++)
                    {
                        int ind;
                        ind = j + p;
                        S2 += (-Mod[ind]) * A[i - (1 + j)];
                    } //S2 += Mod[j]*A[i-(j+1-p)];

                    S1 = S1 + S2;
                    W.Add(S1);
                    ZW.Add(S1);
                    A.Add(0);
                }
            }
            catch
            {
                throw new System.ApplicationException("Исключение при вычислении прогноза"); ///
                //return ZDM;
            }
            aA = A;
            qData tData = new qData(W);
            return tData;
        }
        // в аА вернет ошибку

        private qModelDif Dif = new qModelDif();
        private qModelDis Dis = new qModelDis();
        private bool flagDs; // флаг наличия сезонной разности
        // устанавливаеся при установке Ds Ss
        // нужен для недопущения обновления сезонной разности
        private int Ds; // порядок разности сезонной
        private int Ss; // период разности сезонной

        private int P; // всего параметров вместе с нулями
        private int Q; // всего параметров вместе с нулями
        private int Np; // число параметров
        private int Nq; // число параметров

        private int D; // порядок разности
        //-------------
        private List<int> Mask = new List<int>(); // маска задает 1 сезонные параметры
        private List<int> MaskAR = new List<int>(); // устанавливаются в методах setP setPs
        private List<int> MaskMA = new List<int>(); // устанавливаются в методах setQ setQs
        // для метода revisionParam
        //-------------
        private qData Model = new qData();
        private qData ModelAR = new qData();
        private qData ModelMA = new qData();
        //-------------
        private double Tetta0;
        private qData ZDM = new qData(); // данные для анализа - без среднего
        //-------------
        private qACorr ACorr = new qACorr();
        private qPCorr PCorr = new qPCorr();
        private int kASgm;
        private int kPSgm;
        //-------------
        private double stSgmA2; //вычисляются в clcNewCov
        private double stTetta; //вычисляются в clcNewCov
    }
}