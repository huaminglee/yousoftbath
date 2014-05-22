using System;
using System.Runtime.InteropServices;

namespace YouSoftBathSelf
{
	/// <summary>
	/// IC ��ժҪ˵����
	/// </summary>
	public class IC
	{
		public IC()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public int icdev ; // ͨѶ�豸��ʶ��
		public int ICstate;//�忨״̬ state=1��д�����п���state=0��д��δ�忨

		[DllImport("Mwic_32.dll", EntryPoint="ic_init",  SetLastError=true,
			 CharSet=CharSet.Auto , ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵������ʼ��ͨѶ�ӿ�
		public static extern int ic_init(Int16 port, int baud);

		[DllImport("Mwic_32.dll", EntryPoint="ic_exit",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    �ر�ͨѶ��
		public static extern Int16 ic_exit(int icdev);

		[DllImport("Mwic_32.dll", EntryPoint="get_status",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����     �����豸��ǰ״̬
		public static extern Int16 get_status(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] state);

		[DllImport("Mwic_32.dll", EntryPoint="dv_beep",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ��д������
			//���ã�    icdev:   ͨѶ�豸��ʶ��   time:    ����ʱ�䣬ֵ��Χ0��255����λ10ms��
			//���أ�    <0       ����  	=0       ��ȷ
		public static extern Int16 dv_beep(int icdev,Int16 time);

		[DllImport("Mwic_32.dll", EntryPoint="chk_card",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    �⿨���ͣ�������������˾������IC��
		public static extern Int16 chk_card(int icdev);

		[DllImport("Mwic_32.dll", EntryPoint="setsc_md",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    �����豸����ģʽmode=0ʱ�����豸������Ч�����豸�ӵ�ʱ�����Ⱥ˶�
			//         �豸������ܶ��豸������mode=1ʱ�����豸������Ч��
		public static extern Int16 setsc_md(int icdev,Int16 mode);
			
		[DllImport("Mwic_32.dll", EntryPoint="asc_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����     ��ASCII��ת��Ϊʮ����������
			//���� asc:����Ҫת�����ַ��� hex�� ���ת������ַ��� length: Ϊת������ַ�������
			//���أ�     =0   ��ȷ   <0 ����  
		public static extern Int16 asc_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)] byte[] hex ,int length);

		[DllImport("Mwic_32.dll", EntryPoint="hex_asc",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����     ��ʮ����������ת��ΪASCII��
			//������     hex:     ����Ҫת�����ַ��� asc��    ���ת������ַ���  length:  ΪҪת�����ַ�������
			//���أ�     =0       ��ȷ  <0       ����  [MarshalAs(UnmanagedType.AnsiBStr)]String asc
		public static extern Int16 hex_asc([MarshalAs(UnmanagedType.LPArray)] byte[] hex, [MarshalAs(UnmanagedType.LPArray)] byte[] asc, int length);

		[DllImport("Mwic_32.dll", EntryPoint="srd_ver",  SetLastError=true,
			 CharSet=CharSet.Ansi , ExactSpelling=false,
			 CallingConvention=CallingConvention.Winapi )]
			//˵����    ��ȡӲ���汾��  
			//		   ���ã�    icdev:    ͨѶ�豸��ʶ��	len:      �ַ������ȣ���ֵΪ18
			//  		  		databuff: ������������ŵ�ַָ��
			//���أ�     <>0   ���� 	=0 ��ȷ
		public static extern Int16 srd_ver(int icdev,Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("Mwic_32.dll", EntryPoint="lib_ver",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����     ��ȡ�������̬�⣩�汾��
		public static extern Int16 lib_ver([MarshalAs(UnmanagedType.LPArray)]byte[] ver);


	}
}
