USE [CVAVT]
GO

/****** Object:  Table [dbo].[Kategorie]    Script Date: 24.07.2023 12:40:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kategorie]') AND type in (N'U'))
DROP TABLE [dbo].[Kategorie]
GO

/****** Object:  Table [dbo].[Kategorie]    Script Date: 24.07.2023 12:40:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kategorie](
	[KategorieID] [int] IDENTITY(1,1) PRIMARY KEY,
	[KategorieName] [nvarchar](50) NOT NULL,

) ON [PRIMARY]
GO


