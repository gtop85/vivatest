--Table containing all segments with their ids and names
CREATE TABLE [dbo].[Segments] (
    [Name] NVARCHAR (255)   NULL,
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

