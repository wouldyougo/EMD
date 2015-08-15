using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Decomposition
{
    /// <summary>
    /// Критерий останова
    /// </summary>
    public class StopCondition : IStopCondition
    {
        public int StopConditionType;

        /// <summary>
        /// Максимальное количество итераций
        /// </summary>
        public int J;

        /// <summary>
        /// Нормализованная квадратичная разность между двумя последовательными операциями приближения
        /// </summary>
        public double S;

        /// <summary>
        /// Скорость изменения Нормализованной квадратичной разности
        /// </summary>
        public double dS;

        /// <summary>
        /// Нормализованная квадратичная разность между двумя последовательными операциями приближения
        /// Предыдущее значение
        /// </summary>
        protected double last_sigma;

        /// <summary>
        /// Проверка на останов
        /// Проверка на КоличествоИтераций 
        /// </summary>
        /// <param name="j">текущая итерация</param>
        /// <param name="source">контекст</param>
        /// <returns>false если остановка</returns>

        public virtual bool CheckContinue(int j = 99, IList<IList<double>> H = null)
        {
            if (j < J)
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Проверяется Нормализованная квадратичная разность между двумя последовательными операциями приближения
        /// Значение last_sigma должно быть обнулено для первого использования
        /// sigma = Sumk[(hi-1(k) - hi(k))^2] / Sumk[hi-1(k)^2]
        /// </summary>
        /// <param name="H">данные двух последних операций приближения</param>
        /// <returns>false если остановка</returns>
        protected virtual bool CheckContinueSigma(IList<IList<double>> H)
        {
            IList<double> H0 = H[0];
            IList<double> H1 = H[1];
            double числитель = 0;
            double знаменатель = 0;
            double sigma = 0;
            bool result = false;

            //sigma = Sumk[(hi-1(k) - hi(k))^2] / Sumk[hi-1(k)^2]
            for (int i = 0; i < H0.Count; i++)
            {
                числитель += (H0[i] - H1[i]) * (H0[i] - H1[i]);
                знаменатель += H0[i] * H0[i];
            }
            if (знаменатель != 0)
            {
                sigma = числитель / знаменатель;
            }
            else
            {
                sigma = 0;
            }
            
            //Продолжаем если 
            //Не достигнута нужная точность
            //И скорость изменения точности выше минимальной
            if ((sigma > S) && (System.Math.Abs(sigma - last_sigma) > dS))
            {
                result = true;
            }

            //Запоминаем последнее значение 
            last_sigma = sigma;
            // <returns>false если остановка</returns>
            return result;
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Значение last_sigma должно быть обнулено для первого использования
        /// </summary>
        public virtual void Reset()
        {
            last_sigma = 0;
        }
        /// <summary>
        /// Подсчет экстремуы входного ряда
        /// </summary>
        /// <returns>
        /// false если остановка
        /// true если 2 и более экстремумов
        /// </returns>
        public virtual bool CheckContinueExt(IList<double> source1)
        {
            int extCount = 0;
            bool result = false;

            IList<double> y = source1;

            if (y.Count > 0)
            {
                for (int i = 1; i < y.Count - 1; i++)
                {
                    if ((y[i] < y[i - 1]) && (y[i] <= y[i + 1]))
                    {
                        extCount++;
                    }
                    else if ((y[i] > y[i - 1]) && (y[i] >= y[i + 1]))
                    {
                        extCount++;
                    }

                    if (extCount > 1)
                    {
                        //продолжаем если 2 и более экстремумов не считая граничных
                        result = true;
                        break;
                    }
                }

            }
            return result;
        }
    }
}