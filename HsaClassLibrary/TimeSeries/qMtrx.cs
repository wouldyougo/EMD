using System;

namespace TimeSeries
{
    public static class GlobalMembersMtrx
    {
        /// <summary>
        /// –асчетна€ функци€
        /// </summary>
        /// <param name="i"></param>
        /// <param name="x"></param>
        /// <param name="Nf"></param>
        /// <param name="Nx"></param>
        /// <returns></returns>
        public static double eval_function(int i, double[] x, int Nf, int Nx) 
        {
            return Math.Pow(x[0], 2) + Math.Pow(x[1], 2) + Math.Pow(x[2], 2);
        }

        /// <summary>
        /// вычисление R
        /// хз что это
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Num_rs"></param>
        /// <returns></returns>
        public static TMatrix EvaluateR(TMatrix x, int Num_rs)
        {
            int i;
            double Val;
            double[] X_s;
            TMatrix Ret = new TMatrix(Num_rs, 1);

            if (x.GetRows() != 1)
                //throw E;
                throw new System.ApplicationException("неверные параметры вычислени€ R");

            try
            {
                X_s = new double[x.GetCols()];
                for (i = 0; i < x.GetCols(); i++)
                    X_s[i] = x.GetElm(0, i);
                for (i = 0; i < Ret.GetRows(); i++)
                {
                    Val = GlobalMembersMtrx.eval_function(i, X_s, Num_rs, x.GetCols()); // ¬ычисл€ем i-ю функцию
                    Ret.PutElm(i, 0, Val); // ‘ормируем Ret
                }
            }
            catch
            {
                //throw E;
                throw new System.ApplicationException();
            }
            X_s = null;
            return Ret;
        }
        //---------------------------------------------------------------------------

        /// <summary>
        /// вычисление матрицы якоби
        /// </summary>
        /// <param name="x">параметры вычислени€ матрицы якоби</param>
        /// <param name="Num_rs">параметры вычислени€ матрицы якоби</param>
        /// <param name="h">параметры вычислени€ матрицы якоби</param>
        /// <returns></returns>
        public static TMatrix EvaluateJ(TMatrix x, int Num_rs, double h)
        {
            TMatrix Val_1 = new TMatrix();
            TMatrix Val_2 = new TMatrix();
            TMatrix Val = new TMatrix();

            if (x.GetRows() != 1)
                //throw E;
                throw new System.ApplicationException("неверные параметры вычислени€ матрицы якоби");
            TMatrix Ret = new TMatrix(Num_rs, x.GetCols());
            TMatrix X_s = new TMatrix(1, x.GetCols());

            try
            {
                for (int i = 0; i < Ret.GetCols(); i++)
                {
                    for (int j = 0; j < X_s.GetCols(); j++)
                        X_s.PutElm(0, j, x.GetElm(0, j));
                    X_s.PutElm(0, i, x.GetElm(0, i) - h);
                    Val_1 = GlobalMembersMtrx.EvaluateR(X_s, Num_rs);
                    for (int j = 0; j < X_s.GetCols(); j++)
                        X_s.PutElm(0, j, x.GetElm(0, j));
                    X_s.PutElm(0, i, x.GetElm(0, i) + h);
                    Val_2 = GlobalMembersMtrx.EvaluateR(X_s, Num_rs);
                    Val = (1 / (2 * h)) * (Val_2 - Val_1);
                    for (int j = 0; j < Ret.GetRows(); j++)
                        Ret.PutElm(j, i, (double)Val.GetElm(j, 0));
                }
            }
            catch
            {
                //throw E;
                throw new System.ApplicationException();
            }
            return Ret;
        }
    }
    //---------------------------------------------------------------------------
    public class TMatrix
    {
        private double[][] Data;
        private int Rows;
        private int Cols;

