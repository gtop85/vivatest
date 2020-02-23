--Table containing all discount bands with their ids and names
CREATE TABLE [dbo].[DiscountBands] (
    [Name] NVARCHAR (255)   NULL,
    [Id]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

