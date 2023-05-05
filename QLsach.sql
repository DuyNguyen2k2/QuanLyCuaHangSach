create database [QuanLySach]
USE [QuanLySach]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 5/28/2022 8:11:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[Mã] [int] NOT NULL,
	[Mã hóa đơn] [nvarchar](10) NOT NULL,
	[Mã sách] [nvarchar](10) NULL,
	[Số lượng] [int] NULL,
	[Thành tiền] [int] NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[Mã] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 5/28/2022 8:11:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[Mã hóa đơn] [nvarchar](10) NOT NULL,
	[Ngày tạo] [date] NULL,
	[Tên khách hàng] [nvarchar](50) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[Mã hóa đơn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 5/28/2022 8:11:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[Mã sách] [nvarchar](10) NOT NULL,
	[Số lượng] [int] NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[Mã sách] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 5/28/2022 8:11:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[Mã sách] [nvarchar](10) NOT NULL,
	[Tên sách] [nvarchar](50) NULL,
	[Chủng loại] [nvarchar](50) NULL,
	[Nhà xuất bản] [nvarchar](50) NULL,
	[Tác giả] [nvarchar](50) NULL,
	[Giá] [int] NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[Mã sách] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([Mã hóa đơn])
REFERENCES [dbo].[HoaDon] ([Mã hóa đơn]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_Sach] FOREIGN KEY([Mã sách])
REFERENCES [dbo].[Sach] ([Mã sách]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_Sach]
GO
ALTER TABLE [dbo].[Kho]  WITH CHECK ADD  CONSTRAINT [FK_Kho_Sach] FOREIGN KEY([Mã sách])
REFERENCES [dbo].[Sach] ([Mã sách]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho] CHECK CONSTRAINT [FK_Kho_Sach]
GO
create trigger automatic_update on ChiTietHoaDon after insert as
begin
	update Kho set [Số lượng] = Kho.[Số lượng] - (select [Số lượng] from inserted where [Mã sách] = Kho	.[Mã sách])
	from Kho join inserted on Kho.[Mã sách] = inserted.[Mã sách]
end
go
CREATE TRIGGER [dbo].[DeleteSach]
ON [dbo].[Sach]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM [dbo].[ChiTietHoaDon] WHERE [Mã sách] IN (SELECT [Mã sách] FROM DELETED)
    
    DELETE FROM [dbo].[HoaDon] WHERE [Mã hóa đơn] NOT IN (SELECT [Mã hóa đơn] FROM [dbo].[ChiTietHoaDon])
END
go
CREATE TRIGGER [dbo].[InsertKho]
ON [dbo].[Sach]
AFTER INSERT
AS
BEGIN
	INSERT INTO [dbo].[Kho] values ((SELECT [Mã sách] FROM INSERTED), 0)
END

select * from Sach
select * from HoaDon
select * from ChiTietHoaDon
select * from Kho

insert into Sach values ('1', N'Triết học Mác Lê-nin', N'Chính trị', N'Kim Đồng', 'Lenin', 2000000)
insert into Sach values ('2', N'Triết học Mác Lê-nin', N'Chính trị', N'Kim Đồng', 'Lenin', 2000000)

update Kho set [Số lượng] = 100 where [Mã sách] = '1'
update Kho set [Số lượng] = 100 where [Mã sách] = '2'

insert into HoaDon values ('1', '2022-05-26', N'Nguyễn Văn Duy')
insert into ChiTietHoaDon values (1, '1', '1', 4, 800000)
insert into ChiTietHoaDon values (2, '1', '2', 4, 800000)

delete from Sach where [Mã sách] = '1'
