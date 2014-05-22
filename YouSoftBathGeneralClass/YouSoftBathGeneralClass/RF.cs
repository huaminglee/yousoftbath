using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathGeneralClass
{
    public class RF
    {
        //读卡
        private struct sRFID
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
            public byte id5;
        }

        private struct s4100reader
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
            public byte id5;
        }

        //发卡，销卡
        private struct sCard
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
        }

        //手牌
        private struct sHandle
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
        }

        //时间
        private struct sTime
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
            public byte id5;
            public byte id6;
        }

        private struct sManager
        {
            public byte id1;
            public byte id2;
            public byte id3;
            public byte id4;
        }

        private static char[] asciicode = 
            new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private static int ChartoAscii(char i)
        {
            //  this.m_txtRFID.Text.ToUpper();
            if (Convert.ToInt32(i) >= 48 && Convert.ToInt32(i) <= 57)
            {

                return (Convert.ToInt32(i) - 48);

            }
            else if (Convert.ToInt32(i) >= 'a' && Convert.ToInt32(i) <= 'f')
            {
                return (Convert.ToInt32(i) - 97) + 10;
            }
            else if (Convert.ToInt32(i) >= 'A' && Convert.ToInt32(i) <= 'F')
            {
                return (Convert.ToInt32(i) - 65) + 10;
            }
            else
            {
                return 0;
            }
        }

        public const string DLLFile = "RF003Reader.DLL";

        [DllImport(DLLFile)]
        private static extern int OpenReader(int ipt);
        [DllImport(DLLFile)]
        static extern int CloseReader();
        [DllImport(DLLFile)]
        private static extern int ReadRFID(ref sRFID RFID);
        [DllImport(DLLFile)]
        private static extern int SetCard(ref sCard RFID);
        [DllImport(DLLFile)]
        private static extern int ClearCard(ref sCard RFID);
        [DllImport(DLLFile)]
        private static extern int GetHandId(ref sHandle RFID);
        [DllImport(DLLFile)]
        private static extern int SetHandId(ref sHandle RFID);
        [DllImport(DLLFile)]
        private static extern int GetRTC(ref sTime RTC);
        [DllImport(DLLFile)]
        private static extern int SetRTC(ref sTime RTC);
        [DllImport(DLLFile)]
        private static extern int GetHandType(ref sManager RTC);
        [DllImport(DLLFile)]
        private static extern int SetHandType(ref sManager RTC);
        [DllImport(DLLFile)]
        private static extern int Read4100RFID(ref s4100reader rtc);

        //读rfid
        public static int RF_RFID(ref string rfid)
        {
            sRFID RFID = new sRFID();
            int rtv = ReadRFID(ref RFID);
            if (rtv == 0)
            {
                rfid = asciicode[RFID.id1 / 16].ToString() + asciicode[RFID.id1 % 16].ToString();
                rfid += asciicode[RFID.id2 / 16].ToString() + asciicode[RFID.id2 % 16].ToString();
                rfid += asciicode[RFID.id3 / 16].ToString() + asciicode[RFID.id3 % 16].ToString();
                rfid += asciicode[RFID.id4 / 16].ToString() + asciicode[RFID.id4 % 16].ToString();
            }
            return rtv;
        }

        //发卡
        public static int RF_FK(string str)
        {
            sCard RFID = new sCard();
            //string str = "";
            //RF_RFID(ref str);
            int a = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(0, 1)))) * 16;
            int b = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(1, 1))));
            int ab = a + b;
            RFID.id1 = Convert.ToByte(ab);
            int c = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(2, 1)))) * 16;
            int d = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(3, 1))));
            int cd = c + d;
            RFID.id2 = Convert.ToByte(cd);
            int j = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(4, 1)))) * 16;
            int f = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(5, 1))));
            int jf = j + f;
            RFID.id3 = Convert.ToByte(jf);
            int g = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(6, 1)))) * 16;
            int h = Convert.ToInt32(ChartoAscii(Convert.ToChar(str.Substring(7, 1))));
            int gh = g + h;


            RFID.id4 = Convert.ToByte(gh);
            return SetCard(ref RFID);
        }

        //销卡操作
        public static int RF_MD()
        {
            int rtv;
            sCard RFID = new sCard();
            rtv = ClearCard(ref RFID);
            return rtv;
        }

        //读手牌码
        public static int RF_ID(int seat_length, ref string id)
        {
            int rtv;
            sHandle hand = new sHandle();
            rtv = GetHandId(ref hand);
            if (rtv == 0)
            {
                if (Convert.ToByte(hand.id1 ^ hand.id2 ^ hand.id3) == hand.id4)
                {
                    id = asciicode[hand.id1 / 16].ToString() + asciicode[hand.id1 % 16].ToString();
                    id += asciicode[hand.id2 / 16].ToString() + asciicode[hand.id2 % 16].ToString();
                    id = id.Substring(4 - seat_length);
                    return 0;
                }
                else
                    return -1;
            }
            else
                return rtv;
        }
    }
}
