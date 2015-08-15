using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using HsaClassLibrary.Helpers;

namespace HsaClassLibrary.Decomposition
{
    /// <summary>
    /// Реализует паттерн Шаблонный метод
    /// Реализует паттерн Стратегия
    /// </summary>
    /// <remarks>
    /// Реализует паттерн Шаблонный метод: реализует Шаблонный метод EMD
    /// Реализует паттерн Стратегия: инкапсулирует Стратегии интерполяции и критерий останова
    /// </remarks>
    public class EmDecomposition : HsaClassLibrary.Decomposition.IEmpiricalModeDecomposition//, StopCriterionInterface
	{
        /// <summary>
        /// 
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmDecomposition));

        /// <summary>
        /// Огибающая сигнала верхняя
        /// </summary>
        private IList<double> Ub;

        /// <summary>
        /// Огибающая сигнала нижняя
        /// </summary>
        private IList<double> Ut;

        /// <summary>
        /// Средняя между огибающими
        /// Mi(k) = (Ut(k) + Ub(k))/2
        /// </summary>
        private IList<double> M;

        /// <summary>
        /// Высокочастотная составляющая сигнала
        /// Hi(k)  = Hi-1(k) – Mi(k)
        /// H[0] начальное значение
        /// H[1] конечное  значение 
        /// </summary>
        protected IList<IList<double>> H = new IList<double>[2];
         

        /// <summary>
        /// Длина исходного сигнала
        /// </summary>
        private int yCount;


        /// <summary>
        /// Компонента сигнала на итерации
        /// Cn(k) = Hi(k)
        /// </summary>
        //private List<IList<double>> C;
        public List<IList<double>> C;

        /// <summary>
        /// R[0] = Входному ряду Y
        /// Сигнал на итерации, отфильтрованный, без высочастотных составляющих
        /// R[n](k) = R[n-1](k) – C[n](k)
        ///??? или R[n](k) = R[n-1](k) – H[i](k)
        /// </summary>
        //private List<IList<double>> R;
        public List<IList<double>> R;

        /// <summary>
        /// Критерий останова отсеивания
        /// </summary>
        private IStopCondition _StopConditionSeparate;

        /// <summary>
        /// Критерий останова алгоритма
        /// </summary>
        private IStopCondition _StopCriterion;

        
        /// <summary>
        /// 
        /// </summary>
        public EmDecomposition(
            Func<IList<double>, IList<double>, IList<double>, IList<double>> Interpolation, 
            IStopCondition StopCriterion, 
            IStopCondition StopSiftCriterion)
		{
            this.InterpolationMethod = Interpolation;
            this.StopConditionSeparate = StopSiftCriterion;
            this.StopCondition = StopCriterion;
		}
        /// <summary>
        /// 
        /// </summary>
        public EmDecomposition()
        {

        }

        /// <summary>
        /// Метод интерполяции
        /// </summary>
        //private IInterpolation _Interpolation;

        //// <summary>
        //// Интерполяция
        //// </summary>
        //// <param name="x">Вектор X</param>
        //// <param name="y">Вектор Y</param>
        //// <param name="x_new">Вектор X новая сетка</param>
        //// <returns>Вектор Y новая сетка</returns>        
        //IList<double> Interpolation(IList<double> x, IList<double> y, IList<double> x_new);
        
        /// <summary>
        /// Метод интерполяции
        /// <param name="x">Вектор X</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="x_new">Вектор X новая сетка</param>
        /// <returns>Вектор Y новая сетка</returns>        
        /// </summary>
        private Func<IList<double>, IList<double>, IList<double>, IList<double>> _Interpolation;


        /// <summary>
        /// Метод интерполяции
        /// </summary>
        public Func<IList<double>, IList<double>, IList<double>, IList<double>> InterpolationMethod
        {
            get
            {
                //throw new System.NotImplementedException();
                return _Interpolation;
            }
            set
            {
                _Interpolation = value;
            }
        }

        /// <summary>
        /// Критерий останова отсеивания
        /// </summary>
        public IStopCondition StopConditionSeparate
        {
            get
            {
                return _StopConditionSeparate;
            }
            set
            {
                _StopConditionSeparate = value;
            }
        }

        /// <summary>
        /// Критерий останова алгиритма
        /// </summary>
        public IStopCondition StopCondition
        {
            get
            {
                return _StopCriterion;
            }
            set
            {
                _StopCriterion = value;
            }
        }

        /// <summary>
        /// Интерполяция кубическим сплайном
        /// </summary>
        /// <param name="x">Вектор X</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="source">Входной вектор, для каждого i которого будет вычисляться Y</param>
        /// <returns>Вектор Y</returns>    
        public virtual IList<double> Interpolation(IList<double> x, IList<double> y, IList<double> source)
        {
            //throw new NotImplementedException();
            IList<double> x_new = new double[source.Count];
            IList<double> y_new;

            //Для входного вектора source, для каждого i будет вычисляться значение Y
            //Формируется вектор X со значениями i новой сетки
            for (int i = 0; i < source.Count; i++)
            {
                x_new[i] = i;
            }
            //используем одну из реализаций интерполяции 
            y_new = this._Interpolation(x, y, x_new);
            return y_new;
        }

        
        //// <summary>
        //// Абстрактный метод критерия останова
        //// </summary>
        //// <returns>true если остановка</returns>
        //public abstract bool CheckStop(int i);


        /// <summary>
        /// Реализация шаблонного метода 
        /// ЭМПИРИЧЕСКОЙ МОДОВОЙ ДЕКОМПОЗИЦИИ СИГНАЛОВ
        /// </summary>
        public virtual void Decomposition()
		{
            //System.DateTime time = System.DateTime.Now;

            // Сигнал на итерации, отфильтрованный, без высочастотных составляющих
            IList<double> Ri = R[0];
            // Операция 0
            // H = R[i]
            IList<double> Hi = Ri;
            // Для проверки на останов
            List<IList<double>> R2 = null;
            int i = 0;
            log.Info("R" + i + " " + alglib.ap.format(Hi.ToArray(), 2));
            do
            {
                // Операция 1-5. отдельный метод 
                // Выделение высокочастотной составляющей
                Hi = Sift(Hi);

                // Операция 6.
                //     Сохраниене высокочастотной составляющей и удаление ее из текущего сигнала
                //     Cn(k) = Hi(k);
                C.Add(Hi);
                log.Info("C" + i + " " + alglib.ap.format(Hi.ToArray(), 2));

                // Операция 7. 
                //     Удаление высокочастотной составляющей из текущего сигнала
                //     Rn(k) = Rn-1(k) – Cn(k)
                //Ri = Sub(R[R.Count - 1], C[C.Count - 1]);
                //Ri = R[R.Count - 1];
                //Ri = Sub(Ri, C[C.Count - 1]);
                Ri = MathHelper.Sub(Ri, Hi);
                R.Add(Ri);
                Hi = Ri;
                i++;
                log.Info("R" + i + " " + alglib.ap.format(Hi.ToArray(), 2));
                //Выбираем значение для проверки на отсанов алгоритма
                R2 = R.GetRange(R.Count-2, 2);
                //throw new System.NotImplementedException();
            } 
            // Операция 8. 
            // Проверка на завершение декомпозиции           
            while (this._StopCriterion.CheckContinue(i, R2));            
            //throw new System.NotImplementedException();            

            //System.DateTime time2 = System.DateTime.Now;
            //log.Info("T " + alglib.ap.format(System.DateTime.Compare(time, time2), 4));           
		}


        /// <summary>
        /// Просеивание: внутренний цикл выделения из сигнала высокочастотной составляющей
        /// </summary>
        /// <param name="Hi"></param>
        /// <returns></returns>
        public virtual IList<double> Sift(IList<double> Hi)
        {
            // x of local maxima
            IList<double> xMax;

            // y of local maxima
            IList<double> yMax;

            // x of local minima
            IList<double> xMin;

            // y of local minima
            IList<double> yMin;

            //// Огибающая сигнала верхняя
            //IList<double> Ub;

            //// Огибающая сигнала нижняя
            //IList<double> Ut;

            //// Средняя между огибающими
            //// Mi(k) = (Ut(k) + Ub(k))/2
            //IList<double> M;

            //// Высокочастотная составляющая сигнала
            //// Hi(k)  = Hi-1(k) – Mi(k)
            //// H[0] начальное значение
            //// H[1] конечное  значение 
            //IList<IList<double>> H = new IList<double>[2];
            int j = 0;
            H[1] = Hi;

            log.Info("H" + j + " " + alglib.ap.format(H[1].ToArray(), 2));
            _StopConditionSeparate.Reset();
            do
            {
                H[0] = H[1];
                // Операция 1. 
                FindExt(H[0], out xMax, out yMax, out xMin, out yMin);
                //// Операция 2.	
                ////     вычисляем верх-нюю Ut(k) и нижнюю Ub(k) огибающие процесса соответственно,
                ////     по максимумам и минимумам
                Ut = Interpolation(xMax, yMax, H[0]);
                Ub = Interpolation(xMin, yMin, H[0]);
                //throw new System.NotImplementedException();
                //// Операция 3.
                ////     Нахождение средней между огибающими:
                ////     Mi(k) = (Ut(k) + Ub(k))/2
                M = MathHelper.Sum(Ub, Ut);
                M = MathHelper.Mul(M, 0.5);

                log.Info("Ut" + j + " " + alglib.ap.format(Ut.ToArray(), 2));
                log.Info("Ub" + j + " " + alglib.ap.format(Ub.ToArray(), 2));
                log.Info("M" + j + " " + alglib.ap.format(M.ToArray(), 2));

                //// Операция 4. 
                ////     Вычитание из текущего сигнала средней между огибающими:
                ////     Hi(k)  = Hi-1(k) – Mi(k)
                H[1] = MathHelper.Sub(H[0], M);
                //throw new System.NotImplementedException();
                j++;
                
                log.Info("H" + j + " " + alglib.ap.format(H[1].ToArray(), 2));
            }
            //// Операция 5.
            while (this._StopConditionSeparate.CheckContinue(j, H));                
            //throw new System.NotImplementedException();
            return H[1];
        }   

        /// <summary>
        /// Операция 0. Загрузка исходного ряда
        /// </summary>
        /// <param name="source">Исодный ряд для декомпозиции</param>
        public void DataLoad(IList<double> source)
        {
            // Операция 0. Загрузка исодного ряда
            //!!! здесь надо бы клонировать даннные
            IList<double> Y = source;
            yCount = Y.Count;

            R = new List<IList<double>>();
            C = new List<IList<double>>();

            // Операция 0.1
            //!!! здесь может быть несколько вариантов
           
            //например сигнал может быть искусственно продлен с правой стороны 
            //для получения лучшего результат декомпозиции на границах сигнала
            //hrow new System.NotImplementedException();

            //Простой вариант без продления 
            //Разбираемый сигнал принимаем равным входному ряду
            //R[0] = Y 
            R.Add(Y);
        }

        /// <summary>
        /// Нахождение экстремуы входного ряда
        /// </summary>
        /// <param name="source1">Входной ряд</param>
        /// <param name="xMax">Максимумы X</param>
        /// <param name="yMax">Максимумы Y</param>
        /// <param name="xMin">Минимумы X</param>
        /// <param name="yMin">Минимумы Y</param>
        public virtual void FindExt(IList<double> source1, out IList<double> xMax, out IList<double> yMax, out IList<double> xMin, out IList<double> yMin)
        {
            xMax = new List<double>();
            yMax = new List<double>();
            xMin = new List<double>();
            yMin = new List<double>();

            IList<double> y = source1;

            if (y.Count > 0)
            {
                xMin.Add(0);
                yMin.Add(y[0]);
                xMax.Add(0);
                yMax.Add(y[0]);

                for (int i = 1; i < y.Count - 1; i++)
                {
                    if ((y[i] < y[i - 1]) && (y[i] <= y[i + 1]))
                    {
                        xMin.Add(i);
                        yMin.Add(y[i]);
                    }
                    else if ((y[i] > y[i - 1]) && (y[i] >= y[i + 1]))
                    {
                        xMax.Add(i);
                        yMax.Add(y[i]);
                    }
                }
                
                xMin.Add(y.Count - 1);
                yMin.Add(y[y.Count - 1]);
                xMax.Add(y.Count - 1);
                yMax.Add(y[y.Count - 1]);
            }
        }
    }
    //Исправить значения экстремумов на границе
    //+ Реализовать методы останова алгоритма
    //Реализовать методы экстраполяции компонентов сигнала


    //Реализовать метод удаления тренда
    //Реализовать метод удаления шума
}

