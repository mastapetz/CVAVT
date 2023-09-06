USE [CVAVT]
GO

/****** Object:  Table [dbo].[Aktivitaet]    Script Date: 17.07.2023 17:16:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Aktivitaet](
	[AktivitaetenID] [int] NOT NULL,
	[AktivitaetenName] [nvarchar](50) NOT NULL,
	[AktivitaetenArt] [nvarchar](50) NOT NULL,
	[AktivitaetenDatum] [date] NOT NULL,
	[AktivitaetenZeit] [datetime] NOT NULL,
	[AktivitaetenDauer] [float] NOT NULL,
	[AktivitaetenMaxTeilnehmer] [int] NULL,
	[AktivitaetenVorwissenNoetig] [bit] NOT NULL,
	[AktivitaetenInformation] [nvarchar](100) NULL,
	[LeiterIDFK] [int] NOT NULL,
 CONSTRAINT [PK_Aktivitaet] PRIMARY KEY CLUSTERED 
(
	[AktivitaetenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Aktivitaet] ADD  CONSTRAINT [DF_Aktivitaet_AktivitaetenVorwissenNoetig]  DEFAULT ((0)) FOR [AktivitaetenVorwissenNoetig]
GO


