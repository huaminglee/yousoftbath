USE [master]

GO
/****** ����:  Database [BathDB]    �ű�����: 05/27/2014 18:54:04 ******/
if not exists(select 1 from master..sysdatabases where name='bathdb')
begin
CREATE DATABASE [BathDB] ON  PRIMARY 
( NAME = N'BathDB', FILENAME = N'D:\���ͿƼ�\DB\BathDB.mdf' , SIZE = 55296KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BathDB_log', FILENAME = N'D:\���ͿƼ�\DB\BathDB_log.ldf' , SIZE = 16576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
end

GO
EXEC dbo.sp_dbcmptlevel @dbname=N'BathDB', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BathDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BathDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BathDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BathDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BathDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BathDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BathDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BathDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BathDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BathDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BathDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BathDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BathDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BathDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BathDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BathDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BathDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BathDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BathDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BathDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BathDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BathDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BathDB] SET  READ_WRITE 
GO
ALTER DATABASE [BathDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BathDB] SET  MULTI_USER 
GO
ALTER DATABASE [BathDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BathDB] SET DB_CHAINING OFF 

go
use bathdb

/*�жϱ�[CustomerPays]�Ƿ����*/
if exists (select * from sysobjects where id=object_id(N'[HisOrders]') and OBJECTPROPERTY(id, N'IsUserTable')=1) and 
	exists (select * from sysobjects where id=object_id(N'[Orders]') and OBJECTPROPERTY(id, N'IsUserTable')=1) and
	exists (select * from sysobjects where id=object_id(N'[DFSeat]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
begin
	insert into [HisOrders](menu,text,systemId,number,priceType,money,technician,techType,inputTime,inputEmployee,deleteEmployee,
	comboId,paid,departmentId) select menu,text,systemId,number,priceType,money,technician,techType,inputTime,inputEmployee,deleteEmployee,
	comboId,'True',departmentId from [Orders] where not exists(select * from [Seat] where [Seat].systemId=[Orders].systemId)

	delete from [Orders] where not exists(select * from [Seat] where [Seat].systemId=[Orders].systemId)
end


/***
**********************************************************************
[UploadRecords]���ݿ��ϴ���¼
**********************************************************************
*****/
/*�жϱ�[UploadRecords]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[UploadRecords]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [UploadRecords]([id] [bigint] IDENTITY(1,1) NOT NULL  primary key)

/*[UploadRecords].[tableName]*/
if not exists (select * from syscolumns where id=object_id('[UploadRecords]') and name='tableName')
	alter table [UploadRecords] add [tableName] [nvarchar](max) NULL

/*[UploadRecords].[maxId]*/
if not exists (select * from syscolumns where id=object_id('[UploadRecords]') and name='maxId')
	alter table [UploadRecords] add [maxId] [bigint] NULL

/*[UploadRecords].[abandonId]*/
if not exists (select * from syscolumns where id=object_id('[UploadRecords]') and name='abandonId')
	alter table [UploadRecords] add [abandonId] [nvarchar](max) NULL

/*[UploadRecords].[clearTime]*/
if not exists (select * from syscolumns where id=object_id('[UploadRecords]') and name='clearTime')
	alter table [UploadRecords] add [clearTime] [datetime] NULL

/*[UploadRecords].[note]*/
if not exists (select * from syscolumns where id=object_id('[UploadRecords]') and name='note')
	alter table [UploadRecords] add [note] [nvarchar](max) NULL


/***
**********************************************************************
[CustomerPays]��Ӧ�̸���
**********************************************************************
*****/
/*�жϱ�[CustomerPays]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CustomerPays]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [CustomerPays]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[CustomerPays].[customerId]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='customerId')
	alter table [CustomerPays] add [customerId] [int] NOT NULL

/*[CustomerPays].[cash]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='cash')
	alter table [CustomerPays] add [cash] [float] NULL

/*[CustomerPays].[bank]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='bank')
	alter table [CustomerPays] add [bank] [float] NULL

/*[CustomerPays].[date]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='date')
	alter table [CustomerPays] add [date] [datetime] not NULL

/*[CustomerPays].[payEmployee]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='payEmployee')
	alter table [CustomerPays] add [payEmployee] [nvarchar](max) NULL

/*[CustomerPays].[note]*/
if not exists (select * from syscolumns where id=object_id('[CustomerPays]') and name='note')
	alter table [CustomerPays] add [note] [nvarchar](max) NULL

/***
**********************************************************************
[ProviderPays]��Ӧ�̸���
**********************************************************************
*****/
/*�жϱ�[ProviderPays]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[ProviderPays]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [ProviderPays]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[ProviderPays].[providerId]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='providerId')
	alter table [ProviderPays] add [providerId] [int] NOT NULL

/*[ProviderPays].[cash]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='cash')
	alter table [ProviderPays] add [cash] [float] NULL

/*[ProviderPays].[bank]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='bank')
	alter table [ProviderPays] add [bank] [float] NULL

/*[ProviderPays].[date]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='date')
	alter table [ProviderPays] add [date] [datetime] not NULL

/*[ProviderPays].[payer]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='payer')
	alter table [ProviderPays] add [payer] [nvarchar](max) NULL

/*[ProviderPays].[confirmer]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='confirmer')
	alter table [ProviderPays] add [confirmer] [nvarchar](max) NULL

/*[ProviderPays].[receiver]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='receiver')
	alter table [ProviderPays] add [receiver] [nvarchar](max) NULL

/*[ProviderPays].[note]*/
if not exists (select * from syscolumns where id=object_id('[ProviderPays]') and name='note')
	alter table [ProviderPays] add [note] [nvarchar](max) NULL


/***
**********************************************************************
[Provider]�̵��¼
**********************************************************************
*****/
/*�жϱ�[Provider]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Provider]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Provider]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Provider].[name]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='name')
	alter table [Provider] add [name] [nvarchar](max) NOT NULL

/*[Provider].[contactor]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='contactor')
	alter table [Provider] add [contactor] [nvarchar](max) NULL

/*[Provider].[tel]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='tel')
	alter table [Provider] add [tel] [nvarchar](max) NULL

/*[Provider].[mobile]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='mobile')
	alter table [Provider] add [mobile] [nvarchar](max) NULL

/*[Provider].[address]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='address')
	alter table [Provider] add [address] [nvarchar](max) NULL

/*[Provider].[note]*/
if not exists (select * from syscolumns where id=object_id('[Provider]') and name='note')
	alter table [Provider] add [note] [nvarchar](max) NULL

/***
**********************************************************************
[Pan]�̵��¼
**********************************************************************
*****/
/*�жϱ�[Pan]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Pan]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Pan]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Pan].[name]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='name')
	alter table [Pan] add [name] [nvarchar](max) NOT NULL

/*[Pan].[amount]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='amount')
	alter table [Pan] add [amount] [float] NULL

/*[Pan].[stockId]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='stockId')
	alter table [Pan] add [stockId] [int] NULL

/*[Pan].[date]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='date')
	alter table [Pan] add [date] [datetime] NULL

/*[Pan].[paner]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='paner')
	alter table [Pan] add [paner] [nvarchar](max) NULL

/*[Pan].[note]*/
if not exists (select * from syscolumns where id=object_id('[Pan]') and name='note')
	alter table [Pan] add [note] [nvarchar](max) NULL


/***
**********************************************************************
[OrderStockOut]�����Զ�����
**********************************************************************
*****/
/*�жϱ�[OrderStockOut]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[OrderStockOut]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [OrderStockOut]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[OrderStockOut].[name]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='name')
	alter table [OrderStockOut] add [name] [nvarchar](max) NOT NULL

/*[OrderStockOut].[amount]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='amount')
	alter table [OrderStockOut] add [amount] [float] NULL

/*[OrderStockOut].[unit]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='unit')
	alter table [OrderStockOut] add [unit] [nvarchar](max) NULL

/*[OrderStockOut].[stockId]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='stockId')
	alter table [OrderStockOut] add [stockId] [int] NULL

/*[OrderStockOut].[date]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='date')
	alter table [OrderStockOut] add [date] [datetime] NULL

/*[OrderStockOut].[sales]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='sales')
	alter table [OrderStockOut] add [sales] [nvarchar](max) NULL

/*[OrderStockOut].[note]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='note')
	alter table [OrderStockOut] add [note] [nvarchar](max) NULL

/*[OrderStockOut].[orderId]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='orderId')
	alter table [OrderStockOut] add [orderId] [int] NULL

/*[OrderStockOut].[deleteEmployee]*/
if not exists (select * from syscolumns where id=object_id('[OrderStockOut]') and name='deleteEmployee')
	alter table [OrderStockOut] add [deleteEmployee] [nvarchar](max) NULL

/***
**********************************************************************
[SystemIds]
**********************************************************************
*****/
/*�жϱ�[SystemIds]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[SystemIds]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [SystemIds]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Room].[name]*/
if not exists (select * from syscolumns where id=object_id('[SystemIds]') and name='systemId')
	alter table [SystemIds] add [systemId] [nvarchar](max) NOT NULL

/***
**********************************************************************
[GoodsCat]
**********************************************************************
*****/
/*�жϱ�[GoodsCat]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[GoodsCat]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [GoodsCat]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Room].[name]*/
if not exists (select * from syscolumns where id=object_id('[GoodsCat]') and name='name')
	alter table [GoodsCat] add [name] [nvarchar](max) NOT NULL

/***
**********************************************************************
[Room]
**********************************************************************
*****/
/*�жϱ�[Room]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Room]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Room]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Room].[name]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='name')
	alter table [Room] add [name] [nvarchar](max) NOT NULL

/*[Room].[population]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='population')
	alter table [Room] add [population] [int] not NULL

/*[Room].[openTime]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='openTime')
	alter table [Room] add [openTime] [nvarchar](max) NULL
else
	alter table [Room] alter column [openTime] [nvarchar](max) NULL

/*[Room].[seat]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='seat')
	alter table [Room] add [seat] [nvarchar](max) NULL

/*[Room].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='systemId')
	alter table [Room] add [systemId] [nvarchar](max) NULL

/*[Room].[orderTime]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='orderTime')
	alter table [Room] add [orderTime] [nvarchar](max) NULL
else
	alter table [Room] alter column [orderTime] [nvarchar](max) NULL

/*[Room].[menu]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='menu')
	alter table [Room] add [menu] [nvarchar](max) NULL

/*[Room].[orderTechId]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='orderTechId')
	alter table [Room] add [orderTechId] [nvarchar](max) NULL

/*[Room].[techId]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='techId')
	alter table [Room] add [techId] [nvarchar](max) NULL

/*[Room].[startTime]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='startTime')
	alter table [Room] add [startTime] [nvarchar](max) NULL
else
	alter table [Room] alter column [startTime] [nvarchar](max) NULL

/*[Room].[serverTime]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='serverTime')
	alter table [Room] add [serverTime] [nvarchar](max) NULL
else
	alter table [Room] alter column [serverTime] [nvarchar](max) NULL

/*[Room].[status]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='status')
	alter table [Room] add [status] [nvarchar](max) not NULL

/*[Room].[note]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='note')
	alter table [Room] add [note] [nvarchar](max) NULL

/*[Room].[hintPlayed]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='hintPlayed')
	alter table [Room] add [hintPlayed] [nvarchar](max) NULL
else
	alter table [Room] alter column [hintPlayed] [nvarchar](max) NULL

/*[Room].[reserveId]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='reserveId')
	alter table [Room] add [reserveId] [nvarchar](max) NULL

/*[Room].[reserveTime]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='reserveTime')
	alter table [Room] add [reserveTime] [nvarchar](max) NULL
else
	alter table [Room] alter column [reserveTime] [nvarchar](max) NULL

/*[Room].[selectId]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='selectId')
	alter table [Room] add [selectId] [nvarchar](max) NULL

/*[Room].[seatIds]*/
if not exists (select * from syscolumns where id=object_id('[Room]') and name='seatIds')
	alter table [Room] add [seatIds] [nvarchar](max) NULL



/***
**********************************************************************
[WaiterItem]
**********************************************************************
*****/
/*�жϱ�[WaiterItem]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[WaiterItem]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [WaiterItem]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[WaiterItem].[name]*/
if not exists (select * from syscolumns where id=object_id('[WaiterItem]') and name='name')
	alter table [WaiterItem] add [name] [nvarchar](max) NULL

/*[WaiterItem].[note]*/
if not exists (select * from syscolumns where id=object_id('[WaiterItem]') and name='note')
	alter table [WaiterItem] add [note] [nvarchar](max) NULL


/***
**********************************************************************
[Unit]
**********************************************************************
*****/
/*�жϱ�[Unit]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Unit]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Unit]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Unit].[name]*/
if not exists (select * from syscolumns where id=object_id('[Unit]') and name='name')
	alter table [Unit] add [name] [nvarchar](max) not NULL

/***
**********************************************************************
[TechReturn]
**********************************************************************
*****/
/*�жϱ�[TechReturn]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[TechReturn]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [TechReturn]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[TechReturn].[techId]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='techId')
	alter table [TechReturn] add [techId] [nvarchar](max) NULL

/*[TechReturn].[inputTime]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='inputTime')
	alter table [TechReturn] add [inputTime] [datetime] NULL

/*[TechReturn].[seatId]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='seatId')
	alter table [TechReturn] add [seatId] [nvarchar](max) NULL

/*[TechReturn].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='roomId')
	alter table [TechReturn] add [roomId] [nvarchar](max) NULL

/*[TechReturn].[menu]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='menu')
	alter table [TechReturn] add [menu] [nvarchar](max) NULL

/*[TechReturn].[note]*/
if not exists (select * from syscolumns where id=object_id('[TechReturn]') and name='note')
	alter table [TechReturn] add [note] [nvarchar](max) NULL



/***
**********************************************************************
[TechReservation]
**********************************************************************
*****/
/*�жϱ�[TechReservation]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[TechReservation]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [TechReservation]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[TechReservation].[techId]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='techId')
	alter table [TechReservation] add [techId] [nvarchar](max) NULL

/*[TechReservation].[seatId]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='seatId')
	alter table [TechReservation] add [seatId] [nvarchar](max) NULL

/*[TechReservation].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='roomId')
	alter table [TechReservation] add [roomId] [nvarchar](max) NULL

/*[TechReservation].[gender]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='gender')
	alter table [TechReservation] add [gender] [nvarchar](max) NULL

/*[TechReservation].[time]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='time')
	alter table [TechReservation] add [time] [datetime] NULL

/*[TechReservation].[proceeded]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='proceeded')
	alter table [TechReservation] add [proceeded] [bit] NULL

/*[TechReservation].[accept]*/
if not exists (select * from syscolumns where id=object_id('[TechReservation]') and name='accept')
	alter table [TechReservation] add [accept] [bit] NULL


/***
**********************************************************************
[TechMsg]
**********************************************************************
*****/
/*�жϱ�[TechMsg]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[TechMsg]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [TechMsg]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[TechMsg].[room]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='room')
	alter table [TechMsg] add [room] [nvarchar](max) not NULL

/*[TechMsg].[seat]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='seat')
	alter table [TechMsg] add [seat] [nvarchar](max) NULL

/*[TechMsg].[type]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='type')
	alter table [TechMsg] add [type] [nvarchar](max) NULL

/*[TechMsg].[techType]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='techType')
	alter table [TechMsg] add [techType] [nvarchar](max) NULL

/*[TechMsg].[number]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='number')
	alter table [TechMsg] add [number] [int] NULL

/*[TechMsg].[techId]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='techId')
	alter table [TechMsg] add [techId] [nvarchar](max) NULL

/*[TechMsg].[time]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='time')
	alter table [TechMsg] add [time] [datetime] not NULL

/*[TechMsg].[printed]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='printed')
	alter table [TechMsg] add [printed] [bit] NULL

/*[TechMsg].[read]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='read')
	alter table [TechMsg] add [read] [bit] not NULL

/*[TechMsg].[menu]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='menu')
	alter table [TechMsg] add [menu] [nvarchar](max) NULL

/*[TechMsg].[gender]*/
if not exists (select * from syscolumns where id=object_id('[TechMsg]') and name='gender')
	alter table [TechMsg] add [gender] [nvarchar](max) NULL



/***
**********************************************************************
[TechIndex]
**********************************************************************
*****/
/*�жϱ�[TechIndex]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[TechIndex]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [TechIndex]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[TechIndex].[dutyid]*/
if not exists (select * from syscolumns where id=object_id('[TechIndex]') and name='dutyid')
	alter table [TechIndex] add [dutyid] [int] NULL

/*[TechIndex].[gender]*/
if not exists (select * from syscolumns where id=object_id('[TechIndex]') and name='gender')
	alter table [TechIndex] add [gender] [nvarchar](max) NULL

/*[TechIndex].[ids]*/
if not exists (select * from syscolumns where id=object_id('[TechIndex]') and name='ids')
	alter table [TechIndex] add [ids] [nvarchar](max) NULL


/***
**********************************************************************
[StorageList]
**********************************************************************
*****/
/*�жϱ�[StorageList]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[StorageList]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [StorageList]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[StorageList].[name]*/
if not exists (select * from syscolumns where id=object_id('[StorageList]') and name='name')
	alter table [StorageList] add [name] [nvarchar](max) NULL

/*[StorageList].[minAmount]*/
if not exists (select * from syscolumns where id=object_id('[StorageList]') and name='minAmount')
	alter table [StorageList] add [minAmount] [float] NULL
else
	alter table [StorageList] alter column [minAmount] [float]

/*[StorageList].[note]*/
if not exists (select * from syscolumns where id=object_id('[StorageList]') and name='note')
	alter table [StorageList] add [note] [nvarchar](max) NULL

/*[StorageList].[goodsCatId]*/
if not exists (select * from syscolumns where id=object_id('[StorageList]') and name='goodsCatId')
	alter table [StorageList] add [goodsCatId] [int]  NULL




/***
**********************************************************************
[StockOut]
**********************************************************************
*****/
/*�жϱ�[StockOut]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[StockOut]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [StockOut]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[StockOut].[name]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='name')
	alter table [StockOut] add [name] [nvarchar](max) NULL

/*[StockOut].[amount]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='amount')
	alter table [StockOut] add [amount] [float] NULL
else
	alter table [StockOut] alter column [amount] [float]

/*[StockOut].[unit]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='unit')
	alter table [StockOut] add [unit] [nvarchar](max) NULL

/*[StockOut].[stockId]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='stockId')
	alter table [StockOut] add [stockId] [int] NULL

/*[StockOut].[toStockId]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='toStockId')
	alter table [StockOut] add [toStockId] [int] NULL

/*[StockOut].[date]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='date')
	alter table [StockOut] add [date] [datetime] NULL

/*[StockOut].[receiver]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='receiver')
	alter table [StockOut] add [receiver] [nvarchar](max) NULL

/*[StockOut].[transactor]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='transactor')
	alter table [StockOut] add [transactor] [nvarchar](max) NULL

/*[StockOut].[checker]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='checker')
	alter table [StockOut] add [checker] [nvarchar](max) NULL

/*[StockOut].[note]*/
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='note')
	alter table [StockOut] add [note] [nvarchar](max) NULL

if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='cost')
	alter table [StockOut] add [cost] [float] NULL
	
