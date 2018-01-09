using System;

namespace CBTC
{
    
    class ZCPackage
    {
        MyStruct ZCStruct = new MyStruct();

        UInt64 cycle_;
        public UInt64 Cycle { get { return cycle_; } set { cycle_ = value; } }

        UInt64 type_;
        public UInt64 PackageType { set { type_ = value; } }

        UInt64 length_;
        public UInt64 Length { set { length_ = value; } }

        UInt64 trainID_;
        public UInt64 TrainID { set { trainID_ = value; } }

        UInt64 message_;
        public UInt64 Message { set { message_ = value; } }

        UInt64 location_;
        public UInt64 Location { set { location_ = value; } }

        UInt64 fillInformation_1;
        public UInt64 FillInformation_1 { set { fillInformation_1 = value; } }

        UInt64 fillInformation_2;
        public UInt64 FillInformation_2 { set { fillInformation_2 = value; } }

        UInt64 fillInformation_3;
        public UInt64 FillInformation_3 { set { fillInformation_3 = value; } }

        UInt64 fillInformation_4;
        public UInt64 FillInformation_4 { set { fillInformation_4 = value; } }

        UInt64 fillInformation_5;
        public UInt64 FillInformation_5 { set { fillInformation_5 = value; } }

        UInt64 fillInformation_6;
        public UInt64 FillInformation_6{ set { fillInformation_6 = value; } }

        UInt64 fillInformation_7;
        public UInt64 FillInformation_7 { set { fillInformation_7 = value; } }

        UInt64 fillInformation_8;
        public UInt64 FillInformation_8 { set { fillInformation_8 = value; } }

        UInt64 fillInformation_9;
        public UInt64 FillInformation_9 { set { fillInformation_9 = value; } }

        UInt64 fillInformation_10;
        public UInt64 FillInformation_10 { set { fillInformation_10 = value; } }

        UInt64 fillInformation_11;
        public UInt64 FillInformation_11 { set { fillInformation_11 = value; } }

        UInt64 fillInformation_12;
        public UInt64 FillInformation_12 { set { fillInformation_12 = value; } }

        UInt64 fillInformation_13;
        public UInt64 FillInformation_13 { set { fillInformation_13 = value; } }

        UInt64 fillInformation_14;
        public UInt64 FillInformation_14 { set { fillInformation_14 = value; } }

        UInt64 fillInformation_15;
        public UInt64 FillInformation_15 { set { fillInformation_15 = value; } }

        UInt64 fillInformation_16;
        public UInt64 FillInformation_16 { set { fillInformation_16 = value; } }

        UInt64 fillInformation_17;
        public UInt64 FillInformation_17 { set { fillInformation_17 = value; } }

        UInt64 fillInformation_18;
        public UInt64 FillInformation_18 { set { fillInformation_18 = value; } }

        UInt64 fillInformation_19;
        public UInt64 FillInformation_19 { set { fillInformation_19 = value; } }

        UInt64 fillInformation_20;
        public UInt64 FillInformation_20 { set { fillInformation_20 = value; } }

        UInt64 fillInformation_21;
        public UInt64 FillInformation_21 { set { fillInformation_21 = value; } }

        UInt64 fillInformation_22;
        public UInt64 FillInformation_22 { set { fillInformation_22 = value; } }

        UInt64 fillInformation_23;
        public UInt64 FillInformation_23 { set { fillInformation_23 = value; } }

        UInt64 fillInformation_24;
        public UInt64 FillInformation_24 { set { fillInformation_24 = value; } }

        UInt64 fillInformation_25;
        public UInt64 FillInformation_25 { set { fillInformation_25 = value; } }

        UInt64 fillInformation_26;
        public UInt64 FillInformation_26 { set { fillInformation_26 = value; } }

        UInt64 fillInformation_27;
        public UInt64 FillInformation_27 { set { fillInformation_27 = value; } }

        UInt64 fillInformation_28;
        public UInt64 FillInformation_28 { set { fillInformation_28 = value; } }

        UInt64 fillInformation_29;
        public UInt64 FillInformation_29 { set { fillInformation_29 = value; } }

        UInt64 fillInformation_30;
        public UInt64 FillInformation_30 { set { fillInformation_30 = value; } }

        UInt64 fillInformation_31;
        public UInt64 FillInformation_ { set { fillInformation_31 = value; } }

        UInt64 fillInformation_32;
        public UInt64 FillInformation_32 { set { fillInformation_32 = value; } }

        UInt64 fillInformation_33;
        public UInt64 FillInformation_33 { set { fillInformation_33 = value; } }

