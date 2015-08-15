using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsaClassLibrary.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// Получить рандомный вектор
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="max">максимальное значение</param>
        /// <returns></returns>
        public static IList<double> getRandom(int n, int max)
        {
            Random rand = new Random(0);
            IList<double> b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = Convert.ToDouble(rand.Next(max));
                //throw new System.NotImplementedException();
            }
            return b;
        }

        /// <summary>
        /// Получить вектор sin
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Sin(i)</param>
        /// <param name="b">значение множителя аргумента Sin(i*b)</param>/// 
        /// <returns></returns>
        public static IList<double> getSin(int n, double a = 1, double b = 1)
        {
            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Sin(i);
                data[i] = a * Math.Sin((double)i * b);                
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор a*Cos(b1*x + a2*Cos(b2x))
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b1">значение множителя аргумента Cos(i*b)</param>/// 
        /// <param name="b2">значение множителя аргумента Cos(i*b)</param>/// /// 
        /// <returns></returns>
        public static IList<double> getCosPM(int n, double a = 1, double b1 = 1, double a2 = 1, double b2 = 1)
        {
            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Cos(i);
                data[i] = 1 + a * Math.Cos((double)i * b1 + a2 * Math.Cos((double)i * b2));
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор a*Cos(b1*x*Cos(b2x))
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b1">значение множителя аргумента Cos(i*b)</param>/// 
        /// <param name="b2">значение множителя аргумента Cos(i*b)</param>/// /// 
        /// <returns></returns>
        public static IList<double> getCosFM(int n, double a = 1, double b1 = 1, double b2 = 1)
        {
            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Cos(i);
                data[i] = 1 + a * Math.Cos((double)i * b1 * Math.Cos((double)i * b2));
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор a*Cos(b1*x)Cos(b2*x)
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b1">значение множителя аргумента Cos(i*b)</param>/// 
        /// <param name="b2">значение множителя аргумента Cos(i*b)</param>/// /// 
        /// <returns></returns>
        public static IList<double> getCosAM(int n, double a = 1, double b1 = 1, double b2 = 1)
        {
            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Cos(i);
                data[i] = 1 + a * Math.Cos((double)i * b1) * Math.Cos((double)i * b2);
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор Cos
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b">значение множителя аргумента Cos(i*b)</param>/// 
        /// <returns></returns>
        public static IList<double> getCos(int n, double a = 1, double b = 1)
        {
            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Cos(i);
                data[i] = a * Math.Cos((double)i * b);
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор Cos
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b">значение множителя аргумента Cos(i*b)</param>/// 
        /// <param name="c">значение фазы Cos(i*b + с)</param>/// /// 
        /// <returns></returns>
        public static IList<double> getCos(int n, Func<double, double> a, Func<double, double> b, Func<double, double> c)
        {

            IList<double> data = new double[n];
            for (int i = 0; i < n; i++)
            {
                //data[i] = a * Math.Cos(i);
                data[i] = a(i) * Math.Cos((double)i * b(i) + c(i));
                //data[i] = Cos((double)i, a, b, c);
                //throw new System.NotImplementedException();
            }
            return data;
        }

        /// <summary>
        /// Получить вектор Cos
        /// </summary>
        /// <param name="n">номер отсчета</param>
        /// <param name="a">значение амплитуды a*Cos(i)</param>
        /// <param name="b">значение множителя аргумента Cos(i*b)</param>/// 
        /// <param name="c">значение фазы Cos(i*b + с)</param>/// /// 
        /// <returns></returns>
        public static double Cos(double i, Func<double, double> a, Func<double, double> b, Func<double, double> c)
        {
            double data = a(i) * Math.Cos(i * b(i) + c(i));
            return data;
        }

        /// <summary>
        /// Разность source1(i) - source2(i)
        /// размеры векторов должны совпадать
        /// </summary>
        /// <param name="source1">Уменьшаемое</param>
        /// <param name="source2">Вычитаемое</param>
        /// <returns>Разность</returns>
        public static IList<double> Sub(IList<double> source1, IList<double> source2)
        {
            //if(source1.Count != source2.Count)
            //{
            //    throw new System.ApplicationException("Размеры векторов должны совпадать");
            //}
            IList<double> value = new double[source1.Count];
            for (int i = 0; i < source1.Count; i++)
            {
                value[i] = source1[i] - source2[i];
            }
            return value;
        }

        /// <summary>
        /// Сумма векторов source1(i) + source2(i)
        /// размеры векторов должны совпадать
        /// </summary>
        /// <param name="source1">Слагаемое</param>
        /// <param name="source2">Слагаемое</param>
        /// <returns>Сумма</returns>
        public static IList<double> Sum(IList<double> source1, IList<double> source2)
        {
            //if(source1.Count != source2.Count)
            //{
            //    throw new System.ApplicationException("Размеры векторов должны совпадать");
            //}
            //IIList<double> value0 = new double[50];
            //IIList<IIList<double>> value1 = new IIList<double>[2];
            IList<double> value = new double[source1.Count];
            for (int i = 0; i < source1.Count; i++)
            {
                value[i] = source1[i] + source2[i];
            }
            return value;
        }

        /// <summary>
        /// Умножение вектора на скаляр: source1(i) * m
        /// </summary>
        /// <param name="source1">Входной ряд</param>
        /// <param name="m">Множитель</param>
        /// <returns>Вектор умноженный на скаляр</returns>
        public static IList<double> Mul(IList<double> source1, double m)
        {
            IList<double> value = new double[source1.Count];
            for (int i = 0; i < source1.Count; i++)
            {
                value[i] = source1[i] * m;
            }
            return value;
        }

        /// <summary>
        /// Умножение векторов покомпонентно 
        /// </summary>
        /// <param name="source1">Входной ряд 1</param>
        /// <param name="source2">Входной ряд 2</param>/// 
        /// <returns>Результирующий вектор</returns>
        public static IList<double> Mul(IList<double> source1, IList<double> source2)
        {
            IList<double> value = new double[source1.Count];
            for (int i = 0; i < source1.Count; i++)
            {
                value[i] = source1[i] * source2[i];
            }
            return value;
        }

        /// <summary>
        /// Интерполяция кубическим сплайном
        /// </summary>
        /// <param name="x">Вектор X</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="x_new">Вектор X новая сетка</param>
        /// <returns>Вектор Y новая сетка</returns>        
        public static IList<double> Interpolation_3(IList<double> x, IList<double> y, IList<double> x_new)
        {
            //throw new NotImplementedException();
            double[] x_old = x.ToArray<double>();
            double[] y_old = y.ToArray<double>();
            double[] x_2 = x_new.ToArray<double>();
            double[] y_2;

            alglib.spline1dconvcubic(x_old, y_old, x_2, out y_2);

            //return y_2.ToIList<double>();
            return y_2;
        }

        /// <summary>
        /// throw new System.NotImplementedException();
        /// Квадратичная интерполяция
        /// </summary>
        /// <param name="x">Вектор X</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="x_new">Вектор X новая сетка</param>
        /// <returns>Вектор Y новая сетка</returns>        
        public IList<double> Interpolation_2(IList<double> x, IList<double> y, IList<double> x_new)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throw new System.NotImplementedException();
        /// Линейная интерполяция
        /// </summary>
        /// <param name="x">Вектор X</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="x_new">Вектор X новая сетка</param>
        /// <returns>Вектор Y новая сетка</returns>        
        public IList<double> Interpolation_1(IList<double> x, IList<double> y, IList<double> x_new)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throw new System.NotImplementedException();
        /// посмотреть как должна работать функция нормализации 
        /// </summary>
        /// <param name="asignal"></param>
        /// <returns></returns>
        public static List<double> getNormalize(List<double> asignal)
        {
            //посмотреть как должна работать функция нормализации
            throw new System.NotImplementedException();

            List<double> tmp = new List<double>(asignal.Count);
            double max_data = asignal.Max();
            if (max_data != 0)
            {
                for (int i = 0; i < asignal.Count; i++)
                {
                    tmp[i] = asignal[i] / max_data;
                }
            }
            return tmp;
        }
        /// <summary>
        /// Вычисление разности source1[i] - source1[i-1]
        /// </summary>
        /// <param name="source1">Входной ряд</param>
        /// <returns>Разность source1[i] - source1[i-1]</returns>
        public static IList<double> Difference(IList<double> source1)
        {
            IList<double> value = new double[source1.Count];
            if (source1.Count > 0)
            {
                value[0] = source1[0];
            }
            for (int i = 1; i < source1.Count; i++)
            {
                value[i] = source1[i] - source1[i-1];
            }
            return value;
        }
        /// <summary>
        /// Восстановление из разности source1[i] - source1[i-1]
        /// </summary>
        /// <param name="source1">Входной ряд</param>
        /// <returns>Восстановление из разности source1[i] - source1[i-1]</returns>
        public static IList<double> Integration(IList<double> source1)
        {
            IList<double> value = new double[source1.Count];
            if (source1.Count > 0)
            {
                value[0] = source1[0];
            }
            for (int i = 1; i < source1.Count; i++)
            {
                value[i] = value[i - 1] + source1[i];
            }
            return value;
        }

        /// <summary>
        /// Матожидание
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Average(IList<double> v)
        {
            if (v.Count != 0)
            {
                double M = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    M += v[i];
                }
                M /= v.Count;
                return M;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Дисперсия
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Дисперсия</returns>
        public static double Variance(IList<double> v)
        {
            if (v.Count != 0)
            {
                double M;
                M = MathHelper.Average(v);

                double D = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    D += (v[i] - M) * (v[i] - M);
                }
                D /= v.Count;
                return D;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Стандартное отклонение
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Стандартное отклонение</returns>
        public static double StandardDeviation(IList<double> v)
        {
            if (v.Count != 0)
            {
                double M;
                M = MathHelper.Average(v);

                double D = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    D += (v[i] - M) * (v[i] - M);
                }
                D /= v.Count;
                return Math.Sqrt(D);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Рассчитать SMA (простое скользящее среднее) для текущего бара 
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="curbar">Номер текущего бара</param>
        /// <param name="period">Период SMA (простого скользящего среднего)</param>
        /// <returns></returns>
        public static double SMA(IList<double> candles, int curbar, int period)
        {
            int num = curbar - period + 1;
            if (num < 0)
            {
                num = 0;
            }
            period = curbar - num + 1;
            double num2 = 0.0;
            while (num <= curbar && num < candles.Count)
            {
                num2 += candles[num++];
            }
            return num2 / (double)Math.Min(period, curbar + 1);
        }

        /// <summary>
        /// Рассчитать SMA (простое скользящее среднее) для входящего списка 
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="period">Период SMA (простого скользящего среднего)</param>
        /// <returns></returns>       
        public static IList<double> SMA(IList<double> candles, int period)
        {
            int count = candles.Count;
            double[] array = new double[count];
            if (period < 4)
            {
                for (int i = 0; i < count; i++)
                {
                    array[i] = MathHelper.SMA(candles, i, period);
                }
            }
            else
            {
                int num = Math.Min(count, period);
                double num2 = 0.0;
                for (int j = 0; j < num; j++)
                {
                    num2 += candles[j];
                    array[j] = num2 / (double)(j + 1);
                }
                for (int k = num; k < count; k++)
                {
                    double num3 = candles[k];
                    double num4 = candles[k - period];
                    double num5 = array[k - 1];
                    array[k] = num5 + (num3 - num4) / (double)period;
                }
            }
            return array;
        }
        /// <summary>
        /// Рассчитать Стандартное Отклонение для текущего бара
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="smas">Список посчитанных SMA</param>
        /// <param name="curbar">Номер текущего бара</param>
        /// <param name="period">Период SMA (простого скользящего среднего)</param>
        /// <returns>StDev</returns>
        public static double StDev(IList<double> candles, IList<double> smas, int curbar, int period)
        {
            int num = curbar - period + 1;
            if (num < 0)
            {
                num = 0;
            }
            period = curbar - num + 1;
            double num2 = 0.0;
            while (num <= curbar && num < candles.Count)
            {
                double num3 = candles[num] - smas[curbar];
                num2 += num3 * num3;
                num++;
            }
            int num4 = Math.Min(period, curbar + 1);
            num4 = Math.Max(2, num4);
            return Math.Sqrt(num2 / (double)(num4 - 1));
        }

        /// <summary>
        /// Рассчитать StDev (Стандартное Отклонение) для входящего списка
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="period">Период SMA (простого скользящего среднего)</param>
        /// <returns>Список StDev</returns>
        public static IList<double> StDev(IList<double> candles, int period)
        {
            int count = candles.Count;
            double[] array = new double[count];
            IList<double> smas = MathHelper.SMA(candles, period);
            for (int i = 0; i < count; i++)
            {
                array[i] = MathHelper.StDev(candles, smas, i, period);
            }
            return array;
        }
        /// <summary>
        /// Рассчитать периодическое StDev (Стандартное Отклонение) для входящего списка
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="period">Список Периодов SMA (простого скользящего среднего)</param>
        /// <returns>Список PStDev</returns>
        public static IList<double> PeriodicStandardDeviation(IList<double> candles, IList<int> period)
        {
            int count = candles.Count;
            double[] array = new double[count];
            IList<double> smas = MathHelper.PeriodicMovingAverage(candles, period);
            for (int i = 0; i < count; i++)
            {
                array[i] = MathHelper.StDev(candles, smas, i, period[i]);
            }
            return array;
        }

        /// <summary>
        /// Рассчитать периодическое SMA (простое скользящее среднее) для входящего списка 
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="period">Список Периодов SMA (простого скользящего среднего)</param>
        /// <returns>Список PMA</returns>       
        public static IList<double> PeriodicMovingAverage(IList<double> candles, IList<int> period)
        {
            int count = candles.Count;
            double[] array = new double[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = MathHelper.SMA(candles, i, period[i]);
            }

            return array;
        }

        /// <summary>
        /// Рассчитать MA (скользящее среднее) для входящего списка 
        /// </summary>
        /// <param name="candles">Входящий список</param>
        /// <param name="period">Список Периодов SMA (простого скользящего среднего)</param>
        /// <param name="MovingAverageFunc">функция вичисления i-го значения MA</param>
        /// <returns>Список MA</returns>
        public static IList<double> BaseMovingAverage(IList<double> candles, IList<int> period, Func<IList<double>, int, int, double> MovingAverageFunc)
        {
            int count = candles.Count;
            double[] array = new double[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = MovingAverageFunc(candles, i, period[i]);
            }

            return array;
        }

        /// <summary>
        /// hi^2 95%
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public static double getXi2_95(int ind)
        {
            List<double> xi2_95 = new List<double>(30);
            xi2_95[0] = 3.84;
            xi2_95[1] = 5.99;
            xi2_95[2] = 7.81;
            xi2_95[3] = 9.49;
            xi2_95[4] = 11.1;
            xi2_95[5] = 12.6;
            xi2_95[6] = 14.1;
            xi2_95[7] = 15.5;
            xi2_95[8] = 16.9;
            xi2_95[9] = 18.3;
            xi2_95[10] = 19.7;
            xi2_95[11] = 21;
            xi2_95[12] = 22.4;
            xi2_95[13] = 23.7;
            xi2_95[14] = 25;
            xi2_95[15] = 26.3;
            xi2_95[16] = 27.6;
            xi2_95[17] = 28.9;
            xi2_95[18] = 30.1;
            xi2_95[19] = 31.4;
            xi2_95[20] = 32.7;
            xi2_95[21] = 33.9;
            xi2_95[22] = 35.2;
            xi2_95[23] = 36.4;
            xi2_95[24] = 37.7;
            xi2_95[25] = 38.9;
            xi2_95[26] = 40.1;
            xi2_95[27] = 41.3;
            xi2_95[28] = 42.6;
            xi2_95[29] = 43.8;
            if (ind > 30)
            {
                ind = 30;
            }
            if (ind == 0)
            {
                ind = 1;
            }
            return xi2_95[ind - 1];
        }

    }
}
