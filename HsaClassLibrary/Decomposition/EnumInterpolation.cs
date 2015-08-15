using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Decomposition
{
    /// <summary>
    /// Методы интерполяции
    /// </summary>
    public enum EnumInterpolation
    {
        /// <summary>
        /// 
        /// </summary>
        ЛинейнаяИнтерполяция = 0,
        /// <summary>
        /// 
        /// </summary>
        КвадратичнаяИнтерполяция = 2,
        /// <summary>
        /// 
        /// </summary>
        КубическийСплайн = 3,
    }
}
