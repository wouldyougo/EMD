using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using HsaClassLibrary.Transform;
using HsaClassLibrary.Decomposition;


namespace HsaClassLibrary.Helpers
{

    /// <summary>
    /// Реализует паттерн Фабричный метод
    /// </summary>
    /// <remarks>Создает объект реализующий метод EMD</remarks>
    public class HhtCreator
    {
        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as before field init
        /// </summary>
        static HhtCreator()
        {
        }

        /// <summary>
        /// Singleton EmDecomposition
        /// </summary>
        static readonly EmDecomposition singletonEMD = new EmDecomposition()
        {
            InterpolationMethod = MathHelper.Interpolation_3,
            StopConditionSeparate = HhtCreator.StopConditionSeparate(),
            StopCondition = HhtCreator.StopCondition()
        };

        /// <summary>
        /// Singleton EmDecomposition
        /// </summary>
        public static EmDecomposition SingletonEMD
        {
            get
            {
                return singletonEMD;
            }
        }

        /// <summary>
        /// Создаем критерий останова для процесса отсеивания
        /// </summary>
        /// <param name="stopConditionSeparate">Критерий останова для процесса отсеивания</param>
        /// <returns>Критерий останова для процесса отсеивания</returns>
        public static IStopCondition StopConditionSeparate(EnumStopConditionSeparate stopConditionSeparate = EnumStopConditionSeparate.КоличествоИтерацийИлиДостигнутаТочность)
        {
            IStopCondition _StopConditionSeparate;
            //Создаем критерий останова для процесса отсеивания
            switch (stopConditionSeparate)
            {
                case EnumStopConditionSeparate.ДостигнутаТочностьОтсеивания:
                    _StopConditionSeparate = new StopConditionSeparate_1()
                    {
                        StopConditionType = (int)EnumStopConditionSeparate.ДостигнутаТочностьОтсеивания,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //J = 8;
                        J = 0,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //Нормализованная квадратичная разность между двумя последовательными операциями приближения
                        S = 0.001,
                        dS = 0.001
                    };
                    break;
                case EnumStopConditionSeparate.КоличествоИтерацийИлиДостигнутаТочность:
                    _StopConditionSeparate = new StopConditionSeparate_2()
                    {
                        StopConditionType = (int)EnumStopConditionSeparate.КоличествоИтерацийИлиДостигнутаТочность,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //J = 8;
                        J = 8,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //Нормализованная квадратичная разность между двумя последовательными операциями приближения
                        S = 0.001,
                        dS = 0.001
                    };
                    break;
                default:
                    //EnumStopCondition.КоличествоИтераций:
                    _StopConditionSeparate = new StopCondition()
                    {
                        StopConditionType = (int)EnumStopCondition.КоличествоИтераций,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //J = 8;
                        J = 8,
                        //Задаем максимальное количество внутренних итераций отсеивания (определение высокочастотной составляющей)
                        //Нормализованная квадратичная разность между двумя последовательными операциями приближения
                        S = 0
                    };
                    break;
            }
            return _StopConditionSeparate;
        }

        /// <summary>
        /// Создаем критерий останова
        /// </summary>
        /// <param name="stopCondition">перечисление критерий останова</param>
        /// <returns></returns>
        public static IStopCondition StopCondition(EnumStopCondition stopCondition = EnumStopCondition.КоличествоИтераций)
        {
            IStopCondition _StopCondition;
            //Создаем метод с заданным критерием останова
            switch (stopCondition)
            {
                case EnumStopCondition.КоличествоЭкстремумов:
                    _StopCondition = new StopCondition_1()
                    {
                        StopConditionType = (int)EnumStopCondition.КоличествоЭкстремумов,
                        //Задаем максимальное количество внешних итераций (компонентов сигнала)
                        //I = 8;                       
                        J = 8
                    };
                    break;
                case EnumStopCondition.КоличествоИтерацийИлиЭкстремумов:
                    _StopCondition = new StopCondition_2()
                    {
                        StopConditionType = (int)EnumStopCondition.КоличествоИтерацийИлиЭкстремумов,
                        //Задаем максимальное количество внешних итераций (компонентов сигнала)
                        //I = 8;                       
                        J = 8
                    };
                    break;
                default:
                    //EnumStopCondition.КоличествоИтераций:
                    _StopCondition = new StopCondition()
                    {
                        StopConditionType = (int)EnumStopCondition.КоличествоИтераций,
                        //Задаем максимальное количество внешних итераций (компонентов сигнала)
                        //I = 8;                       
                        J = 8
                    };
                    break;
            }

            return _StopCondition;
        }

