using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HsaClassLibrary.Transform
{
    /// <summary>
    ///   Hilbert Transformation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Берем действительный сигнал xr(N)
    /// получаем спектр Xr(N) через FFT
    /// создаем новый спектр Xс(N) = 2Xr(N) 
    /// получаем односторонний спектр, т.е.
    /// обнуляем отрицательные частоты n в итервале [(N/2+1),,N-1]
    /// Xс(0) = Xс(0)/2
    /// Xс(N/2) = Xс(N/2)/2
    /// получаем комплексный сигнал xc(N) через IFFT
    /// действительная часть xc(N).Re = xr(N)
    /// мнимая часть xc(N).Im = xi(N) представляет собой 
    /// искомый сигнал HT(xr(N))
    ///   <para>
    ///     References:
    ///     <list type="bullet">
    ///       <item><description>
    ///         <a href="Ричард Лайонс. Цифровая обработка сигналов - 2006">
    ///         Ричард Лайонс. Цифровая обработка сигналов - 2006
    ///       </description></item>
    ///     </list>
    ///   </para>
    /// </remarks>
    /// </summary>
    public class HilbertTransform
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HilbertTransform));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual IList<double> HT(IList<double> x)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual IList<double> IHT(IList<double> x)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="xcr"></param>
        /// <param name="xci"></param>
        public static void HTFFT_alglib(IList<double> source, out IList<double> xcr, out IList<double> xci)
        {
            int count = source.Count;

            alglib.complex[] f;

            alglib.fftr1d((double[])source, out f);

            for (int i = 0; i < count; i++)
            {
                f[i] = f[i] * 2;
            }

            for (int i = count / 2; i < count; i++)
            {
                f[i] = f[i] * 0;
            }
            alglib.fftc1dinv(ref f);

            xcr = new double[count];
            xci = new double[count];
            for (int i = 0; i < count; i++)
            {
                xcr[i] = f[i].x;
                xci[i] = f[i].y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="xci"></param>
        public static void HTFFT_alglib(IList<double> source, out IList<double> xci)
        {
            int count = source.Count;

            alglib.complex[] f;

            alglib.fftr1d((double[])source, out f);

            for (int i = 0; i < count; i++)
            {
                f[i] = f[i] * 2;
            }

            for (int i = count / 2; i < count; i++)
            {
                f[i] = f[i] * 0;
            }
            alglib.fftc1dinv(ref f);

            xci = new double[count];
            for (int i = 0; i < count; i++)
            {
                xci[i] = f[i].y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<Complex> HTFFT_alglib(IList<double> source)
        {
            int count = source.Count;

            alglib.complex[] f;

            alglib.fftr1d((double[])source, out f);

            for (int i = 0; i < count; i++)
            {
                f[i] = f[i] * 2;
            }

            for (int i = count / 2; i < count; i++)
            {
                f[i] = f[i] * 0;
            }
            f[0] = f[0] * 0;

            alglib.fftc1dinv(ref f);

            IList<Complex> ht = HsaClassLibrary.Transform.TransformHelper.convert(f);               
            return ht;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<Complex> HTFFT(IList<double> source)
        {
            int count = source.Count;

            IList<Complex> ft;
            ft = HsaClassLibrary.Transform.FourierTransform.fft(source);
            
            for (int i = 0; i < count; i++)
            {
                ft[i] = ft[i] * 2;
            }

            for (int i = count / 2; i < count; i++)
            {
                ft[i] = ft[i] * 0;
            }
            ft[0] = ft[0] * 0;

            IList<Complex> ift;
            ift = HsaClassLibrary.Transform.FourierTransform.ifft(ft);
            return ift;
        }
    }
}
