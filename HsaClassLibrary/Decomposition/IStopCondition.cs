using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsaClassLibrary.Decomposition
{
    /// <summary>
    /// Критерии останова
    /// </summary>
    public interface IStopCondition
    {
        /// <summary>

        /// Проверка на КоличествоИтераций 
        /// </summary>
        /// <returns>false если остановка</returns>

        /// <summary>
        /// Проверка на останов
        /// </summary>
        /// <param name="j">текущая итерация</param>
        /// <param name="source">контекст</param>
        /// <returns>false если остановка</returns>
        bool CheckContinue(int j, IList<IList<double>> source);

        /// <summary>
        /// сброс условий перед началом нового цикла
        /// </summary>
        void Reset();
    }
}