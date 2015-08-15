using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Transform
{
    /// <summary>
    /// 
    /// </summary>
    public enum EnumHilbertTransform
    {
        /// <summary>
        /// -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI) 
        /// </summary>
        HilbertTransform0 = 0,
        /// <summary>
        /// -0.0962, -0.5769, 0.5769, 0.0962
        /// </summary>
        HilbertTransform1 = 1,
        /// <summary>
        /// -0,25 -0,75 0,75 0,25
        /// </summary>
        HilbertTransform2 = 2,
        /// <summary>
        /// -0,333 -1,000 1,000 0,333
        /// </summary>
        HilbertTransform3 = 3,

        /// <summary>
        /// -2 / (5 * Math.PI), -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI), -2 / (5 * Math.PI)
        /// </summary>
        HilbertTransform4 = 4,
        /// <summary>
        /// -2 / (7 * Math.PI), -2 / (5 * Math.PI), -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI), -2 / (5 * Math.PI), -2 / (7 * Math.PI)
        /// </summary>
        HilbertTransform5 = 5,


        /// <summary>
        /// HTFFT
        /// </summary>
        HilbertTransform = 10,
        /// <summary>
        /// HTFFT_alglib
        /// </summary>
        HilbertTransformAlglib = 11,
    }
}
