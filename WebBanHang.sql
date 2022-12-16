-- Tạo cơ sở dữ liệu 
create database Web
go
use Web
go

create table Menus(
	Id int IDENTITY(1,1) not null primary key,
	Title nvarchar(100) not null,
	Alias nvarchar(100) not null,
    Position int,
	CreatedBy nvarchar(100),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100),
)

create table Roles(
	Id int identity(1,1) not null primary key,
	Name nvarchar(50) not null,
)
create table Users(
	Id int IDENTITY(1,1) NOT NULL primary key, 
	RoleId int NOT NULL,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Email nvarchar(100) NOT NULL,
	Password nvarchar(100) NOT NULL,
	ConfirmPassword nvarchar(100) NOT NULL,
	ResetPasswordCode nvarchar(100) NULL,
	CreatedBy nvarchar(100),
	CreatedDate datetime NOT NULL,
	ModifiedDate datetime NOT NULL,
	ModifiedBy nvarchar(100),
	UserImage nvarchar(500),
	Phone varchar(15),
)

create table Comments(
	Id int IDENTITY(1,1) not null primary key,
	ProductId int not null,
	UserId int not null,
	Rate int not null,
	Message nvarchar(2048) not null,
	CreatedDate datetime not null,

)

create table Contact(
	Id int IDENTITY(1,1) not null primary key,
	Name nvarchar(150) not null,
	Email nvarchar(200) null,
    Message nvarchar(2000),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100)
)

create table News(
	Id int IDENTITY(1,1) not null primary key,
	Title nvarchar(200) not null,
	Alias nvarchar(200),
	Description nvarchar(500),
    Detail nvarchar(max),
	Image nvarchar(500),
	IsActive bit NOT NULL,
	Author nvarchar(100),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100),
	CreatedBy nvarchar(100),
)

create table ProductCategory(
	Id int IDENTITY(1,1) not null primary key,
	Title nvarchar(100) not null,
	Alias nvarchar(100) not null,
    Icon nvarchar(250),
	IsActive bit not null,
	CreatedBy nvarchar(100),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100)
)

create table Products(
	Id int IDENTITY(1,1) not null primary key,
	Title nvarchar(100) not null,
	Alias nvarchar(100),
    ProductCode nvarchar(10),
	Description nvarchar(500),
    Detail nvarchar(max),
	Image nvarchar(500),
	Price int not null,
	PriceSale int,
	Quantity int not null,
	ViewCount int default 0 not null,
	IsHome bit not null,
	IsSale bit not null,
	IsFeature bit not null,
	IsHot bit not null,
	IsActive bit not null,
	CreatedBy nvarchar(100),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100),
	ProductCategoryId int not null,
)

create table ProductImage(
	Id int IDENTITY(1,1) not null primary key,
	Image nvarchar(200) not null,
	IsDefault bit not null,
	ProductId int not null,
)

create table Orders(
	Id int IDENTITY(1,1) not null primary key,
	UserId int,
	Code nvarchar(20) not null,
	CustomerName nvarchar(150) not null,
	Phone varchar(14) not null,
    Email nvarchar(200),
	TotalAmount int not null,
    TypePayment int not null,
	CreatedBy nvarchar(100),
    CreatedDate datetime not null,
	ModifiedDate datetime not null,
	ModifiedBy nvarchar(100),
	Street nvarchar (50) not null,
	Ward nvarchar (50) not null,
	District nvarchar (50) not null,
	City nvarchar (50) not null,
)

create table OrderDetail(
	Id int IDENTITY(1,1) not null primary key,
	OrderId int not null,
	ProductId int not null,
	Price int not null,
	Quantity int not null,
)

create table Subscribe(
	Id int IDENTITY(1,1) not null primary key,
	Email nvarchar(200) not null,
	CreatedDate datetime not null
)

create table SystemSetting (
	SettingKey nvarchar(50) not null primary key,
	SettingValue nvarchar(4000) not null,
	SettingDescription nvarchar(4000),
)

create table Statistic(
	 Id int IDENTITY(1,1) not null primary key,
	 NumberOfAccess bigint not null,
	 Time datetime not null
)

GO

ALTER TABLE Users
ADD FOREIGN KEY (RoleId) REFERENCES Roles(Id);
ALTER TABLE Comments
ADD FOREIGN KEY (ProductId) REFERENCES Products(Id)
ALTER TABLE Comments
ADD FOREIGN KEY (UserId) REFERENCES Users(Id)
ALTER TABLE Products
ADD FOREIGN KEY (ProductCategoryId) REFERENCES ProductCategory(Id);
ALTER TABLE ProductImage
ADD FOREIGN KEY (ProductId) REFERENCES Products(Id);
ALTER TABLE Orders
ADD FOREIGN KEY (UserId) REFERENCES Users(Id)
ALTER TABLE OrderDetail
ADD FOREIGN KEY (ProductId) REFERENCES Products(Id);
ALTER TABLE OrderDetail
ADD FOREIGN KEY (OrderId) REFERENCES Orders(Id);

-- Insert Data

-- Lê Tường Vy (01/12/2022) -> Thêm dữ liệu cho loại sản phẩm
INSERT ProductCategory (Id, Title, Alias, Icon, IsActive, CreatedBy, CreatedDate, ModifiedDate) 
VALUES (1, N'Trái Cây Việt Nam', N'trai-cay-viet-nam', N'/Uploads/images/Fruit/2.jpg', 0, N'Phạm Phương', '2022-12-01', '2022-12-01')
INSERT ProductCategory (Id, Title, Alias, Icon, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy) 
VALUES (2, N'Trái Cây Nhập Khẩu', N'trai-cay-nhap-khau', N'/Uploads/images/Fruit/1.jpg', 0, N'Phạm Phương', '2022-12-01', '2022-12-01')

