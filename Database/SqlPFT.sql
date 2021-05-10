--Database/SqlPFT.sql
alter table nhanvien add chucvu nvarchar(20)--(nhân viên/quản lý)
alter table nhanvien add MATKHAU nchar(20)
alter table ve add giavenl money 
alter table ve add giavete money 
alter table ve add trangthai NVARCHAR(20)
alter table bienlai add trangthai NVARCHAR(50)--(đã thanh toán/chưa thanh toán)
alter table bienlai add DONGIA money
go
--select*from NHANVIEN
--dangnhap: Đăng nhập vào chương trình quản lý
CREATE OR ALTER PROC dangNhap @MANV NCHAR(10), @MATKHAU NVARCHAR(20)
AS
BEGIN
select * from NHANVIEN where MANV= @MANV and MATKHAU= @MATKHAU
END
go
--doiMK: Đổi mật khẩu
CREATE OR ALTER PROC doiMK @MANV NCHAR(10), @MATKHAUMOI NVARCHAR(20)
AS
BEGIN
UPDATE NHANVIEN
SET MATKHAU=@MATKHAUMOI
WHERE MANV=@MANV
END
go
--themVE: Thêm vé bán ra
create or  alter proc themVE
@mave nchar(10), @soluongnl int,@soluongte int, @makhu nchar(10),
@manv nchar(10), @ngayban date, @tongtien money
as begin
insert into dbo.VE(MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, NGAYBAN, TONGTIEN)
values(@mave, @soluongnl, @soluongte, @makhu, @manv, @ngayban, @tongtien)
end 
go
--suaVE: Cập nhật lại vé của khu vui chơi
create or alter proc suaVE
@mave nchar(10),@soluongnl int, @soluongte int
as begin
declare @a money, @b money
select @a= k.GIAVENL, @b=k.GIAVETE from KHUVUICHOI as k, VE as v where k.MAKHU= v.MAKHU and v.MAVE=@mave
update VE set SOLUONGNL=@soluongnl, SOLUONGTE=@soluongte,
TONGTIEN=CAST(@soluongnl as money)*@a+CAST(@soluongte as money)*@b
where MAVE=@mave
End
go
go

--xoaVE : Xóa đi  1 vé
create or alter proc xoaVE
@ma nchar(10)
as begin
delete from VE where MAVE=@ma
end
go

--themNV: Thêm nhân viên v
create or alter  proc [dbo].[themNV]
@MANV nchar(10),@TENNV nvarchar(50),@NGAYSINH date, @SDT nchar(10), @GIOITINH 	nchar(3),@LUONG money,@chucvu nvarchar(20),@MAKHU nchar(10),@DIACHI nvarchar(50), @MATKHAU NCHAR(20)
as begin 
if(select COUNT(*)from NHANVIEN where MANV =@MANV)>0
begin
print(N'Đã có mã nhân viên này mời nhập lại')
end
else
begin
if( @MAKHU!='')
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,MAKHU,DIACHI,
MATKHAU, chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@MAKHU,@DIACHI,@MATKHAU, @chucvu)
else if(@MAKHU='')
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,DIACHI,
MATKHAU, chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@DIACHI,@MATKHAU, @chucvu)
print(N'Đã thêm nhân viên thành công')
end
end
GO

