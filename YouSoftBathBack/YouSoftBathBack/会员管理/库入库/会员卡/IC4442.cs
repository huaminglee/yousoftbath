using System;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathBack
{
	/// <summary>
	/// IC4442 ��ժҪ˵����
	/// </summary>
	public class IC4442
	{
		public IC4442()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        [DllImport("Mwic_32.dll", EntryPoint = "swr_4442", SetLastError = true,
			 CharSet=CharSet.Auto, ExactSpelling=false, 
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ��ָ����ַд����
			//���ã�    icdev:    ͨѶ�豸��ʶ��  offset:   ƫ�Ƶ�ַ����ֵ��Χ0��255
			//          len:      �ַ������ȣ���ֵ��Χ1��256	w_string: д������  [MarshalAs(UnmanagedType.LPArray)] byte[] StringBuilder
			//���أ�     <0   ���� =0  ��ȷ
		public static extern Int16 swr_4442(int icdev, Int16 offset, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[]  w_string);
		
		[DllImport("Mwic_32.dll", EntryPoint="srd_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false, 
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ��ָ����ַ������  
			//		   ���ã�    icdev:    ͨѶ�豸��ʶ�� offset:   ƫ�Ƶ�ַ����ֵ��Χ0��255
			//  		len:      �ַ������ȣ���ֵ��Χ1��256  		r_string: ������������ŵ�ַָ��
			//���أ�     <>0   ���� 	=0 ��ȷ
		public  static extern  Int16 srd_4442(int icdev, Int16 offset, Int16 len,[MarshalAs(UnmanagedType.LPArray)]byte[] r_string ); 
		

		[DllImport("Mwic_32.dll", EntryPoint="chk_4442",  SetLastError=true,
			 CharSet=CharSet.Auto , ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//	˵����    ��鿨���Ƿ���ȷ  
			//���ã�    icdev:   ͨѶ�豸��ʶ�� 
			//���أ�     <0   ����   =0   ��ȷ
		public static extern  Int16 chk_4442(int icdev);
		

		[DllImport("Mwic_32.dll", EntryPoint="csc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.Winapi)]
			//˵����    �˶Կ�����  
			//���ã�    icdev:    ͨѶ�豸��ʶ��  len:      �����������ֵΪ3 p_string: �����ַ���ָ��
			//���أ�     <0   ����    =0   ������ȷ
		public static extern Int16 csc_4442(int icdev, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);


		[DllImport("Mwic_32.dll", EntryPoint="wsc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ��д������
			//���ã�    icdev:    ͨѶ�豸��ʶ�� len: �����������ֵΪ3 p_string: �������ַָ��
			//���أ�    <0   ����   =0   ��ȷ
		public static extern Int16 wsc_4442(int icdev, Int16 len, [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);

		[DllImport("Mwic_32.dll", EntryPoint="rsc_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ����������  
			//���ã�    icdev:    ͨѶ�豸��ʶ��  len:      �����������ֵΪ3 	p_string: ��������ַָ��
			// ���أ�    <>0   ����   =0   ��ȷ	
		public static extern Int16 rsc_4442(int icdev, Int16 len,  [MarshalAs(UnmanagedType.LPArray)]byte[] p_string);

		[DllImport("Mwic_32.dll", EntryPoint="rsct_4442",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//˵����    ����������������ֵ
			//���ã�    icdev:    ͨѶ�豸��ʶ�� counter:  ����������ֵ���ָ��
			//���أ�     <0   ���� >=0   ��ȷ
		public static extern Int16 rsct_4442(int icdev, out byte counter);

        [DllImport("decrypt.dll", EntryPoint = "my_decrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //˵����    ����������������ֵ
        //���ã�    icdev:    ͨѶ�豸��ʶ�� counter:  ����������ֵ���ָ��
        //���أ�     <0   ���� >=0   ��ȷ
        public static extern Int16
        my_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport("decrypt.dll", EntryPoint = "my_encrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //˵����    ����
        //���ã�    icdev:    ͨѶ�豸��ʶ�� counter:  ����������ֵ���ָ��
        //���أ�     <0   ���� >=0   ��ȷ
        public static extern Int16
        my_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);
	}
}
