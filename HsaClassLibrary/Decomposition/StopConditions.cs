using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Decomposition
{  
    /// <summary>
    /// Проверка на останов алгоритма отсеивания    
    /// Проверяется Нормализованная квадратичная разность между двумя последовательными операциями приближения
    /// </summary>
    
    public class StopConditionSeparate_1 : StopCondition
    {
        /// <summary>
        /// Метод критерия останова процесса отсеивания высокочатотной составляющей
        /// Достигнута Нормализованная квадратичная разность между двумя последовательными операциями отсеивания
        /// </summary>
        /// <returns>false если остановка</returns>
        public override bool CheckContinue(int j = 99, IList<IList<double>> H = null)
        {            
            if (base.CheckContinueSigma(H))
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Проверка на останов алгоритма отсеивания   
    /// Комбинированная проверка
    /// </summary>
    public class StopConditionSeparate_2 : StopCondition
    {

        /// <summary>
        /// Метод критерия останова процесса отсеивания высокочатотной составляющей
        /// Выполнено заданное количество операций отсеивания или достигнута Нормализованная квадратичная разность между двумя последовательными операциями отсеивания
        /// </summary>
        /// <returns>false если остановка</returns>
        public override bool CheckContinue(int j = 99, IList<IList<double>> H = null)
        {
            if ((j < J)&&(base.CheckContinueSigma(H)))
            {
                return true; 
            }
            return false;
            //throw new NotImplementedException();
        }
        
    }

    /// <summary>
    /// Проверка на останов алгоритма
    /// Проверка по количеству экстремумов остатка
    /// </summary>
    public class StopCondition_1 : StopCondition
    {

        /// <summary>
        /// Метод критерия останова процесса отсеивания высокочатотной составляющей
        /// Подсчет количества экстремумов входного ряда
        /// </summary>
        /// <returns>false если остановка</returns>
        public override bool CheckContinue(int j = 99, IList<IList<double>> R = null)
        {
            IList<double> _R = R[R.Count - 1];
            if (base.CheckContinueExt(_R))
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }

    }
    /// <summary>
    /// Проверка на останов алгоритма
    /// Проверка по количеству экстремумов остатка и количеству итераций
    /// </summary>
    public class StopCondition_2 : StopCondition
    {

        /// <summary>
        /// Проверка по количеству экстремумов остатка и количеству итераций
        /// </summary>
        /// <returns>false если остановка</returns>
        public override bool CheckContinue(int j = 99, IList<IList<double>> R = null)
        {
            IList<double> _R = R[R.Count - 1];
            if ((j < J) && (base.CheckContinueExt(_R)))
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }

    }

}
