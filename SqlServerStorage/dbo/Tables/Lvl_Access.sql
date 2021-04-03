CREATE TABLE [dbo].[Lvl_Access] (
    [lvl_access] INT           IDENTITY (1, 1) NOT NULL,
    [lvl_name]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_Lvl_Access] PRIMARY KEY CLUSTERED ([lvl_access] ASC)
);

