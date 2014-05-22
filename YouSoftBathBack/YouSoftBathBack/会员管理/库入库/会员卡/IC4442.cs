using System;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathBack
{
	/// <summary>
	/// IC4442 的摘要说明。
	/// </summary>
	public class IC4442
	{
		public IC4442()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        [DllImport("Mwic_32.dll", EntryPoint = "swr_4442", SetLastError = true,
			 CharSet=CharSet.Auto, ExactSpelling=false, 
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    向指定地址写数据
			//调用：    icdev:    通讯设备标识符  offset:   偏移地址，其值范围0～255
			//          len:      字符串长度，其值范围1～256	w_string: 写入数据  [MarshalAs(UnmanagedType.LPArray)] byte[] StringBuilder
			//返回：     <0   错误 =0  正确
		public static extern Int16 swr_4442(int icdev, Int16 offset, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[]  w_string);
		
		[DllImport("Mwic_32.dll", EntryPoint="srd_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false, 
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    从指定地址读数据  
			//		   调用：    icdev:    通讯设备标识符 offset:   偏移地址，其值范围0～255
			//  		len:      字符串长度，其值范围1～256  		r_string: 读出数据所存放地址指针
			//返回：     <>0   错误 	=0 正确
		public  static extern  Int16 srd_4442(int icdev, Int16 offset, Int16 len,[MarshalAs(UnmanagedType.LPArray)]byte[] r_string ); 
		

		[DllImport("Mwic_32.dll", EntryPoint="chk_4442",  SetLastError=true,
			 CharSet=CharSet.Auto , ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//	说明：    检查卡型是否正确  
			//调用：    icdev:   通讯设备标识符 
			//返回：     <0   错误   =0   正确
		public static extern  Int16 chk_4442(int icdev);
		

		[DllImport("Mwic_32.dll", EntryPoint="csc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.Winapi)]
			//说明：    核对卡密码  
			//调用：    icdev:    通讯设备标识符  len:      密码个数，其值为3 p_string: 密码字符串指针
			//返回：     <0   错误    =0   密码正确
		public static extern Int16 csc_4442(int icdev, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);


		[DllImport("Mwic_32.dll", EntryPoint="wsc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    改写卡密码
			//调用：    icdev:    通讯设备标识符 len: 密码个数，其值为3 p_string: 新密码地址指针
			//返回：    <0   错误   =0   正确
		public static extern Int16 wsc_4442(int icdev, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);

		[DllImport("Mwic_32.dll", EntryPoint="rsc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    读出卡密码  
			//调用：    icdev:    通讯设备标识符  len:      密码个数，其值为3 	p_string: 存放密码地址指针
			// 返回：    <>0   错误   =0   正确	
		public static extern Int16 rsc_4442(int icdev, Int16 len,  [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);

		[DllImport("Mwic_32.dll", EntryPoint="rsct_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    读出密码错误计数器值
			//调用：    icdev:    通讯设备标识符 counter:  密码错误记数值存放指针
			//返回：     <0   错误 >=0   正确
		public static extern Int16 rsct_4442(int icdev, out byte counter);

        [DllImport("decrypt.dll", EntryPoint = "my_decrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：    读出密码错误计数器值
        //调用：    icdev:    通讯设备标识符 counter:  密码错误记数值存放指针
        //返回：     <0   错误 >=0   正确
        public static extern Int16
        my_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport("decrypt.dll", EntryPoint = "my_encrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：    加密
        //调用：    icdev:    通讯设备标识符 counter:  密码错误记数值存放指针
        //返回：     <0   错误 >=0   正确
        public static extern Int16
        my_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);
	}
}
