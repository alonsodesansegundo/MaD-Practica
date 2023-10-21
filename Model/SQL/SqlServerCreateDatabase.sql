/* SQL Script */

 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]

/***** Drop database if already exists  ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'shopping')
DROP DATABASE [shopping]

USE [master]

/* DataBase Creation */
								  
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [shopping] 
	ON PRIMARY ( NAME = shopping, FILENAME = "' + @Default_DB_Path + N'shopping.mdf")
	LOG ON ( NAME = shopping_log, FILENAME = "' + @Default_DB_Path + N'shopping_log.ldf")'

EXEC(@sql)
PRINT N'Database [shopping] created.'
GO