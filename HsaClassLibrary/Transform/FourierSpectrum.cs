using System.Collections.Generic;
using System;
using System.Numerics;
using System.Linq; //IList<T>.ToList()

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
    /// </summary>
    public class FourierSpectrum : ITransform
    {
        /// <summary>
        /// Преобразование Фурье
        /// </summary>
        public Func<IList<double>, IList<Complex>> transform;

        /// <summary>
        /// Оригинальный сигнал
        /// </summary>
        private IList<double> source;
       
        /// <summary>
        /// Оригинальный сигнал
        /// </summary>
        public IList<double> Source
        {
            set
            {
                source = value;
            }
            get
            {
                return source;
            }
        }
        /// <summary>
        /// комплексный спектр
        /// </summary>
        public IList<Complex> spectrum;

        /// <summary>
        /// Действительная часть аналитического сигнала
        /// Z(t) = XR(t) + jXQ(t)
        /// </summary>
        public IList<double> Real;

        /// <summary>
        /// Мнимая часть аналитического сигнала
        /// Z(t) = XR(t) + jXQ(t)
        /// </summary>
        public IList<double> Imag;

        /// <summary>
        /// Модуль аналитического сигнала
        /// Огибающая для аналитического сигнала
        /// A(t) = (R(t)^2 + Q(t)^2)^1/2
        /// </summary>
        public IList<double> Abs;

        /// <summary>
        /// Мгновенная фаза:
        /// ф(t) = arctg[Q(t)/R(t)]
        /// </summary>
        public IList<double> Phase;


        /// <summary>
        /// Получает величину - абсолютное значение (или величину) комплексного числа.
        /// </summary>
        /// <returns>Массив. Модуль текущего экземпляра./returns>
        public void getAbs()
        {
            IList<double> tmp = new double[spectrum.Count];
            // получаем спектр амплитуд
            for (int i = 0; i < spectrum.Count; i++)
            {
                //tmp[i] = Complex.Abs(spectrum[i]);                
                tmp[i] = spectrum[i].Magnitude;
            }
            Abs = tmp;
        }

        /// <summary>
        /// Получает вещественную часть текущего объекта System.Numerics.Complex.
        /// </summary>
        /// <returns>Массиав. Вещественная часть комплексного числа.</returns>
        public void getReal()
        {
            IList<double> tmp = new double[spectrum.Count];
            // получаем спектр амплитуд
            for (int i = 0; i < spectrum.Count; i++)
            {
                tmp[i] = spectrum[i].Real;
            }
            Real = tmp;
        }

        /// <summary>
        /// Получает мнимую часть текущего объекта System.Numerics.Complex.
        /// </summary>
        /// <returns>Массив. Мнимая часть комплексного числа.</returns>
        public void getImag()
        {
            IList<double> tmp = new double[spectrum.Count];
            // получаем спектр амплитуд
            for (int i = 0; i < spectrum.Count; i++)
            {
                tmp[i] = spectrum[i].Imaginary;
            }
            Imag = tmp;
        }

        /// <summary>
        /// Получает фазу комплексного числа.
        /// </summary>
        /// <returns>Массив. Фаза комплексного числа в радианах.</returns>
        public void getPhase()
        {
            IList<double> tmp = new double[spectrum.Count];
            // получаем спектр амплитуд
            for (int i = 0; i < spectrum.Count; i++)
            {
                //tmp[i] = arg(spectrum[i]);
                tmp[i] = spectrum[i].Phase;
            }
            //throw new System.NotImplementedException();
            Phase = tmp;
        }

        /// <summary>
        /// выполнить преобразование Фурье
        /// </summary>
        public virtual void Transform()
        {
            IList<double> data;
            throw new System.NotImplementedException();
            //здесь дополняются данные нулями
            //data = HsaClassLibrary.Transform.TransformHelper.prepareZero(source, source.Count);
            data = source;

            //spectrum = HsaClassLibrary.Transform.FourierTransform.fft(data);
            spectrum = transform(data);

            //результат
            getAbs();
            getReal();
            getImag();
            getPhase();
        }
    }
}