--timkiemVe: Tìm kiếm vé Theo mã vẽ
create or alter proc timkiemVe
@mave nvarchar(10)
as begin
select * from VE where MAVE=@mave
end
go
--xoa1NV: Xóa đi một nhân viên
create or alter  proc xoa1NV
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
go
-- Capnhatthongtin: Cập nhật thông tin nhân viên
CREATE   or alter    proc [dbo].[Capnhatthongtin] (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10), @chucvu nvarchar(20), @manv_cu nchar(10))
as begin
if(select count(manv)from NHANVIEN where MANV= @manv_cu)=0
print(N'Không có nhân viên cần sửa')
else
begin
if(@makhu!='')
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI, LUONG=@luong, Makhu=@makhu, MANV=@MANV, chucvu=@chucvu
where MANV=@manv_cu
else
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI, LUONG=@luong, Makhu=null, MANV=@MANV, chucvu=@chucvu
where MANV=@manv_cu
print(N'Đã sửa thông tin nhân viên thành công')
end
end
GO
--tăng mã
create or alter function auto_manv()
returns nchar(10)
as
begin
	declare @id nchar(10)
	if(select COUNT(manv)from NHANVIEN)=0
		set @id=0
	else
		select @id= MAX(right(manv,2)) from nhanvien
	select @id = case
		when @id >=0 and @id <9 then 'NV0'+ CONVERT(char, convert(int,@id)+1)
		when @id >=9 and @id <9 then 'NV'+ CONVERT(char, convert(int,@id)+1)
end
return @id
end
go
alter table nhanvien
 add constraint automanv default dbo.auto_manv() for manv 
 go
