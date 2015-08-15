using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program4
    {
        public static int Main(string[] args)
        {
            IList<double> R = HsaClassLibrary.Helpers.MathHelper.getRandom(500, 100);
            HsaClassLibrary.Helpers.HhtCreator emdcr = new HsaClassLibrary.Helpers.HhtCreator();
            //EmdClassLibrary.EmdAbstractClass emd = emdcr.FactoryMethod(EmdClassLibrary.InterpolationEnum.ЛинейнаяИнтерполяция, EmdClassLibrary.StopCriterionEnum.КоличествоИтераций, EmdClassLibrary.StopSiftCriterionEnum.КоличествоИтераций);
            //EmdClassLibrary.EmdAbstractClass emd = emdcr.FactoryMethod(EmdClassLibrary.InterpolationEnum.ЛинейнаяИнEnumInterpolationassLibrary.StopCriterionEnum.КоличествоИтераций, EmdCEnumStopCriterioniftCriterionEnum.ДостигнутаТочностьОтсеивания);
            HsaClassLibrary.Decomposition.EmDecomposition emd = emdcr.EmdFactoryMethod(HsaClassLibrary.Decomposition.EnumInterpolation.ЛинейнаяИнтерполяция, HsaClassLibrary.Decomposition.EnumStopCondition.КоличествоИтерацийИлиЭкстремумов, HsaClassLibrary.Decomposition.EnumStopConditionSeparate.КоличествоИтерацийИлиДостигнутаТочность);
            emd.DataLoad(R);
            emd.Decomposition();
            System.Console.WriteLine("Декомпозиция выполнена.\nRead C:\\tstlogfile.txt ");
            System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            HsaClassLibrary.Helpers.ReadWriteHelper emdWriter = new HsaClassLibrary.Helpers.ReadWriteHelper();
            emdWriter.WriteCSV(emd.R, "D:\\Emd4.csv ");
            System.Console.WriteLine("Файл csv создан.\nD:\\4.csv ");
            System.Console.WriteLine("Console.ReadKey();");
            Console.ReadKey();
            return 0;
        }
    }
}
