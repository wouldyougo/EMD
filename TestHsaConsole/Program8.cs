using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace TestConsoleApplication
{
    class Program8
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
            IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCos(256, 1, 2.0 * Math.PI / 32.0);
            //IList<double> R = HsaClassLibrary.Helpers.MathHelper.getCosAM(256, 1, 2.0 * Math.PI / 32.0, 2.0 * Math.PI / 256.0);
            //Данные для вывода в файл
            IList<IList<double>> data = new List<IList<double>>();
            data.Add(R);            
            Hsa.Source = R;

            //1 ft
            double[] FT1r = new double[R.Count];
            double[] FT1i = new double[R.Count];

            AForge.Math.Complex[] FT1f = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                FT1f[i].Re = R[i];
                FT1f[i].Im = 0;
            }
            AForge.Math.FourierTransform.DFT(FT1f, AForge.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < R.Count; i++)
            {
                FT1r[i] = FT1f[i].Re;
                FT1i[i] = FT1f[i].Im;
            }
            data.Add(FT1r);
            data.Add(FT1i);
            //2 ft
            double[] FT2r = new double[R.Count];
            double[] FT2i = new double[R.Count];

            AForge.Math.Complex[] FT2f = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                FT2f[i].Re = R[i];
                FT2f[i].Im = 0;
            }
            AForge.Math.FourierTransform.DFT(FT2f, AForge.Math.FourierTransform.Direction.Backward);
            for (int i = 0; i < R.Count; i++)
            {
                FT2r[i] = FT2f[i].Re;
                FT2i[i] = FT2f[i].Im;
            }
            data.Add(FT2r);
            data.Add(FT2i);

            //3 ft
            double[] FT3r = new double[R.Count];
            double[] FT3i = new double[R.Count];

            AForge.Math.Complex[] FT3f = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                FT3f[i].Re = R[i];
                FT3f[i].Im = 0;
            }
            AForge.Math.FourierTransform.FFT(FT3f, AForge.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < R.Count; i++)
            {
                FT3r[i] = FT3f[i].Re;
                FT3i[i] = FT3f[i].Im;
            }
            data.Add(FT1r);
            data.Add(FT1i);

            //4 ft
            double[] FT4r = new double[R.Count];
            double[] FT4i = new double[R.Count];

            AForge.Math.Complex[] FT4f = new AForge.Math.Complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                FT4f[i].Re = R[i];
                FT4f[i].Im = 0;
            }
            AForge.Math.FourierTransform.FFT(FT4f, AForge.Math.FourierTransform.Direction.Backward);
            for (int i = 0; i < R.Count; i++)
            {
                FT4r[i] = FT4f[i].Re;
                FT4i[i] = FT4f[i].Im;
            }
            data.Add(FT4r);
            data.Add(FT4i);

            //5
            double[] FT5r = new double[R.Count];
            double[] FT5i = new double[R.Count];

            double[] FT5 = new double[R.Count];
            R.CopyTo(FT5, 0);

            alglib.complex[] FT5f;
            alglib.fftr1d(FT5, out FT5f);
            for (int i = 0; i < R.Count; i++)
            {
                FT5r[i] = FT5f[i].x;
                FT5i[i] = FT5f[i].y;
            }
            data.Add(FT5r);
            data.Add(FT5i);

            //6
            double[] FT6f = new double[R.Count];
            
            alglib.complex[] FT6 = new alglib.complex[R.Count];
            for (int i = 0; i < R.Count; i++)
            {
                FT6[i].x = R[i];
                FT6[i].y = 0;
            }

            alglib.fftr1dinv(FT6, out FT6f);
            data.Add(FT6f);

            //7
            /*
            double[] FT7r = new double[R.Count];
            double[] FT7i = new double[R.Count];

            List<double> FT7 = new List<double>(R);
            List<Complex> FT7f;
            FT7f = HsaClassLibrary.Transform.FourierTransform.ft(FT7);
            for (int i = 0; i < R.Count; i++)
            {
                FT7r[i] = FT7f[i].Real;
                FT7i[i] = FT7f[i].Imaginary;
            }
            data.Add(FT7r);
            data.Add(FT7i);

            data.Add(R);
             */
            //8
            List<double> FT8 = new List<double>(R);
            List<Complex> FT8f;

            FT8f = HsaClassLibrary.Transform.FourierTransform.fft(FT8).ToList();

            IList<double> FT8r = new double[R.Count];
            IList<double> FT8i = new double[R.Count];
            HsaClassLibrary.Transform.TransformHelper.convert(FT8f, out FT8r, out FT8i);           
            data.Add(FT8r);
            data.Add(FT8i);

            //9
            /*
            double[] FT9r = new double[R.Count];
            double[] FT9i = new double[R.Count];

            List<Complex> FT9 = new List<Complex>(R.Count);
            for (int i = 0; i < R.Count; i++)
            {
                FT9.Add(new Complex(R[i],0));
            }
            List<Complex> FT9f;

            FT9f = HsaClassLibrary.Transform.FourierTransform.ifft(FT9);
            for (int i = 0; i < R.Count; i++)
            {
                FT9r[i] = FT9f[i].Real;
                FT9i[i] = FT9f[i].Imaginary;
            }
            data.Add(FT9r);
            data.Add(FT9i);
            */
            //10
            List<Complex> FT10 = new List<Complex>(R.Count);
            for (int i = 0; i < R.Count; i++)
            {
                FT10.Add(new Complex(R[i], 0));
            }

            IList<Complex> FT10f;
            FT10f = HsaClassLibrary.Transform.FourierTransform.ifft(FT10);

            IList<double> FT10r = new double[R.Count];
            IList<double> FT10i = new double[R.Count];
            HsaClassLibrary.Transform.TransformHelper.convert(FT10f, out FT10r, out FT10i);
            data.Add(FT10r);
            data.Add(FT10i);
            
            //AForge.Math.FourierTransform.FFT

            //System.Console.WriteLine("FourierTransform.FFT выполнена.");
            //System.Console.WriteLine("Console.ReadKey();");
            //Console.ReadKey();
            HsaClassLibrary.Helpers.ReadWriteHelper emdWriter = new HsaClassLibrary.Helpers.ReadWriteHelper();
            emdWriter.WriteCSV(data, "D:\\hsa8.csv");
            System.Console.WriteLine("Файл csv создан.\nD:\\hsa8.csv");
            //System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            return 0;
        }
    }
}