--capNhatNV: Cập nhật lại lương của nhân viên của khu vui chơi
create or alter proc capNhatNV
@ma nchar(10), @luong money
as begin
update NHANVIEN set LUONG=@luong where MANV=@ma
End
go
--timkiemNV: Tìm kiếm nhân viên theo mã nhân viên
create or alter proc timkiemNV
@manv nvarchar(10)
as begin
select * from NHANVIEN where MANV=@manv
end
go
--themTrochoi: Thêm một trò chơi
create or  alter proc themTrochoi(@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
  
as begin 
 
insert into TROCHOI(MATC,TENTC,MAKHU) 
 
values(@MATC,@TENTC,@MAKHU) 
 
end  
go
--CapnhatTrochoi: Cập nhật lại dịch vụ của khu vui chơi
create or alter proc  capnhatTroChoi(@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
as begin
update TROCHOI set MAKHU= @MAKHU,TENTC=@TENTC where MATC=@MATC
end
go
-- XoaTroChoi:
create or alter proc xoaTroChoi
@MATC nchar(10)
as begin
delete from TROCHOI where MATC=@MATC
end
go
--suaDICHVU: Cập nhật lại dịch vụ của khu vui chơi
create or alter proc suaDICHVU
(@madv nchar(10), @tendv nvarchar(50), @maldv nchar(10), @dongia money)
as begin
update DICHVU set
TENDV=@tendv, MALDV=@maldv, DONGIA=@dongia
where MADV=@madv
end
go
--xoaDICHVU: Xóa đi một dịch vụ của khu vui chơi
create or alter proc xoaDICHVU @madv nchar(10)
as begin
delete from DICHVU where DICHVU.MADV=@madv 
delete from CHITIETBL where CHITIETBL.MADV=@madv
end
go
--suaLOAIDV: Cập nhật lại thông tin của loại dịch vụ
create or alter proc suaLOAIDV
(@maldv nchar(10), @tenldv nvarchar(50))
as begin
update LOAIDV set
TENLDV=@tenldv
where MALDV=@maldv
end
go
--xoaLOAIDV: Xóa một loại dịch vụ
create or alter proc xoaLOAIDV @maldv nchar(10)
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
go
--Thông tin trochoi: Xem thông tin trò chơi 
create or alter proc ThongTinTroChoi
as begin
select TENKHU, TENTC from KHUVUICHOI,TROCHOI
WHERE TROCHOI.MAKHU=KHUVUICHOI.MAKHU
end
go
-- themKHUVUICHOI: Thêm khu vui chơi
create or alter proc themKHUVUICHOI(@makhu nchar(10),@tenkhu nvarchar(50),@giavenl money,@giavete money)
 as begin
 insert into dbo.KHUVUICHOI(MAKHU, TENKHU, GIAVENL, GIAVETE)
 values(@makhu,@tenkhu,@giavenl,@giavete)
 end
go
--CapnhatKhuVuiChoi: Cập nhật lại thông tin của khu vui chơi
create or alter proc CapNhatKhuVuiChoi(@MAKHU nchar(10),@TENKHU nvarchar(50),@GIAVENL money,@GIAVETE money)

as begin
update KHUVUICHOI set                      TENKHU=@TENKHU,GIAVENL=@GIAVENL,GIAVETE=@GIAVETE
             where MAKHU=@MAKHU
end
go
 -- XoaKhuVuiChoi: Xóa khu vui chơi
create or alter proc xoaKhuVuiChoi
         @MAKHU nchar(10)
as begin
delete from KHUVUICHOI where MAKHU=@MAKHU
end
go
--timkiemtenKVC:Tìm kiếm khu vui chơi theo tên

CREATE OR ALTER proc timkiemtenKVC 

@search nvarchar(10) 

as begin 

select MAKHU as N'Mã khu',TENKHU as N'Tên khu',GIAVENL as N'Giá vé người lớn',GIAVETE as N'Giá vé trẻ em' from KHUVUICHOI where TENKHU like '%' +@search +'%' 

end 

GO
--timkiemmaKVC:tìm kiếm khu vui chơi theo mã
CREATE OR ALTER proc timkiemmaKVC

@search nvarchar(10) 

as begin 

select MAKHU as N'Mã khu',TENKHU as N'Tên khu',GIAVENL as N'Giá vé người lớn',GIAVETE as N'Giá vé trẻ em' from KHUVUICHOI where MAKHU like '%' +@search +'%' 

end 

GO
--auto_makhu :Tự động tăng mã khu
CREATE OR ALTER     proc [dbo].[auto_makhu]
as
begin
	declare @makhu nchar(10)
	declare  @id int
	set @id=0
	set @makhu='K00'
	while exists (select MAKHU from KHUVUICHOI where MAKHU = @makhu )
	begin
		set @id=@id+1
		set @makhu= 'K'+ (REPLICATE('0',2- LEN(CAST(@id as varchar)))+CAST(@id as varchar))
	end
	select @makhu
end
GO

-- Capnhatthongtin: Cập nhật thông tin nhân viên
create or alter proc Capnhatthongtin (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10))
as begin
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI
where MANV=@MANV
print(N'Đã sửa thông tin nhân viên thành công')
end
go
--THEMDICHVU: Thêm dịch vụ khi đã có mã loại dịch vụ
CREATE OR ALTER PROC THEMDICHVU 
 
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
go

Create or ALTER   PROC [dbo].[auto_MaLDV]
as
begin
declare @ma_next varchar(10)
declare @max int
select @max=COUNT(MALDV) + 1 from LOAIDV where MALDV like '%ldv%'
select @ma_next='ldv' + RIGHT('0' + CAST(@max as varchar(7)),7)
while(exists(select MALDV from LOAIDV where MALDV=@ma_next))
begin
     set @max=@max+1
	 set @ma_next='ldv'+ RIGHT('0' + CAST(@max as varchar(7)),7)
end
select @ma_next
end
go
-- THEMLOAIDICHVU: Thêm loại dịch vụ cho khu vui chơi
CREATE OR ALTER PROC THEMlOAIDICHVU 
 
         @MALDV nchar(10),
 @TENLDV nvarchar(25)
 
 
as begin 
 
if (select COUNT(LOAIDV.MALDV) from LOAIDV where LOAIDV.MALDV = @MALDV) != 0
print(N'ĐÃ CÓ LOẠI DỊCH VỤ NÀY!')
else
insert LOAIDV(MALDV, TENLDV) values(@MALDV, @TENLDV)
end
go
-- Hàm:
   -- Hàm tinhTienDV :Tính tiền dịch vụ
CREATE OR ALTER FUNCTION tinhTienDV (@SOBL NCHAR(10), @MADV NCHAR(10))
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


--Thông tin nhân viên trong khu vui chơi
CREATE OR ALTER FUNCTION thonginnhanvien()
RETURNS TABLE
RETURN
(
SELECT * FROM NHANVIEN 
)
go
--Thông tin dịch vụ trong khu vui chơi
CREATE OR ALTER FUNCTION thongtindichvu()
RETURNS TABLE
RETURN
(
SELECT * FROM DICHVU
)
go
--Thông tin dịch vụ của loại dịch vụ có mã ” mã_loại_dịch_vụ”
SELECT * FROM DICHVU JOIN LOAIDV ON LOAIDV.MALDV = DICHVU.MADV WHERE DICHVU.MALDV=' mã_loại_dịch_vụ'
go
-- Hàm thongKeLuong: Thống kê lương của nhân viên
CREATE OR ALTER FUNCTION thongKeLuong()
RETURNS TABLE
RETURN
(
SELECT MANV, TENNV, DIACHI, LUONG
FROM NHANVIEN
)
go
--Thêm nhân viên

CREATE OR ALTER       proc [dbo].[themNV]
@MANV nchar(10),@TENNV nvarchar(50),@NGAYSINH date, @SDT nchar(10), @GIOITINH 	nchar(3),@LUONG money,@chucvu nvarchar(20),@MAKHU nchar(10),@DIACHI nvarchar(50)
as begin 
if(select COUNT(*)from NHANVIEN where MANV =@MANV)>0
begin
print(N'Đã có mã nhân viên này mời nhập lại')
end
else
begin
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,MAKHU,DIACHI,
chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@MAKHU,@DIACHI,@chucvu)
print(N'Đã thêm nhân viên thành công')
end
end
GO

