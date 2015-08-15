using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HsaClassLibrary.Transform
{
    /// <summary>
    /// Реализует методы с FourierTransform параметрами List
    /// </summary>
    public class FourierTransformList
    {
        /// <summary>
        /// FourierTransform
        /// </summary>
        /// <param name="source">исходный дейтвительный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static List<Complex> ft(List<double> source)
        {
            List<Complex> spectrum = new List<Complex>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }

            Complex J = new Complex(0, -1);
            Complex Temp = new Complex();

            int len_spectrum;
            len_spectrum = source.Count;
            for (int k = 0; k < len_spectrum; k++)
            {
                for (int n = 0; n < len_spectrum; n++)
                {
                    Temp = (2 * System.Math.PI * n * k) / len_spectrum;
                    Temp *= J;
                    //Temp = Math.Exp(Temp);
                    Temp = Complex.Exp(Temp);
                    spectrum[k] += Temp * (Complex)(source[n]);
                }
            }
            return spectrum;
        }
        /// <summary>
        /// FourierTransform
        /// </summary>
        /// <param name="source">исходный комплексный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static List<Complex> ft(List<Complex> source)
        {
            List<Complex> spectrum = new List<Complex>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }

            Complex J = new Complex(0, -1);
            Complex Temp = new Complex();

            int len_spectrum;
            len_spectrum = source.Count;
            for (int k = 0; k < len_spectrum; k++)
            {
                for (int n = 0; n < len_spectrum; n++)
                {
                    Temp = (2 * System.Math.PI * n * k) / len_spectrum;
                    Temp *= J;
                    Temp = Complex.Exp(Temp);
                    spectrum[k] += Temp * source[n];
                }
            }
            return spectrum;
        }

        /// <summary>
        /// FastFourierTransform
        /// </summary>
        /// <param name="source">исходный дейтвительный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static List<Complex> fft(List<double> source)
        {
            /*
            List<Complex> spectrum = new List<Complex>(source.Count); 
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }
            */
            int max_count;
            int tmp;
            Complex J = new Complex(0, -1);
            Complex V = new Complex();
            Complex V_tmp = new Complex();
            tmp = source.Count;
            List<Complex> A = new List<Complex>(tmp);
            List<Complex> B = new List<Complex>(tmp);
            for (int i = 0; i < tmp; i++)
            {
                A.Add(new Complex(0, 0));
                B.Add(new Complex(0, 0));
            }

            //надо инициализировать 1 как в исходнике
            //List<Complex > W = new List<Complex >((tmp/2), 1);
            //throw new System.NotImplementedException();
            List<Complex> W = new List<Complex>((tmp / 2));
            for (int i = 0; i < tmp / 2; i++)
            {
                W.Add(new Complex(1, 0));
            }

            int len_spectrum;
            len_spectrum = source.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                B[i] = source[i];
            }

            double chislitel = 0;
            double znamenatel = 0;
            chislitel = Math.Log(len_spectrum);
            znamenatel = Math.Log(2);
            max_count = (int)(chislitel / znamenatel + 0.5); // надо на 1 меньше т.к. двухточечное уже сделал

            // двоично - инверсное представление
            for (int i = 0; i < len_spectrum; i++)
            {
                int t1;
                int t2;
                int t3;
                t1 = i;
                t3 = 0;
                // вычисляю индекс
                for (int j = 0; j < max_count; j++)
                {
                    t2 = t1;
                    t2 = (t2 & 1);
                    t2 <<= (max_count - 1) - j;
                    t3 += t2;
                    t1 >>= 1;
                }
                A[i] = B[t3];
            }

            //--------------------------------------
            //for (int i = 0; i < len_spectrum; i++)
            //{
            //   System.Console.WriteLine("{0} {1}", A[i].Real, A[i].Imaginary);
            //}
            //--------------------------------------
            //--------------------------------------
            //for (int i = 0; i < len_spectrum; i++)
            //{
            //    System.Console.WriteLine("{0} {1}", B[i].Real, B[i].Imaginary);
            //}
            //--------------------------------------
            //Вычисляю двухточечное преобразование
            for (int i = 0; i < len_spectrum; i += 2)
            {
                B[i] = A[i] + A[i + 1];
                B[i + 1] = A[i] - A[i + 1];
            }
            //--------------------------------------
            //for (int i = 0; i < len_spectrum; i++)
            //{
            //    System.Console.WriteLine("{0} {1}", B[i].Real, B[i].Imaginary);
            //}
            //--------------------------------------
            // переставляю чет-нечет
            for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
            }
            for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i];
            }
            //--------------------------------------
            //for (int i = 0; i < len_spectrum; i++)
            //{
            //    System.Console.WriteLine("{0} {1}", A[i].Real, A[i].Imaginary);
            //}
            //--------------------------------------

            // Вычисляю e^(-j2p/N)
            V = (2 * System.Math.PI) / len_spectrum;
            V *= J;
            //V = Math.Exp(V);
            V = Complex.Exp(V);
            //System.Console.WriteLine("{0} {1}", V.Real, V.Imaginary);

            for (int c = 1; c < max_count; c++) //!!!!!!!!!!!!!! max_count !!!!!!!!!!!!!!
            {
                //Вычисляю  W
                tmp = max_count - (c + 1);
                tmp = (int)Math.Pow(2, tmp);
                V_tmp = Complex.Pow(V, tmp);

                for (int i = 0, k = 0; i < Math.Pow(2, c); i++, k++)
                {
                    W[k] = Complex.Pow(V_tmp, i);
                    for (int j = k; j < (tmp - 1) + k; j++)
                    {
                        W[j + 1] = W[j];
                    }
                    k += tmp - 1;
                }
                //Вычисляю преобразование
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    B[i] = A[i] + W[j] * A[i + 1];
                    B[i + 1] = A[i] - W[j] * A[i + 1];
                }
                // переставляю чет-нечет
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
                }
                for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i];
                }
            }
            //spectrum = A;
            return A;
        }
        /// <summary>
        /// FastFourierTransform
        /// </summary>
        /// <param name="source">исходный комплексный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static List<Complex> fft(List<Complex> source)
        {
            /*
            List<Complex> spectrum = new List<Complex>(source.Count); 
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }
            */
            int max_count;
            int tmp;
            Complex J = new Complex(0, -1);
            Complex V = new Complex();
            Complex V_tmp = new Complex();
            tmp = source.Count;
            List<Complex> A = new List<Complex>(tmp);
            List<Complex> B = new List<Complex>(tmp);
            for (int i = 0; i < tmp; i++)
            {
                A.Add(new Complex(0, 0));
                B.Add(new Complex(0, 0));
            }

            //надо инициализировать 1 как в исходнике
            //List<Complex > W = new List<Complex >((tmp/2), 1);
            //throw new System.NotImplementedException();
            List<Complex> W = new List<Complex>((tmp / 2));
            for (int i = 0; i < tmp / 2; i++)
            {
                W.Add(new Complex(1, 0));
            }

            int len_spectrum;
            len_spectrum = source.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                B[i] = source[i];
            }

            double chislitel = 0;
            double znamenatel = 0;
            chislitel = Math.Log(len_spectrum);
            znamenatel = Math.Log(2);
            max_count = (int)(chislitel / znamenatel + 0.5); // надо на 1 меньше т.к. двухточечное уже сделал

            // двоично - инверсное представление
            for (int i = 0; i < len_spectrum; i++)
            {
                int t1;
                int t2;
                int t3;
                t1 = i;
                t3 = 0;
                // вычисляю индекс
                for (int j = 0; j < max_count; j++)
                {
                    t2 = t1;
                    t2 = (t2 & 1);
                    t2 <<= (max_count - 1) - j;
                    t3 += t2;
                    t1 >>= 1;
                }
                A[i] = B[t3];
            }

            //Вычисляю двухточечное преобразование
            for (int i = 0; i < len_spectrum; i += 2)
            {
                B[i] = A[i] + A[i + 1];
                B[i + 1] = A[i] - A[i + 1];
            }
            // переставляю чет-нечет
            for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
            }
            for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i];
            }

            // Вычисляю e^(-j2p/N)
            V = (2 * System.Math.PI) / len_spectrum;
            V *= J;
            V = Complex.Exp(V);

            for (int c = 1; c < max_count; c++) //!!!!!!!!!!!!!! max_count !!!!!!!!!!!!!!
            {
                //Вычисляю  W
                tmp = max_count - (c + 1);
                tmp = (int)Math.Pow(2, tmp);
                V_tmp = Complex.Pow(V, tmp);

                for (int i = 0, k = 0; i < Math.Pow(2, c); i++, k++)
                {
                    W[k] = Complex.Pow(V_tmp, i);
                    for (int j = k; j < (tmp - 1) + k; j++)
                    {
                        W[j + 1] = W[j];
                    }
                    k += tmp - 1;
                }
                //Вычисляю преобразование
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    B[i] = A[i] + W[j] * A[i + 1];
                    B[i + 1] = A[i] - W[j] * A[i + 1];
                }
                // переставляю чет-нечет
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
                }
                for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i];
                }
            }
            //spectrum = A;
            return A;
        }

        /// <summary>
        /// InverseFastFourierTransform
        /// </summary>
        /// <param name="source">дейтвительный сигнал - спектр</param>
        /// <returns>исходный дейтвительный сигнал</returns>
        public static List<double> ifft(List<double> aData)
        {
            List<Complex> spectrum;
            spectrum = fft(aData);
            divideSize(ref spectrum);
            inverseRez(ref spectrum);
            //return getReal();
            throw new NotImplementedException("ifft");
            List<double> Real = null;
            return Real;
        }

        /// <summary>
        /// InverseFastFourierTransform
        /// </summary>
        /// <param name="source">комплексный спектр</param>
        /// <returns>исходный дейтвительный сигнал</returns>
        public static List<Complex> ifft(List<Complex> aData)
        {
            List<Complex> spectrum;
            spectrum = fft(aData);
            divideSize(ref spectrum);
            inverseRez(ref spectrum);
            return spectrum;
            //throw new NotImplementedException("ifft");
        }

        private static void divideSize(ref List<Complex> spectrum)
        {
            int len_spectrum = spectrum.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                spectrum[i] /= len_spectrum;
            }
        }

        /// <summary>
        /// меняет сортировку
        /// </summary>
        /// <param name="spectrum"></param>
        private static void inverseRez(ref List<Complex> source)
        {
            //Не трогаем сортировкой первый 
            source.Reverse(1, source.Count - 1);
        }
    }

    /// <summary>
    /// Реализует методы FourierTransform
    /// </summary>
    public class FourierTransform
    {       
        /// <summary>
        /// FastFourierTransform
        /// надо проверять длину исходного сигнала
        /// </summary>
        /// <param name="source">исходный дейтвительный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static IList<Complex> fft(IList<double> source)
        {
            /*
            List<Complex> spectrum = new List<Complex>(source.Count); 
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }
            */
            int max_count;
            int tmp;
            Complex J = new Complex(0, -1);
            Complex V = new Complex();
            Complex V_tmp = new Complex();
            tmp = source.Count;
            //List<Complex> A = new List<Complex>(tmp);
            //List<Complex> B = new List<Complex>(tmp);
            IList<Complex> A = new Complex[tmp];
            IList<Complex> B = new Complex[tmp];

            //надо инициализировать 1 как в исходнике
            //List<Complex > W = new List<Complex >((tmp/2), 1);
            //throw new System.NotImplementedException();
            //List<Complex> W = new List<Complex>((tmp / 2));
            IList<Complex> W = new Complex[tmp / 2];
            for (int i = 0; i < tmp / 2; i++)
            {
                //W.Add(new Complex(1, 0));
                W[i] = 1;
            }

            int len_spectrum;
            len_spectrum = source.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                B[i] = source[i];
            }

            double chislitel = 0;
            double znamenatel = 0;
            chislitel = Math.Log(len_spectrum);
            znamenatel = Math.Log(2);
            max_count = (int)(chislitel / znamenatel + 0.5); // надо на 1 меньше т.к. двухточечное уже сделал

            // двоично - инверсное представление
            for (int i = 0; i < len_spectrum; i++)
            {
                int t1;
                int t2;
                int t3;
                t1 = i;
                t3 = 0;
                // вычисляю индекс
                for (int j = 0; j < max_count; j++)
                {
                    t2 = t1;
                    t2 = (t2 & 1);
                    t2 <<= (max_count - 1) - j;
                    t3 += t2;
                    t1 >>= 1;
                }
                A[i] = B[t3];
            }

            //Вычисляю двухточечное преобразование
            for (int i = 0; i < len_spectrum; i += 2)
            {
                B[i] = A[i] + A[i + 1];
                B[i + 1] = A[i] - A[i + 1];
            }
            // переставляю чет-нечет
            for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
            }
            for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i];
            }

            // Вычисляю e^(-j2p/N)
            V = (2 * System.Math.PI) / len_spectrum;
            V *= J;
            //V = Math.Exp(V);
            V = Complex.Exp(V);


            for (int c = 1; c < max_count; c++) //!!!!!!!!!!!!!! max_count !!!!!!!!!!!!!!
            {
                //Вычисляю  W
                tmp = max_count - (c + 1);
                tmp = (int)Math.Pow(2, tmp);
                V_tmp = Complex.Pow(V, tmp);

                for (int i = 0, k = 0; i < Math.Pow(2, c); i++, k++)
                {
                    W[k] = Complex.Pow(V_tmp, i);
                    for (int j = k; j < (tmp - 1) + k; j++)
                    {
                        W[j + 1] = W[j];
                    }
                    k += tmp - 1;
                }
                //Вычисляю преобразование
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    B[i] = A[i] + W[j] * A[i + 1];
                    B[i + 1] = A[i] - W[j] * A[i + 1];
                }
                // переставляю чет-нечет
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
                }
                for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i];
                }
            }
            //spectrum = A;
            return A;
        }

        /// <summary>
        /// FastFourierTransform
        /// </summary>
        /// <param name="source">исходный комплексный сигнал</param>
        /// <returns>комплексный спектр</returns>
        public static IList<Complex> fft(IList<Complex> source)
        {
            /*
            List<Complex> spectrum = new List<Complex>(source.Count); 
            for (int i = 0; i < source.Count; i++)
            {
                spectrum.Add(new Complex(0, 0));
            }
            */
            int max_count;
            int tmp;
            Complex J = new Complex(0, -1);
            Complex V = new Complex();
            Complex V_tmp = new Complex();
            tmp = source.Count;
            //List<Complex> A = new List<Complex>(tmp);
            //List<Complex> B = new List<Complex>(tmp);
            IList<Complex> A = new Complex[tmp];
            IList<Complex> B = new Complex[tmp];

            //надо инициализировать 1 как в исходнике
            //List<Complex > W = new List<Complex >((tmp/2), 1);
            //throw new System.NotImplementedException();
            //List<Complex> W = new List<Complex>((tmp / 2));
            IList<Complex> W = new Complex[tmp / 2];
            for (int i = 0; i < tmp / 2; i++)
            {
                //W.Add(new Complex(1, 0));
                W[i] = 1;
            }

            int len_spectrum;
            len_spectrum = source.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                B[i] = source[i];
            }

            double chislitel = 0;
            double znamenatel = 0;
            chislitel = Math.Log(len_spectrum);
            znamenatel = Math.Log(2);
            max_count = (int)(chislitel / znamenatel + 0.5); // надо на 1 меньше т.к. двухточечное уже сделал

            // двоично - инверсное представление
            for (int i = 0; i < len_spectrum; i++)
            {
                int t1;
                int t2;
                int t3;
                t1 = i;
                t3 = 0;
                // вычисляю индекс
                for (int j = 0; j < max_count; j++)
                {
                    t2 = t1;
                    t2 = (t2 & 1);
                    t2 <<= (max_count - 1) - j;
                    t3 += t2;
                    t1 >>= 1;
                }
                A[i] = B[t3];
            }

            //Вычисляю двухточечное преобразование
            for (int i = 0; i < len_spectrum; i += 2)
            {
                B[i] = A[i] + A[i + 1];
                B[i + 1] = A[i] - A[i + 1];
            }
            // переставляю чет-нечет
            for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
            }
            for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
            {
                A[j] = B[i];
            }

            // Вычисляю e^(-j2p/N)
            V = (2 * System.Math.PI) / len_spectrum;
            V *= J;
            V = Complex.Exp(V);

            for (int c = 1; c < max_count; c++) //!!!!!!!!!!!!!! max_count !!!!!!!!!!!!!!
            {
                //Вычисляю  W
                tmp = max_count - (c + 1);
                tmp = (int)Math.Pow(2, tmp);
                V_tmp = Complex.Pow(V, tmp);

                for (int i = 0, k = 0; i < Math.Pow(2, c); i++, k++)
                {
                    W[k] = Complex.Pow(V_tmp, i);
                    for (int j = k; j < (tmp - 1) + k; j++)
                    {
                        W[j + 1] = W[j];
                    }
                    k += tmp - 1;
                }
                //Вычисляю преобразование
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    B[i] = A[i] + W[j] * A[i + 1];
                    B[i + 1] = A[i] - W[j] * A[i + 1];
                }
                // переставляю чет-нечет
                for (int i = 0, j = 0; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i]; // A[j+len_spectrum/2] = B[i+1];
                }
                for (int i = 1, j = len_spectrum / 2; i < len_spectrum; i += 2, j++)
                {
                    A[j] = B[i];
                }
            }
            //spectrum = A;
            return A;
        }

        /// <summary>
        /// InverseFastFourierTransform
        /// </summary>
        /// <param name="source">комплексный спектр</param>
        /// <returns>исходный дейтвительный сигнал</returns>
        public static IList<Complex> ifft(IList<Complex> aData)
        {
            IList<Complex> spectrum;
            spectrum = fft(aData);
            divideSize(ref spectrum);
            inverseRez(ref spectrum);
            return spectrum;
            //throw new NotImplementedException("ifft");
        }

        private static void divideSize(ref IList<Complex> spectrum)
        {
            int len_spectrum = spectrum.Count;
            for (int i = 0; i < len_spectrum; i++)
            {
                spectrum[i] /= len_spectrum;
            }
        }

        /// <summary>
        /// меняет сортировку
        /// </summary>
        /// <param name="spectrum"></param>
        private static void inverseRez(ref IList<Complex> source)
        {
            List<Complex> data = source.ToList();
            //Не трогаем сортировкой первый 
            data.Reverse(1, source.Count - 1);
            source = data;
        }
    }
}
