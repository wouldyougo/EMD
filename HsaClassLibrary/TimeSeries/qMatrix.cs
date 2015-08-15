using System.Collections.Generic;
using System;

namespace TimeSeries
{
    //#define M_PI
    public partial class qMData
    {
        public const double M_PI = 3.14159265358979323846;

        public qMData()
        {
            ;
        }
        //---------------------------------------------------------------------------
        public qMData(List<List<double>> aMtrx)
        {
            this.Mtrx = aMtrx;
        }
        //---------------------------------------------------------------------------
        public qMData(qMData aMData)
        {
            this.Mtrx = aMData.get();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: ClassicVector<ClassicVector<double> > get()const
        public List<List<double>> get()
        {
            return Mtrx;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getRow() const
        public int getRow()
        {
            return Mtrx.Count;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int getCol() const
        public int getCol()
        {
            if (Mtrx.Count != 0)
            {
                return Mtrx[0].Count;
            }
            else
            {
                return 0;
            }
        }
        //---------------------------------------------------------------------------

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qMData operator + (qMData aMData) const
        public static qMData operator +(qMData ImpliedObject, qMData aMData)
        {
            qMData tMData = new qMData();
            tMData = ImpliedObject;
            tMData.mkAdd(aMData.get());
            return tMData;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qMData operator - (qMData aMData) const
        public static qMData operator -(qMData ImpliedObject, qMData aMData)
        {
            qMData tMData = new qMData();
            tMData = ImpliedObject;
            tMData.mkSub(aMData.get());
            return tMData;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qMData operator * (qMData aMData) const
        public static qMData operator *(qMData ImpliedObject, qMData aMData)
        {
            qMData tMData = new qMData();
            tMData = ImpliedObject;
            tMData.mkMul(aMData.get());
            return tMData;
        }
        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: qMData operator * (double aD) const
        public static qMData operator *(qMData ImpliedObject, double aD)
        {
            List<List<double>> tMtrx = new List<List<double>>();
            tMtrx = ImpliedObject.get();
            int row = 0;
            int col = 0;
            row = tMtrx.Count;
            if (row != 0)
            {
                col = tMtrx[0].Count;
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tMtrx[i][j] = tMtrx[i][j] * aD;
                }
            }
            qMData tMData = new qMData();
            tMData.set(tMtrx);
            return tMData;
        }
        //---------------------------------------------------------------------------
        /*
       public static qMData operator += (qMData aMData)
       {
           this.mkAdd(aMData.get());
           return *(this);
       }
       //---------------------------------------------------------------------------
       public static qMData operator -= (qMData aMData)
       {
           this.mkSub(aMData.get());
           return *(this);
       }
       //---------------------------------------------------------------------------
       public static qMData operator *= (qMData aMData)
       {
           this.mkMul(aMData.get());
           return *(this);
       }
        */


        //---------------------------------------------------------------------------

        public void mkAdd(List<List<double>> aMtrx)
        {
            List<double> tVctr1 = new List<double>();
            tVctr1 = clcVctr(this.Mtrx);
            List<double> tVctr2 = new List<double>();
            tVctr2 = clcVctr(aMtrx);
            List<double> tVctr3 = new List<double>();
            List<double> tVctr4 = new List<double>();
            tVctr3.AddRange(tVctr1);
            tVctr4.AddRange(tVctr2);
            this.MatrixPlusMatrix(tVctr3, tVctr4, tVctr3.Count);
            tVctr1.AddRange(tVctr3);
            this.Mtrx = clcMtrx(tVctr1, this.Mtrx.Count);
        }
        //---------------------------------------------------------------------------
        public void mkSub(List<List<double>> aMtrx)
        {
            List<double> tVctr1 = new List<double>();
            tVctr1 = clcVctr(this.Mtrx);
            List<double> tVctr2 = new List<double>();
            tVctr2 = clcVctr(aMtrx);
            List<double> tVctr3 = new List<double>();
            List<double> tVctr4 = new List<double>();
            tVctr3.AddRange(tVctr1);
            tVctr4.AddRange(tVctr2);
            this.MatrixMinusMatrix(tVctr3, tVctr4, tVctr3.Count);
            tVctr1.AddRange(tVctr3);
            this.Mtrx = clcMtrx(tVctr1, this.Mtrx.Count);
        }
        //---------------------------------------------------------------------------
        public void mkMul(List<List<double>> aMtrx)
        {
            int n = 0;
            int m = 0;
            int k = 0;
            n = this.Mtrx.Count;
            m = this.Mtrx[0].Count;
            if (m != aMtrx.Count)
            {
                throw new System.ApplicationException("������������ �������� qMData::mkMul");
            }
            k = aMtrx[0].Count;
            List<double> tVctr1 = new List<double>();
            List<double> tVctr2 = new List<double>();
            tVctr1 = clcVctr(this.Mtrx);
            tVctr2 = clcVctr(aMtrx);
            List<double> tVctr3 = new List<double>();
            List<double> tVctr4 = new List<double>();
            tVctr3.AddRange(tVctr1);
            tVctr4.AddRange(tVctr2);
            List<double> tVctr5 = new List<double>(n * k);
            this.MatrixMultiplyMatrix(tVctr3, tVctr4, tVctr5, n, m, k);
            tVctr1.AddRange(tVctr5);
            this.Mtrx = clcMtrx(tVctr1, this.Mtrx.Count);
        }
        //---------------------------------------------------------------------------
        public double Det(List<List<double>> aMtrx)
        {
            if (aMtrx.Count > 0)
                if (aMtrx.Count == aMtrx[0].Count)
                {
                    List<double> tVctr1 = new List<double>();
                    tVctr1 = clcVctr(aMtrx);
                    List<double> tVctr2 = new List<double>();
                    tVctr2.AddRange(tVctr1);
                    return Det(tVctr2, aMtrx.Count);
                }
                else
                {
                    throw new System.ApplicationException("������� �� ���������� qMData::Det");
                }
            return 0;
        }
        //---------------------------------------------------------------------------
        public List<List<double>> clcTrnsp(List<List<double>> aMtrx)
        {
            List<double> tVctr1 = new List<double>();
            tVctr1 = clcVctr(aMtrx);
            List<double> tVctr3 = new List<double>();
            List<double> tVctr4 = new List<double>();
            tVctr3.AddRange(tVctr1);
            tVctr4.AddRange(tVctr1);
            MatrixT(tVctr3, tVctr4, aMtrx.Count, aMtrx[0].Count);
            tVctr1.AddRange(tVctr4);
            return clcMtrx(tVctr1, aMtrx[0].Count);
        }
        //---------------------------------------------------------------------------
        public List<double> clcVctr(List<List<double>> aMtrx)
        {
            List<double> tVctr = new List<double>(0);
            for (int i = 0; i < aMtrx.Count; i++)
            {
                //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
                tVctr.AddRange(aMtrx[i]);
            }
            return tVctr;
        }
        //---------------------------------------------------------------------------
        /*
        std::vector<std::vector<float> > qMData::clcMtrx(std::vector<float> aVctr, unsigned n){
            std::vector<std::vector<float> > tMtrx(n,std::vector<float> (0,0));
            unsigned m = aVctr.size()/n;
            for(unsigned i = 0; i < n; i++){
                  tMtrx[i].insert( tMtrx[i].end(), aVctr.begin()+i*m, aVctr.begin()+(i+1)*m );
            }
            return tMtrx;
        }    
        */

        public List<List<double>> clcMtrx(List<double> aVctr, int n)
        {
            //List<List<double> > tMtrx = new List<List<double> >(n,List<double> (0,0));
            List<List<double>> tMtrx = new List<List<double>>(n);

            int m = aVctr.Count / n;
            for (int i = 0; i < n; i++)
            {
                //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
                //tMtrx[i].insert(tMtrx[i].end(), aVctr.GetEnumerator()+i *m, aVctr.GetEnumerator()+(i+1)*m);
                List<double> iRange = aVctr.GetRange(i * m, m);
                tMtrx[i] = new List<double>(iRange);
                //���� ���������
                //throw new System.NotImplementedException();
            }
            return tMtrx;
        }

        //---------------------------------------------------------------------------
        public List<List<double>> clcInverseMatrix(List<List<double>> aMtrx)
        {
            if (aMtrx.Count > 0)
                if (aMtrx.Count == aMtrx[0].Count)
                {
                    List<double> tVctr1 = new List<double>();
                    tVctr1 = clcVctr(aMtrx);
                    List<double> tVctr3 = new List<double>();
                    List<double> tVctr4 = new List<double>();
                    tVctr3.AddRange(tVctr1);
                    tVctr4.AddRange(tVctr1);
                    InverseMatrix(tVctr3, tVctr4, aMtrx.Count);
                    tVctr1.AddRange(tVctr4);
                    return clcMtrx(tVctr1, aMtrx.Count);
                }
                else
                {
                    throw new System.ApplicationException("������� �� ���������� qMData::InverseMatrix");
                    //return List<List<double> > (0,List<double> (0,0));
                }
            //return List<List<double> > (0,List<double> (0,0));
            return null;
        }
        //---------------------------------------------------------------------------
        public List<List<double>> clcPseudoInverseMatrix(List<List<double>> aMtrx)
        {
            if (aMtrx.Count > 0)
                if (aMtrx[0].Count > 0)
                {
                    List<double> tVctr1 = new List<double>();
                    tVctr1 = clcVctr(aMtrx);
                    List<double> tVctr3 = new List<double>();
                    List<double> tVctr4 = new List<double>();
                    tVctr3.AddRange(tVctr1);
                    tVctr4.AddRange(tVctr1);
                    PseudoInverseMatrix(tVctr3, tVctr4, aMtrx.Count, aMtrx.Count); //+++
                    tVctr1.AddRange(tVctr4);
                    //throw new System.NotImplementedException();
                    return clcMtrx(tVctr1, aMtrx[0].Count);
                }
                else
                {
                    //ShowMessage("������� ������ qMData::PsInverseMatrix");
                    //return List<List<double> > (0,List<double> (0,0));
                    throw new System.ApplicationException("������� ������ qMData::PsInverseMatrix");
                }
            //return List<List<double> > (0,List<double> (0,0));
            return null;
        }
        //---------------------------------------------------------------------------

        public List<List<double>> clcPIMatrix(List<List<double>> aMtrx)
        {
            int Row = 0;
            int Col = 0;
            Row = aMtrx.Count;
            if (Row != 0)
            {
                Col = aMtrx[0].Count;
            }
            TMatrix A = new TMatrix(Row, Col);
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    //         double tmp = 0;
                    //         tmp = aMtrx[i][j];
                    //         A.PutElm(i,j, tmp );
                    A.PutElm(i, j, aMtrx[i][j]);
                }
            }
            A = ~A;
            //List<List<double> > tVctr = new List<List<double> >(Col, new List<double>(Row));
            List<List<double>> tVctr = new List<List<double>>(Col);
            for (int i = 0; i < Col; i++)
                tVctr[i] = new List<double>(Row);


            for (int j = 0; j < Col; j++)
            {
                for (int i = 0; i < Row; i++)
                {
                    //         double tmp = 0;
                    //         tmp = A.GetElm(j,i);
                    //         tVctr[j][i] = tmp;
                    tVctr[j][i] = A.GetElm(j, i);
                }
            }
            return tVctr;
        }
        public List<List<double>> clcPIMatrix2(List<List<double>> aMtrx)
        {
            qMData A = new qMData();
            qMData B = new qMData();
            qMData F = new qMData();
            qMData C = new qMData();
            qMData I = new qMData();
            A.set(aMtrx);
            A.set(clcMtrx(clcVctr(A.get()), A.getCol()));

            int i;
            int n = A.getRow();
            int m = A.getCol();
            I.set(clcI(m, m));
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: F = I;
            F = I;
            C.set(clcTrnsp(A.get()));
            C = C * A;
            double f = clcTrace(C.get());
            for (i = 0; i < m && i < n; i++)
            {
                qMData F1 = new qMData();
                qMData D1 = new qMData();
                F1 = I * f;
                D1 = C * F;
                F1 = F1 - D1;
                D1 = C * F1;
                double f1 = clcTrace(D1.get());
                f1 = f1 / (i + 2);
                if (f1 < 0.000000001 && f1 > -0.000000001)
                    break;
                f = f1;
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: F = F1;
                F = F1;
            }
            qMData D = new qMData();
            D.set(clcTrnsp(A.get()));
            B = F * D;
            B = B * (1.0 / f);
            List<List<double>> tMtrx = new List<List<double>>(); //--
            B.set(clcMtrx(clcVctr(B.get()), B.getCol()));
            tMtrx = B.get(); //--
            int mm = tMtrx.Count; //--
            int nn = tMtrx[0].Count; //--
            return B.get();
        }
        //---------------------------------------------------------------------------
        public List<List<double>> clcI(int m, int n)
        {
            //List<List<double> > tMtrx = new List<List<double> >(m, List<double> (n,0));
            List<List<double>> tMtrx = new List<List<double>>(m);
            for (int i = 0; i < m; i++)
                tMtrx[i] = new List<double>(n);


            for (int i = 0; i < m && i < n; i++)
                tMtrx[i][i] = 1.0;
            return tMtrx;
        }
        //---------------------------------------------------------------------------
        public double clcTrace(List<List<double>> aMtrx)
        {
            double tr = 0.0;
            int i;
            int n = aMtrx.Count;
            int m = aMtrx[0].Count;
            for (i = 0; i < n && i < m; i++)
                tr += aMtrx[i][i];
            return tr;
        }


        //---------------------------------------------------------------------------
        public void MatrixPlusMatrix(List<double> a, List<double> b, int n)
        // ������� ��������� ����������� ��� �������.
        // �����������:
        //  a � ������ ���������,
        //  b � ������ ���������,
        //  n � ������ ������� �������.
        // ����������.
        //  ��������� ����������� � ������ �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i;
            for (i = 0; i < n; i++)
                a[i] += b[i];
        }
        //---------------------------------------------------------------------------
        public void MatrixMinusMatrix(List<double> a, List<double> b, int n)
        // ������� �������� ����������� �� ������ ������� ������.
        // �����������:
        //  a � �����������,
        //  b � ����������,
        //  n � ������ ������� �������.
        // ����������.
        //  ��������� ����������� � ������ �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i;
            for (i = 0; i < n; i++)
                a[i] -= b[i];
        }
        //---------------------------------------------------------------------------
        public void MatrixMultiplyMatrix(List<double> a, List<double> b, List<double> r, int n, int m, int l)
        // ������� ������������ ������.
        // �����������:
        //  a � ������ �����������, n x m,
        //  b � ������ �����������, m x l,
        //  r � ������������, n x l,
        //  n � ����� ����� � ������� a,
        //  m � ����� �������� � ������� a � ����� � ������� b,
        //  l � ����� �������� � ������� b.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i, j, k;
            int i; // ������� ����� �� ������� b -  ������� ����� �� �������� a -  ������� ����� �� �������� b
            int j;
            int k;
            int ia; // ������ ����� ������ ��������� ������� b -  ������� ��������� ������� r -  ������� ��������� ������� b -  ������� ��������� ������� a
            int ib;
            int ir = -1;
            int ik = -m - 1;
            //////////////////////////////
            // ���� �� �������� ������� b
            for (k = 0; k < l; k++)
                ////////////////////////////
                // ���� �� ������� ������� a
                for (j = 0, ik += m; j < n; j++)
                    /////////////////////////////
                    // ���� �� �������� ������� a
                    for (i = 0, ia = j, ib = ik, r[++ir] = 0; i < m; i++, ia += n)
                        r[ir] += a[ia] * b[++ib];
        }
        //---------------------------------------------------------------------------
        public void MatrixT(List<double> x, List<double> y, int n, int k)
        // ������� ���������������� �������.
        // �����������:
        //  � � �������� ������� �� k �������� �� n ��������� ���� ��
        // ������,
        //  y � ������������������ ������� �� n �������� �� k ���������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j;
            int i; // ������� ������
            int j;
            int mm1; // �������� ��������� ��������
            int mm2;

