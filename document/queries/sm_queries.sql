/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[CompanyName]
      ,[Address]
      ,[Phone]
      ,[City]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [SALES_MANAGEMENT_DB].[dbo].[Suppliers]