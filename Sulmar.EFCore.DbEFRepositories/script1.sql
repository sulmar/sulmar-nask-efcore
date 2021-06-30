CREATE OR ALTER FUNCTION dbo.CountCustomers(@isRemoved bit)
 RETURNS int
  AS
  BEGIN
	RETURN (SELECT count(*) from dbo.Customers WHERE IsRemoved = @isRemoved)
  END