-- Lê Tường Vy (01/12/2022) -> Thêm dữ liệu chi tiết đơn hàng
INSERT OrderDetail (Id, OrderId, ProductId, Price, Quantity) VALUES (1, 1, 1, 1300000, 1)
INSERT OrderDetail (Id, OrderId, ProductId, Price, Quantity) VALUES (2, 1, 1, 1300000, 1)

-- Phạm Đức Phương (01/12/2022) -> Thêm dữ liệu cho đơn hàng
INSERT Orders (Id, UserId, Code, CustomerName, Phone, Email, TotalAmount, TypePayment, 
			CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, Street, Ward, District, City) 
			VALUES (1, NULL, N'DH1517', N'Phạm Đức Phương', N'0902183044', N'vietanh2322001@gmail.com', 1300000, 3, N'Phạm Đức Phương',
			'2022-12-10', '2022-12-10', N'Phạm Phương', N'137/63 Bình Long', N'Phường 12', N'Quận Tân Bình', N'Thành phố Hồ Chí Minh')

INSERT Orders (Id, UserId, Code, CustomerName, Phone, Email, TotalAmount, TypePayment,
			CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, Street, Ward, District, City) 
			VALUES (2, 1, N'DH2316', N'Trần Đức Minh Khôi', N'0123456789', N'quandoinat@gmail.com', 820000, 2, N'Trần Đức Minh Khôi', 
			'2021-08-10', '2021-08-10', N'Phạm Phương', N'25 Nguyễn Thiện Thuật', N'Phường 08', N'Quận 10', N'Thành phố Hồ Chí Minh')

-- Trần Đức Minh Khôi (01/12/2022) -> Thêm dữ liệu cho bảng thống kê
INSERT Statistic (Id, NumberOfAccess, Time) VALUES (1, 9, '2022-11-05')

-- Truy Vấn

-- 10 Câu truy vấn từ đơn giản tới phức tạp lấy dữ liệu từ nhiều bảng

-- 1. Phạm Đức Phương (02/12/2022) -> Khách hàng vãng lai mua với số lượng sản phẩm nhiều nhất trong tháng hiện tại
select top 1 with ties CustomerName [Tên Khách Hàng], Sum(Quantity) [Số lượng] from OrderDetail full join Orders o
on OrderId = o.Id
where  TypePayment = 2 and MONTH(GETDATE()) = MONTH(CreatedDate)
group by CustomerName order by Sum(Quantity) desc

-- 2. Phạm Đức Phương (02/12/2022) -> Khách hàng chưa đăng ký tài khoản mua với số lượng nhiều nhất trong năm hiện tại
select top 1 CustomerName [Tên Khách Hàng], Sum(Quantity)[Số lượng] from OrderDetail full join Orders o
on OrderId = o.Id
where  TypePayment = 2 and YEAR(GETDATE()) = YEAR(CreatedDate)
group by CustomerName order by Sum(Quantity) desc


-- 3. Phạm Đức Phương (02/12/2022) -> Sản phẩm bán nhiều nhất
select top 1 Title [Tên Sản Phẩm], sum(od.Quantity) [Số Lượng] from OrderDetail od
inner join Orders o on od.OrderId = o.Id 
inner join Products p on p.Id = od.ProductId
where TypePayment = 2  
group by Title order by sum(od.Quantity) desc


--	4. Trần Đức Minh Khôi (02/12/2022) -> Sản phẩm bán ít nhất
select top 1 Title [Tên Sản Phẩm], sum(od.Quantity) [Số Lượng] from OrderDetail od 
inner join Orders o on od.OrderId = o.Id 
inner join Products p on p.Id = od.ProductId
where TypePayment = 2  
group by Title order by sum(od.Quantity)

--	5. Lê Tường Vy (02/12/2022) -> Sản phẩm chưa bán được
select Title [Tên sản phẩm] from Products where Id not in (select distinct ProductId from OrderDetail)

--  6. Phạm Đức Phương (05/12/2022) -> Lấy số lượng  mặt hàng của từng danh mục
select pc.Title [Tên loại sản phẩm], count(*) [Mặt hàng] from ProductCategory pc 
left join Products p on p.ProductCategoryId = pc.Id
where Quantity is not null
group by pc.Title
union all
select pc.Title [Tên loại sản phẩm],0 [Mặt hàng] from ProductCategory pc 
left join Products p on p.ProductCategoryId = pc.Id
where Quantity is  null
group by pc.Title

--	7. Phạm Đức Phương (05/12/2022) -> Danh mục nào có nhiều mặt hàng nhất
select top 1 with ties pc.Title [Tên loại sản phẩm], count(*) [Mặt hàng] from ProductCategory pc 
left join Products p on p.ProductCategoryId = pc.Id
where Quantity is not null
group by pc.Title order by count(*)

--	8. Phạm Đức Phương (05/12/2022) -> Xem chi tiết đơn hàng của sản phẩm bán chạy nhất 
select * from OrderDetail where ProductId in (
select top 1 with ties ProductId from OrderDetail od 
inner join Orders o on o.Id = od.OrderId
where TypePayment = 2
group by ProductId
order by sum(Quantity) desc)


