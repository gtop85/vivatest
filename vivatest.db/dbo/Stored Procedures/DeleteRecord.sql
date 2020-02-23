-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <22,2,2020 >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE DeleteRecord
(
    -- Add the parameters for the stored procedure here
    @Id UNIQUEIDENTIFIER
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT OFF

    -- Insert statements for procedure here
    DELETE FROM [dbo].[FinancialRecords] WHERE Id = @Id
END