if not exists (select * from syscolumns where id=object_id('[StockOut]') and name='money')
	alter table [StockOut] add [money] [float] NULL


/***
**********************************************************************
[StockIn]
**********************************************************************
*****/
/*�жϱ�[StockIn]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[StockIn]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [StockIn]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[StockIn].[name]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='name')
	alter table [StockIn] add [name] [nvarchar](max) not NULL

/*[StockIn].[cost]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='cost')
	alter table [StockIn] add [cost] [float] NULL

/*[StockIn].[money]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='money')
	alter table [StockIn] add [money] [float] NULL

/*[StockIn].[amount]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='amount')
	alter table [StockIn] add [amount] [float] NOT NULL
else
	alter table [StockIn] alter column [amount] [float]

/*[StockIn].[unit]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='unit')
	alter table [StockIn] add [unit] [nvarchar](max) NULL

/*[StockIn].[stockId]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='stockId')
	alter table [StockIn] add [stockId] [int] not NULL

/*[StockIn].[note]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='note')
	alter table [StockIn] add [note] [nvarchar](max) NULL

/*[StockIn].[date]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='date')
	alter table [StockIn] add [date] [datetime] not NULL

/*[StockIn].[transactor]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='transactor')
	alter table [StockIn] add [transactor] [nvarchar](max) NOT NULL

/*[StockIn].[checker]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='checker')
	alter table [StockIn] add [checker] [nvarchar](max) NOT NULL

/*[StockIn].[providerId]*/
if not exists (select * from syscolumns where id=object_id('[StockIn]') and name='providerId')
	alter table [StockIn] add [providerId] [int] NULL



/***
**********************************************************************
[Stock]
**********************************************************************
*****/
/*�жϱ�[Stock]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Stock]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Stock]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Stock].[name]*/
if not exists (select * from syscolumns where id=object_id('[Stock]') and name='name')
	alter table [Stock] add [name] [nvarchar](max) NULL

/*[Stock].[note]*/
if not exists (select * from syscolumns where id=object_id('[Stock]') and name='note')
	alter table [Stock] add [note] [nvarchar](max) NULL

/*[Stock].[phone]*/
if not exists (select * from syscolumns where id=object_id('[Stock]') and name='phone')
	alter table [Stock] add [phone] [nvarchar](max) NULL

/*[Stock].[ips]*/
if not exists (select * from syscolumns where id=object_id('[Stock]') and name='ips')
	alter table [Stock] add [ips] [nvarchar](max) NULL

/*[Stock].[main]*/
if not exists (select * from syscolumns where id=object_id('[Stock]') and name='main')
	alter table [Stock] add [main] [bit] NULL



/***
**********************************************************************
[ShoeMsg]
**********************************************************************
*****/
/*�жϱ�[ShoeMsg]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[ShoeMsg]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [ShoeMsg]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[ShoeMsg].[text]*/
if not exists (select * from syscolumns where id=object_id('[ShoeMsg]') and name='text')
	alter table [ShoeMsg] add [text] [nvarchar](max) not NULL

/*[ShoeMsg].[payEmployee]*/
if not exists (select * from syscolumns where id=object_id('[ShoeMsg]') and name='payEmployee')
	alter table [ShoeMsg] add [payEmployee] [nvarchar](max) not NULL

/*[ShoeMsg].[payTime]*/
if not exists (select * from syscolumns where id=object_id('[ShoeMsg]') and name='payTime')
	alter table [ShoeMsg] add [payTime] [datetime] not NULL

/*[ShoeMsg].[processed]*/
if not exists (select * from syscolumns where id=object_id('[ShoeMsg]') and name='processed')
	alter table [ShoeMsg] add [processed] [bit] not NULL



/***
**********************************************************************
[RoomWarn]
**********************************************************************
*****/
/*�жϱ�[RoomWarn]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[RoomWarn]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [RoomWarn]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[RoomWarn].[msg]*/
if not exists (select * from syscolumns where id=object_id('[RoomWarn]') and name='msg')
	alter table [RoomWarn] add [msg] [nvarchar](max) not NULL

/*[RoomWarn].[room]*/
if not exists (select * from syscolumns where id=object_id('[RoomWarn]') and name='room')
	alter table [RoomWarn] add [room] [nvarchar](max) NULL



/***
**********************************************************************
[RoomReserveMsg]
**********************************************************************
*****/
/*�жϱ�[RoomReserveMsg]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[RoomReserveMsg]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [RoomReserveMsg]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[RoomReserveMsg].[techId]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='techId')
	alter table [RoomReserveMsg] add [techId] [nvarchar](max) NULL

/*[RoomReserveMsg].[seatId]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='seatId')
	alter table [RoomReserveMsg] add [seatId] [nvarchar](max) NULL

/*[RoomReserveMsg].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='roomId')
	alter table [RoomReserveMsg] add [roomId] [nvarchar](max) NULL

/*[RoomReserveMsg].[time]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='time')
	alter table [RoomReserveMsg] add [time] [nvarchar](max) NULL

/*[RoomReserveMsg].[proceeded]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='proceeded')
	alter table [RoomReserveMsg] add [proceeded] [nvarchar](max) NULL

/*[RoomReserveMsg].[accept]*/
if not exists (select * from syscolumns where id=object_id('[RoomReserveMsg]') and name='accept')
	alter table [RoomReserveMsg] add [accept] [nvarchar](max) NULL



