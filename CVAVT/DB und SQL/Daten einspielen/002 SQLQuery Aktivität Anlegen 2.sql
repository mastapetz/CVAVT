USE [CVAVT]
GO

INSERT INTO [dbo].[Aktivitaet]
           ([AktivitaetenName]
           ,[AktivitaetenArt]
           ,[AktivitaetenDatum]
           ,[AktivitaetenZeit]
           ,[AktivitaetenDauer]
           ,[AktivitaetenMaxTeilnehmer]
           ,[AktivitaetenVorwissenNoetig]
           ,[AktivitaetenInformation]
           ,[LeiterIDFK])
     VALUES
           ('Modellbau f�r Fortgeschrittene',
           'Workshop',
           '2023-08-12',
           '10:30:00',
           2.5,
           4,
           1,
           'Blabla Blablabla Blablabla',
           1)
GO