--Sửa thông tin nhân viên

CREATE OR ALTER       proc [dbo].[Capnhatthongtin] (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10), @chucvu nvarchar(20), @manv_cu nchar(10))
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
--Xóa nhân viên

CREATE OR ALTER    proc [dbo].[xoa1NV]
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


-- Trigger:
 	--gioiTinhNhanVien(Nhân viên chỉ có giới tính nam hoặc nữ) 
create or alter trigger gioiTinhNhanVien on dbo.NHANVIEN
after insert, update
as begin
if exists (select * from inserted i
where i.GIOITINH not in (N'Nam',N'Nữ'))
begin
raiserror(N'Sai giới tính nhân viên!', 16, 1)
rollback tran
end
End
go
--xóa nhân viên, biên lai, vé, chi tiết biên lai
		create or alter trigger xoanhanvien on nhanvien instead of delete
		as begin
			--delete from CHITIETBL where CHITIETBL.SOBL in(select deleted.sobl from deleted)
			delete from BIENLAI where BIENLAI.MANV in (select deleted.MANV from deleted)
			delete from VE where VE.MANV in (select deleted.MANV from deleted)
			delete from NHANVIEN where manv in (select deleted.MANV from deleted)
		end
go
		create or alter trigger xoabienlai on bienlai instead of delete
		as begin
			delete from CHITIETBL where CHITIETBL.SOBL in (select deleted.sobl from deleted)
			delete from BIENLAI where SOBL in (select deleted.sobl from deleted)
		end


