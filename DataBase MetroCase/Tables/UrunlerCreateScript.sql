USE [MetroCase]
GO

/****** Object:  Table [dbo].[Urunler]    Script Date: 15.11.2018 01:16:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Urunler](
	[UrunId] [int] IDENTITY(1,1) NOT NULL,
	[UrunAdi] [nvarchar](50) NOT NULL,
	[UrunAciklama] [nvarchar](100) NULL,
	[KategoriId] [int] NOT NULL,
	[UrunFiyat] [decimal](18, 0) NOT NULL,
	[UrunFoto] [nvarchar](50) NULL,
	[UrunStokAdedi] [int] NOT NULL,
 CONSTRAINT [PK_Urunler] PRIMARY KEY CLUSTERED 
(
	[UrunId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Urunler]  WITH CHECK ADD  CONSTRAINT [FK_Urunler_Kategoriler] FOREIGN KEY([KategoriId])
REFERENCES [dbo].[Kategoriler] ([KategoriId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Urunler] CHECK CONSTRAINT [FK_Urunler_Kategoriler]
GO

