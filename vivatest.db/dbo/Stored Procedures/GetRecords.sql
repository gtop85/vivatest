-- =============================================
-- Author:      George Topcharas
-- Create Date: 22/2/2020
-- Description: GetRecords
-- =============================================
CREATE PROCEDURE GetRecords
(
    -- Add the parameters for the stored procedure here
	@Id UNIQUEIDENTIFIER,
    @Product nvarchar(255),
	@Segment nvarchar(255),
	@Country nvarchar(255),
	@DiscountBand nvarchar(255)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT OFF

    -- Insert statements for procedure here
    SELECT [FinancialRecords].[Id],[ProductId],[UnitsSold],[ManufacturingPrice],[SalePrice],[GrossSales],
    [Discounts],[Sales],[SalesInEuro],[Profit],[CostOfGoodsSold],[Date]
    FROM [dbo].[FinancialRecords]
    INNER JOIN [Products] AS pr ON [ProductId] = pr.[Id]
    INNER JOIN [GenericProducts] AS gr ON [GenericProductId] = gr.Id  
    INNER JOIN [Segments] AS seg ON [SegmentId] = seg.Id
    INNER JOIN [Countries] AS c ON [CountryId] = c.Id
    INNER JOIN [DiscountBands] db ON [DiscountBandId] = db.Id
    WHERE gr.[Name] = IIF(@Product IS NULL, gr.[Name], @Product) AND
    seg.[Name] = IIF(@Segment IS NULL, seg.[Name], @Segment) AND
    c.[Name] = IIF(@Country IS NULL, c.[Name], @Country) AND
    db.[Name] = IIF(@DiscountBand IS NULL, db.[Name], @DiscountBand) AND
    [FinancialRecords].[Id] = IIF(@Id IS NULL, [FinancialRecords].[Id], @Id)
    ORDER BY [Date] DESC
END
