using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Decomposition
{

        /*
        //------------------------------------------------------------------------------------
        // Find local extremas and creation of boundary points.
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Поиск минимумов и максимумов
        /// реализация взята из стартьи Знакомство с методом эмпирической модовой декомпозиции
        /// </summary>
        /// <param name="source1"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        public override void FindExt(IList<double> source1, out IList<double> xMax, out IList<double> yMax, out IList<double> xMin, out IList<double> yMin)
        {
            xMax = new List<double>();
            yMax = new List<double>();
            xMin = new List<double>();
            yMin = new List<double>();

            IList<double> y = source1;
            int N = source1.Count;

            int i, nb;
            double a, b, c, e;

            int nmax = 0;
            int nmin = 0;
            for (i = 1; i < N - 1; i++)
            {
                a = y[i - 1]; b = y[i]; c = y[i + 1];
                //  e=DBL_MIN+Eps*MathMax(MathAbs(c),MathMax(MathAbs(b),MathAbs(a)));
                e = 8 * double.Epsilon * Math.Max(Math.Abs(c), Math.Max(Math.Abs(b), Math.Abs(a)));
                if (((a - b) <= e) && ((c - b) <= e)) { xMax[2 + nmax] = i; yMax[2 + nmax++] = y[i]; }
                if (((a - b) >= -e) && ((c - b) >= -e)) { xMin[2 + nmin] = i; yMin[2 + nmin++] = y[i]; }
            }
            //------------ boundary points
            nb = 2;
            while (nmin < nb + 1 && nmax < nb + 1) nb--;
            if (nb < 2)
            {
                for (i = 0; i < nmin; i++) { xMin[i + nb] = xMin[i + 2]; yMin[i + nb] = yMin[i + 2]; }
                for (i = 0; i < nmax; i++) { xMax[i + nb] = xMax[i + 2]; yMax[i + nb] = yMax[i + 2]; }
            }
            if (nb == 0) return;
            if (xMax[nb] < xMin[nb])
            {
                if (y[0] > yMin[nb])
                {
                    if (2 * xMax[nb] - xMin[2 * nb - 1] > 0)
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[i] = -xMax[2 * nb - 1 - i];
                            yMax[i] = yMax[2 * nb - 1 - i];
                            xMin[i] = -xMin[2 * nb - 1 - i];
                            yMin[i] = yMin[2 * nb - 1 - i];
                        }
                    }
                    else
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[i] = 2 * xMax[nb] - xMax[2 * nb - i];
                            yMax[i] = yMax[2 * nb - i];
                            xMin[i] = 2 * xMax[nb] - xMin[2 * nb - 1 - i];
                            yMin[i] = yMin[2 * nb - 1 - i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < nb; i++)
                    {
                        xMax[i] = -xMax[2 * nb - 1 - i];
                        yMax[i] = yMax[2 * nb - 1 - i];
                    }
                    for (i = 0; i < nb - 1; i++)
                    {
                        xMin[i] = -xMin[2 * nb - 2 - i];
                        yMin[i] = yMin[2 * nb - 2 - i];
                    }
                    xMin[nb - 1] = 0;
                    yMin[nb - 1] = y[0];
                }
            }
            else
            {
                if (y[0] < yMax[nb])
                {
                    if (2 * xMin[nb] - xMax[2 * nb - 1] > 0)
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[i] = -xMax[2 * nb - 1 - i];
                            yMax[i] = yMax[2 * nb - 1 - i];
                            xMin[i] = -xMin[2 * nb - 1 - i];
                            yMin[i] = yMin[2 * nb - 1 - i];
                        }
                    }
                    else
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[i] = 2 * xMin[nb] - xMax[2 * nb - 1 - i];
                            yMax[i] = yMax[2 * nb - 1 - i];
                            xMin[i] = 2 * xMin[nb] - xMin[2 * nb - i];
                            yMin[i] = yMin[2 * nb - i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < nb; i++)
                    {
                        xMin[i] = -xMin[2 * nb - 1 - i];
                        yMin[i] = yMin[2 * nb - 1 - i];
                    }
                    for (i = 0; i < nb - 1; i++)
                    {
                        xMax[i] = -xMax[2 * nb - 2 - i];
                        yMax[i] = yMax[2 * nb - 2 - i];
                    }
                    xMax[nb - 1] = 0;
                    yMax[nb - 1] = y[0];
                }
            }
            nmin += nb - 1;
            nmax += nb - 1;

            if (xMax[nmax] < xMin[nmin])
            {
                if (y[N - 1] < yMax[nmax])
                {
                    if (2 * xMin[nmin] - xMax[nmax - nb + 1] < (N - 1))
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[nmax + 1 + i] = 2 * (N - 1) - xMax[nmax - i];
                            yMax[nmax + 1 + i] = yMax[nmax - i];
                            xMin[nmin + 1 + i] = 2 * (N - 1) - xMin[nmin - i];
                            yMin[nmin + 1 + i] = yMin[nmin - i];
                        }
                    }
                    else
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[nmax + 1 + i] = 2 * xMin[nmin] - xMax[nmax - i];
                            yMax[nmax + 1 + i] = yMax[nmax - i];
                            xMin[nmin + 1 + i] = 2 * xMin[nmin] - xMin[nmin - 1 - i];
                            yMin[nmin + 1 + i] = yMin[nmin - 1 - i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < nb; i++)
                    {
                        xMin[nmin + 1 + i] = 2 * (N - 1) - xMin[nmin - i];
                        yMin[nmin + 1 + i] = yMin[nmin - i];
                    }
                    for (i = 0; i < nb - 1; i++)
                    {
                        xMax[nmax + 2 + i] = 2 * (N - 1) - xMax[nmax - i];
                        yMax[nmax + 2 + i] = yMax[nmax - i];
                    }
                    xMax[nmax + 1] = N - 1;
                    yMax[nmax + 1] = y[N - 1];
                }
            }
            else
            {
                if (y[N - 1] > yMin[nmin])
                {
                    if (2 * xMax[nmax] - xMin[nmin - nb + 1] < (N - 1))
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[nmax + 1 + i] = 2 * (N - 1) - xMax[nmax - i];
                            yMax[nmax + 1 + i] = yMax[nmax - i];
                            xMin[nmin + 1 + i] = 2 * (N - 1) - xMin[nmin - i];
                            yMin[nmin + 1 + i] = yMin[nmin - i];
                        }
                    }
                    else
                    {
                        for (i = 0; i < nb; i++)
                        {
                            xMax[nmax + 1 + i] = 2 * xMax[nmax] - xMax[nmax - 1 - i];
                            yMax[nmax + 1 + i] = yMax[nmax - 1 - i];
                            xMin[nmin + 1 + i] = 2 * xMax[nmax] - xMin[nmin - i];
                            yMin[nmin + 1 + i] = yMin[nmin - i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < nb; i++)
                    {
                        xMax[nmax + 1 + i] = 2 * (N - 1) - xMax[nmax - i];
                        yMax[nmax + 1 + i] = yMax[nmax - i];
                    }
                    for (i = 0; i < nb - 1; i++)
                    {
                        xMin[nmin + 2 + i] = 2 * (N - 1) - xMin[nmin - i];
                        yMin[nmin + 2 + i] = yMin[nmin - i];
                    }
                    xMin[nmin + 1] = N - 1;
                    yMin[nmin + 1] = y[N - 1];
                }
            }
            nmin = nmin + nb + 1;
            nmax = nmax + nb + 1;
        }
         */
}
