CREATE TABLE [Param].[Measure] (
    [id_measure]   INT            IDENTITY (1, 1) NOT NULL,
    [name_measure] VARCHAR (200)  NOT NULL,
    [spec_measure] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Measure] PRIMARY KEY CLUSTERED ([id_measure] ASC)
);

