using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace CBTC
{

    [StructLayoutAttribute(LayoutKind.Explicit, Size = 400)]
    public struct VobcToZCMessage
    {
        //包序号
        [FieldOffset(0)]
        public UInt64 cycle;
        //包类型，CBTC包/PIS包
        [FieldOffset(8)]
        public UInt64 type;
        //包长 默认400
        [FieldOffset(16)]
        public UInt64 length;
        //车号
        [FieldOffset(24)]
        public UInt64 trainID;
        //包的通信类型，统计包/控制包
        [FieldOffset(32)]
        public UInt64 message;
        //当前车的位置
        [FieldOffset(40)]
        public UInt64 location;
        [FieldOffset(48)]
        public bool isNotLosedata;
        [FieldOffset(56)]
        public DateTime ZCT1;
        [FieldOffset(64)]
        public TimeSpan processTime;

    }

    [StructLayoutAttribute(LayoutKind.Explicit, Size = 400)]
    public struct ZCToVobcMessage
    {
        //包类型，CBTC(0)/PIS(1)
        [FieldOffset(0)]
        public UInt64 type;
        //统计包(0),控制包(1)
        [FieldOffset(8)]
        public UInt64 message;
        //包长
        [FieldOffset(16)]
        public UInt64 length;
        //前车位置
        [FieldOffset(24)]
        public UInt64 prePosition;
        //包号
        [FieldOffset(32)]
        public UInt64 packageNum;
        [FieldOffset(40)]
        public DateTime ZCT1;

    }
    class Message
    {
        //发包
        public byte[] Pack(VobcToZCMessage VobcToZCMes, int size)
        { 
            byte[] tempBytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(VobcToZCMes, structPtr, false);
            Marshal.Copy(structPtr, tempBytes, 0, size);
            Marshal.FreeHGlobal(structPtr);
            return tempBytes;
        }

        //解包
        public ZCToVobcMessage UnPack(byte[] bytes)
        {
            int size = Marshal.SizeOf(typeof(ZCToVobcMessage));
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            ZCToVobcMessage ZCToVobcMes;
            ZCToVobcMes = (ZCToVobcMessage) Marshal.PtrToStructure(structPtr, typeof(ZCToVobcMessage));
            Marshal.FreeHGlobal(structPtr);
            return ZCToVobcMes;
        }
    }
}
