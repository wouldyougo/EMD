using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsaClassLibrary.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadWriteHelper : IReadWrite
    {
        /*
         * http://www.codeproject.com/Questions/453137/Save-multidimensional-array-to-CSV-file
         * 
        double[,] data = new double[10000,650] and each cell is populated with data.
        I would like to visualize a 10000 row x 650 column matrix

        private void Form1_Load(object sender, EventArgs e)
        {
           double[,] data = new double[10000, 650];
           using (StreamWriter outfile = new StreamWriter(@"C:\Temp\test.csv"))
           {
              for (int x = 0; x < 10000; x++)
              {
                 string content = "";
                 for (int y = 0; y < 650; y++)
                 {
                    content += data[x, y].ToString("0.00") + ";";
                 }
                 outfile.WriteLine(content);
              }
           }
        }
         * * */

        /// <summary>
        /// Сохраняет данные [][] в CSV файл
        /// EmdLibrary.ReadWriteHelper emdWriter = new EmdLibrary.ReadWriteHelper();
        /// emdWriter.WriteCSV(emd.R, "D:\\tstcsvfile.csv ");
        /// </summary>
        /// <param name="dataToSave">[i][j] в CSV файл i - столбцы, j - строки</param>
        /// <param name="fileName">"D:\\tstcsvfile.csv "</param>
        public void WriteCSV(IList<IList<double>> dataToSave, string fileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                if (dataToSave != null)
                {
                    for (int j = 0; j < dataToSave[0].Count(); j++)
                    {
                        for (int i = 0; i < dataToSave.Count(); i++)
                        {
                            file.Write(dataToSave[i][j] + ";");
                        }
                        file.Write(Environment.NewLine);
                    }
                }
            }
            //throw new NotImplementedException();
        }
        /*
        public static void SaveArrayAsCSV<T>(T[][] jaggedArrayToSave, string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (T[] array in jaggedArrayToSave)
                {
                    foreach (T item in array)
                    {
                       file.Write(item + ",");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }        
         * */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IList<IList<double>> ReadCSV(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
