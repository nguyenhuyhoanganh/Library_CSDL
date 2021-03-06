USE [master]
GO
/****** Object:  Database [KHUVUICHOIGIAITRI]    Script Date: 3/30/2021 7:18:53 PM ******/
CREATE DATABASE [KHUVUICHOIGIAITRI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KHUVUICHOIGIAITRI', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\KHUVUICHOIGIAITRI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KHUVUICHOIGIAITRI_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\KHUVUICHOIGIAITRI_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KHUVUICHOIGIAITRI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ARITHABORT OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET RECOVERY FULL 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET  MULTI_USER 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET QUERY_STORE = OFF
GO
USE [KHUVUICHOIGIAITRI]
GO
/****** Object:  UserDefinedFunction [dbo].[tinhTienDV]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Hàm:
   -- Hàm tinhTienDV :Tính tiền dịch vụ
CREATE   FUNCTION [dbo].[tinhTienDV] (@SOBL NCHAR(10), @MADV NCHAR(10))
RETURNS MONEY
AS
BEGIN
DECLARE @TT MONEY, @SL INT,@DONGIA MONEY
SELECT @SL=SOLUONG,@DONGIA=DONGIA
FROM CHITIETBL CT , DICHVU DV
WHERE CT.MADV=DV.MADV AND SOBL=@SOBL AND DV.MADV=@MADV
SET @TT=@SL*@DONGIA
RETURN @TT
END
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MANV] [nchar](10) NOT NULL,
	[TENNV] [nvarchar](50) NULL,
	[NGAYSINH] [date] NULL,
	[SDT] [nchar](10) NULL,
	[GIOITINH] [nchar](3) NULL,
	[LUONG] [money] NULL,
	[MAKHU] [nchar](10) NULL,
	[DIACHI] [nvarchar](50) NULL,
	[CHUCVU] [nvarchar](20) NULL,
	[MATKHAU] [nchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[topluong]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[topluong]
as
select nv.MANV, nv.tennv, nv.luong from NHANVIEN nv join (select top 1* from NHANVIEN order by luong desc) as luongnv on luongnv.MANV= nv.MANV
GO
/****** Object:  Table [dbo].[BIENLAI]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BIENLAI](
	[SOBL] [nchar](10) NOT NULL,
	[NGAYBL] [date] NULL,
	[TONGTIEN] [money] NULL,
	[MANV] [nchar](10) NULL,
	[trangthai] [nvarchar](20) NULL,
	[DONGIA] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SOBL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VE]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VE](
	[MAVE] [nchar](10) NOT NULL,
	[SOLUONGNL] [int] NULL,
	[SOLUONGTE] [int] NULL,
	[MAKHU] [nchar](10) NULL,
	[MANV] [nchar](10) NULL,
	[TONGTIEN] [money] NULL,
	[NGAYBAN] [date] NULL,
	[giavenl] [money] NULL,
	[giavete] [money] NULL,
	[trangthai] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MAVE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[thongKeDoanhThu]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   -- Hàm thongKeDoanhThu: Thống kê doanh thu của khu vui chơi qua các dịch vụ được khách hàng sử dụng và trả phí được lưu qua biên lai
CREATE   FUNCTION [dbo].[thongKeDoanhThu](@NGAYBD DATE,@NGAYKT DATE)
RETURNS TABLE
RETURN
(
SELECT NGAYBL, BL.SOBL, BL.TONGTIEN as TONGTIENDV, V.TONGTIEN AS TIENVE
FROM BIENLAI BL, VE V 
WHERE NGAYBL BETWEEN @NGAYBD AND @NGAYKT AND V.NGAYBAN BETWEEN @NGAYBD AND @NGAYKT
)
GO
/****** Object:  UserDefinedFunction [dbo].[thonginnhanvien]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Thông tin nhân viên trong khu vui chơi
CREATE   FUNCTION [dbo].[thonginnhanvien]()
RETURNS TABLE
RETURN
(
SELECT * FROM NHANVIEN 
)
GO
/****** Object:  Table [dbo].[DICHVU]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DICHVU](
	[MADV] [nchar](10) NOT NULL,
	[TENDV] [nvarchar](50) NULL,
	[MALDV] [nchar](10) NULL,
	[DONGIA] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[MADV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[thongtindichvu]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Thông tin dịch vụ trong khu vui chơi
CREATE   FUNCTION [dbo].[thongtindichvu]()
RETURNS TABLE
RETURN
(
SELECT * FROM DICHVU
)
GO
/****** Object:  UserDefinedFunction [dbo].[thongKeLuong]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Hàm thongKeLuong: Thống kê lương của nhân viên
CREATE   FUNCTION [dbo].[thongKeLuong]()
RETURNS TABLE
RETURN
(
SELECT MANV, TENNV, DIACHI, LUONG
FROM NHANVIEN
)
GO
/****** Object:  Table [dbo].[CHITIETBL]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETBL](
	[SOBL] [nchar](10) NOT NULL,
	[MADV] [nchar](10) NOT NULL,
	[SOLUONG] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SOBL] ASC,
	[MADV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHUVUICHOI]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHUVUICHOI](
	[MAKHU] [nchar](10) NOT NULL,
	[TENKHU] [nvarchar](50) NULL,
	[GIAVENL] [money] NULL,
	[GIAVETE] [money] NULL,
	[DIADIEM] [money] NULL,
 CONSTRAINT [PK_KHUVUICHOI] PRIMARY KEY CLUSTERED 
(
	[MAKHU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAIDV]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAIDV](
	[MALDV] [nchar](10) NOT NULL,
	[TENLDV] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MALDV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[qltaikhoan]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qltaikhoan](
	[taikhoan] [nchar](20) NULL,
	[matkhau] [nvarchar](50) NULL,
	[quyentruycap] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TROCHOI]    Script Date: 3/30/2021 7:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TROCHOI](
	[MATC] [nchar](10) NOT NULL,
	[TENTC] [nvarchar](50) NULL,
	[MAKHU] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MATC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl00      ', CAST(N'2020-04-20' AS Date), 70000.0000, N'NV03      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl01      ', CAST(N'2020-04-20' AS Date), 70000.0000, N'NV03      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl02      ', CAST(N'2020-04-20' AS Date), 2145000.0000, N'NV03      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl03      ', CAST(N'2020-04-20' AS Date), 1620000.0000, N'NV12      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl04      ', CAST(N'2020-04-20' AS Date), 870000.0000, N'NV12      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl05      ', CAST(N'2020-04-20' AS Date), 70060000.0000, N'NV12      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl06      ', CAST(N'2020-04-20' AS Date), 3510000.0000, N'NV12      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl07      ', CAST(N'2020-04-20' AS Date), 100000.0000, N'NV13      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl08      ', CAST(N'2020-04-20' AS Date), 200000.0000, N'NV19      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl09      ', CAST(N'2020-04-20' AS Date), 190000.0000, N'NV19      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl10      ', CAST(N'2020-04-20' AS Date), 500000.0000, N'NV19      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl11      ', CAST(N'2020-04-20' AS Date), 70000.0000, N'NV19      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl12      ', CAST(N'2020-04-20' AS Date), 360000.0000, N'NV19      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl13      ', CAST(N'2020-04-20' AS Date), 50010000.0000, N'NV26      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl14      ', CAST(N'2020-04-20' AS Date), 960000.0000, N'NV14      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl15      ', CAST(N'2020-04-20' AS Date), 50000.0000, N'NV26      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl16      ', CAST(N'2020-04-20' AS Date), 30000.0000, N'NV26      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl17      ', CAST(N'2020-04-20' AS Date), 100000.0000, N'NV26      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl18      ', CAST(N'2020-04-20' AS Date), 470000.0000, N'NV14      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl19      ', CAST(N'2020-04-20' AS Date), 20000.0000, N'NV14      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl20      ', CAST(N'2020-04-20' AS Date), 100000.0000, N'NV26      ', NULL, NULL)
INSERT [dbo].[BIENLAI] ([SOBL], [NGAYBL], [TONGTIEN], [MANV], [trangthai], [DONGIA]) VALUES (N'bl26      ', CAST(N'2020-04-20' AS Date), 930000.0000, N'NV28      ', NULL, NULL)
GO
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl00      ', N'dv01      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl00      ', N'dv02      ', 5)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl01      ', N'dv01      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl01      ', N'dv02      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl01      ', N'dv09      ', 12)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl02      ', N'dv05      ', 9)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl02      ', N'dv06      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl02      ', N'dv07      ', 11)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl02      ', N'dv08      ', 15)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl02      ', N'dv09      ', 7)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl03      ', N'dv10      ', 10)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl03      ', N'dv11      ', 5)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl03      ', N'dv12      ', 11)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl03      ', N'dv13      ', 13)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl04      ', N'dv06      ', 11)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl04      ', N'dv14      ', 15)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl04      ', N'dv15      ', 11)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl04      ', N'dv16      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl05      ', N'dv17      ', 14)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl05      ', N'dv18      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl06      ', N'dv19      ', 9)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl06      ', N'dv20      ', 8)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl06      ', N'dv21      ', 7)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl07      ', N'dv11      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl08      ', N'dv10      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl08      ', N'dv11      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl08      ', N'dv14      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl09      ', N'dv09      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl09      ', N'dv11      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl09      ', N'dv12      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl09      ', N'dv16      ', 11)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl10      ', N'dv10      ', 5)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl10      ', N'dv14      ', 5)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl11      ', N'dv12      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl11      ', N'dv13      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl12      ', N'dv02      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl12      ', N'dv10      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl12      ', N'dv11      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl12      ', N'dv12      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl12      ', N'dv20      ', 14)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl13      ', N'dv15      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl13      ', N'dv17      ', 10)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl14      ', N'dv03      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl14      ', N'dv18      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl14      ', N'dv19      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl15      ', N'dv07      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl15      ', N'dv16      ', 5)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl16      ', N'dv15      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl17      ', N'dv15      ', 10)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl18      ', N'dv10      ', 6)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl18      ', N'dv14      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl18      ', N'dv18      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl18      ', N'dv19      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl19      ', N'dv18      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl19      ', N'dv19      ', 13)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl20      ', N'dv15      ', 2)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl20      ', N'dv16      ', 8)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl26      ', N'dv08      ', 1)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl26      ', N'dv20      ', 3)
INSERT [dbo].[CHITIETBL] ([SOBL], [MADV], [SOLUONG]) VALUES (N'bl26      ', N'dv22      ', 3)
GO
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv01      ', N'Giữ đồ khu vui chơi', N'dvgiudo   ', 20000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv02      ', N'Cho thuê sân Futsal', N'dvchothue ', 10000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv03      ', N'Cho thuê đồ bơi', N'dvchothue ', 50000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv04      ', N'Cho thuê giày trượt băng', N'dvchothue ', 50000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv05      ', N'Cho thuê đồ lặn', N'dvchothue ', 150000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv06      ', N'Mua xèng', N'dvbanhang ', 5000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv07      ', N'Sữa', N'dvbanhang ', 160000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv08      ', N'Cho thuê đồ thể thao', N'dvchothue ', 10000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv09      ', N'Coca cola', N'dvbanhang ', 25000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv10      ', N'Nước lọc', N'dvbanhang ', 8000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv11      ', N'Coffe', N'dvbanhang ', 20000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv12      ', N'Pop-corn', N'dvbanhang ', 20000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv13      ', N'Nước ép trái cây', N'dvbanhang ', 50000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv14      ', N'Snack', N'dvbanhang ', 10000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv15      ', N'Thuê máy thường', N'dvchothue ', 10000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv16      ', N'Thuê máy VIP', N'dvchothue ', 10000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv17      ', N'Thuê phòng thi đấu E-Sport', N'dvchothue ', 5000000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv18      ', N'Dạy bi-da', N'dvhotro   ', 250000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv19      ', N'Dạy bơi', N'dvhotro   ', 250000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv20      ', N'Mượn đồ bảo hộ leo núi', N'dvchothue ', 120000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv21      ', N'Mượn đồ giữ ấm', N'dvchothue ', 80000.0000)
INSERT [dbo].[DICHVU] ([MADV], [TENDV], [MALDV], [DONGIA]) VALUES (N'dv22      ', N'Trà sữa', N'dvbanhang ', 30000.0000)
GO
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K00       ', N'Khu Bán Vé', 0.0000, 0.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K01       ', N'Khu Bể Bơi', 50000.0000, 45000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K02       ', N'Khu Nhà hơi', 20000.0000, 10000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K03       ', N'Khu Thể Thao Trong Nhà', 10000.0000, 7000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K04       ', N'Khu Nhà Hàng Ăn Uống', 0.0000, 0.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K05       ', N'Khu Thể Thao Điện Tử', 10000.0000, 10000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K06       ', N'Khu Tàu Lượn Siêu Tốc', 50000.0000, 40000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K07       ', N'Khu Nhà Tuyết', 120000.0000, 110000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K08       ', N'Khu Thủy Cung', 80000.0000, 70000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K09       ', N'Khu Chiếu Phim', 60000.0000, 50000.0000, NULL)
INSERT [dbo].[KHUVUICHOI] ([MAKHU], [TENKHU], [GIAVENL], [GIAVETE], [DIADIEM]) VALUES (N'K10       ', N'Khu Trình Diễn Nhạc Nước', 100000.0000, 90000.0000, NULL)
GO
INSERT [dbo].[LOAIDV] ([MALDV], [TENLDV]) VALUES (N'dvbanhang ', N'Dịch vụ bán hàng')
INSERT [dbo].[LOAIDV] ([MALDV], [TENLDV]) VALUES (N'dvchothue ', N'Dịch vụ cho thuê đồ dùng')
INSERT [dbo].[LOAIDV] ([MALDV], [TENLDV]) VALUES (N'dvgiudo   ', N'Dịch vụ trông giữ đồ')
INSERT [dbo].[LOAIDV] ([MALDV], [TENLDV]) VALUES (N'dvhotro   ', N'Dịch vụ hỗ trợ khách hàng')
GO
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV00      ', N'Lê', CAST(N'2000-01-01' AS Date), N'0123456789', N'Nam', 10000.0000, N'K03       ', N'Địa chỉ', N'Nhân viên', N'1                   ')
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV02      ', N'Lê Phương Thảo', CAST(N'1996-08-05' AS Date), N'0124573698', N'Nữ ', 7000000.0000, N'K01       ', N'Thanh Xuân', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV03      ', N'Nguyễn Tuấn Anh', CAST(N'1988-04-15' AS Date), N'0999999999', N'Nam', 10000000.0000, N'K01       ', N'Thanh Xuân', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV04      ', N'Lê Minh Vương', CAST(N'1993-01-01' AS Date), N'0148997564', N'Nữ ', 9000000.0000, N'K04       ', N'Hà Đông', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV05      ', N'Nguyễn Mai Anh', CAST(N'1997-05-02' AS Date), N'0975841672', N'Nữ ', 7000000.0000, N'K06       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV06      ', N'Nguyễn Văn Dũng', CAST(N'1999-11-23' AS Date), N'0945678912', N'Nam', 20000000.0000, N'K06       ', N'Cổ Nhuế', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV07      ', N'Nguyễn Hữu Giang', CAST(N'1998-06-04' AS Date), N'0918123456', N'Nam', 8000000.0000, N'K02       ', N'Hai Bà Trưng', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV08      ', N'Nguyễn Thị Hương', CAST(N'1989-12-15' AS Date), N'0915123457', N'Nữ ', 8500000.0000, N'K03       ', N'Thanh Xuân', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV09      ', N'Lê Trà Mi', CAST(N'1993-01-06' AS Date), N'0947456789', N'Nữ ', 7000000.0000, N'K06       ', N'Hoàng Quốc Việt', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV10      ', N'Nguyễn Tùng Dương', CAST(N'1997-08-02' AS Date), N'0947456218', N'Nam', 7500000.0000, N'K01       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV11      ', N'Hồ Đắc Thắng', CAST(N'1999-07-24' AS Date), N'0984612620', N'Nam', 90000000.0000, N'K01       ', N'Đống Đa', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV12      ', N'Trần Hùng Phong', CAST(N'1999-10-09' AS Date), N'0918122456', N'Nam', 6000000.0000, N'K02       ', N'Đống Đa', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV13      ', N'Nguyễn Thị Lan', CAST(N'1996-12-15' AS Date), N'0915156857', N'Nữ ', 800000.0000, N'K03       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV14      ', N'Lương Tuấn Minh', CAST(N'1993-01-06' AS Date), N'0947452589', N'Nam', 6000000.0000, N'K06       ', N'Minh Khai', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV15      ', N'Nguyễn Minh Thông', CAST(N'1997-08-02' AS Date), N'0947696218', N'Nam', 7500000.0000, N'K01       ', N'Từ Liêm', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV16      ', N'Đỗ Thành', CAST(N'1995-12-23' AS Date), N'0915313887', N'Nam', 10000000.0000, N'K06       ', N'Đình Thôn', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV17      ', N'Vũ Duy Thịnh', CAST(N'1996-02-13' AS Date), N'0914156447', N'Nam', 7000000.0000, N'K01       ', N'Thanh Xuân', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV18      ', N'Phạm Tuấn Phong', CAST(N'1991-04-15' AS Date), N'0985152237', N'Nam', 10000000.0000, N'K01       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV19      ', N'Lê Hữu Thuyết', CAST(N'1993-02-01' AS Date), N'0981316765', N'Nam', 9000000.0000, N'K04       ', N'Hà Đông', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV20      ', N'Đinh Phương Huế', CAST(N'1997-05-02' AS Date), N'0922856857', N'Nữ ', 7000000.0000, N'K06       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV21      ', N'Nguyễn Văn Đạt', CAST(N'1999-11-23' AS Date), N'0945614852', N'Nam', 50000000.0000, N'K09       ', N'Cổ Nhuế', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV22      ', N'Nguyễn Tuấn Hùng', CAST(N'1998-06-04' AS Date), N'0918153432', N'Nam', 8000000.0000, N'K02       ', N'Hai Bà Trưng', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV23      ', N'Nguyễn Thị Linh', CAST(N'1989-12-15' AS Date), N'0915123497', N'Nữ ', 8500000.0000, N'K03       ', N'Thanh Xuân', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV24      ', N'Chu Phương Anh', CAST(N'1993-01-06' AS Date), N'0947477189', N'Nữ ', 7000000.0000, N'K06       ', N'Hoàng Quốc Việt', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV25      ', N'Nguyễn Tùng Anh', CAST(N'1997-08-02' AS Date), N'0947457677', N'Nam', 7500000.0000, N'K01       ', N'Cầu Giấy', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV26      ', N'Hà Anh Tuấn', CAST(N'2000-01-01' AS Date), N'0123456789', N'Nam', 5000000.0000, N'K05       ', N'Long Biên', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV27      ', N'Chu Văn Sỹ', CAST(N'1995-03-04' AS Date), N'0345567718', N'Nam', 12000000.0000, N'K07       ', N'Mỹ Đình', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV28      ', N'Lê Thị Ngọc', CAST(N'1999-08-09' AS Date), N'0991122762', N'Nữ ', 7000000.0000, N'K08       ', N'Sơn Tây', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV29      ', N'Nguyễn Thị Hà Linh', CAST(N'1999-12-12' AS Date), N'0877366161', N'Nữ ', 9000000.0000, N'K09       ', N'Ba Đình', N'Nhân viên', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV30      ', N'Đặng Thùy Trâm', CAST(N'1998-03-12' AS Date), N'0987635157', N'Nữ ', 8000000.0000, N'K10       ', N'Cầu Giấy', N'Quản lý', NULL)
INSERT [dbo].[NHANVIEN] ([MANV], [TENNV], [NGAYSINH], [SDT], [GIOITINH], [LUONG], [MAKHU], [DIACHI], [CHUCVU], [MATKHAU]) VALUES (N'NV31      ', N'Lê', CAST(N'2000-01-01' AS Date), N'0123456789', N'Nam', 10000.0000, N'K03       ', N'Địa chỉ', N'Nhân viên', NULL)
GO
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC01      ', N'Bơi', N'K01       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC02      ', N'Trượt nước', N'K01       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC03      ', N'Lướt sóng', N'K01       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC04      ', N'Gắp gấu bông', N'K02       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC05      ', N'Đệm lò xo', N'K03       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC06      ', N'Đá bóng Futsal', N'K03       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC07      ', N'Cung thi đấu esport', N'K05       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC08      ', N'Phòng máy thường', N'K05       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC09      ', N'Phòng máy VIP', N'K05       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC10      ', N'Leo núi trong nhà', N'K06       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC11      ', N'Tàu lượn siêu tốc', N'K06       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC12      ', N'Trượt băng', N'K07       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC13      ', N'Nhà tuyết', N'K07       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC14      ', N'Ngôi nhà ma ám', N'K06       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC15      ', N'Chụp ảnh lưu niệm', N'K07       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC16      ', N'Khám phá thủy cung', N'K08       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC17      ', N'Lặn', N'K08       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC18      ', N'Rạp phim 5D', N'K09       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC19      ', N'Rạp phim 3D', N'K09       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC20      ', N'Cung trình diễn nhạc nước', N'K10       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC21      ', N'Nhà Bóng', N'K02       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC22      ', N'Bida', N'K03       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC23      ', N'Đua Xe F1 mini', N'K06       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC24      ', N'Bắn Súng Laser', N'K03       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC25      ', N'Bể Bơi Sóng Thần', N'K01       ')
INSERT [dbo].[TROCHOI] ([MATC], [TENTC], [MAKHU]) VALUES (N'TC26      ', N'Giả lập đua xe ', N'K03       ')
GO
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'000       ', 1, 10, N'K01       ', NULL, 500000.0000, CAST(N'2020-12-02' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'001       ', 2, 0, N'K02       ', N'NV07      ', 40000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'002       ', 12, 10, N'K10       ', N'NV30      ', 2100000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'003       ', 11, 15, N'K01       ', NULL, 1225000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'004       ', 6, 14, N'K02       ', N'NV07      ', 260000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'005       ', 7, 0, N'K03       ', N'NV08      ', 70000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'006       ', 8, 1, N'K06       ', N'NV05      ', 440000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'007       ', 12, 2, N'K05       ', N'NV26      ', 140000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'008       ', 4, 3, N'K07       ', N'NV27      ', 810000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'009       ', 5, 5, N'K08       ', N'NV28      ', 750000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'010       ', 8, 15, N'K03       ', N'NV08      ', 185000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'011       ', 12, 10, N'K05       ', N'NV26      ', 220000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'012       ', 13, 4, N'K01       ', NULL, 830000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'013       ', 6, 5, N'K02       ', N'NV07      ', 170000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'014       ', 8, 12, N'K03       ', N'NV08      ', 164000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'015       ', 12, 7, N'K05       ', N'NV26      ', 190000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'016       ', 5, 6, N'K06       ', N'NV05      ', 490000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'017       ', 1, 0, N'K07       ', N'NV27      ', 120000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'018       ', 1, 3, N'K08       ', N'NV28      ', 290000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'019       ', 4, 4, N'K09       ', N'NV29      ', 440000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'020       ', 20, 12, N'K10       ', N'NV30      ', 3080000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'021       ', 6, 20, N'K01       ', NULL, 1200000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'022       ', 8, 17, N'K02       ', NULL, 330000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'023       ', 15, 13, N'K01       ', N'NV03      ', 1335000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'024       ', 13, 15, N'K03       ', N'NV08      ', 235000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'025       ', 14, 16, N'K01       ', NULL, 1420000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'026       ', 10, 17, N'K02       ', N'NV07      ', 370000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'100       ', 19, 19, N'K06       ', N'NV05      ', 1710000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'101       ', 11, 10, N'K07       ', N'NV27      ', 2420000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'110       ', 21, 6, N'K08       ', N'NV28      ', 2100000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'111       ', 13, 15, N'K09       ', N'NV29      ', 1530000.0000, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[VE] ([MAVE], [SOLUONGNL], [SOLUONGTE], [MAKHU], [MANV], [TONGTIEN], [NGAYBAN], [giavenl], [giavete], [trangthai]) VALUES (N'999       ', 8, 1, N'K06       ', N'NV05      ', 440000.0000, NULL, NULL, NULL, NULL)
GO
ALTER TABLE [dbo].[BIENLAI]  WITH CHECK ADD FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[CHITIETBL]  WITH CHECK ADD FOREIGN KEY([MADV])
REFERENCES [dbo].[DICHVU] ([MADV])
GO
ALTER TABLE [dbo].[CHITIETBL]  WITH CHECK ADD FOREIGN KEY([SOBL])
REFERENCES [dbo].[BIENLAI] ([SOBL])
GO
ALTER TABLE [dbo].[DICHVU]  WITH CHECK ADD FOREIGN KEY([MALDV])
REFERENCES [dbo].[LOAIDV] ([MALDV])
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD FOREIGN KEY([MAKHU])
REFERENCES [dbo].[KHUVUICHOI] ([MAKHU])
GO
ALTER TABLE [dbo].[TROCHOI]  WITH CHECK ADD FOREIGN KEY([MAKHU])
REFERENCES [dbo].[KHUVUICHOI] ([MAKHU])
GO
ALTER TABLE [dbo].[VE]  WITH CHECK ADD FOREIGN KEY([MAKHU])
REFERENCES [dbo].[KHUVUICHOI] ([MAKHU])
GO
ALTER TABLE [dbo].[VE]  WITH CHECK ADD FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
/****** Object:  StoredProcedure [dbo].[auto_manv]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     proc [dbo].[auto_manv]
as
begin
	declare @manv nchar(10)
	declare  @id int
	set @id=0
	set @manv='NV00'
	while exists (select manv from NHANVIEN where MANV = @manv )
	begin
		set @id=@id+1
		set @manv= 'NV'+ (REPLICATE('0',2- LEN(CAST(@id as varchar)))+CAST(@id as varchar))
	end
	select @manv
end
GO
/****** Object:  StoredProcedure [dbo].[CapNhatKhuVuiChoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CapnhatKhuVuiChoi: Cập nhật lại thông tin của khu vui chơi
create   proc [dbo].[CapNhatKhuVuiChoi](@MAKHU nchar(10),@TENKHU nvarchar(50),@GIAVENL money,@GIAVETE money,@DIADIEM nvarchar(50))

as begin
update KHUVUICHOI set                      TENKHU=@TENKHU,GIAVENL=@GIAVENL,GIAVETE=@GIAVETE,DIADIEM=@DIADIEM 
             where MAKHU=@MAKHU
end
GO
/****** Object:  StoredProcedure [dbo].[capNhatNV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--capNhatNV: Cập nhật lại lương của nhân viên của khu vui chơi
create   proc [dbo].[capNhatNV]
@ma nchar(10), @luong money
as begin
update NHANVIEN set LUONG=@luong where MANV=@ma
End
GO
/****** Object:  StoredProcedure [dbo].[Capnhatthongtin]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       proc [dbo].[Capnhatthongtin] (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10), @chucvu nvarchar(20), @manv_cu nchar(10))
as begin
if(select count(manv)from NHANVIEN where MANV= @manv_cu)=0
print(N'Không có nhân viên cần sửa')
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI, LUONG=@luong, Makhu=@makhu, MANV=@MANV, chucvu=@chucvu
where MANV=@manv_cu
print(N'Đã sửa thông tin nhân viên thành công')
end
GO
/****** Object:  StoredProcedure [dbo].[capnhatTroChoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CapnhatTrochoi: Cập nhật lại dịch vụ của khu vui chơi
create   proc  [dbo].[capnhatTroChoi](@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
as begin
update TROCHOI set MAKHU= @MAKHU,TENTC=@TENTC where MATC=@MATC
end
GO
/****** Object:  StoredProcedure [dbo].[dangNhap]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--dangnhap: Đăng nhập vào chương trình quản lý
CREATE   PROC [dbo].[dangNhap] @MANV NCHAR(10), @MATKHAU NVARCHAR(20)
AS
BEGIN
select * from NHANVIEN where MANV= @MANV and MATKHAU= @MATKHAU
END
GO
/****** Object:  StoredProcedure [dbo].[doiMK]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--doiMK: Đổi mật khẩu
CREATE   PROC [dbo].[doiMK] @MANV NCHAR(10), @MATKHAUMOI NVARCHAR(20)
AS
BEGIN
UPDATE NHANVIEN
SET MATKHAU=@MATKHAUMOI
WHERE MANV=@MANV
END
GO
/****** Object:  StoredProcedure [dbo].[KIEMTOAN]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[KIEMTOAN] (@MAKVC NCHAR(10))
	 AS
	 BEGIN
		SELECT @MAKVC, VE.NGAYBAN,SUM(VE.TONGTIEN), SUM(VE.SOLUONGTE+ VE.SOLUONGNL) FROM VE JOIN KHUVUICHOI KVC ON VE.MAKHU = KVC.MAKHU
		GROUP BY  VE.NGAYBAN
	 END
GO
/****** Object:  StoredProcedure [dbo].[KIEMTOAN1]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	 CREATE PROC [dbo].[KIEMTOAN1] (@MAKVC NCHAR(10))
	 AS
	 BEGIN
		SELECT @MAKVC, VE.NGAYBAN,SUM(VE.TONGTIEN) AS TONGTIENVE, SUM(VE.SOLUONGTE+ VE.SOLUONGNL) AS TONGKHACH FROM VE JOIN KHUVUICHOI KVC ON VE.MAKHU = KVC.MAKHU
		GROUP BY  VE.NGAYBAN
	 END
	 
GO
/****** Object:  StoredProcedure [dbo].[suaDICHVU]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--suaDICHVU: Cập nhật lại dịch vụ của khu vui chơi
create   proc [dbo].[suaDICHVU]
(@madv nchar(10), @tendv nvarchar(50), @maldv nchar(10), @dongia money)
as begin
update DICHVU set
TENDV=@tendv, MALDV=@maldv, DONGIA=@dongia
where MADV=@madv
end
GO
/****** Object:  StoredProcedure [dbo].[suaLOAIDV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--suaLOAIDV: Cập nhật lại thông tin của loại dịch vụ
create   proc [dbo].[suaLOAIDV]
(@maldv nchar(10), @tenldv nvarchar(50))
as begin
update LOAIDV set
TENLDV=@tenldv
where MALDV=@maldv
end
GO
/****** Object:  StoredProcedure [dbo].[suaVE]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--suaVE: Cập nhật lại vé của khu vui chơi
create   proc [dbo].[suaVE]
@mave nchar(10),@soluongnl int, @soluongte int
as begin
declare @a money, @b money
select @a= k.GIAVENL, @b=k.GIAVETE from KHUVUICHOI as k, VE as v where k.MAKHU= v.MAKHU and v.MAVE=@mave
update VE set SOLUONGNL=@soluongnl, SOLUONGTE=@soluongte,
TONGTIEN=CAST(@soluongnl as money)*@a+CAST(@soluongte as money)*@b
where MAVE=@mave
End
GO
/****** Object:  StoredProcedure [dbo].[THEMDICHVU]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--THEMDICHVU: Thêm dịch vụ khi đã có mã loại dịch vụ
CREATE   PROC [dbo].[THEMDICHVU] 
 
         @MADV nchar(10),
 @TENDV nvarchar(25),
 @DONGIA money,
 @MALDV nchar(10)
 
as begin 
 
if (select COUNT(LOAIDV.MALDV) from LOAIDV where LOAIDV.MALDV = @MALDV) = 0
print(N'KHÔNG CÓ LOẠI DỊCH VỤ NÀY!')
else
insert DICHVU(MADV, TENDV, DONGIA, MALDV) values(@MADV, @TENDV, @DONGIA, @MALDV)
end
GO
/****** Object:  StoredProcedure [dbo].[themKHUVUICHOI]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- themKHUVUICHOI: Thêm khu vui chơi
create   proc [dbo].[themKHUVUICHOI](@makhu nchar(10),@tenkhu nvarchar(50),@giavenl money,@giavete money,@diadiem nvarchar(50))
 as begin
 insert into dbo.KHUVUICHOI(MAKHU, TENKHU, GIAVENL, GIAVETE, DIADIEM)
 values(@makhu,@tenkhu,@giavenl,@giavete,@diadiem)
 end
GO
/****** Object:  StoredProcedure [dbo].[THEMlOAIDICHVU]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- THEMLOAIDICHVU: Thêm loại dịch vụ cho khu vui chơi
CREATE   PROC [dbo].[THEMlOAIDICHVU] 
 
         @MALDV nchar(10),
 @TENLDV nvarchar(25)
 
 
as begin 
 
if (select COUNT(LOAIDV.MALDV) from LOAIDV where LOAIDV.MALDV = @MALDV) != 0
print(N'ĐÃ CÓ LOẠI DỊCH VỤ NÀY!')
else
insert LOAIDV(MALDV, TENLDV) values(@MALDV, @TENLDV)
end
GO
/****** Object:  StoredProcedure [dbo].[themNV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   proc [dbo].[themNV]
@MANV nchar(10),@TENNV nvarchar(50),@NGAYSINH date, @SDT nchar(10), @GIOITINH 	nchar(3),@LUONG money,@chucvu nvarchar(20),@MAKHU nchar(10),@DIACHI nvarchar(50), @MATKHAU NCHAR(20)
as begin 
if(select COUNT(*)from NHANVIEN where MANV =@MANV)>0
begin
print(N'Đã có mã nhân viên này mời nhập lại')
end
else
begin
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,MAKHU,DIACHI,
MATKHAU, chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@MAKHU,@DIACHI,@MATKHAU, @chucvu)
print(N'Đã thêm nhân viên thành công')
end
end
GO
/****** Object:  StoredProcedure [dbo].[themTrochoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--themTrochoi: Thêm một trò chơi
create    proc [dbo].[themTrochoi](@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
  
as begin 
 
insert into TROCHOI(MATC,TENTC,MAKHU) 
 
values(@MATC,@TENTC,@MAKHU) 
 
end  
GO
/****** Object:  StoredProcedure [dbo].[themVE]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--themVE: Thêm vé bán ra
create    proc [dbo].[themVE]
@mave nchar(10), @soluongnl int,@soluongte int, @makhu nchar(10),
@manv nchar(10), @ngayban date, @tongtien money
as begin
insert into dbo.VE(MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, NGAYBAN, TONGTIEN)
values(@mave, @soluongnl, @soluongte, @makhu, @manv, @ngayban, @tongtien)
end 
GO
/****** Object:  StoredProcedure [dbo].[ThongTinTroChoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Thông tin trochoi: Xem thông tin trò chơi 
create   proc [dbo].[ThongTinTroChoi]
as begin
select TENKHU, TENTC from KHUVUICHOI,TROCHOI
WHERE TROCHOI.MAKHU=KHUVUICHOI.MAKHU
end
GO
/****** Object:  StoredProcedure [dbo].[timkiemMADV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   proc [dbo].[timkiemMADV] @madv nchar(10)
as
begin
	select * from NHANVIEN where MANV like '%' +@madv+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[timkiemNV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[timkiemNV]
@manv nvarchar(10), @tennv nvarchar(50)
as begin
if(@tennv='')
select * from NHANVIEN where MANV like '%'+@manv+'%'
else if(@manv='')
select * from NHANVIEN where TENNV like '%' +@tennv+'%'
else
select * from NHANVIEN where TENNV like '%'+@tennv+'%' and MANV like '%'+@manv+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[timkiemVe]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--timkiemVe: Tìm kiếm vé Theo mã vẽ
create   proc [dbo].[timkiemVe]
@mave nvarchar(10)
as begin
select * from VE where MAVE=@mave
end
GO
/****** Object:  StoredProcedure [dbo].[tkmatrochoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   proc [dbo].[tkmatrochoi] (@key nvarchar(10))
as
begin
select matc as N'Mã trò chơi', tentc as N'Tên trò chơi', makhu as N'Mã khu'  from TROCHOI where MATC like N'%'  + @key + '%'
end
GO
/****** Object:  StoredProcedure [dbo].[tktrochoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[tktrochoi] (@key nvarchar(10))
as
begin
select matc as N'Mã trò chơi', tentc as N'Tên trò chơi', makhu as N'Mã khu'  from TROCHOI where TENTC like N'%'  + @key + '%'
end
GO
/****** Object:  StoredProcedure [dbo].[xoa1NV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    proc [dbo].[xoa1NV]
@ma nchar(10)
as begin
if(select COUNT(*)from NHANVIEN where MANV =@MA)=0
print(N'Không có nhân viên này mời nhập lại thông tin')
else
begin
delete NHANVIEN where MANV=@ma
print(N'Xóa nhân viên thành công')
end
end
GO
/****** Object:  StoredProcedure [dbo].[xoaDICHVU]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--xoaDICHVU: Xóa đi một dịch vụ của khu vui chơi
create   proc [dbo].[xoaDICHVU] @madv nchar(10)
as begin
delete from DICHVU where DICHVU.MADV=@madv 
delete from CHITIETBL where CHITIETBL.MADV=@madv
end
GO
/****** Object:  StoredProcedure [dbo].[xoaKhuVuiChoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- XoaKhuVuiChoi: Xóa khu vui chơi
create   proc [dbo].[xoaKhuVuiChoi]
         @MAKHU nchar(10)
as begin
delete from KHUVUICHOI where MAKHU=@MAKHU
end
GO
/****** Object:  StoredProcedure [dbo].[xoaLOAIDV]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--xoaLOAIDV: Xóa một loại dịch vụ
create   proc [dbo].[xoaLOAIDV] @maldv nchar(10)
as begin
declare curLDV cursor
for select dv.MADV from DICHVU as dv
where dv.MALDV=@maldv
declare @madv nchar(10)
open curLDV
fetch next from curLDV into @madv 
while (@@FETCH_STATUS=0) 
begin 
delete from CHITIETBL where CHITIETBL.MADV=@madv
fetch next from curLDV into @madv 
end 
close curLDV 
deallocate curLDV 
delete from LOAIDV where MALDV=@maldv
delete from DICHVU where DICHVU.MALDV=@maldv 
end
GO
/****** Object:  StoredProcedure [dbo].[xoaTroChoi]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- XoaTroChoi:
create   proc [dbo].[xoaTroChoi]
@MATC nchar(10)
as begin
delete from TROCHOI where MATC=@MATC
end
GO
/****** Object:  StoredProcedure [dbo].[xoaVE]    Script Date: 3/30/2021 7:18:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--xoaVE : Xóa đi  1 vé
create   proc [dbo].[xoaVE]
@ma nchar(10)
as begin
delete from VE where MAVE=@ma
end
GO
USE [master]
GO
ALTER DATABASE [KHUVUICHOIGIAITRI] SET  READ_WRITE 
GO
