CREATE DATABASE DoAn3 
GO
USE DoAn3

CREATE TABLE NhaXuatBan (
    MaNhaXuatBan INT IDENTITY(1,1) PRIMARY KEY,
    TenNhaXuatBan NVARCHAR(200),
    DiaChi NVARCHAR(500),
    DienThoai NVARCHAR(20)
);

CREATE TABLE TacGia (
    MaTacGia INT IDENTITY(1,1) PRIMARY KEY,
    TenTacGia NVARCHAR(100),
    TieuSu NVARCHAR(MAX)
);

CREATE TABLE LoaiSach
(
MaLoai INT PRIMARY KEY IDENTITY,
TenLH NVARCHAR(50),
MoTa Nvarchar(100), 
HinhAnh Nvarchar(100)
)


CREATE TABLE Sach (
    MaLoai INT,
    MaSach INT IDENTITY(1,1) PRIMARY KEY,
    slTon INT,
    HinhAnh NVARCHAR(255),
    MaNhaXuatBan INT,
	TenSach NVARCHAR(255),
    MaTacGia INT,
    Gia DECIMAL(18, 2),
    MoTa NVARCHAR(MAX),
    CONSTRAINT FK_Sach_NhaXuatBan FOREIGN KEY (MaNhaXuatBan)
        REFERENCES NhaXuatBan(MaNhaXuatBan)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Sach_TacGia FOREIGN KEY (MaTacGia)
        REFERENCES TacGia(MaTacGia)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
ALTER TABLE Sach
ADD CONSTRAINT FK_Sach_LoaiSach FOREIGN KEY (MaLoai)
    REFERENCES LoaiSach(MaLoai)
    ON DELETE CASCADE
    ON UPDATE CASCADE;
	select * from loaisach ;

CREATE TABLE Users (
    user_id INT IDENTITY PRIMARY KEY,
    username NVARCHAR(255),
	email NVARCHAR(255),
    pass NVARCHAR(255)
);

CREATE TABLE Cart(
	MaCart INT PRIMARY KEY IDENTITY,
	User_id int,
	MaSach INT,
	SoLuong INT,
	TongTien1Loai FLOAT,
	TongTienCart FLOAT,
	CONSTRAINT FK_Cart_MaSach FOREIGN KEY (MaSach)
        REFERENCES Sach(MaSach)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_Cart_UserId FOREIGN KEY (User_id)
        REFERENCES Users(User_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
USE DoAn3 
select * from Cart;
SELECT 
-- Drop the existing foreign key constraint
ALTER TABLE Cart
DROP CONSTRAINT FK_Cart_UserId;

-- Drop the column User_id
ALTER TABLE Cart
DROP COLUMN user_id;

-- Add the column user_id with the new definition
ALTER TABLE Cart
ADD User_id INT;
-- Drop the existing foreign key constraint
ALTER TABLE Cart
DROP CONSTRAINT FK_Cart_UserId;

-- Recreate the foreign key constraint with the corrected REFERENCES clause
ALTER TABLE Cart
ADD CONSTRAINT FK_Cart_UserId FOREIGN KEY (User_id)
REFERENCES Users(User_id)
ON DELETE CASCADE
ON UPDATE CASCADE;

select * from users 
CREATE TABLE CheckOut(
    MaCheckOut INT PRIMARY KEY IDENTITY,
    MaCart INT,
    NameUser NVARCHAR(255),
    Email NVARCHAR(50),
    SoDienThoai NVARCHAR(15),
    QuocGia NVARCHAR(50),
    Tinh NVARCHAR(50),
    Quan NVARCHAR(50),
    DiaChi NVARCHAR(50),
    PhuongTienThanhToan NVARCHAR(100)
);

CREATE TABLE DetailCheckOut(
	MaDetail int primary key identity,
	user_id INT,
	MaCheckOut INT,
	MaSach INT,
	SoLuong INT,
	TongTien1Loai FLOAT,
	CONSTRAINT FK_DetailCheckOut_MaSach FOREIGN KEY (MaSach)
        REFERENCES Sach(MaSach)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_DetailCheckOut_MaCheckOut FOREIGN KEY (MaCheckOut)
        REFERENCES CheckOut(MaCheckOut)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_DetailCheckOut_user_id FOREIGN KEY (user_id)
		REFERENCES Users(user_id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
-- Bước 1: Thêm cột user_id vào bảng DetailCheckOut
ALTER TABLE DetailCheckOut
ADD user_id INT;

-- Bước 2: Thêm ràng buộc khóa ngoại cho cột user_id
ALTER TABLE DetailCheckOut
ADD CONSTRAINT FK_DetailCheckOut_user_id FOREIGN KEY (user_id)
    REFERENCES Users(user_id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

CREATE PROCEDURE sp_GetDetailCheckOutAndSachInfo
    @MaCheckOut INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT dc.MaDetail, dc.MaCheckOut, dc.MaSach, dc.SoLuong, dc.TongTien1Loai,
           s.MaLoai, s.slTon, s.HinhAnh, s.MaNhaXuatBan, s.TenSach, s.MaTacGia, s.Gia, s.MoTa
    FROM DetailCheckOut dc
    INNER JOIN Sach s ON dc.MaSach = s.MaSach
    WHERE dc.MaCheckOut = @MaCheckOut;
END;

CREATE PROCEDURE sp_DeleteFromCheckOut
    @MaCheckOut INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CheckOut WHERE MaCheckOut = @MaCheckOut;
END;

CREATE PROCEDURE sp_GetAllCheckOut
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM CheckOut;
END;


CREATE PROCEDURE sp_InsertCheckOutList
    @MaCartList NVARCHAR(MAX),
    @NameUser NVARCHAR(255),
    @Email NVARCHAR(50),
    @SoDienThoai NVARCHAR(15),
    @QuocGia NVARCHAR(50),
    @Tinh NVARCHAR(50),
    @Quan NVARCHAR(50),
    @DiaChi NVARCHAR(50),
    @PhuongTienThanhToan NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaCart INT;
        DECLARE @MaCheckOut INT;

        -- Split the comma-separated MaCart list
        DECLARE @CartList TABLE (MaCart INT);
        INSERT INTO @CartList
        SELECT CAST(value AS INT) AS MaCart
        FROM STRING_SPLIT(@MaCartList, ',');

        -- Insert each MaCart from the list into CheckOut table
        DECLARE cart_cursor CURSOR FOR
        SELECT MaCart FROM @CartList;

        OPEN cart_cursor;
        FETCH NEXT FROM cart_cursor INTO @MaCart;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Kiểm tra sự tồn tại của MaCart trong bảng Cart
            IF EXISTS (SELECT 1 FROM Cart WHERE MaCart = @MaCart)
            BEGIN
                -- Thêm bản ghi vào bảng CheckOut
                INSERT INTO CheckOut (MaCart, NameUser, Email, SoDienThoai, QuocGia, Tinh, Quan, DiaChi, PhuongTienThanhToan)
                VALUES (@MaCart, @NameUser, @Email, @SoDienThoai, @QuocGia, @Tinh, @Quan, @DiaChi, @PhuongTienThanhToan);

                -- Lấy MaCheckOut vừa thêm vào
                SET @MaCheckOut = SCOPE_IDENTITY();

                -- Thêm dữ liệu từ bảng Cart vào DetailCheckOut với MaCheckOut tương ứng
                INSERT INTO DetailCheckOut (MaCheckOut, MaSach, SoLuong, TongTien1Loai)
                SELECT @MaCheckOut, MaSach, SoLuong, TongTien1Loai
                FROM Cart
                WHERE MaCart = @MaCart;

                -- Xóa bản ghi khỏi bảng Cart sau khi thêm vào bảng CheckOut và DetailCheckOut
                DELETE FROM Cart WHERE MaCart = @MaCart;
            END
            ELSE
            BEGIN
                -- Nếu MaCart không tồn tại, rollback transaction và trả về lỗi
                ROLLBACK TRANSACTION;
                RAISERROR ('MaCart %d không tồn tại trong bảng Cart.', 16, 1, @MaCart);
                RETURN;
            END;

            FETCH NEXT FROM cart_cursor INTO @MaCart;
        END;

        CLOSE cart_cursor;
        DEALLOCATE cart_cursor;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        -- Trả về thông báo lỗi
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;


-- Thêm dữ liệu vào bảng NhaXuatBan
INSERT INTO NhaXuatBan (TenNhaXuatBan, DiaChi, DienThoai)
VALUES 
(N'NXB Kim Đồng', N'Hà Nội', '0123456789'),
('NXB Trẻ', 'TP. Hồ Chí Minh', '0987654321'),
('NXB Văn Học', 'Hà Nội', '0234567890'),
('NXB Giáo Dục', 'Hà Nội', '0345678901'),
('NXB Thanh Niên', 'TP. Hồ Chí Minh', '0456789012'),
('NXB Phụ Nữ', 'Hà Nội', '0567890123'),
('NXB Công An Nhân Dân', 'Hà Nội', '0678901234'),
('NXB Quân Đội Nhân Dân', 'Hà Nội', '0789012345'),
('NXB Văn Hóa Thông Tin', 'Hà Nội', '0890123456'),
('NXB Lao Động', 'TP. Hồ Chí Minh', '0901234567');

-- Thêm dữ liệu vào bảng TacGia
INSERT INTO TacGia (TenTacGia, TieuSu)
VALUES 
('Nguyễn Nhật Ánh', 'Tiểu thuyết gia nổi tiếng với nhiều tác phẩm văn học cho tuổi teen.'),
('Nguyễn Du', 'Tác giả của Truyện Kiều, một tác phẩm kinh điển của văn học Việt Nam.'),
('Tô Hoài', 'Tác giả của Dế Mèn phiêu lưu ký và nhiều tác phẩm khác.'),
('Xuân Quỳnh', 'Nhà thơ nữ với những bài thơ tình nổi tiếng.'),
('Nguyễn Tuân', 'Tác giả của nhiều tác phẩm văn học nổi tiếng về phong cách sống và văn hóa.'),
('Nam Cao', 'Nhà văn hiện thực phê phán với những tác phẩm nổi tiếng như Chí Phèo.'),
('Hồ Chí Minh', 'Nhà cách mạng, nhà văn hóa lớn của Việt Nam.'),
('Nguyễn Minh Châu', 'Nhà văn hiện thực với nhiều tác phẩm về chiến tranh và xã hội.'),
('Nguyễn Ngọc Tư', 'Tác giả của Cánh đồng bất tận và nhiều tác phẩm về miền Tây Nam Bộ.'),
('Phạm Tiến Duật', 'Nhà thơ nổi tiếng với những bài thơ về chiến tranh và người lính.');

-- Thêm dữ liệu vào bảng LoaiSach
INSERT INTO LoaiSach (TenLH, MoTa, HinhAnh)
VALUES 
('Tiểu thuyết', 'Sách tiểu thuyết các loại', 'tieuthuyet.jpg'),
('Khoa học', 'Sách khoa học và khám phá', 'khoahoc.jpg'),
('Văn học', 'Sách văn học cổ điển và hiện đại', 'vanhoc.jpg'),
('Lịch sử', 'Sách lịch sử Việt Nam và thế giới', 'lichsu.jpg'),
('Thiếu nhi', 'Sách dành cho trẻ em', 'thieunhi.jpg'),
('Tâm lý', 'Sách tâm lý học và phát triển bản thân', 'tamly.jpg'),
('Kinh tế', 'Sách về kinh tế và kinh doanh', 'kinhte.jpg'),
('Ngoại ngữ', 'Sách học ngoại ngữ', 'ngoaingu.jpg'),
('Truyện tranh', 'Sách truyện tranh và manga', 'truyentranh.jpg'),
('Giáo dục', 'Sách giáo dục và học tập', 'giaoduc.jpg');

-- Thêm dữ liệu vào bảng Sach
INSERT INTO Sach (MaLoai, slTon, HinhAnh, MaNhaXuatBan, TenSach, MaTacGia, Gia, MoTa)
VALUES 
(1, 100, 'itemvh7.jpg', 1, 'PINOCHIO', 1, 50000, 'Tiểu thuyết hay cho tuổi teen.'),
(2, 200, 'sach2.jpg', 2, 'ádasdas',2, 60000, 'Sách khoa học dành cho học sinh.'),
(3, 150, 'sach3.jpg', 3, 'ádasdqw',3, 70000, 'Tác phẩm văn học kinh điển.'),
(4, 300, 'sach4.jpg', 4, 'ádcxsads',4, 80000, 'Sách lịch sử chi tiết.'),
(5, 250, 'sach5.jpg', 5, 'ádawq',5, 90000, 'Sách thiếu nhi hấp dẫn.'),
(6, 400, 'sach6.jpg', 6, 'ádasd',6, 100000, 'Sách tâm lý phát triển bản thân.'),
(7, 350, 'sach7.jpg', 7, 'ádqwewq',7, 110000, 'Sách kinh tế học.'),
(8, 450, 'sach8.jpg', 8, 'ádasd',8, 120000, 'Sách học ngoại ngữ dễ hiểu.'),
(9, 500, 'sach9.jpg', 9, 'hrthtr',9, 130000, 'Truyện tranh vui nhộn.'),
(10, 600, 'sach10.jpg', 10,'iuuili', 10, 140000, 'Sách giáo dục chất lượng.');

-- Thêm dữ liệu vào bảng users
INSERT INTO users (username, email, pass)
VALUES 
('user1','user1@gmail.com', 'pass1'),
('user2', 'user2@gmail.com', 'pass2'),
('user3', 'user3@gmail.com', 'pass3')
select * from sach 

CREATE PROCEDURE loaisach_create
    @TenLH NVARCHAR(50),
    @MoTa NVARCHAR(100),
    @HinhAnh NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LoaiSach (TenLH, MoTa, HinhAnh)
    VALUES (@TenLH, @MoTa, @HinhAnh);
END;

--thực thi
--tìm kiếm
create PROCEDURE [dbo].[loaisach_search]
	@tenlh nvarchar(100)
AS
    BEGIN
        SELECT *                       
        FROM LoaiSach
		WHERE TenLH LIKE '%' + @tenlh + '%';
    END;

--SỬA
CREATE PROCEDURE [dbo].[loaisach_update]
    @maLoai INT,
    @tenLH NVARCHAR(50),
    @moTa NVARCHAR(100),
	@hinhanh NVARCHAR(100)
AS
BEGIN
    UPDATE LoaiSach
    SET 
        TenLH = @tenLH,
        MoTa = @moTa,
		HinhAnh=@hinhanh
    WHERE
        MaLoai = @maLoai;
END;

--Xóa
CREATE PROCEDURE [dbo].[loaisach_delete]
    @maLoai INT
AS
BEGIN
    DELETE FROM LoaiSach
    WHERE
        MaLoai = @maLoai;
END;

--thêm 

CREATE PROCEDURE [dbo].[sach_insert]
    @maLoai INT,
    @tenSach NVARCHAR(255),
	@hinhanh NVARCHAR(255), -- Đảm bảo rằng kích thước của cột hình ảnh phù hợp với nhu cầu của bạn
    @slTon INT,
    @Gia DECIMAL(18, 2),
    @MoTa NVARCHAR(MAX),
    @MaNhaXuatBan INT,
    @MaTacGia INT
AS
BEGIN
    -- Kiểm tra xem MaLoai có tồn tại trong LOAISACH không
    IF EXISTS (SELECT 1 FROM LOAISACH WHERE MALOAI = @maLoai)
    BEGIN
        -- Chèn sách mới vào bảng Sach
        INSERT INTO Sach (MaLoai, TenSach, slTon, HinhAnh, Gia, MoTa, MaNhaXuatBan, MaTacGia)
        VALUES (@maLoai, @tenSach, @slTon, @hinhanh, @Gia, @MoTa, @MaNhaXuatBan, @MaTacGia);
        
        PRINT N'Thêm sách thành công.';
    END
    ELSE
    BEGIN
        PRINT N'Lỗi: Mã loại không tồn tại trong bảng LOAISACH.';
    END
END;


--thực thi 
-- Gọi thủ tục để thêm sách
--Sửa 
create PROCEDURE [dbo].[sach_update]
    @maSach INT,
    @maLoai INT,
    @tenSach NVARCHAR(50),
    @hinhanh NVARCHAR(MAX),
    @slTon INT,
    @Gia DECIMAL(18, 2),
    @MoTa NVARCHAR(MAX),
    @MaNhaXuatBan INT,
    @MaTacGia INT
AS
BEGIN
    -- Kiểm tra xem MaSach có tồn tại trong bảng Sach không
    IF EXISTS (SELECT 1 FROM Sach WHERE MaSach = @maSach)
    BEGIN
        -- Kiểm tra xem MaLoai có tồn tại trong LOAISACH không
        IF EXISTS (SELECT 1 FROM LOAISACH WHERE MALOAI = @maLoai)
        BEGIN
            -- Cập nhật thông tin sách
            UPDATE Sach
            SET 
                MaLoai = @maLoai,
                TenSach = @tenSach,
				HinhAnh = @hinhanh,
                slTon = @slTon,
                Gia = @Gia,
                MoTa = @MoTa,
                MaNhaXuatBan = @MaNhaXuatBan,
                MaTacGia = @MaTacGia
            WHERE
                MaSach = @maSach;

            PRINT N'Sửa sách thành công.';
        END
        ELSE
        BEGIN
            PRINT N'Lỗi: Mã loại không tồn tại trong bảng LOAISACH.';
        END
    END
    ELSE
    BEGIN
        PRINT N'Lỗi: Mã sách không tồn tại trong bảng Sach.';
    END
END;
 
--thực thi
-- Gọi thủ tục để sửa sách
--Tìm kiếm theo 1 trong các trường
create PROCEDURE [dbo].[sach_search]
    @keyword NVARCHAR(50)
AS
BEGIN
    SELECT S.*, LH.TenLH
    FROM Sach S
    INNER JOIN LoaiSach LH ON S.MaLoai = LH.MaLoai
    LEFT JOIN NhaXuatBan NXB ON S.MaNhaXuatBan = NXB.MaNhaXuatBan
    LEFT JOIN TacGia TG ON S.MaTacGia = TG.MaTacGia
    WHERE
        CONVERT(NVARCHAR(50), S.MaSach) LIKE '%' + @keyword + '%' OR
        LH.TenLH LIKE '%' + @keyword + '%' OR
        S.TenSach LIKE '%' + @keyword + '%' OR
        CONVERT(NVARCHAR(50), S.slTon) LIKE '%' + @keyword + '%' OR
        CONVERT(NVARCHAR(50), S.Gia) LIKE '%' + @keyword + '%' OR
        S.MoTa LIKE '%' + @keyword + '%' OR
        NXB.TenNhaXuatBan LIKE '%' + @keyword + '%' OR
        TG.TenTacGia LIKE '%' + @keyword + '%';
END;


--thực thi
-- Gọi thủ tục để tìm kiếm sách
EXEC sach_search @keyword =N'Ngươi'  ;
--Xóa
create   PROCEDURE [dbo].[sach_delete]
    @maSach INT
AS
BEGIN
    -- Kiểm tra xem MaSach có tồn tại trong bảng Sach không
    IF EXISTS (SELECT 1 FROM Sach WHERE MaSach = @maSach)
    BEGIN
        -- Xóa sách dựa vào MaSach
        DELETE FROM Sach
        WHERE MaSach = @maSach;

        PRINT N'Xóa sách thành công.';
    END
    ELSE
    BEGIN
        PRINT N'Lỗi: Mã sách không tồn tại trong bảng Sach.';
    END
END;

create PROCEDURE [dbo].[AllNhaXuatBan]
AS
BEGIN
    SELECT
        MaNhaXuatBan,
        TenNhaXuatBan,
        DiaChi,
        DienThoai
    FROM
        NhaXuatBan;
	END;

create  PROCEDURE [dbo].[AllTacGia]
AS
BEGIN
    SELECT
        MaTacGia,
        TenTacGia,
        TieuSu
    FROM
        TacGia;
END;


EXEC [AllTacGia]
select * from loaisach 
Alter  PROCEDURE [dbo].[AllLoaiSach]
AS
BEGIN
    SELECT
       
        MaLoai,
        TenLH,
		HinhAnh,
        MoTa 
    FROM
        LoaiSach;
END;
exec [AllLoaiSach]

alter PROCEDURE [dbo].[sach_get_by_id](@id INT)
AS
BEGIN
    SELECT
        S.MaSach,
        S.MaLoai,
        LS.TenLH, -- Lấy tên loại sách
        S.MaTacGia,
        TG.TenTacGia, -- Lấy tên tác giả
        S.MaNhaXuatBan,
        NXB.TenNhaXuatBan, -- Lấy tên nhà xuất bản
        S.TenSach,
        S.HinhAnh,
        S.MoTa,
        S.Gia,
        S.slTon
    FROM
        Sach S
    LEFT JOIN
        LoaiSach LS ON S.MaLoai = LS.MaLoai
    LEFT JOIN
        TacGia TG ON S.MaTacGia = TG.MaTacGia
    LEFT JOIN
        NhaXuatBan NXB ON S.MaNhaXuatBan = NXB.MaNhaXuatBan
    WHERE
        S.MaSach = @id;
END;

create  PROCEDURE [dbo].[loaisach_get_by_id](@id int)
AS
    BEGIN
      SELECT  *
      FROM loaisach
      where maLoai= @id;
    END;

--Thủ tục Tác giả 
CREATE PROCEDURE [dbo].[tacgia_create](
    @tentacgia NVARCHAR(100),
    @tieusu NVARCHAR(MAX)
)
AS
BEGIN
    INSERT INTO TacGia (TenTacGia, TieuSu)
    VALUES (@tentacgia, @tieusu);
END;
Create PROCEDURE [dbo].[tacgia_search](@keyword NVARCHAR(100))
AS
BEGIN
    SELECT *
    FROM TacGia
    WHERE
        TenTacGia LIKE '%' + @keyword + '%' OR
        TieuSu LIKE '%' + @keyword + '%';
END;


CREATE PROCEDURE [dbo].[tacgia_update](
    @matacgia INT,
    @tentacgia NVARCHAR(100),
    @tieusu NVARCHAR(MAX)
)
AS
BEGIN
    UPDATE TacGia
    SET 
        TenTacGia = @tentacgia,
        TieuSu = @tieusu
    WHERE
        MaTacGia = @matacgia;
END;
CREATE PROCEDURE [dbo].[tacgia_delete](@matacgia INT)
AS
BEGIN
    DELETE FROM TacGia
    WHERE
        MaTacGia = @matacgia;
END;
CREATE PROCEDURE [dbo].[tacgia_get_by_id](@matacgia INT)
AS
BEGIN
    SELECT *
    FROM TacGia
    WHERE MaTacGia = @matacgia;
END;

------Thủ tục NhaXuatBan
CREATE PROCEDURE [dbo].[nhaxuatban_create](
    @tennhaxuatban NVARCHAR(200),
    @diachi NVARCHAR(500),
    @dienthoai NVARCHAR(20)
)
AS
BEGIN
    INSERT INTO NhaXuatBan (TenNhaXuatBan, DiaChi, DienThoai)
    VALUES (@tennhaxuatban, @diachi, @dienthoai);
END;
CREATE PROCEDURE [dbo].[nhaxuatban_search](@keyword NVARCHAR(100))
AS
BEGIN
    SELECT *
    FROM NhaXuatBan
    WHERE TenNhaXuatBan LIKE '%' + @keyword + '%'
       OR DiaChi LIKE '%' + @keyword + '%'
       OR DienThoai LIKE '%' + @keyword + '%';
END;
CREATE PROCEDURE [dbo].[nhaxuatban_update](
    @manhaxuatban INT,
    @tennhaxuatban NVARCHAR(200),
    @diachi NVARCHAR(500),
    @dienthoai NVARCHAR(20)
)
AS
BEGIN
    UPDATE NhaXuatBan
    SET 
        TenNhaXuatBan = @tennhaxuatban,
        DiaChi = @diachi,
        DienThoai = @dienthoai
    WHERE
        MaNhaXuatBan = @manhaxuatban;
END;
CREATE PROCEDURE [dbo].[nhaxuatban_delete](@manhaxuatban INT)
AS
BEGIN
    DELETE FROM NhaXuatBan
    WHERE
        MaNhaXuatBan = @manhaxuatban;
END;
CREATE PROCEDURE [dbo].[nhaxuatban_get_by_id](@manhaxuatban INT)
AS
BEGIN
    SELECT *
    FROM NhaXuatBan
    WHERE MaNhaXuatBan = @manhaxuatban;
END;
CREATE  PROCEDURE [dbo].[LayDanhSachSach]
AS
BEGIN
    SELECT
        S.MaSach,
        S.MaLoai,
        LS.TenLH , -- Lấy tên loại sách
		S.MaTacGia,
		TG.TenTacGia , -- Lấy tên tác giả
		NXB.MaNhaXuatBan,
		NXB.TenNhaXuatBan, -- Lấy tên nhà xuất bản
        S.TenSach,
		S.HinhAnh,
		S.MoTa,
		S.Gia,
        S.slTon
    FROM
        Sach S
    LEFT JOIN
        LoaiSach LS ON S.MaLoai = LS.MaLoai
    LEFT JOIN
        TacGia TG ON S.MaTacGia = TG.MaTacGia
    LEFT JOIN
        NhaXuatBan NXB ON S.MaNhaXuatBan = NXB.MaNhaXuatBan;
END;

CREATE PROCEDURE sp_AddToCart
    @MaSach INT,
	@UserId INT,
    @SoLuong INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Gia FLOAT;
    DECLARE @TongTien1Loai FLOAT;
    DECLARE @TongTienCart FLOAT;

    -- Lấy giá của sách từ bảng Sach
    SELECT @Gia = Gia FROM Sach WHERE MaSach = @MaSach;

    -- Tính toán TongTien1Loai
    SET @TongTien1Loai = @SoLuong * @Gia;

    -- Thêm bản ghi vào bảng Cart
    INSERT INTO Cart (MaSach, User_id, SoLuong, TongTien1Loai)
    VALUES (@MaSach, @UserId, @SoLuong, @TongTien1Loai);

    -- Cập nhật TongTienCart
    SET @TongTienCart = (SELECT SUM(TongTien1Loai) FROM Cart);
    
    -- Cập nhật TongTienCart cho tất cả các bản ghi trong bảng Cart
    UPDATE Cart
    SET TongTienCart = @TongTienCart;
END;


CREATE PROCEDURE sp_GetCartDetails
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.MaCart,
        c.User_id,
        c.MaSach,
        s.HinhAnh,
        s.TenSach,
        c.SoLuong,
        s.Gia,
        c.TongTien1Loai,
        c.TongTienCart
    FROM 
        Cart c
    INNER JOIN 
        Sach s ON c.MaSach = s.MaSach
    WHERE
        c.User_id = @UserId;
END;


CREATE PROCEDURE sp_DeleteCartRecord
    @MaCart INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nếu bản ghi tồn tại trong bảng Cart
    IF EXISTS (SELECT 1 FROM Cart WHERE MaCart = @MaCart)
    BEGIN
        -- Xóa bản ghi từ bảng Cart
        DELETE FROM Cart WHERE MaCart = @MaCart;

        -- Cập nhật lại tổng tiền của giỏ hàng
        UPDATE Cart
        SET TongTienCart = (SELECT SUM(TongTien1Loai) FROM Cart)
        WHERE MaCart = @MaCart;
        
        PRINT 'Bản ghi đã được xóa thành công và tổng tiền của giỏ hàng đã được cập nhật.';
    END
    ELSE
    BEGIN
        PRINT 'Không tìm thấy bản ghi với MaCart đã cho.';
    END
END;

CREATE PROCEDURE [dbo].[checkout_delete]
	@maCheckOut INT
AS
BEGIN
    DELETE FROM CheckOut
    WHERE
        MaCheckOut = @maCheckOut;
END;

CREATE PROCEDURE sp_register
    @username NVARCHAR(255),
    @Email NVARCHAR(255),
    @Pass NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem email đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Users WHERE email = @Email)
    BEGIN
        -- Nếu email đã tồn tại, trả về thông báo lỗi
        RAISERROR ('Email đã tồn tại.', 16, 1);
    END
    ELSE
    BEGIN
        -- Nếu email chưa tồn tại, thêm bản ghi mới vào bảng Users
        INSERT INTO Users (username, email, pass)
        VALUES (@username, @Email, @Pass);

        -- Trả về thông báo thành công
        SELECT 'Đăng ký thành công!' AS Message;
    END
END;

CREATE PROCEDURE sp_login
    @Email NVARCHAR(255),
    @Pass NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem email và mật khẩu có khớp không
    IF EXISTS (SELECT 1 FROM Users WHERE email = @Email AND pass = @Pass)
    BEGIN
        -- Nếu khớp, trả về thông tin người dùng
        SELECT 
            user_id,
            username,
            email
        FROM 
            Users
        WHERE 
            email = @Email AND pass = @Pass;
    END
    ELSE
    BEGIN
        -- Nếu không khớp, trả về thông báo lỗi
        SELECT 'Email hoặc mật khẩu không đúng.' AS Message;
    END
END;


CREATE PROCEDURE sp_get_user_by_id
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN
        SELECT 
           *
        FROM 
            Users
        WHERE 
            user_id = @UserId;
    END
END;
-----------------
-----------------
CREATE PROCEDURE GetBooksByCategory
    @MaLoai INT
AS
BEGIN
    SELECT 
        Sach.MaSach,
        Sach.TenSach,
        Sach.Gia,
        Sach.HinhAnh,
        Sach.MoTa,
        Sach.slTon,
        LoaiSach.TenLH,
        LoaiSach.MoTa AS MoTaLoaiSach,
        LoaiSach.HinhAnh AS HinhAnhLoaiSach
    FROM 
        Sach
    JOIN 
        LoaiSach ON Sach.MaLoai = LoaiSach.MaLoai
    WHERE 
        Sach.MaLoai = @MaLoai;
END;

CREATE PROCEDURE sp_GetTop8LoaiSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 8 *
    FROM LoaiSach
    ORDER BY NEWID();  -- Sắp xếp ngẫu nhiên
END;

--------------------
------------------------
---------------------
SELECT * FROM sach 
CREATE PROCEDURE sp_history
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        dc.MaDetail,
        dc.user_id,
        dc.MaCheckOut,
        dc.MaSach,
        dc.SoLuong,
        dc.TongTien1Loai,
        s.HinhAnh,
		s.TenSach
    FROM 
        DetailCheckOut dc
    INNER JOIN 
        Sach s ON dc.MaSach = s.MaSach
    WHERE 
        dc.user_id = @UserId;
END;

EXEC sp_history @UserId = 3;
CREATE PROCEDURE sp_DeleteDetailCheckOut
    @MaDetail INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaCheckOut INT;

        -- Lấy MaCheckOut từ DetailCheckOut
        SELECT @MaCheckOut = MaCheckOut FROM DetailCheckOut WHERE MaDetail = @MaDetail;

        -- Xóa bản ghi từ DetailCheckOut
        DELETE FROM DetailCheckOut WHERE MaDetail = @MaDetail;

        -- Kiểm tra xem còn bản ghi nào khác trong DetailCheckOut có cùng MaCheckOut không
        IF NOT EXISTS (SELECT 1 FROM DetailCheckOut WHERE MaCheckOut = @MaCheckOut)
        BEGIN
            -- Nếu không còn bản ghi nào khác, xóa bản ghi tương ứng trong CheckOut
            DELETE FROM CheckOut WHERE MaCheckOut = @MaCheckOut;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        -- Trả về thông báo lỗi
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
ALTER PROCEDURE [dbo].[sp_login]
    @Email NVARCHAR(255),
    @Pass NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem email và mật khẩu có khớp không
    IF EXISTS (SELECT 1 FROM Users WHERE email = @Email AND pass = @Pass)
    BEGIN
        -- Nếu khớp, trả về thông tin người dùng
        SELECT 
            user_id,
            username,
            email
        FROM 
            Users
        WHERE 
            email = @Email AND pass = @Pass;
    END
    ELSE
    BEGIN
        -- Nếu không khớp, trả về thông báo lỗi
        SELECT 
            CAST(0 AS INT) AS user_id, 
            NULL AS username, 
            NULL AS email;
    END
END;

ALTER PROCEDURE sp_DeleteCartRecord
    @MaCart INT,
    @User_id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Xóa sản phẩm từ giỏ hàng
        DELETE FROM Cart
        WHERE MaCart = @MaCart AND User_id = @User_id;

        -- Tính toán lại tổng tiền của giỏ hàng cho người dùng
        DECLARE @TongTienCart FLOAT;

        SELECT @TongTienCart = SUM(TongTien1Loai)
        FROM Cart
        WHERE User_id = @User_id;
		-- Cập nhật lại tổng tiền của giỏ hàng cho người dùng
        UPDATE Cart
        SET TongTienCart = @TongTienCart
        WHERE User_id = @User_id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        -- Trả về thông báo lỗi
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
select * from Users

ALTER PROCEDURE [dbo].[sp_AddToCart]
    @MaSach INT,
    @UserId INT,
    @SoLuong INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Gia FLOAT;
    DECLARE @TongTien1Loai FLOAT;
    DECLARE @TongTienCart FLOAT;
    DECLARE @CurrentSoLuong INT;

    -- Lấy giá của sách từ bảng Sach
    SELECT @Gia = Gia FROM Sach WHERE MaSach = @MaSach;

    -- Kiểm tra xem MaSach đã tồn tại trong giỏ hàng của người dùng chưa
    IF EXISTS (SELECT 1 FROM Cart WHERE MaSach = @MaSach AND User_id = @UserId)
    BEGIN
        -- Lấy số lượng hiện tại của MaSach trong giỏ hàng
        SELECT @CurrentSoLuong = SoLuong FROM Cart WHERE MaSach = @MaSach AND User_id = @UserId;

        -- Tính toán tổng số lượng mới
        SET @SoLuong = @SoLuong + @CurrentSoLuong;

        -- Tính toán TongTien1Loai
        SET @TongTien1Loai = @SoLuong * @Gia;

        -- Cập nhật số lượng và TongTien1Loai cho bản ghi hiện có
        UPDATE Cart
        SET SoLuong = @SoLuong, TongTien1Loai = @TongTien1Loai
        WHERE MaSach = @MaSach AND User_id = @UserId;
    END
    ELSE
    BEGIN
        -- Tính toán TongTien1Loai
        SET @TongTien1Loai = @SoLuong * @Gia;

        -- Thêm bản ghi mới vào bảng Cart
        INSERT INTO Cart (MaSach, User_id, SoLuong, TongTien1Loai)
        VALUES (@MaSach, @UserId, @SoLuong, @TongTien1Loai);
    END

    -- Tính toán lại TongTienCart cho người dùng hiện tại
    SET @TongTienCart = (SELECT SUM(TongTien1Loai) FROM Cart WHERE User_id = @UserId);

    -- Cập nhật TongTienCart cho tất cả các bản ghi trong giỏ hàng của người dùng hiện tại
    UPDATE Cart
    SET TongTienCart = @TongTienCart
    WHERE User_id = @UserId;
END;

ALTER PROCEDURE [dbo].[sp_InsertCheckOutList]
    @MaCartList NVARCHAR(MAX),
    @NameUser NVARCHAR(255),
    @Email NVARCHAR(50),
    @SoDienThoai NVARCHAR(15),
    @QuocGia NVARCHAR(50),
    @Tinh NVARCHAR(50),
    @Quan NVARCHAR(50),
    @DiaChi NVARCHAR(50),
    @PhuongTienThanhToan NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaCart INT;
        DECLARE @MaCheckOut INT;
        DECLARE @UserId INT;

        -- Split the comma-separated MaCart list
        DECLARE @CartList TABLE (MaCart INT);
        INSERT INTO @CartList
        SELECT CAST(value AS INT) AS MaCart
        FROM STRING_SPLIT(@MaCartList, ',');

        -- Insert each MaCart from the list into CheckOut table
        DECLARE cart_cursor CURSOR FOR
        SELECT MaCart FROM @CartList;

        OPEN cart_cursor;
        FETCH NEXT FROM cart_cursor INTO @MaCart;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Kiểm tra sự tồn tại của MaCart trong bảng Cart
            IF EXISTS (SELECT 1 FROM Cart WHERE MaCart = @MaCart)
            BEGIN
                -- Lấy user_id từ bảng Cart
                SELECT @UserId = User_id FROM Cart WHERE MaCart = @MaCart;

                -- Thêm bản ghi vào bảng CheckOut
                INSERT INTO CheckOut (MaCart, NameUser, Email, SoDienThoai, QuocGia, Tinh, Quan, DiaChi, PhuongTienThanhToan)
                VALUES (@MaCart, @NameUser, @Email, @SoDienThoai, @QuocGia, @Tinh, @Quan, @DiaChi, @PhuongTienThanhToan);

                -- Lấy MaCheckOut vừa thêm vào
                SET @MaCheckOut = SCOPE_IDENTITY();

                -- Thêm dữ liệu từ bảng Cart vào DetailCheckOut với MaCheckOut và user_id tương ứng
                INSERT INTO DetailCheckOut (MaCheckOut, user_id, MaSach, SoLuong, TongTien1Loai)
                SELECT @MaCheckOut, @UserId, MaSach, SoLuong, TongTien1Loai
                FROM Cart
                WHERE MaCart = @MaCart;

                -- Xóa bản ghi khỏi bảng Cart sau khi thêm vào bảng CheckOut và DetailCheckOut
                DELETE FROM Cart WHERE MaCart = @MaCart;
            END
            ELSE
            BEGIN
                -- Nếu MaCart không tồn tại, rollback transaction và trả về lỗi
                ROLLBACK TRANSACTION;
                RAISERROR ('MaCart %d không tồn tại trong bảng Cart.', 16, 1, @MaCart);
                RETURN;
            END;

            FETCH NEXT FROM cart_cursor INTO @MaCart;
        END;

        CLOSE cart_cursor;
        DEALLOCATE cart_cursor;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        -- Trả về thông báo lỗi
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
----------------------
ALTER PROCEDURE [dbo].[sp_AddToCart]
    @MaSach INT,
    @UserId INT,
    @SoLuong INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Gia FLOAT;
    DECLARE @TongTien1Loai FLOAT;
    DECLARE @TongTienCart FLOAT;
    DECLARE @CurrentSoLuong INT;

    -- Lấy giá của sách từ bảng Sach
    SELECT @Gia = Gia FROM Sach WHERE MaSach = @MaSach;

    -- Kiểm tra xem MaSach đã tồn tại trong giỏ hàng của người dùng chưa
    IF EXISTS (SELECT 1 FROM Cart WHERE MaSach = @MaSach AND User_id = @UserId)
    BEGIN
        -- Lấy số lượng hiện tại của MaSach trong giỏ hàng
        SELECT @CurrentSoLuong = SoLuong FROM Cart WHERE MaSach = @MaSach AND User_id = @UserId;

        -- Tính toán tổng số lượng mới
        SET @SoLuong = @SoLuong + @CurrentSoLuong;

        -- Tính toán TongTien1Loai
        SET @TongTien1Loai = @SoLuong * @Gia;

        -- Cập nhật số lượng và TongTien1Loai cho bản ghi hiện có
        UPDATE Cart
        SET SoLuong = @SoLuong, TongTien1Loai = @TongTien1Loai
        WHERE MaSach = @MaSach AND User_id = @UserId;
    END
    ELSE
    BEGIN
        -- Tính toán TongTien1Loai
        SET @TongTien1Loai = @SoLuong * @Gia;

        -- Thêm bản ghi mới vào bảng Cart
        INSERT INTO Cart (MaSach, User_id, SoLuong, TongTien1Loai)
        VALUES (@MaSach, @UserId, @SoLuong, @TongTien1Loai);
    END

    -- Tính toán lại TongTienCart cho người dùng hiện tại
    SET @TongTienCart = (SELECT SUM(TongTien1Loai) FROM Cart WHERE User_id = @UserId);

    -- Cập nhật TongTienCart cho tất cả các bản ghi trong giỏ hàng của người dùng hiện tại
    UPDATE Cart
    SET TongTienCart = @TongTienCart
    WHERE User_id = @UserId;
END;
select * from checkout 
select * from Cart   
delete from Cart 
SELECT * FROM USERS 