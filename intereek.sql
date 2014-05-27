USE [master]

GO
/****** 对象:  Database [BathDB]    脚本日期: 05/27/2014 18:54:04 ******/
if not exists(select 1 from master..sysdatabases where name='bathdb')
begin
CREATE DATABASE [BathDB] ON  PRIMARY 
( NAME = N'BathDB', FILENAME = N'D:\连客科技\DB\BathDB.mdf' , SIZE = 55296KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BathDB_log', FILENAME = N'D:\连客科技\DB\BathDB_log.ldf' , SIZE = 16576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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

/*判断表[CustomerPays]是否存在*/
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
[UploadRecords]数据库上传记录
**********************************************************************
*****/
/*判断表[UploadRecords]是否存在*/
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
[CustomerPays]供应商付款
**********************************************************************
*****/
/*判断表[CustomerPays]是否存在*/
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
[ProviderPays]供应商付款
**********************************************************************
*****/
/*判断表[ProviderPays]是否存在*/
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
[Provider]盘点记录
**********************************************************************
*****/
/*判断表[Provider]是否存在*/
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
[Pan]盘点记录
**********************************************************************
*****/
/*判断表[Pan]是否存在*/
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
[OrderStockOut]销售自动出库
**********************************************************************
*****/
/*判断表[OrderStockOut]是否存在*/
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
/*判断表[SystemIds]是否存在*/
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
/*判断表[GoodsCat]是否存在*/
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
/*判断表[Room]是否存在*/
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
/*判断表[WaiterItem]是否存在*/
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
/*判断表[Unit]是否存在*/
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
/*判断表[TechReturn]是否存在*/
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
/*判断表[TechReservation]是否存在*/
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
/*判断表[TechMsg]是否存在*/
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
/*判断表[TechIndex]是否存在*/
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
/*判断表[StorageList]是否存在*/
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
/*判断表[StockOut]是否存在*/
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
/*判断表[StockIn]是否存在*/
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
/*判断表[Stock]是否存在*/
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
/*判断表[ShoeMsg]是否存在*/
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
/*判断表[RoomWarn]是否存在*/
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
/*判断表[RoomReserveMsg]是否存在*/
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
/*判断表[RoomCall]是否存在*/
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
/*判断表[Promotion]是否存在*/
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
/*判断表[GroupBuyPromotion]是否存在*/
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
/*判断表[PayMsg]是否存在*/
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
/*判断表[Options]是否存在*/
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

/*[Options].[取消开牌时限]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='取消开牌时限')
	alter table [Options] add [取消开牌时限] [int] NULL

/*[Options].[取消开房时限]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='取消开房时限')
	alter table [Options] add [取消开房时限] [int] NULL

/*[Options].[删除支出时限]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='删除支出时限')
	alter table [Options] add [删除支出时限] [int] NULL

/*[Options].[退钟时限]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='退钟时限')
	alter table [Options] add [退钟时限] [int] NULL

/*[Options].[技师条数]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='技师条数')
	alter table [Options] add [技师条数] [int] NULL

/*[Options].[启用鞋部]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用鞋部')
	alter table [Options] add [启用鞋部] [bit] NULL

/*[Options].[鞋部条数]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='鞋部条数')
	alter table [Options] add [鞋部条数] [int] NULL

/*[Options].[启用会员卡密码]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用会员卡密码')
	alter table [Options] add [启用会员卡密码] [bit] NULL

/*[Options].[会员卡密码类型]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='会员卡密码类型')
	alter table [Options] add [会员卡密码类型] [nvarchar](max) NULL

/*[Options].[启用结账监控]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用结账监控')
	alter table [Options] add [启用结账监控] [bit] NULL

/*[Options].[结账视频长度]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='结账视频长度')
	alter table [Options] add [结账视频长度] [nvarchar](max) NULL

/*[Options].[启用手牌锁]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用手牌锁')
	alter table [Options] add [启用手牌锁] [bit] NULL

/*[Options].[开业时间]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='开业时间')
	alter table [Options] add [开业时间] [int] NULL

/*[Options].[启用客房面板]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用客房面板')
	alter table [Options] add [启用客房面板] [bit] NULL

/*[Options].[包房等待时限]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='包房等待时限')
	alter table [Options] add [包房等待时限] [int] NULL

/*[Options].[下钟提醒]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='下钟提醒')
	alter table [Options] add [下钟提醒] [int] NULL

/*[Options].[启用ID手牌锁]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用ID手牌锁')
	alter table [Options] add [启用ID手牌锁] [bit] NULL

/*[Options].[允许手工输入手牌号开牌]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='允许手工输入手牌号开牌')
	alter table [Options] add [允许手工输入手牌号开牌] [bit] NULL

/*[Options].[允许手工输入手牌号结账]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='允许手工输入手牌号结账')
	alter table [Options] add [允许手工输入手牌号结账] [bit] NULL

/*[Options].[录单输入单据编号]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='录单输入单据编号')
	alter table [Options] add [录单输入单据编号] [bit] NULL

/*[Options].[结账未打单锁定手牌]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='结账未打单锁定手牌')
	alter table [Options] add [结账未打单锁定手牌] [bit] NULL

/*[Options].[营业报表格式]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='营业报表格式')
	alter table [Options] add [营业报表格式] [int] NULL

/*[Options].[提成报表格式]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='提成报表格式')
	alter table [Options] add [提成报表格式] [int] NULL

/*[Options].[结账打印结账单]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='结账打印结账单')
	alter table [Options] add [结账打印结账单] [bit] NULL

/*[Options].[结账打印存根单]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='结账打印存根单')
	alter table [Options] add [结账打印存根单] [bit] NULL

/*[Options].[结账打印取鞋小票]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='结账打印取鞋小票')
	alter table [Options] add [结账打印取鞋小票] [bit] NULL

/*[Options].[抹零限制]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='抹零限制')
	alter table [Options] add [抹零限制] [int] NULL

/*[Options].[手牌锁类型]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='手牌锁类型')
	alter table [Options] add [手牌锁类型] [nvarchar](max) NULL

/*[Options].[自动加收过夜费]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='自动加收过夜费')
	alter table [Options] add [自动加收过夜费] [bit] NULL

/*[Options].[过夜费起点]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='过夜费起点')
	alter table [Options] add [过夜费起点] [nvarchar](max) NULL

/*[Options].[过夜费终点]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='过夜费终点')
	alter table [Options] add [过夜费终点] [nvarchar](max) NULL

/*[Options].[启用分单结账]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用分单结账')
	alter table [Options] add [启用分单结账] [bit] NULL

/*[Options].[启用员工服务卡]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用员工服务卡')
	alter table [Options] add [启用员工服务卡] [bit] NULL

/*[Options].[台位类型分页显示]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='台位类型分页显示')
	alter table [Options] add [台位类型分页显示] [bit] NULL

/*[Options].[自动感应手牌]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='自动感应手牌')
	alter table [Options] add [自动感应手牌] [bit] NULL

/*[Options].[录单区分点钟轮钟]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='录单区分点钟轮钟')
	alter table [Options] add [录单区分点钟轮钟] [bit] NULL

/*[Options].[打印技师派遣单]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='打印技师派遣单')
	alter table [Options] add [打印技师派遣单] [bit] NULL

/*[Options].[启用大项拆分]*/
if not exists (select * from syscolumns where id=object_id('[Options]') and name='启用大项拆分')
	alter table [Options] add [启用大项拆分] [bit] NULL



/***
**********************************************************************
[Operation]
**********************************************************************
*****/
/*判断表[Operation]是否存在*/
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
/*判断表[Menu]是否存在*/
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
/*判断表[MemberType]是否存在*/
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
/*判断表[MemberSetting]是否存在*/
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
/*判断表[Department]是否存在*/
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
/*判断表[DepartmentLog]是否存在*/
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
/*判断表[Job]是否存在*/
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
/*判断表[HotelRoomType]是否存在*/
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
/*判断表[SeatType]是否存在*/
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
/*判断表[HotelRoom]是否存在*/
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
/*判断表[Seat]是否存在*/
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
/*判断表[Orders]是否存在*/
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
/*判断表[HisOrders]是否存在*/
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
/*判断表[GroupBuy]是否存在*/
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
/*判断表[ExpenseType]是否存在*/
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
/*判断表[Expense]是否存在*/
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
/*判断表[Employee]是否存在*/
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
/*判断表[Duty]是否存在*/
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
/*判断表[Customer]是否存在*/
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
/*判断表[Coupon]是否存在*/
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
/*判断表[Combo]是否存在*/
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
/*判断表[ClearTable]是否存在*/
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
/*判断表[Catgory]是否存在*/
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
/*判断表[Catgory]是否存在*/
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
/*判断表[CashPrintTime]是否存在*/
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
/*判断表[CardSale]是否存在*/
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
/*判断表[CardPopSale]是否存在*/
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
/*判断表[CardInfo]是否存在*/
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
/*判断表CardCharge是否存在*/
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
/*判断表BarMsg是否存在*/
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
表Account
**********************************************************************
**********************************************************************
**********************************************************************
*****/
/*判断表Account是否存在*/
if not exists (select * from sysobjects where id=object_id(N'[Account]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table Account([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*Account列text*/
if not exists (select * from syscolumns where id=object_id('Account') and name='text')
	alter table Account add text [nvarchar](max) NULL

/*Account列systemdId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='systemdId')
	alter table Account add systemdId [nvarchar](max) NULL

/*Account列openTime*/
if not exists (select * from syscolumns where id=object_id('Account') and name='openTime')
	alter table Account add openTime [nvarchar](max) NULL

/*Account列openEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='openEmployee')
	alter table Account add openEmployee [nvarchar](max) NULL

/*Account列payTime*/
if not exists (select * from syscolumns where id=object_id('Account') and name='payTime')
	alter table Account add payTime [datetime] not NULL

/*Account列payEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='payEmployee')
	alter table Account add payEmployee [nvarchar](max) not NULL

/*Account列name*/
if not exists (select * from syscolumns where id=object_id('Account') and name='name')
	alter table Account add name [nvarchar](max) NULL

/*Account列promotionMemberId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='promotionMemberId')
	alter table Account add promotionMemberId [nvarchar](max) NULL

/*Account列promotionAmount*/
if not exists (select * from syscolumns where id=object_id('Account') and name='promotionAmount')
	alter table Account add promotionAmount float NULL

/*Account列memberId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='memberId')
	alter table Account add memberId [nvarchar](max) NULL

/*Account列discountEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='discountEmployee')
	alter table Account add discountEmployee [nvarchar](max) NULL

/*Account列discount*/
if not exists (select * from syscolumns where id=object_id('Account') and name='discount')
	alter table Account add discount float NULL

/*Account列serverEmployee*/
if not exists (select * from syscolumns where id=object_id('Account') and name='serverEmployee')
	alter table Account add serverEmployee [nvarchar](max) NULL

/*Account列cash*/
if not exists (select * from syscolumns where id=object_id('Account') and name='cash')
	alter table Account add cash float NULL

/*Account列bankUnion*/
if not exists (select * from syscolumns where id=object_id('Account') and name='bankUnion')
	alter table Account add bankUnion float NULL

/*Account列creditCard*/
if not exists (select * from syscolumns where id=object_id('Account') and name='creditCard')
	alter table Account add creditCard float NULL

/*Account列coupon*/
if not exists (select * from syscolumns where id=object_id('Account') and name='coupon')
	alter table Account add coupon float NULL

/*Account列groupBuy*/
if not exists (select * from syscolumns where id=object_id('Account') and name='groupBuy')
	alter table Account add groupBuy float NULL

/*Account列zero*/
if not exists (select * from syscolumns where id=object_id('Account') and name='zero')
	alter table Account add zero float NULL

/*Account列server*/
if not exists (select * from syscolumns where id=object_id('Account') and name='server')
	alter table Account add server float NULL

/*Account列deducted*/
if not exists (select * from syscolumns where id=object_id('Account') and name='deducted')
	alter table Account add deducted float NULL

/*Account列changes*/
if not exists (select * from syscolumns where id=object_id('Account') and name='changes')
	alter table Account add changes float NULL

/*Account列wipeZero*/
if not exists (select * from syscolumns where id=object_id('Account') and name='wipeZero')
	alter table Account add wipeZero float NULL

/*Account列macAddress*/
if not exists (select * from syscolumns where id=object_id('Account') and name='macAddress')
	alter table Account add macAddress [nvarchar](max) NULL

/*Account列abandon*/
if not exists (select * from syscolumns where id=object_id('Account') and name='abandon')
	alter table Account add abandon [nvarchar](max) NULL

/*Account列departmentId*/
if not exists (select * from syscolumns where id=object_id('Account') and name='departmentId')
	alter table Account add departmentId [int] NULL



/***
**********************************************************************
**********************************************************************
表Apk
**********************************************************************
**********************************************************************
**********************************************************************
*****/

/*判断表Apk是否存在*/
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
表Authority
**********************************************************************
**********************************************************************
**********************************************************************
*****/

/*判断表Authority是否存在*/
if not exists (select * from sysobjects where id=object_id(N'Authority') and OBJECTPROPERTY(id, N'IsUserTable')=1)
	create table Authority([id] [int] IDENTITY(1,1) NOT NULL  primary key)

/*Authority.emplyeeId*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='emplyeeId')
	alter table Authority add emplyeeId [varchar](10) NULL

/*Authority.jobId*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='jobId')
	alter table Authority add jobId [int] NULL

/*Authority.[开牌]*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='开牌')
	alter table Authority add 开牌 [bit] NULL

/*Authority.取消开牌*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='取消开牌')
	alter table Authority add 取消开牌 [bit] NULL

/*Authority.微信赠送*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='微信赠送')
	alter table Authority add 微信赠送 [bit] NULL

/*Authority.更换手牌*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='更换手牌')
	alter table Authority add 更换手牌 [bit] NULL

/*Authority.锁定解锁*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='锁定解锁')
	alter table Authority add 锁定解锁 [bit] NULL

/*Authority.解除警告*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='解除警告')
	alter table Authority add 解除警告 [bit] NULL

/*Authority.停用启用*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='停用启用')
	alter table Authority add 停用启用 [bit] NULL

/*Authority.添加备注*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='添加备注')
	alter table Authority add 添加备注 [bit] NULL

/*Authority.挂失手牌*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='挂失手牌')
	alter table Authority add 挂失手牌 [bit] NULL

/*Authority.完整点单*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='完整点单')
	alter table Authority add 完整点单 [bit] NULL

/*Authority.可见本人点单*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='可见本人点单')
	alter table Authority add 可见本人点单 [bit] NULL

/*Authority.退单*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='退单')
	alter table Authority add 退单 [bit] NULL

/*Authority.手工打折*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='手工打折')
	alter table Authority add 手工打折 [bit] NULL

/*Authority.签字免单*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='签字免单')
	alter table Authority add 签字免单 [bit] NULL

/*Authority.转账*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='转账')
	alter table Authority add 转账 [bit] NULL

/*Authority.重新结账*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='重新结账')
	alter table Authority add 重新结账 [bit] NULL

/*Authority.结账*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='结账')
	alter table Authority add 结账 [bit] NULL

/*Authority.技师管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='技师管理')
	alter table Authority add 技师管理 [bit] NULL

/*Authority.收银汇总统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='收银汇总统计')
	alter table Authority add 收银汇总统计 [bit] NULL

/*Authority.包房管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='包房管理')
	alter table Authority add 包房管理 [bit] NULL

/*Authority.收银单据查询*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='收银单据查询')
	alter table Authority add 收银单据查询 [bit] NULL

/*Authority.录单汇总*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='录单汇总')
	alter table Authority add 录单汇总 [bit] NULL

/*Authority.营业信息查看*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='营业信息查看')
	alter table Authority add 营业信息查看 [bit] NULL

/*Authority.售卡*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='售卡')
	alter table Authority add 售卡 [bit] NULL

/*Authority.充值*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='充值')
	alter table Authority add 充值 [bit] NULL

/*Authority.挂失*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='挂失')
	alter table Authority add 挂失 [bit] NULL

/*Authority.补卡*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='补卡')
	alter table Authority add 补卡 [bit] NULL

/*Authority.读卡*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='读卡')
	alter table Authority add 读卡 [bit] NULL

/*Authority.扣卡*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='扣卡')
	alter table Authority add 扣卡 [bit] NULL

/*Authority.卡入库*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='卡入库')
	alter table Authority add 卡入库 [bit] NULL

/*Authority.异常状况统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='异常状况统计')
	alter table Authority add 异常状况统计 [bit] NULL

/*Authority.提成统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='提成统计')
	alter table Authority add 提成统计 [bit] NULL

/*Authority.手工打折汇总*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='手工打折汇总')
	alter table Authority add 手工打折汇总 [bit] NULL

/*Authority.项目报表*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='项目报表')
	alter table Authority add 项目报表 [bit] NULL

/*Authority.信用卡统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='信用卡统计')
	alter table Authority add 信用卡统计 [bit] NULL

/*Authority.营业报表*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='营业报表')
	alter table Authority add 营业报表 [bit] NULL

/*Authority.退免单汇总*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='退免单汇总')
	alter table Authority add 退免单汇总 [bit] NULL

/*Authority.支出统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='支出统计')
	alter table Authority add 支出统计 [bit] NULL

/*Authority.收银员收款统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='收银员收款统计')
	alter table Authority add 收银员收款统计 [bit] NULL

/*Authority.月报表*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='月报表')
	alter table Authority add 月报表 [bit] NULL

/*Authority.往来单位账目*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='往来单位账目')
	alter table Authority add 往来单位账目 [bit] NULL

/*Authority.扣卡*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='扣卡')
	alter table Authority add 扣卡 [bit] NULL

/*Authority.会员积分设置*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='会员积分设置')
	alter table Authority add 会员积分设置 [bit] NULL

/*Authority.优惠方案*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='优惠方案')
	alter table Authority add 优惠方案 [bit] NULL

/*Authority.会员分析*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='会员分析')
	alter table Authority add 会员分析 [bit] NULL

/*Authority.会员管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='会员管理')
	alter table Authority add 会员管理 [bit] NULL

/*Authority.会员消费统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='会员消费统计')
	alter table Authority add 会员消费统计 [bit] NULL

/*Authority.会员售卡及充值统计*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='会员售卡及充值统计')
	alter table Authority add 会员售卡及充值统计 [bit] NULL

/*Authority.手牌管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='手牌管理')
	alter table Authority add 手牌管理 [bit] NULL

/*Authority.券类管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='券类管理')
	alter table Authority add 券类管理 [bit] NULL

/*Authority.客户管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='客户管理')
	alter table Authority add 客户管理 [bit] NULL

/*Authority.项目档案管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='项目档案管理')
	alter table Authority add 项目档案管理 [bit] NULL

/*Authority.客房管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='客房管理')
	alter table Authority add 客房管理 [bit] NULL

/*Authority.员工管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='员工管理')
	alter table Authority add 员工管理 [bit] NULL

/*Authority.权限管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='权限管理')
	alter table Authority add 权限管理 [bit] NULL

/*Authority.套餐管理*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='套餐管理')
	alter table Authority add 套餐管理 [bit] NULL

/*Authority.库存参数*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='库存参数')
	alter table Authority add 库存参数 [bit] NULL

/*Authority.仓库设定*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='仓库设定')
	alter table Authority add 仓库设定 [bit] NULL

/*Authority.补货标准*/
if exists (select * from syscolumns where id=object_id('Authority') and name='补货标准')
	exec sp_rename 'Authority.补货标准','供应商管理','column';

/*Authority.进货入库*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='进货入库')
	alter table Authority add 进货入库 [bit] NULL

/*Authority.现有库存*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='现有库存')
	alter table Authority add 现有库存 [bit] NULL

/*Authority.调货补货*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='调货补货')
	alter table Authority add 调货补货 [bit] NULL

/*Authority.盘点清册*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='盘点清册')
	alter table Authority add 盘点清册 [bit] NULL

/*Authority.盘点调整*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='盘点调整')
	alter table Authority add 盘点调整 [bit] NULL

/*Authority.应付账款*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='应付账款')
	alter table Authority add 应付账款 [bit] NULL

/*Authority.系统设置*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='系统设置')
	alter table Authority add 系统设置 [bit] NULL

/*Authority.数据优化*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='数据优化')
	alter table Authority add 数据优化 [bit] NULL

/*Authority.收银报表*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='收银报表')
	alter table Authority add 收银报表 [bit] NULL

/*Authority.团购打折*/
if not exists (select * from syscolumns where id=object_id('Authority') and name='团购打折')
	alter table Authority add 团购打折 [bit] NULL
	
	/***
**********************************************************************
**********************************************************************
表BigCombo
**********************************************************************
**********************************************************************
**********************************************************************
*****/
/*判断表BigCombo是否存在*/
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

if not exists(select * from Job where name='经理')
	insert into Job(id,name) values(1,'经理')

if not exists(select * from Employee where id='1000')
	insert into Employee(id,name,gender,birthday,jobId,onDuty,password,phone,entryDate) 
	select '1000','系统','男',getdate(),id,1,'4IhfVN1IxdD5Az82+2Fjxg==','1',getdate() from Job

if exists(select * from Authority where emplyeeid='1000')
	delete from Authority where emplyeeid='1000'

insert into Authority(emplyeeid,
开牌,			取消开牌,		微信赠送,			更换手牌,		锁定解锁,	解除警告,		停用启用,		添加备注,		挂失手牌,		完整点单,		可见本人点单,
退单,			手工打折,		签字免单,			转账,			重新结账,	结账,			技师管理,		收银汇总统计,	包房管理,		收银单据查询,	录单汇总,
营业信息查看,	售卡,			充值,				挂失,			补卡,		读卡,			扣卡,			卡入库,			异常状况统计,	提成统计,		手工打折汇总,
项目报表,		信用卡统计,		营业报表,			退免单汇总,		支出统计,	收银员收款统计,	月报表,			往来单位账目,	会员积分设置,	优惠方案,		会员分析,
会员管理,		会员消费统计,	会员售卡及充值统计,	手牌管理,		券类管理,	客户管理,		项目档案管理,	客房管理,		员工管理,		权限管理,		团购打折,
套餐管理,		库存参数,		仓库设定,			进货入库,		现有库存,	调货补货,		盘点清册,		盘点调整,		应付账款,		系统设置,		数据优化,
收银报表) values('1000',
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1,				1,				1,					1,				1,			1,				1,				1,				1,				1,				1,
1)