--	9. Lê Tường Vy (03/12/2022) Xem danh danh sách họ tên của người dùng
select FirstName + ' ' + LastName [FullName] from Users

-- 10. Trần Đức Minh Khôi (05/12/2022) Khách hàng nào đã đăng ký tài khoản sở hữu đơn hàng có giá trị cao nhất
select top 1 u.FirstName + ' ' + u.LastName [Tên Khách Hàng], TotalAmount [Giá trị đơn hàng] from Orders o 
inner join Users u on u.Id = o.UserId
where TypePayment = 2
group by o.UserId, u.FirstName, u.LastName, o.Id, TotalAmount order by TotalAmount desc



-- Tạo View


-- 1. Phạm Đức Phương (01/12/2022) -> Xem danh sách sản phẩm
create view  DanhSachSanPham AS  select * from Products  

-- 2.Lê Tường Vy (01/12/2022) -> Xem danh sách liên hệ
create view DanhSachLienHe as select * from Contact

-- 3. Lê Tường Vy (01/12/2022) -> Xem danh sách loại sản phẩm
create view DanhSachLoaiSanPham as select * from ProductCategory

-- 4. Lê Tường Vy (01/12/2022) -> Xem danh sách tài khoản
create view DanhSachTaiKhoan as select * from Users

-- 5. Lê Tường Vy (01/12/2022) -> Xem danh sách hóa đơn
create view DanhSachHoaDon as select * from Orders

-- 6. Lê Tường Vy (01/12/2022) -> Xem danh sách tin tức
create view DanhSachTinTuc as select * from News

-- 7. Lê Tường Vy (01/12/2022) -> Xem danh sách quyền
create view DanhSachQuyen as select * from Roles

-- 8. Lê Tường Vy (01/12/2022) -> Xem danh sách bình luận của khách hàng
create view DanhSachBinhLuan as select * from Comments

-- 9. Trần Đức Minh Khôi (02/12/2022) Xem danh sách sản phẩm giá tăng dần
create view DanhSachSanPhamGiaTangDan as 
select Title [Tên Sản Phẩm], Price from Products
group by Title, Price order by Price asc

-- 10. Phạm Đức Phương (02/12/2022) Xem số lượng đơn đặt hàng của các tỉnh thành
create view XemTinhThanhDuocMua as 
select City, Count(City) [Số lượng đơn đặt] from Orders
group by City order by Count(City) desc




-- Tạo Stored Procedures 

-- Stored Procedures --

-- 1. Lê Tường Vy (04/12/2022) -> Xem danh sách loại sản phẩm
create proc SP_GETDATA_PRODUCTCATEGORY (@ProductCategory_Count int output)
as
begin
	select Title, CreatedDate from ProductCategory
	 SELECT @ProductCategory_Count = @@ROWCOUNT;
	RETURN;
end

go

declare @count int;
exec SP_GETDATA_PRODUCTCATEGORY @ProductCategory_Count = @count OUTPUT;
SELECT @count AS 'Tổng số loại sản phẩm';

-- 2. Lê Tường Vy (04/12/2022) -> Thêm loại sản phẩm
create proc SP_INSERT_PRODUCTCATEGORY(@Title nvarchar(100), @Alias nvarchar(100), @CreatedDate datetime, @ModifiedDate datetime)
as 
begin
	insert into ProductCategory(Title, Alias, CreatedDate, ModifiedDate)
	values (@Title, @Alias, @CreatedDate, @ModifiedDate)
	RETURN;
end

go

exec SP_INSERT_PRODUCTCATEGORY N'Trái Cây Châu Á', N'trai-cay-chau-a', '2022/10/10', '2022/10/10'

-- 3. Lê Tường Vy (04/12/2022) -> Cập nhật loại sản phẩm
create proc SP_UPDATE_PRODUCTCATEGORY(@Id int, @Title nvarchar(100), @Alias nvarchar(100), @CreatedDate datetime, @ModifiedDate datetime)
as 
begin
	update ProductCategory
	SET Title = @Title, Alias = @Alias, CreatedDate = @CreatedDate, ModifiedDate = @ModifiedDate
	where Id = @Id
	RETURN;
end

go

exec SP_UPDATE_PRODUCTCATEGORY 4, N'Trái Cây Châu Âu', N'trai-cay-chau-au', '2022/10/10', '2022/10/10'

-- 4. Lê Tường Vy (04/12/2022) -> Xóa loại sản phẩm
create proc SP_DELETE_PRODUCTCATEGORY(@Id int)
as 
begin
	delete from ProductCategory
	where Id = @Id	
	RETURN;
end

go

exec SP_DELETE_PRODUCTCATEGORY 4

-- 5. Phạm Đức Phương (04/12/2022) -> Xem danh sách tài khoản
create proc SP_GETDATA_USERS (@USER_COUNT int output)
as
begin
	select FirstName + ' ' + LastName, Email, CreatedDate from Users
	 SELECT @USER_COUNT = @@ROWCOUNT;
	RETURN;
end

go 

declare @count int;
exec SP_GETDATA_USERS @USER_COUNT = @count OUTPUT;
SELECT @count AS 'Tổng số lượng tài khoản';

-- 6. Phạm Đức Phương (04/12/2022) -> Thêm tài khoản ( Thiếu modified date)
create proc SP_INSERT_USERS(@RoleId int, @Email nvarchar(100), @FirstName nvarchar(50), @LastName nvarchar(50), @CreatedDate datetime, @ModifiedDate datetime, @Password nvarchar(100), @ConfirmPassword nvarchar(100))
as 
begin
	insert into Users(RoleId, Email, FirstName, LastName, CreatedDate, ModifiedDate, Password, ConfirmPassword)
	values (@RoleId, @Email, @FirstName, @LastName, @CreatedDate, @ModifiedDate, @Password, @ConfirmPassword)
	RETURN;
