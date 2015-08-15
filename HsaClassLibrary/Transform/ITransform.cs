using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HsaClassLibrary.Transform
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// выполнить преобразование 
        /// </summary>
        void Transform();
        
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        void Transform(IList<double> x);
         */
    }
}