/***
**********************************************************************
[RoomCall]
**********************************************************************
*****/
/*�жϱ�[RoomCall]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[RoomCall]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [RoomCall]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[RoomCall].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[RoomCall]') and name='roomId')
	alter table [RoomCall] add [roomId] [nvarchar](max) NULL

/*[RoomCall].[seatId]*/
if not exists (select * from syscolumns where id=object_id('[RoomCall]') and name='seatId')
	alter table [RoomCall] add [seatId] [nvarchar](max) NULL

/*[RoomCall].[read]*/
if not exists (select * from syscolumns where id=object_id('[RoomCall]') and name='read')
	alter table [RoomCall] add [read] [bit] not NULL

/*[RoomCall].[msg]*/
if not exists (select * from syscolumns where id=object_id('[RoomCall]') and name='msg')
	alter table [RoomCall] add [msg] [nvarchar](max) not NULL


/***
**********************************************************************
[Promotion]
**********************************************************************
*****/
/*�жϱ�[Promotion]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Promotion]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Promotion]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Promotion].[name]*/
if not exists (select * from syscolumns where id=object_id('[Promotion]') and name='name')
	alter table [Promotion] add [name] [nvarchar](max) not NULL

/*[Promotion].[status]*/
if not exists (select * from syscolumns where id=object_id('[Promotion]') and name='status')
	alter table [Promotion] add [status] [bit] not NULL

/*[Promotion].[menuIds]*/
if not exists (select * from syscolumns where id=object_id('[Promotion]') and name='menuIds')
	alter table [Promotion] add [menuIds] [nvarchar](max) not NULL

/***
**********************************************************************
[GroupBuyPromotion]
**********************************************************************
*****/
/*�жϱ�[GroupBuyPromotion]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[GroupBuyPromotion]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [GroupBuyPromotion]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[GroupBuyPromotion].[menuIds]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuyPromotion]') and name='menuIds')
	alter table [GroupBuyPromotion] add [menuIds] [nvarchar](max) not NULL


/***
**********************************************************************
[PayMsg]
**********************************************************************
*****/
/*�жϱ�[PayMsg]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[PayMsg]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [PayMsg]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[PayMsg].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[PayMsg]') and name='systemId')
	alter table [PayMsg] add [systemId] [nvarchar](max) NULL

/*[PayMsg].[ip]*/
if not exists (select * from syscolumns where id=object_id('[PayMsg]') and name='ip')
	alter table [PayMsg] add [ip] [nvarchar](max) NULL


/***
**********************************************************************
[Options]
**********************************************************************
*****/
/*�жϱ�[Options]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Options]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Options]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Options].[companyName]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='companyName')
	alter table [Options] add [companyName] [nvarchar](max) not NULL

/*[Options].[companyCode]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='companyCode')
	alter table [Options] add [companyCode] [nvarchar](max) not NULL

/*[Options].[company_Code]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='company_Code')
	alter table [Options] add [company_Code] [nvarchar](max) NULL

/*[Options].[companyPhone]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='companyPhone')
	alter table [Options] add [companyPhone] [nvarchar](max) NULL

/*[Options].[companyAddress]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='companyAddress')
	alter table [Options] add [companyAddress] [nvarchar](max) NULL

/*[Options].[ȡ������ʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='ȡ������ʱ��')
	alter table [Options] add [ȡ������ʱ��] [int] NULL

/*[Options].[ȡ������ʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='ȡ������ʱ��')
	alter table [Options] add [ȡ������ʱ��] [int] NULL

/*[Options].[ɾ��֧��ʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='ɾ��֧��ʱ��')
	alter table [Options] add [ɾ��֧��ʱ��] [int] NULL

/*[Options].[����ʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����ʱ��')
	alter table [Options] add [����ʱ��] [int] NULL

/*[Options].[��ʦ����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ʦ����')
	alter table [Options] add [��ʦ����] [int] NULL

/*[Options].[����Ь��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����Ь��')
	alter table [Options] add [����Ь��] [bit] NULL

/*[Options].[Ь������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='Ь������')
	alter table [Options] add [Ь������] [int] NULL

/*[Options].[���û�Ա������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���û�Ա������')
	alter table [Options] add [���û�Ա������] [bit] NULL

/*[Options].[��Ա����������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��Ա����������')
	alter table [Options] add [��Ա����������] [nvarchar](max) NULL

/*[Options].[���ý��˼��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���ý��˼��')
	alter table [Options] add [���ý��˼��] [bit] NULL

/*[Options].[������Ƶ����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='������Ƶ����')
	alter table [Options] add [������Ƶ����] [nvarchar](max) NULL

/*[Options].[����������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����������')
	alter table [Options] add [����������] [bit] NULL

/*[Options].[��ҵʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ҵʱ��')
	alter table [Options] add [��ҵʱ��] [int] NULL

/*[Options].[���ÿͷ����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���ÿͷ����')
	alter table [Options] add [���ÿͷ����] [bit] NULL

/*[Options].[�����ȴ�ʱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='�����ȴ�ʱ��')
	alter table [Options] add [�����ȴ�ʱ��] [int] NULL

/*[Options].[��������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��������')
	alter table [Options] add [��������] [int] NULL

/*[Options].[����ID������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����ID������')
	alter table [Options] add [����ID������] [bit] NULL

/*[Options].[�����ֹ��������ƺſ���]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='�����ֹ��������ƺſ���')
	alter table [Options] add [�����ֹ��������ƺſ���] [bit] NULL

/*[Options].[�����ֹ��������ƺŽ���]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='�����ֹ��������ƺŽ���')
	alter table [Options] add [�����ֹ��������ƺŽ���] [bit] NULL

/*[Options].[¼�����뵥�ݱ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='¼�����뵥�ݱ��')
	alter table [Options] add [¼�����뵥�ݱ��] [bit] NULL

/*[Options].[����δ����������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����δ����������')
	alter table [Options] add [����δ����������] [bit] NULL

/*[Options].[Ӫҵ�����ʽ]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='Ӫҵ�����ʽ')
	alter table [Options] add [Ӫҵ�����ʽ] [int] NULL

/*[Options].[��ɱ����ʽ]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ɱ����ʽ')
	alter table [Options] add [��ɱ����ʽ] [int] NULL

/*[Options].[���˴�ӡ���˵�]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���˴�ӡ���˵�')
	alter table [Options] add [���˴�ӡ���˵�] [bit] NULL

/*[Options].[���˴�ӡ�����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���˴�ӡ�����')
	alter table [Options] add [���˴�ӡ�����] [bit] NULL

/*[Options].[���˴�ӡȡЬСƱ]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���˴�ӡȡЬСƱ')
	alter table [Options] add [���˴�ӡȡЬСƱ] [bit] NULL

/*[Options].[Ĩ������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='Ĩ������')
	alter table [Options] add [Ĩ������] [int] NULL

/*[Options].[����������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����������')
	alter table [Options] add [����������] [nvarchar](max) NULL

/*[Options].[�Զ����չ�ҹ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='�Զ����չ�ҹ��')
	alter table [Options] add [�Զ����չ�ҹ��] [bit] NULL

/*[Options].[��ҹ�����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ҹ�����')
	alter table [Options] add [��ҹ�����] [nvarchar](max) NULL

/*[Options].[��ҹ���յ�]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ҹ���յ�')
	alter table [Options] add [��ҹ���յ�] [nvarchar](max) NULL

/*[Options].[���÷ֵ�����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���÷ֵ�����')
	alter table [Options] add [���÷ֵ�����] [bit] NULL

/*[Options].[����Ա������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='����Ա������')
	alter table [Options] add [����Ա������] [bit] NULL

/*[Options].[̨λ���ͷ�ҳ��ʾ]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='̨λ���ͷ�ҳ��ʾ')
	alter table [Options] add [̨λ���ͷ�ҳ��ʾ] [bit] NULL

/*[Options].[�Զ���Ӧ����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='�Զ���Ӧ����')
	alter table [Options] add [�Զ���Ӧ����] [bit] NULL

/*[Options].[¼�����ֵ�������]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='¼�����ֵ�������')
	alter table [Options] add [¼�����ֵ�������] [bit] NULL

/*[Options].[��ӡ��ʦ��ǲ��]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='��ӡ��ʦ��ǲ��')
	alter table [Options] add [��ӡ��ʦ��ǲ��] [bit] NULL

/*[Options].[���ô�����]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='���ô�����')
	alter table [Options] add [���ô�����] [bit] NULL



/***
**********************************************************************
[Operation]
**********************************************************************
*****/
/*�жϱ�[Operation]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Operation]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Operation]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Operation].[seat]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='seat')
	alter table [Operation] add [seat] [nvarchar](max) NULL

/*[Operation].[openEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='openEmployee')
	alter table [Operation] add [openEmployee] [nvarchar](max) NULL

/*[Operation].[openTime]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='openTime')
	alter table [Operation] add [openTime] [datetime] NULL

/*[Operation].[employee]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='employee')
	alter table [Operation] add [employee] [nvarchar](max) NULL

/*[Operation].[explain]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='explain')
	alter table [Operation] add [explain] [nvarchar](max) NULL

/*[Operation].[opTime]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='opTime')
	alter table [Operation] add [opTime] [datetime] NULL

/*[Operation].[note1]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='note1')
	alter table [Operation] add [note1] [nvarchar](max) NULL

/*[Operation].[note2]*/
if not exists (select * from syscolumns where id=object_id('[Operation]') and name='note2')
	alter table [Operation] add [note2] [nvarchar](max) NULL





/***
**********************************************************************
[Menu]
**********************************************************************
*****/
/*�жϱ�[Menu]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Menu]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Menu]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Menu].[name]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='name')
	alter table [Menu] add [name] [nvarchar](max) NOT NULL

/*[Menu].[catgoryId]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='catgoryId')
	alter table [Menu] add [catgoryId] [int] not NULL

/*[Menu].[unit]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='unit')
	alter table [Menu] add [unit] [nvarchar](max) NOT NULL

/*[Menu].[price]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='price')
	alter table [Menu] add [price] [float] not NULL

/*[Menu].[technician]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='technician')
	alter table [Menu] add [technician] [bit] not NULL

/*[Menu].[techRatioCat]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='techRatioCat')
	alter table [Menu] add [techRatioCat] [nvarchar](max) NULL

/*[Menu].[techRatioType]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='techRatioType')
	alter table [Menu] add [techRatioType] [nvarchar](max) NULL

/*[Menu].[onRatio]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='onRatio')
	alter table [Menu] add [onRatio] [float] NULL

/*[Menu].[orderRatio]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='orderRatio')
	alter table [Menu] add [orderRatio] [float] NULL

/*[Menu].[timeLimitType]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='timeLimitType')
	alter table [Menu] add [timeLimitType] [nvarchar](max) NULL

/*[Menu].[timeLimitHour]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='timeLimitHour')
	alter table [Menu] add [timeLimitHour] [int] NULL

/*[Menu].[timeLimitMiniute]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='timeLimitMiniute')
	alter table [Menu] add [timeLimitMiniute] [int] NULL

/*[Menu].timeLimitSecond*/
if exists (select * from syscolumns where id=object_id('[Menu]') and name='timeLimitSecond')
	alter table [Menu] drop column [timeLimitSecond]

/*[Menu].[addAutomatic]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='addAutomatic')
	alter table [Menu] add [addAutomatic] [bit] not NULL

/*[Menu].[addType]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='addType')
	alter table [Menu] add [addType] [nvarchar](max) NULL

/*[Menu].[addMoney]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='addMoney')
	alter table [Menu] add [addMoney] [float] NULL

/*[Menu].[note]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='note')
	alter table [Menu] add [note] [nvarchar](max) NULL

/*[Menu].[waiter]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='waiter')
	alter table [Menu] add [waiter] [bit] NULL

/*[Menu].[waiterRatioType]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='waiterRatioType')
	alter table [Menu] add [waiterRatioType] [int] NULL

/*[Menu].[waiterRatio]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='waiterRatio')
	alter table [Menu] add [waiterRatio] [float] NULL

/*[Menu].[ResourceExpense]*/
if not exists (select * from syscolumns where id=object_id('[Menu]') and name='ResourceExpense')
	alter table [Menu] add [ResourceExpense] [nvarchar](max) NULL



