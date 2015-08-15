using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using EmdClassLibrary.HilbertTransform;

namespace HsaClassLibrary.Transform
{
    
    /// <summary>
    /// Берем исходный сигнал, разбираем его на составляющие с помощью метода EMD
    /// Некоторые компоненты могут быть отброшены для удаления шума и\или удаления тренда из сигнала
    /// Для каждой компоненты сигнала получаем аналитеческий сигнал с помощью преобразования Гильберта
    /// По аналитическому сигналу каждой составляющей вычисляем спектр: амплитуду, фазу и частоту (период)
    /// Спектральные характеристики всех составляющих могут быть обобщены для получения амплитудно-частотно-временной характеристики
    /// Спектральные характеристики фаза и частота (период) могут быть использованиы в качестве параметров для индикаторов или методов прогнозирования
    /// </summary>
    public abstract class HilbertHuangTransformAbstract
    {
        HsaClassLibrary.Decomposition.EmDecomposition emDecomposition;

        /// <summary>
        /// 
        /// </summary>
        public HsaClassLibrary.Decomposition.EmDecomposition EmDecomposition
        {
            get
            {
                return emDecomposition;
            }
            set
            {
                emDecomposition = value;
            }
        }

        HilbertSpectrum hilbertSpectrum;

        /// <summary>
        /// 
        /// </summary>
        public HilbertSpectrum HilbertSpectrum
        {
            get
            {
                return hilbertSpectrum;
            }
            set
            {
                hilbertSpectrum = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<HilbertSpectrum> HSA;

        /// <summary>
        /// Компонента сигнала на итерации
        /// Cn(k) = Hi(k)
        /// </summary>
        private List<IList<double>> C;

        /// <summary>
        /// R[0] = Входному ряду Y
        /// Сигнал на итерации, отфильтрованный, без высочастотных составляющих
        /// R[n](k) = R[n-1](k) – C[n](k)
        ///??? или R[n](k) = R[n-1](k) – H[i](k)
        /// </summary>
        private List<IList<double>> R;

        /// <summary>
        /// Исходный сигнал
        /// </summary>
        private IList<double> Data;

        /// <summary>
        /// 
        /// </summary>
        public virtual void Decompose()
        {
            emDecomposition.DataLoad(Data);
            //emDecomposition.Decompose();
            emDecomposition.Decomposition();
            C = emDecomposition.C;
            R = emDecomposition.R;
        }
    }
}
