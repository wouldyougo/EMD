using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace TS.DataSource
{
    public class BarList
    {
        private List<BaseBar> bar_list;

        public List<BaseBar> Bars
        {
            get
            {
                return this.bar_list;
            }
            set
            {
                this.bar_list = value;
            }
        }

        public BarList()
        {
            bar_list = new List<BaseBar>();
        }

        public BarList(string path)
        {
            bar_list = new List<BaseBar>();
            if (path == "")
            {
                path = "z:\\YandexDisk\\Data\\GAZP_test_1h.txt";
            }
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(path);

            string line;
            //Read the first line of text
            line = sr.ReadLine();
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the lie to console window
                //Console.WriteLine(line);
                TS.DataSource.BaseBar bb;
                bb = new TS.DataSource.BaseBar(line);
                bar_list.Add(bb);
                //Read the next line
                line = sr.ReadLine();
            }

            //close the file
            sr.Close();
        }
        public void ToTextFile(string path, bool append)
        {
            if (path == "")
            {
                path = "C:\\Dropbox\\TSLab\\Data\\test_out.txt";
            }
            StreamWriter sw = new StreamWriter(path, append);
            sw.WriteLine("<TICKER>,<PER>,<DATE>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>");
            foreach (TS.DataSource.BaseBar bb in this.Bars)
            {
                sw.WriteLine("{0}", bb.ToString());
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Возвращяет double[n] Close
        /// </summary>
        public IList<double> Close
        {
            get
            {
                int n = bar_list.Count;
                IList<double> data = new double[n];
                for (int i = 0; i < n; i++)
                {
                    data[i] = this.bar_list[i].Close;
                }
                return data;
            }
        }

        /// <summary>
        /// Возвращяет double[n] Volume
        /// </summary>
        public IList<double> Volume
        {
            get
            {
                int n = bar_list.Count;
                IList<double> data = new double[n];
                for (int i = 0; i < n; i++)
                {
                    data[i] = this.bar_list[i].Volume;
                }
                return data;
            }
        }

    }


    //[Serializable]
    public class BaseBar : ICloneable
    {

        private string m_ticker;
        private string m_period;
        private DateTime m_date;

        private double m_open;
        private double m_high;
        private double m_low;
        private double m_close;
        private double m_volume;

        public string Ticker
        {
            get
            {
                return this.m_ticker;
            }
            set
            {
                this.m_ticker = value;
            }
        }

        public string Period
        {
            get
            {
                return this.m_period;
            }
            set
            {
                this.m_period = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.m_date;
            }
            set
            {
                this.m_date = (DateTime)value;
            }
        }

        public double Open
        {
            get
            {
                return this.m_open;
            }
            protected set
            {
                this.m_open = (double)value;
            }
        }
        public double High
        {
            get
            {
                return this.m_high;
            }
            protected set
            {
                this.m_high = (double)value;
            }
        }
        public double Low
        {
            get
            {
                return this.m_low;
            }
            protected set
            {
                this.m_low = (double)value;
            }
        }
        public double Close
        {
            get
            {
                return this.m_close;
            }
            protected set
            {
                this.m_close = (double)value;
            }
        }

        public double Volume
        {
            get
            {
                return this.m_volume;
            }
            protected set
            {
                this.m_volume = (double)value;
            }
        }
        /// <summary>
        /// Создает бар из строки
        /// </summary>
        /// <param name="bar"><TICKER>,<PER>,<DATE>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL></param>
        public BaseBar(string bar)
        {
            string[] split = bar.Split(new Char[] { '/', ',', ';', '\t' });
            string dateValue = null;

            if (split.Count() >= 8)
            {
                //<TICKER>
                this.m_ticker = split[0].Trim();
                //<PER>
                this.m_period = split[1].Trim();
                //<DATE>
                dateValue = split[2].Trim();
                //<TIME>
                dateValue = dateValue + split[3].Trim();
                //<OPEN>
                this.m_open = double.Parse(split[4].Replace(".", ",").Trim());
                //<HIGH>
                this.m_high = double.Parse(split[5].Replace(".", ",").Trim());
                //<LOW>
                this.m_low = double.Parse(split[6].Replace(".", ",").Trim());
                //<CLOSE>
                this.m_close = double.Parse(split[7].Replace(".", ",").Trim());

                this.m_volume = 0;
            }
            //<VOL>
            if (split.Count() == 9)
            {
                this.m_volume = double.Parse(split[8].Replace(".", ",").Trim());
            }

            string pattern = "yyyyMMddHHmmss";
            DateTime parsedDate;
            DateTime.TryParseExact(dateValue, pattern, null,
                            DateTimeStyles.None, out parsedDate);
            this.m_date = parsedDate;
        }


        public BaseBar(DateTime date, double open, double high, double low, double close, double volume)
        {
            this.m_date = date;
            this.m_open = open;
            this.m_high = high;
            this.m_low = low;
            this.m_close = close;
            this.m_volume = volume;
        }
        protected BaseBar(BaseBar bar)
        {
            this.m_date = bar.m_date;
            this.m_open = bar.m_open;
            this.m_high = bar.m_high;
            this.m_low = bar.m_low;
            this.m_close = bar.m_close;
            this.m_volume = bar.m_volume;
        }
        public virtual void Add(BaseBar b2)
        {
            this.m_high = Math.Max(this.m_high, b2.m_high);
            this.m_low = Math.Min(this.m_low, b2.m_low);
            this.m_close = b2.m_close;
        }
        public object Clone()
        {
            return new BaseBar(this);
        }
        public override string ToString()
        {
            return string.Concat(new object[]
			{
				this.Ticker, 
				",", 
				this.Period, 
				",", 
				this.m_date.ToString("yyyyMMdd,HHmmss"), 
				",", 
				this.Open, 
				",", 
				this.Low, 
				",", 
				this.High, 
				",", 
				this.Close,
				",", 
				this.Volume
			});
        }
    }
}
