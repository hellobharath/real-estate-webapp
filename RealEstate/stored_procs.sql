/*Trigger to update availability of properties*/
USE RealEstateDB
GO
CREATE TRIGGER UpdateAvailability ON [dbo].[Agreement]
AFTER UPDATE
AS
BEGIN
	DECLARE @id int;
	DECLARE @status nvarchar(50);
	SELECT @id = Property_Id FROM inserted i;
	SELECT @status = i.Status FROM inserted i;
	IF (@status = 'Accepted')
	BEGIN
	UPDATE [dbo].[Ad]
	SET Status = 'Unavailable'
	WHERE Property_Id = @Id;
	END
	IF(@status = 'Rejected')
	BEGIN
	UPDATE [dbo].[Ad]
	SET Status = 'Available'
	WHERE Property_Id = @Id;
	END
END
GO
ALTER TABLE [dbo].[Agreement] ENABLE TRIGGER [UpdateAvailability]
GO

/*Stored Procedure to calculate total price*/
CREATE PROCEDURE [dbo].[CalcPrice] @id int,@ppsqft int, @length int, @width int 
AS
BEGIN

	DECLARE @Price int;
	SET @Price = @length * @width * @ppsqft;
	UPDATE Ad
	SET Price = @Price
	WHERE Id = @id;
END
GO
/*Trigger to set plot price*/
CREATE TRIGGER [dbo].[SetPrice] ON [dbo].[Plot]
AFTER INSERT 
AS
BEGIN
	DECLARE @plot_id nvarchar(128);
	DECLARE @id int;
	DECLARE @ppsqft int; 
	DECLARE @length int;
	DECLARE @width int;
	
	SELECT @plot_id = Id FROM inserted i;
	SELECT @id = Id FROM Ad WHERE Property_Id = @plot_id;
	SELECT @ppsqft = Price_Per_Sqft FROM Plot Where Id = @plot_id;
	SELECT @length = Length FROM Plot Where Id = @plot_id;
	SELECT @width = Width FROM Plot Where Id = @plot_id;
	EXEC [dbo].[CalcPrice] @id = @id,@ppsqft = @ppsqft,@length = @length,@width = @width;
END
GO

ALTER TABLE [dbo].[Plot] ENABLE TRIGGER [SetPrice]
GO