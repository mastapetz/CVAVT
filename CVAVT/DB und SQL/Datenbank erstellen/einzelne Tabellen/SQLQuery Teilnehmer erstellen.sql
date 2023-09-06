USE [CVAVT]
GO

ALTER TABLE [dbo].[Teilnehmer] DROP CONSTRAINT [FK_Teilnehmer_Aktivitaet]
GO

/****** Object:  Table [dbo].[Teilnehmer]    Script Date: 24.07.2023 12:49:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teilnehmer]') AND type in (N'U'))
DROP TABLE [dbo].[Teilnehmer]
GO

/****** Object:  Table [dbo].[Teilnehmer]    Script Date: 24.07.2023 12:49:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Teilnehmer](
	[TeilnehmerID] [int] IDENTITY(1,1) PRIMARY KEY,
	[TeilnehmerName] [nvarchar](50) NOT NULL,
	[AktivitaetIDFK] [int] NOT NULL,
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Teilnehmer]  WITH CHECK ADD  CONSTRAINT [FK_Teilnehmer_Aktivitaet] FOREIGN KEY([AktivitaetIDFK])
REFERENCES [dbo].[Aktivitaet] ([AktivitaetenID])
GO

ALTER TABLE [dbo].[Teilnehmer] CHECK CONSTRAINT [FK_Teilnehmer_Aktivitaet]
GO