        public TMatrix() // Constructors
        {
            Rows = Cols = 0;
            Data = null;
        }
        //---------------------------------------------------------------------------
        public TMatrix(int i, int j)
        {
            Rows = i;
            Cols = j;
            Data = new double[Rows][];
            for (i = 0; i < Rows; i++)
                Data[i] = new double[Cols];
        }
        //---------------------------------------------------------------------------
        public TMatrix(ref TMatrix M)
        {
            Cols = M.GetCols();
            Rows = M.GetRows();
            Data = new double[Rows][];
            for (int i = 0; i < Rows; i++)
                Data[i] = new double[Cols];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    Data[i][j] = M.GetElm(i, j);
        }
        //---------------------------------------------------------------------------
        /*
          public TMatrix(ref string FileName, int Pos)
        {
         double Elm;
         FILE file;
         if((file = fopen(FileName,"r")) == null)
         {
          Cols = Rows = 0;
          Data = null;
         }
  
         fseek(file,Pos,SEEK_SET);
         fscanf(file, "%d%d", Rows, Cols);
         Data = new double[Rows];
         for(int i = 0;i < Rows;i++)
             Data[i] = new double[Cols];
  
         for(int i = 0;i < Rows;i++)
          for(int j = 0;j < Cols;j++)
          {
           fscanf(file, "%lf", Elm);
           Data[i][j] = Elm;
          }
         fclose(file);
        }
         */
        //---------------------------------------------------------------------------
        public virtual void Dispose()
        {
            if (Data != null)
            {
                for (int i = 0; i < Rows; i++)
                    Data[i] = null;
                Data = null;
                Data = null;
            }
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double GetElm(int i, int j) const
        public double GetElm(int i, int j)
        {
            if (Data != null && i < Rows && j < Cols)
                return Data[i][j];
            else
                //throw E;
                throw new System.ApplicationException("недопустимые индексы доступа к элементам матрицы");
        }
        //---------------------------------------------------------------------------
        public void PutElm(int i, int j, double Elm)
        {
            if (Data != null && i < Rows && j < Cols)
                Data[i][j] = Elm;
            else
                //throw E;
                throw new System.ApplicationException("недопустимые индексы доступа к элементам матрицы");
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int GetRows() const
        public int GetRows()
        {
            return Rows;
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int GetCols() const
        public int GetCols()
        {
            return Cols;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double Trace() const
        public double Trace()
        {
            double Ret = 0;
            if (Rows == Cols)
                for (int i = 0; i < Rows; i++)
                    Ret += Data[i][i];
            else
                //throw E;
                throw new System.ApplicationException("ошибка в функции trace");
            return Ret;
        }
        //---------------------------------------------------------------------------
        public void FillMatrix(double Fill)
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    Data[i][j] = Fill;
        }
        //---------------------------------------------------------------------------
        public void Make_I()
        {
            if (Cols == Rows)
            {
                FillMatrix(0.0);
                for (int i = 0; i < Rows; i++)
                    Data[i][i] = 1.0;
            }
            else
                //throw E;
                throw new System.ApplicationException("не могу сделать единичной неквадратную матрицу");
        }
        //---------------------------------------------------------------------------
        public double Norma()
        {
            double Value = 0;
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    Value += GetElm(i, j) * GetElm(i, j);
            // Value *= 0.5;
            Value = Math.Pow(Value, 0.5);
            return Value;
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original C++ copy assignment operator:
        //ORIGINAL LINE: TMatrix& operator = (const TMatrix& M)
        public TMatrix CopyFrom(TMatrix M)
        {
            if (Data == null)
            {
                Rows = M.GetRows();
                Cols = M.GetCols();
                Data = new double[Rows][];
                for (int i = 0; i < Rows; i++)
                    Data[i] = new double[Cols];
            }
            else if (M.GetRows() != Rows || M.GetCols() != Cols)
            {
                for (int i = 0; i < Rows; i++)
                    Data[i] = null;
                Data = null;
                Rows = M.GetRows();
                Cols = M.GetCols();
                Data = new double[Rows][];
                for (int i = 0; i < Rows; i++)
                    Data[i] = new double[Cols];
            }

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    PutElm(i, j, M.GetElm(i, j));
            return this;
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        public static TMatrix operator +(TMatrix A, TMatrix B)
        {
            TMatrix M = new TMatrix();
            if (A.GetRows() == B.GetRows() && A.GetCols() == B.GetCols())
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: M = A;
                M.CopyFrom(A);
                for (int i = 0; i < M.GetRows(); i++)
                    for (int j = 0; j < M.GetCols(); j++)
                        M.PutElm(i, j, A.GetElm(i, j) + B.GetElm(i, j));
            }
            else
                //throw E;
                throw new System.ApplicationException("попытка сложени€ матриц разного размера");
            return M;
        }
        //---------------------------------------------------------------------------
        public static TMatrix operator -(TMatrix A, TMatrix B)
        {
            TMatrix M = new TMatrix();
            if (A.GetRows() == B.GetRows() && A.GetCols() == B.GetCols())
            {
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: M = A;
                M.CopyFrom(A);
                for (int i = 0; i < M.GetRows(); i++)
                    for (int j = 0; j < M.GetCols(); j++)
                        M.PutElm(i, j, A.GetElm(i, j) - B.GetElm(i, j));
            }
            else
                //throw E;
                throw new System.ApplicationException("попытка вычитани€ матриц разного размера");
            return M;
        }
        //---------------------------------------------------------------------------
        public static TMatrix operator *(TMatrix A, TMatrix B)
        {
            double sum;
            TMatrix M = new TMatrix(A.GetRows(), B.GetCols());

            if (A.GetCols() == B.GetRows())
            {
                for (int i = 0; i < M.GetRows(); i++)
                    for (int j = 0; j < M.GetCols(); j++)
                    {
                        sum = 0.0;
                        for (int k = 0; k < A.GetCols(); k++)
                            sum += A.GetElm(i, k) * B.GetElm(k, j);
                        M.PutElm(i, j, sum);
                    }
            }
            else
                //throw E;
                throw new System.ApplicationException("неверные размеры умножаемых матриц");
            return M;
        }
        //---------------------------------------------------------------------------
        public static TMatrix operator *(double m, TMatrix A)
        {
            TMatrix M = new TMatrix(A.GetRows(), A.GetCols());
            for (int i = 0; i < M.GetRows(); i++)
                for (int j = 0; j < M.GetCols(); j++)
                    M.PutElm(i, j, m * A.GetElm(i, j));
            return M;
        }

        public static TMatrix operator !(TMatrix A)
        {
            TMatrix M = new TMatrix(A.GetCols(), A.GetRows());
            for (int i = 0; i < M.GetRows(); i++)
                for (int j = 0; j < M.GetCols(); j++)
                    M.PutElm(i, j, A.GetElm(j, i));
            return M;
        }

        public static TMatrix operator ~(TMatrix A)
        {
            int k_step;
            bool Stop = false;
            double phi_k;
            double phi_k1;

            TMatrix A_plus = new TMatrix();
            TMatrix F_k = new TMatrix();
            TMatrix F_k1 = new TMatrix();
            TMatrix I = new TMatrix();
            TMatrix AtA = new TMatrix();

            TMatrix M = new TMatrix(A.GetCols(), A.GetRows());

            AtA = !M * M;
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: F_k = AtA;
            F_k.CopyFrom(AtA);
            F_k.Make_I();
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: I = AtA;
            I.CopyFrom(AtA);
            I.Make_I();

            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: F_k = I;
            F_k.CopyFrom(I);
            phi_k = AtA.Trace();
            k_step = 2;

            for (; ; )
            {
                F_k1 = (phi_k * I) - (AtA * F_k);
                phi_k1 = (AtA * F_k1).Trace() / k_step;

                if (Math.Abs(phi_k1) == 0)
                    Stop = true;
                if (k_step > M.GetRows())
                    Stop = true;
                if (k_step > M.GetCols())
                    Stop = true;
                if (Stop == true)
                    break;

                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: F_k = F_k1;
                F_k.CopyFrom(F_k1);
                phi_k = phi_k1;
                k_step++;
            }

            TMatrix No_rang = new TMatrix(1, 1);
            No_rang.PutElm(0, 0, (double)10E+200);
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: A_plus = No_rang;
            A_plus.CopyFrom(No_rang);
            if (phi_k == 0)
                return A_plus;
            A_plus = (1 / phi_k) * F_k * (!M);
            return A_plus;
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: TMatrix operator ~ () const
        /*
          public static TMatrix operator ~ ()
        {
         int k_step;
         bool Stop = false;
         double phi_k;
         double phi_k1;
  
         TMatrix A_plus = new TMatrix();
         TMatrix F_k = new TMatrix();
         TMatrix F_k1 = new TMatrix();
         TMatrix I = new TMatrix();
         TMatrix AtA = new TMatrix();
  
         AtA = !(this) * (this);
      //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
      //ORIGINAL LINE: F_k = AtA;
         F_k.CopyFrom(AtA);
         F_k.Make_I();
      //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
      //ORIGINAL LINE: I = AtA;
         I.CopyFrom(AtA);
         I.Make_I();
  
      //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
      //ORIGINAL LINE: F_k = I;
         F_k.CopyFrom(I);
         phi_k = AtA.Trace();
         k_step = 2;
  
         for(;;)
         {
          F_k1 = (phi_k * I) - (AtA * F_k);
          phi_k1 = (AtA * F_k1).Trace() / k_step;
  
          if(Math.Abs(phi_k1) == 0)
              Stop = true;
          if(k_step > this.GetRows())
              Stop = true;
          if(k_step > this.GetCols())
              Stop = true;
          if(Stop == true)
              break;
  
      //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
      //ORIGINAL LINE: F_k = F_k1;
          F_k.CopyFrom(F_k1);
          phi_k = phi_k1;
          k_step++;
         }
  
         TMatrix No_rang = new TMatrix(1, 1);
         No_rang.PutElm(0, 0, (double)10E+200);
      //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
      //ORIGINAL LINE: A_plus = No_rang;
         A_plus.CopyFrom(No_rang);
         if(phi_k == 0)
             return A_plus;
         A_plus = (1/phi_k) * F_k * !(this);
         return A_plus;
        }
          */
    }
}