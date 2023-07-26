USE [CVAVT]
GO



ALTER TABLE [dbo].[Aktivitaet] DROP CONSTRAINT [DF_Aktivitaet_AktivitaetenVorwissenNoetig]
GO

/****** Object:  Table [dbo].[Aktivitaet]    Script Date: 24.07.2023 12:54:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Aktivitaet]') AND type in (N'U'))
DROP TABLE [dbo].[Aktivitaet]
GO

/****** Object:  Table [dbo].[Aktivitaet]    Script Date: 24.07.2023 12:54:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Aktivitaet](
	[AktivitaetenID]  [int] IDENTITY(1,1) PRIMARY KEY,
	[AktivitaetenName] [nvarchar](50) NOT NULL,
	[AktivitaetenArt] [nvarchar](50) NOT NULL,
	[AktivitaetenDatum] [date] NOT NULL,
	[AktivitaetenZeit] [datetime] NOT NULL,
	[AktivitaetenDauer] [float] NOT NULL,
	[AktivitaetenMaxTeilnehmer] [int] NULL,
	[AktivitaetenVorwissenNoetig] [bit] NOT NULL,
	[AktivitaetenInformation] [nvarchar](100) NULL,
	[LeiterIDFK] [int] NOT NULL,
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Aktivitaet] ADD  CONSTRAINT [DF_Aktivitaet_AktivitaetenVorwissenNoetig]  DEFAULT ((0)) FOR [AktivitaetenVorwissenNoetig]
GO

ALTER TABLE [dbo].[Aktivitaet]  WITH CHECK ADD  CONSTRAINT [FK_Aktivitaet_Leiter] FOREIGN KEY([LeiterIDFK])
REFERENCES [dbo].[Leiter] ([LeiterID])
GO

ALTER TABLE [dbo].[Aktivitaet] CHECK CONSTRAINT [FK_Aktivitaet_Leiter]
GO