end

go

exec SP_INSERT_USERS 1,'vietanh23220011@gmail.com', N'Khứ', N'Khứ', '2022/10/1','2022/10/1','123456','123456'

-- 7. Phạm Đức Phương (04/12/2022) -> Cập nhật mật khẩu
create proc SP_UPDATE_Password(@Id int, @Password nvarchar(100), @ConfirmPassWord nvarchar(100))
as 
begin
	update Users
	SET Password = @Password, ConfirmPassWord = @ConfirmPassWord
	where Id = @Id
	RETURN;
end

go

exec SP_UPDATE_Password 1, '123456', '123456'

-- 8. Trần Đức Minh Khôi (04/12/2022) -> Xóa tài khoản
create proc SP_DELETE_USERS(@Id int)
as 
begin
	delete from Users
	where Id = @Id	
	RETURN;
end

-- 9. Phạm Đức Phương (04/12/2022) -> Xem danh sách sản phẩm thuộc loại sản phẩm nào
create proc SP_GETDATA_PRODUCTS (@PRODUCT_COUNT int output)
as
begin
	select p.Title [Product's Name], pc.Title [Product Category], Price, PriceSale, Quantity, p.CreatedDate from Products p
	inner join ProductCategory pc on pc.Id = p.ProductCategoryId
	 SELECT @PRODUCT_COUNT = @@ROWCOUNT;
	RETURN;
end

declare @count int;
exec SP_GETDATA_PRODUCTS @PRODUCT_COUNT = @count OUTPUT;
SELECT @count AS 'Tổng sản phẩm';

-- 10. Phạm Đức Phương (04/12/2022) -> Xem 5 sản phẩm có giá niêm yết giảm giần
create proc SP_GETDATA_PRODUCT_Price_Decreasing AS
	set ROWCOUNT 5
		select Title, Quantity, Price from Products
order by Products.Price DESC

exec SP_GETDATA_PRODUCT_Price_Decreasing

-- 11. Trần Đức Minh Khôi (04/12/2022) -> Xem 10 sản phẩm có số lượng giảm dần
create proc SP_GETDATA_PRODUCT_Quantity_Decreasing AS
	set ROWCOUNT 10
		select Title, Quantity, Price from Products
order by Quantity DESC

exec SP_GETDATA_PRODUCT_Quantity_Decreasing

-- 12. Trần Đức Minh Khôi (04/12/2022) -> Thêm sản phẩm
create proc SP_INSERT_PRODUCT(@ProductCategoryId int, @Title nvarchar(100), @ProductCode nvarchar(10),
								@Description nvarchar(500), @Detail nvarchar(MAX), @Price int, 
								@PriceSale int, @Quantity int, @ViewCount int, @CreatedDate datetime, @ModifiedDate datetime,
								@IsHome bit, @IsSale bit, @IsFeature bit, @IsHot bit, @IsActive bit)
as 
begin
	insert into Products(ProductCategoryId, Title, ProductCode, Description, Detail, Price, PriceSale, Quantity,ViewCount, CreatedDate, ModifiedDate, IsHome, IsSale, IsFeature, IsHot, IsActive)
	values (@ProductCategoryId, @Title, @ProductCode, @Description, @Detail, @Price, @PriceSale, @Quantity, 0 , @CreatedDate, @ModifiedDate,@IsHome , @IsSale , @IsFeature , @IsHot , @IsActive )

	RETURN;
end

exec SP_INSERT_PRODUCT 2, N'Mít','F20010', 'MÍT NGON', 'MÍT RẤT NGON', 100000, 99999, 3,0, '2022/10/10', '2022/10/10', 1, 1,1,1,0

-- 13. Trần Đức Minh Khôi (04/12/2022) -> Cập nhật sản phẩm
create proc SP_UPDATE_Product(@Id int, @ProductCategoryId int, @Title nvarchar(100), @Quantity int, @Price int, @PriceSale int, @CreatedBy nvarchar(100), @ModifiedBy nvarchar(100))
as 
begin
	update Products
	SET ProductCategoryId = @ProductCategoryId, Title = @Title, Quantity = @Quantity,Price = @Price, PriceSale = @PriceSale,  CreatedBy = @CreatedBy, ModifiedBy = @ModifiedBy
	where Id = @Id
	RETURN;
end

-- 14. Phạm Đức Phương (04/12/2022) -> Thống kê lượt truy cập
create proc sp_ThongKe
as
BEGIN
	DECLARE @SoTruyCapGanNhat int
	DECLARE @Count int
	SELECT @Count=COUNT(*) FROM Statistic
	IF @Count IS NULL SET @Count=0
	IF @Count=0
		INSERT INTO Statistic(Time, NumberOfAccess)
		Values(GETDATE(),1)
	ELSE
		BEGIN
			DECLARE @ThoiGianGanNhat datetime
			SELECT @SoTruyCapGanNhat=tk.NumberOfAccess,@ThoiGianGanNhat=tk.Time FROM Statistic tk
			WHERE tk.id=(SELECT MAX(tk1.Id) FROM Statistic tk1)
			IF @SoTruyCapGanNhat IS NULL SET @SoTruyCapGanNhat=0
			IF @ThoiGianGanNhat IS NULL SET @ThoiGianGanNhat=GETDATE()
			--Nếu chuyển sang ngày  mới chưa (sau 12h đêm)
			-- nếu chưa chuyển sang ngày mới thì update
			IF DAY(@ThoiGianGanNhat)=DAY(GETDATE())
				BEGIN
					UPDATE Statistic
					SET 
					NumberOfAccess=@SoTruyCapGanNhat+1,
					Time=GETDATE()
					WHERE Id=(SELECT MAX(tk1.Id) FROM Statistic tk1)
				END
			-- Nếu đã sang ngày mới rồi thì chúng thêm 1 bản ghi mới
			ELSE
				INSERT INTO Statistic(Time,NumberOfAccess)
				Values(GETDATE(),1)
		END

			-- Thống kê Hom nay, Hom qua, Tuan nay, Tuan Truoc, Thang nay, Thang Truoc
		DECLARE @HomNay int
		SET @HomNay=DATEPART(DW,GETDATE());
		SELECT @SoTruyCapGanNhat=NumberOfAccess,@ThoiGianGanNhat=Time FROM Statistic 
		WHERE Id=(SELECT MAX(Id) FROM Statistic)

		--so truy cap ngay hqua
		DECLARE @TruyCapHomQua bigint
		SELECT @TruyCapHomQua=ISNULL(NumberOfAccess,0) FROM Statistic 
		Where CONVERT(nvarchar(20),Time,103)=CONVERT(nvarchar(20),GETDATE()-1,103)
		IF @TruyCapHomQua IS NULL SET @TruyCapHomQua=0

		--truy cap dau tuan này
		DECLARE @DauTuanNay datetime
		SET @DauTuanNay= DATEADD(wk, DATEDIFF(wk, 6, GetDate()), 6)
		-- Tính Ngày đầu của tuần trước Tính từ thời điểm 00:00:))
		DECLARE @NgayDauTuanTruoc datetime
		SET @NgayDauTuanTruoc = Cast(CONVERT(nvarchar(30),DATEADD(dd, -(@HomNay+6), GetDate()),101) AS datetime)
		-- Tính ngày cuối tuần trước tính đến 24h ngày cuối tuần 
		DECLARE @NgayCuoiTuanTruoc datetime
		SET @NgayCuoiTuanTruoc = Cast(CONVERT(nvarchar(30),DATEADD(dd, -@HomNay, GetDate()),101) +' 23:59:59' AS datetime)

		-- so truy cap tuan nay
		DECLARE @TruyCapTuanNay bigint
		SELECT @TruyCapTuanNay=ISNULL(sum(NumberOfAccess),0) FROM Statistic
		Where Time BETWEEN @DauTuanNay AND GETDATE()

		-- Tính số truy cập tuần trước
		DECLARE @SoLanTruyCapTuanTruoc bigint
		SELECT @SoLanTruyCapTuanTruoc=isnull(sum(NumberOfAccess),0)  FROM Statistic ttk 
			WHERE ttk.Time BETWEEN @NgayDauTuanTruoc AND @NgayCuoiTuanTruoc
		
		-- Tính số truy cập tháng này
		DECLARE @SoTruyCapThangNay bigint 
		SELECT @SoTruyCapThangNay=isnull(Sum(NumberOfAccess),0)
			FROM Statistic ttk WHERE MONTH(ttk.Time)=MONTH(GETDATE())
		
		-- Tính số truy cập tháng trước
		DECLARE @SoTruyCapThangTruoc bigint 
		SELECT @SoTruyCapThangTruoc=isnull(Sum(NumberOfAccess),0)
			FROM Statistic ttk WHERE MONTH(ttk.Time)=MONTH(GETDATE())-1
		
		-- Tính tổng số
		DECLARE @TongSo bigint
		SELECT  @TongSo=isnull(Sum(NumberOfAccess),0) FROM Statistic ttk
		
		SELECT @SoTruyCapGanNhat AS HomNay, 
		@TruyCapHomQua AS HomQua,
		@TruyCapTuanNay AS TuanNay, 
		@SoLanTruyCapTuanTruoc AS TuanTruoc, 
		@SoTruyCapThangNay AS ThangNay, 
		@SoTruyCapThangTruoc AS ThangTruoc,
		@TongSo AS TatCa
