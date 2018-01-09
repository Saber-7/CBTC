using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Timers;

namespace CBTC
{
    public partial class VOBC : Form
    {
        public static string sourceIP = "";
        public static string sourcePort = "";
        public static string desIP = "";
        public static string desPort = "";
        public static string trainNumber = "";
        static UInt64 numberBefore = 0;
        static UInt64 number = 0;
        static UInt64 vobc1Location = 1500;
        LinkStatus linkstatus;
        Thread th;//开线程与ZC通信
        Thread th2;//开线程计算列车运行位置和速度
        HiPerfTimer hip = new HiPerfTimer();
        HiPerfTimer tim = new HiPerfTimer();
        static int I = 0;//用来累计包数
        static int k = 0;
        static int m = 0;
        static double Vi = 0;//列车实时速度，单位km/h
        static double Si = 0;//列车实时位置
        static double Ti = 0;//列车运行实时时间
        static double v = 0;//列车运行实时速度，单位m/s
        static double v0 = 0;//列车初速度
        static double s = 0;//列车加速度变化时的s
        static double t = 0;//列车加速度变化时的t
        static double a = 0;//列车加速度
        static double d = -1.2;//列车最大减速度
        static double Vmax = 70;//列车防护曲线的最大速度
        static double Vp = 0;//列车防护曲线的实时速度
        static double Sp = 0;//制动距离
        static int interval = 5000;//站间停车间隔5s
        static double lineLength = 1500;//站间距离1500m
        static bool btnStopFlag = false;//制动停车标志
        static bool trainRunFlag = true;//列车运行标志
        static bool decelerationSpeedFlag = true;//减速标志,保证列车在减速时只进入一次if
        static bool communicationToZCFlag = true;//ZC通信标志
        static bool reStartFlag = false;//制动后重新启动标志
        static bool breakCommunicationFlag = false;//中断通信标志
        static bool breakFlag = false;//停车标志
        static double realTimeX1, realTimeY1;//列车实时曲线        
        static bool firstFlag = true;
        static bool confirmCommunicationFlag = true;
        static bool stopStatistics = false;
        DateTime recTime = DateTime.Now;
        DateTime firstTime = DateTime.Now;
        static double[] ATPX = new double[9];
        static double[] ATPY = new double[9];
        public static int[] T = new int[9];
        static int UpdateChartDone = 0;
        static double firstPackageNumber = 0;
        static double countPackageNumber = 0;
        static double packageLossNumber = 0;
        static double packageLossRate = 0;
        static bool stationStopFlag = false;
        string logPath = Application.StartupPath + @"\log\" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        Socket socket;
        UInt64 packageNumber = 0;
        int loseNum = 0;
        //DateTime RecT

        private void communicationToZC()
        {
            while (communicationToZCFlag)
            {
                while (confirmCommunicationFlag)
                {
                    if (recTime.AddSeconds(1.6) < DateTime.Now && firstFlag == false)
                    {
                        linkstatus.LinkStateShow(false);
                        breakCommunicationFlag = true;
                    }
                    else
                    {
                        breakCommunicationFlag = false;
                    }
                    if (breakCommunicationFlag == false && stopStatistics == false)
                    {
                        Message mes = new Message();
                        bool isNotlose=true;
                        if (loseNum != 0)
                        {
                            isNotlose = false;
                        }
                        TimeSpan ts;
                        ts=(DateTime.Now - recTime);
                        VobcToZCMessage VobcToZCMes = new VobcToZCMessage
                        {
                            cycle= packageNumber++,
                            length = 400,
                            trainID= Convert.ToUInt64(trainNumber),
                            location= (ulong)Math.Round(Si + k * 1500 + 24000 * m, 0),
                            isNotLosedata=isNotlose,
                            processTime = DateTime.Now-recTime,
                            ZCT1 = socket.ZCT1
                            
                        };
                        byte[] tempBytes = mes.Pack(VobcToZCMes, 400);
                        socket.Send(tempBytes,desIP, Convert.ToInt32(desPort));

                        using (StreamWriter sf = File.AppendText(Application.StartupPath + "\\SendData.txt"))
                        {
                            try
                            {
                                sf.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + VobcToZCMes.cycle);
                            }
                            catch (Exception e)
                            { }
                        };         
                    }                
                    hip.Start();
                    //是否在200毫秒内返回成功                    
                    Thread.Sleep(180);
                    double timeDifference = 0.2 - hip.Duration;
                    if (timeDifference > 0)
                    {
                        hip.Interval(timeDifference * 1000);
                    }
                    else
                    {
                    }
                    if (breakCommunicationFlag == true)
                    {
                        break;
                    }
                }
                //中断通信后进入循环等待，等待着车的再次开启
                while (breakCommunicationFlag)
                {
                    if (breakCommunicationFlag == false)
                    {
                        break;
                    }
                }
            }
        }

