CREATE TABLE [Param].[Built_Tech] (
    [id_built_tech] INT            IDENTITY (1, 1) NOT NULL,
    [built_tech]    VARCHAR (50)   NULL,
    [spec_built]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Built_Tech] PRIMARY KEY CLUSTERED ([id_built_tech] ASC)
);



