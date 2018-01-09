namespace ZC
{
    partial class ZC
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.trackPicture = new System.Windows.Forms.PictureBox();
            this.confirm = new System.Windows.Forms.Button();
            this.stopTrain = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sourceIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sourceport = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.forIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.forPort = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.后车IP = new System.Windows.Forms.Label();
            this.beIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bePort = new System.Windows.Forms.TextBox();
            this.chart_textbox_panel1 = new System.Windows.Forms.Panel();
            this.firstLostDataPercentTB = new System.Windows.Forms.TextBox();
            this.firstChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.firstMessageBox = new System.Windows.Forms.TextBox();
            this.chart_textbox_panel2 = new System.Windows.Forms.Panel();
            this.secondLostDataPercentTB = new System.Windows.Forms.TextBox();
            this.secondChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.secondMessageBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helper = new System.Windows.Forms.Button();
            this.config = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.chart_textbox_panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstChart)).BeginInit();
            this.chart_textbox_panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondChart)).BeginInit();
            this.SuspendLayout();
            // 
            // trackPicture
            // 
            this.trackPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackPicture.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.trackPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.trackPicture.Location = new System.Drawing.Point(-3, 79);
            this.trackPicture.Name = "trackPicture";
            this.trackPicture.Size = new System.Drawing.Size(1264, 299);
            this.trackPicture.TabIndex = 0;
            this.trackPicture.TabStop = false;
            // 
            // confirm
            // 
            this.confirm.BackColor = System.Drawing.Color.Black;
            this.confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.confirm.FlatAppearance.BorderSize = 0;
            this.confirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.confirm.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.confirm.Location = new System.Drawing.Point(43, 8);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 10;
            this.confirm.Text = "确定开始";
            this.toolTip1.SetToolTip(this.confirm, "配置完成开始统计");
            this.confirm.UseVisualStyleBackColor = false;
            this.confirm.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopTrain
            // 
            this.stopTrain.BackColor = System.Drawing.SystemColors.Desktop;
            this.stopTrain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopTrain.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.stopTrain.FlatAppearance.BorderSize = 0;
            this.stopTrain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stopTrain.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.stopTrain.Location = new System.Drawing.Point(124, 8);
            this.stopTrain.Name = "stopTrain";
            this.stopTrain.Size = new System.Drawing.Size(75, 23);
            this.stopTrain.TabIndex = 12;
            this.stopTrain.Text = "停止统计";
            this.toolTip1.SetToolTip(this.stopTrain, "停止发送数据,输出测试结果");
            this.stopTrain.UseVisualStyleBackColor = false;
            this.stopTrain.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(546, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 51;
            this.label6.Text = "列车实时运行图";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.chart_textbox_panel1);
            this.panel2.Controls.Add(this.chart_textbox_panel2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.trackPicture);
            this.panel2.Location = new System.Drawing.Point(57, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1268, 726);
            this.panel2.TabIndex = 55;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(224, 279);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(840, 58);
            this.flowLayoutPanel1.TabIndex = 59;
            this.flowLayoutPanel1.Visible = false;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sourceIP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sourceport);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 38);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZC";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(11, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "本机IP";
            // 
            // sourceIP
            // 
            this.sourceIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sourceIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sourceIP.ForeColor = System.Drawing.SystemColors.Window;
            this.sourceIP.Location = new System.Drawing.Point(58, 18);
            this.sourceIP.Name = "sourceIP";
            this.sourceIP.Size = new System.Drawing.Size(100, 14);
            this.sourceIP.TabIndex = 8;
            this.sourceIP.Text = "192.168.111.111";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "本机端口";
            // 
            // sourceport
            // 
            this.sourceport.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.sourceport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sourceport.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.sourceport.Location = new System.Drawing.Point(223, 17);
            this.sourceport.Name = "sourceport";
            this.sourceport.Size = new System.Drawing.Size(30, 14);
            this.sourceport.TabIndex = 7;
            this.sourceport.Text = "10000";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.forIP);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.forPort);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(280, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 38);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "前车";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "前车IP";
            // 
            // forIP
            // 
            this.forIP.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.forIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.forIP.ForeColor = System.Drawing.SystemColors.Menu;
            this.forIP.Location = new System.Drawing.Point(58, 17);
            this.forIP.Name = "forIP";
            this.forIP.Size = new System.Drawing.Size(100, 14);
            this.forIP.TabIndex = 5;
            this.forIP.Text = "192.168.1.21";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "前车端口";
            // 
            // forPort
            // 
            this.forPort.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.forPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.forPort.ForeColor = System.Drawing.SystemColors.Menu;
            this.forPort.Location = new System.Drawing.Point(223, 17);
            this.forPort.Name = "forPort";
            this.forPort.Size = new System.Drawing.Size(32, 14);
            this.forPort.TabIndex = 6;
            this.forPort.Text = "5001";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.后车IP);
            this.groupBox3.Controls.Add(this.beIP);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.bePort);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox3.Location = new System.Drawing.Point(557, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 38);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "后车";
            // 
            // 后车IP
            // 
            this.后车IP.AutoSize = true;
            this.后车IP.Location = new System.Drawing.Point(6, 17);
            this.后车IP.Name = "后车IP";
            this.后车IP.Size = new System.Drawing.Size(41, 12);
            this.后车IP.TabIndex = 14;
            this.后车IP.Text = "后车IP";
            // 
            // beIP
            // 
            this.beIP.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.beIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.beIP.ForeColor = System.Drawing.SystemColors.Menu;
            this.beIP.Location = new System.Drawing.Point(53, 18);
            this.beIP.Name = "beIP";
            this.beIP.Size = new System.Drawing.Size(100, 14);
            this.beIP.TabIndex = 13;
            this.beIP.Text = "192.168.1.21";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "后车端口";
            // 
            // bePort
            // 
            this.bePort.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.bePort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bePort.ForeColor = System.Drawing.SystemColors.Menu;
            this.bePort.Location = new System.Drawing.Point(209, 18);
            this.bePort.Name = "bePort";
            this.bePort.Size = new System.Drawing.Size(32, 14);
            this.bePort.TabIndex = 16;
            this.bePort.Text = "5002";
            // 
            // chart_textbox_panel1
            // 
            this.chart_textbox_panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chart_textbox_panel1.Controls.Add(this.firstLostDataPercentTB);
            this.chart_textbox_panel1.Controls.Add(this.firstChart);
            this.chart_textbox_panel1.Controls.Add(this.firstMessageBox);
            this.chart_textbox_panel1.Location = new System.Drawing.Point(80, 397);
            this.chart_textbox_panel1.Name = "chart_textbox_panel1";
            this.chart_textbox_panel1.Size = new System.Drawing.Size(550, 340);
            this.chart_textbox_panel1.TabIndex = 58;
            // 
            // firstLostDataPercentTB
            // 
            this.firstLostDataPercentTB.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.firstLostDataPercentTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstLostDataPercentTB.ForeColor = System.Drawing.Color.White;
            this.firstLostDataPercentTB.Location = new System.Drawing.Point(110, 257);
            this.firstLostDataPercentTB.Name = "firstLostDataPercentTB";
            this.firstLostDataPercentTB.Size = new System.Drawing.Size(361, 14);
            this.firstLostDataPercentTB.TabIndex = 56;
            // 
            // firstChart
            // 
            this.firstChart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            chartArea9.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea9.AxisX.LineColor = System.Drawing.Color.White;
            chartArea9.AxisX.Title = "丢包周期";
            chartArea9.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea9.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea9.AxisY.LineColor = System.Drawing.Color.White;
            chartArea9.AxisY.Title = "丢包数量";
            chartArea9.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea9.BackColor = System.Drawing.Color.Black;
            chartArea9.Name = "ChartArea1";
            this.firstChart.ChartAreas.Add(chartArea9);
            legend9.Alignment = System.Drawing.StringAlignment.Center;
            legend9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            legend9.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend9.ForeColor = System.Drawing.Color.White;
            legend9.Name = "Legend1";
            legend9.Title = "前车丢包统计";
            legend9.TitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            legend9.TitleForeColor = System.Drawing.Color.White;
            this.firstChart.Legends.Add(legend9);
            this.firstChart.Location = new System.Drawing.Point(19, 25);
            this.firstChart.Name = "firstChart";
            series9.ChartArea = "ChartArea1";
            series9.Color = System.Drawing.Color.Blue;
            series9.IsValueShownAsLabel = true;
            series9.LabelBackColor = System.Drawing.Color.Transparent;
            series9.LabelForeColor = System.Drawing.Color.White;
            series9.Legend = "Legend1";
            series9.Name = "前车丢包";
            series9.ShadowColor = System.Drawing.Color.Transparent;
            this.firstChart.Series.Add(series9);
            this.firstChart.Size = new System.Drawing.Size(500, 200);
            this.firstChart.TabIndex = 53;
            this.firstChart.Text = "chart2";
            // 
            // firstMessageBox
            // 
            this.firstMessageBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.firstMessageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstMessageBox.ForeColor = System.Drawing.Color.White;
            this.firstMessageBox.Location = new System.Drawing.Point(170, 231);
            this.firstMessageBox.Multiline = true;
            this.firstMessageBox.Name = "firstMessageBox";
            this.firstMessageBox.Size = new System.Drawing.Size(280, 20);
            this.firstMessageBox.TabIndex = 55;
            // 
            // chart_textbox_panel2
            // 
            this.chart_textbox_panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_textbox_panel2.Controls.Add(this.secondLostDataPercentTB);
            this.chart_textbox_panel2.Controls.Add(this.secondChart);
            this.chart_textbox_panel2.Controls.Add(this.secondMessageBox);
            this.chart_textbox_panel2.Location = new System.Drawing.Point(636, 397);
            this.chart_textbox_panel2.Name = "chart_textbox_panel2";
            this.chart_textbox_panel2.Size = new System.Drawing.Size(550, 340);
            this.chart_textbox_panel2.TabIndex = 57;
            // 
            // secondLostDataPercentTB
            // 
            this.secondLostDataPercentTB.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.secondLostDataPercentTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondLostDataPercentTB.ForeColor = System.Drawing.Color.White;
            this.secondLostDataPercentTB.Location = new System.Drawing.Point(79, 257);
            this.secondLostDataPercentTB.Name = "secondLostDataPercentTB";
            this.secondLostDataPercentTB.Size = new System.Drawing.Size(425, 14);
            this.secondLostDataPercentTB.TabIndex = 57;
            // 
            // secondChart
            // 
            this.secondChart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            chartArea10.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea10.AxisX.LineColor = System.Drawing.Color.White;
            chartArea10.AxisX.Title = "丢包周期";
            chartArea10.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea10.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea10.AxisY.LineColor = System.Drawing.Color.White;
            chartArea10.AxisY.Title = "丢包数量";
            chartArea10.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea10.BackColor = System.Drawing.Color.Black;
            chartArea10.Name = "ChartArea1";
            this.secondChart.ChartAreas.Add(chartArea10);
            legend10.Alignment = System.Drawing.StringAlignment.Center;
            legend10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            legend10.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend10.ForeColor = System.Drawing.Color.White;
            legend10.Name = "Legend1";
            legend10.Title = "后车丢包统计";
            legend10.TitleBackColor = System.Drawing.Color.Black;
            legend10.TitleForeColor = System.Drawing.Color.White;
            this.secondChart.Legends.Add(legend10);
            this.secondChart.Location = new System.Drawing.Point(19, 25);
            this.secondChart.Name = "secondChart";
            series10.ChartArea = "ChartArea1";
            series10.Color = System.Drawing.Color.Red;
            series10.IsValueShownAsLabel = true;
            series10.LabelBackColor = System.Drawing.Color.Transparent;
            series10.LabelForeColor = System.Drawing.Color.White;
            series10.Legend = "Legend1";
            series10.Name = "后车丢包";
            this.secondChart.Series.Add(series10);
            this.secondChart.Size = new System.Drawing.Size(500, 200);
            this.secondChart.TabIndex = 54;
            this.secondChart.Text = "chart3";
            // 
            // secondMessageBox
            // 
            this.secondMessageBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.secondMessageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondMessageBox.ForeColor = System.Drawing.Color.White;
            this.secondMessageBox.Location = new System.Drawing.Point(170, 231);
            this.secondMessageBox.Multiline = true;
            this.secondMessageBox.Name = "secondMessageBox";
            this.secondMessageBox.Size = new System.Drawing.Size(280, 20);
            this.secondMessageBox.TabIndex = 56;
            // 
            // helper
            // 
            this.helper.BackColor = System.Drawing.SystemColors.Desktop;
            this.helper.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.helper.Location = new System.Drawing.Point(205, 8);
            this.helper.Name = "helper";
            this.helper.Size = new System.Drawing.Size(75, 23);
            this.helper.TabIndex = 56;
            this.helper.Text = "帮助";
            this.toolTip1.SetToolTip(this.helper, "打开帮助文档");
            this.helper.UseVisualStyleBackColor = false;
            this.helper.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // config
            // 
            this.config.BackColor = System.Drawing.SystemColors.Desktop;
            this.config.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.config.Location = new System.Drawing.Point(283, 8);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(75, 23);
            this.config.TabIndex = 57;
            this.config.Text = "配置";
            this.toolTip1.SetToolTip(this.config, "配置IP、端口等内容");
            this.config.UseVisualStyleBackColor = false;
            this.config.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.SystemColors.Desktop;
            this.clear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clear.Location = new System.Drawing.Point(364, 8);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 58;
            this.clear.Text = "清空";
            this.toolTip1.SetToolTip(this.clear, "清空调试文档内容");
            this.clear.UseVisualStyleBackColor = false;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // ZC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1370, 805);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.config);
            this.Controls.Add(this.helper);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.stopTrain);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ZC";
            this.Text = "ZC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZC_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ZC_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ZC_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ZC_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.chart_textbox_panel1.ResumeLayout(false);
            this.chart_textbox_panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstChart)).EndInit();
            this.chart_textbox_panel2.ResumeLayout(false);
            this.chart_textbox_panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox trackPicture;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button stopTrain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart secondChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart firstChart;
        private System.Windows.Forms.Panel chart_textbox_panel1;
        private System.Windows.Forms.Panel chart_textbox_panel2;
        private System.Windows.Forms.TextBox secondMessageBox;
        private System.Windows.Forms.TextBox firstMessageBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label 后车IP;
        private System.Windows.Forms.TextBox beIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox bePort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox forIP;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox forPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox sourceIP;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox sourceport;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox firstLostDataPercentTB;
        private System.Windows.Forms.TextBox secondLostDataPercentTB;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button helper;
        private System.Windows.Forms.Button config;
        private System.Windows.Forms.Button clear;
    }
}