go
alter table nhanvien add chucvu nvarchar(20)--(nhân viên/quản lý)
alter table nhanvien add MATKHAU nchar(20)
alter table ve add giavenl money 
alter table ve add giavete money 
alter table ve add trangthai NVARCHAR(20)
alter table bienlai add trangthai NVARCHAR(50)--(đã thanh toán/chưa thanh toán)
alter table bienlai add DONGIA money
--select*from NHANVIEN
--dangnhap: Đăng nhập vào chương trình quản lý
go
CREATE OR ALTER PROC dangNhap @MANV NCHAR(10), @MATKHAU NVARCHAR(20)
AS
BEGIN
select * from NHANVIEN where MANV= @MANV and MATKHAU= @MATKHAU
END
go
--doiMK: Đổi mật khẩu
CREATE OR ALTER PROC doiMK @MANV NCHAR(10), @MATKHAUMOI NVARCHAR(20)
AS
BEGIN
UPDATE NHANVIEN
SET MATKHAU=@MATKHAUMOI
WHERE MANV=@MANV
END
go
--themVE: Thêm vé bán ra
create or  alter proc themVE
@mave nchar(10), @soluongnl int,@soluongte int, @makhu nchar(10),
@manv nchar(10), @ngayban date, @tongtien money
as begin
insert into dbo.VE(MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, NGAYBAN, TONGTIEN)
values(@mave, @soluongnl, @soluongte, @makhu, @manv, @ngayban, @tongtien)
end 
go
--suaVE: Cập nhật lại vé của khu vui chơi
create or alter proc suaVE
@mave nchar(10),@soluongnl int, @soluongte int
as begin
declare @a money, @b money
select @a= k.GIAVENL, @b=k.GIAVETE from KHUVUICHOI as k, VE as v where k.MAKHU= v.MAKHU and v.MAVE=@mave
update VE set SOLUONGNL=@soluongnl, SOLUONGTE=@soluongte,
TONGTIEN=CAST(@soluongnl as money)*@a+CAST(@soluongte as money)*@b
where MAVE=@mave
End
go
go

--xoaVE : Xóa đi  1 vé
create or alter proc xoaVE
@ma nchar(10)
as begin
delete from VE where MAVE=@ma
end
go

--themNV: Thêm nhân viên v
create or alter proc themNV
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
go

--timkiemVe: Tìm kiếm vé Theo mã vẽ
create or alter proc timkiemVe
@mave nvarchar(10)
as begin
select * from VE where MAVE=@mave
end
go
--xoa1NV: Xóa đi một nhân viên
create or alter  proc xoa1NV
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
go
-- Capnhatthongtin: Cập nhật thông tin nhân viên
create or alter proc Capnhatthongtin (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10))
as begin
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI
where MANV=@MANV
print(N'Đã sửa thông tin nhân viên thành công')
end
go
--tăng mã
create or alter function auto_manv()
returns nchar(10)
as
begin
	declare @id nchar(10)
	if(select COUNT(manv)from NHANVIEN)=0
		set @id=0
	else
		select @id= MAX(right(manv,2)) from nhanvien
	select @id = case
		when @id >=0 and @id <9 then 'NV0'+ CONVERT(char, convert(int,@id)+1)
		when @id >=9 and @id <9 then 'NV'+ CONVERT(char, convert(int,@id)+1)
end
return @id
end
go
alter table nhanvien
 add constraint automanv default dbo.auto_manv() for manv 
 go
--capNhatNV: Cập nhật lại lương của nhân viên của khu vui chơi
create or alter proc capNhatNV
@ma nchar(10), @luong money
as begin
update NHANVIEN set LUONG=@luong where MANV=@ma
End
go
--timkiemNV: Tìm kiếm nhân viên theo mã nhân viên
create or alter proc timkiemNV
@manv nvarchar(10)
as begin
select * from NHANVIEN where MANV=@manv
end
go
--themTrochoi: Thêm một trò chơi
create or  alter proc themTrochoi(@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
  
as begin 
 
insert into TROCHOI(MATC,TENTC,MAKHU) 
 
values(@MATC,@TENTC,@MAKHU) 
 
end  
go
--CapnhatTrochoi: Cập nhật lại dịch vụ của khu vui chơi
create or alter proc  capnhatTroChoi(@MATC nchar(10),@TENTC nvarchar(50),@MAKHU nchar(10))
as begin
update TROCHOI set MAKHU= @MAKHU,TENTC=@TENTC where MATC=@MATC
end
go
-- XoaTroChoi:
create or alter proc xoaTroChoi
@MATC nchar(10)
as begin
delete from TROCHOI where MATC=@MATC
end
go
--suaDICHVU: Cập nhật lại dịch vụ của khu vui chơi
create or alter proc suaDICHVU
(@madv nchar(10), @tendv nvarchar(50), @maldv nchar(10), @dongia money)
as begin
update DICHVU set
TENDV=@tendv, MALDV=@maldv, DONGIA=@dongia
where MADV=@madv
end
go
--xoaDICHVU: Xóa đi một dịch vụ của khu vui chơi
create or alter proc xoaDICHVU @madv nchar(10)
as begin
delete from DICHVU where DICHVU.MADV=@madv 
delete from CHITIETBL where CHITIETBL.MADV=@madv
end
go
--suaLOAIDV: Cập nhật lại thông tin của loại dịch vụ
create or alter proc suaLOAIDV
(@maldv nchar(10), @tenldv nvarchar(50))
as begin
update LOAIDV set
TENLDV=@tenldv
where MALDV=@maldv
end
go
--xoaLOAIDV: Xóa một loại dịch vụ
create or alter proc xoaLOAIDV @maldv nchar(10)
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
go
--Thông tin trochoi: Xem thông tin trò chơi 
create or alter proc ThongTinTroChoi
as begin
select TENKHU, TENTC from KHUVUICHOI,TROCHOI
WHERE TROCHOI.MAKHU=KHUVUICHOI.MAKHU
end
go

-- Capnhatthongtin: Cập nhật thông tin nhân viên
create or alter proc Capnhatthongtin (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10))
as begin
update NHANVIEN set   TENNV=@TENNV,
                                   NGAYSINH=@Ngaysinh,
                                   SDT=@SDT,
                                   GIOITINH=@GIOITINH,
     DIACHI=@DIACHI
