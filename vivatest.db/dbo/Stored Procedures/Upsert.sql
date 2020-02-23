-- =============================================
-- Author:      GeorgeTopcharas
-- Create Date: 22/2/2020
-- Description: Upsert
-- =============================================
CREATE PROCEDURE Upsert
(
    -- Add the parameters for the stored procedure here
	@Id UNIQUEIDENTIFIER,
    @ProductId UNIQUEIDENTIFIER,
	@UnitsSold decimal(19, 2),
	@ManufacturingPrice decimal(19, 2),
	@SalePrice decimal(19, 2),
	@GrossSales decimal(19, 2),
	@Discounts decimal(19, 2),
	@Sales decimal(19, 2),
	@SalesInEuro decimal(19, 2),
	@Profit decimal(19, 2),
	@CostOfGoodsSold decimal(19, 2), 
	@Date datetime
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT OFF

    -- Insert statements for procedure here
    BEGIN TRAN
      IF EXISTS (SELECT * FROM [FinancialRecords] WITH (UPDLOCK,SERIALIZABLE) WHERE Id = @Id)
      BEGIN
          UPDATE [FinancialRecords] SET [ProductId] = @ProductId,[UnitsSold] = @UnitsSold,[ManufacturingPrice] = @ManufacturingPrice,
                                        [SalePrice] = @SalePrice,[GrossSales] = @GrossSales,[Discounts] = @Discounts,[Sales] = @Sales,
                                        [SalesInEuro] = @SalesInEuro,[Profit] = @Profit,[CostOfGoodsSold] = @CostOfGoodsSold,[Date] = @Date
      								    OUTPUT [INSERTED].[Id]
                                        WHERE Id = @Id
      END
      ELSE
      BEGIN
          INSERT INTO [FinancialRecords] ([ProductId],[UnitsSold],[ManufacturingPrice],[SalePrice],[GrossSales],[Discounts],
                                          [Sales],[SalesInEuro],[Profit],[CostOfGoodsSold],[Date])
                                          OUTPUT [INSERTED].[Id]
                                          VALUES(@ProductId,@UnitsSold,@ManufacturingPrice,@SalePrice,@GrossSales,@Discounts,
                                          @Sales,@SalesInEuro,@Profit,@CostOfGoodsSold,@Date)
         
      END
    COMMIT TRAN
END
