CREATE TABLE [Param].[Kind] (
    [id_kind]   INT            IDENTITY (1, 1) NOT NULL,
    [name_kind] VARCHAR (300)  NULL,
    [spec_kind] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Kind] PRIMARY KEY CLUSTERED ([id_kind] ASC)
);

