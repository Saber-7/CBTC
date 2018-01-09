using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Timers;
namespace ZC
{
    class Communication
    {
        ZC form1 = new ZC();
        public Communication(ZC form1)
        {
            this.form1 = form1;
        }
        public Socket socketMain = null;
        public string hostIPAdress = "192.168.1.21";
        public string DIP = "";
        int MainSport = 5001;//初始化监听端口
        public int Dport = 5001;
        //bool forIsFirstMessage=true;
        //bool beIsFirstMessage=true;
        //int forTotal,beTotal;
       
        byte[] receiveDataArray = new byte[400];

        private string _st = "";
        public string St
        {
            get { return _st; }
            set { _st = value; }
        }
        #region 监听端口
        public void ListenControlData()
        {
            IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(hostIPAdress), MainSport);
            EndPoint EP = (EndPoint)ipEP;

            if (socketMain != null)
            {
                try
                {
                    socketMain.BeginReceiveFrom(receiveDataArray, 0, receiveDataArray.Length, SocketFlags.None, ref EP, new AsyncCallback(ReceiveControlData), null);
                }
                catch
                {

                }
            }
        }

        #endregion
        #region 接收信息

        public void ReceiveControlData(IAsyncResult iar)
        {

            if (socketMain != null)
            {
                try
                {
                   socketMain.EndReceive(iar);
                   //form1.classifyTrain(receiveDataArray);
                   form1.classifyTrain(receiveDataArray, DateTime.Now);
                   receiveDataArray = new byte[400];
                   
                }
                catch
                {
                }
                //回调函数
                ListenControlData();
            }
        }
        #endregion




        #region 发送函数

        public void SendControlData(byte[] sendControlPacket,EndPoint Ep)
        {
            socketMain.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 0);
            try
            {
                socketMain.SendTo(sendControlPacket, 0, sendControlPacket.Length, SocketFlags.None, Ep);
            }
            catch
            {
            }

        }

        #endregion
    }

}
