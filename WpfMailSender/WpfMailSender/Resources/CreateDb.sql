USE master;
--------------------------
IF DB_ID('MailsAndSenders') IS NOT NULL DROP DATABASE MailsAndSenders;

CREATE DATABASE MailsAndSenders;
GO

USE MailsAndSenders;
GO

--------------------------

CREATE SCHEMA ML AUTHORIZATION dbo;
GO

--------------------------

CREATE TABLE ML.Emails
(
	RecipientId		INT				NOT NULL IDENTITY(1, 1),
	Email			NVARCHAR(MAX)	NOT NULL,
	Name			NVARCHAR(MAX)	NOT NULL,
	
	CONSTRAINT PK_Department PRIMARY KEY(RecipientId)
);

--------------------------
INSERT INTO [ML].[Emails]
 VALUES ('spam1@ksergey.ru', 'Тестовый 1')
INSERT INTO [ML].[Emails]
 VALUES ('spam2@ksergey.ru', 'Тестовый 2')
INSERT INTO [ML].[Emails]
 VALUES ('spam3@ksergey.ru', 'Тестовый 3')
INSERT INTO [ML].[Emails]
 VALUES ('spam4@ksergey.ru', 'Тестовый 4')
INSERT INTO [ML].[Emails]
 VALUES ('spam5@ksergey.ru', 'Тестовый 5')
INSERT INTO [ML].[Emails]
 VALUES ('spam6@ksergey.ru', 'Тестовый 6')
INSERT INTO [ML].[Emails]
 VALUES ('spam7@ksergey.ru', 'Тестовый 7')
INSERT INTO [ML].[Emails]
 VALUES ('spam8@ksergey.ru', 'Тестовый 8')
INSERT INTO [ML].[Emails]
 VALUES ('spam9@ksergey.ru', 'Тестовый 9')
INSERT INTO [ML].[Emails]
 VALUES ('spam10@ksergey.ru', 'Тестовый 10')
INSERT INTO [ML].[Emails]
 VALUES ('spam11@ksergey.ru', 'Тестовый 11')
INSERT INTO [ML].[Emails]
 VALUES ('spam12@ksergey.ru', 'Тестовый 12')
INSERT INTO [ML].[Emails]
 VALUES ('spam13@ksergey.ru', 'Тестовый 13')
INSERT INTO [ML].[Emails]
 VALUES ('spam14@ksergey.ru', 'Тестовый 14')
INSERT INTO [ML].[Emails]
 VALUES ('spam15@ksergey.ru', 'Тестовый 15')