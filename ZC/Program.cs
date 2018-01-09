using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace ZC
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //HardwareInfo hardwareInfo = new HardwareInfo();
            //string cpuID = hardwareInfo.GetCpuID();
            //if (cpuID == "BFEBFBFF000406F1")  //填主机cpu序列号
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ZC());
            //}
            //else
            //{
            //    MessageBox.Show("非本机，不能使用！");
            //    Application.Exit();
            //}

        }
    }
}
