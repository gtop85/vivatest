--Table containing all products with their ids and names
CREATE TABLE [dbo].[GenericProducts] (
    [Name] NVARCHAR (255)   NULL,
    [Id]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

