using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace CBTC
{
    class LinkStatus
    {
        private bool connectFlag = false;
        private bool isLinkNormal = true;
        private PictureBox pbShow = null;
        string discoonnectData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string coonnectData = "";
        public LinkStatus(PictureBox pb, bool isLinkNormal)
        {
            this.pbShow = pb;
            this.isLinkNormal = isLinkNormal;
            LinkStateShow(isLinkNormal);
        }
        public void LinkStateShow(bool isLinkNormal)
        {
            if (isLinkNormal == true) //如果链路正常
            {
                if (connectFlag==true)
                {
                    coonnectData= DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    connectFlag = false;
                }                
                Bitmap lineStatusBitMap = new Bitmap(pbShow.Width, pbShow.Height);
                Graphics gcLinkStatus = Graphics.FromImage(lineStatusBitMap); //从指定的 Image 创建新的 Graphics
                gcLinkStatus.SmoothingMode = SmoothingMode.AntiAlias;  //抗锯齿
                SolidBrush sLinkStatusBrush = new SolidBrush(Color.White);
                Font LinkStatusTextPen = new Font("宋体", 15, FontStyle.Regular);
                gcLinkStatus.DrawString(coonnectData + " " + "与ZC通信状态连接", LinkStatusTextPen, sLinkStatusBrush, pbShow.Width - 380, 17);
                Pen linkStatusPen = new Pen(Color.Black, 5);
                gcLinkStatus.DrawEllipse(linkStatusPen, pbShow.Width - 300, 15, pbShow.Height - 20, pbShow.Height - 20);
                SolidBrush sLinkStatusShowBrush = new SolidBrush(Color.Green);
                gcLinkStatus.FillEllipse(sLinkStatusShowBrush, pbShow.Width - 430, 15, pbShow.Height - 23, pbShow.Height - 23);
                pbShow.Image = lineStatusBitMap; 
                              
 
            }
            else// 如果链路不正常
            {
                connectFlag = true;
                discoonnectData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                Bitmap lineStatusBitMap = new Bitmap(pbShow.Width, pbShow.Height);
                Graphics gcLinkStatus = Graphics.FromImage(lineStatusBitMap); //从指定的 Image 创建新的 Graphics
                gcLinkStatus.SmoothingMode = SmoothingMode.AntiAlias;  //抗锯齿
                SolidBrush sLinkStatusBrush = new SolidBrush(Color.White);
                Font LinkStatusTextPen = new Font("宋体", 15, FontStyle.Regular);
                gcLinkStatus.DrawString(discoonnectData + " "+"与ZC通信状态断开", LinkStatusTextPen, sLinkStatusBrush, pbShow.Width - 380, 17);
                Pen linkStatusPen = new Pen(Color.Black, 5);
                gcLinkStatus.DrawEllipse(linkStatusPen, pbShow.Width - 300, 15, pbShow.Height - 20, pbShow.Height - 20);
                SolidBrush sLinkStatusShowBrush = new SolidBrush(Color.Red);
                gcLinkStatus.FillEllipse(sLinkStatusShowBrush, pbShow.Width - 430, 15, pbShow.Height - 23, pbShow.Height - 23);
                pbShow.Image = lineStatusBitMap;                  
                
            }
        }
    }
}
