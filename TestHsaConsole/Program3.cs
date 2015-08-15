using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program3
    {
        /// <summary>
        /// test alglib.spline
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            //
            // We use cubic spline to do resampling, i.e. having
            // values of f(x)=x^2 sampled at 5 equidistant nodes on [-1,+1]
            // we calculate values/derivatives of cubic spline on 
            // another grid (equidistant with 9 nodes on [-1,+1])
            // WITHOUT CONSTRUCTION OF SPLINE OBJECT.
            //
            // There are efficient functions spline1dconvcubic(),
            // spline1dconvdiffcubic() and spline1dconvdiff2cubic() 
            // for such calculations.
            //
            // We use default boundary conditions ("parabolically terminated
            // spline") because cubic spline built with such boundary conditions 
            // will exactly reproduce any quadratic f(x).
            //
            // Actually, we could use natural conditions, but we feel that 
            // spline which exactly reproduces f() will show us more 
            // understandable results.
            //
            double[] x_old = new double[] { -1.0, -0.5, 0.0, +0.5, +1.0 };
            double[] y_old = new double[] { +1.0, 0.25, 0.0, 0.25, +1.0 };
            double[] x_new = new double[] { -1.00, -0.75, -0.50, -0.25, 0.00, +0.25, +0.50, +0.75, +1.00 };
            double[] y_new;
            double[] d1_new;
            double[] d2_new;

            //
            // First, conversion without differentiation.
            //
            //
            //alglib.spline1dconvcubic //(x_old, y_old, x_new, out y_new);
            alglib.spline1dconvcubic(x_old, y_old, x_new, out y_new);
            System.Console.WriteLine("{0}", alglib.ap.format(y_new, 3)); // EXPECTED: [1.0000, 0.5625, 0.2500, 0.0625, 0.0000, 0.0625, 0.2500, 0.5625, 1.0000]

            //
            // Then, conversion with differentiation (first derivatives only)
            //
            //
            alglib.spline1dconvdiffcubic(x_old, y_old, x_new, out y_new, out d1_new);
            System.Console.WriteLine("{0}", alglib.ap.format(y_new, 3)); // EXPECTED: [1.0000, 0.5625, 0.2500, 0.0625, 0.0000, 0.0625, 0.2500, 0.5625, 1.0000]
            System.Console.WriteLine("{0}", alglib.ap.format(d1_new, 3)); // EXPECTED: [-2.0, -1.5, -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0]

            //
            // Finally, conversion with first and second derivatives
            //
            //
            alglib.spline1dconvdiff2cubic(x_old, y_old, x_new, out y_new, out d1_new, out d2_new);
            System.Console.WriteLine("{0}", alglib.ap.format(y_new, 3)); // EXPECTED: [1.0000, 0.5625, 0.2500, 0.0625, 0.0000, 0.0625, 0.2500, 0.5625, 1.0000]
            System.Console.WriteLine("{0}", alglib.ap.format(d1_new, 3)); // EXPECTED: [-2.0, -1.5, -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0]
            System.Console.WriteLine("{0}", alglib.ap.format(d2_new, 3)); // EXPECTED: [2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0]
            System.Console.ReadLine();
            return 0;
        }
    }
}
