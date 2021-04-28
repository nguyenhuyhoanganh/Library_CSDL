--Hàm thống kê tiền vé theo thời gian
Create or Alter Function thongKe_Ve(@dateBD date, @dateKT date)
returns money
as
begin
declare @tongTien money
Select @tongTien=sum(tongtien) from VE where ve.NGAYBAN between @dateBD and @dateKT
return @tongTien
end
go
-- Hàm thống kê tiền dịch vụ theo thời gian
Create or Alter Function thongKe_BL(@dateBD date, @dateKt date)
returns money
as
begin
declare @tongTien money
Select @tongTien=sum(tongtien) from BIENLAI where BIENLAI.NGAYBL between @dateBD and @dateKt
return @tongTien
end
go
--THống kê tổng thu nhập qua
--///////////
--Hàm thông kê tất cả khu + tháng với 2 trường tiền dịch vụ và tiền vé bán
Create or Alter proc thongKeTatCa_Khu_Thang @dateBD date, @dateKT date
as
begin
Select dbo.thongKe_Ve(@dateBD,@dateKT) as TienVe, dbo.thongKe_BL(@dateBD,@dateKT) as DichVu
end
go
--//////////
--Hàm thongKeTung_Khu_Thang thống kê trong tháng của 1 khu theo các tháng trong năm

Create or Alter proc thongKeTung_Khu_Thang @dateBD date, @dateKT date, @Khu nchar(10)
as
begin
Select sum(TongTien) as TienVe from Ve 
where (Ve.NGAYBAN between @dateBD and @dateKT) and Ve.MAKHU=@Khu
end
go

--Hàm thống kê tổng của 1 tháng
Create or Alter function tongTien1Thang(@ngayBD date, @ngayKT date)
returns money
as
begin
declare @tongTien money
if(dbo.thongKe_Ve(@ngayBD,@ngayKT)) is not null
begin
Select @tongTien=dbo.thongKe_Ve(@ngayBD, @ngayKT) 
end
if(dbo.thongKe_BL(@ngayBD,@ngayKT)) is not null
begin
Select @tongTien=@tongTien + dbo.thongKe_BL(@ngayBD,@ngayKT)
end
return @tongTien
end
go

--Proc thongKeTungThang thống kê doanh thu từng tháng trong năm

Create or Alter proc thongKeTungThang (@nam nvarchar(4))
as
Declare @nam2 int=Convert(int,@nam)-1
Select dbo.tongTien1Thang(Concat('12/31/',@nam2),Concat('01/31/',@nam)) as T1,
dbo.tongTien1Thang(Concat('02/01/',@nam),Concat('02/28/',@nam)) as T2, 
dbo.tongTien1Thang(Concat('03/01/',@nam),Concat('03/31/',@nam)) as T3, 
dbo.tongTien1Thang(Concat('04/01/',@nam),Concat('04/30/',@nam)) as T4, 
dbo.tongTien1Thang(Concat('05/01/',@nam),Concat('05/31/',@nam)) as T5, 
dbo.tongTien1Thang(Concat('06/01/',@nam),Concat('06/30/',@nam)) as T6, 
dbo.tongTien1Thang(Concat('07/01/',@nam),Concat('07/31/',@nam)) as T7,
dbo.tongTien1Thang(Concat('08/01/',@nam),Concat('08/31/',@nam)) as T8, 
dbo.tongTien1Thang(Concat('09/01/',@nam),Concat('09/30/',@nam)) as T9, 
dbo.tongTien1Thang(Concat('10/01/',@nam),Concat('10/31/',@nam)) as T10, 
dbo.tongTien1Thang(Concat('11/01/',@nam),Concat('11/30/',@nam)) as T11, 
dbo.tongTien1Thang(Concat('12/01/',@nam),Concat('12/31/',@nam)) as T12

go
--Thông kê tiền vé của 1 khu
Create or Alter function thongKeTien_Khu_Thang(@dateBD date, @dateKT date, @Khu nchar(10))
returns money
as
begin
declare @tongTien money
Select @tongTien=sum(TongTien)from Ve 
where (Ve.NGAYBAN between @dateBD and @dateKT) and Ve.MAKHU=@Khu
return @tongTien
end
go


--Proc thông kê tưng khu từng tháng:
Create or Alter proc thongKeKhu_Nam @nam nvarchar(10), @khu nchar(10)
as
begin
Declare @nam2 int=Convert(int,@nam)-1
Select dbo.thongKeTien_Khu_Thang(Concat('12/31/',@nam2),Concat('01/31/',@nam), @khu) as T1,
dbo.thongKeTien_Khu_Thang(Concat('02/01/',@nam),Concat('02/28/',@nam), @khu) as T2, 
dbo.thongKeTien_Khu_Thang(Concat('03/01/',@nam),Concat('03/31/',@nam), @khu) as T3, 
dbo.thongKeTien_Khu_Thang(Concat('04/01/',@nam),Concat('04/30/',@nam), @khu) as T4, 
dbo.thongKeTien_Khu_Thang(Concat('05/01/',@nam),Concat('05/31/',@nam), @khu) as T5, 
dbo.thongKeTien_Khu_Thang(Concat('06/01/',@nam),Concat('06/30/',@nam), @khu) as T6, 
dbo.thongKeTien_Khu_Thang(Concat('07/01/',@nam),Concat('07/31/',@nam), @khu) as T7,
dbo.thongKeTien_Khu_Thang(Concat('08/01/',@nam),Concat('08/31/',@nam), @khu) as T8, 
dbo.thongKeTien_Khu_Thang(Concat('09/01/',@nam),Concat('09/30/',@nam), @khu) as T9, 
dbo.thongKeTien_Khu_Thang(Concat('10/01/',@nam),Concat('10/31/',@nam), @khu) as T10, 
dbo.thongKeTien_Khu_Thang(Concat('11/01/',@nam),Concat('11/30/',@nam), @khu) as T11, 
dbo.thongKeTien_Khu_Thang(Concat('12/01/',@nam),Concat('12/31/',@nam), @khu) as T12
end
go
