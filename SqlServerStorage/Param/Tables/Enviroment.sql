CREATE TABLE [Param].[Enviroment] (
    [id_envi]   INT            IDENTITY (1, 1) NOT NULL,
    [name_envi] VARCHAR (100)  NULL,
    [spec_envi] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_id_envi] PRIMARY KEY CLUSTERED ([id_envi] ASC)
);

