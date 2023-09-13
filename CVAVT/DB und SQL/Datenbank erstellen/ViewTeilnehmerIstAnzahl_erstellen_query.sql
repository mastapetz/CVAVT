USE [CVAVT]
GO

/****** Object:  View [dbo].[ViewTeilnehmerIstAnzahl]    Script Date: 13.09.2023 11:27:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[ViewTeilnehmerIstAnzahl] AS
SELECT [AktivitaetIDFK], COUNT(*) As TeilnehmerIstAnzahl
From [Teilnehmer]
Group by [AktivitaetIDFK]
GO


