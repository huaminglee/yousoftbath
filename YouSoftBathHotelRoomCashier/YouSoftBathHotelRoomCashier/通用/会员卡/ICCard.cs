using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YouSoftBathGeneralClass;
using System.Windows.Forms;

namespace YouSoftBathReception
{
    class ICCard
    {
        private static int icdev;// 通讯设备标识符

        //读取会员卡数据 
        public static bool read_data_4442(string company_code, ref string card_str)
        {
            if (!connect_card_machine("SLE4442"))
                return false;

            int i = 0;
            byte[] data = new byte[16];
            byte[] p_dest = new byte[16];

            for (i = 0; i < 16; i++)
                data[i] = 0;
            for (i = 0; i < 16; i++)
                p_dest[i] = 0;

            int st = IC4442.srd_4442(icdev, 32, 16, data);
            if (st == 0)
            {
                st = IC4442.my_decrypt(data, p_dest);
                card_str = System.Text.Encoding.ASCII.GetString(p_dest);
                if (!card_str.Contains(company_code))
                    return false;

                card_str = card_str.Substring(company_code.Length);
                string tmpStr = "";
                foreach (char c in card_str)
                {
                    if (char.IsDigit(c))
                        tmpStr += c;
                }
                card_str = tmpStr;

                st = IC.dv_beep(icdev, 20);

                st = IC.ic_exit(icdev);
                return true;
            }
            else
                return false;
        }

        //连接读卡机
        private static bool connect_card_machine(string cardType)
        {
            if (icdev > 0)
            {
                if (cardType == "SLE4442")
                    IC.ic_exit(icdev);
                else if (cardType == "M1")
                    ICRF.rf_exit(icdev);
            }

            string card_port = BathClass.get_config_by_key("card_port");
            string card_baud = BathClass.get_config_by_key("card_baud");
            string no_hint = BathClass.get_config_by_key("no_hint");
            if ((card_port == "" || card_baud == "") && (no_hint == "" || no_hint == "false"))
            {
                CardPortBaudForm cardPortBaudForm = new CardPortBaudForm();
                if (cardPortBaudForm.ShowDialog() != DialogResult.OK)
                    return false;

                card_port = cardPortBaudForm.card_port.ToString();
                card_baud = cardPortBaudForm.card_baud.ToString();
            }

            if (card_port == "" || card_baud == "")
                return false;

            Int16 port = Convert.ToInt16(card_port);
            int baud = Convert.ToInt32(card_baud);

            if (cardType == "SLE4442")
                icdev = IC.ic_init(port, baud);
            else if (cardType == "M1")
                icdev = ICRF.rf_init(port, baud);
            if (icdev <= 0)
            {
                BathClass.printErrorMsg("连接读卡器失败，请重试!");
                return false;
            }
            return true;
        }

        //发卡
        public static bool destribute_card_4442(string company_code, string cardCode)
        {
            if (!connect_card_machine("SLE4442"))
                return false;

            if (verify_pwd() != 0)
            {
                BathClass.printErrorMsg("密码验证失败!");
                return false;
            }
            string str = company_code + cardCode;
            byte[] data = Encoding.ASCII.GetBytes(str);
            byte[] buff = new byte[16];
            int st = IC4442.my_encrypt(data, buff);
            st = IC4442.swr_4442(icdev, 32, 16, buff);

            if (st != 0)
            {
                BathClass.printErrorMsg("写卡失败!");
                return false;
            }
            st = IC.dv_beep(icdev, 20);
            st = IC.ic_exit(icdev);
            return true;
        }

        private static int verify_pwd()
        {
            byte[] key1 = new byte[20];
            byte[] key2 = new byte[20];

            string skey = "51020f";
            //string skey = "ffffff";
            int keylen = skey.Length;

            key1 = Encoding.ASCII.GetBytes(skey);
            IC.asc_hex(key1, key2, 6);
            int st = IC4442.csc_4442(icdev, 3, key2);

            return st;
        }

        public static bool read_data_M1(string compnay_code, ref string card_str)
        {
            if (!connect_card_machine("M1"))
                return false;

            ulong snr = 0;
            char mode = '1';
            ICRF.rf_card(icdev, mode, ref snr);

            byte[] key1 = new byte[20];
            byte[] key2 = new byte[20];

            string skey = "6a3530303033";
            string skey_init = "FFFFFFFFFFFF";

            if (verify_pwd_M1(skey_init) || verify_pwd_M1(skey))
            {
                byte[] data = new byte[16];
                byte[] p_dest = new byte[16];
                for (int i = 0; i < 16; i++)
                    data[i] = 0;
                for (int i = 0; i < 16; i++)
                    p_dest[i] = 0;

                int st = ICRF.rf_read(icdev, 9, data);
                if (st == 0)
                {
                    card_str = System.Text.Encoding.ASCII.GetString(data);
                    if (!card_str.Contains(compnay_code))
                        return false;

                    card_str = card_str.Substring(compnay_code.Length);
                    string tmpStr = "";
                    foreach (char c in card_str)
                    {
                        if (char.IsDigit(c))
                            tmpStr += c;
                    }
                    card_str = tmpStr;
                }
                else
                {
                    MessageBox.Show("读取数据失败");
                    ICRF.rf_exit(icdev);
                    return false;
                }

            }
            else
            {
                MessageBox.Show("装载密码失败");
                ICRF.rf_exit(icdev);
                return false;
            }




            ICRF.rf_exit(icdev);

            return true;
        }
        
        private static bool verify_pwd_M1(string skey)
        {
            byte[] key1 = new byte[20];
            byte[] key2 = new byte[20];

            key1 = Encoding.ASCII.GetBytes(skey);
            ICRF.a_hex(key1, key2, 12);
            int st = ICRF.rf_load_key(icdev, 1, 2, key2);
            if (st != 0)
                return false;

            st = ICRF.rf_authentication(icdev, 1, 2);
            if (st != 0)
                return false;

            return true;
        }

        //发卡
        public static bool destribute_card_M1(string company_code, string cardCode)
        {
            if (!connect_card_machine("M1"))
                return false;

            ulong snr = 0;
            char mode = '1';
            ICRF.rf_card(icdev, mode, ref snr);

            byte[] key1 = new byte[20];
            byte[] key2 = new byte[20];

            string skey = "6a3530303033";
            int keylen = skey.Length;

            key1 = Encoding.ASCII.GetBytes(skey);
            ICRF.a_hex(key1, key2, 12);
            int st = ICRF.rf_load_key(icdev, 1, 2, key2);
            if (st != 0)
            {
                MessageBox.Show("装载密码失败");
                ICRF.rf_exit(icdev);
                return false;
            }

            st = ICRF.rf_authentication(icdev, 1, 2);
            if (st != 0)
            {
                MessageBox.Show("验证密码失败");
                ICRF.rf_exit(icdev);
                return false;
            }

            string str = company_code + cardCode;
            byte[] data = Encoding.ASCII.GetBytes(str);
            //byte[] buff = new byte[16];
            //st = IC4442.my_encrypt(data, buff);
            st = ICRF.rf_write(icdev, 9, data);

            if (st != 0)
            {
                BathClass.printErrorMsg("写卡失败!");
                return false;
            }
            st = ICRF.rf_beep(icdev, 20);
            st = ICRF.rf_exit(icdev);
            return true;

        }
    }
}
