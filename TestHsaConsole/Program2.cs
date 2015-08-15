using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program2
    {
        /// <summary>
        ///  alglib.spline1dbuildlinear
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            //
            // We use piecewise linear spline to interpolate f(x)=x^2 sampled 
            // at 5 equidistant nodes on [-1,+1].
            //
            double[] x = new double[] { -1.0, -0.5, 0.0, +0.5, +1.0 };
            double[] y = new double[] { +1.0, 0.25, 0.0, 0.25, +1.0 };
            double t = 0.25;
            double v;
            alglib.spline1dinterpolant s;

            // build spline
            alglib.spline1dbuildlinear(x, y, out s);

            // calculate S(0.25) - it is quite different from 0.25^2=0.0625
            v = alglib.spline1dcalc(s, t);
            System.Console.WriteLine("V {0}", v); // EXPECTED: 0.125
            System.Console.ReadLine();
            return 0;
        }
    }
}
