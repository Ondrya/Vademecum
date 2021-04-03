CREATE TABLE [dbo].[User] (
    [user_id]    INT           IDENTITY (1, 1) NOT NULL,
    [login]      NVARCHAR (50) NULL,
    [password]   NVARCHAR (50) NULL,
    [name]       NVARCHAR (50) NULL,
    [surname]    NVARCHAR (50) NULL,
    [patronymic] NVARCHAR (50) NULL,
    [lvl_access] INT           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([user_id] ASC),
    CONSTRAINT [FK_User_Lvl_Access] FOREIGN KEY ([lvl_access]) REFERENCES [dbo].[Lvl_Access] ([lvl_access]) ON DELETE CASCADE ON UPDATE CASCADE
);