/***
**********************************************************************
[MemberType]
**********************************************************************
*****/
/*�жϱ�[MemberType]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[MemberType]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [MemberType]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[MemberType].[name]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='name')
	alter table [MemberType] add [name] [nvarchar](max) NOT NULL

/*[MemberType].[timSpan]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='timSpan')
	alter table [MemberType] add [timSpan] [nvarchar](max) NULL

/*[MemberType].[times]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='times')
	alter table [MemberType] add [times] [int] NULL

/*[MemberType].[money]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='money')
	alter table [MemberType] add [money] [float] NULL

/*[MemberType].[maxOpenMoney]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='maxOpenMoney')
	alter table [MemberType] add [maxOpenMoney] [float] NULL

/*[MemberType].[expireDate]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='expireDate')
	alter table [MemberType] add [expireDate] [datetime] NULL

/*[MemberType].[offerId]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='offerId')
	alter table [MemberType] add [offerId] [int] NULL

/*[MemberType].[credits]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='credits')
	alter table [MemberType] add [credits] [bit] not NULL

/*[MemberType].[smsAfterUsing]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='smsAfterUsing')
	alter table [MemberType] add [smsAfterUsing] [bit] NULL

/*[MemberType].[userOneTimeOneDay]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='userOneTimeOneDay')
	alter table [MemberType] add [userOneTimeOneDay] [bit] NULL

/*[MemberType].[LimitedTimesPerMonth]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='LimitedTimesPerMonth')
	alter table [MemberType] add [LimitedTimesPerMonth] [bit] NULL

/*[MemberType].[TimesPerMonth]*/
if not exists (select * from syscolumns where id=object_id('[MemberType]') and name='TimesPerMonth')
	alter table [MemberType] add [TimesPerMonth] [int] NULL



/***
**********************************************************************
[MemberSetting]
**********************************************************************
*****/
/*�жϱ�[MemberSetting]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[MemberSetting]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [MemberSetting]([id] [int] NOT NULL  primary key)

/*[MemberSetting].[money]*/
if not exists (select * from syscolumns where id=object_id('[MemberSetting]') and name='money')
	alter table [MemberSetting] add [money] [int] NULL

/*[MemberSetting].[cardType]*/
if not exists (select * from syscolumns where id=object_id('[MemberSetting]') and name='cardType')
	alter table [MemberSetting] add [cardType] [nvarchar](max) NULL


/***
**********************************************************************
[Department]
**********************************************************************
*****/
/*�жϱ�[Department]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Department]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Department]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Department].[name]*/
if not exists (select * from syscolumns where id=object_id('[Department]') and name='name')
	alter table [Department] add [name] [nvarchar](max) not NULL

/*[Department].[note]*/
if not exists (select * from syscolumns where id=object_id('[Department]') and name='note')
	alter table [Department] add [note] [nvarchar](max) NULL

/***
**********************************************************************
[DepartmentLog]
**********************************************************************
*****/
/*�жϱ�[DepartmentLog]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[DepartmentLog]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [DepartmentLog]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[DepartmentLog].[departId]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='departId')
	alter table [DepartmentLog] add [departId] [int] NULL

/*[DepartmentLog].[sender]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='sender')
	alter table [DepartmentLog] add [sender] [nvarchar](max) NULL

/*[DepartmentLog].[msg]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='msg')
	alter table [DepartmentLog] add [msg] [nvarchar](max) NULL

/*[DepartmentLog].[img]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='img')
	alter table [DepartmentLog] add [img] [varbinary](max) NULL

/*[DepartmentLog].[imgUrl]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='imgUrl')
	alter table [DepartmentLog] add [imgUrl] [nvarchar](max) NULL

/*[DepartmentLog].[img2]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='img2')
	alter table [DepartmentLog] add [img2] [varbinary](max) NULL

/*[DepartmentLog].[img2Url]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='img2Url')
	alter table [DepartmentLog] add [img2Url] [nvarchar](max) NULL

/*[DepartmentLog].[img3]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='img3')
	alter table [DepartmentLog] add [img3] [varbinary](max) NULL

/*[DepartmentLog].[img3Url]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='img3Url')
	alter table [DepartmentLog] add [img3Url] [nvarchar](max) NULL

/*[DepartmentLog].[urgent]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='urgent')
	alter table [DepartmentLog] add [urgent] [bit] NULL

/*[DepartmentLog].[done]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='done')
	alter table [DepartmentLog] add [done] [bit] NULL

/*[DepartmentLog].[dueTime]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='dueTime')
	alter table [DepartmentLog] add [dueTime] [datetime] NULL

/*[DepartmentLog].[date]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='date')
	alter table [DepartmentLog] add [date] [datetime] NULL

/*[DepartmentLog].[urgentDate]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='urgentDate')
	alter table [DepartmentLog] add [urgentDate] [datetime] NULL

/*[DepartmentLog].[doneDate]*/
if not exists (select * from syscolumns where id=object_id('[DepartmentLog]') and name='doneDate')
	alter table [DepartmentLog] add [doneDate] [datetime] NULL


/***
**********************************************************************
[Job]
**********************************************************************
*****/
/*�жϱ�[Job]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Job]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Job]([id] [int] NOT NULL  primary key)

/*[Job].[name]*/
if not exists (select * from syscolumns where id=object_id('[Job]') and name='name')
	alter table [Job] add [name] [nvarchar](max) not NULL

/*[Job].[note]*/
if not exists (select * from syscolumns where id=object_id('[Job]') and name='note')
	alter table [Job] add [note] [nvarchar](max) NULL

/*[Job].[ip]*/
if not exists (select * from syscolumns where id=object_id('[Job]') and name='ip')
	alter table [Job] add [ip] [nvarchar](max) NULL

/*[Job].[leaderId]*/
if not exists (select * from syscolumns where id=object_id('[Job]') and name='leaderId')
	alter table [Job] add [leaderId] [int] NULL

/*[Job].[departId]*/
if not exists (select * from syscolumns where id=object_id('[Job]') and name='departId')
	alter table [Job] add [departId] [int] NULL

/***
**********************************************************************
[HotelRoomType]
**********************************************************************
*****/
/*�жϱ�[HotelRoomType]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[HotelRoomType]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [HotelRoomType]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[HotelRoomType].[name]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoomType]') and name='name')
	alter table [HotelRoomType] add [name] [nvarchar](max) NULL

/*[HotelRoomType].[population]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoomType]') and name='population')
	alter table [HotelRoomType] add [population] [int] NULL

/*[HotelRoomType].[menuId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoomType]') and name='menuId')
	alter table [HotelRoomType] add [menuId] [int] NULL


/***
**********************************************************************
[SeatType]
**********************************************************************
*****/
/*�жϱ�[SeatType]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[SeatType]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [SeatType]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[SeatType].[name]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='name')
	alter table [SeatType] add [name] [nvarchar](max) not NULL

/*[SeatType].[population]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='population')
	alter table [SeatType] add [population] [int] not NULL

/*[SeatType].[menuId]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='menuId')
	alter table [SeatType] add [menuId] [int] NULL

/*[SeatType].[department]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='department')
	alter table [SeatType] add [department] [nvarchar](max) NULL

/*[SeatType].[guoyePrice]*/
if exists (select * from syscolumns where id=object_id('[SeatType]') and name='guoyePrice')
	alter table [SeatType] drop column [guoyePrice]

/*[SeatType].guoyeStartTime*/
if exists (select * from syscolumns where id=object_id('[SeatType]') and name='guoyeStartTime')
	alter table [SeatType] drop column [guoyeStartTime]

/*[SeatType].guoyeEndTime*/
if exists (select * from syscolumns where id=object_id('[SeatType]') and name='guoyeEndTime')
	alter table [SeatType] drop column [guoyeEndTime]

/*[SeatType].[depositeRequired]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='depositeRequired')
	alter table [SeatType] add [depositeRequired] [bit] NULL

/*[SeatType].[depositeAmountMin]*/
if not exists (select * from syscolumns where id=object_id('[SeatType]') and name='depositeAmountMin')
	alter table [SeatType] add [depositeAmountMin] [int] NULL

/***
**********************************************************************
[HotelRoom]
**********************************************************************
*****/
/*�жϱ�[HotelRoom]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[HotelRoom]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [HotelRoom]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[HotelRoom].[oId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='oId')
	alter table [HotelRoom] add [oId] [nvarchar](max) NULL

/*[HotelRoom].[text]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='text')
	alter table [HotelRoom] add [text] [nvarchar](max) not NULL

/*[HotelRoom].[typeId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='typeId')
	alter table [HotelRoom] add [typeId] [int] not NULL

/*[HotelRoom].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='systemId')
	alter table [HotelRoom] add [systemId] [nvarchar](max) NULL

/*[HotelRoom].[name]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='name')
	alter table [HotelRoom] add [name] [nvarchar](max) NULL

/*[HotelRoom].[population]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='population')
	alter table [HotelRoom] add [population] [int] NULL

/*[HotelRoom].[openTime]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='openTime')
	alter table [HotelRoom] add [openTime] [datetime] NULL

/*[HotelRoom].[openEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='openEmployee')
	alter table [HotelRoom] add [openEmployee] [nvarchar](max) NULL

/*[HotelRoom].[payTime]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='payTime')
	alter table [HotelRoom] add [payTime] [datetime] NULL

/*[HotelRoom].[payEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='payEmployee')
	alter table [HotelRoom] add [payEmployee] [nvarchar](max) NULL

/*[HotelRoom].[phone]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='phone')
	alter table [HotelRoom] add [phone] [nvarchar](max) NULL

/*[HotelRoom].[discountEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='discountEmployee')
	alter table [HotelRoom] add [discountEmployee] [nvarchar](max) NULL

/*[HotelRoom].[discount]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='discount')
	alter table [HotelRoom] add [discount] [float] NULL

/*[HotelRoom].[memberDiscount]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='memberDiscount')
	alter table [HotelRoom] add [memberDiscount] [bit] NULL

/*[HotelRoom].[memberPromotionId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='memberPromotionId')
	alter table [HotelRoom] add [memberPromotionId] [nvarchar](max) NULL

/*[HotelRoom].[freeEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='freeEmployee')
	alter table [HotelRoom] add [freeEmployee] [nvarchar](max) NULL

/*[HotelRoom].[chainId]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='chainId')
	alter table [HotelRoom] add [chainId] [nvarchar](max) NULL

/*[HotelRoom].[status]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='status')
	alter table [HotelRoom] add [status] [int] not NULL

/*[HotelRoom].[ordering]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='ordering')
	alter table [HotelRoom] add [ordering] [bit] NULL

/*[HotelRoom].[paying]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='paying')
	alter table [HotelRoom] add [paying] [bit] NULL

/*[HotelRoom].[note]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='note')
	alter table [HotelRoom] add [note] [nvarchar](max) NULL

/*[HotelRoom].[unwarn]*/
if not exists (select * from syscolumns where id=object_id('[HotelRoom]') and name='unwarn')
	alter table [HotelRoom] add [unwarn] [nvarchar](max) NULL

/***
**********************************************************************
[Seat]
**********************************************************************
*****/
/*�жϱ�[Seat]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Seat]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Seat]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Seat].[oId]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='oId')
	alter table [Seat] add [oId] [nvarchar](max) NULL

/*[Seat].[text]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='text')
	alter table [Seat] add [text] [nvarchar](max) not NULL

/*[Seat].[typeId]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='typeId')
	alter table [Seat] add [typeId] [int] not NULL

/*[Seat].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='systemId')
	alter table [Seat] add [systemId] [nvarchar](max) NULL

/*[Seat].[name]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='name')
	alter table [Seat] add [name] [nvarchar](max) NULL

/*[Seat].[population]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='population')
	alter table [Seat] add [population] [int] NULL

/*[Seat].[openTime]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='openTime')
	alter table [Seat] add [openTime] [datetime] NULL

/*[Seat].[dueTime]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='dueTime')
	alter table [Seat] add [dueTime] [datetime] NULL

/*[Seat].[openEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='openEmployee')
	alter table [Seat] add [openEmployee] [nvarchar](max) NULL

/*[Seat].[payTime]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='payTime')
	alter table [Seat] add [payTime] [datetime] NULL

/*[Seat].[payEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='payEmployee')
	alter table [Seat] add [payEmployee] [nvarchar](max) NULL

/*[Seat].[phone]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='phone')
	alter table [Seat] add [phone] [nvarchar](max) NULL

/*[Seat].[discountEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='discountEmployee')
	alter table [Seat] add [discountEmployee] [nvarchar](max) NULL

/*[Seat].[discount]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='discount')
	alter table [Seat] add [discount] [float] NULL

/*[Seat].[memberDiscount]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='memberDiscount')
	alter table [Seat] add [memberDiscount] [bit] NULL

/*[Seat].[memberPromotionId]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='memberPromotionId')
	alter table [Seat] add [memberPromotionId] [nvarchar](max) NULL

/*[Seat].[freeEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='freeEmployee')
	alter table [Seat] add [freeEmployee] [nvarchar](max) NULL

/*[Seat].[chainId]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='chainId')
	alter table [Seat] add [chainId] [nvarchar](max) NULL

/*[Seat].[status]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='status')
	alter table [Seat] add [status] [int] not NULL

/*[Seat].[ordering]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='ordering')
	alter table [Seat] add [ordering] [bit] NULL

/*[Seat].[paying]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='paying')
	alter table [Seat] add [paying] [bit] NULL

/*[Seat].[note]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='note')
	alter table [Seat] add [note] [nvarchar](max) NULL

/*[Seat].[unwarn]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='unwarn')
	alter table [Seat] add [unwarn] [nvarchar](max) NULL

/*[Seat].[roomStatus]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='roomStatus')
	alter table [Seat] add [roomStatus] [nvarchar](max) NULL

/*[Seat].[deposit]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='deposit')
	alter table [Seat] add [deposit] [int] NULL

/*[Seat].[depositBank]*/
if not exists (select * from syscolumns where id=object_id('[Seat]') and name='depositBank')
	alter table [Seat] add [depositBank] [int] NULL

