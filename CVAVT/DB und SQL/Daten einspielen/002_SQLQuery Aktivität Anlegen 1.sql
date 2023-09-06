USE [CVAVT]
GO

INSERT INTO [dbo].[Aktivitaet]
           ([AktivitaetenName]
           ,[AktivitaetenArt]
           ,[AktivitaetenDatum]
           ,[AktivitaetenZeit]
           ,[AktivitaetenDauer]
           ,[AktivitaetenMaxTeilnehmer]
           
           ,[AktivitaetenInformation]
           ,[LeiterIDFK])
     VALUES
           ('Call of Cthulu',
           'Rollenspiel',
           '12.12.2023',
           '12:00:00',
           2.5,
           4,
           
           'Blabla Blablabla Blablabla',
           1)
GO