        UInt64 fillInformation_34;
        public UInt64 FillInformation_34 { set { fillInformation_34 = value; } }

        UInt64 fillInformation_35;
        public UInt64 FillInformation_35 { set { fillInformation_35 = value; } }

        UInt64 fillInformation_36;
        public UInt64 FillInformation_36 { set { fillInformation_36 = value; } }

        UInt64 fillInformation_37;
        public UInt64 FillInformation_37 { set { fillInformation_37 = value; } }

        UInt64 fillInformation_38;
        public UInt64 FillInformation_38 { set { fillInformation_38 = value; } }

        UInt64 fillInformation_39;
        public UInt64 FillInformation_39 { set { fillInformation_39 = value; } }

        UInt64 fillInformation_40;
        public UInt64 FillInformation_40 { set { fillInformation_40 = value; } }

        UInt64 fillInformation_41;
        public UInt64 FillInformation_41 { set { fillInformation_41 = value; } }

        UInt64 fillInformation_42;
        public UInt64 FillInformation_42 { set { fillInformation_42 = value; } }

        UInt64 fillInformation_43;
        public UInt64 FillInformation_43 { set { fillInformation_43 = value; } }

        UInt64 fillInformation_44;
        public UInt64 FillInformation_44 { set { fillInformation_44 = value; } }


        public int Pack(byte[] buf)
        {
            ZCStruct.PackedSize = 0;
            ZCStruct.PackUint64(buf, cycle_++);
            ZCStruct.PackUint64(buf, type_);
            ZCStruct.PackUint64(buf, length_);
            ZCStruct.PackUint64(buf, trainID_);
            ZCStruct.PackUint64(buf, message_);
            ZCStruct.PackUint64(buf, location_);
            ZCStruct.PackUint64(buf, fillInformation_1);
            ZCStruct.PackUint64(buf, fillInformation_2);
            ZCStruct.PackUint64(buf, fillInformation_3);
            ZCStruct.PackUint64(buf, fillInformation_4);
            ZCStruct.PackUint64(buf, fillInformation_5);
            ZCStruct.PackUint64(buf, fillInformation_6);
            ZCStruct.PackUint64(buf, fillInformation_7);
            ZCStruct.PackUint64(buf, fillInformation_8);
            ZCStruct.PackUint64(buf, fillInformation_9);
            ZCStruct.PackUint64(buf, fillInformation_10);
            ZCStruct.PackUint64(buf, fillInformation_11);
            ZCStruct.PackUint64(buf, fillInformation_12);
            ZCStruct.PackUint64(buf, fillInformation_13);
            ZCStruct.PackUint64(buf, fillInformation_14);
            ZCStruct.PackUint64(buf, fillInformation_15);
            ZCStruct.PackUint64(buf, fillInformation_16);
            ZCStruct.PackUint64(buf, fillInformation_17);
            ZCStruct.PackUint64(buf, fillInformation_18);
            ZCStruct.PackUint64(buf, fillInformation_19);
            ZCStruct.PackUint64(buf, fillInformation_20);
            ZCStruct.PackUint64(buf, fillInformation_21);
            ZCStruct.PackUint64(buf, fillInformation_22);
            ZCStruct.PackUint64(buf, fillInformation_23);
            ZCStruct.PackUint64(buf, fillInformation_24);
            ZCStruct.PackUint64(buf, fillInformation_25);
            ZCStruct.PackUint64(buf, fillInformation_26);
            ZCStruct.PackUint64(buf, fillInformation_27);
            ZCStruct.PackUint64(buf, fillInformation_28);
            ZCStruct.PackUint64(buf, fillInformation_29);
            ZCStruct.PackUint64(buf, fillInformation_30);
            ZCStruct.PackUint64(buf, fillInformation_31);
            ZCStruct.PackUint64(buf, fillInformation_32);
            ZCStruct.PackUint64(buf, fillInformation_33);
            ZCStruct.PackUint64(buf, fillInformation_34);
            ZCStruct.PackUint64(buf, fillInformation_35);
            ZCStruct.PackUint64(buf, fillInformation_36);
            ZCStruct.PackUint64(buf, fillInformation_37);
            ZCStruct.PackUint64(buf, fillInformation_38);
            ZCStruct.PackUint64(buf, fillInformation_39);
            ZCStruct.PackUint64(buf, fillInformation_40);
            ZCStruct.PackUint64(buf, fillInformation_41);
            ZCStruct.PackUint64(buf, fillInformation_42);
            ZCStruct.PackUint64(buf, fillInformation_43);
            ZCStruct.PackUint64(buf, fillInformation_44);


            return ZCStruct.PackedSize;
        }
    }
}