/***
**********************************************************************
[Orders]
**********************************************************************
*****/
/*�жϱ�[Orders]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Orders]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Orders]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Orders].[menu]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='menu')
	alter table [Orders] add [menu] [nvarchar](max) NULL

/*[Orders].[text]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='text')
	alter table [Orders] add [text] [nvarchar](max) not NULL

/*[Orders].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='systemId')
	alter table [Orders] add [systemId] [nvarchar](max) not NULL

/*[Orders].[number]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='number')
	alter table [Orders] add [number] [float] not NULL

/*[Orders].[priceType]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='priceType')
	alter table [Orders] add [priceType] [nvarchar](max) NULL

/*[Orders].[money]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='money')
	alter table [Orders] add [money] [float] not NULL

/*[Orders].[technician]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='technician')
	alter table [Orders] add [technician] [nvarchar](max) NULL

/*[Orders].[techType]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='techType')
	alter table [Orders] add [techType] [nvarchar](max) NULL

/*[Orders].[startTime]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='startTime')
	alter table [Orders] add [startTime] [datetime] NULL

/*[Orders].[inputTime]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='inputTime')
	alter table [Orders] add [inputTime] [datetime] not NULL

/*[Orders].[inputEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='inputEmployee')
	alter table [Orders] add [inputEmployee] [nvarchar](max) not NULL

/*[Orders].[deleteEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='deleteEmployee')
	alter table [Orders] add [deleteEmployee] [nvarchar](max) NULL

/*[Orders].[donorEmployee]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='donorEmployee')
	alter table [Orders] add [donorEmployee] [nvarchar](max) NULL

/*[Orders].[donorExplain]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='donorExplain')
	alter table [Orders] add [donorExplain] [nvarchar](max) NULL

/*[Orders].[donorTime]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='donorTime')
	alter table [Orders] add [donorTime] [datetime] NULL

/*[Orders].[comboId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='comboId')
	alter table [Orders] add [comboId] [int] NULL

/*[Orders].[paid]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='paid')
	alter table [Orders] add [paid] [bit] not NULL

/*[Orders].[accountId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='accountId')
	alter table [Orders] add [accountId] [int] NULL

/*[Orders].[billId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='billId')
	alter table [Orders] add [billId] [nvarchar](max) NULL

/*[Orders].[stopTiming]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='stopTiming')
	alter table [Orders] add [stopTiming] [bit] NULL

/*[Orders].[departmentId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='departmentId')
	alter table [Orders] add [departmentId] [int] NULL

/*[Orders].[deleteExplain]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='deleteExplain')
	alter table [Orders] add [deleteExplain] [nvarchar](max) NULL

/*[Orders].[deleteTime]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='deleteTime')
	alter table [Orders] add [deleteTime] [datetime] NULL

/*[Orders].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[Orders]') and name='roomId')
	alter table [Orders] add [roomId] [nvarchar](max) NULL

/***
**********************************************************************
[HisOrders]
**********************************************************************
*****/
/*�жϱ�[HisOrders]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[HisOrders]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [HisOrders]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[HisOrders].[menu]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='menu')
	alter table [HisOrders] add [menu] [nvarchar](max) NULL

/*[HisOrders].[text]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='text')
	alter table [HisOrders] add [text] [nvarchar](max) not NULL

/*[HisOrders].[systemId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='systemId')
	alter table [HisOrders] add [systemId] [nvarchar](max) not NULL

/*[HisOrders].[number]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='number')
	alter table [HisOrders] add [number] [float] not NULL

/*[HisOrders].[priceType]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='priceType')
	alter table [HisOrders] add [priceType] [nvarchar](max) NULL

/*[HisOrders].[money]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='money')
	alter table [HisOrders] add [money] [float] not NULL

/*[HisOrders].[technician]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='technician')
	alter table [HisOrders] add [technician] [nvarchar](max) NULL

/*[HisOrders].[techType]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='techType')
	alter table [HisOrders] add [techType] [nvarchar](max) NULL

/*[HisOrders].[startTime]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='startTime')
	alter table [HisOrders] add [startTime] [datetime] NULL

/*[HisOrders].[inputTime]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='inputTime')
	alter table [HisOrders] add [inputTime] [datetime] not NULL

/*[HisOrders].[inputEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='inputEmployee')
	alter table [HisOrders] add [inputEmployee] [nvarchar](max) not NULL

/*[HisOrders].[deleteEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='deleteEmployee')
	alter table [HisOrders] add [deleteEmployee] [nvarchar](max) NULL

/*[HisOrders].[donorEmployee]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='donorEmployee')
	alter table [HisOrders] add [donorEmployee] [nvarchar](max) NULL

/*[HisOrders].[donorExplain]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='donorExplain')
	alter table [HisOrders] add [donorExplain] [nvarchar](max) NULL

/*[HisOrders].[donorTime]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='donorTime')
	alter table [HisOrders] add [donorTime] [datetime] NULL

/*[HisOrders].[comboId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='comboId')
	alter table [HisOrders] add [comboId] [int] NULL

/*[HisOrders].[paid]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='paid')
	alter table [HisOrders] add [paid] [bit] not NULL

/*[HisOrders].[accountId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='accountId')
	alter table [HisOrders] add [accountId] [int] NULL

/*[HisOrders].[billId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='billId')
	alter table [HisOrders] add [billId] [nvarchar](max) NULL

/*[HisOrders].[departmentId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='departmentId')
	alter table [HisOrders] add [departmentId] [int] NULL

/*[HisOrders].[deleteExplain]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='deleteExplain')
	alter table [HisOrders] add [deleteExplain] [nvarchar](max) NULL

/*[HisOrders].[deleteTime]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='deleteTime')
	alter table [HisOrders] add [deleteTime] [datetime] NULL

/*[HisOrders].[roomId]*/
if not exists (select * from syscolumns where id=object_id('[HisOrders]') and name='roomId')
	alter table [HisOrders] add [roomId] [nvarchar](max) NULL



/***
**********************************************************************
[GroupBuy]
**********************************************************************
*****/
/*�жϱ�[GroupBuy]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[GroupBuy]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [GroupBuy]([id] [int] NOT NULL  primary key)

/*[GroupBuy].[name]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='name')
	alter table [GroupBuy] add [name] [nvarchar](max) not NULL

/*[GroupBuy].[money]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='money')
	alter table [GroupBuy] add [money] [float] not NULL

/*[GroupBuy].[issueDate]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='issueDate')
	alter table [GroupBuy] add [issueDate] [datetime] not NULL

/*[GroupBuy].[expireDate]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='expireDate')
	alter table [GroupBuy] add [expireDate] [datetime] NULL

/*[GroupBuy].[issueTransactor]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='issueTransactor')
	alter table [GroupBuy] add [issueTransactor] [nvarchar](max) NULL

/*[GroupBuy].[note]*/
if not exists (select * from syscolumns where id=object_id('[GroupBuy]') and name='note')
	alter table [GroupBuy] add [note] [nvarchar](max) NULL

/***
**********************************************************************
[ExpenseType]
**********************************************************************
*****/
/*�жϱ�[ExpenseType]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[ExpenseType]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [ExpenseType]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[ExpenseType].[name]*/
if not exists (select * from syscolumns where id=object_id('[ExpenseType]') and name='name')
	alter table [ExpenseType] add [name] [nvarchar](max) NULL

/***
**********************************************************************
[Expense]
**********************************************************************
*****/
/*�жϱ�[Expense]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Expense]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Expense]([id] [int] NOT NULL  primary key)

/*[Expense].[name]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='name')
	alter table [Expense] add [name] [nvarchar](max) not NULL

/*[Expense].[typeId]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='typeId')
	alter table [Expense] add [typeId] [int] NULL

/*[Expense].[money]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='money')
	alter table [Expense] add [money] [float] not NULL

/*[Expense].[payType]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='payType')
	alter table [Expense] add [payType] [nvarchar](max) not NULL

/*[Expense].[inputDate]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='inputDate')
	alter table [Expense] add [inputDate] [datetime] not NULL

/*[Expense].[expenseDate]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='expenseDate')
	alter table [Expense] add [expenseDate] [datetime] not NULL

/*[Expense].[toWhom]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='toWhom')
	alter table [Expense] add [toWhom] [nvarchar](max) NULL

/*[Expense].[transactor]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='transactor')
	alter table [Expense] add [transactor] [nvarchar](max) not NULL

/*[Expense].[tableMaker]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='tableMaker')
	alter table [Expense] add [tableMaker] [nvarchar](max) not NULL

/*[Expense].[checker]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='checker')
	alter table [Expense] add [checker] [nvarchar](max) not NULL

/*[Expense].[note]*/
if not exists (select * from syscolumns where id=object_id('[Expense]') and name='note')
	alter table [Expense] add [note] [nvarchar](max) NULL

/***
**********************************************************************
[Employee]
**********************************************************************
*****/
/*�жϱ�[Employee]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Employee]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Employee]([id] [varchar](10) NOT NULL  primary key)

/*[Employee].[name]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='name')
	alter table [Employee] add [name] [nvarchar](max) not NULL

/*[Employee].[cardId]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='cardId')
	alter table [Employee] add [cardId] [nvarchar](max) NULL

/*[Employee].[gender]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='gender')
	alter table [Employee] add [gender] [nvarchar](max) not NULL

/*[Employee].[birthday]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='birthday')
	alter table [Employee] add [birthday] [datetime] not NULL

/*[Employee].[jobId]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='jobId')
	alter table [Employee] add [jobId] [int] not NULL

/*[Employee].[onDuty]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='onDuty')
	alter table [Employee] add [onDuty] [bit] not NULL

/*[Employee].[salary]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='salary')
	alter table [Employee] add [salary] [nvarchar](max) NULL

/*[Employee].[password]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='password')
	alter table [Employee] add [password] [nvarchar](max) not NULL

/*[Employee].[phone]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='phone')
	alter table [Employee] add [phone] [nvarchar](max) not NULL

/*[Employee].[address]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='address')
	alter table [Employee] add [address] [nvarchar](max) NULL

/*[Employee].[email]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='email')
	alter table [Employee] add [email] [nvarchar](max) NULL

/*[Employee].[entryDate]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='entryDate')
	alter table [Employee] add [entryDate] [datetime] not NULL

/*[Employee].[idCard]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='idCard')
	alter table [Employee] add [idCard] [nvarchar](max) NULL

/*[Employee].[note]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='note')
	alter table [Employee] add [note] [nvarchar](max) NULL

/*[Employee].[status]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='status')
	alter table [Employee] add [status] [nvarchar](max) NULL

/*[Employee].[OrderClock]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='OrderClock')
	alter table [Employee] add [OrderClock] [bit] NULL

/*[Employee].[startTime]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='startTime')
	alter table [Employee] add [startTime] [datetime] NULL

/*[Employee].[serverTime]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='serverTime')
	alter table [Employee] add [serverTime] [int] NULL

/*[Employee].[techStatus]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='techStatus')
	alter table [Employee] add [techStatus] [nvarchar](max) NULL

/*[Employee].[room]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='room')
	alter table [Employee] add [room] [nvarchar](max) NULL

/*[Employee].[seat]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='seat')
	alter table [Employee] add [seat] [nvarchar](max) NULL

/*[Employee].[techMenu]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='techMenu')
	alter table [Employee] add [techMenu] [nvarchar](max) NULL

/*[Employee].[msgId]*/
if not exists (select * from syscolumns where id=object_id('[Employee]') and name='msgId')
	alter table [Employee] add [msgId] [int] NULL


