USE [CVAVT]
GO


/****** Object:  Table [dbo].[Leiter]    Script Date: 24.07.2023 12:34:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Leiter]') AND type in (N'U'))
DROP TABLE [dbo].[Leiter]
GO

/****** Object:  Table [dbo].[Leiter]    Script Date: 24.07.2023 12:34:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Leiter](
	[LeiterID] [int] IDENTITY(1,1) PRIMARY KEY,
	[LeiterName] [nvarchar](50) NOT NULL
) ON [PRIMARY];

GO

ALTER TABLE [dbo].[Leiter]  WITH CHECK ADD  CONSTRAINT [FK_Leiter_Leiter] FOREIGN KEY([LeiterID])
REFERENCES [dbo].[Leiter] ([LeiterID])
GO

ALTER TABLE [dbo].[Leiter] CHECK CONSTRAINT [FK_Leiter_Leiter]
GO


