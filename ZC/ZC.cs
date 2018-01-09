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
using System.Drawing;


namespace ZC
{
    public partial class ZC : Form
    {
        //存放IP
        //static List<string> IP = new List<string>();
        //public List<string> Content = new List<string>();
       
        Socket socketMain = null;
        byte[] sendControlPacket;
        //operation operation;
        public bool circle = true;
        static float screenK;
        static float formK;
        Communication com1, com2;
        //static Dictionary<string, string> IP_Num = new Dictionary<string, string>();


        bool isMouseDown = false;
        Point currentFormLocation = new Point(); //当前窗体位置
        Point currentMouseOffset = new Point(); //当前鼠标的按下位置

        Bitmap trainBitmap;
        //int x, y;
        List<Train> trainList;
        /*-----------声明委托----------*/
        private delegate void UpdateChartDelegate(int[] lostData);
        private UpdateChartDelegate updateFirstChart, updateSecondChart;
        private delegate void ShowStatus(string comStatus);
        private ShowStatus showFirst, showSecond;
        private delegate void UpdatePercent(float percent,string tempString);
        private UpdatePercent updFirstPercent, updSecondPercent;

        /*-----------声明委托----------*/



        public List<Train> TrainList
        {
            get { return trainList; }
            set { trainList = value; }
        }


        //配置文档路径
        private readonly string configPath = Application.StartupPath + @"\ZC_Config.ini";
        //log记录路径
        string logPath = Application.StartupPath + @"\log\" + DateTime.Today.ToString("yyyyMMdd") + ".txt";

        public ZC()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /*---------定义委托-----------*/
            updateFirstChart += Updatechart1;
            updateSecondChart += Updatechart2;
            showFirst += ShowFirst;
            showSecond += ShowSecond;
            updFirstPercent += firstLostDataPercent;
            updSecondPercent += secondLostDataPercent;
            
            /*---------定义委托-----------*/
            //operation = new operation();
            //sourceIP.Text = operation.GetIP();
    
            trainList = new List<Train>();
      


            /*创建记录文件*/
            try
            {
                if (File.Exists(@logPath))
                {

                }
                else
                {

                    using (FileStream aFile = new FileStream(@logPath, FileMode.Create))
                    {

                    } 
                }
            }
            catch
            { 
            
            }
            /*创建记录文件*/

            
           
            #region 轨道屏幕参数等调整
            //水平比例尺
            screenK = this.Width / (float)1680;
            //竖直比例尺
            formK = this.Height /( float)(1050);

            /*设定控件的相对位置*/

            flowLayoutPanel1.Location = new Point(Convert.ToInt32((panel2.Width - flowLayoutPanel1.Width) / 2f), Convert.ToInt32(trackPicture.Location.Y + trackPicture.Height - 70 * formK));
            chart_textbox_panel1.Location = new Point(Convert.ToInt32((panel2.Width / 2f - chart_textbox_panel1.Width)), Convert.ToInt32(flowLayoutPanel1.Location.Y + flowLayoutPanel1.Height));
            chart_textbox_panel2.Location = new Point(Convert.ToInt32(panel2.Width / 2f), Convert.ToInt32(flowLayoutPanel1.Location.Y + flowLayoutPanel1.Height));
            label6.Location = new Point(Convert.ToInt32((panel2.Width - label6.Width) / 2), trackPicture.Location.Y + 20);
            //label18.Location = new Point(Convert.ToInt32((panel2.Width - label18.Width) / 2), label6.Location.Y - 60);

            /*设定控件的相对位置*/

            /*轨道屏幕参数等调整*/
            //水平半轨长度
            horizontalHalfTrack = 750 * screenK;
            //双向半间距
            gapHalfWidth = 75 * screenK;
            //双向间距
            gapWidth = 150 * screenK;
            //车站分布水平半长
            stationHalfLength = 500 * screenK;
            //车站水平间隔
            stationInterval = 143 * screenK;
            //车站尺寸
            stationSize = 12 * screenK;

            //根据屏幕大小调整行车比例尺

            returnK *= screenK;
            horizontalK *= screenK;

            #endregion

            trackPicture.BackgroundImage = trackDrawing();
          
        }





        #region 读取配置文件
        private void ReadConfigData(String path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("未找到配置文件，ZC将不能运行！");
                    return;
                }
                else
                {
                    //打开文件
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        //获取文件内容，调整格式
                        String line;
                        line = sr.ReadToEnd().Trim();
                        line = line.Replace("\t", " ");
                        //分开每一行
                        String[] strArr = line.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //将行内的数据打断成多个数据
                        string[] strZC1 = System.Text.RegularExpressions.Regex.Split(strArr[1], @"\s+");
                        string[] strVOBC1 = System.Text.RegularExpressions.Regex.Split(strArr[3], @"\s+");//@"\s+"是正则表达式表示多于一次的空格
                        string[] strZC2 = System.Text.RegularExpressions.Regex.Split(strArr[5], @"\s+");
                        string[] strVOBC2 = System.Text.RegularExpressions.Regex.Split(strArr[7], @"\s+");
                        //前车
                        int MainSport;
                        forIP.Text = strVOBC1[2].Trim();
                        forPort.Text = strVOBC1[1].Trim();

                        IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse(strVOBC1[2].Trim()), int.Parse(strVOBC1[1].Trim()));
                        EndPoint EP = (EndPoint)IPEP;
                        trainList[0].EP = EP;

