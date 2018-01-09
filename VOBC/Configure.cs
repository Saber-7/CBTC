using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace CBTC
{
    //配置端口和IP地址
    public class ConfigurationData
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private static string filePath = Application.StartupPath + "\\IP-Port-List.ini";//获取INI文件路径
        private static string sectionVOBC = "VOBC"; //INI文件名
        private static string sectionZC = "ZC"; //INI文件名  
        private static string sectionTRAIN = "TRAIN"; //车序号  

        // 自定义读取INI文件中的内容方法
        private static string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, filePath);
            return temp.ToString();
        }
        public static void ReadConfigData()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("未找到配置文件，DMI将不能运行！");
                    return;
                }
                else
                {
                    VOBC.sourceIP = ContentValue(sectionVOBC, "IP");
                    VOBC.sourcePort = ContentValue(sectionVOBC, "port");
                    VOBC.desIP = ContentValue(sectionZC, "IP");
                    VOBC.desPort = ContentValue(sectionZC, "port");
                    VOBC.trainNumber = ContentValue(sectionTRAIN, "number");
                }
            }
            catch
            {
                MessageBox.Show("配置文件中有错误，请修改，并重新启动！配置文件路径为：" + filePath);
                System.Environment.Exit(0);
            }
        }
    }
}
