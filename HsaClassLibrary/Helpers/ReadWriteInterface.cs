using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReadWrite
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        void WriteCSV(IList<IList<double>> data, string fileName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        IList<IList<double>> ReadCSV(string fileName);
    }
}
