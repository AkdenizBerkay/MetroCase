USE [MetroCase]
GO

/****** Object:  Table [dbo].[Satislar]    Script Date: 15.11.2018 01:16:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Satislar](
	[SatisId] [int] IDENTITY(1,1) NOT NULL,
	[UrunId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[UrunAdet] [int] NOT NULL,
	[SatisDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Satislar] PRIMARY KEY CLUSTERED 
(
	[SatisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Satislar]  WITH CHECK ADD  CONSTRAINT [FK_Satislar_Kullanicilar] FOREIGN KEY([UserId])
REFERENCES [dbo].[Kullanicilar] ([UserId])
GO

ALTER TABLE [dbo].[Satislar] CHECK CONSTRAINT [FK_Satislar_Kullanicilar]
GO

ALTER TABLE [dbo].[Satislar]  WITH CHECK ADD  CONSTRAINT [FK_Satislar_Urunler] FOREIGN KEY([UrunId])
REFERENCES [dbo].[Urunler] ([UrunId])
GO

ALTER TABLE [dbo].[Satislar] CHECK CONSTRAINT [FK_Satislar_Urunler]
GO

