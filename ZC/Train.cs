using System;
using System.Net;
using System.Drawing;
public class Train
{
    //private int _dircet;
    private UInt64 packetNum;

    public UInt64 PacketNum
    {
        get { return packetNum; }
        set { packetNum = value; }
    }

   // public int Dircet
   // {
   //     get { return _dircet; }
   //     set { _dircet = value; }
   // }
   // private int _speed;

   //public int Speed
   // {
   //     get { return _speed; }
   //     set { _speed = value; }
   // }
   //private int _x, _y;

   //public int Y
   //{
   //    get { return _y; }
   //    set { _y = value; }
   //}

   //public int X
   //{
   //    get { return _x; }
   //    set { _x = value; }
   //}
   private int _position;

   public int Position
   {
       get { return _position; }
       set { _position = value; }
   }
   public EndPoint _EP;

   public EndPoint EP
   {
       get { return _EP; }
       set { _EP = value; }
   }

   private int trainNum;

   public int TrainNum
   {
       get { return trainNum; }
       set { trainNum = value; }
   }



   //public byte[] ReceiveData
   //{
   //    get { return receiveData; }
   //    set { receiveData = value; }
   //}

   private Pen trainP;

   public Pen TrainP
   {
       get { return trainP; }
       set { trainP = value; }
   }


   UInt64 _totalPacNum;

   public UInt64 TotalPacNum
   {
       get { return _totalPacNum; }
       set { _totalPacNum = value; }
   }


   private UInt64 _totalLost;

   public UInt64 TotalLost
   {
       get { return _totalLost; }
       set { _totalLost = value; }
   }





   //private bool beginSta;

   //public bool BeginSta
   //{
   //    get { return beginSta; }
   //    set { beginSta = value; }
   //}
   DateTime _beginTime;

   public DateTime BeginTime
   {
       get { return _beginTime; }
       set { _beginTime = value; }
   }
   UInt64 _beginNum;

   public UInt64 BeginNum
   {
       get { return _beginNum; }
       set { _beginNum = value; }
   }

   bool isFirstMessage;

   public bool IsFirstMessage
   {
       get { return isFirstMessage; }
       set { isFirstMessage = value; }
   }
   private int outageT;

   public int OutageT
   {
       get { return outageT; }
       set { outageT = value; }
   }
   private bool isOutageT;

   public bool IsOutageT
   {
       get { return isOutageT; }
       set { isOutageT = value; }
   }
   private DateTime recordTime;

   public DateTime RecordTime
   {
       get { return recordTime; }
       set { recordTime = value; }
   }
   private DateTime sendRecTime;

   public DateTime SendRecTime
   {
       get { return sendRecTime; }
       set { sendRecTime = value; }
   }

   private TimeSpan maxReTime;

   public TimeSpan MaxReTime
   {
       get { return maxReTime; }
       set { maxReTime = value; }
   }

   private TimeSpan averReTime;

   public TimeSpan AverReTime
   {
       get { return averReTime; }
       set { averReTime = value; }
   }

   private TimeSpan minReTime;

   public TimeSpan MinReTime
   {
       get { return minReTime; }
       set { minReTime = value; }
   }

	public Train()
	{
        this.Position = 0;
        this.IsFirstMessage = true;
        this.IsOutageT = false;
        this.RecordTime = DateTime.Now;
        this.SendRecTime = DateTime.Now;
        this.trainNum = 0;
        this.TotalPacNum = 0;
        this.MaxReTime=TimeSpan.MinValue;
        this.AverReTime = TimeSpan.MinValue;
        this.minReTime = TimeSpan.MinValue;
	}
}
