using System;
using System.Text;

namespace CBTC
{
    class MyStruct
    {
        public int PackedSize { get; set; }

        public void PackUint16(byte[] buf, UInt16 value)
        {
            buf[PackedSize] = (byte)(value & 0xff);
            buf[PackedSize + 1] = (byte)(value >> 8);
            PackedSize += 2;
        }
  
        public void PackUint32(byte[] buf, UInt32 value)
        {
            PackUint16(buf, (UInt16)(value & 0xffff));
            PackUint16(buf, (UInt16)(value >> 16));
        }

        public void PackUint64(byte[] buf, UInt64 value)
        {
            PackUint32(buf, (UInt16)(value & 0xffffffff));
            PackUint32(buf, (UInt16)(value >> 32));
        }
        
        public UInt64 UnpackUint64(byte[] buf)
        {
            UInt64 value_1 = (UInt64)(buf[PackedSize + 7] << 56);
            UInt64 value_2 = (UInt64)(buf[PackedSize + 6] << 48);
            value_1 |= value_2;
            UInt64 value_3 = (UInt64)(buf[PackedSize + 5] << 40);
            UInt64 value_4 = (UInt64)(buf[PackedSize + 4] << 32);
            value_3 |= value_4;
            UInt64 value_5 = (UInt64)(buf[PackedSize + 3] << 24);
            UInt64 value_6 = (UInt64)(buf[PackedSize + 2] << 16);
            value_5 |= value_6;
            UInt64 value_7 = (UInt64)(buf[PackedSize + 1] << 8);
            UInt64 value_8 = (UInt64)buf[PackedSize];
            value_7 |= value_8;
            PackedSize += 8;
            UInt64 value = value_1 |= value_3 |= value_5 |= value_7;
            return value;
        }
    }
}
