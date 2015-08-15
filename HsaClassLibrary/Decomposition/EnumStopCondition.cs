using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Decomposition
{
    /// <summary>
    /// Критерии останова алгоритма EMD
    /// </summary>
    public enum EnumStopCondition
    {
        /// <summary>
        /// Выполнено заданное количество итераций
        /// </summary>
        КоличествоИтераций = 0,
        /// <summary>
        /// Проверка на количество экстремумов остатка
        /// </summary>        
        КоличествоЭкстремумов = 1,
        /// <summary>
        /// Выполнено заданное количество итераций или недостатосное количество экстремумов остатка
        /// </summary>
        КоличествоИтерацийИлиЭкстремумов = 2,
        /// <summary>
        /// 
        /// </summary>
        ОстатокНесущественный = 3,
        /// <summary>
        /// 
        /// </summary>
        ПогрешностьРеконструкцииЗаданная = 4,
        /// <summary>
        /// 
        /// </summary>
        ПогрешностьРеконструкцииУвеличилась = 5,
    }
}
