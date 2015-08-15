using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HsaClassLibrary.Transform
{   
    /// <summary>
    /// Сигнал:
    /// Z(t) = R(t) + jQ(t)
    /// 
    /// Амплитуда:
    /// A(t) = |Z(t)| = (R(t)^2 + Q(t)^2)^1/2
    /// 
    /// Мгновенная фаза:
    /// ф(t) = arctg[Q(t)/R(t)]
    /// 
    /// Мгновенная частота сигнала определяется по скорости изменения мгновенной фазы:
    /// F(t) = W(t) = dф(t)/dt = [Q'(t)R(t) - R'(t)Q(t)] / (Q^2(t)+R^2(t))
    /// 
    /// Разделив мгновенную частоту сигнала F(t)на 2Pi, мы получим частоту в Гц.
    /// f(t) = [Q(t)'R(t) - Q(t)R(t)']/(R(t)^2 + Q(t)^2)2Pi
    /// 
    /// Период P(t) = 1/f(t)
    /// P(t) = 2Pi (R(t)2 + Q(t)2)/[Q(t)'R(t) - Q(t)R(t)']
    /// 
    /// </summary>
    public class HilbertSpectrum : FourierSpectrum
    {
        /// <summary>
        /// Преобразование Гильберта
        /// </summary>
        public Func<IList<double>, IList<Complex>> transform;
        
        //public HilbertSpectrum(ITransform HilbertTransform = null, IList<double> Data = null)

        /// <summary>
        /// Мгновенная частота (угловая):
        /// W(t) = dф(t)/dt = [Q'(t)R(t) - R'(t)Q(t)] / (Q^2(t)+R^2(t))
        /// </summary>
        public IList<double> RadPs;

        /// <summary>
        /// Мгновенная частота (в Гц):
        /// Получаем разделив мгновенную частоту сигнала W(t) на 2Pi
        /// f(t) = [Q(t)'R(t) - Q(t)R(t)']/(R(t)^2 + Q(t)^2)2Pi
        /// </summary>
        public IList<double> Frequency;

        /// <summary>
        /// Период P(t) = 1/f(t)
        /// P(t) = 2Pi (R(t)2 + Q(t)2)/[Q(t)'R(t) - Q(t)R(t)']
        /// </summary>
        public IList<double> Period;

        /// <summary>
        /// Мгновенная частота (угловая):
        /// W(t) = dф(t)/dt = [Q'(t)R(t) - R'(t)Q(t)] / (Q^2(t)+R^2(t))
        /// </summary>
        /// <returns>Мгновенная частота (угловая)</returns>
        private void getRadPs()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            
            IList<double> x = Real;
            IList<double> y = Imag;
            IList<double> dPhase = new double[count];
            for (int i = 1; i < count; i++)
            {
                dPhase[i] = (y[i] - y[i - 1]) * x[i] - (x[i] - x[i - 1]) * y[i];
            }
            if (x.Count > 1)
            {
                dPhase[0] = dPhase[1];
            }

            for (int i = 0; i < count; i++)
            {
                try
                {
                    result[i] = dPhase[i] / (Abs[i] * Abs[i]);
                    
                    if (result[i] > 500)
                    {
                        result[i] = result[i - 1];
                    }
                    else if (result[i] < -500)
                    {
                        result[i] = result[i - 1];
                    }
                }
                catch
                {
                    result[i] = 0;
                }
            }
            RadPs = result;
        }
        public IList<double> Phase1()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Abs(Phase[i]);
            }
            return result;
        }

        /// <summary>
        /// Получает фазу комплексного числа.
        /// </summary>
        /// <returns>Массив. Фаза комплексного числа в радианах.</returns>
        public IList<double> Phase2()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Atan(spectrum[i].Imaginary / spectrum[i].Real);                                                
            }
            return result;
        }

        /// <summary>
        /// Получает фазу комплексного числа.
        /// </summary>
        /// <returns>Массив. Фаза комплексного числа в радианах.</returns>
        public IList<double> Phase3()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = spectrum[i].Phase;
            }
            return result;
        }
        /// <summary>
        /// Получает фазу комплексного числа.
        /// </summary>
        /// <returns>Массив. Фаза комплексного числа в радианах.</returns>
        public IList<double> Phase4()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Atan2(spectrum[i].Imaginary, spectrum[i].Real);
            }
            return result;
        }        


        public IList<double> RadPs1()
        {
            var count = spectrum.Count;
            IList<double> result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Abs(RadPs[i]);
            }
            return result;
        }

        public IList<double> Period1()
        {
            var count = Frequency.Count;
            IList<double> result = new double[count];

            for (int i = 0; i < count; i++)
            {
                try
                {
                    result[i] = 1 / (Math.Abs(Frequency[i]) + 0.01);

                    /*
                    if (result[i] > 500)
                    {
                        result[i] = result[i - 1];
                    }
                    else if (result[i] < -500)
                    {
                        result[i] = result[i - 1];
                    }
                     */

                }
                catch
                {
                    result[i] = 100;
                }
            }
            return result;
        }

        public IList<double> Period2()
        {
            var count = Frequency.Count;
            IList<double> result = new double[count];

            for (int i = 0; i < count; i++)
            {
                try
                {
                    result[i] = 1 / (Math.Abs(Frequency[i]));


                    if (result[i] > 100)
                    {
                        result[i] = result[i - 1];
                    }
                    else if (result[i] < -100)
                    {
                        result[i] = result[i - 1];
                    }

                }
                catch
                {
                    result[i] = 100;
                }
            }
            return result;
        }

        /// <summary>
        /// Мгновенная частота (в Гц):
        /// Получаем разделив мгновенную частоту сигнала W(t) на 2Pi
        /// f(t) = [Q(t)'R(t) - Q(t)R(t)']/(R(t)^2 + Q(t)^2)2Pi
        /// </summary>
        /// <returns>Мгновенная частота (в Гц)</returns>
        private void getFrequency()
        {
            var count = RadPs.Count;
            IList<double> result = new double[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = RadPs[i] / (2 * Math.PI);
            }
            Frequency = result;
        }

        /// <summary>
        /// Период P(t) = 1/f(t)
        /// P(t) = 2Pi (R(t)2 + Q(t)2)/[Q(t)'R(t) - Q(t)R(t)']
        /// </summary>
        /// <returns>Период P(t) = 1/f(t)</returns>
        private void getPeriod()
        {
            var count = Frequency.Count;
            IList<double> result = new double[count];

            for (int i = 0; i < count; i++)
            {
                try
                {
                    result[i] = 1 / (Frequency[i]);

                    
                    if (result[i] > 500)
                    {
                        result[i] = result[i - 1];
                    }
                    else if (result[i] < -500)
                    {
                        result[i] = result[i - 1];
                    }
                    
                }
                catch
                {
                    result[i] = 0;
                }
            }
            Period = result;
        }

        /// <summary>
        /// выполнить преобразование Гильберта
        /// </summary>
        public override void Transform()
        {
            //переделать
            //где-то здесь нужно проверить размер данных
            IList<double> data;
            data = Source;

            //spectrum = HsaClassLibrary.Transform.HilbertTransform.HTFFT(data);
            spectrum = transform(data);

            //результат
            getAbs();
            getReal();
            getImag();
            getPhase();
            getRadPs();
            getFrequency();
            getPeriod();
            ///  Переделать
            ///  задавать маску что вычислять

        }
    }
}