where MANV=@MANV
print(N'Đã sửa thông tin nhân viên thành công')
end
go
--THEMDICHVU: Thêm dịch vụ khi đã có mã loại dịch vụ
CREATE OR ALTER PROC THEMDICHVU 
 
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
go
-- THEMLOAIDICHVU: Thêm loại dịch vụ cho khu vui chơi
CREATE OR ALTER PROC THEMlOAIDICHVU 
 
         @MALDV nchar(10),
 @TENLDV nvarchar(25)
 
 
as begin 
 
if (select COUNT(LOAIDV.MALDV) from LOAIDV where LOAIDV.MALDV = @MALDV) != 0
print(N'ĐÃ CÓ LOẠI DỊCH VỤ NÀY!')
else
insert LOAIDV(MALDV, TENLDV) values(@MALDV, @TENLDV)
end
go
-- Hàm:
   -- Hàm tinhTienDV :Tính tiền dịch vụ
CREATE OR ALTER FUNCTION tinhTienDV (@SOBL NCHAR(10), @MADV NCHAR(10))
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


--Thông tin nhân viên trong khu vui chơi
CREATE OR ALTER FUNCTION thonginnhanvien()
RETURNS TABLE
RETURN
(
SELECT * FROM NHANVIEN 
)
go
--Thông tin dịch vụ trong khu vui chơi
CREATE OR ALTER FUNCTION thongtindichvu()
RETURNS TABLE
RETURN
(
SELECT * FROM DICHVU
)
go
--Thông tin dịch vụ của loại dịch vụ có mã ” mã_loại_dịch_vụ”
SELECT * FROM DICHVU JOIN LOAIDV ON LOAIDV.MALDV = DICHVU.MADV WHERE DICHVU.MALDV=' mã_loại_dịch_vụ'
go
-- Hàm thongKeLuong: Thống kê lương của nhân viên
CREATE OR ALTER FUNCTION thongKeLuong()
RETURNS TABLE
RETURN
(
SELECT MANV, TENNV, DIACHI, LUONG
FROM NHANVIEN
)
go
--Thêm nhân viên

CREATE OR ALTER       proc [dbo].[themNV]
@MANV nchar(10),@TENNV nvarchar(50),@NGAYSINH date, @SDT nchar(10), @GIOITINH 	nchar(3),@LUONG money,@chucvu nvarchar(20),@MAKHU nchar(10),@DIACHI nvarchar(50)
as begin 
if(select COUNT(*)from NHANVIEN where MANV =@MANV)>0
begin
print(N'Đã có mã nhân viên này mời nhập lại')
end
else
begin
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,MAKHU,DIACHI,
chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@MAKHU,@DIACHI,@chucvu)
print(N'Đã thêm nhân viên thành công')
end
end
GO