END
-- 15. Phạm Đức Phương (04/12/2022) -> Top sản phẩm bán chạy
create proc sp_top_selling_products
AS
BEGIN

select top 4 p.Title [product], sum(v.Quantity) [quantity] from OrderDetail v
inner join Products p on p.Id = v.ProductId
inner join Orders o on o.Id = v.OrderId
where TypePayment = 2
group by p.Title
order by sum(v.Quantity) desc
end

-- 16. Trần Đức Minh Khôi (04/12/2022) -> Doanh thu theo năm
create proc sp_GetRevenueByYear
as
BEGIN
SELECT year(CreatedDate) [Nam], SUM(TotalAmount) [Total] FROM Orders 
where TypePayment = 2
group by year(CreatedDate)

-- 17. Lê Tường Vy (04/12/2022) -> Doanh thu theo tháng của các năm gộp lại
create proc sp_GetMonthlyRevenue
as
BEGIN
SELECT month(CreatedDate) [Thang], SUM(TotalAmount) [Total] FROM Orders 
where TypePayment = 2
group by month(CreatedDate)
end

-- 18. Phạm Đức Phương (04/12/2022) -> Doanh thu theo quý năm 2022
create proc sp_GetRevenueByQuarter
as
begin
SELECT datepart(quarter, CreatedDate) [Quy], SUM(TotalAmount) [Total] FROM Orders 
where TypePayment = 2 and year(CreatedDate) = 2022
group by datepart(quarter, CreatedDate)
end



