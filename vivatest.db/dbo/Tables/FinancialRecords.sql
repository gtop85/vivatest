--Main table of the application.
--Normalization forms were applied.
--From the initial excel file some redundunt columns (day, month, year) were removed.
--The profit column also was corrected with the proper negative values.
CREATE TABLE [dbo].[FinancialRecords] (
    [UnitsSold]          DECIMAL (19, 2)  NULL,
    [ManufacturingPrice] DECIMAL (19, 2)  NULL,
    [SalePrice]          DECIMAL (19, 2)  NULL,
    [GrossSales]         DECIMAL (19, 2)  NULL,
    [Discounts]          DECIMAL (19, 2)  NULL,
    [Sales]              DECIMAL (19, 2)  NULL,
    [CostOfGoodsSold]    DECIMAL (19, 2)  NULL,
    [Profit]             DECIMAL (19, 2)  NULL,
    [Date]               DATETIME         NULL,
    [SalesInEuro]        DECIMAL (19, 2)  DEFAULT (NULL) NULL,
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ProductId]          UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ProductId]
    ON [dbo].[FinancialRecords]([Id] ASC, [ProductId] ASC);