--Sửa thông tin nhân viên

CREATE OR ALTER       proc [dbo].[Capnhatthongtin] (@MANV nchar(10),@TENNV nvarchar(50),@Ngaysinh date,@SDT nchar(10) ,@GIOITINH nchar(3),@DIACHI nvarchar(50),@luong money, @makhu nchar(10), @chucvu nvarchar(20), @manv_cu nchar(10))
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
--Xóa nhân viên

CREATE OR ALTER    proc [dbo].[xoa1NV]
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


-- Trigger:
 	--gioiTinhNhanVien(Nhân viên chỉ có giới tính nam hoặc nữ) 
create or alter trigger gioiTinhNhanVien on dbo.NHANVIEN
after insert, update
as begin
if exists (select * from inserted i
where i.GIOITINH not in (N'Nam',N'Nữ'))
begin
raiserror(N'Sai giới tính nhân viên!', 16, 1)
rollback tran
end
End
go
--xóa nhân viên, biên lai, vé, chi tiết biên lai
		create or alter trigger xoanhanvien on nhanvien instead of delete
		as begin
			--delete from CHITIETBL where CHITIETBL.SOBL in(select deleted.sobl from deleted)
			delete from BIENLAI where BIENLAI.MANV in (select deleted.MANV from deleted)
			delete from VE where VE.MANV in (select deleted.MANV from deleted)
			delete from NHANVIEN where manv in (select deleted.MANV from deleted)
		end
go
		create or alter trigger xoabienlai on bienlai instead of delete
		as begin
			delete from CHITIETBL where CHITIETBL.SOBL in (select deleted.sobl from deleted)
			delete from BIENLAI where SOBL in (select deleted.sobl from deleted)
		end
go
--Hàm tăng mã trò chơi

Create or Alter function at_ma_tc()
returns nchar(10)
as
begin
declare @id int
declare @matc nchar(10)
if(Select Count(matc) from trochoi)=0
   set @id=0
ELSE
    Select @id= Count(*) from trochoi
	Select @matc = case
	    When @id>=0 and @id <9 then 'TC00' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=9 and @id <99 then 'TC0' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=99 then 'TC' + Convert(CHAR, Convert(int,@id)+1)
	end
--Kiem tra ton tai
while(exists(select matc from trochoi where matc=@matc))
begin
   set @id=@id+1
   Select @matc = case
	    When @id>=0 and @id <9 then 'TC00' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=9 and @id <99 then 'TC0' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=99 then 'TC' + Convert(CHAR, Convert(int,@id)+1)
   end
end
return @matc

end
go

--select dbo.at_ma_ve()
go
--Proc thêm trò chơi
Create or Alter proc themTC  @tentc  nvarchar(50), @makhu nchar(10)
as
begin
declare @matc nchar(10)
select @matc=dbo.at_ma_tc()
Insert into TROCHOI(matc, tentc, makhu) 
values (@matc, @tentc, @makhu)
end
go
--Proc sửa trò chơi
Create or Alter proc suaTroChoi @maTC nchar(10), @tenTC nvarchar(50), @maKhu nchar(10)
as
begin
Update trochoi 
set TENTC=@tenTC, MAKHU=@maKhu
where MATC=@maTC
end
go
--Proc xóa trò chơi
Create or Alter proc xoaTroChoi @maTC nchar(10)
as
begin
delete from trochoi where MATC=@maTC
end
go

