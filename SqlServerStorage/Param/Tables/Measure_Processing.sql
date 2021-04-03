CREATE TABLE [Param].[Measure_Processing] (
    [id_measure_proc]   INT            IDENTITY (1, 1) NOT NULL,
    [name_measure_proc] VARCHAR (100)  NULL,
    [spec_measure_proc] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_id_measure_proc] PRIMARY KEY CLUSTERED ([id_measure_proc] ASC)
);