        #region 列车运行曲线公式
        private void Run()
        {
            if (UpdateChartDone == 0)
            {
                Sp = -Vmax / 3.6 * Vmax / 3.6 / 2 / d;
                tim.Start();
                while (trainRunFlag)
                {
                    #region 列车运行公式
                    Ti = tim.Duration;
                    Si = s + v0 * (Ti - t) + 0.5 * a * (Ti - t) * (Ti - t);
                    v = v0 + a * (Ti - t);
                    Vi = v * 3.6;
                    #endregion

                    #region 匀速行驶
                    if (Vi >= Vmax - 10)
                    {
                        traAcceleration.Value = 0;
                    }
                    else if ((k % 16 == 7) && Vi >= 20 && breakCommunicationFlag == false)
                    {
                        traAcceleration.Value = 0;
                    }
                    else if ((k % 16 == 15) && Vi >= 20 && breakCommunicationFlag == false)
                    {
                        traAcceleration.Value = 0;
                    }
                    #endregion

                    #region 列车到站前开始减速
                    if (vobc1Location < lineLength * (k + 1) && decelerationSpeedFlag && breakCommunicationFlag == false)
                    {
                        if (Si >= ((vobc1Location % 1500) - Sp - 50))
                        {
                            try
                            {
                                traAcceleration.Value = (int)(-v * v * 100 / 2 / ((vobc1Location % 1500) - Si));
                            }
                            catch
                            { }
                            a = (-v * v / 2 / ((vobc1Location % 1500) - Si));
                            decelerationSpeedFlag = false;
                            breakFlag = true;
                        }
                    }
                    else if (vobc1Location >= lineLength * (k + 1) && decelerationSpeedFlag && breakCommunicationFlag == false)
                    {
                        if (Si >= (lineLength - Sp - 50))
                        {
                            try
                            {
                                traAcceleration.Value = (int)(-v * v * 100 / 2 / (lineLength - Si));
                            }
                            catch
                            { }
                            a = (-v * v / 2 / (lineLength - Si));
                            decelerationSpeedFlag = false;
                            breakFlag = true;
                        }
                    }
                    #endregion

                    #region 判断列车运行速度是否超出ATP防护曲线，若超过，则触发紧急制动
                    if (vobc1Location >= lineLength * (k + 1))
                    {
                        if (Si >= (lineLength - Sp))
                        {
                            Vp = System.Math.Sqrt(Vmax / 3.6 * Vmax / 3.6 + 2 * d * (Si - lineLength + Sp)) * 3.6;
                        }
                        else
                        {
                            Vp = Vmax;
                        }
                    }
                    else
                    {
                        if (Si >= (vobc1Location - Sp))
                        {
                            Vp = System.Math.Sqrt(Vmax / 3.6 * Vmax / 3.6 + 2 * d * (Si - vobc1Location + Sp)) * 3.6;
                        }
                        else
                        {
                            Vp = Vmax;
                        }
                    }
                    if (Vi >= Vp)
                    {
                        try
                        {
                            traAcceleration.Value = -120;
                        }
                        catch
                        {
                        }
                        a = -1.2;
                    }
                    #endregion

                    #region 当连续8个周期未收到通信消息时，紧急停车
                    if (breakCommunicationFlag == true || stopStatistics == true)
                    {
                        traAcceleration.Value = -120;
                        breakFlag = true;
                    }
                    #endregion

                    #region 列车停车后的处理
                    if (v <= 0.1 && breakFlag == true)
                    {
                        v = 0;
                        Vi = 0;
                        traAcceleration.Value = 0;
                        // 手动点击紧急制动停车，点缓解后开车
                        if (btnStopFlag == true)
                        {
                            while(true)
                            {
                                if(btnStopFlag == false)
                                {
                                    break;
                                }
                            }
                        }
                        else if(breakCommunicationFlag==true)
                        {
                            while (true)
                            {
                                if (reStartFlag == true)
                                {
                                    break;
                                }
                            }
                            reStartFlag = false;
                        }
                        else//在站停车，等一个时间间隔在开车
                        {
                            SetupTimerBreak();
                            while (true)
                            {
                                if(stationStopFlag)
                                {                                    
                                    break;
                                }                                
                            }                            
                            chart1.Series["Series1"].Points.Clear();
                            k++;
                            Si = 0;                           
                        }
                        try
                        {
                            traAcceleration.Value = (int)80;
                        }
                        catch
                        { }
                        a = 0.8;
                        if (decelerationSpeedFlag == false)
                        {
                            decelerationSpeedFlag = true;
                        }
                        if (breakFlag == true)
                        {
                            breakFlag = false;
                        }
                        if (stationStopFlag ==true)
                        {
                            stationStopFlag = false;
                        }
                        if (k == 16)
                        {
                            k = 0;
                            m++;
                        }
                        tim.Start();
                    }
                    //Thread.Sleep(100);
                    #endregion

                }
            }

        }
        #endregion