/***
**********************************************************************
[Duty]
**********************************************************************
*****/
/*�жϱ�[Duty]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Duty]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Duty]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Duty].[dutyname]*/
if not exists (select * from syscolumns where id=object_id('[Duty]') and name='dutyname')
	alter table [Duty] add [dutyname] [nvarchar](max) NULL

/*[Duty].[startTime]*/
if not exists (select * from syscolumns where id=object_id('[Duty]') and name='startTime')
	alter table [Duty] add [startTime] [datetime] NULL

/*[Duty].[endTime]*/
if not exists (select * from syscolumns where id=object_id('[Duty]') and name='endTime')
	alter table [Duty] add [endTime] [datetime] NULL



/***
**********************************************************************
[Customer]
**********************************************************************
*****/
/*�жϱ�[Customer]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Customer]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Customer]([id] [int] NOT NULL  primary key)

/*[Customer].[name]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='name')
	alter table [Customer] add [name] [nvarchar](max) not NULL

/*[Customer].[contact]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='contact')
	alter table [Customer] add [contact] [nvarchar](max) NULL

/*[Customer].[address]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='address')
	alter table [Customer] add [address] [nvarchar](max) NULL

/*[Customer].[phone*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='phone')
	alter table [Customer] add [phone] [nvarchar](max) NULL

/*[Customer].[mobile]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='mobile')
	alter table [Customer] add [mobile] [nvarchar](max) not NULL

/*[Customer].[fax]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='fax')
	alter table [Customer] add [fax] [nvarchar](max) NULL

/*[Customer].[qq]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='qq')
	alter table [Customer] add [qq] [nvarchar](max) NULL

/*[Customer].[email]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='email')
	alter table [Customer] add [email] [nvarchar](max) NULL

/*[Customer].[money]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='money')
	alter table [Customer] add [money] [float] NULL

/*[Customer].[registerDate]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='registerDate')
	alter table [Customer] add [registerDate] [datetime] not NULL

/*[Customer].[mainBusiness]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='mainBusiness')
	alter table [Customer] add [mainBusiness] [nvarchar](max) NULL

/*[Customer].[note]*/
if not exists (select * from syscolumns where id=object_id('[Customer]') and name='note')
	alter table [Customer] add [note] [nvarchar](max) NULL


/***
**********************************************************************
[Coupon]
**********************************************************************
*****/
/*�жϱ�[Coupon]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Coupon]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Coupon]([pkey] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Coupon].[id]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='id')
	alter table [Coupon] add [id] [nvarchar](max) not NULL

/*[Coupon].[name]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='name')
	alter table [Coupon] add [name] [nvarchar](max) not NULL

/*[Coupon].[money]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='money')
	alter table [Coupon] add [money] [float] not NULL

/*[Coupon].[menuId]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='menuId')
	alter table [Coupon] add [menuId] [int] NULL

/*[Coupon].[issueDate]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='issueDate')
	alter table [Coupon] add [issueDate] [datetime] not NULL

/*[Coupon].[expireDate]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='expireDate')
	alter table [Coupon] add [expireDate] [datetime] not NULL

/*[Coupon].[issueTransator]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='issueTransator')
	alter table [Coupon] add [issueTransator] [nvarchar](max) not NULL

/*[Coupon].[note]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='note')
	alter table [Coupon] add [note] [nvarchar](max) NULL

/*[Coupon].[img]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='img')
	alter table [Coupon] add [img] [varbinary](max) NULL

/*[Coupon].[minAmount]*/
if not exists (select * from syscolumns where id=object_id('[Coupon]') and name='minAmount')
	alter table [Coupon] add [minAmount] [float] NULL


/***
**********************************************************************
[Combo]
**********************************************************************
*****/
/*�жϱ�[Combo]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Combo]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Combo]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[ClearTable].[originPrice]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='originPrice')
	alter table [Combo] add [originPrice] [float] not NULL

/*[ClearTable].[priceType]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='priceType')
	alter table [Combo] add [priceType] [nvarchar](max) not NULL

ALTER TABLE [Combo] ALTER COLUMN [priceType] [nvarchar](max) not NULL

/*[ClearTable].[price]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='price')
	alter table [Combo] add [price] [float] NULL

/*[ClearTable].[freePrice]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='freePrice')
	alter table [Combo] add [freePrice] [float] NULL

/*[ClearTable].[expenseUpTo]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='expenseUpTo')
	alter table [Combo] add [expenseUpTo] [float] NULL

/*[ClearTable].[menuIds]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='menuIds')
	alter table [Combo] add [menuIds] [nvarchar](max) NULL

/*[ClearTable].[freeMenuIds]*/
if not exists (select * from syscolumns where id=object_id('[Combo]') and name='freeMenuIds')
	alter table [Combo] add [freeMenuIds] [nvarchar](max) NULL

/***
**********************************************************************
[ClearTable]
**********************************************************************
*****/
/*�жϱ�[ClearTable]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[ClearTable]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [ClearTable]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[ClearTable].[clearTime]*/
if not exists (select * from syscolumns where id=object_id('[ClearTable]') and name='clearTime')
	alter table [ClearTable] add [clearTime] [datetime] not NULL

/*[ClearTable].[proceeded]*/
if not exists (select * from syscolumns where id=object_id('[ClearTable]') and name='proceeded')
	alter table [ClearTable] add [proceeded] bit NULL


/***
**********************************************************************
[ChainStore]
**********************************************************************
*****/
/*�жϱ�[Catgory]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[ChainStore]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [ChainStore]([id] [int] NOT NULL  primary key)

/*[ChainStore].name*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='name')
	alter table [ChainStore] add [name] [nvarchar](max) NULL

/*[ChainStore].[ip]*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='ip')
	alter table [ChainStore] add [ip] [varchar](16) NULL

/*[ChainStore].[address]*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='address')
	alter table [ChainStore] add [address] [nvarchar](max) NULL

/*[ChainStore].[phone]*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='phone')
	alter table [ChainStore] add [phone] [nvarchar](max) NULL

/*[ChainStore].[manager]*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='manager')
	alter table [ChainStore] add [manager] [nvarchar](max) NULL

/*[ChainStore].[mobile]*/
if not exists (select * from syscolumns where id=object_id('[ChainStore]') and name='mobile')
	alter table [ChainStore] add [mobile] [nvarchar](max) NULL



/***
**********************************************************************
[Catgory]
**********************************************************************
*****/
/*�жϱ�[Catgory]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Catgory]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [Catgory]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[Catgory].name*/
if not exists (select * from syscolumns where id=object_id('[Catgory]') and name='name')
	alter table [Catgory] add [name] [nvarchar](max) NOT NULL

/*[Catgory].[kitchPrinterName]*/
if not exists (select * from syscolumns where id=object_id('[Catgory]') and name='kitchPrinterName')
	alter table [Catgory] add [kitchPrinterName] [nvarchar](max) NULL

/***
**********************************************************************
[CashPrintTime]
**********************************************************************
*****/
/*�жϱ�[CashPrintTime]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CashPrintTime]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [CashPrintTime]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[CashPrintTime].[memberId]*/
if not exists (select * from syscolumns where id=object_id('[CashPrintTime]') and name='macAdd')
	alter table [CashPrintTime] add [macAdd] [nvarchar](max) NULL

/*[CashPrintTime].[time]*/
if not exists (select * from syscolumns where id=object_id('[CashPrintTime]') and name='time')
	alter table [CashPrintTime] add [time] [datetime] not NULL


/***
**********************************************************************
[CardSale]
**********************************************************************
*****/
/*�жϱ�[CardSale]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CardSale]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [CardSale]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[CardSale].[memberId]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='memberId')
	alter table [CardSale] add [memberId] [nvarchar](max) NULL

/*[CardSale].[seat]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='seat')
	alter table [CardSale] add [seat] [nvarchar](max) NULL

/*[CardSale].[balance]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='balance')
	alter table [CardSale] add [balance] [float] NULL

/*[CardSale].[cash]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='cash')
	alter table [CardSale] add [cash] [float] NULL

/*[CardSale].[bankUnion]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='bankUnion')
	alter table [CardSale] add [bankUnion] [float] NULL

/*[CardSale].[payTime]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='payTime')
	alter table [CardSale] add [payTime] [datetime] NULL

/*[CardSale].[payEmployee]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='payEmployee')
	alter table [CardSale] add [payEmployee] [nvarchar](max) NULL

/*[CardSale].[note]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='note')
	alter table [CardSale] add [note] [nvarchar](max) NULL

/*[CardSale].[macAddress]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='macAddress')
	alter table [CardSale] add [macAddress] [nvarchar](max) NULL

/*[CardSale].[explain]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='explain')
	alter table [CardSale] add [explain] [nvarchar](max) NULL

/*[CardSale].[server]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='server')
	alter table [CardSale] add [server] [float] NULL

/*[CardSale].[serverEmployee]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='serverEmployee')
	alter table [CardSale] add [serverEmployee] [nvarchar](max) NULL

/*[CardSale].[abandon]*/
if not exists (select * from syscolumns where id=object_id('[CardSale]') and name='abandon')
	alter table [CardSale] add [abandon] [nvarchar](max) NULL


/***
**********************************************************************
[CardPopSale]
**********************************************************************
*****/
/*�жϱ�[CardPopSale]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CardPopSale]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [CardPopSale]([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*[CardPopSale].[mimMoney]*/
if not exists (select * from syscolumns where id=object_id('[CardPopSale]') and name='mimMoney')
	alter table [CardPopSale] add [mimMoney] [int] NULL

/*[CardPopSale].[saleMoney]*/
if not exists (select * from syscolumns where id=object_id('[CardPopSale]') and name='saleMoney')
	alter table [CardPopSale] add [saleMoney] [int] NULL


