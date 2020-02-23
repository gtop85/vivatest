--Table containing all countries with their ids and names
CREATE TABLE [dbo].[Countries] (
    [Name] NVARCHAR (255)   NULL,
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

