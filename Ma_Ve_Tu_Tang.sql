Create or Alter function at_ma_ve()
returns nchar(10)
as
begin
declare @id nchar(10), @id2 nchar(10)
if(Select Count(MAVE) from VE)=0
begin
   set @id=0
   set @id2=0
end
ELSE
    Select @id= Count(*) from VE
	Set @id2=@id
	Select @id = case
	    When @id>=0 and @id <9 then 'VE00' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=9 and @id <99 then 'VE0' + Convert(CHAR, Convert(int,@id)+1)
		When @id>=99 then 'VE' + Convert(CHAR, Convert(int,@id)+1)
	end
--Kiem tra ton tai
while(exists(select MAVE from VE where MAVE=@id))
begin
   set @id2=@id+2
   Select @id = case
	    When @id2>=0 and @id2 <9 then 'VE00' + Convert(CHAR, Convert(int,@id2)+1)
		When @id2>=9 and @id2 <99 then 'VE0' + Convert(CHAR, Convert(int,@id2)+1)
		When @id2>=99 then 'VE' + Convert(CHAR, Convert(int,@id2)+1)
   end
return @id2
end
return @id

end
go

--select dbo.at_ma_ve() as N'Mã Vé'