-- Tạo Function

-- Function --

-- 1. Lê Tường Vy (07/12/2022) -> Hàm đọc danh sách sản phẩm theo mã loại sản phẩm truyền vào
create function Danh_sach_san_pham(@categoryid int)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Products
    WHERE ProductCategoryId = @categoryid 
)

SELECT * FROM Danh_sach_san_pham(1)

-- 2. Lê Tường Vy (07/12/2022) -> Hàm đọc danh sách đơn hàng theo tỉnh, thành phố truyền vào
create function Danh_sach_don_hang_theo_tinh_thanh_pho(@city nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Orders
    WHERE City = @city
)

SELECT * FROM Danh_sach_don_hang_theo_tinh_thanh_pho(N'Tỉnh Bắc Giang')

-- 3. Phạm Đức Phương (07/12/2022) -> Hàm cho biết số lượng khách hàng theo tỉnh thành bất kỳ nhận vào từ tham số với điều kiện là khách hàng có tổng số tiền mua hàng từ trước đến nay từ 1 triệu trở lên
create function So_luong_khach_hang_theo_tinh_thanh_co_hoa_don_tren_muoi_trieu (@city nvarchar(50))
RETURNS int
AS 
BEGIN
	DECLARE @count int = 0
	SELECT @count = count(*) FROM (
        SELECT Id
        FROM Orders
        WHERE City = @city and TypePayment = 2
        GROUp BY Id 
        HAVING SUM(TotalAmount) >= 123311
    ) AS Temp
    RETURN @count
END

PRINT dbo.so_luong_khach_hang_theo_tinh_thanh_co_hoa_don_tren_muoi_trieu(N'Tỉnh Quảng Ninh')

-- 4. Phạm Đức Phương (07/12/2022) -> Hàm tính % sau khi giảm giá sản phẩm -- chưa được
create function Tinh_phan_tram_san_pham(@Price int, @PriceSale int)
RETURNS float
AS
begin
	RETURN abs(100.00 - cast((1.00 * @PriceSale / @Price * 1.00) * 100 as float))
end

select Title [Sản phẩm], Price [Giá], PriceSale [Giá khuyến mãi], dbo.Tinh_phan_tram_san_pham(Price, PriceSale) [Phần trăm]  from Products

-- 5. Trần Đức Minh Khôi (07/12/2022) -> Hàm đếm xem có bao nhiêu sản phẩm với loại sản phẩm là tham số truyền vào
create FUNCTION Dem_san_pham(
	@ProductCategoryId int
)
RETURNS int
AS
BEGIN
	RETURN (
		SELECT COUNT(*) FROM Products
		WHERE ProductCategoryId = @ProductCategoryId
	)

END

select dbo.dem_san_pham(1) [Quantity], ProductCategoryId from Products
group by ProductCategoryId

-- 6. Trần Đức Minh Khôi (07/12/2022) -> Tính số ngày từ lúc tạo đến ngày sửa đổi
create function khoang_cach_ngay(
	@CreatedDate datetime,
	@ModifiedDate datetime
)
RETURNS int
AS

BEGIN
	RETURN DateDiff(day,@CreatedDate,@ModifiedDate)

END

select dbo.khoang_cach_ngay(CreatedDate, ModifiedDate) from Products

-- Trigger

-- 1. Lê Tường Vy (08/12/2022) -> Kiểm tra tên sản phẩm là duy nhất, giá, giá khuyến mãi, số lượng không được nhỏ hơn không 
-- và ngày sửa đổi không được bé hơn ngày tạo, giá khuyến mãi phải nhỏ hơn giá 
create trigger trg_kiem_tra_san_pham
on Products
instead of INSERT, UPDATE
as 
begin
	if exists ( select * from inserted where Title in (select Products.Title from Products))
		begin
			raiserror ('Ten san pham phai la duy nhat', 16, 1)
			rollback tran
			return
		end
		if exists ( select * from inserted where Price < 0)
	begin
			raiserror ('Gia san pham khong duoc be hon 0', 16, 1)
			rollback tran
			return
		end
			if exists ( select * from inserted where PriceSale < 0)
	begin
			raiserror ('Gia khuyen mai khong duoc be hon 0', 16, 1)
			rollback tran
			return
		end
		if exists ( select * from inserted where Quantity < 0)
	begin
			raiserror ('So luong san pham khong duoc be hon 0', 16, 1)
			rollback tran
			return
		end
		if exists ( select * from inserted where CreatedDate > ModifiedDate)
	begin
			raiserror ('Ngay sua doi khong duoc be hon ngay tao', 16, 1)
			rollback tran
			return
		end
		if exists ( select * from inserted where PriceSale > Price)
	begin
			raiserror ('Gia khuyen mai phai nho hon gia san pham', 16, 1)
			rollback tran
			return
		end
end
SET IDENTITY_INSERT Products ON 

-- Kiểm tra giá sản phẩm Không được bé hơn không
INSERT Products (Id, Title, Alias, ProductCode, Description, Detail, Image, Price, PriceSale, Quantity, ViewCount, 
				IsHome, IsSale, IsFeature, IsHot, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, ProductCategoryId) 
