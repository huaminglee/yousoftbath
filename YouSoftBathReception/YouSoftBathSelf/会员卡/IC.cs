using System;
using System.Runtime.InteropServices;

namespace YouSoftBathSelf
{
	/// <summary>
	/// IC 的摘要说明。
	/// </summary>
	public class IC
	{
		public IC()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public int icdev ; // 通讯设备标识符
		public int ICstate;//插卡状态 state=1读写器插有卡；state=0读写器未插卡

		[DllImport("Mwic_32.dll", EntryPoint="ic_init",  SetLastError=true,
			 CharSet=CharSet.Auto , ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：初始化通讯接口
		public static extern int ic_init(Int16 port, int baud);

		[DllImport("Mwic_32.dll", EntryPoint="ic_exit",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    关闭通讯口
		public static extern Int16 ic_exit(int icdev);

		[DllImport("Mwic_32.dll", EntryPoint="get_status",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 get_status(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] state);

		[DllImport("Mwic_32.dll", EntryPoint="dv_beep",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    读写器蜂鸣
			//调用：    icdev:   通讯设备标识符   time:    蜂鸣时间，值范围0～255（单位10ms）
			//返回：    <0       错误  	=0       正确
		public static extern Int16 dv_beep(int icdev,Int16 time);

		[DllImport("Mwic_32.dll", EntryPoint="chk_card",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    测卡类型，仅适用明华公司生产的IC卡
		public static extern Int16 chk_card(int icdev);

		[DllImport("Mwic_32.dll", EntryPoint="setsc_md",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：    设置设备密码模式mode=0时设置设备密码有效，在设备加电时必须先核对
			//         设备密码才能对设备操作；mode=1时设置设备密码无效。
		public static extern Int16 setsc_md(int icdev,Int16 mode);
			
		[DllImport("Mwic_32.dll", EntryPoint="asc_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     将ASCII码转换为十六进制数据
			//参数 asc:输入要转换的字符串 hex： 存放转换后的字符串 length: 为转换后的字符串长度
			//返回：     =0   正确   <0 错误  
		public static extern Int16 asc_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)] byte[] hex ,int length);

		[DllImport("Mwic_32.dll", EntryPoint="hex_asc",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     将十六进制数据转换为ASCII码
			//参数：     hex:     输入要转换的字符串 asc：    存放转换后的字符串  length:  为要转换的字符串长度
			//返回：     =0       正确  <0       错误  [MarshalAs(UnmanagedType.AnsiBStr)]String asc
		public static extern Int16 hex_asc([MarshalAs(UnmanagedType.LPArray)] byte[] hex, [MarshalAs(UnmanagedType.LPArray)] byte[] asc, int length);

		[DllImport("Mwic_32.dll", EntryPoint="srd_ver",  SetLastError=true,
			 CharSet=CharSet.Ansi , ExactSpelling=false,
			 CallingConvention=CallingConvention.Winapi )]
			//说明：    读取硬件版本号  
			//		   调用：    icdev:    通讯设备标识符	len:      字符串长度，其值为18
			//  		  		databuff: 读出数据所存放地址指针
			//返回：     <>0   错误 	=0 正确
		public static extern Int16 srd_ver(int icdev,Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("Mwic_32.dll", EntryPoint="lib_ver",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     读取软件（动态库）版本号
		public static extern Int16 lib_ver([MarshalAs(UnmanagedType.LPArray)]byte[] ver);


	}
}
