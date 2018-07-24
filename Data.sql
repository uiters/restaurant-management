create database QL_NhaHang
go
use Ql_NhaHang
go 
create table TableFood
(
id int identity primary key,
name nvarchar(100) not null default N'Chưa đặt tên',
status nvarchar(100) not null default N'Trống'-- || Có người 
)
go 
create table Account
(
UserName nvarchar(100) primary key,
DisplayName nvarchar(100) not null default N'Nhân viên',
PassWord nvarchar(1000) not null,
Type int default 0--staff
)
go
create table FoodCategory
(
id int identity primary key,
name nvarchar(100) not null default N'Chưa đặt tên'
)
go 
create table Food
(
id int identity primary key,
name nvarchar(100) not null default N'Chưa đặt tên',
idCategory int not null,
price float not null default 0
foreign key (idCategory) references FoodCategory(id)
)
go 
create table Bill
(
id int identity primary key,
DateCheckIn DATETIME not null default GetDate(),
DateCheckOut DATETIME,
idTable int not null,
status int not null default 0,--Đã thanh toán:1, chưa thanh toán:0
discount int not null default 0
foreign key (idTable) references TableFood(id)
)
GO 

create table BillInfo
(
id int identity primary key,
idBill int not null,
idFood int not null,
count int not null default 0
foreign key(idBill) references Bill(id),
foreign key(idFood) references Food(id)
)
go
CREATE Procedure USP_Login
@user nvarchar(100),@pass nvarchar(100)
AS
BEGIN
SELECT * FROM dbo.Account WHERE UserName=@user AND PassWord=@pass
END

GO
CREATE PROCEDURE USP_LoadMenuByIdTable
@idTable int
AS
BEGIN
SELECT c.name,B.count,A.discount,c.price
FROM dbo.Bill A, dbo.BillInfo B, dbo.Food c
WHERE A.id=b.idBill AND c.id=b.idFood AND A.idTable=@idTable AND A.status=0
END

GO
CREATE PROCEDURE USP_LoadFoodListByIdCategory
@idCate INT
AS
BEGIN
SELECT * FROM dbo.Food WHERE idCategory=@idCate
END
GO 


--TH1: Bàn chưa có Bill, khi thêm món ăn thì tạo Bill cho bàn--> tạo BillInfo
--TH2: Bàn đã có Bill, tuy nhiên thêm món mới-->thêm idFood vào BillInfo+ Count
--TH3: Bàn đã có Bill, chỉ thêm số lượng món ăn đã tồn tại-->tăng count theo idFood
--Thêm món ăn vào bàn
CREATE PROCEDURE USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill 
	        ( DateCheckIn ,
	          DateCheckOut ,
	          idTable ,
	          status,
	          discount
	        )
	VALUES  ( GETDATE() , -- DateCheckIn - date
	          NULL , -- DateCheckOut - date
	          @idTable , -- idTable - int
	          0,  -- status - int
	          0
	        )
END

GO
CREATE PROCEDURE USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idFood = @idFood

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idFood, count )
		VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
	END
END


GO 
CREATE TRIGGER UTG_InsertBillInfo
ON dbo.BillInfo
FOR INSERT
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill=idBill FROM Inserted
	DECLARE @idTable INT,@count INT=0
	SELECT @idTable=idTable FROM dbo.Bill WHERE id=@idBill
	SELECT @count=COUNT(*) FROM dbo.BillInfo WHERE idBill=@idBill--@count không cần thiết nhưng để chắc kèo, tránh những trường hợp PROC xét thiếu
	IF(@count>0)
	UPDATE dbo.TableFood SET status=N'Có người' WHERE id=@idTable
	ELSE UPDATE dbo.TableFood SET status=N'Trống' WHERE id=@idTable
END 
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill
FOR UPDATE,INSERT
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill=id FROM Inserted
	DECLARE @idTable INT
	SELECT @idTable=idTable FROM dbo.Bill WHERE id=@idBill
	DECLARE @countBill INT=0,@countBillInfo INT=0
	SELECT @countBillInfo=COUNT(*) FROM dbo.BillInfo WHERE idBill=@idBill
	SELECT @countBill=COUNT(*) FROM dbo.Bill WHERE status=0 AND idTable=@idTable
	IF(@countBill>0 AND @countBillInfo>0) UPDATE dbo.TableFood SET status=N'Có người' WHERE id=@idTable
	ELSE UPDATE dbo.TableFood SET status=N'Trống' WHERE id=@idTable
