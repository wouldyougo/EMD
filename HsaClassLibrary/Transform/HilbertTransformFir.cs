using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Transform
{
    //Переделать - заменить HT на функцию свертки

    /// <summary>
    /// -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI)
    /// </summary>
    public class HilbertTransformFir
    {
        /// <summary>
        /// -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI) 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR0(IList<double> x)
        {
            
            int count = x.Count;
            IList<double> result = new double[count];

            //IList<double> h = new double[6] { -2 / (5 * Math.PI), - 2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI), 2 / (5 * Math.PI) };
            //(1-COS(Math.PI*i))/(Math.PI*i)
            IList<double> h = new double[4] 
            { -2.0 / (3 * Math.PI), 
              -2.0 / Math.PI,               
               2.0 / Math.PI, 
               2.0 / (3 * Math.PI) 
            };
            
            /*
            for (int i = 0; i < h.Count; i++) {
                h[i] = h[i] * 1.0 / 64;
            }*/

            /*
            коэффициенты из примера ниже
             * IList<double> h = new double[4] 
            { 
                -0.0962,
                -0.5769,
                 0,5769,
                 0,0962
            };
             * */
            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 6; i < count; i++)
            {
                result[i-3] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6];
            }
            return result;
        }
        /// <summary>
        /// -2 / (5 * Math.PI), -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI), -2 / (5 * Math.PI)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR1(IList<double> x)
        {

            int count = x.Count;
            IList<double> result = new double[count];

            IList<double> h = new double[6] 
            { -2.0 / (5 * Math.PI), 
              -2.0 / (3 * Math.PI), 
              -2.0 / Math.PI, 
               2.0 / Math.PI, 
               2.0 / (3 * Math.PI), 
               2.0 / (5 * Math.PI)
            };

            /*
            for (int i = 0; i < h.Count; i++)
            {
                h[i] = h[i] * 1.0 / 32;
            }*/


            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 10; i < count; i++)
            {
                result[i - 5] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6] + h[4] * x[i - 8] + h[5] * x[i - 10];
            }
            return result;
        }
        /// <summary>
        /// -2 / (7 * Math.PI), -2 / (5 * Math.PI), -2 / (3 * Math.PI), -2 / Math.PI, 2 / Math.PI, 2 / (3 * Math.PI), -2 / (5 * Math.PI), -2 / (7 * Math.PI)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR2(IList<double> x)
        {

            int count = x.Count;
            IList<double> result = new double[count];

            IList<double> h = new double[8] 
            { -2.0 / (7 * Math.PI), 
              -2.0 / (5 * Math.PI), 
              -2.0 / (3 * Math.PI), 
              -2.0 / Math.PI, 
               2.0 / Math.PI, 
               2.0 / (3 * Math.PI), 
               2.0 / (5 * Math.PI),
               2.0 / (7 * Math.PI)
            };

            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 14; i < count; i++)
            {
                result[i - 7] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6] + h[4] * x[i - 8] + h[5] * x[i - 10] + +h[6] * x[i - 12] + +h[7] * x[i - 14];
            }
            return result;
        }
        /// <summary>
        /// -0,333 -1,000 1,000 0,333
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR3(IList<double> x)
        {
            
            int count = x.Count;
            IList<double> result = new double[count];

            //коэффициенты из примера ниже
            IList<double> h = new double[4] 
            { 
               -0.333,
               -1.000,
               1.000,
               0.333
            };
            
            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 6; i < count; i++)
            {
                result[i-3] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6];
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR4(IList<double> x)
        {

            int count = x.Count;
            IList<double> result = new double[count];

            IList<double> h = new double[6] 
            {                 
              -Math.Sin(Math.PI*5.0/2.0)*Math.Sin(Math.PI*5.0/2.0)/5,             
              -Math.Sin(Math.PI*3.0/2.0)*Math.Sin(Math.PI*3.0/2.0)/3,
              -Math.Sin(Math.PI*1.0/2.0)*Math.Sin(Math.PI*1.0/2.0)/1,              
              Math.Sin(Math.PI*1.0/2.0)*Math.Sin(Math.PI*1.0/2.0)/1,
              Math.Sin(Math.PI*3.0/2.0)*Math.Sin(Math.PI*3.0/2.0)/3,
              Math.Sin(Math.PI*5.0/2.0)*Math.Sin(Math.PI*5.0/2.0)/5,             
            };

            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 10; i < count; i++)
            {
                result[i - 5] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6] + h[4] * x[i - 8] + h[5] * x[i - 10];
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR5(IList<double> x)
        {
            
            int count = x.Count;
            IList<double> result = new double[count];

            IList<double> h = new double[8] 
            {                 
              -Math.Sin(Math.PI*7.0/2.0)*Math.Sin(Math.PI*7.0/2.0)/7,
              -Math.Sin(Math.PI*5.0/2.0)*Math.Sin(Math.PI*5.0/2.0)/5,             
              -Math.Sin(Math.PI*3.0/2.0)*Math.Sin(Math.PI*3.0/2.0)/3,
              -Math.Sin(Math.PI*1.0/2.0)*Math.Sin(Math.PI*1.0/2.0)/1,              
              Math.Sin(Math.PI*1.0/2.0)*Math.Sin(Math.PI*1.0/2.0)/1,
              Math.Sin(Math.PI*3.0/2.0)*Math.Sin(Math.PI*3.0/2.0)/3,
              Math.Sin(Math.PI*5.0/2.0)*Math.Sin(Math.PI*5.0/2.0)/5,             
              Math.Sin(Math.PI*7.0/2.0)*Math.Sin(Math.PI*7.0/2.0)/7
            };
            
            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 14; i < count; i++)
            {
                result[i - 7] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6] + h[4] * x[i - 8] + h[5] * x[i - 10] + +h[6] * x[i - 12] + +h[7] * x[i - 14];
            }
            return result;
        }
     
        /// <summary>
        /// -0,25 -0,75 0,75 0,25
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR6(IList<double> x)
        {

            int count = x.Count;
            IList<double> result = new double[count];

            //коэффициенты из примера ниже
            IList<double> h = new double[4] 
            { 
                -0.25,
                -0.75,
                0.75,
                0.25
            };

            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 6; i < count; i++)
            {
                result[i - 3] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6];
            }
            return result;
        }
        /// <summary>
        /// -0.0962, -0.5769, 0.5769, 0.0962
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IList<double> HTFIR7(IList<double> x)
        {

            int count = x.Count;
            IList<double> result = new double[count];

            //коэффициенты из примера ниже
            IList<double> h = new double[4] 
            { 
                -0.0962,
                -0.5769,
                 0.5769,
                 0.0962
            };

            //пример из http://www.mql5.com/ru/articles/288
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) * (0.5 + 0.08 * InstPeriod[i + 1]);
            //I1[i] = Cycle[i + 3];

            //сопоставим порядок коэффициентов:
            //Q1[i] = (0.0962 * Cycle[i] + 0.5769 * Cycle[i + 2] - 0.5769 * Cycle[i + 4] - 0.0962 * Cycle[i + 6]) 
            //           h[3] * x[i - 6] +   h[2] * x[i - 4]     +   h[1] * x[i - 2]     +   h[0] * x[i]

            //В прямом порядке для x[j]
            //Q[3] = h[3] * x[i - 6] + h[2] * x[i - 4] + h[1] * x[i - 2] + h[0] * x[i];

            //В прямом порядке для h[j]
            //Q[3] = h[0] * x[i] + h[1] * x[i-2] + h[2] * x[i-4] + h[3] * x[i-6];

            for (int i = 6; i < count; i++)
            {
                result[i - 3] = h[0] * x[i] + h[1] * x[i - 2] + h[2] * x[i - 4] + h[3] * x[i - 6];
            }
            return result;
        }
    }
}
