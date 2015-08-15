using System.Collections.Generic;
using System;
using System.Numerics;
using System.Linq; //IList<T>.ToList()

namespace HsaClassLibrary.Transform
{
    /// <summary>
    /// ������:
    /// Z(t) = R(t) + jQ(t)
    /// 
    /// ���������:
    /// A(t) = |Z(t)| = (R(t)^2 + Q(t)^2)^1/2
    /// 
    /// ���������� ����:
    /// �(t) = arctg[Q(t)/R(t)]
    /// 
    /// </summary>
    public class FourierSpectrum : ITransform
    {
        /// <summary>
        /// �������������� �����
        /// </summary>
        public Func<IList<double>, IList<Complex>> transform;

        /// <summary>
        /// ������������ ������
        /// </summary>
        private IList<double> source;
       
        /// <summary>
        /// ������������ ������
        /// </summary>
        public IList<double> Source
        {
            set
            {
                source = value;
            }
            get
            {
                return source;
            }
        }
        /// <summary>
        /// ����������� ������
        /// </summary>
        public IList<Complex> spectrum;

        /// <summary>
        /// �������������� ����� �������������� �������
        /// Z(t) = XR(t) + jXQ(t)
        /// </summary>
        public IList<double> Real;

        /// <summary>
        /// ������ ����� �������������� �������
        /// Z(t) = XR(t) + jXQ(t)
        /// </summary>
        public IList<double> Imag;

        /// <summary>
        /// ������ �������������� �������
        /// ��������� ��� �������������� �������
        /// A(t) = (R(t)^2 + Q(t)^2)^1/2
        /// </summary>
        public IList<double> Abs;

        /// <summary>
        /// ���������� ����:
        /// �(t) = arctg[Q(t)/R(t)]
        /// </summary>
        public IList<double> Phase;


        /// <summary>
        /// �������� �������� - ���������� �������� (��� ��������) ������������ �����.
        /// </summary>
        /// <returns>������. ������ �������� ����������./returns>
        public void getAbs()
        {
            IList<double> tmp = new double[spectrum.Count];
            // �������� ������ ��������
            for (int i = 0; i < spectrum.Count; i++)
            {
                //tmp[i] = Complex.Abs(spectrum[i]);                
                tmp[i] = spectrum[i].Magnitude;
            }
            Abs = tmp;
        }

        /// <summary>
        /// �������� ������������ ����� �������� ������� System.Numerics.Complex.
        /// </summary>
        /// <returns>�������. ������������ ����� ������������ �����.</returns>
        public void getReal()
        {
            IList<double> tmp = new double[spectrum.Count];
            // �������� ������ ��������
            for (int i = 0; i < spectrum.Count; i++)
            {
                tmp[i] = spectrum[i].Real;
            }
            Real = tmp;
        }

        /// <summary>
        /// �������� ������ ����� �������� ������� System.Numerics.Complex.
        /// </summary>
        /// <returns>������. ������ ����� ������������ �����.</returns>
        public void getImag()
        {
            IList<double> tmp = new double[spectrum.Count];
            // �������� ������ ��������
            for (int i = 0; i < spectrum.Count; i++)
            {
                tmp[i] = spectrum[i].Imaginary;
            }
            Imag = tmp;
        }

        /// <summary>
        /// �������� ���� ������������ �����.
        /// </summary>
        /// <returns>������. ���� ������������ ����� � ��������.</returns>
        public void getPhase()
        {
            IList<double> tmp = new double[spectrum.Count];
            // �������� ������ ��������
            for (int i = 0; i < spectrum.Count; i++)
            {
                //tmp[i] = arg(spectrum[i]);
                tmp[i] = spectrum[i].Phase;
            }
            //throw new System.NotImplementedException();
            Phase = tmp;
        }

        /// <summary>
        /// ��������� �������������� �����
        /// </summary>
        public virtual void Transform()
        {
            IList<double> data;
            throw new System.NotImplementedException();
            //����� ����������� ������ ������
            //data = HsaClassLibrary.Transform.TransformHelper.prepareZero(source, source.Count);
            data = source;

            //spectrum = HsaClassLibrary.Transform.FourierTransform.fft(data);
            spectrum = transform(data);

            //���������
            getAbs();
            getReal();
            getImag();
            getPhase();
        }
    }
}