        /// <summary>
        /// Создать новый объект EMD
        /// </summary>
        /// <param name="interpolation"></param>
        /// <param name="stopCondition"></param>
        /// <param name="stopConditionSeparate"></param>
        /// <returns></returns>
        public EmDecomposition EmdFactoryMethod(EnumInterpolation interpolation = EnumInterpolation.КубическийСплайн, 
                                                EnumStopCondition stopCondition = EnumStopCondition.КоличествоИтерацийИлиЭкстремумов, 
                                                EnumStopConditionSeparate stopConditionSeparate = EnumStopConditionSeparate.КоличествоИтерацийИлиДостигнутаТочность)
        {
            EmDecomposition emd;
            Func<IList<double>, IList<double>, IList<double>, IList<double>> _Interpolation;
            IStopCondition _StopConditionSeparate;
            IStopCondition _StopCondition;

            //Создаем метод интерполяции
            switch (interpolation)
            {
                case EnumInterpolation.КубическийСплайн:
                    _Interpolation = MathHelper.Interpolation_3;
                    break;
                default:
                    //throw new NotImplementedException();
                    _Interpolation = MathHelper.Interpolation_3;
                    break;
            }

            // Создаем критерий останова
            _StopCondition = HhtCreator.StopCondition(stopCondition);
            // Создаем критерий останова для процесса отсеивания
            _StopConditionSeparate = HhtCreator.StopConditionSeparate(stopConditionSeparate);            
            

            //emd = new EmdImplementationClass1(Interpolation, StopCriterion, StopSiftCriterion);
            emd = new EmDecomposition_2()
            {
                InterpolationMethod = _Interpolation,
                StopConditionSeparate = _StopConditionSeparate,
                StopCondition = _StopCondition               
            };
            
            return emd;            
        }

        /// <summary>
        ///Создаем метод преобразования гильберта
        /// </summary>
        /// <param name="ht">перечисление метод преобразования гильберта</param>
        /// <returns>HilbertSpectrum</returns>
        public HilbertSpectrum HsaFactoryMethod(EnumHilbertTransform ht)
        {
            HilbertSpectrum hsa;

            hsa = new HilbertSpectrum()
            {
                //Создаем метод преобразования гильберта
                transform = getHilbertTransform(ht)
            };

            return hsa;
        }

        /// <summary>
        /// словарь с методами HilbertTransform
        /// </summary>
        private Dictionary<EnumHilbertTransform, Func<IList<double>, IList<Complex>>> hilbertTransform =
            new Dictionary<EnumHilbertTransform, Func<IList<double>, IList<Complex>>>
            {
                { EnumHilbertTransform.HilbertTransform,        HsaClassLibrary.Transform.HilbertTransform.HTFFT },
                { EnumHilbertTransform.HilbertTransformAlglib,  HsaClassLibrary.Transform.HilbertTransform.HTFFT_alglib },
            };

        /// <summary>
        /// возвращает метод HilbertTransform
        /// </summary>
        /// <param name="ht">EnumHilbertTransform</param>
        /// <returns>Func HilbertTransform</returns>
        public Func<IList<double>, IList<Complex>> getHilbertTransform(EnumHilbertTransform ht)
        {
            if (!hilbertTransform.ContainsKey(ht))
                throw new ArgumentException(string.Format("Operation {0} is invalid", ht), "EnumHilbertTransform");
                //throw new NotImplementedException();
            return hilbertTransform[ht];
        }
    }
}