VALUES (1, N'Nho mẫu đơn', N'Nho mẫu đơn', N'F1001', N'Quả to, tròn, vỏ màu xanh sữa', N'Quả to, tròn, vỏ màu xanh sữa, vị rất ngọt, không chát, không hạt và có hương thơm đặc trưng'
, NULL, -10, 1300000, 10, 43, 1, 1, 1, 1, 1, NULL, CAST(N'2022-11-03T00:26:53.143' AS DateTime), CAST(N'2022-10-08T00:26:53.143' AS DateTime), NULL, 1)

-- Kiểm tra tên sản phẩm không được trùng nhau
INSERT Products (Id, Title, Alias, ProductCode, Description, Detail, Image, Price, PriceSale, Quantity, ViewCount, 
				IsHome, IsSale, IsFeature, IsHot, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, ProductCategoryId) 
VALUES (1, N'Nho mẫu đơn', N'Nho mẫu đơn', N'F1001', N'Quả to, tròn, vỏ màu xanh sữa', N'Quả to, tròn, vỏ màu xanh sữa, vị rất ngọt, không chát, không hạt và có hương thơm đặc trưng'
, NULL, 1500000, 1300000, 10, 43, 1, 1, 1, 1, 1, NULL, CAST(N'2022-11-03T00:26:53.143' AS DateTime), CAST(N'2022-10-08T00:26:53.143' AS DateTime), NULL, 1)

-- Kiểm tra giá sản khuyến mãi nhỏ hơn giá sản phẩm
INSERT Products (Id, Title, Alias, ProductCode, Description, Detail, Image, Price, PriceSale, Quantity, ViewCount, 
				IsHome, IsSale, IsFeature, IsHot, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, ProductCategoryId) 
VALUES (1, N'Nho mẫu đơn 2', N'Nho mẫu đơn', N'F1001', N'Quả to, tròn, vỏ màu xanh sữa', N'Quả to, tròn, vỏ màu xanh sữa, vị rất ngọt, không chát, không hạt và có hương thơm đặc trưng'
, NULL, 10, 1300000, 10, 43, 1, 1, 1, 1, 1, NULL, CAST(N'2022-11-03T00:26:53.143' AS DateTime), CAST(N'2022-10-08T00:26:53.143' AS DateTime), NULL, 1)


-- 2. Phạm Đức Phương (08/12/2022) -> Tên loại sản phẩm phải là duy nhất
create trigger trg_kiem_tra_ten_loai_san_pham
on ProductCategory
instead of INSERT, UPDATE
as 
begin
	if exists ( select * from inserted where Title in (select ProductCategory.Title from ProductCategory))
		begin
			raiserror ('Ten loai san pham phai la duy nhat', 16, 1)
			rollback tran
			return
		end
end

SET IDENTITY_INSERT ProductCategory ON

insert ProductCategory (Id, Title, Alias, Icon, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy) 
values (1, N'Trái Cây Nhập Khẩu', N'trai-cay-nhap-khau', N'/Uploads/images/Fruit/1.jpg', NULL, NULL, CAST(N'2022-12-09' AS DateTime), CAST(N'2022-12-09' AS DateTime), NULL)

insert ProductCategory (Id, Title, Alias, Icon, IsActive, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy) 
values (1, N'Trái Cây Nhập Khẩu', N'trai-cay-nhap-khau', N'/Uploads/images/Fruit/1.jpg', NULL, NULL, CAST(N'2022-12-09' AS DateTime), CAST(N'2022-12-09' AS DateTime), NULL)

-- 3. Lê Tường Vy (08/12/2022) -> Menu phải là duy nhất
create trigger trg_kiem_tra_menu
on Menus
instead of INSERT, UPDATE
as 
begin
	if exists ( select * from inserted where Title in (select Menus.Title from Menus))
		begin
			raiserror ('Ten menu phai la duy nhat', 16, 1)
			rollback tran
			return
		end
end

UPDATE Menus
SET Title = N'Trang chủ'
WHERE Id = 1;

-- 4. Phạm (08/12/2022) -> Tiêu đề tin tức phải là duy nhất
create trigger trg_kiem_tra_tin_tuc
on News
instead of INSERT, UPDATE
as 
begin
	if exists ( select * from inserted where Title in (select News.Title from News))
		begin
			raiserror ('Tieu de tin tuc phai la duy nhat', 16, 1)
			rollback tran
			return
		end
end

UPDATE News
SET Title = N'6 loại quả giàu canxi không thua kém gì sữa mẹ'
WHERE Id = 1;


-- 5. Phạm Đức Phương (08/12/2022) -> Tổng đơn hàng không được bé hơn 0 và mã đơn hàng là duy nhất
create trigger trg_kiem_tra_don_hang
on Orders
instead of INSERT, UPDATE
as 
begin
	if exists ( select * from inserted where TotalAmount < 0)
	begin
			raiserror ('Tong don hang khong duoc be hon khong', 16, 1)
			rollback tran
			return
		end
		if exists ( select * from inserted where Code in (select Orders.Code from Orders))
	begin
			raiserror ('Ma don hang khong duoc trung nhau', 16, 1)
			rollback tran
			return
		end
end

SET IDENTITY_INSERT Orders ON 

insert Orders (Id, Code, CustomerName,Phone, Email, TotalAmount,TypePayment,Street,Ward,District,City,  CreatedDate, ModifiedDate) 
values (7, N'DH0111', N'Phương','12345', N'vietanh@gmail.com', 150000, 1, 'aa','aa','aa', 'aa', CAST(N'2022-12-08' AS DateTime), CAST(N'2022-12-08' AS DateTime))