/***
**********************************************************************
[CardInfo]
**********************************************************************
*****/
/*�жϱ�[CardInfo]�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CardInfo]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table [CardInfo]([CI_CardNo] [varchar](20) NOT NULL  primary key)

/*[CardInfo].[CI_SystemICNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SystemICNo')
	alter table [CardInfo] add [CI_SystemICNo] [varchar](20) NULL

/*[CardInfo].[CI_CardTypeNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CardTypeNo')
	alter table [CardInfo] add [CI_CardTypeNo] [int] NULL

/*[CardInfo].[CI_Name]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Name')
	alter table [CardInfo] add [CI_Name] [varchar](20) NULL

/*[CardInfo].[CI_Sexno]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Sexno')
	alter table [CardInfo] add [CI_Sexno] [varchar](4) NULL

/*[CardInfo].[CI_Address]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Address')
	alter table [CardInfo] add [CI_Address] [varchar](50) NULL

/*[CardInfo].[CI_Telephone]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Telephone')
	alter table [CardInfo] add [CI_Telephone] [varchar](30) NULL

/*[CardInfo].[CI_Remark]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Remark')
	alter table [CardInfo] add [CI_Remark] [varchar](200) NULL

/*[CardInfo].[CI_SendCardDate]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SendCardDate')
	alter table [CardInfo] add [CI_SendCardDate] [datetime] NULL

/*[CardInfo].[CI_SendCardOperator]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SendCardOperator')
	alter table [CardInfo] add [CI_SendCardOperator] [varchar](6) NULL

/*[CardInfo].[CI_DiscountRate]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_DiscountRate')
	alter table [CardInfo] add [CI_DiscountRate] [decimal](3, 2) NULL

/*[CardInfo].[CI_DiscountRatepos]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_DiscountRatepos')
	alter table [CardInfo] add [CI_DiscountRatepos] [decimal](3, 2) NULL

/*[CardInfo].[CI_Limitation]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Limitation')
	alter table [CardInfo] add [CI_Limitation] [money] NULL

/*[CardInfo].[CI_CardStateNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CardStateNo')
	alter table [CardInfo] add [CI_CardStateNo] [varchar](1) NULL

/*[CardInfo].[CI_UsefulLife]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_UsefulLife')
	alter table [CardInfo] add [CI_UsefulLife] [datetime] NULL

/*[CardInfo].[CI_SalesNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SalesNo')
	alter table [CardInfo] add [CI_SalesNo] [varchar](3) NULL

/*[CardInfo].[CI_Company]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Company')
	alter table [CardInfo] add [CI_Company] [varchar](50) NULL

/*[CardInfo].[CI_CardExplain]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CardExplain')
	alter table [CardInfo] add [CI_CardExplain] [text] NULL

/*[CardInfo].[CI_IsLock]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_IsLock')
	alter table [CardInfo] add [CI_IsLock] [bit] NULL

/*[CardInfo].[CI_PaperNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_PaperNo')
	alter table [CardInfo] add [CI_PaperNo] [varchar](18) NULL

/*[CardInfo].[CI_AccountNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_AccountNo')
	alter table [CardInfo] add [CI_AccountNo] [varchar](10) NULL

/*[CardInfo].[CI_Password]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Password')
	alter table [CardInfo] add [CI_Password] [nvarchar](max) NULL
else
	alter table [CardInfo] alter column [CI_Password] [nvarchar](max)

/*[CardInfo].[CI_Name]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Name')
	alter table [CardInfo] add [CI_Name] [varchar](20) NULL

/*[CardInfo].[CI_Integral]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Integral')
	alter table [CardInfo] add [CI_Integral] [numeric](12, 2) NULL

/*[CardInfo].[CI_Photo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Photo')
	alter table [CardInfo] add [CI_Photo] [image] NULL

/*[CardInfo].[CI_Station]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Station')
	alter table [CardInfo] add [CI_Station] [varchar](10) NULL

/*[CardInfo].[CI_ConsumeType]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_ConsumeType')
	alter table [CardInfo] add [CI_ConsumeType] [varchar](4) NULL

/*[CardInfo].[CI_State]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_State')
	alter table [CardInfo] add [CI_State] [varchar](1) NULL

/*[CardInfo].[CI_CheckDate]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CheckDate')
	alter table [CardInfo] add [CI_CheckDate] [datetime] NULL

/*[CardInfo].[CI_CheckOperator]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CheckOperator')
	alter table [CardInfo] add [CI_CheckOperator] [varchar](16) NULL

/*[CardInfo].[CI_SendFlag]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SendFlag')
	alter table [CardInfo] add [CI_SendFlag] [bit] NULL

/*[CardInfo].CI_CheckFlag*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CheckFlag')
	alter table [CardInfo] add [CI_CheckFlag] [bit] NULL

/*[CardInfo].[CI_CardAccountNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CardAccountNo')
	alter table [CardInfo] add [CI_CardAccountNo] [varchar](10) NULL

/*[CardInfo].[CI_EName]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_EName')
	alter table [CardInfo] add [CI_EName] [varchar](20) NULL

/*[CardInfo].[CI_Birthday]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Birthday')
	alter table [CardInfo] add [CI_Birthday] [datetime] NULL

/*[CardInfo].[CI_City]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_City')
	alter table [CardInfo] add [CI_City] [varchar](20) NULL

/*[CardInfo].[CI_Professional]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Professional')
	alter table [CardInfo] add [CI_Professional] [varchar](6) NULL

/*[CardInfo].[CI_Religions]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Religions')
	alter table [CardInfo] add [CI_Religions] [varchar](3) NULL

/*[CardInfo].[CI_Special1]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_Special1')
	alter table [CardInfo] add [CI_Special1] [varchar](20) NULL

/*[CardInfo].[CI_SpecialDate1]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_SpecialDate1')
	alter table [CardInfo] add [CI_SpecialDate1] [datetime] NULL

/*[CardInfo].[CI_VipNo]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_VipNo')
	alter table [CardInfo] add [CI_VipNo] [varchar](10) NULL

/*[CardInfo].[CI_CardCode]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CardCode')
	alter table [CardInfo] add [CI_CardCode] [varchar](3) NULL

/*[CardInfo].[CI_CreditsUsed]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='CI_CreditsUsed')
	alter table [CardInfo] add [CI_CreditsUsed] [int] NULL

/*[CardInfo].[birthday]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='birthday')
	alter table [CardInfo] add [birthday] [datetime] NULL

/*[CardInfo].[state]*/
if not exists (select * from syscolumns where id=object_id('[CardInfo]') and name='state')
	alter table [CardInfo] add [state] [varchar](20) NULL



/***
**********************************************************************
CardCharge
**********************************************************************
*****/
/*�жϱ�CardCharge�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[CardCharge]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table CardCharge([CC_Id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*CardCharge.CC_State*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_State')
	alter table CardCharge add CC_State [varchar](1) NULL

/*CardCharge.CC_CardNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CardNo')
	alter table CardCharge add CC_CardNo [varchar](20) NULL

/*CardCharge.CC_DepartmentNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_DepartmentNo')
	alter table CardCharge add CC_DepartmentNo [varchar](3) NULL

/*CardCharge.C_TableNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='C_TableNo')
	alter table CardCharge add C_TableNo [varchar](6) NULL

/*CardCharge.C_AccountNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='C_AccountNo')
	alter table CardCharge add C_AccountNo [varchar](10) NULL

/*CardCharge.CC_ItemNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_ItemNo')
	alter table CardCharge add CC_ItemNo [varchar](4) NULL

/*CardCharge.CC_ItemExplain*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_ItemExplain')
	alter table CardCharge add CC_ItemExplain [varchar](60) NULL

/*CardCharge.CC_BeginSum*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_BeginSum')
	alter table CardCharge add CC_BeginSum [float] NULL

/*CardCharge.CC_DebitSum*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_DebitSum')
	alter table CardCharge add CC_DebitSum [float] NULL

/*CardCharge.CC_LenderSum*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_LenderSum')
	alter table CardCharge add CC_LenderSum [float] NULL

/*CardCharge.CC_InputOperator*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_InputOperator')
	alter table CardCharge add CC_InputOperator [varchar](6) NULL

/*CardCharge.CC_InputDate*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_InputDate')
	alter table CardCharge add CC_InputDate [datetime] NULL

/*CardCharge.CC_OutOperator*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_OutOperator')
	alter table CardCharge add CC_OutOperator [varchar](6) NULL

/*CardCharge.CC_OutTime*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_OutTime')
	alter table CardCharge add CC_OutTime [datetime] NULL

/*CardCharge.CC_ChangeOperator*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_ChangeOperator')
	alter table CardCharge add CC_ChangeOperator [varchar](6) NULL

/*CardCharge.CC_ChangeDate*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_ChangeDate')
	alter table CardCharge add CC_ChangeDate [datetime] NULL

/*CardCharge.CC_OldCardNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_OldCardNo')
	alter table CardCharge add CC_OldCardNo [varchar](8) NULL

/*CardCharge.CC_DataType*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_DataType')
	alter table CardCharge add CC_DataType [varchar](50) NULL

/*CardCharge.CC_AuditDate*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_AuditDate')
	alter table CardCharge add CC_AuditDate [datetime] NULL

/*CardCharge.CC_CheckoutNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CheckoutNo')
	alter table CardCharge add CC_CheckoutNo [varchar](10) NULL

/*CardCharge.CC_SendFlag*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_SendFlag')
	alter table CardCharge add CC_SendFlag [bit] NULL

/*CardCharge.CC_CheckOperator*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CheckOperator')
	alter table CardCharge add CC_CheckOperator [varchar](20) NULL

/*CardCharge.[CC_CheckTime]*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CheckTime')
	alter table CardCharge add CC_CheckTime [datetime] NULL

/*CardCharge.CC_CheckFlag*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CheckFlag')
	alter table CardCharge add CC_CheckFlag [bit] NULL

/*CardCharge.CC_CheckoutNo*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_CheckoutNo')
	alter table CardCharge add CC_CheckoutNo [varchar](10) NULL

/*CardCharge.[CC_Station]*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='CC_Station')
	alter table CardCharge add [CC_Station] [varchar](8) NULL

/*CardCharge.[expense]*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='expense')
	alter table CardCharge add [expense] [float] NULL

/*CardCharge.[systemId]*/
if not exists (select * from syscolumns where id=object_id('CardCharge') and name='systemId')
	alter table CardCharge add [systemId] [nvarchar](max) NULL


/***
**********************************************************************
BarMsg
**********************************************************************
*****/
/*�жϱ�BarMsg�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[BarMsg]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table BarMsg([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*BarMsg.roomId*/
if not exists (select * from syscolumns where id=object_id('BarMsg') and name='roomId')
	alter table BarMsg add roomId [nvarchar](max) NULL

/*BarMsg.seatId*/
if not exists (select * from syscolumns where id=object_id('BarMsg') and name='seatId')
	alter table BarMsg add seatId [nvarchar](max) NULL

/*BarMsg.msg*/
if not exists (select * from syscolumns where id=object_id('BarMsg') and name='msg')
	alter table BarMsg add msg [nvarchar](max) NULL

/*BarMsg.time*/
if not exists (select * from syscolumns where id=object_id('BarMsg') and name='time')
	alter table BarMsg add time [datetime] not NULL

/*BarMsg.read*/
if not exists (select * from syscolumns where id=object_id('BarMsg') and name='read')
	alter table BarMsg add [read] [bit] NULL

/***
**********************************************************************
**********************************************************************
��Account
**********************************************************************
**********************************************************************
**********************************************************************
*****/
/*�жϱ�Account�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Account]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table Account([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*Account��text*/
if not exists (select * from syscolumns where id=object_id('Account') and name='text')
	alter table Account add text [nvarchar](max) NULL

/*Account��systemdId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='systemdId')
	alter table Account add systemdId [nvarchar](max) NULL

/*Account��openTime*/
if not exists (select * from syscolumns where id=object_id('Account') and name='openTime')
	alter table Account add openTime [nvarchar](max) NULL

/*Account��openEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='openEmployee')
	alter table Account add openEmployee [nvarchar](max) NULL

/*Account��payTime*/
if not exists (select * from syscolumns where id=object_id('Account') and name='payTime')
	alter table Account add payTime [datetime] not NULL

/*Account��payEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='payEmployee')
	alter table Account add payEmployee [nvarchar](max) not NULL

/*Account��name*/
if not exists (select * from syscolumns where id=object_id('Account') and name='name')
	alter table Account add name [nvarchar](max) NULL

/*Account��promotionMemberId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='promotionMemberId')
	alter table Account add promotionMemberId [nvarchar](max) NULL

/*Account��promotionAmount*/
if not exists (select * from syscolumns where id=object_id('Account') and name='promotionAmount')
	alter table Account add promotionAmount float NULL

/*Account��memberId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='memberId')
	alter table Account add memberId [nvarchar](max) NULL

/*Account��discountEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='discountEmployee')
	alter table Account add discountEmployee [nvarchar](max) NULL

/*Account��discount*/
if not exists (select * from syscolumns where id=object_id('Account') and name='discount')
	alter table Account add discount float NULL

/*Account��serverEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='serverEmployee')
	alter table Account add serverEmployee [nvarchar](max) NULL

/*Account��cash*/
if not exists (select * from syscolumns where id=object_id('Account') and name='cash')
	alter table Account add cash float NULL

/*Account��bankUnion*/
if not exists (select * from syscolumns where id=object_id('Account') and name='bankUnion')
	alter table Account add bankUnion float NULL

/*Account��creditCard*/
if not exists (select * from syscolumns where id=object_id('Account') and name='creditCard')
	alter table Account add creditCard float NULL

/*Account��coupon*/
if not exists (select * from syscolumns where id=object_id('Account') and name='coupon')
	alter table Account add coupon float NULL

/*Account��groupBuy*/
if not exists (select * from syscolumns where id=object_id('Account') and name='groupBuy')
	alter table Account add groupBuy float NULL

/*Account��zero*/
if not exists (select * from syscolumns where id=object_id('Account') and name='zero')
	alter table Account add zero float NULL

/*Account��server*/
if not exists (select * from syscolumns where id=object_id('Account') and name='server')
	alter table Account add server float NULL

/*Account��deducted*/
if not exists (select * from syscolumns where id=object_id('Account') and name='deducted')
	alter table Account add deducted float NULL

