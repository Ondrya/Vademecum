CREATE TABLE [Param].[Literature] (
    [id_lit]      INT             IDENTITY (1, 1) NOT NULL,
    [lit_type]    VARCHAR (100)   NULL,
    [lit_name]    VARCHAR (500)   NULL,
    [lit_author]  VARCHAR (200)   NULL,
    [lit_date]    INT             NULL,
    [lit_publish] VARCHAR (200)   NULL,
    [lit_web]     VARCHAR (MAX)   NULL,
    [file_name]   NVARCHAR (200)  NULL,
    [lit_file]    VARBINARY (MAX) NULL,
    CONSTRAINT [PK_Literature] PRIMARY KEY CLUSTERED ([id_lit] ASC)
);



