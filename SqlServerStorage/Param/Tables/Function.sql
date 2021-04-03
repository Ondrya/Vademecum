CREATE TABLE [Param].[Function] (
    [id_func]   INT            IDENTITY (1, 1) NOT NULL,
    [name_func] VARCHAR (200)  NULL,
    [spec_func] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED ([id_func] ASC)
);