--Produce tìm kiếm trò chơi
Create or Alter function timKiemTroChoi (@tenTC nvarchar(50), @khuTC nchar(10))
returns Table
as
return (Select * from TROCHOI where TENTC like N'%'+@tenTC+'%' and MAKHU = @khuTC)
go
--Proc tìm kiếm trò chơi
Create or Alter proc timKiemTC @tenTC nvarchar(50), @khuTC nchar(10)
as
begin
Select * from TROCHOI where TENTC like N'%'+@tenTC+'%' and MAKHU = @khuTC
end
go
--Hàm tăng mã vé
Create or Alter function at_ma_ve()
returns nchar(10)
as
begin
declare @id int
declare @mave nchar(10)
if(Select Count(MAVE) from VE)=0
   set @id=0
ELSE
    Select @id= Count(*) from VE
	Select @mave = case
	    When @id>=0 and @id <9 then 'VE00' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=9 and @id <99 then 'VE0' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=99 then 'VE' + Convert(CHAR, Convert(int,@id)+1)
	end
--Kiem tra ton tai
while(exists(select MAVE from VE where MAVE = @mave))
begin
   set @id=@id+1
   Select @mave = case
	    When @id>=0 and @id <9 then 'VE00' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=9 and @id <99 then 'VE0' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=99 then 'VE' + Convert(CHAR, Convert(int,@id)+1)
   end
end
return @mave
end
go
CREATE OR ALTER proc auto_manv1
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
alter table nhanvien drop constraint automanv
drop proc exec auto_manv1


USE [KHUVUICHOIGIAITRI]
GO

/****** Object:  StoredProcedure [dbo].[themNV]    Script Date: 4/29/2021 8:36:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Thêm nhân viên

create or alter  proc [dbo].[themNV]
@MANV nchar(10),@TENNV nvarchar(50),@NGAYSINH date, @SDT nchar(10), @GIOITINH 	nchar(3),@LUONG money,@chucvu nvarchar(20),@MAKHU nchar(10),@DIACHI nvarchar(50), @MATKHAU NCHAR(20)
as begin 
if(select COUNT(*)from NHANVIEN where MANV =@MANV)>0
begin
print(N'Đã có mã nhân viên này mời nhập lại')
end
else
begin
if( @MAKHU!='')
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,MAKHU,DIACHI,
MATKHAU, chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@MAKHU,@DIACHI,@MATKHAU, @chucvu)
else if(@MAKHU='')
insert  into dbo.NHANVIEN(MANV,TENNV,NGAYSINH,SDT,GIOITINH,LUONG,DIACHI,
MATKHAU, chucvu)
values(@MANV,@TENNV,@NGAYSINH, @SDT, @GIOITINH,@LUONG,@DIACHI,@MATKHAU, @chucvu)
print(N'Đã thêm nhân viên thành công')
end
end
GO

USE [KHUVUICHOIGIAITRI]
GO
/****** Object:  StoredProcedure [dbo].[auto_MaDV]    Script Date: 4/29/2021 8:47:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create or ALTER   PROC [dbo].[auto_MaDV]
as
begin
declare @ma_next varchar(10)
declare @max int
select @max=COUNT(MADV) + 1 from DICHVU where MADV like '%dv%'
select @ma_next='dv' + RIGHT('0' + CAST(@max as varchar(8)),8)
while(exists(select MADV from DICHVU where MADV=@ma_next))
begin
     set @max=@max+1
	 set @ma_next='dv'+ RIGHT('0' + CAST(@max as varchar(7)),7)
end
select @ma_next
end

USE [KHUVUICHOIGIAITRI]
GO
/****** Object:  StoredProcedure [dbo].[auto_MaLDV]    Script Date: 4/29/2021 8:46:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create or ALTER   PROC [dbo].[auto_MaLDV]
as
begin
declare @ma_next varchar(10)
declare @max int
select @max=COUNT(MALDV) + 1 from LOAIDV where MALDV like '%ldv%'
select @ma_next='ldv' + RIGHT('0' + CAST(@max as varchar(7)),7)
while(exists(select MALDV from LOAIDV where MALDV=@ma_next))
begin
     set @max=@max+1
	 set @ma_next='ldv'+ RIGHT('0' + CAST(@max as varchar(7)),7)
end
select @ma_next
end
