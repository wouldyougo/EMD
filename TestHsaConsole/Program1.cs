using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program1
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// test log4net
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();
            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Debug("This is a debug message");
            log.Warn("This is a warn message");
            log.Error("This is a error message");
            log.Fatal("This is a fatal message");
            log.Info("This is a info message");
            //Console.ReadLine();
            Console.ReadKey();
        }

        /// <summary>
        /// тест getRandomVector
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();

            //log.Debug("This is a debug message");
            //log.Info("This is a info message");
            //log.Warn("This is a warn message");
            //log.Error("This is a error message");
            //log.Fatal("This is a fatal message");
            IList<double> R = getRandomVector(50, 100);
            //log.Info("R " + R);
            int i = 2;
            //log.Info("R " + alglib.ap.format(R.ToArray(), 2));
            log.Info("R" + i + " " + alglib.ap.format(R.ToArray(), 2));
            //System.Console.WriteLine("R {0}", alglib.ap.format(R.ToArray(), 3)); // EXPECTED: 0.125
            Console.ReadKey();
        }
        /// <summary>
        /// Получить рандомный вектор
        /// </summary>
        /// <param name="n">количество элементов</param>
        /// <param name="max">максимальное значение</param>
        /// <returns></returns>
        private static IList<double> getRandomVector(int n, int max)
        {
            Random rand = new Random(0);
            IList<double> b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = Convert.ToDouble(rand.Next(max));
                //throw new System.NotImplementedException();
            }
            return b;
        }
    }
}
