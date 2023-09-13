USE [CVAVT]
GO
/****** Object:  Table [dbo].[Teilnehmer]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teilnehmer](
	[TeilnehmerID] [int] IDENTITY(1,1) NOT NULL,
	[TeilnehmerName] [nvarchar](50) NOT NULL,
	[AktivitaetIDFK] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeilnehmerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewTeilnehmerIstAnzahl]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[ViewTeilnehmerIstAnzahl] AS
SELECT [AktivitaetIDFK], COUNT(*) As TeilnehmerIstAnzahl
From [Teilnehmer]
Group by [AktivitaetIDFK]
GO
/****** Object:  Table [dbo].[ActivitaetKategorie]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivitaetKategorie](
	[ActivitaetIDFK] [int] NOT NULL,
	[KategorieIDFK] [int] NOT NULL,
 CONSTRAINT [PK_ActivitaetKategorie] PRIMARY KEY CLUSTERED 
(
	[ActivitaetIDFK] ASC,
	[KategorieIDFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aktivitaet]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aktivitaet](
	[AktivitaetenID] [int] IDENTITY(1,1) NOT NULL,
	[AktivitaetenName] [nvarchar](50) NOT NULL,
	[AktivitaetenArt] [nvarchar](50) NOT NULL,
	[AktivitaetenDatum] [date] NOT NULL,
	[AktivitaetenZeit] [datetime] NOT NULL,
	[AktivitaetenDauer] [float] NOT NULL,
	[AktivitaetenMaxTeilnehmer] [int] NULL,
	[AktivitaetenVorwissenNoetig] [bit] NOT NULL,
	[AktivitaetenInformation] [nvarchar](100) NULL,
	[LeiterIDFK] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AktivitaetenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategorie]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategorie](
	[KategorieID] [int] IDENTITY(1,1) NOT NULL,
	[KategorieName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[KategorieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leiter]    Script Date: 13.09.2023 11:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leiter](
	[LeiterID] [int] IDENTITY(1,1) NOT NULL,
	[LeiterName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LeiterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aktivitaet] ADD  CONSTRAINT [DF_Aktivitaet_AktivitaetenVorwissenNoetig]  DEFAULT ((0)) FOR [AktivitaetenVorwissenNoetig]
GO
ALTER TABLE [dbo].[ActivitaetKategorie]  WITH CHECK ADD  CONSTRAINT [FK_ActivitaetKategorie_Aktivitaet] FOREIGN KEY([ActivitaetIDFK])
REFERENCES [dbo].[Aktivitaet] ([AktivitaetenID])
GO
ALTER TABLE [dbo].[ActivitaetKategorie] CHECK CONSTRAINT [FK_ActivitaetKategorie_Aktivitaet]
GO
ALTER TABLE [dbo].[ActivitaetKategorie]  WITH CHECK ADD  CONSTRAINT [FK_ActivitaetKategorie_Kategorie] FOREIGN KEY([KategorieIDFK])
REFERENCES [dbo].[Kategorie] ([KategorieID])
GO
ALTER TABLE [dbo].[ActivitaetKategorie] CHECK CONSTRAINT [FK_ActivitaetKategorie_Kategorie]
GO
ALTER TABLE [dbo].[Aktivitaet]  WITH CHECK ADD  CONSTRAINT [FK_Aktivitaet_Leiter] FOREIGN KEY([LeiterIDFK])
REFERENCES [dbo].[Leiter] ([LeiterID])
GO
ALTER TABLE [dbo].[Aktivitaet] CHECK CONSTRAINT [FK_Aktivitaet_Leiter]
GO
ALTER TABLE [dbo].[Teilnehmer]  WITH CHECK ADD  CONSTRAINT [FK_Teilnehmer_Aktivitaet] FOREIGN KEY([AktivitaetIDFK])
REFERENCES [dbo].[Aktivitaet] ([AktivitaetenID])
GO
ALTER TABLE [dbo].[Teilnehmer] CHECK CONSTRAINT [FK_Teilnehmer_Aktivitaet]
GO
