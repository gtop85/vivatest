--Brigde table containing ids for the products varieties. Primary key and foreign keys are also set up.
CREATE TABLE [dbo].[Products] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GenericProductId] UNIQUEIDENTIFIER NULL,
    [CountryId]        UNIQUEIDENTIFIER NULL,
    [DiscountBandId]   UNIQUEIDENTIFIER NULL,
    [SegmentId]        UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]),
    FOREIGN KEY ([DiscountBandId]) REFERENCES [dbo].[DiscountBands] ([Id]),
    FOREIGN KEY ([GenericProductId]) REFERENCES [dbo].[GenericProducts] ([Id]),
    FOREIGN KEY ([SegmentId]) REFERENCES [dbo].[Segments] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [Product]
    ON [dbo].[Products]([Id] ASC, [GenericProductId] ASC, [CountryId] ASC, [DiscountBandId] ASC, [SegmentId] ASC);

