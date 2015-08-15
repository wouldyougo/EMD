using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HsaClassLibrary.Transform
{
    /// <summary>
    /// вспомогательные статические методы
    /// </summary>
    public class TransformHelper
    {
        /// <summary>
        /// Дополняет данные до нужного размера своими даными в цикле
        /// throw new System.ApplicationException("prepareZero: не проверена работа этой функции");
        /// </summary>
        /// <param name="asignal"></param>
        /// <param name="asize"></param>
        /// <returns></returns>
        public static List<double> prepareData(List<double> asignal, int asize)
        {
            throw new System.ApplicationException("prepareZero: не проверена работа этой функции");

            List<double> tmp = new List<double>(asignal);
            int size;
            double log_size;
            int int_log_size;
            if ((int)(asize) < asignal.Count)
            {
                asize = asignal.Count;
                throw new System.ApplicationException("Spectrum::prepareData error");
            }
            //log_size = Log2(asize);
            //Log2(x) = log(x)/log(2)
            log_size = Math.Log(asize) / Math.Log(2);


            int_log_size = (int)(log_size + 0.5);
            if ((log_size / (double)(int_log_size)) != 1)
            {
                if (log_size > int_log_size)
                {
                    int_log_size++;
                }
            }
            size = (int)Math.Pow(2, int_log_size);
            if ((int)(size) > asignal.Count)
            {
                for (int i = 0, j = 0; i < size - asignal.Count; i++, j++)
                {
                    if (j == asignal.Count)
                    {
                        j = 0;
                    }
                    tmp.Add(tmp[j]);
                }
            }
            return tmp;
        }
        /// <summary>
        /// Дополняет данные до нужного размера нулями
        /// throw new System.ApplicationException("prepareZero: не проверена работа этой функции");
        /// </summary>
        /// <param name="asignal"></param>
        /// <param name="asize"></param>
        /// <returns></returns>
        public static List<double> prepareZero(List<double> asignal, int asize)
        {
            throw new System.ApplicationException("prepareZero: не проверена работа этой функции");

            List<double> tmp = new List<double>(asignal);
            int size;
            double log_size;
            int int_log_size;
            if ((int)(asize) < asignal.Count)
            {
                asize = asignal.Count;
                throw new System.ApplicationException("Spectrum::prepareData error");
            }
            //log_size = Log2(asize);
            log_size = Math.Log(asize) / Math.Log(2);

            int_log_size = (int)(log_size + 0.5);
            if ((log_size / (double)(int_log_size)) != 1)
            {
                if (log_size > int_log_size)
                {
                    int_log_size++;
                }
            }
            size = (int)Math.Pow(2, int_log_size);
            if ((int)(size) > asignal.Count)
            {
                for (int i = 0, j = 0; i < size - asignal.Count; i++, j++)
                {
                    if (j == asignal.Count)
                    {
                        j = 0;
                    }
                    tmp.Add(0);
                }
            }
            return tmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<Complex> convert(alglib.complex[] source)
        {
            int count = source.Length;
            IList<Complex> result = new Complex[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new Complex(source[i].x, source[i].y);
            }
            return result;        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Re"></param>
        /// <param name="Im"></param>
        public static void convert(IList<Complex> source, out IList<double> Re, out IList<double> Im)
        {
            int count = source.Count;            
            Re = new double[count];
            Im = new double[count];

            for (int i = 0; i < count; i++)
            {
                Re[i] = source[i].Real;
                Im[i] = source[i].Imaginary;
            }
        }
    }
}
