using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Management;
using YouSoftBath;

namespace YouSoftBath
{
    class GeneralClass
    {
        //打印错误信息
        public static void printErrorMsg(string msg)
        {
            ErrorDlg edlg = new ErrorDlg(msg);
            edlg.ShowDialog();
            //MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //打印警告信息
        public static void printWarningMsg(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //打印询问信息
        public static DialogResult printAskMsg(string msg)
        {
            AskDialog askDlg = new AskDialog(msg);
            return askDlg.ShowDialog();
            //return MessageBox.Show(msg, "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        //打印信息
        public static void printInformation(string msg)
        {
            InformationDlg informationDlg = new InformationDlg(msg);
            informationDlg.ShowDialog();
        }

    }
}
