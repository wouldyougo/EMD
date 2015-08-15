using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace TestConsoleApplication
{
    class Program7
    {
        public static int Main(string[] args)
        {
            //IList<double> R = getRandomVector(500, 100);
            //Читаем файлик OHLCV
            //string path = "z:\\YandexDisk\\Data\\GAZP_test_1h.txt";
            //TS.DataSource.BarList BarList = new TS.DataSource.BarList(path);

            HsaClassLibrary.Helpers.HhtCreator emdcr = new HsaClassLibrary.Helpers.HhtCreator();
            //EmdClassLibrary.EmdAbstractClass emd = emdcr.FactoryMethod(EmdClassLibrary.InterpolationEnum.ЛинейнаяИнтерполяция, EmdClassLibrary.StopCriterionEnum.КоличествоИтераций, EmdClassLibrary.StopSiftCriterionEnum.КоличествоИтераций);
            //EmdClassLibrary.EmdAbstractClass emd = emdcr.FactoryMethod(EmdClassLibrary.InterpolationEnum.ЛинейнаяИнтерполяция, EmdClassLibrary.StopCriterionEnum.КоличествоИтераций, EmdClassLibrary.StopSiftCriterionEnum.ДостигнутаТочностьОтсеивания);
            HsaClassLibrary.Transform.HilbertSpectrum Hsa = emdcr.HsaFactoryMethod(HsaClassLibrary.Transform.EnumHilbertTransform.HilbertTransform);

            //Выбираем для анализа
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCosAM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //y <- sin(2*pi*n*Tn/(T*N))
            IList<double> R = HsaClassLibrary.Helpers.MathHelper.getSin(256, 1, 2.0 * Math.PI / 256.0);

            //Данные для вывода в файл
            IList<IList<double>> data = new List<IList<double>>();
            data.Add(R);
            Hsa.Source = R;
            /*
            //заменяем коэффичиента преобразования
            //Hsa.HilbertTransform = new EmdClassLibrary.HilbertTransform1();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);
            //Hsa.Data = Hsa.Xi;
            //Hsa.HS(true, false, false, false);
            //data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform1();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform2();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform3();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform4();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform5();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);
            /*
            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform6();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new EmdClassLibrary.Transform.HilbertTransform7();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.Xi);
            */

            //0
            double[] HT0 = new double[R.Count];
            R.CopyTo(HT0, 0);

            Accord.Math.HilbertTransform.FHT(HT0, AForge.Math.FourierTransform.Direction.Forward);
            data.Add(HT0);

            //1
            //double[] dHT = new double[R.Count];
            double[] HT1 = new double[R.Count];
            R.CopyTo(HT1, 0);

            Accord.Math.HilbertTransform.FHT2(HT1, AForge.Math.FourierTransform.Direction.Forward);
            data.Add(HT1);

            // 2 
            // Правильно но странно
            // инвертирован вызов преобразований фурье
            double[] HT2r = new double[R.Count];
            double[] HT2i = new double[R.Count];

            AForge.Math.Complex[] HT2 = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                HT2[i].Re = R[i];
                HT2[i].Im = 0;
            }
            Accord.Math.HilbertTransform.FHT(HT2, AForge.Math.FourierTransform.Direction.Forward);

            for (int i = 0; i < R.Count; i++)
            {
                HT2r[i] = HT2[i].Re;
                HT2i[i] = HT2[i].Im;
            }
            data.Add(HT2r);
            data.Add(HT2i);

            //3
            /*
            ///  QW модифицировал Accord.Math.HilbertTransform.FHT2
            ///  неправильно - инвертирован знак мнимой части            
            double[] HT3r = new double[R.Count];
            double[] HT3i = new double[R.Count];

            //AForge.Math.Complex[] cHT = new AForge.Math.Complex[R.Count];
            AForge.Math.Complex[] HT3 = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                HT3[i].Re = R[i];
                HT3[i].Im = 0;
            }
            Accord.Math.HilbertTransform.FHT2(HT3, AForge.Math.FourierTransform.Direction.Forward);

            //double[] HTr = new double[R.Count];
            //double[] HTi = new double[R.Count];
            HT3r = new double[R.Count];
            HT3i = new double[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                HT3r[i] = HT3[i].Re;
                HT3i[i] = HT3[i].Im;
            }
            data.Add(HT3r);
            data.Add(HT3i);
            */
            //4 не совсем Правильно - постоянная
            double[] X4 = new double[R.Count];
            IList<double> HT4r = new double[R.Count];
            IList<double> HT4i = new double[R.Count];
            R.CopyTo(X4, 0);

            //HsaClassLibrary.Transform.HilbertTransformFFT FHT = new HsaClassLibrary.Transform.HilbertTransformFFT();
            //HT4i = FHT.(X4, out HT4r);
            HsaClassLibrary.Transform.HilbertTransform.HTFFT_alglib(X4, out HT4r, out HT4i);
            //сохраняем для вывода
            data.Add(HT4r);
            data.Add(HT4i);

            //--            
            //4 Правильно
            double[] X5 = new double[R.Count];
            IList<double> HT5r = new double[R.Count];
            IList<double> HT5i = new double[R.Count];
            R.CopyTo(X5, 0);

            IList<Complex> HT5 = HsaClassLibrary.Transform.HilbertTransform.HTFFT(X5);
            HsaClassLibrary.Transform.TransformHelper.convert(HT5, out HT5r, out HT5i);
            //сохраняем для вывода
            data.Add(HT5r);
            data.Add(HT5i);
            //--            

            //--            
            //4 Правильно
            double[] X6 = new double[R.Count];
            IList<double> HT6r = new double[R.Count];
            IList<double> HT6i = new double[R.Count];
            R.CopyTo(X6, 0);

            IList<Complex> HT6 = HsaClassLibrary.Transform.HilbertTransform.HTFFT_alglib(X6);
            HsaClassLibrary.Transform.TransformHelper.convert(HT6, out HT6r, out HT6i);
            //сохраняем для вывода
            data.Add(HT6r);
            data.Add(HT6i);
            //--            


            System.Console.WriteLine("HilbertTransform выполнена.");
            System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            HsaClassLibrary.Helpers.ReadWriteHelper emdWriter = new HsaClassLibrary.Helpers.ReadWriteHelper();
            emdWriter.WriteCSV(data, "D:\\hsa7.csv");
            System.Console.WriteLine("Файл csv создан.\nD:\\hsa7.csv");
            System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            return 0;
        }
    }
}
