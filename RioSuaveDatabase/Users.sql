﻿CREATE TABLE [dbo].[Users]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] BINARY(32) NOT NULL, 
    [Salt] BINARY(32) NOT NULL
)