                        //后车
                        beIP.Text = strVOBC2[2].Trim();
                        bePort.Text = strVOBC2[1].Trim();


                        IPEP = new IPEndPoint(IPAddress.Parse(strVOBC2[2].Trim()), int.Parse(strVOBC2[1].Trim()));
                        EP = (EndPoint)IPEP;
                        trainList[1].EP = EP;

                        //ZC_前车
                        sourceIP.Text = strZC1[2].Trim();
                        sourceport.Text = strZC1[1].Trim();

                        MainSport = int.Parse(sourceport.Text);
                        IPEP = new IPEndPoint(IPAddress.Parse(sourceIP.Text), MainSport);
                        EP = (EndPoint)IPEP;
                        socketMain = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        socketMain.Blocking = false;
                        socketMain.Bind(EP);
                        com1.socketMain = socketMain;
                        //ZC_后车
                        sourceIP.Text = strZC2[2].Trim();
                        sourceport.Text = strZC2[1].Trim();
                        MainSport = int.Parse(sourceport.Text);
                        IPEP = new IPEndPoint(IPAddress.Parse(sourceIP.Text), MainSport);
                        EP = (EndPoint)IPEP;
                        socketMain = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        socketMain.Blocking = false;
                        socketMain.Bind(EP);
                        com2.socketMain = socketMain;
                    }
                }
            }
            catch
            {
                MessageBox.Show("配置文件中有错误，请修改，并重新启动！配置文件路径为：" + path);
                System.Environment.Exit(0);
            }
        }
        #endregion
        Thread th;

        private void button1_Click(object sender, EventArgs e)
        {
            //取消修改键和修改的可视化部分
            
            flowLayoutPanel1.Visible = false;
            //显示确认开始选项
            firstMessageBox.BeginInvoke(showFirst, "配置完成，确认开始");
            secondMessageBox.BeginInvoke(showSecond, "配置完成，确认开始");
            #region 启动
            //int MainSport;
            //try
            //{
            //    MainSport = int.Parse(sourceport.Text);
            //    IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse(sourceIP.Text), MainSport);
                
            //    EndPoint EP = (EndPoint)IPEP;
            //    socketMain = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //    socketMain.Blocking = false;
            //    socketMain.Bind(EP);
                
            //}
            //catch
            //{
            //    MessageBox.Show("配置文件中有错误，请修改，并重新启动！配置文件路径为：" + configPath);
            //    System.Environment.Exit(0);
            //}
            //com = new Communication(this);
            //com.hostIPAdress = sourceIP.Text;
            //com.socketMain = socketMain;
            //com.ListenControlData();
            //EndPoint ipsource = (EndPoint)(new IPEndPoint(IPAddress.Parse(forIP.Text), int.Parse(forPort.Text)));
            Train tempTrain = new Train();
            //tempTrain.TrainNum = 1;
            //tempTrain.EP = ipsource;
            tempTrain.TrainP = new Pen(Color.Blue, 8);
            trainList.Add(tempTrain);
            tempTrain = new Train();
            //ipsource = (EndPoint)(new IPEndPoint(IPAddress.Parse(beIP.Text), int.Parse(bePort.Text)));
            //tempTrain.EP = ipsource;
            //tempTrain.TrainNum = 2;
            tempTrain.TrainP = new Pen(Color.Red, 8);
            trainList.Add(tempTrain);
            com1 = new Communication(this);
            com2 = new Communication(this);
            ReadConfigData(configPath);
            com1.ListenControlData();
            com2.ListenControlData();
            #endregion
            /*--------log记录---------*/
            StreamWriter sw = File.AppendText(logPath);
            try
            {
                sw.WriteLine(DateTime.Now+"开始监听");
            }
            catch
            {
               
            }

            sw.Flush();
            sw.Close();
            /*--------log记录---------*/

            th = new Thread(ListenTrain);
            th.IsBackground = true;
            th.Start();
            confirm.Enabled = false;

            /*关闭无关按钮，防止误操作*/
            this.config.Enabled = false;
            this.clear.Enabled=false;

        }
        #region 通信
        //public void send(string message,EndPoint Ep)
        //{
        //    sendControlPacket = new ASCIIEncoding().GetBytes(message);
        //    com.SendControlData(sendControlPacket, Ep);
        //}
        #endregion

        public void send(int trainNum,string message, EndPoint Ep)
        {
            sendControlPacket = new ASCIIEncoding().GetBytes(message);
            if(trainNum==1)
            com1.SendControlData(sendControlPacket, Ep);
            else
                com2.SendControlData(sendControlPacket,Ep);
        }

        public void send(int trainNum, byte[] bytes, EndPoint Ep)
        {
            if (trainNum == 1)
                com1.SendControlData(bytes, Ep);
            else
                com2.SendControlData(bytes, Ep);
        }

        int prePosition;
        public delegate void Delegate(int[] lostData);
        public void ListenTrain()
        {
            while (circle)
            {
                       for (int i = 0; i < trainList.Count; i++)
                       {
                           
                           if (trainList[i].TrainNum != 0)
                           {
                               trainList[i].PacketNum += 1;
                               if (i == 0)
                               {
                                   //实际位置修正 车行驶到1499位置已经算是到达终点了，系统仍然只认1500
                                   prePosition = 1500 * ((trainList[i].Position + 1) / 1500 + 1);
                                   TestCommunicationOutage(0);
                                   if (trainList[0].IsOutageT && trainList[0].IsFirstMessage == false)
                                   {
                                       firstMessageBox.BeginInvoke(showFirst, trainList[0].RecordTime.ToString("yyyy/MM/dd HH:mm:ss") + "前车通信中断");
                                       StreamWriter sw1 = File.AppendText(logPath);
                                       try
                                       {
                                           sw1.WriteLine(trainList[0].RecordTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + "前车通信中断");
                                           TimeSpan ts = DateTime.Now - TrainList[0].BeginTime;
                                           sw1.WriteLine("前车—— 发包总数:" + trainList[0].TotalPacNum + " 丢包总数:" + trainList[0].TotalLost + "丢包率:" + ((float)trainList[0].TotalLost / trainList[0].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString()+" 最大时延:"+trainList[0].MaxReTime.ToString("fff")+"ms");
                                       }
                                       catch
                                       {

                                       }

                                       sw1.Flush();
                                       sw1.Close();
                                       trainList[0].IsFirstMessage = true;
                                   }
                                   //写文件
                                   using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\sendFirstData.txt"))
                                   {
                                       try
                                       {
                                           sw.WriteLine(trainList[i].SendRecTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + " " + trainList[i].PacketNum);
                                       }
                                       catch
                                       {

                                       }
                                   };


                               }
                               else
                               {
                                   prePosition = trainList[i - 1].Position;
                                   TestCommunicationOutage(1);
                                   if (trainList[1].IsOutageT && trainList[1].IsFirstMessage == false)
                                   {
                                       secondMessageBox.BeginInvoke(showSecond, trainList[1].RecordTime.ToString("yyyy/MM/dd HH:mm:ss") + "后车通信中断");
                                       StreamWriter sw1 = File.AppendText(logPath);
                                       try
                                       {
                                           sw1.WriteLine(trainList[1].RecordTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + "后车通信中断");
                                           TimeSpan ts = DateTime.Now - TrainList[1].BeginTime;
                                           sw1.WriteLine("后车—— 发包总数:" + trainList[1].TotalPacNum + " 丢包总数:" + trainList[1].TotalLost + "丢包率:" + ((float)trainList[1].TotalLost / trainList[1].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString() + "最大时延:"+trainList[1].MaxReTime.ToString("fff") + "ms");
                                       }
                                       catch
                                       {
                                           sw1.WriteLine("当前数据格式不对");
                                       }

                                       sw1.Flush();
                                       sw1.Close();
                                       trainList[1].IsFirstMessage = true;
                                   }
                                   using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\sendSecondData.txt"))
                                   {
                                       try
                                       {
                                           sw.WriteLine(trainList[i].SendRecTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + " " + trainList[i].PacketNum);
                                       }
                                       catch
                                       {
                                           sw.WriteLine("当前数据格式不对");
                                       }
                                   }
                               }
                               Response(trainList[i], prePosition);
                           }
                       }
                       HiPerfTimer hip = new HiPerfTimer();
                       hip.Start();
                Thread.Sleep(190);
                double timeDifference = 0.2- hip.Duration;
                if (timeDifference > 0)
                {
                    hip.Interval(timeDifference * 1000);
                }
                else
                {

                }

               
            }
        }


        //public void Response(Train tempTrain,int prePos)
        //{
        //    string tempStr="";
        //    string tempStr1 = "";             
        //    tempStr = "2221010" + tempTrain.PacketNum.ToString().Length + tempTrain.PacketNum.ToString()+prePos.ToString().Length+prePos.ToString();
        //    tempStr1 = tempStr.PadRight(400, 'a');
        //    send(tempTrain.TrainNum,tempStr1, tempTrain.EP);
        //    tempTrain.SendRecTime = DateTime.Now;
        //}

        public void Response(Train tempTrain, int prePos)
        {
            Message mes=new Message();
            ZCToVobcMessage ZcToVobcMes=new ZCToVobcMessage
                {
                    type=1001,
                    message=2002,
                    length=400,
                    prePosition=(UInt64)prePos,
                    packageNum=(UInt64)tempTrain.PacketNum,
                    ZCT1=DateTime.Now
                };
            byte[] tempBytes=mes.Pack(ZcToVobcMes,400);
            send(tempTrain.TrainNum,tempBytes , tempTrain.EP);
            tempTrain.SendRecTime = DateTime.Now;
        }


        private void button2_Click(object sender, EventArgs e)
        {
          

            for (int i = 0; i < trainList.Count; i++)
            {
                ZCToVobcMessage ZcToVobcMes = new ZCToVobcMessage
                {
                    type = 1001,
                    message = 3003,
                    length = 400,
                    packageNum = (UInt64)trainList[i].PacketNum
                };
                Message mes = new Message();
                byte[] tempBytes = mes.Pack(ZcToVobcMes, 400);
                trainList[i].PacketNum++;
                send(i + 1, tempBytes, trainList[i].EP);
            }
            th.Abort();
            using(StreamWriter sw=File.AppendText(logPath))
            {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")+"停止监听");
                    if (TrainList[0].TotalPacNum != 0)
                    {
                        TimeSpan ts = DateTime.Now - TrainList[0].BeginTime;
                        sw.WriteLine("前车—— 发包总数:" + trainList[0].TotalPacNum + " 丢包总数:" + trainList[0].TotalLost + "丢包率:" + ((float)trainList[0].TotalLost / trainList[0].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString()+" 最大时延:"+trainList[0].MaxReTime.ToString("fff")+"ms");
                    }
                    if (TrainList[1].TotalPacNum != 0)
                    {
                        TimeSpan ts = DateTime.Now - TrainList[1].BeginTime;
                        sw.WriteLine("后车—— 发包总数:" + trainList[1].TotalPacNum + " 丢包总数:" + trainList[1].TotalLost + "丢包率:" + ((float)trainList[1].TotalLost / trainList[1].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString()+" 最大时延:"+trainList[1].MaxReTime.ToString("fff")+"ms");
                    } 
            };

         

            //显示停止统计
            firstMessageBox.BeginInvoke(showFirst, DateTime.Now.ToString("yyyy/MM/dd hh:mm.fff")+"停止统计");
            secondMessageBox.BeginInvoke(showSecond,  DateTime.Now.ToString("yyyy/MM/dd hh:mm.fff")+"停止统计");

            stopTrain.Enabled = false;

        }

     

        #region 轨道


        int midX, midY;

        #region 轨道参数 单位pixel
        //水平半轨长度750
        float horizontalHalfTrack = 750 ;
        //双向半间距
        float gapHalfWidth = 75;
        //双向间距
        float gapWidth = 150;
        //车站分布水平半长
        float stationHalfLength = 500;
        //车站水平间隔
        float stationInterval = 143;
        //车站尺寸
        float stationSize = 12;



        #endregion
        public Bitmap trackDrawing()
        {
            Bitmap bgImage;


            try
            {
                 bgImage= new Bitmap(trackPicture.ClientSize.Width, trackPicture.ClientSize.Height);
            }
            catch
            { 
            bgImage=new Bitmap(1500,1000);
            }

            //寻找画图中点
            midX = trackPicture.ClientSize.Width / 2;
            midY = trackPicture.ClientSize.Height / 2;
           


            //构建画笔
            Graphics bgg = Graphics.FromImage(bgImage);



            Pen p1 = new Pen(Color.Green, 5);
            //水平轨道1500
            bgg.DrawLine(p1, new PointF(midX-horizontalHalfTrack,midY-gapHalfWidth), new PointF(midX+horizontalHalfTrack, midY-gapHalfWidth));
            bgg.DrawLine(p1, new PointF(midX-horizontalHalfTrack, midY+gapHalfWidth),new PointF(midX+horizontalHalfTrack, midY+gapHalfWidth));

            //道岔部分
            bgg.DrawLine(p1, new PointF(midX - horizontalHalfTrack , midY- gapHalfWidth), new PointF(midX- horizontalHalfTrack+ gapWidth,midY+gapHalfWidth));
            bgg.DrawLine(p1, new PointF(midX - horizontalHalfTrack, midY+gapHalfWidth), new PointF(midX - horizontalHalfTrack + gapWidth, midY -gapHalfWidth));
            bgg.DrawLine(p1, new PointF(midX +horizontalHalfTrack, midY - gapHalfWidth), new PointF(midX +horizontalHalfTrack -gapWidth, midY + gapHalfWidth));
            bgg.DrawLine(p1, new PointF(midX +horizontalHalfTrack, midY + gapHalfWidth), new PointF(midX +horizontalHalfTrack -gapWidth, midY - gapHalfWidth));

            //站名数组
            string[] stationName = { "燕化", "老城区北", "饶乐府", "顾八路", "星城", "阎村", "大紫草坞", "阎村北" };
            Font font = new Font("宋体",12f);
            for(int i=0;i<stationName.Length;i++)
            {
            //画车站

                bgg.FillRectangle(Brushes.White, midX - stationHalfLength + i * stationInterval - stationSize / 2, midY - gapHalfWidth - stationSize / 2, stationSize, stationSize);
                bgg.FillRectangle(Brushes.White, midX - stationHalfLength + i * stationInterval - stationSize / 2, midY + gapHalfWidth - stationSize / 2, stationSize, stationSize); 

            //标注站名
                float fontOffst = stationSize ;
                bgg.FillRectangle(Brushes.Black, new RectangleF(new PointF(midX - stationHalfLength + i * stationInterval-fontOffst, midY), bgg.MeasureString(stationName[i], font)));
                bgg.DrawString(stationName[i], font, Brushes.White, new PointF(midX -stationHalfLength -3+ i * stationInterval-fontOffst, midY));
            }

            //trainPicturePosition.X = beginPosition.X + 2 * stationHalfLength + 100 - (trainActuralPosition - returnPos2) * returnK;
            //trainPicturePosition.Y = midY + gapHalfWidth;
            //PointF poi1=new Point(midX+stationHalfLength+100,midY+75);
            //bgg.FillEllipse(Brushes.Red,poi1.X-5,poi1.Y-5, 10, 10);
            return bgImage;


        }
        #endregion

        #region 画车

        /*画车参数*/
        //列车的实际位置
        int trainActuralPosition;
        //水平比例尺
        float horizontalK = 1000f/10500f;
        //折返段比例尺
        float returnK = (350 + 150 * 1.414f) / 1500f;
        //半车长度
        float trainHalfLength = 20;
        //折返区间内的实际折点1,2
        float returnPos1 = 10500 + 1500 * 250 / (350 + 150 * 1.414f), returnPos2 = 10500 + 1500 * (250 + 150 * 1.414f) / (350 + 150 * 1.414f);
        //折返时的部分坐标
        float x1, x2, y1, y2;
        /*画车参数*/

        float sin45 =1/1.414f, cos45 =1/1.414f;

        //对称偏移
        int offset = 12000;



        public void TPShow()
        {

        
            //开车位置确定
            PointF beginPosition = new PointF(midX - stationHalfLength, midY - gapHalfWidth);
            PointF trainPicturePosition = beginPosition;
             


              

              
            this.Invoke((EventHandler)delegate
            {
                 //更新车位置
                trackPicture.Refresh();

                //绘制车
                try
                {
                    trainBitmap = new Bitmap(trackPicture.ClientSize.Width, trackPicture.ClientSize.Height);
                }
                catch
                {
                    trainBitmap = new Bitmap(1600, 1500);
                }


                Graphics a = Graphics.FromImage(trainBitmap);
                for (int i = 0; i < trainList.Count; i++)
                {
                    trainActuralPosition = trainList[i].Position % 24000;
                    if (trainActuralPosition >= 0 && trainActuralPosition < 10500)
                    {
                        trainPicturePosition.X = beginPosition.X + trainActuralPosition * horizontalK;
                        trainPicturePosition.Y = beginPosition.Y;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X +trainHalfLength, trainPicturePosition.Y));
                    }
                    else if (trainActuralPosition >= 10500 && trainActuralPosition < (returnPos1 - trainHalfLength / returnK) )
                    {
                        trainPicturePosition.X = beginPosition.X + 2 * stationHalfLength + (trainActuralPosition - 10500) * returnK;
                        trainPicturePosition.Y = beginPosition.Y;

                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X + trainHalfLength, trainPicturePosition.Y));
                    }
                    else if (returnPos1 - trainHalfLength / returnK <= trainActuralPosition && trainActuralPosition < returnPos1 + trainHalfLength / returnK)
                    {


                        x1 = beginPosition.X + 2 * stationHalfLength + (trainActuralPosition - trainHalfLength / returnK - 10500) * returnK;
                        y1 = beginPosition.Y;
                        a.DrawLine(trainList[i].TrainP, new PointF(x1, y1), new PointF(midX + horizontalHalfTrack, y1));
                        x2 = beginPosition.X + stationHalfLength+ horizontalHalfTrack - ((trainActuralPosition + trainHalfLength / returnK - returnPos1) * returnK) * cos45;
                        y2 = beginPosition.Y + ((trainActuralPosition + trainHalfLength / returnK - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(x2, y2), new PointF(midX + horizontalHalfTrack, y1));




                    }
                    else if (trainActuralPosition >= returnPos1 + trainHalfLength / returnK && trainActuralPosition < returnPos2 - trainHalfLength / returnK)
                    {
                        trainPicturePosition.X = beginPosition.X +  stationHalfLength+ horizontalHalfTrack - (trainActuralPosition - returnPos1) * returnK * cos45;
                        trainPicturePosition.Y = beginPosition.Y + ((trainActuralPosition - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength * cos45, trainPicturePosition.Y + trainHalfLength * cos45), new PointF(trainPicturePosition.X + trainHalfLength * cos45, trainPicturePosition.Y - trainHalfLength * sin45));
                    }
                    else if (trainActuralPosition >= returnPos2 - trainHalfLength / returnK && trainActuralPosition < returnPos2 + trainHalfLength / returnK)
                    {
                        x1 = beginPosition.X +  stationHalfLength+ horizontalHalfTrack - (trainActuralPosition - trainHalfLength / returnK - returnPos1) * returnK * cos45;
                        y1 = beginPosition.Y + ((trainActuralPosition - trainHalfLength / returnK - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(x1, y1), new PointF(midX + horizontalHalfTrack-150*screenK, midY + gapHalfWidth));
                        x2 = beginPosition.X + 2 * stationHalfLength + 100 *screenK- (trainActuralPosition - returnPos2 + trainHalfLength / returnK) * returnK;
                        y2 = midY + gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(x2, y2), new PointF(midX + horizontalHalfTrack-150*screenK, y2));

                    }
                    else if (trainActuralPosition >= returnPos2 + trainHalfLength / returnK && trainActuralPosition < 12000)
                    {
                        trainPicturePosition.X = beginPosition.X +2*stationHalfLength+100*screenK- (trainActuralPosition - returnPos2) * returnK;
                        trainPicturePosition.Y = midY + gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X + trainHalfLength, trainPicturePosition.Y));
                    }

                    //下半段
                    if (trainActuralPosition >= 12000 && trainActuralPosition < 10500+offset)
                    {
                        trainActuralPosition -= offset;
                        trainPicturePosition.X = beginPosition.X +2*stationHalfLength-trainActuralPosition * horizontalK;
                        trainPicturePosition.Y = midY+gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X + trainHalfLength, trainPicturePosition.Y));
                    }
                    else if (trainActuralPosition >= 22500 && trainActuralPosition-offset < (returnPos1 - trainHalfLength / returnK))
                    {
                        trainActuralPosition -= offset;
                        trainPicturePosition.X = beginPosition.X -(trainActuralPosition - 10500) * returnK;
                        trainPicturePosition.Y = midY+gapHalfWidth;

                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X + trainHalfLength, trainPicturePosition.Y));
                    }
                    else if (returnPos1 - trainHalfLength / returnK <= trainActuralPosition-offset && trainActuralPosition-offset < returnPos1 + trainHalfLength / returnK)
                    {
                        trainActuralPosition -= offset;
                        x1 = beginPosition.X - (trainActuralPosition - trainHalfLength / returnK - 10500) * returnK;
                        y1 = midY+gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(x1, y1), new PointF(midX - horizontalHalfTrack, y1));
                        x2 = midX -horizontalHalfTrack+  ((trainActuralPosition + trainHalfLength / returnK - returnPos1) * returnK) * cos45;
                        y2 = midY+gapHalfWidth - ((trainActuralPosition + trainHalfLength / returnK - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(x2, y2), new PointF(midX - horizontalHalfTrack, y1));




                    }
                    else if (trainActuralPosition -offset>= returnPos1 + trainHalfLength / returnK && trainActuralPosition-offset < returnPos2 - trainHalfLength / returnK)
                    {
                        trainActuralPosition -= offset;
                        trainPicturePosition.X = midX -horizontalHalfTrack +(trainActuralPosition - returnPos1) * returnK * cos45;
                        trainPicturePosition.Y = midY+gapHalfWidth - ((trainActuralPosition - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength * cos45, trainPicturePosition.Y + trainHalfLength * cos45), new PointF(trainPicturePosition.X + trainHalfLength * cos45, trainPicturePosition.Y - trainHalfLength * sin45));
                    }
                    else if (trainActuralPosition -offset>= returnPos2 - trainHalfLength / returnK && trainActuralPosition-offset < returnPos2 + trainHalfLength / returnK)
                    {
                        trainActuralPosition -= offset;
                        x1 = midX - horizontalHalfTrack + (trainActuralPosition - trainHalfLength / returnK - returnPos1) * returnK * cos45;
                        y1 = midY+gapHalfWidth - ((trainActuralPosition - trainHalfLength / returnK - returnPos1) * returnK) * sin45;
                        a.DrawLine(trainList[i].TrainP, new PointF(x1, y1), new PointF(midX -horizontalHalfTrack +150*screenK, midY -gapHalfWidth));
                        x2 = midX-horizontalHalfTrack +150*screenK + (trainActuralPosition - returnPos2 + trainHalfLength / returnK) * returnK;
                        y2 = midY - gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(x2, y2), new PointF(midX - horizontalHalfTrack + 150*screenK, midY - gapHalfWidth));

                    }
                    else if (trainActuralPosition-offset >= returnPos2 + trainHalfLength / returnK && trainActuralPosition-offset < 12000)
                    {
                        trainActuralPosition -= offset;
                        trainPicturePosition.X = midX-horizontalHalfTrack+150*screenK + (trainActuralPosition - returnPos2) * returnK;
                        trainPicturePosition.Y = midY-gapHalfWidth;
                        a.DrawLine(trainList[i].TrainP, new PointF(trainPicturePosition.X - trainHalfLength, trainPicturePosition.Y), new PointF(trainPicturePosition.X + trainHalfLength, trainPicturePosition.Y));
                    }
                }
                trackPicture.Image = trainBitmap;

            });
        }
        #endregion


        #region 未全屏时拖动窗体的方法
        private void ZC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                currentFormLocation = this.Location;
                currentMouseOffset = Control.MousePosition;
            }
        }

        private void ZC_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void ZC_MouseMove(object sender, MouseEventArgs e)
        {
            int rangeX = 0, rangeY = 0; //计算当前鼠标光标的位移，让窗体进行相同大小的位移
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                rangeX = currentMouseOffset.X - pt.X;
                rangeY = currentMouseOffset.Y - pt.Y;
                this.Location = new Point(currentFormLocation.X - rangeX, currentFormLocation.Y - rangeY);
            }
        }


        #endregion


        private void ZC_FormClosed(object sender, FormClosedEventArgs e)
        {



            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }
                else
                {

                }
            }

            using (StreamWriter sw = File.AppendText(logPath))
            {
                   try
                {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "停止监听");
                if (TrainList[0].TotalPacNum != 0)
                {
                    TimeSpan ts = DateTime.Now - TrainList[0].BeginTime;
                    sw.WriteLine("前车—— 发包总数:" + trainList[0].TotalPacNum + " 丢包总数:" + trainList[0].TotalLost + "丢包率:" + ((float)trainList[0].TotalLost / trainList[0].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString() + " 最大时延:" + trainList[0].MaxReTime.ToString("fff") + "ms");
                }
                if (TrainList[1].TotalPacNum != 0)
                {
                    TimeSpan ts = DateTime.Now - TrainList[1].BeginTime;
                    sw.WriteLine("后车—— 发包总数:" + trainList[1].TotalPacNum + " 丢包总数:" + trainList[1].TotalLost + "丢包率:" + ((float)trainList[1].TotalLost / trainList[1].TotalPacNum).ToString("P") + " 测试时长:" + ts.ToString() + " 最大时延:" + trainList[1].MaxReTime.ToString("fff") + "ms");
                }

                }
                   catch
                   {

                   }
            };  
        }

        #region 统计图更新

        //前车丢包图表更新
        public void Updatechart1(int[] firstLose)
        {
            firstChart.Series["前车丢包"].Points.Clear();
            for (int i = 0; i < 8; i++)
                firstChart.Series["前车丢包"].Points.AddXY(i + 1, firstLose[i]);
        }

        //后车丢包图表更新
        public void Updatechart2(int[] secondLose)
        {
            secondChart.Series["后车丢包"].Points.Clear();
            for (int i = 0; i < 8; i++)
                secondChart.Series["后车丢包"].Points.AddXY(i + 1, secondLose[i]);
        }
        #endregion

        #region 消息框更新
        public void ShowFirst(string message)
        {
            firstMessageBox.Text = message;
        }
        public void ShowSecond(string message)
        {
            secondMessageBox.Text = message;
        }
        #endregion
        //丢包统计
        int[] firstLose = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] secondLose = { 0, 0, 0, 0, 0, 0, 0, 0 };
        //当前序号&上次序号
        UInt64[] firstNum = { 0, 0 };
        UInt64[] secondNum = { 0,0};
        //更新计数，降低图表的率刷新频率
        int staNum;
        public void classifyTrain(byte[] bytes)
        {
            staNum++;
            //string tempStr;
            //int orderNum, poNum;
            //tempStr = string.Copy(_st);
            //int trainNum = Convert.ToInt32(tempStr.Substring(1, 1));
            //数据分类

            Message mes = new Message();
            DateTime tempDT = DateTime.Now;
            TimeSpan reponseT = TimeSpan.MinValue;
            VobcToZCMessage VobcToZcMes = mes.UnPack(bytes);
            TrainList[(int)(VobcToZcMes.trainID - 1)].Position = (int)VobcToZcMes.location;
            //TrainList[(int)(VobcToZcMes.trainID - 1)].PacketNum = (int)VobcToZcMes.cycle;
            //TrainList[trainNum - 1].Str = String.Copy(tempStr);
            //orderNum = Convert.ToInt32(tempStr.Substring(4, 1));
            //int packetNum = Convert.ToInt32(tempStr.Substring(5, orderNum));
            //poNum = Convert.ToInt32(tempStr.Substring(4 + orderNum + 1, 1));
            //TrainList[trainNum - 1].Position = Convert.ToInt32(tempStr.Substring(4 + 1 + orderNum + 1, poNum));
            //loseDataSta(trainNum, packetNum);
            if (trainList[(int)(VobcToZcMes.trainID - 1)].PacketNum >= 3)
            {
                if (loseDataSta((int)(VobcToZcMes.trainID), VobcToZcMes.cycle) == false && VobcToZcMes.isNotLosedata)
                {
                    reponseT = tempDT - VobcToZcMes.ZCT1 - VobcToZcMes.processTime;
                }

                trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime =
                    trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime > reponseT ? trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime : reponseT;
            }
            trainList[(int)(VobcToZcMes.trainID - 1)].TrainNum = (int)VobcToZcMes.trainID;
            trainList[(int)(VobcToZcMes.trainID - 1)].RecordTime = DateTime.Now;
            if (staNum%5==1)
            {
                TPShow();
            }

            if ((int)(VobcToZcMes.trainID) == 1)
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\receiveFirstData.txt"))
                {
                    try
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + firstNum[1] + " " + trainList[0].Position);
                    }
                    catch
                    {
                        sw.WriteLine("当前数据格式不对");
                    }
                };


            }
            else
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\receiveSecondData.txt"))
                {
                try
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") +" "+secondNum[1]+" "+trainList[1].Position);
                }
                catch
                {
                    sw.WriteLine("当前数据格式不对");
                }
                };
            }


        }

        public void classifyTrain(byte[] bytes,DateTime tempDt)
        {
            staNum++;
            //string tempStr;
            //int orderNum, poNum;
            //tempStr = string.Copy(_st);
            //int trainNum = Convert.ToInt32(tempStr.Substring(1, 1));
            //数据分类

            Message mes = new Message();
            //DateTime tempDT = DateTime.Now;
            TimeSpan reponseT = TimeSpan.MinValue;
            VobcToZCMessage VobcToZcMes = mes.UnPack(bytes);
            TrainList[(int)(VobcToZcMes.trainID - 1)].Position = (int)VobcToZcMes.location;
            //TrainList[(int)(VobcToZcMes.trainID - 1)].PacketNum = (int)VobcToZcMes.cycle;
            //TrainList[trainNum - 1].Str = String.Copy(tempStr);
            //orderNum = Convert.ToInt32(tempStr.Substring(4, 1));
            //int packetNum = Convert.ToInt32(tempStr.Substring(5, orderNum));
            //poNum = Convert.ToInt32(tempStr.Substring(4 + orderNum + 1, 1));
            //TrainList[trainNum - 1].Position = Convert.ToInt32(tempStr.Substring(4 + 1 + orderNum + 1, poNum));
            //loseDataSta(trainNum, packetNum);
            if (trainList[(int)(VobcToZcMes.trainID - 1)].PacketNum >= 3)
            {
                if (loseDataSta((int)(VobcToZcMes.trainID), VobcToZcMes.cycle) == false && VobcToZcMes.isNotLosedata)
                {
                    reponseT = tempDt - VobcToZcMes.ZCT1 - VobcToZcMes.processTime;
                }

                trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime =
                    trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime > reponseT ? trainList[(int)(VobcToZcMes.trainID - 1)].MaxReTime : reponseT;
            }
            trainList[(int)(VobcToZcMes.trainID - 1)].TrainNum = (int)VobcToZcMes.trainID;
            trainList[(int)(VobcToZcMes.trainID - 1)].RecordTime = DateTime.Now;
            if (staNum % 5 == 1)
            {
                TPShow();
            }

            if ((int)(VobcToZcMes.trainID) == 1)
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\receiveFirstData.txt"))
                {
                    try
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + firstNum[1] + " " + trainList[0].Position);
                    }
                    catch
                    {
                        sw.WriteLine("当前数据格式不对");
                    }
                };


            }
            else
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\receiveSecondData.txt"))
                {
                    try
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + secondNum[1] + " " + trainList[1].Position);
                    }
                    catch
                    {
                        sw.WriteLine("当前数据格式不对");
                    }
                };
            }


        }



   

        int staNum1, staNum2;
        public bool loseDataSta(int trainNum,UInt64 orderNum)
        {
            UInt64 loseNum;
            switch (trainNum)
            {
                case 1:
                    {
                        staNum1++;
                        if (trainList[trainNum - 1].IsFirstMessage == true)
                        {
                            firstNum[0] = orderNum - 1;
                            trainList[trainNum - 1].IsFirstMessage = false;
                            trainList[trainNum - 1].BeginTime = DateTime.Now;
                            TrainList[trainNum - 1].BeginNum = orderNum;
                            StreamWriter sw = File.AppendText(logPath);
                            try
                            {
                                
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "前车开始通信");

                            }
                            catch
                            {

                            }
                            
                   

                            sw.Flush();
                            sw.Close();
                            firstMessageBox.BeginInvoke(showFirst, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "前车开始通信");

                        }
                        else
                        {
                            firstNum[0] = firstNum[1];
                        }
                        firstNum[1] = orderNum;
                        loseNum = firstNum[1] - firstNum[0]-1;
                        if (loseNum > 0 && loseNum < 9)
                        {
                            firstLose[loseNum - 1]++;
                            
                        }
                        else if (loseNum > 10)
                            {
                                firstLose[7]++;
                            }
                        trainList[trainNum - 1].TotalLost += loseNum;
                        TrainList[trainNum - 1].TotalPacNum = orderNum - TrainList[trainNum - 1].BeginNum+1;
                        if (staNum1 % 5 == 0)
                        {
                           firstChart.BeginInvoke(updateFirstChart,firstLose);
                           if (TrainList[trainNum - 1].TotalPacNum!=0)
                               firstLostDataPercentTB.BeginInvoke(updFirstPercent, (float)trainList[trainNum - 1].TotalLost / (TrainList[trainNum - 1].TotalPacNum), trainList[trainNum - 1].MaxReTime.ToString("fff")+"ms");
                        }
                        if (loseNum <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                case 2:
                    {
                        staNum2++;
                      
                        if (trainList[trainNum - 1].IsFirstMessage == true)
                        {
                            secondNum[0] = orderNum - 1;
                            trainList[trainNum - 1].IsFirstMessage = false;
                            trainList[trainNum - 1].BeginTime = DateTime.Now;
                            TrainList[trainNum - 1].BeginNum = orderNum;
                            StreamWriter sw = File.AppendText(logPath);
                            try
                            {
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff")  +"后车开始通信");
                            }
                            catch
                            {

                            }

                            sw.Flush();
                            sw.Close();
                            secondMessageBox.BeginInvoke(showSecond, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + "后车开始通信");
                        }
                        else
                        {
                            secondNum[0] = secondNum[1];
                        }
                        secondNum[1] = orderNum;
                        loseNum = secondNum[1] - secondNum[0] - 1;
                        if (loseNum > 0 && loseNum < 9)
                        {
                            secondLose[loseNum - 1]++;
                        }
                        else if (loseNum > 10)
                        {
                            secondLose[7]++;
                        }
                        trainList[trainNum - 1].TotalLost += loseNum;
                        TrainList[trainNum - 1].TotalPacNum = orderNum - TrainList[trainNum - 1].BeginNum+1;
                        if (staNum2 % 5 == 0)
                        {
                            secondChart.BeginInvoke(updateSecondChart, secondLose);

                            if (TrainList[trainNum - 1].TotalPacNum!=0)
                                secondLostDataPercentTB.BeginInvoke(updSecondPercent, (float)trainList[trainNum - 1].TotalLost / TrainList[trainNum - 1].TotalPacNum,trainList[trainNum - 1].MaxReTime.ToString("fff") + "ms");
                        } 
                        if (loseNum <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
 
                    }
                default: return true; 
            }
        }



        public void TestCommunicationOutage(int trainNum)
        {
            if (trainList[trainNum].RecordTime.AddSeconds(1.6) < DateTime.Now)
            {
                trainList[trainNum].IsOutageT = true;
                
            }
            else
            {
                trainList[trainNum].IsOutageT = false;
            }
        }
    
        private void changeIP_Port_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
        }

        public void firstLostDataPercent(float percent,string ResponseTime)
        {
            firstLostDataPercentTB.Text = "前车丢包率：" + percent.ToString("P")+" 最大时延:"+ResponseTime;
        }
        public void secondLostDataPercent(float percent, string ResponseTime)
        {
            secondLostDataPercentTB.Text = "后车丢包率：" + percent.ToString("P") + " 最大时延:" + ResponseTime;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\help.pdf");
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\ZC_Config.ini");
        }

        string[] textPath =
                          {
                          Application.StartupPath+@"\receiveFirstData.txt",
                          Application.StartupPath+@"\receiveSecondData.txt",
                          Application.StartupPath+@"\sendFirstData.txt",
                          Application.StartupPath+@"\sendSecondData.txt"
                          };


        private void clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textPath.Length; i++)
            {
                if (File.Exists(textPath[i]))
                {
                    try
                    {
                        File.Delete(textPath[i]);

                    }
                    catch (Exception deleteE)
                    {

                    }
                }
            }

        }










    }
}


