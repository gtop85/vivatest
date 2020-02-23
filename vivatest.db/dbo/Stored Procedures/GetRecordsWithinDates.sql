-- =============================================
-- Author:      George Topcharas
-- Create Date: 22/2/2020
-- Description: GetRecordsWithinDates
-- =============================================
CREATE PROCEDURE GetRecordsWithinDates
(
    -- Add the parameters for the stored procedure here
    @DateFrom datetime,
	@DateTo datetime
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT [ProductId],[UnitsSold],[ManufacturingPrice],[SalePrice],[GrossSales],[Discounts],
                                       [Sales],[SalesInEuro],[Profit],[CostOfGoodsSold],[Date]
                                       FROM [FinancialRecords]
                                       WHERE [Date] >= @DateFrom AND [Date] <= @DateTo ORDER BY [Date] DESC
END