        System.Timers.Timer timerBreak = new System.Timers.Timer(interval);
        private void SetupTimerBreak()
        {
            timerBreak.Elapsed += TimerBreak_Elapsed;
            timerBreak.AutoReset = false;
            timerBreak.Start();
        }
        private void TimerBreak_Elapsed(object sender, ElapsedEventArgs e)
        {
            stationStopFlag = true;
            timerBreak.Stop();
        }

        #region 紧急制动
        private void btnStop_Click(object sender, EventArgs e)
        {
            traAcceleration.Value = -120;
            breakFlag = true;
            btnStopFlag = true;
            breakCommunicationFlag = true;
        }
        #endregion

        #region 缓解
        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (btnStopFlag == true)
            {
                btnStopFlag = false;
            }
            reStartFlag = true;
            breakCommunicationFlag = false;
            firstFlag = true;
            stopStatistics = false;
        }
        #endregion

        #region 列车运行图绘制及刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            Interlocked.Exchange(ref UpdateChartDone, 1);
            int n = (int)(Vmax - 0) / 7;
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart2.Series["Series1"].Points.Clear();
            realTimeX1 = Si;
            realTimeY1 = Vi;
            ATPX[0] = 0;
            ATPY[0] = Vmax;
            if (vobc1Location >= lineLength * (k + 1))
            {
                ATPX[1] = lineLength - Sp;
            }
            else
            {
                ATPX[1] = vobc1Location % 1500 - Sp;
            }
            ATPY[1] = Vmax;
            ATPX[2] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - n) / 3.6 * (Vmax - n) / 3.6) / 2 / (-d);
            ATPY[2] = Vmax - n;
            ATPX[3] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - 2 * n) / 3.6 * (Vmax - 2 * n) / 3.6) / 2 / (-d);
            ATPY[3] = Vmax - 2 * n;
            ATPX[4] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - 3 * n) / 3.6 * (Vmax - 3 * n) / 3.6) / 2 / (-d);
            ATPY[4] = Vmax - 3 * n;
            ATPX[5] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - 4 * n) / 3.6 * (Vmax - 4 * n) / 3.6) / 2 / (-d);
            ATPY[5] = Vmax - 4 * n;
            ATPX[6] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - 5 * n) / 3.6 * (Vmax - 5 * n) / 3.6) / 2 / (-d);
            ATPY[6] = Vmax - 5 * n;
            ATPX[7] = ATPX[1] + (Vmax / 3.6 * Vmax / 3.6 - (Vmax - 6 * n) / 3.6 * (Vmax - 6 * n) / 3.6) / 2 / (-d);
            ATPY[7] = Vmax - 6 * n;
            if (vobc1Location >= lineLength * (k + 1))
            {
                ATPX[8] = lineLength;
            }
            else
            {
                ATPX[8] = vobc1Location % 1500;
            }
            ATPY[8] = 0;
            for (int i = 0; i < 8; i++)
            {
                chart1.Series["Series2"].Points.AddXY(ATPX[i], ATPY[i]);
                chart1.Series["Series3"].Points.AddXY(ATPX[i], ATPY[i] - 5);
            }
            for (int i = 1; i < 9; i++)
            {
                chart2.Series["Series1"].Points.AddXY(i, T[i]);
            }
            chart1.Series["Series2"].Points.AddXY(ATPX[8], ATPY[8]);
            chart1.Series["Series3"].Points.AddXY(ATPX[8], 0);
            chart1.Series["Series1"].Points.AddXY(realTimeX1, realTimeY1);
            Interlocked.Exchange(ref UpdateChartDone, 0);
            float targetDistance = (float)(lineLength - Si);
            if(targetDistance<=0)
            {
                targetDistance = 0;
            }
            a2_TargetDistance1.Distance = targetDistance;
            a2_TargetDistance1.Speed = targetDistance;
            b_AGauge1.CurSpeed = (float)Vi;
            b_AGauge1.LimitSpeed = (float)Vp;
            b_AGauge1.RecommedSpeed = (float)(Vp - 5);
            b_AGauge1.Refresh();
        }
        #endregion

        #region chart控件界面初始化
        private void GraphicInterface()
        {
            chart1.Titles.Add("速度-位置曲线");
            chart1.Titles[0].ForeColor = Color.White;//标题的颜色
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "列车位置（m）";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "列车速度（km/h）";
            chart1.ChartAreas["ChartArea1"].AxisX.LineWidth = 2;//x轴的宽度
            chart1.ChartAreas["ChartArea1"].AxisY.LineWidth = 2;//y轴的宽度            
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = System.Drawing.Color.White;//x轴的颜色
            chart1.ChartAreas["ChartArea1"].AxisY.LineColor = System.Drawing.Color.White;//y轴的颜色           
            chart1.ChartAreas["ChartArea1"].AxisX.TitleForeColor = System.Drawing.Color.White;//x轴标题的颜色
            chart1.ChartAreas["ChartArea1"].AxisY.TitleForeColor = System.Drawing.Color.White;//y轴标题的颜色
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;//x轴刻度的颜色
            chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;//y轴刻度的颜色
            //chart1.ChartAreas["ChartArea1"].AxisX.Interval = 500;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 80;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;//设置X轴不可以进行缩放
            chart1.Series[0].BorderWidth = 3;
            chart1.Series[1].BorderWidth = 3;
            chart1.Series[2].BorderWidth = 3;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[2].Color = Color.Yellow;
            chart2.Titles.Add("丢包-周期曲线");
            chart2.Titles[0].ForeColor = Color.White;//标题的颜色
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "连续丢包的周期数";
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "发生频次";
            chart2.ChartAreas["ChartArea1"].AxisX.LineWidth = 3;//x轴的宽度
            chart2.ChartAreas["ChartArea1"].AxisY.LineWidth = 3;//y轴的宽度
            chart2.ChartAreas["ChartArea1"].AxisX.LineColor = System.Drawing.Color.White;//x轴的颜色
            chart2.ChartAreas["ChartArea1"].AxisY.LineColor = System.Drawing.Color.White;//y轴的颜色
            chart2.ChartAreas["ChartArea1"].AxisX.TitleForeColor = System.Drawing.Color.White;//x轴标题的颜色
            chart2.ChartAreas["ChartArea1"].AxisY.TitleForeColor = System.Drawing.Color.White;//y轴标题的颜色
            chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;//x轴刻度的颜色
            chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;//y轴刻度的颜色
            chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart2.ChartAreas["ChartArea1"].AxisX.Maximum = 9;
            chart2.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        }
        #endregion

        #region 在界面关闭时，关闭线程，保存操作日志        
        private void FrontTrain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                th.Abort(); 
                th2.Abort();
                socket.closeThread();
            }
            catch
            {
            }
            using (StreamWriter sw = File.AppendText(logPath))
            {                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "停止通信");
                if (countPackageNumber != 0)
                {
                    TimeSpan ts = DateTime.Now - firstTime;
                    sw.WriteLine("CBTC—— 包总数:" + countPackageNumber + " 丢包总数:" + packageLossNumber + "丢包率:" + packageLossRate.ToString("P") + " 测试时长:" + ts.ToString());
                }
            };
        }
        #endregion

        #region 加速度变化时变量初始化
        private void traAcceleration_ValueChanged(object sender, EventArgs e)
        {
            a = (double)traAcceleration.Value / 100;
            t = Ti;
            s = Si;
            v0 = v;
            if (v == 0)
            {
                t = 0;
            }
        }
        #endregion

        #region 界面退出
        private void FrontTrain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("确定要退出程序吗？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    socket.closeThread();
                    this.Close();
                }
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //加速度范围设置
            traAcceleration.Minimum = (int)-120;
            traAcceleration.Maximum = (int)120;
            traAcceleration.TickFrequency = (int)30;
            traAcceleration.SmallChange = (int)30;
            traAcceleration.LargeChange = (int)30;
            traAcceleration.Value = 120;//设置初始值
            //表盘，目标距离参数初始化
            b_AGauge1.CurSpeed = 0;
            b_AGauge1.LimitSpeed = 0;
            b_AGauge1.RecommedSpeed = 0;
            a2_TargetDistance1.Distance = 750;
            a2_TargetDistance1.Speed = 0;
            label2.Text = trainNumber;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            socket.Start(sourceIP, Convert.ToInt32(sourcePort), desIP, Convert.ToInt32(desPort));
            GraphicInterface();
            //开线程
            th = new Thread(communicationToZC);
            th.IsBackground = true;
            th.Start();
            th2 = new Thread(Run);
            th2.IsBackground = true;
            th2.Start();
            timer1.Enabled = true;
            linkstatus = new LinkStatus(pictureBoxLinkStatus, false);
        }

        public VOBC()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
            int wid = Screen.PrimaryScreen.WorkingArea.Width;
            int hei = Screen.PrimaryScreen.WorkingArea.Height;
            panel1.Height = 660;
            panel1.Width = 1030;
            panel1.Location = new Point(wid / 2 - 515, hei / 2 - 330);

            socket = new Socket();
            ConfigurationData.ReadConfigData();
            socket.refEvent += new Socket.RefDelegate(recDataHandle);
        }

        public void recDataHandle()
        {
            recTime = DateTime.Now;
            if (socket.Message == 3003)//停止统计
            {
                stopStatistics = true;
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "停止通信");
                    if (countPackageNumber != 0)
                    {
                        TimeSpan ts = DateTime.Now - firstTime;
                        sw.WriteLine("CBTC—— 包总数:" + countPackageNumber + " 丢包总数:" + packageLossNumber + "丢包率:" + packageLossRate.ToString("P") + " 测试时长:" + ts.ToString());
                    }
                };
            }
            if (socket.Message == 2002)
            {
                if (firstFlag == false)
                {
                    if (recTime.AddSeconds(1.6) < DateTime.Now)
                    {
                        linkstatus.LinkStateShow(false);
                        breakCommunicationFlag = true;
                    }
                    else
                    {
                        linkstatus.LinkStateShow(true);//与ZC通信正常
                        breakCommunicationFlag = false;
                    }
                }
                if (firstFlag == true)
                {
                    numberBefore = socket.Cycle;//发来的序号
                    firstPackageNumber = numberBefore;
                    firstTime = DateTime.Now;
                    firstFlag = false;
                }
                
                number = socket.Cycle;
                countPackageNumber = number - firstPackageNumber + 1;
                vobc1Location = socket.Location;
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\ReceiveData.txt"))
                {
                    try
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + number);
                    }
                    catch (Exception e)
                    { }
                };
            }
            for (int i = 2; i < 9; i++)
            {
                if ((int)(number - numberBefore) == i)
                {
                    T[i - 1]++;
                    packageLossNumber += i - 1;
                }
            }
            if (number - numberBefore >= 9)
            {
                T[8]++;
            }
            loseNum =(int)(number - numberBefore - 1);
            numberBefore = number;
            packageLossRate = packageLossNumber / countPackageNumber;
            packLossRate.Text = packageLossRate.ToString("P");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\help.pdf");
        }

    }
}
