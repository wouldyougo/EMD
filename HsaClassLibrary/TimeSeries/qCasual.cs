using System.Collections.Generic;
using System;

namespace TimeSeries
{
    //#include "UMatrix.h"
    public class qKritZix : qModel
    {
        public qKritZix()
        {
            ModelName = "Проверка случайности.";
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
        //#pragma package(smart_init)
        //---------------------------------------------------------------------------
        //qKritZix::
        //---------------------------------------------------------------------------
        public qKritZix(qKritZix aKritZix)
        {
            if (this != aKritZix)
            {
                //this = aKritZix;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setZix(ref qKritZix aKritZix)
        {
            if (this != aKritZix)
            {
                //this = aKritZix;
                throw new System.NotImplementedException();
            }
        }
        //++++++++++++++++++++++++++
        //void clear(void);
        //void set(qData aData);
        //qData getA(void);
        //qData getPrognos(void){;}
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
        public void clcZix(bool a, bool b, bool c, bool d)
        {
            /*
              ofstream @out = new ofstream("tmpKrit1.rpt");
             if(a)
             {
                clcZix1(ref @out);
             }
             if(b)
             {
                clcZix2(ref @out);
             }
             if(c)
             {
                clcZix3(ref @out);
             }
             if(d)
             {
                clcZix4(ref @out);
             }
             @out.close();
            */
            throw new System.NotImplementedException();
        }
        //void clcBestPoryd(bool prognos);        //подобрать лучшее бетта для текущго порядка
        //void setPoryd(unsigned aPoryd);
        //unsigned getPoryd(void);
        //qData getParam(void);
        //---------------------------------------------------------------------------
        public int clcZixSeas()
        {
            List<double> VData = new List<double>();
            VData = this.Data.get();
            int dataSize = 0;
            dataSize = VData.Count;
            List<double> Vx = new List<double>(dataSize);
            for (int i = 1; i < dataSize - 1; i++)
            {
                if ((VData[i - 1] < VData[i]) && (VData[i] > VData[i + 1]))
                {
                    Vx[i] = 1;
                }
                if ((VData[i - 1] > VData[i]) && (VData[i] < VData[i + 1]))
                {
                    Vx[i] = -1;
                }
            }
            List<double> Vf = new List<double>(dataSize - 3);
            int count = 0;
            for (int i = 1; i < dataSize - 1; i++)
            {
                if (Vx[i] != 0)
                {
                    for (int j = i + 1; i < dataSize - 1; j++)
                    {
                        if (Vx[j] != 0)
                        {
                            break;
                        }
                        count++;
                        if (j == dataSize - 1)
                        {
                            count = -1;
                        }
                    }
                    if (count != -1)
                    {
                        Vf[count] += 1;
                    }
                }
                count = 0;
            }
            int Pf = 0; // число фаз
            for (int i = 0; i < dataSize - 3; i++)
            {
                Pf += (int)Vf[i];
            }
            double MPf = 0;
            double DPf = 0;
            double SPf = 0;
            MPf = 2 * ((2 * dataSize - 7) / 6.0);
            DPf = (16 * dataSize - 29) / 90.0;
            SPf = Math.Sqrt(DPf);
            int num = 0;
            int index = 0;
            for (int i = 0; i < dataSize - 3; i++)
            {
                if (Vf[i] != 0)
                {
                    if (Vf[i] >= num)
                    {
                        num = (int)Vf[i];
                        index = i + 1;
                    }
                }
            }
            return index;
        }
        //---------------------------------------------------------------------------
        //private void clcZix1(ref std.ofstream @out)
        private void clcZix1()
        {
            List<double> VData = new List<double>();
            VData = this.Data.get();
            int dataSize = 0;
            dataSize = VData.Count;
            List<double> Vx = new List<double>(dataSize);
            for (int i = 1; i < dataSize - 1; i++)
            {
                if ((VData[i - 1] < VData[i]) && (VData[i] > VData[i + 1]))
                {
                    Vx[i] = 1;
                }
                if ((VData[i - 1] > VData[i]) && (VData[i] < VData[i + 1]))
                {
                    Vx[i] = -1;
                }
            }
            int P = 0; // число поворотных точек
            for (int i = 1; i < dataSize - 1; i++)
            {
                if (Vx[i] != 0)
                {
                    P++;
                }
            }
            double MP = 0;
            double DP = 0;
            double SP = 0;
            MP = (2.0 / 3.0) * (dataSize - 2);
            DP = (16 * dataSize - 29) / 90.0;
            SP = Math.Sqrt(DP);
            /*
            @out << "Проверка случайности данных.";
            @out << "\n";
            @out << "\n";
            @out << "Критерий поворотных точек.";
            @out << "\n";
            @out << "\n";
            @out << "Число поворотных точек: ";
            @out << P;
            @out << "\n";
            @out << "Ожидаемое число поворотных точек: ";
            @out << MP;
            @out << "\n";
            @out << "Дисперсия числа поворотных точек: ";
            @out << DP;
            @out << "\n";
            @out << "Среднеквадратичное отклонение числа поворотных точек: ";
            @out << SP;
            @out << "\n";
            @out << "Доверительный интервал: ";
            @out << "\t";
            @out << "[";
            @out << MP - SP;
            @out << ";";
            @out << MP + SP;
            @out << "]";
            @out << "\n";
            */
            if ((MP - SP <= P) && (P <= MP + SP))
            {
                //@out << "Ряд случайный.";
                //@out << "\n";
            }
            else
                if (MP < P)
                {
                    //@out << "Последовательные значения ряда отрицательно коррелированы.";
                    //@out << "\n";
                }
                else
                {
                    //@out << "Последовательные значения ряда положительно коррелированы.";
                    //@out << "\n";
                }
        }
        //---------------------------------------------------------------------------
        //private void clcZix2(ref std.ofstream @out)
        private void clcZix2()
        {
            List<double> VData = new List<double>();
            VData = this.Data.get();
            int dataSize = 0;
            dataSize = VData.Count;
            List<double> Vx = new List<double>(dataSize);
            for (int i = 1; i < dataSize - 1; i++)
            {
                if ((VData[i - 1] < VData[i]) && (VData[i] > VData[i + 1]))
                {
                    Vx[i] = 1;
                }
                if ((VData[i - 1] > VData[i]) && (VData[i] < VData[i + 1]))
                {
                    Vx[i] = -1;
                }
            }
            List<double> Vf = new List<double>(dataSize - 3);
            int count = 0;
            for (int i = 1; i < dataSize - 1; i++)
            {
                if (Vx[i] != 0)
                {
                    for (int j = i + 1; i < dataSize - 1; j++)
                    {
                        if (Vx[j] != 0)
                        {
                            break;
                        }
                        count++;
                        if (j == dataSize - 1)
                        {
                            count = -1;
                        }
                    }
                    if (count != -1)
                    {
                        Vf[count] += 1;
                    }
                }
                count = 0;
            }
            int Pf = 0; // число фаз
            for (int i = 0; i < dataSize - 3; i++)
            {
                Pf += (int)Vf[i];
            }
            double MPf = 0;
            double DPf = 0;
            double SPf = 0;
            MPf = 2 * ((2 * dataSize - 7) / 6.0);
            DPf = (16 * dataSize - 29) / 90.0;
            SPf = Math.Sqrt(DPf);

            //@out << "Критерий длин фаз.";

            for (int i = 0; i < dataSize - 3; i++)
            {
                if (Vf[i] != 0)
                {
                    /*
                     @out << "Число фаз длины ";
                    @out << i+1;
                    @out << ": ";
                    @out << Vf[i];
                    @out << "\n";
                     */
                }
            }
            /*
             @out << "Число фаз: ";
            @out << Pf;
            @out << "\n";
            @out << "Ожидаемое число фаз: ";
            @out << MPf;
            @out << "\n";
            @out << "Дисперсия числа фаз: ";
            @out << DPf;
            @out << "\n";
            @out << "Среднеквадратичное отклонение числа фаз: ";
            @out << SPf;
            @out << "\n";
            @out << "Доверительный интервал: ";
            @out << "\t";
            @out << "[";
            @out << MPf - SPf;
            @out << ";";
            @out << MPf + SPf;
            @out << "]";
            @out << "\n";
             */
            //oooooooooooooooooooooooooooooo
            if ((MPf - SPf <= Pf) && (Pf <= MPf + SPf))
            {
                //@out << "Ряд случайный.";
                //@out << "\n";
                ;
            }
            else
            {
                //@out << "Ряд не случайный.";
                //@out << "\n";
                ;
            }
        }
        //---------------------------------------------------------------------------
        //private void clcZix3(ref std.ofstream @out)
        private void clcZix3()
        {
            List<double> VData = new List<double>();
            VData = this.Data.get();
            int dataSize = 0;
            dataSize = VData.Count;
            List<double> Vx = new List<double>(dataSize);
            for (int i = 1; i < dataSize; i++)
            {
                if (VData[i - 1] < VData[i])
                {
                    Vx[i] = 1;
                }
            }
            int P = 0; // число точек возрастания
            for (int i = 1; i < dataSize; i++)
            {
                if (Vx[i] != 0)
                {
                    P++;
                }
            }
            double MP = 0;
            double DP = 0;
            double SP = 0;
            MP = 0.5 * (dataSize - 1);
            DP = (dataSize + 1) / 12.0;
            SP = Math.Sqrt(DP);
            /*
            @out << "\n";
            @out << "Критерий знаков разностей.";
            @out << "\n";
            @out << "\n";
            @out << "Число точек возрастания ряда: ";
            @out << P;
            @out << "\n";
            @out << "Ожидаемое число точек возрастания ряда: ";
            @out << MP;
            @out << "\n";
            @out << "Дисперсия числа точек возрастания ряда: ";
            @out << DP;
            @out << "\n";
            @out << "Среднеквадратичное отклонение числа точек возрастания ряда: ";
            @out << SP;
            @out << "\n";
            @out << "Доверительный интервал: ";
            @out << "\t";
            @out << "[";
            @out << MP - SP;
            @out << ";";
            @out << MP + SP;
            @out << "]";
            @out << "\n";
            */
            if ((MP - SP <= P) && (P <= MP + SP))
            {
                //@out << "Ряд случайный.";
                //@out << "\n";
            }
            else
                if (MP - SP > P)
                {
                    //@out << "Ряд не случайный.";
                    //@out << "\n";
                    //@out << "Критерий указывает на наличие убывающего тренда.";
                    //@out << "\n";
                }
                else
                {
                    //@out << "Ряд не случайный.";
                    //@out << "\n";
                    //@out << "Критерий указывает на наличие возрастающего тренда.";
                    //@out << "\n";
                }
        }
        //---------------------------------------------------------------------------
        //private void clcZix4(ref std.ofstream @out)
        private void clcZix4()
        {
            List<double> VData = new List<double>();
            VData = this.Data.get();
            int dataSize = 0;
            dataSize = VData.Count;
            List<double> Vx = new List<double>(dataSize);
            for (int i = 1; i < dataSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (VData[j] < VData[i])
                    {
                        Vx[i]++;
                    }
                }
            }
            int P = 0; // число случаев, когда последующее значение ряда больше предыдущих
            for (int i = 1; i < dataSize; i++)
            {
                P += (int)Vx[i];
            }
            double MP = 0;
            double KK = 0;
            double DK = 0;
            double SK = 0;
            MP = 0.25 * (dataSize - 1) * dataSize;
            KK = 4.0 * P / (double)((dataSize - 1) * dataSize) - 1;
            DK = 2.0 * (2.0 * dataSize + 5) / (double)((dataSize - 1) * dataSize * 9);
            SK = Math.Sqrt(DK);
            /*
            @out << "\n";
            @out << "Критерий ранговой корреляции.";
            @out << "\n";
            @out << "\n";
            @out << "Значени параметра: ";
            @out << P;
            @out << "\n";
            @out << "Ожидаемое значение параметраы: ";
            @out << MP;
            @out << "\n";
            @out << "Коэффициент Кендэла: ";
            @out << KK;
            @out << "\n";
            @out << "Дисперсия  коэффициента: ";
            @out << DK;
            @out << "\n";
            @out << "Среднеквадратичное отклонение  коэффициента: ";
            @out << SK;
            @out << "\n";
            @out << "Доверительный интервал: ";
            @out << "\t";
            @out << "[";
            @out << -SK;
            @out << ";";
            @out << SK;
            @out << "]";
            @out << "\n";
            */
            if ((-SK <= KK) && (KK <= +SK))
            {
                //@out << "Ряд случайный.";
                //@out << "\n";
            }
            else
                if (-SK > KK)
                {
                    //@out << "Ряд не случайный.";
                    //@out << "\n";
                    //@out << "Критерий указывает на наличие убывающего тренда.";
                    //@out << "\n";
                }
                else
                {
                    //@out << "Ряд не случайный.";
                    //@out << "\n";
                    //@out << "Критерий указывает на наличие возрастающего тренда.";
                    //@out << "\n";
                }
        }
    }
    //---------------------------------------------------------------------------

    //#include "UMatrix.h"

    //---------------------------------------------------------------------------
    public class qHist : qModel
    {
        public qHist()
        {
            ModelName = "Гисторамма.";
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public qHist(qHist aHist)
        {
            if (this != aHist)
            {
                //this = aHist;
                throw new System.NotImplementedException();
            }
        }
        //---------------------------------------------------------------------------
        public void setHist(ref qHist aHist)
        {
            if (this != aHist)
            {
                //this = aHist;
                throw new System.NotImplementedException();
            }
        }

        //---------------------------------------------------------------------------
        public void setPoryd(int aPoryd)
        {
            Poryd = aPoryd;
            if ((Poryd > 16))
            {
                Poryd = 1;
                throw new System.ApplicationException("(Poryd > 16) qHist::setPoryd порядок задан неверно");
            }
        }
        //---------------------------------------------------------------------------
        public int getPoryd()
        {
            return Poryd;
        }
        //++++++++++++++++++++++++++
        //void clear(void);
        //void set(qData aData);
        //qData getA(void);
        //qData getPrognos(void){;}
        public override void mkModel(int Metod)
        {
            ;
        }
        public new void mkPrognos(int NumPrognos)
        {
            ;
        }
        //---------------------------------------------------------------------------
        public void clcHist()
        {
            List<double> VData = new List<double>();
            int dataSize = 0;
            VData = this.Data.get();
            dataSize = VData.Count;

            int K = Poryd;
            double min = 0;
            double max = 0;

            VHist = new List<int>(K);
            //min = *(std.min_element(VData.GetEnumerator(), VData.end()));
            //max = *(std.max_element(VData.GetEnumerator(), VData.end()));
            //найти минимумы и максимумы
            throw new System.NotImplementedException();
            delta = (max - min) / (double)(K);

            for (int i = 0; i < dataSize; i++)
            {
                for (int j = 1; j <= K; j++)
                {
                    if (j == K)
                    {
                        VHist[j - 1]++;
                        break;
                    }
                    if (VData[i] <= min + delta * j)
                    {
                        VHist[j - 1]++;
                        break;
                    }
                }
            }
            VDiap = new List<double>(K + 1);
            for (int i = 0; i < K + 1; i++)
            {
                VDiap[i] = min + delta * i;
            }
            List<double> Proc = new List<double>(K);
            for (int i = 0; i < K; i++)
            {
                Proc[i] = VHist[i] / (double)(delta * dataSize);
            }
            /*
             ofstream @out = new ofstream();
             @out.open("tmpHist.rpt");
             if(!@out.is_open())
                 ShowMessage("Ошибка при создании файла");
             @out << "Число интервалов: ";
             @out << "\n";
             @out << K;
             @out << "\n";
             @out << "\n";
             @out << "Число попаданий в диапазон: ";
             @out << "\n";
             for(int i = 0;i < K;i++)
             {
                 @out << i;
                 @out << ": ";
                 @out << VHist[i];
                 @out << "\n";
             }
   
             @out << "\n";
             @out << "Процент попаданий в диапазон: ";
             @out << "\n";
             for(int i = 0;i < K;i++)
             {
                 @out << i;
                 @out << ": ";
                 @out << Proc[i];
                 @out << "\n";
             }
             @out << "\n";
             @out << "Диапазон интервала: ";
             @out << "\n";
             for(int i = 0;i < K;i++)
             {
                 @out << i;
                 @out << ": ";
                 @out << "[";
                 @out << VDiap[i];
                 @out << ";  ";
                 @out << VDiap[i+1];
                 @out << "]";
                 @out << "\n";
             }
             @out.close();
             */
        }
        //---------------------------------------------------------------------------

        //---------------------------------------------------------------------------
        public void clcDiagr()
        {
            List<double> VData = new List<double>();
            int size = 0;
            VData = this.Data.get();
            size = VData.Count;

            if (DiaPor == 0)
            {
                if (DiaPer == 0)
                {
                    DiaPer = 1;
                }
                DiaPor = size / DiaPer;
                if ((size % DiaPer) != 0)
                {
                    DiaPor++;
                }
            }

            VDiagr = new List<double>(DiaPor);
            //   int MinI = BegPoi;

            int MinJ = BegPoi % DiaPer;
            int count = BegPoi / DiaPer;
            int j = 0;
            //for(int i = 0; i < size; i)
            for (int i = 0; i < size; )
            {
                double Sum = 0;
                try
                {
                    for (j = MinJ; j < DiaPer; j++)
                    {
                        Sum = Sum + VData[i];
                        i++;
                    }
                    MinJ = 0;
                }
                catch
                {
                }
                VDiagr[count] = VDiagr[count] + Sum;
                count++;
                count = count % DiaPor;
            }
        }
        //---------------------------------------------------------------------------
        public void setDiaPor(int aPoryd)
        {
            DiaPor = aPoryd;
        }
        //---------------------------------------------------------------------------
        public int getDiaPor()
        {
            return DiaPor;
        }
        //---------------------------------------------------------------------------
        public void setDiaPer(int aPoryd)
        {
            DiaPer = aPoryd;
        }
        //---------------------------------------------------------------------------
        public int getDiaPer()
        {
            return DiaPer;
        }
        //---------------------------------------------------------------------------
        public void setBegPoi(int aPoryd)
        {
            BegPoi = aPoryd;
        }
        //---------------------------------------------------------------------------
        public int getBegPoi()
        {
            return BegPoi;
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public void clcSkat()
        {
        }

        private int Poryd;
        private List<int> VHist = new List<int>();
        private List<double> VDiap = new List<double>();
        private double delta;

        private List<double> VDiagr = new List<double>();
        private int DiaPor;
        private int DiaPer;
        private int BegPoi;
    }
}