            for (i = 0, mm2 = 0; i < k; i++)
                for (j = 0, mm1 = i; j < n; j++, mm1 += k)
                    y[mm1] = x[mm2++];
        }
        //---------------------------------------------------------------------------
        public double Det(List<double> a, int n)
        // ������� ���������� ������������.
        // �����������:
        //  a � �������� �������,
        //  n � ������� �������.
        // ����������:
        // ������� �� ��������� ��������� ������������� ��������� ������.
        // ������������ ��������:
        //  �������� ������������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j,k;
            int i; // �������� �����
            int j;
            int k;
            int m; // ���� ��������������� ���������� -  ������� ����������� �������
            int m1;
            int sign = 1;
            double s = 0; // ����� -  ������������ ��������
            List<double> a1;
            ////////////////////////
            // ��� ������� ������� 1
            if (n == 1)
                return a[0];
            else
            {
                ///////////////////////////////////
                // �������� ��� ������� ������� > 1
                a1 = new List<double>((n - 1) * (n - 1));
                for (i = 0; i < n; i++)
                {
                    ///////////////////
                    // ��������� ������
                    for (j = 1, m = 0, m1 = n; j < n; j++)
                        for (k = 0; k < n; k++, m1++)
                            if (k != i)
                                a1[m++] = a[m1];
                    //////////////////////////
                    // ���������� ������������
                    sign *= -1;
                    s += a[i] * sign * Det(a1, n - 1);
                }
                a1 = null;
            }
            return s;
        }
        //---------------------------------------------------------------------------
        public int InverseMatrix(List<double> a, List<double> t, int k)
        // ������� ���������� �������� �������.
        // �����������:
        //  a � �������� �������,
        //  t � �������� �������,
        //  k � ������� �������.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �������,
        //  �1 ��� ���������� ������,
        //  �2 ��� ������ � �����������.
        {
            int Code; // ������������ ��� ������
            List<double> a1; // ����� �������� �������
            a1 = new List<double>(k * k);
            //if ((a1 = new double[k * k]) == null)
            //  return -1;
            ArrayToArray(a, a1, k * k);
            MatrixUnit(t, k);
            Code = Gauss(t, a1, k, k, 0.000001);
            a1 = null;
            return Code;
        }
        //---------------------------------------------------------------------------

        public int PseudoInverseMatrix(List<double> a, List<double> ap, int m, int n)
        // ������� ���������� �������������� �������.
        // �����������:
        //   a � �������� ������� m x n, �� �����������,
        //  ap � �������������� ������� n x m,
        //   m � ����� ����� ������� a,
        //   n � ����� �������� ������� a,
        // ������������ ��������:
        //   0 ��� ���������� ��������� �������,
        //  �1 ��� ���������� ������ ��� ����������,
        //  �2 ��� ������ ���������� ��� ������ � �����������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i; // ������� �����
            int Code = 0; // ������������ ��������
            List<double> u; // ������� ������, ����� �� ��������, ���� -  ������� ���������� �� ����������� ������
            List<double> v;
            List<double> s;
            List<double> r;
            // ��������� �������� �������
            u = new List<double>(m * m);
            v = new List<double>(n * n);
            s = new List<double>(n * n);
            r = new List<double>(n * n);

            Code = SingularValueDecomposition(a, m, n, u, s, v);
            if (Code != 0)
                return Code;
            for (i = 0; i < n * n; i += n + 1)
                if (s[i] != 0)
                    s[i] = 1.0 / s[i];
            MatrixMultiplyMatrix(v, s, r, n, n, n);
            MatrixMultiplyMatrixT(r, u, ap, n, n, m);
            u = null;
            v = null;
            s = null;
            r = null;
            return Code;
        }

        //---------------------------------------------------------------------------
        public void MatrixMultiplyVector(double[] a, double[] u, double[] t, int n)
        // ������� ��������� ������� �� ������.
        // �����������:
        //  a � ������� �������� n * n (�������� �� ��������),
        //  u � ������������� ������ n,
        //  t � �������������� ������������� ������ n.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j,k;
            int i;
            int j;
            int k;
            for (i = 0; i < n; i++)
                for (j = 0, t[i] = 0, k = i; j < n; j++, k += n)
                    t[i] += a[k] * u[j];
        }

        //---------------------------------------------------------------------------

        public int Solves(List<double> a, List<double> b, int n, int m)
        // ������� ������� �������������� ������� �������� ���������.
        // �����������:
        //  a � �������� ������� ������� �������� m x n, �����������,
        //  b � ������� ��������� ������ ������ n, �� ������ � �������,
        //      �� ����� ������������ ������ m <= n �����,
        //  n � ������� ������� (����� �����������),
        //  m � ���� ������� ������� <= n.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �������,
        //  �1 ��� ���������� ������ ��� ����������,
        //  �2 ��� ������ ���������� ��� ������ � �����������.
        {
            int Code = 0; // ������������ �������e
            List<double> t; // ������� ������
            //int i;
            //int j;
            //int l;
            t = new List<double>(m * n);
            MatrixT(a, t, m, n);
            Code = PseudoInverseMatrix(t, a, n, m);
            //throw new System.NotImplementedException();
            //if (!(Code))
            if (Code != 0)
            {
                MatrixTMultiplyMatrix(b, a, t, 1, m, n);
                ArrayToArray(t, b, n);
            }
            t = null;
            return Code;
        }

        //---------------------------------------------------------------------------
        public int LinearRegression(double[] t, double[] z, double[] data, int m, int n)
        // ������� ���������� ������������� �������� �������������// ������.
        // �����������:   // ����������: m <= n.
        //  z    � ������� ������� �������� m x n,
        //  t    � ������ ��������� ������ ������ m,
        //  data � ������ ����������� ������������� ������ m,
        //  n    � ����� ������,
        //  m    � ����� ����������.
        // ������������ ��������:
        //  0 � ��� ���������� ��������� �����,
        // �1 ��� ���������� ������,
        // �2 ��� ������ ����������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j,k,mm,mm1,mm2;
            int i; // ��������
            int j;
            int k;
            int mm;
            int mm1;
            int mm2;
            int Code = 0; // ��� ������
            List<double> zz; // ������ ������ ������ -  ������ ����� ������
            List<double> bb;
            zz = new List<double>(m * m);
            bb = new List<double>(m);
            // ���������� ������� �������� ������������� ���������
            FillUp(zz, m * m, 0);
            FillUp(bb, m, 0);
            // ���������� ������� �������� ���������
            for (k = 0, mm2 = 0; k < m; k++)
            {
                for (i = 0, mm = k; i < n; i++)
                {
                    bb[k] += t[i] * z[mm];
                    mm += m;
                }
                for (j = 0; j < m; j++)
                {
                    for (i = 0, mm1 = j, mm = k; i < n; i++)
                    {
                        zz[mm2] += z[mm1] * z[mm];
                        mm += m;
                        mm1 += m;
                    }
                    mm2++;
                }
            }
            // ������� ������� �������� ���������
            Code = Gauss(bb, zz, m, 1, 0.000001);
            //if (!(Code))
            if (Code != 0)
                for (i = 0; i < m; i++)
                    data[i] = bb[i];
            zz = null;
            bb = null;
            if (Code == 0)
                return 0;
            else
                return -2;
        }
        //---------------------------------------------------------------------------
        public int LinearRegression0(double[] data, double[] z, double[] t, int numbers, int m)
        // ������� ���������� ������������� �������� ������������� ������ �� ��������� ������.
        // �����������:              // ����������: m <= n.
        //   z       � ������� ������� �������� numbers x m,
        //   data    � ������ ��������� ������ ������ m,
        //   t       � ������ ������� ������ m + 1,
        //   m       � ����� ������,
        //   numbers � ����� ����������.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �����,
        //  �1 ��� ���������� ������,
        //  �2 ��� ������ ����������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,count,mm1,mm2;
            int i; // ��������
            int count;
            int mm1;
            int mm2;
            int Code = 0; // ��� ������
            double[] zz; // ������ ����� ������
            if ((zz = new double[(numbers + 1) * m]) == null)
                return -1;
            // ������������� ������� �������� ������
            for (i = 0, mm1 = 0; i < m; i++, mm1 += numbers + 1)
                zz[mm1] = 1; // ��������� ������ � ��������� ����
            for (i = 0; i < numbers; i++)
                for (count = 0, mm1 = i + 1, mm2 = i; count < m; count++)
                {
                    zz[mm1] = z[mm2];
                    mm1 += numbers + 1;
                    mm2 += numbers;
                }
            Code = LinearRegression(data, zz, t, numbers + 1, m);
            zz = null;
            if (Code == 0)
                return 0;
            else
                return -2;
        }

        //---------------------------------------------------------------------------
        public void MatrixPlusMatrix(double[] a, double[] b, double[] c, int n)
        // ������� ��������� ����������� ��� �������.
        // �����������:
        //  a � ������ ���������,
        //  b � ������ ���������,
        //  c � �����,
        //  n � ������ ������� �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i;
            for (i = 0; i < n; i++)
                c[i] = a[i] + b[i];
        }
        //---------------------------------------------------------------------------
        public void MatrixMinusMatrix(double[] a, double[] b, double[] c, int n)
        // ������� �������� ����������� �� ������ ������� ������.
        // �����������:
        //  a � �����������,
        //  b � ����������,
        //  c � ��������.
        //  n � ������ ������� �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i;
            for (i = 0; i < n; i++)
                c[i] = a[i] - b[i];
        }
        //---------------------------------------------------------------------------

        public void ArrayToArray(List<double> x, List<double> y, int n)
        // ������� ����������� ������� � ������.
        // �����������:
        //  � � ������ � ��������,
        //  y � ������ � ��������,
        //  n � ���������� ���������� ���������.
        {
            //for (register int i = 0; i < n; i++)
            for (int i = 0; i < n; i++)
                y[i] = x[i];
        }
        //---------------------------------------------------------------------------
        public void MatrixUnit(List<double> e, int n)
        // �����������:
        //  e � �� ������ � ��������� �������,
        //  n � ����������� �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j;
            int i; // �������� �����
            int j;
            int k = 0; // ������� ����������
            int m = 0;
            for (i = 0; i < n; i++, e[m] = 1, m += n + 1)
                for (j = 0; j < n; j++)
                    e[k++] = 0;
        }

        //---------------------------------------------------------------------------
        public int Gauss(List<double> b, List<double> a, int row, int column, double eps)
        // ������� ������� ��������� ������� �������� ��������������
        // ��������� ������� ������ � ������� �������� ��������.
        // �����������:
        //       b � ������� ��� ������ ��������� ������,
        //           �����������, �� ������ � ��������������
        //           ������� ��� ������ �������,
        //       a � ������� ������� ����������� row, �����������,
        //     row � ����������� ������� � ����� ����� ������ a � b �
        //           �������� ������� a,
        //  column � ����� �������� ������� b,
        //     eps � ����� �������� ��� �������� ����������.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �����,
        //  �2 ��� ������ ����������.
        {
            int i; // ����� ������� �������� �������� -  ����� ������ �������� �������� -  ����� ������� ������ -  ����� ������ ������ �������� �������� -  ��������
            int j;
            int k;
            int js;
            int ji;
            int ij;
            int im;
            int mi;
            int mj;
            int @is;
            int ik;
            int @in;
            int jn;

            double guide; // ����� -  ���������� �������� �������� ��������
            double save;
            // ������ ��������� � �������������� �������
            for (k = 0, ik = -row; k < row; k++)
            {
                ik += row;
                @is = ik + k;
                // ����� �������� ��������
                for (i = k, ij = @is, @in = k, jn = k, guide = a[@is]; i < row; i++)
                    for (j = k, im = ij++; j < row; j++)
                    {
                        if (Math.Abs(a[im]) > Math.Abs(guide))
                        {
                            guide = a[im];
                            @in = i;
                            jn = j;
                        }
                        im += row;
                    }
                // �������� �� ����������
                if (Math.Abs(guide) < eps)
                    return -2;
                // ������������ �����
                for (i = k, im = @is, ij = ik + @in; i < row; i++)
                {
                    save = a[ij];
                    a[ij] = a[im];
                    a[im] = save / guide;
                    ij += row;
                    im += row;
                }
                for (i = 0, ij = @in, im = k; i < column; i++)
                {
                    save = b[ij];
                    b[ij] = b[im];
                    b[im] = save / guide;
                    ij += row;
                    im += row;
                }
                // ������������ ��������
                for (i = 0, im = ik - 1, ij = row * jn - 1; i < row; i++)
                {
                    save = a[++ij];
                    a[ij] = a[++im];
                    a[im] = save;
                }
                // �������������� ���������� ����� �������
                ji = k + 1;
                for (i = ji, im = @is, mi = @is + row; i < row; i++)
                {
                    for (j = ji, mj = mi, ij = ++im + row, save = a[im]; j < row; j++)
                    {
                        a[ij] = a[ij] - a[mj] * save;
                        mj += row;
                        ij += row;
                    }
                    for (j = 0, ij = i, mj = k; j < column; j++)
                    {
                        b[ij] = b[ij] - b[mj] * save;
                        ij += row;
                        mj += row;
                    }
                }
                // ���������� ������� ��������� �������� ������� �������
                // � ������ ������� ������� �������
                if (k == 0)
                    for (i = 0; i < row; i++)
                        a[i] = (double)i; // ��������� ������� �������
                save = a[jn]; // �������� ������� ������������
                a[jn] = a[k];
                a[k] = save;
            }
            // �������� ��������� � ��������� �������
            for (i = row - 1, im = row * row - 1; i > 0; i--)
                for (js = 0, --im, @in = i - 1, mi = 0; js < column; js++)
                {
                    for (j = row - 1, ij = im, save = 0; j >= i; j--)
                    {
                        save += b[j + mi] * a[ij];
                        ij -= row;
                    }
                    ji = @in + mi;
                    b[ji] = b[ji] - save;
                    mi += row;
                }
            // �������������� ������� �������
            for (i = 0, j = 0; i < row; i++)
            {
                @in = (int)(a[j]);
                if (@in == j)
                    j++; // ������� ������� ������� �� ����� �����
                else
                {
                    for (js = 0, ij = @in, ji = j; js < column; js++)
                    {
                        save = b[ij];
                        b[ij] = b[ji];
                        b[ji] = save;
                        ij += row;
                        ji += row;
                    }
                    save = a[@in];
                    a[@in] = a[j];
                    a[j] = save;
                }
            }
            return 0;
        }

        //---------------------------------------------------------------------------
        public int SingularValueDecomposition(List<double> a, int m, int n, List<double> u, List<double> s, List<double> v)
        // ������� ���������� ������� �� ����������� ������.
        // �����������:
        //   a � �������� ������� m x n, �� �����������,
        //   m � ����� ����� ������� a,
        //   n � ����� �������� ������� a,
        //   u � ����� ������� ������������� �������������� m x n,
        //   s � ������� ����������� ����� n x n,
        //   v � ������ ������� ������������� �������������� n x n.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �������,
        //  �1 ��� ���������� ������ ��� ����������,
        //  �2 ��� ������ ���������� ��� ������ � �����������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j;
            int i = 0; // �������
            int j = 0;
            int k = 0; // ������������ �������� -  ������� ����������
            int Code = 0;
            double save; // ������� ������ -  ������� ����������
            List<double> c;
            List<double> t;
            c = new List<double>(n * n);
            t = new List<double>(n * n);
            // ���������� ����������� �������� � ��������
            MatrixTMultiplyMatrix(a, a, s, n, m, n);
            if ((Code = Jacobi(s, n, 0.000001, k, v)) != 0)
                return Code;
            // �������������� ����������� �������� � ��������
            for (i = 1, k = n + 1; i < n; i++, k += n + 1)
                s[i] = s[k];
            for (i = 0; i < n; i++)
            {
                k = i;
                save = s[i];
                for (j = i + 1; j < n; j++)
                    if (save < s[j])
                    {
                        k = j;
                        save = s[j];
                    }
                ColInterchange(s, 1, i, k);
                ColInterchange(v, n, i, k);
            }
            // ������� ����������� �����
            for (i = 1, k = n + 1; i < n; i++, k += n + 1)
            {
                s[k] = s[i];
                s[i] = 0;
            }
            for (i = 0; i < n * n; i += n + 1)
                //   s[i] = sqrt (s[i]);
                s[i] = Math.Sqrt(Math.Abs(s[i]));
            // ����� �������
            MatrixMultiplyMatrixT(s, v, c, n, n, n);
            InverseMatrix(c, t, n);
            MatrixMultiplyMatrix(a, t, u, m, n, n);
            c = null;
            t = null;
            return Code;
        }
        //---------------------------------------------------------------------------
        public void ColInterchange(List<double> a, int n, int k, int l)
        // ������� ������������ �������� �������.
        // �����������:
        //  a   � �������� �������,
        //  n   � ����� ����� �������,
        //  k,l � ������ �������������� �������� (��������� � ����).
        // ����������:
        //  ��������� ������������ �� ����� �������� �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i; // ������� �����

            int m1; // �������� ����������� �������
            int m2;

            double save; // ��������������� ����������

            for (i = 0, m1 = k * n, m2 = l * n; i < n; i++)
            {
                save = a[m1];
                a[m1++] = a[m2];
                a[m2++] = save;
            }
        }
        //---------------------------------------------------------------------------
        public void RowInterchange(double[] a, int n, int m, int k, int l)
        // ������� ������������ ����� �������.
        // �����������:
        //  a   � �������� �������,
        //  n   � ����� ����� �������,
        //  m   � ����� �������� �������,
        //  k,l � ������ �������������� ����� (��������� � ����).
        // ����������:
        //  ��������� ������������ �� ����� �������� �������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i;
            int i; // ������� �����
            double save; // ��������������� ����������
            for (i = 0; i < m; i++, k += n, l += n)
            {
                save = a[k];
                a[k] = a[l];
                a[l] = save;
            }
        }
        //---------------------------------------------------------------------------
        public void MatrixMultiplyMatrixT(List<double> a, List<double> b, List<double> r, int n, int m, int l)
        // ������� ��������� ������� �� ����������������� �������.
        // �����������:
        //  a � ������ �����������, n x m,
        //  b � ������ �����������, l x m,
        //  r � ���������, n x l,
        //  n � ����� ����� � ������� a,
        //  m � ����� �������� � ������� a � �������� � ������� b,
        //  l � ����� ����� � ������� b.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i, j, k;
            int i; // ������� ����� �� ������� b -  ������� ����� �� �������� a -  ������� ����� �� �������� b
            int j;
            int k;
            int ia; // ������ ������� ������� ������ b -  ������� ��������� ������� r -  ������� ��������� ������� b -  ������� ��������� ������� a
            int ib;
            int ir = -1;
            int ik = -1 - l;
            // ���� �� ������� ������� b
            for (k = 0; k < l; k++)
                // ���� �� ������� ������� a
                for (j = 0, ik++; j < n; j++)
                    // ���� �� �������� ������� a
                    for (i = 0, ia = j - n, ib = ik, r[++ir] = 0; i < m; i++)
                    {
                        ia += n;
                        ib += l;
                        r[ir] += a[ia] * b[ib];
                    }
        }
        //---------------------------------------------------------------------------
        public void MatrixTMultiplyMatrix(List<double> a, List<double> b, List<double> r, int n, int m, int l)
        // ������� ��������� ����������������� ������� �� �������.
        // �����������:
        //  a � ������ �����������, m x n,
        //  b � ������ �����������, m x l,
        //  r � ���������, n x l,
        //  n � ����� �������� � ������� a,
        //  m � ����� ����� � ������� a � ����� � ������� b,
        //  l � ����� �������� � ������� b.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i, j, k;
            int i; // ������� ����� �� ������� b -  ������� ����� �� �������� a -  ������� ����� �� �������� b
            int j;
            int k;
            int ia; // ������ ������� �������� ������� b -  ������� ��������� ������� r -  ������� ��������� ������� b -  ������� ��������� ������� a
            int ib;
            int ir = -1;
            int bs = -m - 1;
            // ���� �� �������� ������� b
            for (k = 0; k < l; k++)
                // ���� �� �������� ������� a
                for (j = 0, ia = -1, bs += m; j < n; j++)
                    // ���� �� ������� ������� a
                    for (i = 0, r[++ir] = 0, ib = bs; i < m; i++)
                        r[ir] += a[++ia] * b[++ib];
        }

        //---------------------------------------------------------------------------

        public int Jacobi(List<double> a, int n, double eps, int it, List<double> b)
        // ������� ���������� ����������� �������� � ���������������
        // ����������� �������� �������������� ������������ �������
        // ������� �����.
        // �����������:
        //  a    � ������������ �������������� �������, �����������,
        // �� ������ �� ������� ��������� � ����������� ��������,
        //  n    � ������� ������� a,
        //  eps  � ����� �������� ��� �������� ����������,
        //  *it  � ������� ��������,
        //  b  � ������ ����������� ��������, ��������������� �����������
        //       ����������� ���������, ������������� �� ��������.
        // ����������:
        //  �������� ������������� ������� ������� �� ������������ �
        //  ��� ����������� ������� ��������� ������� � �������� ��������
        //  ���������� ����������� ��������. ��� �������� �������������
        //  ����������� ������� Krauth.
        // ������������ ��������:
        //   0 ��� ���������� ��������� �������,
        //  �1 ��� ���������� ������ ��� ������� ��������,
        //  �2 ��� ������ � �����������.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j;
            int i = 0; // �������� �����
            int j = 0;
            int p = 0; // ������ ����������� ������� -  ���������� ����������� ���������������� // ��������
            int q = 0;
            int m1 = 0;
            double max = 0; // ������� ������� -  ����� � ������� ���� �������� � �� �������� -  �������� ���� �������� -  ������ ����������� ���������������� ��������
            double t;
            double s;
            double s2;
            double c;
            double c2;
            List<double> r1;
            List<double> r2;
            // ��� ����������� 1
            if (n == 1)
            {
                if (a[0] == 0)
                    return -2;
                else
                    a[0] = 1.0 / a[0];
                b[0] = 1;
                it = 1;
                return 0;
            }
            // ��������� ��������
            MatrixUnit(b, n);
            it = 0;
            /*
            if ((r1 = new double[n * n]) == false)
              return -1;
            if ((r2 = new double[n * n]) == false)
            {
               r1 = null;
               return -1;
              }
             */
            r1 = new List<double>(n * n);
            r2 = new List<double>(n * n);
            do // ����� ����������� ���������������� ��������
            {
                for (i = 0; i < n; i++)
                    for (j = i + 1, m1 = i * n + i + 1; j < n; j++, m1++)
                    {
                        t = Math.Abs(a[m1]);
                        if (m1 == 1 || t > max)
                        {
                            p = i;
                            q = j;
                            max = t;
                        }
                    }
                // ���������� ���������� ��������
                //    t = a[p * (n + 1)] == a[q * (n + 1)] ? M_PI / 4 * Sign (a[n * q + p]) :
                t = a[p * (n + 1)] == a[q * (n + 1)] != false ? M_PI / 4 * SignNull(a[n * q + p]) : Math.Atan(a[q * n + p] / (a[p * n + p] - a[q * n + q]) * 2) / 2;
                s = Math.Sin(t);
                c = Math.Cos(t);
                s2 = s * s;
                c2 = c * c;
                // �������� �������� �������
                ArrayToArray(a, r1, n * n);
                for (i = 0; i < n; i++)
                    if (i != p && i != q)
                    {
                        r1[p * n + i] = a[p * n + i] * c + a[q * n + i] * s;
                        r1[i * n + p] = r1[p * n + i];
                        r1[q * n + i] = -a[p * n + i] * s + a[q * n + i] * c;
                        r1[i * n + q] = r1[q * n + i];
                    }
                r1[p * n + p] = a[p * n + p] * c2 + 2 * a[q * n + p] * c * s + a[q * n + q] * s2;
                r1[q * n + q] = a[p * n + p] * s2 - 2 * a[q * n + p] * c * s + a[q * n + q] * c2;
                r1[q * n + p] = (a[q * n + q] - a[p * n + p]) * c * s + a[q * n + p] * (c2 - s2);
                r1[p * n + q] = r1[q * n + p];
                it++;
                ArrayToArray(r1, a, n * n);
                // ���������� ����������� ��������
                MatrixUnit(r1, n);
                r1[p * n + p] = c;
                r1[q * n + q] = c;
                r1[q * n + p] = s;
                r1[p * n + q] = -s;
                MatrixMultiplyMatrixT(b, r1, r2, n, n, n);
                ArrayToArray(r2, b, n * n);
                // ������� ��������� ������������� ��������
            } while (EuclidNormaWithoutDiag(a, n) > eps);
            r1 = null;
            r2 = null;
            return 0;
        }

        //---------------------------------------------------------------------------

        public double EuclidNormaWithoutDiag(List<double> a, int n)
        // ������� ���������� ���������� ����� ������� ��� �����
        // ������������ ���������.
        // �����������:
        //  a � �������� �������,
        //  n � ������� �������.
        // ������������ ��������:
        //  �������� �����.
        {
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j;
            int i;
            int j;
            int m = 0;
            // long double s = 0;//+++++++
            double s = 0;
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++, m++)
                    if (i != j)
                        s += a[m] * a[m];
            return Math.Sqrt(s);
        }
        //---------------------------------------------------------------------------
        public void FillUp(List<double> a, int n, double p)
        // ������� ��������� ������ �������� ���������.
        // �����������:
        //  a � ������ ������ n,
        //  p � �����������.
        {
            for (int i = 0; i < n; i++)
                a[i] = p;
        }
        //---------------------------------------------------------------------------
        public void FillUp(List<int> a, int n, int p)
        // ������� ��������� ������ ����� �������� ���������.
        // �����������:
        //  a � ������ ������ n,
        //  p � �����������.
        {
            for (int i = 0; i < n; i++)
                a[i] = p;
        }
        //---------------------------------------------------------------------------
        public double Sign(double k)
        // ������� ���������� ����� ������������� �����.
        // �����������:
        //  k � �����.
        // ������������ ��������:
        //  0, ���� ����� ����� ����,
        //  ������� �� ������ ����� � ���� ������.
        {
            if (k > 0)
                return 1;
            else if (k < 0)
                return -1;
            else
                return 0;
        }
        //---------------------------------------------------------------------------
        public int Sign(int k)
        // ������� ���������� ����� ������ �����.
        // �����������:
        //  k � �����.
        // ������������ ��������:
        //  0, ���� ����� ����� ����,
        //  ������� �� ������ ����� � ���� ������.
        {
            if (k > 0)
                return 1;
            else if (k < 0)
                return -1;
            else
                return 0;
        }
        //---------------------------------------------------------------------------
        public double SignNull(double k)
        // ������� ���������� ����� ������������� �����.
        // �����������:
        //  k � �����.
        // ����������:
        //  0 ��������� ������������� ������.
        // ������������ ��������:
        //  ������� �� ������ �����.
        {
            if (k >= 0)
                return 1;
            else
                return -1;
        }

        //---------------------------------------------------------------------------
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
        //#pragma package(smart_init)
        //---------------------------------------------------------------------------
        public double clcFabs(double k)
        {
            if (k < 0)
            {
                return -k;
            }
            else
            {
                return k;
            }
        }
        //---------------------------------------------------------------------------
        public int Polynomial(List<double> y, List<double> x, int u, int m, int n, List<double> c)
        {
            //int qMData::Polynomial (double y[],double x[],int u,int m,int n,double c[]){
            // ������� ���������� �������������� ������.// �����������:
            //   y � ������ ������� ������ n,
            //   x � ������ ��������� ������ n,
            //   u � ������� ������������ �����,
            //   m � ������� ��������,
            //   c � ����������� ������ ������������� ������ m � u + 1.
            // ������������ ��������:
            //   0 ��� ���������� ��������� �����,
            //  �1 ��� ���������� ������,
            //  �2 ��� ������ ����������.
            // ����������: m <= n.
            //C++ TO C# CONVERTER NOTE: 'register' variable declarations are not supported in C#:
            //ORIGINAL LINE: register int i,j,k,l;
            int i; // ��������
            int j;
            int k;
            int l;
            int Code = 0; // ��������������� �������� -  ��� ������
            int mu = m - u + 1;
            List<double> z; // ������ ������ ������ -  ������ ����� ������
            List<double> b;
            z = new List<double>(mu * mu);
            b = new List<double>(mu);
            //////////////////////////////////////////////////////
            // ���������� ������� �������� ������������� ���������
            FillUp(z, mu * mu, 0);
            FillUp(b, mu, 0);
            ////////////////////////////////////////
            // ���������� ������� �������� ���������
            for (k = u; k <= m; k++)
            {
                for (i = 0; i < n; i++)
                {
                    for (j = u, l = k - u; j <= m; j++)
                    {
                        z[l] += Math.Pow(x[i], j + k);
                        l += m - u + 1;
                    }
                    b[k - u] += y[i] * Math.Pow(x[i], k);
                }
            }
            /////////////////////////////////////
            // ������� ������� �������� ���������
            Code = Gauss(b, z, mu, 1, 0.000001);
            if (Code == 0)
            {
                ArrayToArray(b, c, mu);
            }
            z = null;
            b = null;
            return Code;
        }
        //int Polynomial (double y[],double x[],int u,int m,int n,double c[]);
        private List<List<double>> Mtrx = new List<List<double>>();
        //   virtual void create(int a);

        public void set(List<List<double>> aMtrx)
        {
            this.Mtrx = aMtrx;
        }
        public void set(qMData aMData)
        {
            this.Mtrx = aMData.get();
        }
    }
}