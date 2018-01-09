using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace CBTC
{
    class Socket
    {
        private IPEndPoint ipLocalPoint;
        private EndPoint RemotePoint;
        private System.Net.Sockets.Socket mySocket;
        public bool runningFlag = false;
        public int localPort = 0;
        public int sendPort = 0;
        public int length = 0;
        public byte[] recv = new byte[1024];
        public byte[] sendBuf_ = new byte[400];
        public delegate void RefDelegate();
        public event RefDelegate refEvent;
        public UInt64 Cycle = 0;
        public UInt64 Message = 0;
        public UInt64 Location = 0;
        Thread thread;
        Message mess = new Message();
        public DateTime ZCT1;
        public void Start(string ip, int port, string dIP, int dPort)
        {
            localPort = port;
            ipLocalPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(dIP), dPort);
            RemotePoint = (EndPoint)(ipep);
            mySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            mySocket.Bind(ipLocalPoint);
            runningFlag = true;
            thread = new Thread(new ThreadStart(this.ReceiveHandle));
            thread.IsBackground = true;
            thread.Start();
        }

        public void Send(byte[]sendByte, string dIP, int dPort)
        {
            sendPort = dPort;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(dIP), dPort);
            RemotePoint = (EndPoint)(ipep);
            mySocket.SendTo(sendByte, SocketFlags.None, RemotePoint);
        }
        
        private void ReceiveHandle()
        {
            while (runningFlag)
            {
                try
                {
                    length = mySocket.ReceiveFrom(recv, ref RemotePoint);
                    if (length > 0)
                    {
                        processData(recv);
                    }
                    recv = new byte[1024];
                }
                catch (Exception e)
                {
                }
            }
        }

        private void processData(byte[] data)
        {


            ZCToVobcMessage ZCToVobcMes = mess.UnPack(data);
            UInt64 PackageType = ZCToVobcMes.type;
            Message = ZCToVobcMes.message;
            UInt64 Length = ZCToVobcMes.length;
            Location = ZCToVobcMes.prePosition;
            Cycle = ZCToVobcMes.packageNum;
            ZCT1 = ZCToVobcMes.ZCT1;
            if (PackageType == 1001)//表示ZC发来的数据
            {
                refEvent.Invoke();
                Array.Clear(recv, 0, 1024);
            }


        }


        public void closeThread()
        {
            thread.Abort();
        }


    }
}
