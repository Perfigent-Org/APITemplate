-----------FOR CREATE TEMPORAL TABLES-----------


USE YOUR_DATABASE_NAME
GO
IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = N'History')
BEGIN
	EXEC('CREATE SCHEMA History')
END
GO
DECLARE @Counter INT = 0, @TableName NVARCHAR(500)
DECLARE @TotalTables int = (SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA <> N'History' AND TABLE_TYPE = N'BASE TABLE')
DECLARE @Temporals NVARCHAR(Max);
WHILE (@Counter IS NOT NULL AND @Counter < @TotalTables)
BEGIN
    SELECT @TableName = TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA <> N'History' AND TABLE_TYPE = N'BASE TABLE' ORDER BY TABLE_NAME 
	OFFSET @Counter ROWS FETCH NEXT 1 ROWS ONLY
	
	PRINT('ALTER TABLE '+ @TableName +' ADD
		SysStartTime datetime2 GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT DF_SysStart_'+@TableName+' DEFAULT SYSUTCDATETIME(),
		SysEndTime datetime2 GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT DF_SysEnd_'+@TableName+' DEFAULT CONVERT(datetime2, ''9999-12-31 23:59:59.9999999''),
		PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime);
		
	ALTER TABLE '+ @TableName +' SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = History.'+ @TableName +'));
Go
');
	 
	SET @Counter  = @Counter + 1;
END


-----------FOR DELETE TEMPORAL TABLES-----------


USE YOUR_DATABASE_NAME
GO
DECLARE @Counter INT = 0, @TableName NVARCHAR(500)
DECLARE @TotalTables int = (SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES)


WHILE (@Counter IS NOT NULL AND @Counter < @TotalTables)
BEGIN
    SELECT @TableName = TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA <> N'History' ORDER BY TABLE_NAME 
	OFFSET @Counter ROWS FETCH NEXT 1 ROWS ONLY
	
	EXEC('IF OBJECTPROPERTY(OBJECT_ID('''+ @TableName +'''), ''TableTemporalType'') = 2
		BEGIN
		ALTER TABLE '+ @TableName +' SET (SYSTEM_VERSIONING = OFF)
		ALTER TABLE '+ @TableName +' DROP PERIOD FOR SYSTEM_TIME;
		ALTER TABLE '+ @TableName +' DROP CONSTRAINT DF_SysStart_'+@TableName+';
		ALTER TABLE '+ @TableName +' DROP CONSTRAINT DF_SysEnd_'+@TableName+';
		ALTER TABLE '+ @TableName +' DROP COLUMN SysStartTime;
		ALTER TABLE '+ @TableName +' DROP COLUMN SysEndTime;
		DROP TABLE History.'+ @TableName +'
		END'
		)

	SET @Counter  = @Counter + 1
END
 