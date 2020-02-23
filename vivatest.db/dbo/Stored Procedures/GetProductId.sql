-- =============================================
-- Author:      George Topcharas
-- Create Date: 22/2/2020
-- Description: GetProductId
-- =============================================
CREATE PROCEDURE GetProductId
(
    -- Add the parameters for the stored procedure here
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
SELECT [Products].[Id] from [Products]
INNER JOIN [GenericProducts] AS gr on [GenericProductId] = gr.Id  
INNER JOIN [Segments] AS seg on [SegmentId] = seg.Id
INNER JOIN [Countries] AS c on [CountryId] = c.Id
INNER JOIN [DiscountBands] db on [DiscountBandId] = db.Id
WHERE gr.[Name]= @Product AND seg.[Name]= @Segment AND
c.[Name]= @Country AND db.[Name]= @DiscountBand
END