END 
go
CREATE PROCEDURE USP_ListPay
@dateCheckOut DATE
AS
BEGIN 
SELECT a.name [Tablename],b.DateCheckIn,b.DateCheckOut,b.discount,SUM(c.price*d.count) [TotalPrice],SUM(c.price*d.count*(100-b.discount)/100) [FinalPrice]
FROM dbo.TableFood a, dbo.Bill b,dbo.Food c,dbo.BillInfo d
WHERE b.idTable=a.id AND b.id=d.idBill AND c.id=d.idFood AND b.status=1 AND YEAR(b.DateCheckOut) =YEAR(@dateCheckOut) AND MONTH(b.DateCheckOut) =MONTH(@dateCheckOut) AND DAY(b.DateCheckOut) = DAY(@dateCheckOut)
GROUP BY a.name,b.DateCheckIn,b.DateCheckOut,b.discount
ORDER BY b.DateCheckOut DESC
END 
GO
CREATE PROC SwitchTable
@idTable1 INT,@idTable2 int
AS
BEGIN
	DECLARE @idBill1 INT,@idBill2 INT,@countBillInfo1 INT,@countBillInfo2 INT
	SELECT @idBill1=id FROM dbo.Bill WHERE idTable=@idTable1 AND status=0
	SELECT @idBill2=id FROM dbo.Bill WHERE idTable=@idTable2 AND status=0
	SELECT @countBillInfo1=COUNT(*) FROM dbo.BillInfo WHERE idBill=@idBill1
	SELECT @countBillInfo2=COUNT(*) FROM dbo.BillInfo WHERE idBill=@idBill2
		UPDATE dbo.Bill SET idTable=@idTable2 WHERE id=@idBill1
		UPDATE dbo.Bill SET idTable=@idTable1 WHERE id=@idBill2
    IF(@countBillInfo1=0) UPDATE dbo.TableFood SET status=N'Trống' WHERE id=@idTable2
	IF(@countBillInfo2=0) UPDATE dbo.TableFood SET status=N'Trống' WHERE id=@idTable1
END
GO 
EXEC dbo.SwitchTable @idTable1 = 3, -- int
                     @idTable2 = 20  -- int
GO
CREATE PROCEDURE USP_GetListBill
@fromDate DATE,@toDate DATE
AS
BEGIN 
SELECT a.name [Tên bàn],b.DateCheckIn [Ngày vào],b.DateCheckOut [Ngày ra],b.discount [Giảm giá],SUM(c.price*d.count) [Tổng tiền],SUM(c.price*d.count*(100-b.discount)/100) [Thành tiền]
FROM dbo.TableFood a, dbo.Bill b,dbo.Food c,dbo.BillInfo d
WHERE b.idTable=a.id AND b.id=d.idBill AND c.id=d.idFood AND b.status=1 AND b.DateCheckIn>=@fromDate AND b.DateCheckOut<=@toDate
GROUP BY a.name,b.DateCheckIn,b.DateCheckOut,b.discount
END 
GO

CREATE PROCEDURE USP_GetAccount
@userName NVARCHAR(100),@passWord NVARCHAR(100)
as
SELECT * FROM dbo.Account WHERE UserName=@userName AND PassWord=@passWord
GO
CREATE PROCEDURE USP_ChangeAccount
@userName NVARCHAR(100),@displayName NVARCHAR(100),@newPassWord NVARCHAR(100)
AS
BEGIN
UPDATE dbo.Account
SET DisplayName=@displayName,PassWord=@newPassWord
WHERE UserName=@userName
END
GO
CREATE PROCEDURE USP_ChangeAccountOnlyDislayName
@userName NVARCHAR(100),@displayName NVARCHAR(100)
AS
BEGIN
UPDATE dbo.Account
SET DisplayName=@displayName
WHERE UserName=@userName
END
GO
CREATE PROC USP_GetDisplayNameByUserName
@userName Nvarchar(100)
AS
BEGIN
SELECT * FROM dbo.Account WHERE UserName=@userName
END 
GO
CREATE PROC USP_getFoodList
AS
BEGIN
SELECT a.id [ID],a.name [Tên món ăn],b.name [Danh mục],a.price [Giá]
FROM dbo.Food a INNER JOIN dbo.FoodCategory b ON b.id = a.idCategory
END 
GO
--Chuyển định dạng chữ tiếng việt có dấu sang không dấu\
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
go
CREATE PROC USP_getFoodListByName
@name NVARCHAR(100)
AS
BEGIN
SELECT a.id [ID],a.name [Tên món ăn],b.name [Danh mục],a.price [Giá]
FROM dbo.Food a INNER JOIN dbo.FoodCategory b ON b.id = a.idCategory
WHERE dbo.fuConvertToUnsign1(a.name) LIKE '%'+dbo.fuConvertToUnsign1(@name)+'%'
END 
GO
CREATE PROC USP_InsertFood
@name NVARCHAR(100),@idCate INT,@price FLOAT
as
INSERT INTO dbo.Food (name,idCategory,price) VALUES(@name,@idCate,@price)
GO
CREATE PROC USP_UpdateFood
@name NVARCHAR(100),@idCate INT,@price FLOAT,@id int
as
UPDATE dbo.Food SET name=@name,idCategory=@idCate,price=@price WHERE id=@id
GO
CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo
FOR DELETE
AS
BEGIN
DECLARE @idBill INT,@idTable INT,@count INT=0
SELECT @idBill=idBill FROM Deleted
SELECT @idTable=idTable FROM dbo.Bill WHERE id=@idBill
SELECT @count=COUNT(*) 
FROM dbo.BillInfo A INNER JOIN dbo.Bill B ON B.id = A.idBill
WHERE B.status=0 AND b.id=@idBill
IF(@count=0) UPDATE dbo.TableFood SET status=N'Trống' WHERE id=@idTable
END 
go
SELECT id [ID], name [Tên danh mục] FROM dbo.FoodCategory
GO
CREATE PROC USP_InsertCate
@name NVARCHAR(100)
as
INSERT dbo.FoodCategory
(
    name
)
VALUES
(@name -- name - nvarchar(100)
    )
