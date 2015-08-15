using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program6
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
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCos(128, 1, 2.0 * Math.PI / 32.0);
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCos(128, (x) => { return 1; }, (x) => { return 2.0 * Math.PI / 32.0; }, (c) => { return 0; });
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCos(128, (x) => { return 1; }, (x) => { return (2.0 * Math.PI * x/ 32.0) * Math.Cos(2.0 * Math.PI * x/ 256.0); }, (c) => { return 0; });
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCosAM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCosFM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCosPM(256, 1, 2.0 * Math.PI / 32.0, 1,  2.0 * Math.PI / 128.0);
            IList<double> R = HsaClassLibrary.Helpers.MathHelper.getSin(256, 1, 2.0 * Math.PI / 256.0);
            //Данные для вывода в файл
            IList<IList<double>> data = new List<IList<double>>();
            Hsa.Source = R;

            Hsa.Transform();
            //сохраняем для вывода
            data.Add(R);
            data.Add(Hsa.Real);
            data.Add(Hsa.Imag);
            data.Add(Hsa.Abs);
            data.Add(Hsa.Phase);
            data.Add(Hsa.Phase1());
            data.Add(Hsa.Phase2());
            data.Add(Hsa.Phase3());
            data.Add(Hsa.Phase4());
            //data.Add(Hsa.RadPs);
            //data.Add(Hsa.RadPs1());
            //data.Add(Hsa.Frequency);
            //data.Add(Hsa.Period);
            //data.Add(Hsa.Period1());
            //data.Add(Hsa.Period2());          

            /*
            //заменяем метод преобразования
            Hsa.transform = HsaClassLibrary.Transform.HilbertTransform.HTFFT_alglib;
            //R = HsaClassLibrary.Helpers.MathHelper.getCos(128, (x) => { return 2; }, (x) => { return 2.0 * Math.PI / 16.0; }, (x) => { return 0; });
            //R = HsaClassLibrary.Helpers.MathHelper.getCos(128, (x) => { return Math.Cos(2.0 * Math.PI * x/ 32.0); }, (x) => { return 2.0 * Math.PI / 16.0; }, (x) => { return 0; });
            //R = HsaClassLibrary.Helpers.MathHelper.getCosAM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //R = HsaClassLibrary.Helpers.MathHelper.getCosFM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //R = HsaClassLibrary.Helpers.MathHelper.getSin(256, 1, 2.0 * Math.PI / 256.0);
            Hsa.Source = R;
            Hsa.Transform();
            //сохраняем для вывода
            data.Add(R);
            data.Add(Hsa.Real);
            data.Add(Hsa.Imag);
            data.Add(Hsa.Abs);
            data.Add(Hsa.Phase);
            data.Add(Hsa.RadPs);
            data.Add(Hsa.Frequency);
            data.Add(Hsa.Period);
            */
            /*
            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform1();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform2();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform3();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform4();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform5();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования
            Hsa.HilbertTransform = new HsaClassLibrary.Transform.HilbertTransform6();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);

            //заменяем коэффичиента преобразования            
            HsaClassLibrary.Transform.HilbertTransformFir.HTFIR7();
            Hsa.HS(true, false, false, false);
            //сохраняем для вывода
            data.Add(Hsa.xi);
            */

            System.Console.WriteLine("HilbertTransform выполнена.");
            //System.Console.WriteLine("Console.ReadKey();");
            //Console.ReadKey();
            HsaClassLibrary.Helpers.ReadWriteHelper emdWriter = new HsaClassLibrary.Helpers.ReadWriteHelper();
            emdWriter.WriteCSV(data, "D:\\hsa6.csv");
            System.Console.WriteLine("Файл csv создан.\nD:\\hsa6.csv");
            System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            return 0;
        }
    }
}
