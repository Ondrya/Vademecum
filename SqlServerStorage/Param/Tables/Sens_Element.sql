CREATE TABLE [Param].[Sens_Element] (
    [id_se]   INT           IDENTITY (1, 1) NOT NULL,
    [name_se] VARCHAR (100) NULL,
    [spec_se] VARCHAR (200) NULL,
    CONSTRAINT [PK_Sens_Element] PRIMARY KEY CLUSTERED ([id_se] ASC)
);

