-- =============================================
-- Author:      George Topcharas
-- Create Date: 22/2/2020
-- Description: GetProductByProductId
-- =============================================
CREATE PROCEDURE GetProductByProductId
(
    -- Add the parameters for the stored procedure here
	@ProductId UNIQUEIDENTIFIER
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT OFF

    -- Insert statements for procedure here
SELECT p.[Id] Id, gp.[Name] AS Product, s.[Name] AS Segment, c.[Name] AS Country, db.[Name] AS DiscountBand 
FROM products AS p  
LEFT JOIN [GenericProducts] AS gp ON gp.Id = p.GenericProductId  
LEFT JOIN [Segments] AS s ON s.Id = p.SegmentId
LEFT JOIN [Countries] AS c ON c.Id = p.[CountryId]
LEFT JOIN [DiscountBands] AS db ON db.Id = p.[DiscountBandId]
WHERE p.Id = @ProductId
END