GO
CREATE PROCEDURE USP_UpdateCate
@name NVARCHAR(100),@id int
AS
UPDATE dbo.FoodCategory SET name=@name WHERE id=@id
GO
CREATE PROCEDURE USP_InsertTable
@name NVARCHAR(100),@status NVARCHAR(100)
AS
INSERT dbo.TableFood
(
    name,
    status
)
VALUES
(   @name, -- name - nvarchar(100)
    @status  -- status - nvarchar(100)
    )
GO
CREATE PROCEDURE USP_UpdateTable
@id INT,@name NVARCHAR(100),@status NVARCHAR(100)
AS
UPDATE dbo.TableFood SET name=@name,status=@status WHERE id=@id
GO
CREATE PROC USP_InsertAcc
@user NVARCHAR(100),@display NVARCHAR(100),@pass NVARCHAR(100),@type int
AS
INSERT dbo.Account
(
    UserName,
    DisplayName,
    PassWord,
    Type
)
VALUES
(   @user, -- UserName - nvarchar(100)
    @display, -- DisplayName - nvarchar(100)
    @pass, -- PassWord - nvarchar(1000)
    @type    -- Type - int
    )
GO
CREATE PROCEDURE USP_UpdateAcc
@display NVARCHAR(100),@type INT,@user NVARCHAR(100)
AS
UPDATE dbo.Account SET DisplayName=@display, Type=@type WHERE UserName=@user
GO
CREATE PROCEDURE USP_CheckUserName
@user NVARCHAR(100)
AS
SELECT * FROM dbo.Account WHERE UserName=@user
GO
CREATE PROCEDURE USP_DeleteAcc
@user NVARCHAR(100)
AS
DELETE dbo.Account WHERE UserName=@user
GO
--//Đối với trường hợp Xóa bàn, nếu xóa bàn thì sẽ phải xóa BillInfo và Bill=> ảnh hưởng
--            //đến danh thu do đó không thể xóa trực tiếp bàn đó.
--            //Theo suy nghĩ của bản thân thì: Chỉ cần update trạng thái bàn đó thành"Đã xóa"
--            //sau đó không show bàn đó nữa =))
CREATE PROCEDURE USP_DeleteTable
@id INT
AS
UPDATE dbo.TableFood SET status=N'Đã xóa' WHERE id=@id
GO
CREATE PROC USP_GetListBillByEnglish--ReportViewer không nhận Tiếng Việt nên phải dùng Tiếng Anh
@fromDate DATE,@toDate DATE
AS
BEGIN
SELECT a.name,b.DateCheckIn,b.DateCheckOut,b.discount,SUM(c.price*d.count) [TotalPrice],SUM(c.price*d.count*(100-b.discount)/100) [FinalPrice]
FROM dbo.TableFood a, dbo.Bill b,dbo.Food c,dbo.BillInfo d
WHERE b.idTable=a.id AND b.id=d.idBill AND c.id=d.idFood AND b.status=1 AND b.DateCheckIn>=@fromDate AND b.DateCheckOut<=@toDate
GROUP BY a.name,b.DateCheckIn,b.DateCheckOut,b.discount
END
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'root', N'Nguyễn Duy Cương', N'3724923114850596956133245243127987711', 2)
GO 
SELECT * FROM dbo.Account


















