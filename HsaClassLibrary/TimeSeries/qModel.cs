using System.Collections.Generic;
using System;
using HsaClassLibrary.Helpers;

namespace TimeSeries
{
    public abstract class qModel
    {
        /// <summary>
        /// 
        /// </summary>
        public qModel()
        {
            State = false;
        }
        //   qModel(qModel &aModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aData"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]       
        public qModel(qData aData)
        {
            State = false;
            set(aData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aData"></param>
        public virtual void set(qData aData)
        {
            List<double> tVctr = new List<double>();
            tVctr = aData.get();
            Data.set(aData);
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void clear()
        {
            Data.clear();
            Prognos.clear();
            A.clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aData"></param>
        public virtual void setDataTrans(qData aData)
        {
            DataTrans.set(aData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aData"></param>
        public virtual void setPrognos(qData aData)
        {
            Prognos.set(aData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual qModel newModel()
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual qModel clonModel()
        {
            return null;
        }
        /// <summary>
        /// "��������";
        /// </summary>
        /// <returns></returns>
        public virtual qData getData()
        {
            return Data;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public virtual qData getPrognos()
        {
            return Prognos;
        }
        /// <summary>
        /// ������ ��������
        /// </summary>
        /// <returns></returns>
        public virtual qData getA()
        {
            return A;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual qData getDataPrognos()
        {
            return DataPrognos;
        }
        /// <summary>
        /// return Data.size();
        /// </summary>
        /// <returns></returns>
        public virtual int getSize()
        {
            return Data.size();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getNum()
        {
            return ModelNum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getType()
        {
            return ModelType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool getState()
        {
            return State;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string getName()
        {
            return ModelName;
        }
        
        /// <summary>
        /// ������ ��������
        /// </summary>
        public void mkA()
        {
            List<double> VData = new List<double>();
            VData = Data.get();
            List<double> VPrognos = new List<double>();
            VPrognos = Prognos.get();
            int DataSize = 0;
            DataSize = VData.Count;
            List<double> VA = new List<double>(DataSize);

            for (int i = 0; i < DataSize; i++)
            {
                VA[i] = VData[i] - VPrognos[i];
            }
            A.set(VA);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Metod"></param>
        public abstract void mkModel(int Metod);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumPrognos"></param>
        public virtual void mkPrognos(int NumPrognos)
        {
            ;
        }
        //---------------------------------------------------------------------------
        /// <summary>
        /// ��c�������� ������ �� �������� �� ������
        /// � �������� ��������������� ������
        /// </summary>
        public virtual void mkDataPrognos()
        {
            List<double> VPrognos = new List<double>();
            List<double> VTrans = new List<double>();
            List<double> VDataPr = new List<double>();
            VPrognos = Prognos.get();
            VTrans = DataTrans.get();
            int sizePr;
            int sizeTr;
            int sizeData;
            sizePr = VPrognos.Count;
            sizeTr = VTrans.Count;
            if (sizePr != sizeTr)
            {
                //ShowMessage("qModel::mkDataPrognos: ������ ������������ ������ �� ���������");
                throw new System.ApplicationException("qModel::mkDataPrognos: ������ ������������ ������ �� ���������");
            }
            if (sizePr > sizeTr)
            {
                sizeData = sizeTr;
            }
            else
            {
                sizeData = sizePr;
            }
            VDataPr = new List<double>(sizeData);
            for (int i = 0; i < sizeData; i++)
            {
                VDataPr[i] = VPrognos[i] + VTrans[i];
            }
            DataPrognos.set(VDataPr);
        }
        // � �������� ��������������� ������

        /// <summary>
        /// NumPointPrognos
        /// </summary>
        /// <returns></returns>
        public int getNumPoPr()
        {
            return NumPointPrognos;
        }

        /// <summary>
        /// NumPointPrognos ����� ����������
        /// </summary>
        /// <returns></returns>
        public int getNumStPr()
        {
            return NumPointStart;
        }

        /// <summary>
        /// NumPointPrognos ����� �����������
        /// </summary>
        /// <returns></returns>
        public int getNumGrPr()
        {
            return NumPointShow;
        }

        /// <summary>
        /// NumPointPrognos
        /// </summary>
        /// <param name="NPP"></param>
        public void setNumPoPr(int NPP)
        {
            NumPointPrognos = NPP;
        }

        /// <summary>
        /// NumPointPrognos ����� ����������
        /// </summary>
        /// <param name="NPP"></param>
        public void setNumStPr(int NPP)
        {
            NumPointStart = NPP;
        }

        /// <summary>
        /// NumPointPrognos ����� �����������
        /// </summary>
        /// <param name="NPP"></param>
        public void setNumGrPr(int NPP)
        {
            NumPointShow = NPP;
        }

        /// <summary>
        /// ������ �������������� ���������� ����
        /// "�������������� ��������: "+stParData[0]
        /// "���������: "+stParData[1]
        /// "������� �������������� ����������: "+stParData[2]
        /// </summary>
        public List<double> clcStParData()
        {
            //std::vector<double>
            stParData = new List<double>(3);
            stParData[0] = Data.clcMid();
            stParData[1] = Data.clcVar();
            stParData[2] = Math.Sqrt(stParData[1]);
            return stParData;
        }

        /// <summary>
        /// ������ �������������� ���������� ��������
        /// "�������������� ��������: "+stParProg[0]
        /// "���������: "+stParProg[1]
        /// "������� �������������� ����������: "+stParProg[2]
        /// </summary>
        public List<double> clcStParProg()
        {
            //std::vector<double>
            stParProg = new List<double>(3);
            stParProg[0] = Prognos.clcMid();
            stParProg[1] = Prognos.clcVar();
            stParProg[2] = Math.Sqrt(stParProg[1]);
            return stParProg;
        }
        /// <summary>
        /// ������ �������������� ���������� ������ ��������
        /// "�������������� ��������: "+stParA[0]
        /// "���������: "+stParA[1]
        /// "������� �������������� ����������: "+stParA[2]
        /// "�� ������� ����������: "+stParA[3]
        /// </summary>
        /// <returns></returns>
        public List<double> clcStParA()
        {
            //std::vector<double>
            stParA = new List<double>(6);
            stParA[0] = A.clcMid();
            stParA[1] = A.clcVar();
            stParA[2] = Math.Sqrt(stParA[1]);

            List<double> rAA = new List<double>();
            rAA = A.clcACorrelation().get();
            int size = rAA.Count;
            int K = 30;
            double Sum = 0;
            for (int i = 0; i < K; i++)
            {
                Sum += rAA[i] * rAA[i];
            }
            stParA[3] = Sum;
            //   stParA[3] = A.clcAmountSquare( rAA );
            stParA[3] = (stParA[3] - 1) * size;
            stParA[4] = K;
            stParA[5] = HsaClassLibrary.Helpers.MathHelper.getXi2_95(K);
            //if(stParA[3]<stParA[5]) {
            //"�� ����������� �������� �� ������� �������������: "+stParA[5]
            //"��� �������� �������: "+stParA[4], ffFixed, 6, 3)+" ������ ���������.";
            //}else{
            //"����������� �������� �� ������� �������������: "+stParA[5]
            //"��� �������� �������: "+stParA[4], ffFixed, 6, 3)+" ������ �������� �� ���������.";
            //}
            return stParA;
        }

        /// <summary>
        /// ������ �������������� ���������� ����
        /// </summary>
        public virtual void shwStParData()
        {
            /*
            "������ �������������� ���������� ����."
            "�������������� ��������: "+stParData[0]
            "���������: "+stParData[1]
            "������� �������������� ����������: "+stParData[2]
             */
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ������ �������������� ���������� ��������
        /// </summary>
        public virtual void shwStParProg()
        {
            /*
            "������ �������������� ���������� ��������."
            "�������������� ��������: "+stParProg[0]
            "���������: "+stParProg[1]
            "������� �������������� ����������: "+stParProg[2]
             */
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ������ �������������� ���������� ������ ��������
        /// </summary>
        public virtual void shwStParA()
        {
            /*
              "������ �������������� ���������� ������ ��������.");
            "�������������� ��������: "+stParA[0]
            "���������: "+stParA[1]
            "������� �������������� ����������: "+stParA[2]
            "�� ������� ����������: "+stParA[3]
            if(stParA[3]<stParA[5])
            {
            "�� ����������� �������� �� ������� �������������: "+stParA[5]
            "��� �������� �������: "+stParA[4] "������ ���������.");
            }
            else
            {
            "����������� �������� �� ������� �������������: "+stParA[5]
            "��� �������� �������: "+stParA[4] "������ �������� �� ���������.");
            }
             */
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ������ ��� �������
        /// </summary>
        protected qData Data = new qData(); 
        /// <summary>
        /// ������� ������
        /// </summary>
        protected qData Prognos = new qData();
        /// <summary>
        /// ����������� ������
        /// </summary>
        protected qData A = new qData();
        /// <summary>
        /// ������� ������ ���������������� ������
        /// </summary>
        protected qData DataTrans = new qData(); 
        /// <summary>
        /// �������� ������
        /// </summary>
        protected qData DataPrognos = new qData();
        /// <summary>
        /// ����� ����� ��������
        /// </summary>
        protected int NumPointPrognos; 
        /// <summary>
        /// ����� ����� �� ������
        /// </summary>
        protected int NumPointStart; 
        /// <summary>
        /// ����� ����� �� ������
        /// </summary>
        protected int NumPointShow;  

        /// <summary>
        /// �������������� ���������
        /// </summary>
        protected List<double> stParData = new List<double>();
        /// <summary>
        /// 1 ���. ��������, 2 ���������, 3 ���
        /// </summary>
        protected List<double> stParProg = new List<double>();
        // 1 ���. ��������, 2 ���������, 3 ���
        protected List<double> stParA = new List<double>(); 

        //---------------------------------------------------------------------------
        /// <summary>
        /// ��� ������
        /// </summary>
        protected String ModelName = "";
        /// <summary>
        /// State
        /// </summary>
        protected bool State;
        /// <summary>
        /// ModelNum - ����� ����������� ������ 1 ARIMA 2 Ss
        /// </summary>
        protected int ModelNum;
        /// <summary>
        /// ModelType - ��� ����������� ������ // �������//�����...
        /// </summary>
        protected int ModelType;
    }
}