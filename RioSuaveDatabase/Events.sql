﻿CREATE TABLE [dbo].[Events]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(150) NOT NULL, 
    [DateTimeStart] DATETIME2 NOT NULL, 
    [DateTimeEnd] DATETIME2 NOT NULL, 
    [Location] VARCHAR(150) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL
)