/*Account��changes*/
if not exists (select * from syscolumns where id=object_id('Account') and name='changes')
	alter table Account add changes float NULL

/*Account��wipeZero*/
if not exists (select * from syscolumns where id=object_id('Account') and name='wipeZero')
	alter table Account add wipeZero float NULL

/*Account��macAddress*/
if not exists (select * from syscolumns where id=object_id('Account') and name='macAddress')
	alter table Account add macAddress [nvarchar](max) NULL

/*Account��abandon*/
if not exists (select * from syscolumns where id=object_id('Account') and name='abandon')
	alter table Account add abandon [nvarchar](max) NULL

/*Account��departmentId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='departmentId')
	alter table Account add departmentId [int] NULL



/***
**********************************************************************
**********************************************************************
��Apk
**********************************************************************
**********************************************************************
**********************************************************************
*****/

/*�жϱ�Apk�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[Apk]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table Apk([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*Apk.name*/
if not exists (select * from syscolumns where id=object_id('Apk') and name='name')
	alter table Apk add name [nvarchar](max) NULL

/*Apk.path*/
if not exists (select * from syscolumns where id=object_id('Apk') and name='path')
	alter table Apk add path [nvarchar](max) NULL

/*Apk.binary*/
if not exists (select * from syscolumns where id=object_id('Apk') and name='binary')
	alter table Apk add binary [varbinary](max) NULL

/*Apk.version*/
if not exists (select * from syscolumns where id=object_id('Apk') and name='version')
	alter table Apk add version [nvarchar](max) NULL

/*Apk.size*/
if not exists (select * from syscolumns where id=object_id('Apk') and name='size')
	alter table Apk add size [bigint] NULL



/***
**********************************************************************
**********************************************************************
��Authority
**********************************************************************
**********************************************************************
**********************************************************************
*****/

/*�жϱ�Authority�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'Authority') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table Authority([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*Authority.emplyeeId*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='emplyeeId')
	alter table Authority add emplyeeId [varchar](10) NULL

/*Authority.jobId*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='jobId')
	alter table Authority add jobId [int] NULL

/*Authority.[����]*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='����')
	alter table Authority add ���� [bit] NULL

/*Authority.ȡ������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ȡ������')
	alter table Authority add ȡ������ [bit] NULL

/*Authority.΢������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='΢������')
	alter table Authority add ΢������ [bit] NULL

/*Authority.��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������')
	alter table Authority add �������� [bit] NULL

/*Authority.��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������')
	alter table Authority add �������� [bit] NULL

/*Authority.�������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�������')
	alter table Authority add ������� [bit] NULL

/*Authority.ͣ������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ͣ������')
	alter table Authority add ͣ������ [bit] NULL

/*Authority.��ӱ�ע*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��ӱ�ע')
	alter table Authority add ��ӱ�ע [bit] NULL

/*Authority.��ʧ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��ʧ����')
	alter table Authority add ��ʧ���� [bit] NULL

/*Authority.�����㵥*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�����㵥')
	alter table Authority add �����㵥 [bit] NULL

/*Authority.�ɼ����˵㵥*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ɼ����˵㵥')
	alter table Authority add �ɼ����˵㵥 [bit] NULL

/*Authority.�˵�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�˵�')
	alter table Authority add �˵� [bit] NULL

/*Authority.�ֹ�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ֹ�����')
	alter table Authority add �ֹ����� [bit] NULL

/*Authority.ǩ���ⵥ*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ǩ���ⵥ')
	alter table Authority add ǩ���ⵥ [bit] NULL

/*Authority.ת��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ת��')
	alter table Authority add ת�� [bit] NULL

/*Authority.���½���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���½���')
	alter table Authority add ���½��� [bit] NULL

/*Authority.����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='����')
	alter table Authority add ���� [bit] NULL

/*Authority.��ʦ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��ʦ����')
	alter table Authority add ��ʦ���� [bit] NULL

/*Authority.��������ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������ͳ��')
	alter table Authority add ��������ͳ�� [bit] NULL

/*Authority.��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������')
	alter table Authority add �������� [bit] NULL

/*Authority.�������ݲ�ѯ*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�������ݲ�ѯ')
	alter table Authority add �������ݲ�ѯ [bit] NULL

/*Authority.¼������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='¼������')
	alter table Authority add ¼������ [bit] NULL

/*Authority.Ӫҵ��Ϣ�鿴*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='Ӫҵ��Ϣ�鿴')
	alter table Authority add Ӫҵ��Ϣ�鿴 [bit] NULL

/*Authority.�ۿ�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ۿ�')
	alter table Authority add �ۿ� [bit] NULL

/*Authority.��ֵ*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��ֵ')
	alter table Authority add ��ֵ [bit] NULL

/*Authority.��ʧ*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��ʧ')
	alter table Authority add ��ʧ [bit] NULL

/*Authority.����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='����')
	alter table Authority add ���� [bit] NULL

/*Authority.����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='����')
	alter table Authority add ���� [bit] NULL

/*Authority.�ۿ�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ۿ�')
	alter table Authority add �ۿ� [bit] NULL

/*Authority.�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�����')
	alter table Authority add ����� [bit] NULL

/*Authority.�쳣״��ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�쳣״��ͳ��')
	alter table Authority add �쳣״��ͳ�� [bit] NULL

/*Authority.���ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���ͳ��')
	alter table Authority add ���ͳ�� [bit] NULL

/*Authority.�ֹ����ۻ���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ֹ����ۻ���')
	alter table Authority add �ֹ����ۻ��� [bit] NULL

/*Authority.��Ŀ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ŀ����')
	alter table Authority add ��Ŀ���� [bit] NULL

/*Authority.���ÿ�ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���ÿ�ͳ��')
	alter table Authority add ���ÿ�ͳ�� [bit] NULL

/*Authority.Ӫҵ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='Ӫҵ����')
	alter table Authority add Ӫҵ���� [bit] NULL

/*Authority.���ⵥ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���ⵥ����')
	alter table Authority add ���ⵥ���� [bit] NULL

/*Authority.֧��ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='֧��ͳ��')
	alter table Authority add ֧��ͳ�� [bit] NULL

/*Authority.����Ա�տ�ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='����Ա�տ�ͳ��')
	alter table Authority add ����Ա�տ�ͳ�� [bit] NULL

/*Authority.�±���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�±���')
	alter table Authority add �±��� [bit] NULL

/*Authority.������λ��Ŀ*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='������λ��Ŀ')
	alter table Authority add ������λ��Ŀ [bit] NULL

/*Authority.�ۿ�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ۿ�')
	alter table Authority add �ۿ� [bit] NULL

/*Authority.��Ա��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ա��������')
	alter table Authority add ��Ա�������� [bit] NULL

/*Authority.�Żݷ���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�Żݷ���')
	alter table Authority add �Żݷ��� [bit] NULL

/*Authority.��Ա����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ա����')
	alter table Authority add ��Ա���� [bit] NULL

/*Authority.��Ա����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ա����')
	alter table Authority add ��Ա���� [bit] NULL

/*Authority.��Ա����ͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ա����ͳ��')
	alter table Authority add ��Ա����ͳ�� [bit] NULL

/*Authority.��Ա�ۿ�����ֵͳ��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ա�ۿ�����ֵͳ��')
	alter table Authority add ��Ա�ۿ�����ֵͳ�� [bit] NULL

/*Authority.���ƹ���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���ƹ���')
	alter table Authority add ���ƹ��� [bit] NULL

/*Authority.ȯ�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ȯ�����')
	alter table Authority add ȯ����� [bit] NULL

/*Authority.�ͻ�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ͻ�����')
	alter table Authority add �ͻ����� [bit] NULL

/*Authority.��Ŀ��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��Ŀ��������')
	alter table Authority add ��Ŀ�������� [bit] NULL

/*Authority.�ͷ�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ͷ�����')
	alter table Authority add �ͷ����� [bit] NULL

/*Authority.Ա������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='Ա������')
	alter table Authority add Ա������ [bit] NULL

/*Authority.Ȩ�޹���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='Ȩ�޹���')
	alter table Authority add Ȩ�޹��� [bit] NULL

/*Authority.�ײ͹���*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ײ͹���')
	alter table Authority add �ײ͹��� [bit] NULL

/*Authority.������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='������')
	alter table Authority add ������ [bit] NULL

/*Authority.�ֿ��趨*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�ֿ��趨')
	alter table Authority add �ֿ��趨 [bit] NULL

/*Authority.������׼*/
if exists (select * from syscolumns where id=object_id('Authority') and name='������׼')
	exec sp_rename 'Authority.������׼','��Ӧ�̹���','column';

/*Authority.�������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�������')
	alter table Authority add ������� [bit] NULL

/*Authority.���п��*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='���п��')
	alter table Authority add ���п�� [bit] NULL

/*Authority.��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������')
	alter table Authority add �������� [bit] NULL

/*Authority.�̵����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�̵����')
	alter table Authority add �̵���� [bit] NULL

/*Authority.�̵����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�̵����')
	alter table Authority add �̵���� [bit] NULL

/*Authority.Ӧ���˿�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='Ӧ���˿�')
	alter table Authority add Ӧ���˿� [bit] NULL

/*Authority.ϵͳ����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='ϵͳ����')
	alter table Authority add ϵͳ���� [bit] NULL

/*Authority.�����Ż�*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�����Ż�')
	alter table Authority add �����Ż� [bit] NULL

/*Authority.��������*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='��������')
	alter table Authority add �������� [bit] NULL

/*Authority.�Ź�����*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='�Ź�����')
	alter table Authority add �Ź����� [bit] NULL
	
	/***
**********************************************************************
**********************************************************************
��BigCombo
**********************************************************************
**********************************************************************
**********************************************************************
*****/
/*�жϱ�BigCombo�Ƿ����*/
if not exists (select * from sysobjects where id=object_id(N'[BigCombo]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table BigCombo([id] [int] IDENTITY(1,1) NOT NULL  primary key)
	
if not exists (select * from syscolumns where id=object_id('BigCombo') and name='menuid')
	alter table BigCombo add menuid [int] NOT NULL

if not exists (select * from syscolumns where id=object_id('BigCombo') and name='price')
	alter table BigCombo add price [float] NULL

if not exists (select * from syscolumns where id=object_id('BigCombo') and name='substmenuid')
	alter table BigCombo add substmenuid [nvarchar](max) NULL	
	
if not exists (select * from syscolumns where id=object_id('BigCombo') and name='note')
	alter table BigCombo add note [nvarchar](max) NULL	

if not exists(select * from Job where name='����')
	insert into Job(id,name) values(1,'����')

if not exists(select * from Employee where id='1000')
	insert into Employee(id,name,gender,birthday,jobId,onDuty,password,phone,entryDate) 
	select '1000','ϵͳ','��',getdate(),id,1,'4IhfVN1IxdD5Az82+2Fjxg==','1',getdate() from Job

if exists(select * from Authority where emplyeeid='1000')
	delete from Authority where emplyeeid='1000'

insert into Authority(emplyeeid,
����,			ȡ������,		΢������,			��������,		��������,	�������,		ͣ������,		��ӱ�ע,		��ʧ����,		�����㵥,		�ɼ����˵㵥,
�˵�,			�ֹ�����,		ǩ���ⵥ,			ת��,			���½���,	����,			��ʦ����,		��������ͳ��,	��������,		�������ݲ�ѯ,	¼������,
Ӫҵ��Ϣ�鿴,	�ۿ�,			��ֵ,				��ʧ,			����,		����,			�ۿ�,			�����,			�쳣״��ͳ��,	���ͳ��,		�ֹ����ۻ���,
��Ŀ����,		���ÿ�ͳ��,		Ӫҵ����,			���ⵥ����,		֧��ͳ��,	����Ա�տ�ͳ��,	�±���,			������λ��Ŀ,	��Ա��������,	�Żݷ���,		��Ա����,
��Ա����,		��Ա����ͳ��,	��Ա�ۿ�����ֵͳ��,	���ƹ���,		ȯ�����,	�ͻ�����,		��Ŀ��������,	�ͷ�����,		Ա������,		Ȩ�޹���,		�Ź�����,
�ײ͹���,		������,		�ֿ��趨,			�������,		���п��,	��������,		�̵����,		�̵����,		Ӧ���˿�,		ϵͳ����,		�����Ż�,
��������) values('1000',
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1)