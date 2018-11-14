USE [MetroCase]
GO

/****** Object:  Table [dbo].[Kategoriler]    Script Date: 15.11.2018 01:15:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kategoriler](
	[KategoriId] [int] IDENTITY(1,1) NOT NULL,
	[KategoriAdi] [nvarchar](50) NOT NULL,
	[KategoriAciklama] [nvarchar](100) NULL,
 CONSTRAINT [PK_Kategoriler] PRIMARY KEY CLUSTERED 
(
	[KategoriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