-- 6. Phạm Đức Phương (08/12/2022) -> Tên tài khoản phải là duy nhất
create trigger trg_kiem_tra_email_duy_nhat
on Users
instead of INSERT, UPDATE
as 
begin
	if exists (  select * from inserted where Email in (select Users.Email from Users))
		begin
			raiserror ('Ten tai khoan phai la duy nhat', 16, 1)
			rollback tran
			return
		end
end

UPDATE Users
SET Email = 'vietanh2322001@gmail.com'
WHERE Id = 1;

-- 7. Trần Đức Minh Khôi (08/12/2022) -> Cập nhật số lượng sản phẩm sau khi đặt hàng 
create trigger trg_dat_hang on OrderDetail after insert as
begin
	update Products
	set Products.Quantity = Products.Quantity - (
		select OrderDetail.Quantity
		from inserted
		where ProductId = Products.Id
		)
		from OrderDetail
end

-- 8. Trần Đức Minh Khôi (08/12/2022) -> Cập nhật số lượng sản phẩm sau khi hủy đặt hàng
create trigger trg_huy_dat_hang on OrderDetail for delete as 
begin
	update Products
	set Products.Quantity = Products.Quantity + (select OrderDetail.Quantity from deleted where ProductId = Products.Id)
	from OrderDetail 
end

-- 9. Phạm Đức Phương (08/12/2022) -> Không xóa user khi có đơn đặt hàng
create trigger trg_khong_duoc_xoa on Users INSTEAD OF DELETE as
begin
SET NOCOUNT ON
	if exists (select 1 from deleted where Id  in (Select UserId from Orders))
	begin
	raiserror ('Khong duoc xoa tai khoan vi da co don dat hang', 16, 1)
			rollback tran
			return
		end
end

delete Users where Users.Id = 1

-- 10. Phạm Đức Phương (08/12/2022) -> Không xóa loại sản phẩm khi có sản phẩm thuộc loại sản phẩm 
create trigger trg_Khong_Xoa_Loai_San_Pham_Khi_Co_San_Pham_Thuoc_Loai_San_Pham
on ProductCategory
INSTEAD OF DELETE
AS
    SET NOCOUNT ON
    IF EXISTS(SELECT 1 FROM deleted WHERE Id IN (SELECT ProductCategoryId FROM Products))
    BEGIN
        RAISERROR(N'Không được xóa vì tồn tại sản phẩm thuộc loại sản phẩm này', 16, 1)
        RETURN
    END

delete ProductCategory where Id = 1

-- 11. Trần Đức Minh Khôi (08/12/2022) -> Tên quyền phải là duy nhất
create trigger trg_kiem_tra_quyen_duy_nhat
on Roles
instead of INSERT, UPDATE
as 
begin
	if exists (  select * from inserted where Name in (select Roles.Name from Roles))
		begin
			raiserror ('Ten quyen phai la duy nhat', 16, 1)
			rollback tran
			return
		end
end

UPDATE Roles
SET Name = 'Admin'
WHERE Id = 1

-- 12. Trần Đức Minh Khôi (08/12/2022) -> Khi thêm nhiều sản phẩm thì kiểm tra nếu mã loại sản phẩm
-- không có trong bảng ProductCategory thì không cho thêm và thông báo lỗi
create trigger trg_kiem_loai_san_pham_khi_them_san_pham
on Products
after insert
as
    declare @ProductCategoryId int
    --Đọc dữ liệu trong bảng tạm inserted
    select @ProductCategoryId = Id from inserted
    if not exists(select 1 from ProductCategory where Id = @ProductCategoryId)
    begin
        rollback
        RAISERROR(N'Mã loại sản phẩm không hợp lệ', 16, 1)
        return
    end

-- 13. Lê Tường Vy (08/12/2022) -> Không cho phép xóa sản phẩm khi có đơn đặt hàng
create trigger trg_khong_xoa_san_pham_khi_co_don_dat
on Products
INSTEAD OF DELETE
AS
    SET NOCOUNT ON
    IF EXISTS(SELECT 1 FROM deleted WHERE Id IN (SELECT ProductId FROM OrderDetail))
    BEGIN
        RAISERROR(N'Không xóa được sản phẩm vì đã có đặt hàng!', 16, 1)
        RETURN
    END

delete from Products where Id = 1


-- Phân quyền

use WebBanHang
-- Tạo quyền
create role Admin
create role Employee

create login DucPhuong with password = '123456' must_change, check_expiration = on
create login MinhKhoi with password = '123456' must_change, check_expiration = on
create login TuongVy with password = '123456' must_change, check_expiration = on

create user MinhKhoi for login MinhKhoi
create user DucPhuong for login DucPhuong
create user TuongVy for login TuongVy

-- Cấp quyền
exec sp_addrolemember Admin, DucPhuong
exec sp_addrolemember Employee, TuongVy
exec sp_addrolemember Employee, MinhKhoi

grant select, insert, delete, update on Comments to Employee
grant select, insert on Contact to Employee
grant select, insert, delete, update on Orders to Employee
grant select, insert, delete, update on OrderDetail to Employee
grant select, insert, delete, update on ProductCategory to Employee
grant select, insert, delete, update on Products to Employee
grant select, insert, delete, update on News to Employee
grant select, insert, update on Users to Employee
grant select, insert, delete, update on Subscribe to Employee
grant select, insert, delete, update on Roles to Employee


grant All to Admin

-- Thu hồi quyền quản lý user cho role Employee 
revoke ALL ON Roles FROM Employee;